Public Function FillReportCell(reportSheet As Worksheet, _
                               rowIndex As Long, _
                               columnIndex As Long, _
                               dataSheetRows As Variant, _
                               reportColumn As Variant, _
                               dataSheetColumns As Collection, _ 
                               reportRow As Long) As Variant
    Dim cellValue As Variant
    Dim cusipColIndex As Long
    Dim issuerColIndex As Long
    Dim maturityDateColIndex As Long
    Dim issueDateColIndex As Long
    Dim amtInvestedColIndex As Long
    Dim markToMarketColIndex As Long
    Dim totalNotionalColIndex As Long
    Dim intrinsicValueColIndex As Long
    Dim structureTypeColIndex As Long
    Dim protectionProximityColIndex As Long
    Dim underlierPerformanceColIndex As Long
    Dim maxReturnColIndex As Long
    Dim upsideParticipationColIndex As Long
    Dim annualYieldColIndex As Long
    Dim paymentsReceivedColIndex As Long
    Dim underliersColIndex As Long
    Dim activeUnderlierColIndex As Long
    
    ' Check if the reporting column is "Issuer/CUSIP"
    If reportColumn(2) = "Issuer/CUSIP" Then
        ' Validate that both "Issuer" and "Cusip" columns exist in dataSheetColumns
        On Error Resume Next
        issuerColIndex = dataSheetColumns("Issuer")
        cusipColIndex = dataSheetColumns("Cusip")
        On Error GoTo 0

        If issuerColIndex > 0 And cusipColIndex > 0 Then
            ' Calculate cellValue by combining Issuer and Cusip values
            cellValue = dataSheetRows(rowIndex, issuerColIndex) & "/" & dataSheetRows(rowIndex, cusipColIndex)
        End If

    ' Check if the reporting column is "Term"
    ElseIf reportColumn(2) = "Term" Then
        ' Validate that both "Maturity Date" and "Issue Date" columns exist in dataSheetColumns
        On Error Resume Next
        maturityDateColIndex = dataSheetColumns("Maturity Date")
        issueDateColIndex = dataSheetColumns("Issue Date")
        On Error GoTo 0
        
        If maturityDateColIndex > 0 And issueDateColIndex > 0 Then
            ' Calculate the difference in months between Maturity Date and Issue Date, and append "M"
            cellValue = DateDiff("m", dataSheetRows(rowIndex, issueDateColIndex), dataSheetRows(rowIndex, maturityDateColIndex)) & "M"
        End If

    ' Check if the reporting column is "Redemption"
    ElseIf reportColumn(2) = "Redemption" Then
        ' Validate that the "Maturity Date" column exists in dataSheetColumns
        On Error Resume Next
        maturityDateColIndex = dataSheetColumns("Maturity Date")
        On Error GoTo 0
        
        If maturityDateColIndex > 0 Then
            ' Set cellValue to the value of the Maturity Date
            cellValue = dataSheetRows(rowIndex, maturityDateColIndex)
        End If

    ' Check if the reporting column is "Amt Invested"
    ElseIf reportColumn(2) = "Amt Invested" Then
        ' Validate that the "Amt Invested" column exists in dataSheetColumns
        On Error Resume Next
        amtInvestedColIndex = dataSheetColumns("Total Notional (USD)")
        On Error GoTo 0
        
        If amtInvestedColIndex > 0 Then
            ' Set cellValue to the value of the Amt Invested
            cellValue = dataSheetRows(rowIndex, amtInvestedColIndex)
            ' Apply USD currency formatting to the cell in the report sheet
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "[$$-409]#,##0"
        End If

    ' Check if the reporting column is "Current Value"
    ElseIf reportColumn(2) = "Current Value" Then
        ' Validate that the necessary columns exist in dataSheetColumns
        On Error Resume next
        markToMarketColIndex = dataSheetColumns("Mark To Market Value")
        totalNotionalColIndex = dataSheetColumns("Total Notional (USD)")
        On Error GoTo 0
        
        If markToMarketColIndex > 0 And totalNotionalColIndex > 0 Then
            ' Calculate the Current Value
            cellValue = dataSheetRows(rowIndex, markToMarketColIndex) * dataSheetRows(rowIndex, totalNotionalColIndex) / 100
            ' Apply USD currency formatting to the cell in the report sheet
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "[$$-409]#,##0"
        End If

    ' Check if the reporting column is "Current Value %"
    ElseIf reportColumn(2) = "Current Value %" Then
        ' Validate that the necessary column exists in dataSheetColumns
        On Error Resume Next
        markToMarketColIndex = dataSheetColumns("Mark To Market Value")
        On Error GoTo 0
        
        If markToMarketColIndex > 0 Then
            ' Calculate the Current Value %: (Mark To Market Value - 100) rounded to 2 decimal places, and append "%"
            cellValue = Round(dataSheetRows(rowIndex, markToMarketColIndex) - 100, 2) / 100

            ' Apply percentage formatting
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "0.00%"
        End If

    ' Check if the reporting column is "Intrinsic Value"
    ElseIf reportColumn(2) = "Intrinsic Value" Then
        ' Validate that the necessary columns exist in dataSheetColumns
        On Error Resume Next
        totalNotionalColIndex = dataSheetColumns("Total Notional (USD)")
        intrinsicValueColIndex = dataSheetColumns("Intrinsic Value")
        On Error GoTo 0
        
        If totalNotionalColIndex > 0 And intrinsicValueColIndex > 0 Then
            ' Calculate the Intrinsic Value
            cellValue = dataSheetRows(rowIndex, totalNotionalColIndex) * dataSheetRows(rowIndex, intrinsicValueColIndex) / 100
            ' Apply USD currency formatting to the cell in the report sheet
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "[$$-409]#,##0"
        End If

    ' Check if the reporting column is "Intrinsic Value %"
    ElseIf reportColumn(2) = "Intrinsic Value %" Then
        ' Validate that the "Intrinsic Value" column exists in dataSheetColumns
        On Error Resume Next
        intrinsicValueColIndex = dataSheetColumns("Intrinsic Value")
        On Error GoTo 0
        
        If intrinsicValueColIndex > 0 Then
            ' Calculate the Intrinsic Value %, round to 2 decimal places, and append "%"
            cellValue = Round(dataSheetRows(rowIndex, intrinsicValueColIndex), 2) / 100

            ' Apply percentage formatting
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "0.00%"
        End If

    ' Check if the reporting column is "Protection"
    ElseIf reportColumn(2) = "Protection" Then
        ' Validate that the necessary columns exist in dataSheetColumns
        On Error Resume Next
        structureTypeColIndex = dataSheetColumns("Structure Type")
        protectionProximityColIndex = dataSheetColumns("Protection Proximity Level Abs")
        underlierPerformanceColIndex = dataSheetColumns("Underlier Performance Percent")
        On Error GoTo 0
        
        If structureTypeColIndex > 0 And protectionProximityColIndex > 0 And underlierPerformanceColIndex > 0 Then
            ' Determine whether "Trigger" or "Buffer" is present in the "Structure Type"
            If InStr(1, dataSheetRows(rowIndex, structureTypeColIndex), "Trigger") > 0 Or _
            InStr(1, dataSheetRows(rowIndex, structureTypeColIndex), "Buffer") > 0 Then
                ' Calculate and set the value as "% Buffer"
                cellValue = Round(dataSheetRows(rowIndex, protectionProximityColIndex) - dataSheetRows(rowIndex, underlierPerformanceColIndex), 0) & "% Buffer"
            Else
                ' Calculate and set the value as "% Barrier"
                cellValue = Round(dataSheetRows(rowIndex, protectionProximityColIndex) - dataSheetRows(rowIndex, underlierPerformanceColIndex), 0) & "% Barrier"
            End If
        End If

    ' Check if the reporting column is "Protection Level"
    ElseIf reportColumn(2) = "Protection Level" Then
        ' Validate that the "Protection Proximity Level Abs" column exists in dataSheetColumns
        On Error Resume Next        
        protectionProximityColIndex = dataSheetColumns("Protection Proximity Level Abs")
        On Error GoTo 0
        
        If protectionProximityColIndex > 0 Then
            ' Set the Protection Level with Percentage Formatting
            cellValue = dataSheetRows(rowIndex, protectionProximityColIndex) / 100

            ' Apply percentage formatting
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "0.00%"
        End If

    ' Check if the reporting column is "Max Return"
    ElseIf reportColumn(2) = "Max Return" Then
        ' Validate that the "Max Return" column exists in dataSheetColumns
        On Error Resume Next
        maxReturnColIndex = dataSheetColumns("Max Return")
        On Error GoTo 0
        
        If maxReturnColIndex > 0 Then
            ' Check if the Max Return value is empty or less than or equal to 0
            If dataSheetRows(rowIndex, maxReturnColIndex) = "" Or dataSheetRows(rowIndex, maxReturnColIndex) <= 0 Then
                cellValue = "Unlimited"
            Else
                cellValue = dataSheetRows(rowIndex, maxReturnColIndex)
            End If
        End If

    ' Check if the reporting column is "Upside Participation"
    ElseIf reportColumn(2) = "Upside Participation" Then
        ' Validate that the "Upside Participation Rate" column exists in dataSheetColumns
        On Error Resume Next
        upsideParticipationColIndex = dataSheetColumns("Upside Participation Rate")
        On Error GoTo 0
        
        If upsideParticipationColIndex > 0 Then
            ' Set the Upside Participation with Percentage Formatting
            cellValue = dataSheetRows(rowIndex, upsideParticipationColIndex) / 100

            ' Apply percentage formatting
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "0.00%"
        End If

    ' Check if the reporting column is "Features"
    ElseIf reportColumn(2) = "Features" Then
        ' Validate that the "Structure Type" column exists in dataSheetColumns
        On Error Resume Next
        structureTypeColIndex = dataSheetColumns("Structure Type")
        On Error GoTo 0
        
        If structureTypeColIndex > 0 Then
            ' Set the Features value based on the "Structure Type"
            cellValue = dataSheetRows(rowIndex, structureTypeColIndex)
        End If

    ' Check if the reporting column is "Annual Yield"
    ElseIf reportColumn(2) = "Annual Yield" Then
        ' Validate that the "Coupon Rate Per Annum Percent" column exists in dataSheetColumns
        On Error Resume Next
        annualYieldColIndex = dataSheetColumns("Coupon Rate Per Annum Percent")
        On Error GoTo 0
        
        If annualYieldColIndex > 0 Then
            ' Check if the Annual Yield value is not empty
            If Not IsEmpty(dataSheetRows(rowIndex, annualYieldColIndex)) Then
                cellValue = dataSheetRows(rowIndex, annualYieldColIndex) / 100

                ' Apply percentage formatting
                reportSheet.Cells(reportRow, columnIndex).NumberFormat = "0.00%"
            Else
                cellValue = ""
            End If
        End If

    ' Check if the reporting column is "Yield"
    ElseIf reportColumn(2) = "Yield" Then
        ' Validate that the "Coupon Rate Per Annum Percent" column exists in dataSheetColumns
        On Error Resume Next
        annualYieldColIndex = dataSheetColumns("Coupon Rate Per Annum Percent")
        On Error GoTo 0
        
        If annualYieldColIndex > 0 Then
            ' Check if the Annual Yield value is not empty
            If Not IsEmpty(dataSheetRows(rowIndex, annualYieldColIndex)) Then
                ' Calculate Yield as Annual Yield divided by 12, rounded to 2 decimal places, and append "% per month"
                cellValue = Round(dataSheetRows(rowIndex, annualYieldColIndex) / 12, 2) & "% per month"
            Else
                cellValue = ""
            End If
        End If

    ' Check if the reporting column is "% Paid So Far"
    ElseIf reportColumn(2) = "% Paid So Far" Then
        ' Validate that the "Payments Received Percent" column exists in dataSheetColumns
        On Error Resume Next
        paymentsReceivedColIndex = dataSheetColumns("Payments Received Percent")
        On Error GoTo 0
        
        If paymentsReceivedColIndex > 0 Then
            ' Calculate % Paid So Far, round to 2 decimal places, and append "%"
            cellValue = Round(dataSheetRows(rowIndex, paymentsReceivedColIndex), 2) / 100

            ' Apply percentage formatting
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "0.00%"
        End If

    ' Check if the reporting column is "$ Paid So Far"
    ElseIf reportColumn(2) = "$ Paid So Far" Then
        ' Validate that the necessary columns exist in dataSheetColumns
        On Error Resume Next
        totalNotionalColIndex = dataSheetColumns("Total Notional (USD)")
        paymentsReceivedColIndex = dataSheetColumns("Payments Received Percent")
        On Error GoTo 0
        
        If totalNotionalColIndex > 0 And paymentsReceivedColIndex > 0 Then
            ' Calculate $ Paid So Far
            cellValue = dataSheetRows(rowIndex, totalNotionalColIndex) * dataSheetRows(rowIndex, paymentsReceivedColIndex) / 100
            ' Apply USD currency formatting to the cell in the report sheet
            reportSheet.Cells(reportRow, columnIndex).NumberFormat = "[$$-409]#,##0"
        End If

    ' Check if the reporting column is "Underliers"
    ElseIf reportColumn(2) = "Underliers" Then
        ' Retrieve column indices for "List Of Underliers", "Active Underlier", and "Underlier Performance Percent"
        On Error Resume Next
        underliersColIndex = dataSheetColumns("List Of Underliers")
        activeUnderlierColIndex = dataSheetColumns("Active Underlier")
        underlierPerformanceColIndex = dataSheetColumns("Underlier Performance Percent")
        On Error GoTo 0
        
        ' Proceed only if all necessary columns are found
        If underliersColIndex > 0 And activeUnderlierColIndex > 0 And underlierPerformanceColIndex > 0 Then
            ' Process underliers
            Dim underliers As String
            Dim activeUnderlier As String
            Dim underlierList() As String
            Dim j As Long
            
            underliers = Replace(Replace(dataSheetRows(rowIndex, underliersColIndex), "[", ""), "]", "")
            activeUnderlier = Trim(dataSheetRows(rowIndex, activeUnderlierColIndex))
            underlierList = Split(underliers, ",")

            ' Insert underliers into sub-rows inside the reportSheet starting from rowIndex
            For j = LBound(underlierList) To UBound(underlierList)
                If j > 0 Then
                    reportRow = reportRow + 1
                End If
                
                ' Trim the underlier and add it to the new row
                underlierList(j) = Trim(underlierList(j))
                
                ' Check if this is the active underlier
                If underlierList(j) = activeUnderlier Then
                    ' Append performance and highlight active underlier in the report sheet at columnIndex
                    reportSheet.Cells(reportRow, columnIndex).Value = underlierList(j) & " " & Round(dataSheetRows(rowIndex, underlierPerformanceColIndex), 2) & "%"
                    reportSheet.Cells(reportRow, columnIndex).Interior.Color = RGB(169, 208, 142) ' Highlight in light green
                Else
                    ' Just add the underlier
                    reportSheet.Cells(reportRow, columnIndex).Value = underlierList(j)
                    ' Remove background color for non-active underliers
                    reportSheet.Cells(reportRow, columnIndex).Interior.ColorIndex = xlNone
                End If
            Next j
        End If

        FillReportCell = reportRow
        Exit Function
    Else
        ' Return a flag indicating that the calculation method is not implemented
        cellValue = "Not Implemented: " & reportColumn(2)
    End If

    ' Use reportRow and columnIndex to fill a cell in reportSheet with the calculated cellValue
    reportSheet.Cells(reportRow, columnIndex).Value = cellValue

    ' Return the calculated or fetched cell value
    FillReportCell = cellValue
