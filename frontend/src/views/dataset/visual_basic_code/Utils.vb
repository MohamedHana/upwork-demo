Public Function FillReportCell(reportSheet As Worksheet, _
    rowIndex As Long, _
    columnIndex As Long, _
    dataSheetRows As Variant, _
    reportColumn As Variant, _
    dataSheetColumns As Collection, _
    reportRow As Long, _
    reportColumnNumber As Long) As Variant
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

    ' Check If the reporting column is "Issuer/CUSIP"
    If reportColumn(2) = "Issuer/CUSIP" Then
        ' Validate that both "Issuer" And "Cusip" columns exist in dataSheetColumns
        On Error Resume Next
        issuerColIndex = dataSheetColumns("Issuer")
        cusipColIndex = dataSheetColumns("Cusip")
        On Error Goto 0

            If issuerColIndex > 0 And cusipColIndex > 0 Then
                ' Calculate cellValue by combining Issuer And Cusip values
                cellValue = dataSheetRows(rowIndex, issuerColIndex) & "/" & dataSheetRows(rowIndex, cusipColIndex)
            End If

            ' Check If the reporting column is "Term"
        Elseif reportColumn(2) = "Term" Then
            ' Validate that both "Maturity Date" And "Issue Date" columns exist in dataSheetColumns
            On Error Resume Next
            maturityDateColIndex = dataSheetColumns("Maturity Date")
            issueDateColIndex = dataSheetColumns("Issue Date")
            On Error Goto 0

                If maturityDateColIndex > 0 And issueDateColIndex > 0 Then
                    ' Calculate the difference in months between Maturity Date And Issue Date, And append "M"
                    cellValue = DateDiff("m", dataSheetRows(rowIndex, issueDateColIndex), dataSheetRows(rowIndex, maturityDateColIndex)) & "M"
                End If

                ' Check If the reporting column is "Redemption"
            Elseif reportColumn(2) = "Redemption" Then
                ' Validate that the "Maturity Date" column exists in dataSheetColumns
                On Error Resume Next
                maturityDateColIndex = dataSheetColumns("Maturity Date")
                On Error Goto 0

                    If maturityDateColIndex > 0 Then
                        ' Set cellValue To the value of the Maturity Date
                        cellValue = dataSheetRows(rowIndex, maturityDateColIndex)
                    End If

                    ' Check If the reporting column is "Amt Invested"
                Elseif reportColumn(2) = "Amt Invested" Then
                    ' Validate that the "Amt Invested" column exists in dataSheetColumns
                    On Error Resume Next
                    amtInvestedColIndex = dataSheetColumns("Total Notional (USD)")
                    On Error Goto 0

                        If amtInvestedColIndex > 0 Then
                            ' Set cellValue To the value of the Amt Invested
                            cellValue = dataSheetRows(rowIndex, amtInvestedColIndex)
                        End If

                        ' Apply USD currency formatting To the cell in the report sheet
                        reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "[$$-409]#,##0"

                        ' Check If the reporting column is "Current Value"
                    Elseif reportColumn(2) = "Current Value" Then
                        ' Validate that the necessary columns exist in dataSheetColumns
                        On Error Resume Next
                        markToMarketColIndex = dataSheetColumns("Mark To Market Value")
                        totalNotionalColIndex = dataSheetColumns("Total Notional (USD)")
                        On Error Goto 0

                            If markToMarketColIndex > 0 And totalNotionalColIndex > 0 Then
                                ' Calculate the Current Value
                                cellValue = dataSheetRows(rowIndex, markToMarketColIndex) * dataSheetRows(rowIndex, totalNotionalColIndex) / 100
                            End If

                            ' Apply USD currency formatting To the cell in the report sheet
                            reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "[$$-409]#,##0"

                            ' Check If the reporting column is "Current Value %"
                        Elseif reportColumn(2) = "Current Value %" Then
                            ' Validate that the necessary column exists in dataSheetColumns
                            On Error Resume Next
                            markToMarketColIndex = dataSheetColumns("Mark To Market Value")
                            On Error Goto 0

                                If markToMarketColIndex > 0 Then
                                    ' Calculate the Current Value %: (Mark To Market Value - 100) rounded To 2 decimal places, And append "%"
                                    cellValue = Round(dataSheetRows(rowIndex, markToMarketColIndex) - 100, 2) / 100

                                End If

                                ' Apply percentage formatting
                                reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "0.00%"

                                ' Check If the reporting column is "Intrinsic Value"
                            Elseif reportColumn(2) = "Intrinsic Value" Then
                                ' Validate that the necessary columns exist in dataSheetColumns
                                On Error Resume Next
                                totalNotionalColIndex = dataSheetColumns("Total Notional (USD)")
                                intrinsicValueColIndex = dataSheetColumns("Intrinsic Value")
                                On Error Goto 0

                                    If totalNotionalColIndex > 0 And intrinsicValueColIndex > 0 Then
                                        ' Calculate the Intrinsic Value
                                        cellValue = dataSheetRows(rowIndex, totalNotionalColIndex) * dataSheetRows(rowIndex, intrinsicValueColIndex) / 100
                                    End If

                                    ' Apply USD currency formatting To the cell in the report sheet
                                    reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "[$$-409]#,##0"

                                    ' Check If the reporting column is "Intrinsic Value %"
                                Elseif reportColumn(2) = "Intrinsic Value %" Then
                                    ' Validate that the "Intrinsic Value" column exists in dataSheetColumns
                                    On Error Resume Next
                                    intrinsicValueColIndex = dataSheetColumns("Intrinsic Value")
                                    On Error Goto 0

                                        If intrinsicValueColIndex > 0 Then
                                            ' Calculate the Intrinsic Value %, round To 2 decimal places, And append "%"
                                            cellValue = Round(dataSheetRows(rowIndex, intrinsicValueColIndex), 2) / 100
                                        End If

                                        ' Apply percentage formatting
                                        reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "0.00%"

                                        ' Check If the reporting column is "Protection"
                                    Elseif reportColumn(2) = "Protection" Then
                                        ' Validate that the necessary columns exist in dataSheetColumns
                                        On Error Resume Next
                                        structureTypeColIndex = dataSheetColumns("Structure Type")
                                        protectionProximityColIndex = dataSheetColumns("Protection Proximity Level Abs")
                                        underlierPerformanceColIndex = dataSheetColumns("Underlier Performance Percent")
                                        On Error Goto 0

                                            If structureTypeColIndex > 0 And protectionProximityColIndex > 0 And underlierPerformanceColIndex > 0 Then
                                                ' Determine whether "Trigger" Or "Buffer" is present in the "Structure Type"
                                                If InStr(1, dataSheetRows(rowIndex, structureTypeColIndex), "Trigger") > 0 Or _
                                                    InStr(1, dataSheetRows(rowIndex, structureTypeColIndex), "Buffer") > 0 Then
                                                    ' Calculate And Set the value As "% Buffer"
                                                    cellValue = Round(dataSheetRows(rowIndex, protectionProximityColIndex) - dataSheetRows(rowIndex, underlierPerformanceColIndex), 0) & "% Buffer"
                                                Else
                                                    ' Calculate And Set the value As "% Barrier"
                                                    cellValue = Round(dataSheetRows(rowIndex, protectionProximityColIndex) - dataSheetRows(rowIndex, underlierPerformanceColIndex), 0) & "% Barrier"
                                                End If
                                            End If

                                            ' Check If the reporting column is "Protection Level"
                                        Elseif reportColumn(2) = "Protection Level" Then
                                            ' Validate that the "Protection Proximity Level Abs" column exists in dataSheetColumns
                                            On Error Resume Next
                                            protectionProximityColIndex = dataSheetColumns("Protection Proximity Level Abs")
                                            On Error Goto 0

                                                If protectionProximityColIndex > 0 Then
                                                    ' Set the Protection Level With Percentage Formatting
                                                    cellValue = dataSheetRows(rowIndex, protectionProximityColIndex) / 100
                                                End If

                                                ' Apply percentage formatting
                                                reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "0.00%"

                                                ' Check If the reporting column is "Max Return"
                                            Elseif reportColumn(2) = "Max Return" Then
                                                ' Validate that the "Max Return" column exists in dataSheetColumns
                                                On Error Resume Next
                                                maxReturnColIndex = dataSheetColumns("Max Return")
                                                On Error Goto 0

                                                    If maxReturnColIndex > 0 Then
                                                        ' Check If the Max Return value is empty Or less than Or equal To 0
                                                        If dataSheetRows(rowIndex, maxReturnColIndex) = "" Or dataSheetRows(rowIndex, maxReturnColIndex) <= 0 Then
                                                            cellValue = "Unlimited"
                                                        Else
                                                            cellValue = dataSheetRows(rowIndex, maxReturnColIndex)
                                                        End If
                                                    End If

                                                    ' Check If the reporting column is "Upside Participation"
                                                Elseif reportColumn(2) = "Upside Participation" Then
                                                    ' Validate that the "Upside Participation Rate" column exists in dataSheetColumns
                                                    On Error Resume Next
                                                    upsideParticipationColIndex = dataSheetColumns("Upside Participation Rate")
                                                    On Error Goto 0

                                                        If upsideParticipationColIndex > 0 Then
                                                            ' Set the Upside Participation With Percentage Formatting
                                                            cellValue = dataSheetRows(rowIndex, upsideParticipationColIndex) / 100
                                                        End If

                                                        ' Apply percentage formatting
                                                        reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "0.00%"

                                                        ' Check If the reporting column is "Features"
                                                    Elseif reportColumn(2) = "Features" Then
                                                        ' Validate that the "Structure Type" column exists in dataSheetColumns
                                                        On Error Resume Next
                                                        structureTypeColIndex = dataSheetColumns("Structure Type")
                                                        On Error Goto 0

                                                            If structureTypeColIndex > 0 Then
                                                                ' Set the Features value based on the "Structure Type"
                                                                cellValue = dataSheetRows(rowIndex, structureTypeColIndex)
                                                            End If

                                                            ' Check If the reporting column is "Annual Yield"
                                                        Elseif reportColumn(2) = "Annual Yield" Then
                                                            ' Validate that the "Coupon Rate Per Annum Percent" column exists in dataSheetColumns
                                                            On Error Resume Next
                                                            annualYieldColIndex = dataSheetColumns("Coupon Rate Per Annum Percent")
                                                            On Error Goto 0

                                                                If annualYieldColIndex > 0 Then
                                                                    ' Check If the Annual Yield value is Not empty
                                                                    If Not IsEmpty(dataSheetRows(rowIndex, annualYieldColIndex)) Then
                                                                        cellValue = dataSheetRows(rowIndex, annualYieldColIndex) / 100
                                                                    Else
                                                                        cellValue = ""
                                                                    End If
                                                                End If

                                                                ' Apply percentage formatting
                                                                reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "0.00%"

                                                                ' Check If the reporting column is "Yield"
                                                            Elseif reportColumn(2) = "Yield" Then
                                                                ' Validate that the "Coupon Rate Per Annum Percent" column exists in dataSheetColumns
                                                                On Error Resume Next
                                                                annualYieldColIndex = dataSheetColumns("Coupon Rate Per Annum Percent")
                                                                On Error Goto 0

                                                                    If annualYieldColIndex > 0 Then
                                                                        ' Check If the Annual Yield value is Not empty
                                                                        If Not IsEmpty(dataSheetRows(rowIndex, annualYieldColIndex)) Then
                                                                            ' Calculate Yield As Annual Yield divided by 12, rounded To 2 decimal places, And append "% per month"
                                                                            cellValue = Round(dataSheetRows(rowIndex, annualYieldColIndex) / 12, 2) & "% per month"
                                                                        Else
                                                                            cellValue = ""
                                                                        End If
                                                                    End If

                                                                    ' Check If the reporting column is "% Paid So Far"
                                                                Elseif reportColumn(2) = "% Paid So Far" Then
                                                                    ' Validate that the "Payments Received Percent" column exists in dataSheetColumns
                                                                    On Error Resume Next
                                                                    paymentsReceivedColIndex = dataSheetColumns("Payments Received Percent")
                                                                    On Error Goto 0

                                                                        If paymentsReceivedColIndex > 0 Then
                                                                            ' Calculate % Paid So Far, round To 2 decimal places, And append "%"
                                                                            cellValue = Round(dataSheetRows(rowIndex, paymentsReceivedColIndex), 2) / 100
                                                                        End If

                                                                        ' Apply percentage formatting
                                                                        reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "0.00%"

                                                                        ' Check If the reporting column is "$ Paid So Far"
                                                                    Elseif reportColumn(2) = "$ Paid So Far" Then
                                                                        ' Validate that the necessary columns exist in dataSheetColumns
                                                                        On Error Resume Next
                                                                        totalNotionalColIndex = dataSheetColumns("Total Notional (USD)")
                                                                        paymentsReceivedColIndex = dataSheetColumns("Payments Received Percent")
                                                                        On Error Goto 0

                                                                            If totalNotionalColIndex > 0 And paymentsReceivedColIndex > 0 Then
                                                                                ' Calculate $ Paid So Far
                                                                                cellValue = dataSheetRows(rowIndex, totalNotionalColIndex) * dataSheetRows(rowIndex, paymentsReceivedColIndex) / 100
                                                                            End If

                                                                            ' Apply USD currency formatting To the cell in the report sheet
                                                                            reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "[$$-409]#,##0"

                                                                            ' Check If the reporting column is "Protection Buffer"
                                                                        Elseif reportColumn(2) = "Protection Buffer" Then
                                                                            ' Validate that both "Maturity Date" And "Issue Date" columns exist in dataSheetColumns
                                                                            On Error Resume Next
                                                                            protectionProximityColIndex = dataSheetColumns("Protection Proximity Level Abs")
                                                                            underlierPerformanceColIndex = dataSheetColumns("Underlier Performance Percent")
                                                                            On Error Goto 0

                                                                                If protectionProximityColIndex > 0 And underlierPerformanceColIndex > 0 Then
                                                                                    ' Calculate the difference 
                                                                                    cellValue = Round(dataSheetRows(rowIndex, protectionProximityColIndex) - dataSheetRows(rowIndex, underlierPerformanceColIndex), 2) / 100 
                                                                                End If

                                                                                ' Apply percentage formatting
                                                                                reportSheet.Cells(reportRow, reportColumnNumber).NumberFormat = "0.00%"

                                                                                ' Check If the reporting column is "Underliers"
                                                                            Elseif reportColumn(2) = "Underliers" Then
                                                                                ' Retrieve column indices For "List Of Underliers", "Active Underlier", And "Underlier Performance Percent"
                                                                                On Error Resume Next
                                                                                underliersColIndex = dataSheetColumns("List Of Underliers")
                                                                                activeUnderlierColIndex = dataSheetColumns("Active Underlier")
                                                                                underlierPerformanceColIndex = dataSheetColumns("Underlier Performance Percent")
                                                                                On Error Goto 0

                                                                                    ' Proceed only If all necessary columns are found
                                                                                    If underliersColIndex > 0 And activeUnderlierColIndex > 0 And underlierPerformanceColIndex > 0 Then
                                                                                        ' Process underliers
                                                                                        Dim underliers As String
                                                                                        Dim activeUnderlier As String
                                                                                        Dim underlierList() As String
                                                                                        Dim j As Long

                                                                                        underliers = Replace(Replace(dataSheetRows(rowIndex, underliersColIndex), "[", ""), "]", "")
                                                                                        activeUnderlier = Trim(dataSheetRows(rowIndex, activeUnderlierColIndex))
                                                                                        underlierList = Split(underliers, ",")

                                                                                        ' Insert underliers into Sub-rows inside the reportSheet starting from rowIndex
                                                                                        For j = LBound(underlierList) To UBound(underlierList)
                                                                                            If j > 0 Then
                                                                                                reportRow = reportRow + 1
                                                                                            End If

                                                                                            ' Trim the underlier And add it To the New row
                                                                                            underlierList(j) = Trim(underlierList(j))

                                                                                            ' Check If this is the active underlier
                                                                                            If underlierList(j) = activeUnderlier Then
                                                                                                ' Append performance And highlight active underlier in the report sheet at reportColumnNumber
                                                                                                reportSheet.Cells(reportRow, reportColumnNumber).value = underlierList(j) & " " & Round(dataSheetRows(rowIndex, underlierPerformanceColIndex), 2) & "%"
                                                                                                reportSheet.Cells(reportRow, reportColumnNumber).Interior.Color = RGB(169, 208, 142) ' Highlight in light green
                                                                                            Else
                                                                                                ' Just add the underlier
                                                                                                reportSheet.Cells(reportRow, reportColumnNumber).value = underlierList(j)
                                                                                                ' Remove background color For non-active underliers
                                                                                                reportSheet.Cells(reportRow, reportColumnNumber).Interior.ColorIndex = xlNone
                                                                                            End If
                                                                                        Next j
                                                                                    End If

                                                                                    FillReportCell = reportRow
                                                                                 Exit Function
                                                                                Else
                                                                                    ' Return a flag indicating that the calculation method is Not implemented
                                                                                    cellValue = "Not Implemented: " & reportColumn(2)
                                                                                End If

                                                                                ' Use reportRow And columnIndex To fill a cell in reportSheet With the calculated cellValue
                                                                                reportSheet.Cells(reportRow, reportColumnNumber).value = cellValue

                                                                                ' Return the calculated Or fetched cell value
                                                                                FillReportCell = cellValue
