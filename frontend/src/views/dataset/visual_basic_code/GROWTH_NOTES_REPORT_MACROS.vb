Sub RESET_GROWTH_NOTES_REPORT()
    Dim reportSheet As Worksheet
    Set reportSheet = ThisWorkbook.Sheets("GROWTH NOTES REPORT")

    Call Utils.ResetReport(reportSheet)
End Sub

Sub REGENERATE_GROWTH_NOTES_REPORT()
    Application.ScreenUpdating = False ' Turn off screen updating
    Application.Calculation = xlCalculationManual ' Turn off automatic calculation

    Dim dataSheet As Worksheet
    Dim reportSheet As Worksheet
    
    ' Set the data sheet and report sheet
    Set dataSheet = ThisWorkbook.Sheets("DATA SHEET")
    Set reportSheet = ThisWorkbook.Sheets("GROWTH NOTES REPORT")
    
    ' Check if the data sheet is empty (i.e., only headers or completely empty)
    If Utils.IsEmptySheet(dataSheet) Then
        MsgBox "The DATA SHEET is empty. No data available to generate the report.", vbExclamation
        Application.ScreenUpdating = True ' Turn screen updating back on
        Application.Calculation = xlCalculationAutomatic ' Turn calculation back on
        Exit Sub
    End If
    
    ' Reset the report sheet to its initial state
    Call Utils.ResetReport(reportSheet)
    
    ' Determine data sheet last row
    Dim dataSheetLastRow As Long
    dataSheetLastRow = dataSheet.Cells(dataSheet.Rows.Count, 1).End(xlUp).Row

    Dim i As Long
    Dim reportRow As Long
    
    ' Underliers (Sub-row for each underlier, highlight active underlier, and append performance)
    Dim underliers As String
    Dim activeUnderlier As String
    Dim underlierList As Variant
    Dim j As Integer
    Dim rowsToMerge As Long

    ' Start populating the report from row 3 onwards
    reportRow = 3
    
    ' Initialize total variables
    totalRows = 0
    totalAmtInvested = 0
    totalCurrentValue = 0
    totalIntrinsicValue = 0
    totalCurrentValuePercent = 0
    totalIntrinsicValuePercent = 0
    
    ' Loop through the data sheet and copy data to the report sheet
    For i = 2 To dataSheetLastRow
        ' Check if Return Type is "Growth"
        If dataSheet.Cells(i, 20).value <> "Growth" Then
            ' Skip to the next iteration if Return Type is not "Growth"
            GoTo SkipRow
        End If

        totalRows = totalRows + 1
        
        ' Issuer/CUSIP
        reportSheet.Cells(reportRow, 1).value = dataSheet.Cells(i, 3).value & ", " & dataSheet.Cells(i, 1).value
        
        ' Term (Calculate difference in months between Maturity Date and Issue Date, and append "M")
        reportSheet.Cells(reportRow, 2).value = DateDiff("m", dataSheet.Cells(i, 19).value, dataSheet.Cells(i, 6).value) & "M"
        
        ' Redemption (Maturity Date)
        reportSheet.Cells(reportRow, 3).value = dataSheet.Cells(i, 6).value
        
        ' Amt Invested (Force USD Currency Formatting)
        reportSheet.Cells(reportRow, 4).value = dataSheet.Cells(i, 8).value
        reportSheet.Cells(reportRow, 4).NumberFormat = "[$$-409]#,##0"
        
        ' Current Value (Force USD Currency Formatting)
        reportSheet.Cells(reportRow, 5).value = dataSheet.Cells(i, 5).value * dataSheet.Cells(i, 8).value / 100
        reportSheet.Cells(reportRow, 5).NumberFormat = "[$$-409]#,##0"
        
        ' Current Value % (Correct calculation, round to 2 decimal places, and append "%" as a string)
        reportSheet.Cells(reportRow, 6).value = Round(dataSheet.Cells(i, 5).value - 100, 2) & "%"

        ' Intrinsic Value (Force USD Currency Formatting)
        reportSheet.Cells(reportRow, 7).value = dataSheet.Cells(i, 8).value * dataSheet.Cells(i, 22).value / 100
        reportSheet.Cells(reportRow, 7).NumberFormat = "[$$-409]#,##0"
        
        ' Intrinsic Value % (Correct calculation, round to 2 decimal places, and append "%" as a string)
        reportSheet.Cells(reportRow, 8).value = dataSheet.Cells(i, 22).value & "%"
        
        ' Accumulate totals for the relevant columns
        totalAmtInvested = totalAmtInvested + dataSheet.Cells(i, 8).value
        totalCurrentValue = totalCurrentValue + reportSheet.Cells(reportRow, 5).value
        totalIntrinsicValue = totalIntrinsicValue + reportSheet.Cells(reportRow, 7).value
        totalCurrentValuePercent = totalCurrentValuePercent + (dataSheet.Cells(i, 5).value - 100)
        totalIntrinsicValuePercent = totalIntrinsicValuePercent + dataSheet.Cells(i, 22).value
        
        ' Protection (Buffer or Barrier)
        If InStr(1, dataSheet.Cells(i, 4).value, "Trigger") > 0 Or InStr(1, dataSheet.Cells(i, 4).value, "Buffer") > 0 Then
            reportSheet.Cells(reportRow, 9).value = Round(dataSheet.Cells(i, 16).value - dataSheet.Cells(i, 11).value, 0) & "% Buffer"
        Else
            reportSheet.Cells(reportRow, 9).value = Round(dataSheet.Cells(i, 16).value - dataSheet.Cells(i, 11).value, 0) & "% Barrier"
        End If
        
        ' Protection Level (Percentage Formatting)
        reportSheet.Cells(reportRow, 10).value = dataSheet.Cells(i, 16).value & "%"
        
        ' Max Return
        If dataSheet.Cells(i, 29).value = "" Or dataSheet.Cells(i, 29).value <= 0 Then
            reportSheet.Cells(reportRow, 11).value = "Unlimited"
        Else
            reportSheet.Cells(reportRow, 11).value = dataSheet.Cells(i, 29).value
        End If
        
        ' Upside Participation (Percentage Formatting)
        reportSheet.Cells(reportRow, 12).value = dataSheet.Cells(i, 30).value & "%"
        
        ' Features
        reportSheet.Cells(reportRow, 14).value = dataSheet.Cells(i, 4).value
        
        ' Underliers: Split into sub-rows and highlight the active underlier
        underliers = Replace(Replace(dataSheet.Cells(i, 12).value, "[", ""), "]", "")
        activeUnderlier = dataSheet.Cells(i, 13).value
        underlierList = Split(underliers, ",")
        
        ' Calculate how many rows we need to merge
        rowsToMerge = UBound(underlierList) - LBound(underlierList) + 1
        
        ' Merge the relevant cells
        If rowsToMerge > 1 Then
            reportSheet.Range(reportSheet.Cells(reportRow, 1), reportSheet.Cells(reportRow + rowsToMerge - 1, 1)).Merge ' Issuer/CUSIP
            reportSheet.Range(reportSheet.Cells(reportRow, 2), reportSheet.Cells(reportRow + rowsToMerge - 1, 2)).Merge ' Term
            reportSheet.Range(reportSheet.Cells(reportRow, 3), reportSheet.Cells(reportRow + rowsToMerge - 1, 3)).Merge ' Redemption
            reportSheet.Range(reportSheet.Cells(reportRow, 4), reportSheet.Cells(reportRow + rowsToMerge - 1, 4)).Merge ' Amt Invested
            reportSheet.Range(reportSheet.Cells(reportRow, 5), reportSheet.Cells(reportRow + rowsToMerge - 1, 5)).Merge ' Current Value
            reportSheet.Range(reportSheet.Cells(reportRow, 6), reportSheet.Cells(reportRow + rowsToMerge - 1, 6)).Merge ' Current Value %
            reportSheet.Range(reportSheet.Cells(reportRow, 7), reportSheet.Cells(reportRow + rowsToMerge - 1, 7)).Merge ' Intrinsic Value
            reportSheet.Range(reportSheet.Cells(reportRow, 8), reportSheet.Cells(reportRow + rowsToMerge - 1, 8)).Merge ' Intrinsic Value %
            reportSheet.Range(reportSheet.Cells(reportRow, 9), reportSheet.Cells(reportRow + rowsToMerge - 1, 9)).Merge ' Protection
            reportSheet.Range(reportSheet.Cells(reportRow, 10), reportSheet.Cells(reportRow + rowsToMerge - 1, 10)).Merge ' Protection Level
            reportSheet.Range(reportSheet.Cells(reportRow, 11), reportSheet.Cells(reportRow + rowsToMerge - 1, 11)).Merge ' Max Return
            reportSheet.Range(reportSheet.Cells(reportRow, 12), reportSheet.Cells(reportRow + rowsToMerge - 1, 12)).Merge ' Upside Participation
            reportSheet.Range(reportSheet.Cells(reportRow, 14), reportSheet.Cells(reportRow + rowsToMerge - 1, 14)).Merge ' Features
        End If
        
        ' Insert underliers into separate rows
        For j = LBound(underlierList) To UBound(underlierList)
            If j > 0 Then
                reportRow = reportRow + 1
            End If
            
            ' Trim the underlier and add it to the new row
            underlierList(j) = Trim(underlierList(j))
            
            ' Check if this is the active underlier
            If underlierList(j) = activeUnderlier Then
                ' Append performance and highlight active underlier
                reportSheet.Cells(reportRow, 13).value = underlierList(j) & " " & dataSheet.Cells(i, 11).value & "%"
                reportSheet.Cells(reportRow, 13).Interior.Color = RGB(169, 208, 142) ' Highlight in light green
            Else
                ' Just add the underlier
                reportSheet.Cells(reportRow, 13).value = underlierList(j)
                ' Remove background color for non-active underliers
                reportSheet.Cells(reportRow, 13).Interior.ColorIndex = xlNone
            End If
        Next j
        
        ' Move to the next row in the report sheet
        reportRow = reportRow + 1
    SkipRow:
    Next i
    
    ' Insert the Total row
    reportSheet.Cells(reportRow, 1).value = "TOTAL"
    reportSheet.Cells(reportRow, 1).Font.Bold = True
    
    ' Add totals to the appropriate columns
    reportSheet.Cells(reportRow, 4).value = totalAmtInvested
    reportSheet.Cells(reportRow, 4).NumberFormat = "[$$-409]#,##0"
    reportSheet.Cells(reportRow, 4).Font.Bold = True
    
    reportSheet.Cells(reportRow, 5).value = totalCurrentValue
    reportSheet.Cells(reportRow, 5).NumberFormat = "[$$-409]#,##0"
    reportSheet.Cells(reportRow, 5).Font.Bold = True
    
    reportSheet.Cells(reportRow, 6).value = Round(totalCurrentValuePercent / totalRows, 2) & "%"
    reportSheet.Cells(reportRow, 6).Font.Bold = True
    
    reportSheet.Cells(reportRow, 7).value = totalIntrinsicValue
    reportSheet.Cells(reportRow, 7).NumberFormat = "[$$-409]#,##0"
    reportSheet.Cells(reportRow, 7).Font.Bold = True
    
    reportSheet.Cells(reportRow, 8).value = Round(totalIntrinsicValuePercent / totalRows, 2) & "%"
    reportSheet.Cells(reportRow, 8).Font.Bold = True
    
    ' Style the total row
    reportSheet.Range(reportSheet.Cells(reportRow, 1), reportSheet.Cells(reportRow, 14)).Interior.Color = RGB(221, 235, 247) ' Light blue background
    
    ' Increase the height of the total row
    reportSheet.Rows(reportRow).RowHeight = 60 ' Adjust the height as needed
    
    ' Merge the first three columns (A, B, C) for the total row
    reportSheet.Range(reportSheet.Cells(reportRow, 1), reportSheet.Cells(reportRow, 3)).Merge

    ' Set the font size and style for the totals row
    With reportSheet.Rows(reportRow)
        .Font.Name = "Arial"
        .Font.Size = 14
    End With

    ' Add black borders to the cells
    With reportSheet.Range(reportSheet.Cells(reportRow, 1), reportSheet.Cells(reportRow, 14)).Borders
        .LineStyle = xlContinuous
        .Color = RGB(0, 0, 0)
        .Weight = xlThin
    End With

    ' Turn screen updating and calculation back on
    Application.ScreenUpdating = True
    Application.Calculation = xlCalculationAutomatic
End Sub