End Function

Public Function GetColumnIndex(sheet As Worksheet, headerName As String) As Long
    Dim colIndex As Long
    On Error Resume Next
    colIndex = sheet.Rows(1).Find(what:=headerName, LookIn:=xlValues, lookat:=xlWhole).Column
    On Error GoTo 0
    GetColumnIndex = colIndex
End Function

Public Function AddTotalsRow(sheet As Worksheet)
  
End Function

Public Function ResetReport(reportSheet As Worksheet)
    Dim lastRow As Long
    
    ' Find the last row in the sheet (where the previous totals row might be)
    lastRow = reportSheet.Cells(reportSheet.Rows.Count, 1).End(xlUp).row

    ' If the last row contains "TOTAL", reset its row height before clearing
    If reportSheet.Cells(lastRow, 1).value = "TOTAL" Then
        reportSheet.Rows(lastRow).RowHeight = reportSheet.StandardHeight ' Reset to default row height
    End If
    
    ' Clear all contents (values, formulas) and formats starting from row 3 onwards
    With reportSheet.Rows("1:" & reportSheet.Rows.Count)
        .ClearContents
        .ClearFormats
        .HorizontalAlignment = xlCenter
        .VerticalAlignment = xlCenter
    End With
End Function

Public Function IsEmptySheet(sheet As Worksheet) As Boolean
    Dim lastRow As Long

    ' Find the last row
    lastRow = sheet.Cells(sheet.Rows.Count, 1).End(xlUp).row

    ' Check if the sheet is empty (i.e., only headers or completely empty)
    If lastRow <= 1 Then
        IsEmptySheet = True
    Else
        IsEmptySheet = False
    End If