End Function

Public Function GetColumnIndex(sheet As Worksheet, headerName As String) As Long
    Dim colIndex As Long
    On Error Resume Next
    colIndex = sheet.Rows(1).Find(what:=headerName, LookIn:=xlValues, lookat:=xlWhole).Column
    On Error Goto 0
        GetColumnIndex = colIndex
End Function

Public Function AddTotalsRow(sheet As Worksheet)

End Function

Public Function IsEmptySheet(sheet As Worksheet) As Boolean
    Dim lastRow As Long

    ' Find the last row
    lastRow = sheet.Cells(sheet.Rows.Count, 1).End(xlUp).row

    ' Check If the sheet is empty (i.e., only headers Or completely empty)
    If lastRow <= 1 Then
        IsEmptySheet = True
    Else
        IsEmptySheet = False
    End If
End Function

' Function To retrieve data from sheet starting from row 2 To the last row, With filtering based on Return Type
Public Function GetDataSheetRows(Byval ws As Worksheet, dataSheetColumns As Collection, reportName As String) As Variant
    Dim lastRow As Long
    Dim lastCol As Long
    Dim dataRange As Variant
    Dim filteredRows As Collection
    Dim returnTypeColIndex As Long
    Dim i As Long
    Dim row As Variant

    ' Initialize the collection To store filtered rows
    Set filteredRows = New Collection

    ' Find the last row With data in column A
    lastRow = ws.Cells(ws.Rows.Count, 1).End(xlUp).row

    ' Find the last column With data in the first row
    lastCol = ws.Cells(1, ws.Columns.Count).End(xlToLeft).Column

    ' Check If the range is valid (i.e., there is data after the header row)
    If lastRow < 2 Or lastCol < 1 Then
        ' Return an empty variant If no data is found
        GetDataSheetRows = Array()
     Exit Function
    End If

    ' Get the column index For "Return Type"
    On Error Resume Next
    returnTypeColIndex = dataSheetColumns("Return Type")
    On Error Goto 0

        ' If "Return Type" column is Not found, return all rows
        If returnTypeColIndex = 0 Then
            GetDataSheetRows = ws.Cells(2, 1).Resize(lastRow - 1, lastCol).value
         Exit Function
        End If

        ' Get the data range (excluding the header row) As a 2D array
        dataRange = ws.Cells(2, 1).Resize(lastRow - 1, lastCol).value

        ' Loop through each row And filter based on the Return Type
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

        ' Convert the filtered rows collection To a 2D array
        If filteredRows.Count > 0 Then
            ReDim outputArray(1 To filteredRows.Count, 1 To lastCol)
            For i = 1 To filteredRows.Count
                For j = 1 To lastCol
                    outputArray(i, j) = filteredRows(i)(j)
                Next j
            Next i
            GetDataSheetRows = outputArray
        Else
            GetDataSheetRows = Array() ' Return an empty array If no rows match the criteria
        End If
