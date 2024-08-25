Sub RESET_DIGITAL_NOTE_REPORT()
    Dim reportSheet As Worksheet
    Dim lastRow As Long
    
    ' Set the report sheet
    Set reportSheet = ThisWorkbook.Sheets("DIGITAL NOTE REPORT")

    ' Find the last row in the sheet (where the previous totals row might be)
    lastRow = reportSheet.Cells(reportSheet.Rows.Count, 1).End(xlUp).Row

    ' If the last row contains "TOTAL", reset its row height before clearing
    If reportSheet.Cells(lastRow, 1).Value = "TOTAL" Then
        reportSheet.Rows(lastRow).RowHeight = reportSheet.StandardHeight ' Reset to default row height
    End If
    
    If reportSheet.Cells(lastRow, 1).Value = "TOTAL" Then
    End If
    
    ' Clear all contents (values, formulas) and formats starting from row 3 onwards
    With reportSheet.Rows("3:" & reportSheet.Rows.Count)
        .ClearContents
        .ClearFormats
        .HorizontalAlignment = xlCenter
        .VerticalAlignment = xlCenter
    End With
End Sub

Sub REGENERATE_DIGITAL_NOTE_REPORT()
    Application.ScreenUpdating = False ' Turn off screen updating
    Application.Calculation = xlCalculationManual ' Turn off automatic calculation

    ' Reset the DIGITAL NOTE Report to its initial state
    Call RESET_DIGITAL_NOTE_REPORT
    
    Dim dataSheet As Worksheet
    Dim reportSheet As Worksheet
    Dim lastRow As Long
    Dim i As Long
    Dim reportRow As Long
    
    ' Underliers (Sub-row for each underlier, highlight active underlier, and append performance)
    Dim underliers As String
    Dim activeUnderlier As String
    Dim underlierList As Variant
    Dim j As Integer
    Dim rowsToMerge As Long

    ' Set the data sheet and report sheet
    Set dataSheet = ThisWorkbook.Sheets("DATA SHEET")
    Set reportSheet = ThisWorkbook.Sheets("DIGITAL NOTE REPORT")

    ' Find the last row in the data sheet
    lastRow = dataSheet.Cells(dataSheet.Rows.Count, "A").End(xlUp).Row
    
   ' Check if the data sheet is empty (i.e., only headers or completely empty)
    If lastRow <= 1 Then
        MsgBox "The DATA SHEET is empty. No data to generate the report.", vbExclamation
        Application.ScreenUpdating = True ' Turn screen updating back on
        Application.Calculation = xlCalculationAutomatic ' Turn calculation back on
        Exit Sub
    End If

    ' Start populating the report from row 3 onwards
    reportRow = 3
    
    ' Initialize total variables
    totalAmtInvested = 0
    totalCurrentValue = 0
    totalIntrinsicValue = 0
    totalCurrentValuePercent = 0
    totalIntrinsicValuePercent = 0
    
    ' Loop through the data sheet and copy data to the report sheet
    For i = 2 To lastRow
        
        ' Issuer/CUSIP
        reportSheet.Cells(reportRow, 1).Value = dataSheet.Cells(i, 3).Value & ", " & dataSheet.Cells(i, 1).Value
        
        ' Term (Calculate difference in months between Maturity Date and Issue Date, and append "M")
        reportSheet.Cells(reportRow, 2).Value = DateDiff("m", dataSheet.Cells(i, 19).Value, dataSheet.Cells(i, 6).Value) & "M"
        
        ' Redemption (Maturity Date)
        reportSheet.Cells(reportRow, 3).Value = dataSheet.Cells(i, 6).Value
        
        ' Amt Invested (Force USD Currency Formatting)
        reportSheet.Cells(reportRow, 4).Value = dataSheet.Cells(i, 8).Value
        reportSheet.Cells(reportRow, 4).NumberFormat = "[$$-409]#,##0"
        
        ' Current Value (Force USD Currency Formatting)
        reportSheet.Cells(reportRow, 5).Value = dataSheet.Cells(i, 5).Value * dataSheet.Cells(i, 8).Value
        reportSheet.Cells(reportRow, 5).NumberFormat = "[$$-409]#,##0"
        
        ' Current Value % (Correct calculation, round to 2 decimal places, and append "%" as a string)
        reportSheet.Cells(reportRow, 6).Value = Round(dataSheet.Cells(i, 5).Value - 100, 2) & "%"

        ' Intrinsic Value (Force USD Currency Formatting)
        reportSheet.Cells(reportRow, 7).Value = dataSheet.Cells(i, 8).Value * dataSheet.Cells(i, 22).Value
        reportSheet.Cells(reportRow, 7).NumberFormat = "[$$-409]#,##0"
        
        ' Intrinsic Value % (Correct calculation, round to 2 decimal places, and append "%" as a string)
        reportSheet.Cells(reportRow, 8).Value = Round(dataSheet.Cells(i, 22).Value - 100, 2) & "%"
        
        ' Accumulate totals for the relevant columns
        totalAmtInvested = totalAmtInvested + dataSheet.Cells(i, 8).Value
        totalCurrentValue = totalCurrentValue + reportSheet.Cells(reportRow, 5).Value
        totalIntrinsicValue = totalIntrinsicValue + reportSheet.Cells(reportRow, 7).Value
        totalCurrentValuePercent = totalCurrentValuePercent + (dataSheet.Cells(i, 5).Value - 100)
        totalIntrinsicValuePercent = totalIntrinsicValuePercent + (dataSheet.Cells(i, 22).Value - 100)
        
        ' Protection (Buffer or Barrier)
        If InStr(1, dataSheet.Cells(i, 4).Value, "Trigger") > 0 Or InStr(1, dataSheet.Cells(i, 4).Value, "Buffer") > 0 Then
            reportSheet.Cells(reportRow, 9).Value = Round(dataSheet.Cells(i, 16).Value - dataSheet.Cells(i, 11).Value, 0) & "% Buffer"
        Else
            reportSheet.Cells(reportRow, 9).Value = Round(dataSheet.Cells(i, 16).Value - dataSheet.Cells(i, 11).Value, 0) & "% Barrier"
        End If
        
        ' Protection Level (Percentage Formatting)
        reportSheet.Cells(reportRow, 10).Value = dataSheet.Cells(i, 16).Value & "%"
        
        ' Features
        reportSheet.Cells(reportRow, 12).Value = dataSheet.Cells(i, 4).Value
        
        ' Underliers: Split into sub-rows and highlight the active underlier
        underliers = Replace(Replace(dataSheet.Cells(i, 12).Value, "[", ""), "]", "")
        activeUnderlier = dataSheet.Cells(i, 13).Value
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
            reportSheet.Range(reportSheet.Cells(reportRow, 12), reportSheet.Cells(reportRow + rowsToMerge - 1, 12)).Merge ' Features
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
                reportSheet.Cells(reportRow, 11).Value = underlierList(j) & " " & dataSheet.Cells(i, 11).Value & "%"
                reportSheet.Cells(reportRow, 11).Interior.Color = RGB(169, 208, 142) ' Highlight in light green
            Else
                ' Just add the underlier
                reportSheet.Cells(reportRow, 11).Value = underlierList(j)
                ' Remove background color for non-active underliers
                reportSheet.Cells(reportRow, 11).Interior.ColorIndex = xlNone
            End If
        Next j
        
        ' Move to the next row in the report sheet
        reportRow = reportRow + 1
    Next i
    
    ' Insert the Total row
    reportSheet.Cells(reportRow, 1).Value = "TOTAL"
    reportSheet.Cells(reportRow, 1).Font.Bold = True
    
    ' Add totals to the appropriate columns
    reportSheet.Cells(reportRow, 4).Value = totalAmtInvested
    reportSheet.Cells(reportRow, 4).NumberFormat = "[$$-409]#,##0"
    reportSheet.Cells(reportRow, 4).Font.Bold = True
    
    reportSheet.Cells(reportRow, 5).Value = totalCurrentValue
    reportSheet.Cells(reportRow, 5).NumberFormat = "[$$-409]#,##0"
    reportSheet.Cells(reportRow, 5).Font.Bold = True
    
    reportSheet.Cells(reportRow, 6).Value = Round(totalCurrentValuePercent / (lastRow - 1), 2) & "%"
    reportSheet.Cells(reportRow, 6).Font.Bold = True
    
    reportSheet.Cells(reportRow, 7).Value = totalIntrinsicValue
    reportSheet.Cells(reportRow, 7).NumberFormat = "[$$-409]#,##0"
    reportSheet.Cells(reportRow, 7).Font.Bold = True
    
    reportSheet.Cells(reportRow, 8).Value = Round(totalIntrinsicValuePercent / (lastRow - 1), 2) & "%"
    reportSheet.Cells(reportRow, 8).Font.Bold = True
    
    ' Style the total row
    reportSheet.Range(reportSheet.Cells(reportRow, 1), reportSheet.Cells(reportRow, 12)).Interior.Color = RGB(221, 235, 247) ' Light blue background
    
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
    With reportSheet.Range(reportSheet.Cells(reportRow, 1), reportSheet.Cells(reportRow, 12)).Borders
        .LineStyle = xlContinuous
        .Color = RGB(0, 0, 0)
        .Weight = xlThin
    End With

    ' Turn screen updating and calculation back on
    Application.ScreenUpdating = True
    Application.Calculation = xlCalculationAutomatic
End Sub