End Function

' Function to retrieve data from sheet starting from row 2 to the last row, with filtering based on Return Type
Public Function GetDataSheetRows(ByVal ws As Worksheet, dataSheetColumns As Collection, reportName As String) As Variant
    Dim lastRow As Long
    Dim lastCol As Long
    Dim dataRange As Variant
    Dim filteredRows As Collection
    Dim returnTypeColIndex As Long
    Dim i As Long
    Dim row As Variant
    
    ' Initialize the collection to store filtered rows
    Set filteredRows = New Collection
    
    ' Find the last row with data in column A
    lastRow = ws.Cells(ws.Rows.Count, 1).End(xlUp).Row
    
    ' Find the last column with data in the first row
    lastCol = ws.Cells(1, ws.Columns.Count).End(xlToLeft).Column
    
    ' Check if the range is valid (i.e., there is data after the header row)
    If lastRow < 2 Or lastCol < 1 Then
        ' Return an empty variant if no data is found
        GetDataSheetRows = Array()
        Exit Function
    End If
    
    ' Get the column index for "Return Type"
    On Error Resume Next
    returnTypeColIndex = dataSheetColumns("Return Type")
    On Error GoTo 0
    
    ' If "Return Type" column is not found, return all rows
    If returnTypeColIndex = 0 Then
        GetDataSheetRows = ws.Cells(2, 1).Resize(lastRow - 1, lastCol).Value
        Exit Function
    End If
    
    ' Get the data range (excluding the header row) as a 2D array
    dataRange = ws.Cells(2, 1).Resize(lastRow - 1, lastCol).Value
    
    ' Loop through each row and filter based on the Return Type
    For i = 1 To UBound(dataRange, 1)
        Select Case reportName
            Case "GROWTH NOTES REPORT"
                If dataRange(i, returnTypeColIndex) = "Growth" Then
                    filteredRows.Add Application.Index(dataRange, i, 0)
                End If
            Case "INCOME NOTE REPORT"
                If dataRange(i, returnTypeColIndex) = "Income" Then
                    filteredRows.Add Application.Index(dataRange, i, 0)
                End If
            Case "DIGITAL NOTE REPORT"
                If dataRange(i, returnTypeColIndex) = "Digital" Then
                    filteredRows.Add Application.Index(dataRange, i, 0)
                End If
            Case Else
                ' If no specific report name, add all rows
                filteredRows.Add Application.Index(dataRange, i, 0)
        End Select
    Next i
    
    ' Convert the filtered rows collection to a 2D array
    If filteredRows.Count > 0 Then
        ReDim outputArray(1 To filteredRows.Count, 1 To lastCol)
        For i = 1 To filteredRows.Count
            For j = 1 To lastCol
                outputArray(i, j) = filteredRows(i)(j)
            Next j
        Next i
        GetDataSheetRows = outputArray
    Else
        GetDataSheetRows = Array() ' Return an empty array if no rows match the criteria
    End If