End Function

' Function To retrieve sheet headers from the first row With their indices
Public Function GetDataSheetColumns(Byval ws As Worksheet) As Collection
    Dim lastCol As Long
    Dim headers As New Collection
    Dim i As Long
    Dim headerName As String

    ' Find the last column With data in the first row
    lastCol = ws.Cells(1, ws.Columns.Count).End(xlToLeft).Column

    ' Populate the Collection With headers And their indices
    For i = 1 To lastCol
        headerName = Trim$(ws.Cells(1, i).value) ' Trim To remove any leading/trailing spaces
        If LenB(headerName) > 0 Then
            On Error Resume Next
            headers.Add i, headerName ' Add index With headerName As key
            On Error Goto 0
            End If
        Next i

        ' Return the headers Collection
        Set GetDataSheetColumns = headers
End Function

Public Sub StyleRow(Byref sheet As Worksheet, Byval row As Long)
    With sheet.Rows(row)
        .Font.name = "Arial"
        .Font.Size = 14
        .Interior.Color = RGB(221, 235, 247) ' Light blue background
        .Borders(xlEdgeBottom).LineStyle = xlContinuous
        .Borders(xlEdgeBottom).Color = RGB(0, 0, 0) ' Black border
        .Borders(xlEdgeBottom).Weight = xlThin
    End With