End Function




' Function to retrieve sheet headers from the first row with their indices
Public Function GetDataSheetColumns(ByVal ws As Worksheet) As Collection
    Dim lastCol As Long
    Dim headers As New Collection
    Dim i As Long
    Dim headerName As String

    ' Find the last column with data in the first row
    lastCol = ws.Cells(1, ws.Columns.Count).End(xlToLeft).Column
    
    ' Populate the Collection with headers and their indices
    For i = 1 To lastCol
        headerName = Trim$(ws.Cells(1, i).Value) ' Trim to remove any leading/trailing spaces
        If LenB(headerName) > 0 Then
            On Error Resume Next
            headers.Add i, headerName ' Add index with headerName as key
            On Error GoTo 0
        End If
    Next i
    
    ' Return the headers Collection
    Set GetDataSheetColumns = headers
End Function

Public Sub StyleRow(ByRef sheet As Worksheet, ByVal row As Long)
    With sheet.Rows(row)
        .Font.name = "Arial"
        .Font.Size = 14
        .Interior.Color = RGB(221, 235, 247) ' Light blue background
        .Borders(xlEdgeBottom).LineStyle = xlContinuous
        .Borders(xlEdgeBottom).Color = RGB(0, 0, 0) ' Black border
        .Borders(xlEdgeBottom).Weight = xlThin
    End With