End Sub

Public Function GetColumnsOfReport(reportSheet As Worksheet) As String()
    Dim colIndex As Integer
    Dim lastCol As Integer
    Dim dataArray() As String
    Dim resultCounter As Integer

    ' Find the last column With data in the second row
    lastCol = reportSheet.Cells(2, reportSheet.Columns.Count).End(xlToLeft).Column

    ' Initialize the dynamic array based on the number of filled cells in the second row
    ReDim dataArray(1 To lastCol - 1) ' Initialize the array With size equal To number of columns minus the first column

    resultCounter = 0
    ' Loop through each cell in the second row starting from the second column
    For colIndex = 2 To lastCol
        If reportSheet.Cells(2, colIndex).Value <> "" Then
            resultCounter = resultCounter + 1
            dataArray(resultCounter) = Trim(reportSheet.Cells(2, colIndex).Value)
        End If
    Next colIndex

    ' Resize the array To the actual number of items found
    If resultCounter = 0 Then
        ReDim dataArray(0 To 0) ' Return empty string array If no data was found
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

    ' Set the worksheet object To the "REPORTING COLUMNS" sheet
    Set ws = ThisWorkbook.Sheets("REPORTING COLUMNS")

    ' Determine the last filled column in row 3
    lastCol = ws.Cells(3, ws.Columns.Count).End(xlToLeft).Column

    ' Initialize the array To hold the reporting columns data
    ReDim reportingColumnsArray(1 To lastCol - 1) ' Columns start from B, so minus 1

    ' Loop through each column from B (column 2) To the last filled column
    For colIndex = 2 To lastCol
        ' Initialize an array To hold the reporting column attributes
        Dim reportingColumn(1 To 6) As Variant

        ' Populate the array With values from the corresponding rows
        reportingColumn(1) = colIndex
        reportingColumn(2) = ws.Cells(3, colIndex).value
        reportingColumn(3) = (ws.Cells(4, colIndex).value = True)
        reportingColumn(4) = ws.Cells(5, colIndex).value
        reportingColumn(5) = ws.Cells(6, colIndex).value
        reportingColumn(6) = ws.Cells(7, colIndex).value

        ' Store the array in the reportingColumnsArray
        reportingColumnsArray(colIndex - 1) = reportingColumn
    Next colIndex

    ' Return the array of reporting columns
    GetReportingColumns = reportingColumnsArray