End Sub

Public Function GetColumnsOfReport(reportName As String) As String()
  Dim ws As Worksheet
  Dim colIndex As Integer
  Dim rowIndex As Integer
  Dim lastRow As Long
  Dim dataArray() As String
  Dim reportFound As Boolean
  Dim resultCounter As Integer
  
  ' Assuming that the current active sheet is the one containing the report data
  Set ws = ThisWorkbook.Sheets("REPORTS")
  
  ' Search the first row for the matching report name
  reportFound = False
  For colIndex = 1 To ws.Cells(1, ws.Columns.Count).End(xlToLeft).Column
    If ws.Cells(1, colIndex).value = reportName Then
      reportFound = True
      Exit For
    End If
  Next colIndex
  
  ' If the report name is not found, return an empty string array
  If Not reportFound Then
    ReDim dataArray(0 To 0) ' Return an empty string array with 0 elements
    GetColumnsOfReport = dataArray
    Exit Function
  End If
  
  ' Find the last row in the report's column with data
  lastRow = ws.Cells(ws.Rows.Count, colIndex).End(xlUp).row
  
  ' Validate that lastRow is >= 2 to ensure there is data beyond the header row
  If lastRow < 2 Then
    ReDim dataArray(0 To 0) ' Return an empty string array
    GetColumnsOfReport = dataArray
    Exit Function
  End If
  
  ' Initialize a dynamic array based on the data rows
  ReDim dataArray(1 To lastRow - 1) ' Initialize the array with size equal to number of rows minus the header
  
  resultCounter = 0
  ' Loop through each row in the found column from row 2 to lastRow and collect the values
  For rowIndex = 2 To lastRow
    If ws.Cells(rowIndex, colIndex).value <> "" Then
      resultCounter = resultCounter + 1
      dataArray(resultCounter) = ws.Cells(rowIndex, colIndex).value
    End If
  Next rowIndex
  
  ' Resize the array to the actual number of items found
  If resultCounter = 0 Then
    ReDim dataArray(0 To 0) ' Return empty string array if no data was found
  Else
    ReDim Preserve dataArray(1 To resultCounter)
  End If
  
  GetColumnsOfReport = dataArray