End Function

Public Function GetReportingColumnByName(reportingColumnsArray As Variant, columnName As String) As Variant
    Dim i As Long

    ' Loop through the reportingColumnsArray To find the matching column by name
    For i = LBound(reportingColumnsArray) To UBound(reportingColumnsArray)
        ' Check If the name matches the requested columnName
        If reportingColumnsArray(i)(2) = Trim(columnName) Then
            ' Return the matching reportingColumn array
            GetReportingColumnByName = reportingColumnsArray(i)
         Exit Function
        End If
    Next i

    ' If no match is found, return Nothing
    GetReportingColumnByName = Empty
End Function

Public Function GetReportColumns(reportSheet As Worksheet) As Variant
    Dim reportColumnsArray As Variant
    Dim columnNamesArray() As String
    Dim reportingColumnsArray As Variant
    Dim reportColumn As Variant
    Dim i As Long, reportColumnIndex As Long

    ' Get all available columns For reporting
    reportingColumnsArray = GetReportingColumns()

    ' Get the array of column names For the specified report
    columnNamesArray = GetColumnsOfReport(reportSheet)

    ' Initialize the array To hold the report columns structure
    ReDim reportColumnsArray(1 To UBound(columnNamesArray))

    ' Loop through the array of column names
    For i = LBound(columnNamesArray) To UBound(columnNamesArray)
        ' Get the column configuration by name
        reportColumn = GetReportingColumnByName(reportingColumnsArray, columnNamesArray(i))

        ' If the column configuration is found, build the report column object
        If Not IsEmpty(reportColumn) Then
            reportColumnIndex = i
            ' Create a dictionary Or array To hold the report column properties
            Dim reportColumnObject As Variant
            ReDim reportColumnObject(1 To 7) ' Adjust the size As needed

            ' Populate the report column object
            reportColumnObject(1) = reportColumnIndex ' Index in column names array
            reportColumnObject(2) = Trim(reportColumn(2)) ' Column name
            reportColumnObject(3) = reportColumn(3) ' Calculate total? (True/False)
            reportColumnObject(4) = reportColumn(4) ' Total value calculation method
            reportColumnObject(5) = reportColumn(5) ' Calculation method description
            reportColumnObject(6) = 0 ' Total value initialized To 0
            reportColumnObject(7) = reportColumn(6) ' total value number format

            ' Add the report column object To the report columns array
            reportColumnsArray(i) = reportColumnObject
        End If
    Next i

    ' Return the final array of report columns
    GetReportColumns = reportColumnsArray
End Function