End Function

Public Function GetReportingColumns() As Variant
  Dim lastCol As Long
  Dim colIndex As Long
  Dim reportingColumnsArray() As Variant
  Dim ws As Worksheet
  
  ' Set the worksheet object to the "REPORTING COLUMNS" sheet
  Set ws = ThisWorkbook.Sheets("REPORTING COLUMNS")
  
  ' Determine the last filled column in row 3
  lastCol = ws.Cells(3, ws.Columns.Count).End(xlToLeft).Column
  
  ' Initialize the array to hold the reporting columns data
  ReDim reportingColumnsArray(1 To lastCol - 1) ' Columns start from B, so minus 1
  
  ' Loop through each column from B (column 2) to the last filled column
  For colIndex = 2 To lastCol
    ' Initialize an array to hold the reporting column attributes
    Dim reportingColumn(1 To 6) As Variant
    
    ' Populate the array with values from the corresponding rows
    reportingColumn(1) = colIndex
    reportingColumn(2) = ws.Cells(3, colIndex).Value
    reportingColumn(3) = (ws.Cells(4, colIndex).Value = True)
    reportingColumn(4) = ws.Cells(5, colIndex).Value
    reportingColumn(5) = ws.Cells(6, colIndex).Value
    reportingColumn(6) = ws.Cells(7, colIndex).Value

    Debug.Print "Percentage Format: " & ws.Cells(7, colIndex).Value
    
    ' Store the array in the reportingColumnsArray
    reportingColumnsArray(colIndex - 1) = reportingColumn
  Next colIndex
  
  ' Return the array of reporting columns
  GetReportingColumns = reportingColumnsArray
End Function

Public Function GetReportingColumnByName(reportingColumnsArray As Variant, columnName As String) As Variant
  Dim i As Long
  
  ' Loop through the reportingColumnsArray to find the matching column by name
  For i = LBound(reportingColumnsArray) To UBound(reportingColumnsArray)
    ' Check if the name matches the requested columnName
    If reportingColumnsArray(i)(2) = columnName Then
      ' Return the matching reportingColumn array
      GetReportingColumnByName = reportingColumnsArray(i)
      Exit Function
    End If
  Next i
  
  ' If no match is found, return Nothing
  GetReportingColumnByName = Empty
End Function

Public Function GetReportColumns(reportName As String) As Variant
    Dim reportColumnsArray As Variant
    Dim columnNamesArray() As String
    Dim reportingColumnsArray As Variant
    Dim reportColumn As Variant
    Dim i As Long, reportColumnIndex As Long
    
    ' Get all available columns for reporting
    reportingColumnsArray = GetReportingColumns()
    
    ' Get the array of column names for the specified report
    columnNamesArray = GetColumnsOfReport(reportName)
    
    ' Initialize the array to hold the report columns structure
    ReDim reportColumnsArray(1 To UBound(columnNamesArray))
    
    ' Loop through the array of column names
    For i = LBound(columnNamesArray) To UBound(columnNamesArray)
        ' Get the column configuration by name
        reportColumn = GetReportingColumnByName(reportingColumnsArray, columnNamesArray(i))
        
        ' If the column configuration is found, build the report column object
        If Not IsEmpty(reportColumn) Then
            reportColumnIndex = i
            ' Create a dictionary or array to hold the report column properties
            Dim reportColumnObject As Variant
            ReDim reportColumnObject(1 To 7) ' Adjust the size as needed
            
            ' Populate the report column object
            reportColumnObject(1) = reportColumnIndex ' Index in column names array
            reportColumnObject(2) = reportColumn(2) ' Column name
            reportColumnObject(3) = reportColumn(3) ' Calculate total? (True/False)
            reportColumnObject(4) = reportColumn(4) ' Total value calculation method
            reportColumnObject(5) = reportColumn(5) ' Calculation method description
            reportColumnObject(6) = 0.0 ' Total value initialized to 0
            reportColumnObject(7) = reportColumn(6) ' Calculation method description
            
            ' Add the report column object to the report columns array
            reportColumnsArray(i) = reportColumnObject
        End If
    Next i
    
    ' Return the final array of report columns
    GetReportColumns = reportColumnsArray
End Function

Sub DrawReportHeaders(reportSheet As Worksheet, reportName As String, reportColumns As Variant)
    ' First Row: Merge all columns, center the text, and apply formatting
    With reportSheet
        .Range(.Cells(1, 1), .Cells(1, UBound(reportColumns))).Merge
        .Cells(1, 1).Value = reportName
        .Cells(1, 1).HorizontalAlignment = xlCenter
        .Cells(1, 1).VerticalAlignment = xlCenter
        .Cells(1, 1).Font.Bold = True
        .Cells(1, 1).Font.Size = 16
        .Cells(1, 1).Font.Name = "Arial"
        .Cells(1, 1).Interior.Color = RGB(163, 191, 226) ' Background color #A3BFE2
        .Cells(1, 1).Borders.LineStyle = xlContinuous
        .Rows(1).RowHeight = 60
    End With

    ' Second Row: Set each cell to the corresponding report column name, center the text, wrap text, and apply formatting
    Dim i As Long
    For i = LBound(reportColumns) To UBound(reportColumns)
        With reportSheet.Cells(2, i)
            .Value = reportColumns(i)(2)
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .Font.Bold = True
            .Font.Size = 14
            .Font.Name = "Arial"
            .Interior.Color = RGB(221, 235, 247) ' Light blue background
            .Borders.LineStyle = xlContinuous
            .WrapText = True
            .ColumnWidth = 25 ' Set a default width, which may increase based on text size.
        End With
    Next i
    
    ' Set the row height for the second row
    reportSheet.Rows(2).RowHeight = 60
    
    ' Adjust column widths to a minimum of 140 points
    For i = 1 To UBound(reportColumns) + 1
        If reportSheet.Columns(i).ColumnWidth < 25 Then
            reportSheet.Columns(i).ColumnWidth = 25
        End If
    Next i