Sub DrawReportHeaders(reportSheet As Worksheet, reportName As String, reportColumns As Variant)
    ' First Row: Merge all columns, center the text, And apply formatting
    With reportSheet
        .Range(.Cells(1, 1), .Cells(1, UBound(reportColumns))).Merge
        .Cells(1, 1).value = reportName
        .Cells(1, 1).HorizontalAlignment = xlCenter
        .Cells(1, 1).VerticalAlignment = xlCenter
        .Cells(1, 1).Font.Bold = True
        .Cells(1, 1).Font.Size = 16
        .Cells(1, 1).Font.name = "Arial"
        .Cells(1, 1).Interior.Color = RGB(163, 191, 226) ' Background color #A3BFE2
        .Cells(1, 1).Borders.LineStyle = xlContinuous
        .Rows(1).RowHeight = 60
    End With

    ' Second Row: Set each cell To the corresponding report column name, center the text, wrap text, And apply formatting
    Dim i As Long
    For i = LBound(reportColumns) To UBound(reportColumns)
        With reportSheet.Cells(2, i)
            .value = reportColumns(i)(2)
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .Font.Bold = True
            .Font.Size = 14
            .Font.name = "Arial"
            .Interior.Color = RGB(221, 235, 247) ' Light blue background
            .Borders.LineStyle = xlContinuous
            .WrapText = True
            .ColumnWidth = 25 ' Set a default width, which may increase based on text size.
        End With
    Next i

    ' Set the row height For the second row
    reportSheet.Rows(2).RowHeight = 60

    ' Adjust column widths To a minimum of 140 points
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

    ' Add a New row at the end of the report
    reportSheet.Rows(reportRow).RowHeight = 60

    ' Label of Total columns
    With reportSheet.Cells(reportRow, 1)
        .value = "Total"
        .HorizontalAlignment = xlCenter
        .VerticalAlignment = xlCenter
        .Font.Bold = True
        .Font.Size = 14
        .Font.name = "Arial"
        .Interior.Color = RGB(221, 235, 247) ' Light blue background
        .Borders.LineStyle = xlContinuous
    End With

    ' Loop through each report column
    For i = LBound(reportColumns) To UBound(reportColumns)
        With reportSheet.Cells(reportRow, i + 1)
            ' Center the text, apply formatting, And Set the background color
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .Font.Bold = True
            .Font.Size = 14
            .Font.name = "Arial"
            .Interior.Color = RGB(221, 235, 247) ' Light blue background
            .Borders.LineStyle = xlContinuous

            ' Check If the reportColumn(3) is True (calculate total)
            If reportColumns(i)(3) = True Then
                If reportColumns(i)(4) = "Sum" Then
                    ' Put the Sum in the cell value
                    .value = reportColumns(i)(6)
                Elseif reportColumns(i)(4) = "Average" Then
                    ' Calculate the Average And put it in the cell value
                    If reportRow > 0 Then
                        .value = reportColumns(i)(6) / dataCount
                    Else
                        .value = reportColumns(i)(6)
                    End If
                End If

                If Not IsEmpty(reportColumns(i)(7)) And reportColumns(i)(7) <> "" Then
                    .NumberFormat = Replace(reportColumns(i)(7), """", "")
                End If
            Else
                .value = ""
            End If
        End With
    Next i
End Sub

Sub AddToContextMenu()
    Dim contextMenu As CommandBar
    Dim newMenuItem As CommandBarButton

    ' Reference the cell context menu
    Set contextMenu = Application.CommandBars("Cell")

    ' Remove the custom menu item If it already exists To avoid duplicates
    On Error Resume Next
    contextMenu.Controls("Create a New report sheet from selection").Delete
    On Error Goto 0

        ' Add a New menu item
        Set newMenuItem = contextMenu.Controls.Add(Type:=msoControlButton, Temporary:=True)

        ' Set properties For the New menu item
        With newMenuItem
            .Caption = "Create a New report sheet from selection"
            .OnAction = "Utils.CreateReportSheetFromSelection" ' Link To the macro
        End With
End Sub

Sub RemoveFromContextMenu()
    Dim contextMenu As CommandBar

    ' Reference the cell context menu
    Set contextMenu = Application.CommandBars("Cell")

    ' Remove the custom menu item
    On Error Resume Next
    contextMenu.Controls("Create New Sheet from Selection").Delete
    On Error Goto 0
End Sub

Sub CreateReportSheetFromSelection()
    Dim selectedRange As Range
    Dim reportName As String
    Dim reportColumns As Range
    Dim newSheet As Worksheet
    Dim colCount As Long
    Dim i As Long
    Dim generateButton As Object
    Dim exportButton As Object
    Dim buttonWidth As Double
    Dim buttonHeight As Double
    Dim buttonSpacing As Double
    Dim buttonLeftPosition As Double
    Dim textWidth As Double
    Dim response As VbMsgBoxResult

    ' Step 1: Validate the selected cells
    Set selectedRange = Selection

    ' Check If the selection is in one column And has at least 2 rows
    If selectedRange.Columns.Count <> 1 Or selectedRange.Rows.Count < 2 Then
        MsgBox "Please Select a single column With at least 2 rows.", vbExclamation
     Exit Sub
    End If

    ' Check If any cells in the selection are empty
    If WorksheetFunction.CountBlank(selectedRange) > 0 Then
        MsgBox "Please make sure all selected cells are Not empty.", vbExclamation
     Exit Sub
    End If

    ' Step 2: Assign the first row As the report name And the rest As report columns
    reportName = selectedRange.Cells(1, 1).Value
    ' Validate the report name length
    If Len(reportName) > 31 Then
        MsgBox "The report name exceeds 31 characters. Please shorten the name.", vbExclamation
     Exit Sub
    End If

    ' Validate the report name For invalid characters
    Dim invalidChars As String
    invalidChars = ":\/?*[]"

    For i = 1 To Len(invalidChars)
        If InStr(reportName, Mid(invalidChars, i, 1)) > 0 Then
            MsgBox "The report name contains invalid characters (" & invalidChars & "). Please use a valid name.", vbExclamation
         Exit Sub
        End If
    Next i  

    Set reportColumns = selectedRange.Offset(1, 0).Resize(selectedRange.Rows.Count - 1, 1)

    ' Step 3: Create a New sheet With the report name
    On Error Resume Next
    Set newSheet = ThisWorkbook.Sheets(reportName)
    On Error Goto 0

        ' Check If the New sheet already exists
        If Not newSheet Is Nothing Then
            response = MsgBox("Click Yes To replace it, No To rename the New sheet, Or Cancel To terminate the operation at all.", _
            vbExclamation + vbYesNoCancel + vbDefaultButton3, "A sheet With the name '" & reportName & "' already exists")

            Select Case response
             Case vbYes ' Replace button
                ' Delete the existing sheet And proceed To create the New one
                Application.DisplayAlerts = False
                newSheet.Delete
                Application.DisplayAlerts = True

             Case vbNo ' Rename button
                ' Focus the cursor on the first cell in the selected range To allow renaming
                selectedRange.Cells(1, 1).Select
             Exit Sub

             Case vbCancel ' Cancel button
                ' Do nothing And just close the message box
             Exit Sub
            End Select
        End If

        Set newSheet = ThisWorkbook.Sheets.Add(After:=ThisWorkbook.Sheets(1))
        newSheet.Name = reportName

        ' Step 4: Format the first row in the New sheet
        colCount = reportColumns.Rows.Count

        Dim minWidth As Double
        minWidth = 24

        With newSheet
            ' Merge all columns in the first row For the report title
            .Range(.Cells(1, 1), .Cells(1, colCount + 1)).Merge
            .Cells(1, 1).Value = reportName
            .Cells(1, 1).Font.Name = "Arial"
            .Cells(1, 1).Font.Size = 16
            .Cells(1, 1).Font.Bold = True
            .Cells(1, 1).HorizontalAlignment = xlLeft
            .Cells(1, 1).VerticalAlignment = xlCenter
            .Cells(1, 1).Interior.Color = RGB(168, 190, 223) ' Background color #A8BEDF
            .Cells(1, 1).Borders.LineStyle = xlContinuous
            .Rows(1).RowHeight = 60

            ' Step 5: Add an empty cell in the first column of the second row
            .Cells(2, 1).Value = ""
            .Cells(2, 1).Font.Name = "Arial"
            .Cells(2, 1).Font.Size = 14
            .Cells(2, 1).Font.Bold = True
            .Cells(2, 1).HorizontalAlignment = xlCenter
            .Cells(2, 1).VerticalAlignment = xlCenter
            .Cells(2, 1).Interior.Color = RGB(223, 235, 246) ' Background color #DFEBF6
            .Cells(2, 1).Borders.LineStyle = xlContinuous
            .Cells(2, 1).ColumnWidth = minWidth

            ' Step 6: Format And add report columns in the second row starting from the second column
            For i = 1 To reportColumns.Rows.Count
                .Cells(2, i + 1).Value = " " & reportColumns.Cells(i, 1).Value & " "
                .Cells(2, i + 1).Font.Name = "Arial"
                .Cells(2, i + 1).Font.Size = 14
                .Cells(2, i + 1).Font.Bold = True
                .Cells(2, i + 1).HorizontalAlignment = xlCenter
                .Cells(2, i + 1).VerticalAlignment = xlCenter
                .Cells(2, i + 1).Interior.Color = RGB(223, 235, 246) ' Background color #DFEBF6
                .Cells(2, i + 1).Borders.LineStyle = xlContinuous
            Next i

            .Rows(2).RowHeight = 60

            ' Step 7: Auto-fit column width For title cells And add indentation
            .Columns.AutoFit

            ' Add indentation To title cells
            For i = 2 To reportColumns.Rows.Count + 1
                .Cells(2, i).ColumnWidth = .Cells(2, i).ColumnWidth + 10 ' Add extra space For padding
            Next i

            ' Step 9: Add buttons in the first row after the report title
            buttonWidth = 100
            buttonHeight = 30
            buttonSpacing = 40 ' 40 pixels after the value

            ' Calculate text width of the report name
            textWidth = Len(.Cells(1, 1).Value) * 12

            ' Position buttons after the text width plus the spacing
            buttonLeftPosition = textWidth + buttonSpacing

            ' Add "Generate" button
            Set generateButton = .Buttons.Add(Left:=buttonLeftPosition, _
            Top:=.Cells(1, 1).Top + 15, Width:=buttonWidth, Height:=buttonHeight)
            With generateButton
                .Caption = "Generate"
                .Font.Name = "Arial"
                .Font.Size = 14
                .Font.Bold = True
                .OnAction = "Utils.GenerateReport"
            End With

            ' Add "Export" button Next To "Generate"
            Set exportButton = .Buttons.Add(Left:=buttonLeftPosition + buttonWidth + buttonSpacing, _
            Top:=.Cells(1, 1).Top + 15, Width:=buttonWidth, Height:=buttonHeight)
            With exportButton
                .Caption = "Export"
                .Font.Name = "Arial"
                .Font.Size = 14
                .Font.Bold = True
                .OnAction = "Utils.ExportReport"
            End With

            ' Auto focus first cell in second row (report columns row)
            .Cells(2, 1).Select
        End With
End Sub

Sub ResetReport()
    Dim reportSheet As Worksheet
    Set reportSheet = ThisWorkbook.ActiveSheet

    Dim lastRow As Long

    ' Find the last row in the sheet (where the previous totals row might be)
    lastRow = reportSheet.Cells(reportSheet.Rows.Count, 1).End(xlUp).row

    ' Clear all contents (values, formulas) And formats starting from row 3 onwards
    With reportSheet.Rows("3:" & reportSheet.Rows.Count)
        .ClearContents
        .ClearFormats
        .HorizontalAlignment = xlCenter
        .VerticalAlignment = xlCenter
        .RowHeight = reportSheet.StandardHeight
    End With
End Sub

Sub ExportReport()
    Dim reportSheet As Worksheet
    Set reportSheet = ThisWorkbook.ActiveSheet

    ' Create a New workbook
    Dim newWorkbook As Workbook
    Set newWorkbook = Workbooks.Add

    ' Copy the reportSheet To the New workbook
    reportSheet.Copy Before:=newWorkbook.Sheets(1)

    ' Get a reference To the copied sheet in the New workbook
    Dim copiedSheet As Worksheet
    Set copiedSheet = newWorkbook.Sheets(1)

    ' Remove all buttons (form controls) from the copied sheet
    Dim shape As shape
    For Each shape In copiedSheet.Shapes
        If shape.Type = msoFormControl Then
            shape.Delete
        End If
    Next shape

    ' Remove any other sheets in the New workbook
    Dim ws As Worksheet
    For Each ws In newWorkbook.Sheets
        If ws.name <> copiedSheet.name Then
            Application.DisplayAlerts = False
            ws.Delete
            Application.DisplayAlerts = True
        End If
    Next ws

    ' Trigger the default Save As dialogue
    On Error Resume Next
    Application.DisplayAlerts = True
    newWorkbook.SaveAs
    On Error Goto 0
End Sub

Sub GenerateReport()
    Application.ScreenUpdating = False ' Turn off screen updating
    Application.Calculation = xlCalculationManual ' Turn off automatic calculation

    Dim reportSheet As Worksheet
    Set reportSheet = ThisWorkbook.ActiveSheet

    Dim dataSheet As Worksheet
    Set dataSheet = ThisWorkbook.Sheets("DATA SHEET")

    ' Check If the data sheet is empty (i.e., only headers Or completely empty)
    If IsEmptySheet(dataSheet) Then
        MsgBox "The DATA SHEET is empty. No data available To generate the report.", vbExclamation
        Application.ScreenUpdating = True ' Turn screen updating back on
        Application.Calculation = xlCalculationAutomatic ' Turn calculation back on
     Exit Sub
    End If

    ' Reset the report sheet To its initial state
    Call ResetReport()

    ' Get the structured report columns For a specific report name
    Dim reportColumns As Variant
    reportColumns = GetReportColumns(reportSheet)

    ' Get column headers from data sheet using the Collection method
    Dim dataSheetColumns As Collection
    Set dataSheetColumns = GetDataSheetColumns(dataSheet)

    ' Get rows from data sheet
    Dim dataSheetRows As Variant
    dataSheetRows = GetDataSheetRows(dataSheet, dataSheetColumns, reportSheet.name)

    Dim cellValue As Variant
    Dim rowIndex As Long, columnIndex As Long, reportRow As Long, iterationRow As Long, reportColumnNumber As Long
    Dim haveUnderliers As Boolean

    reportRow = 3
    iterationRow = 3
    haveUnderliers = False

    ' Loop through data sheet rows
    For rowIndex = LBound(dataSheetRows) To UBound(dataSheetRows)
        ' Loop through report columns
        For columnIndex = LBound(reportColumns) To UBound(reportColumns)
            reportColumnNumber = columnIndex + 1

            ' Fill the cell in report sheet (current cell)
            cellValue = FillReportCell(reportSheet, rowIndex, columnIndex, dataSheetRows, reportColumns(columnIndex), dataSheetColumns, reportRow, reportColumnNumber)

            If reportColumns(columnIndex)(2) = "Underliers" Then
                reportRow = cellValue
                haveUnderliers = True
            End If

            ' If current report sheet columnIndex "calculate total?" is True Then update totalValue
            If reportColumns(columnIndex)(3) And IsNumeric(cellValue) Then
                reportColumns(columnIndex)(6) = reportColumns(columnIndex)(6) + cellValue
            End If
        Next columnIndex

        ' If reportColumns(columnIndex)(2) = "Underliers"
        If haveUnderliers Then
            ' Merge cells For the columns that should be merged If empty
            For columnIndex = 1 To UBound(reportColumns)
                If reportColumns(columnIndex)(2) <> "Underliers" And iterationRow <> reportRow Then
                    reportSheet.Range(reportSheet.Cells(iterationRow, columnIndex + 1), reportSheet.Cells(reportRow, columnIndex + 1)).Merge
                End If
            Next columnIndex
        End If

        reportRow = reportRow + 1
        iterationRow = reportRow
    Next rowIndex

    ' Draw report totals (last row in the report)
    Call DrawReportTotals(reportSheet, reportColumns, reportRow, UBound(dataSheetRows))

    ' Turn screen updating And calculation back on
    Application.ScreenUpdating = True
    Application.Calculation = xlCalculationAutomatic
End Sub