End Sub

Sub DrawReportTotals(reportSheet As Worksheet, reportColumns As Variant, reportRow As Long, dataCount As Long)
    Dim i As Long
    Dim totalValue As Double
    Dim firstTotalColumn As Long
    
    ' Add a new row at the end of the report
    reportSheet.Rows(reportRow).RowHeight = 60
    
    ' Initialize firstTotalColumn as 0
    firstTotalColumn = 1
    
    ' Loop through each report column
    For i = LBound(reportColumns) To UBound(reportColumns)
        With reportSheet.Cells(reportRow, i)
            ' Center the text, apply formatting, and set the background color
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .Font.Bold = True
            .Font.Size = 14
            .Font.Name = "Arial"
            .Interior.Color = RGB(221, 235, 247) ' Light blue background
            .Borders.LineStyle = xlContinuous
            
            ' Check if the reportColumn(3) is True (calculate total)
            If reportColumns(i)(3) = True Then
                If firstTotalColumn = 1 Then
                    firstTotalColumn = i
                End If
                
                If reportColumns(i)(4) = "Sum" Then
                    ' Put the Sum in the cell value
                    .Value = reportColumns(i)(6)
                ElseIf reportColumns(i)(4) = "Average" Then
                    ' Calculate the Average and put it in the cell value
                    If reportRow > 0 Then
                        .Value = reportColumns(i)(6) / dataCount
                    Else
                        .Value = reportColumns(i)(6)
                    End If
                End If

                If Not IsEmpty(reportColumns(i)(7)) And reportColumns(i)(7) <> "" Then
                    .NumberFormat = Replace(reportColumns(i)(7), """", "")
                End If
            Else
                .Value = ""
            End If
        End With
    Next i

    ' Merge columns from start to the firstTotalColumn and set the value to "Total"
    If firstTotalColumn > 1 Then
        With reportSheet.Range(reportSheet.Cells(reportRow, 1), reportSheet.Cells(reportRow, firstTotalColumn - 1))
            .Merge
            .Value = "Total"
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .Font.Bold = True
            .Font.Size = 14
            .Font.Name = "Arial"
            .Interior.Color = RGB(221, 235, 247) ' Light blue background
            .Borders.LineStyle = xlContinuous
        End With
    End If
End Sub

Sub ExportReport(reportSheet As Worksheet)
    Dim newWorkbook As Workbook
    
    ' Create a new workbook
    Set newWorkbook = Workbooks.Add
    
    ' Copy the reportSheet to the new workbook
    reportSheet.Copy Before:=newWorkbook.Sheets(1)
    
    ' Get a reference to the copied sheet in the new workbook
    Dim copiedSheet As Worksheet
    Set copiedSheet = newWorkbook.Sheets(1)
    
    ' Remove all buttons (form controls) from the copied sheet
    Dim shape As Shape
    For Each shape In copiedSheet.Shapes
        If shape.Type = msoFormControl Then
            shape.Delete
        End If
    Next shape
    
    ' Remove any other sheets in the new workbook
    Dim ws As Worksheet
    For Each ws In newWorkbook.Sheets
        If ws.Name <> copiedSheet.Name Then
            Application.DisplayAlerts = False
            ws.Delete
            Application.DisplayAlerts = True
        End If
    Next ws
    
    ' Trigger the default Save As dialogue
    On Error Resume Next
    Application.DisplayAlerts = True
    newWorkbook.SaveAs
    On Error GoTo 0
    
    ' Notify the user
    MsgBox "Report has been exported successfully.", vbInformation
End Sub



