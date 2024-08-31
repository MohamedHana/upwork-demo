Sub RESET_GROWTH_NOTES_REPORT()
    Dim reportSheet As Worksheet
    Set reportSheet = ThisWorkbook.Sheets("GROWTH NOTES REPORT")

    Call Utils.ResetReport(reportSheet)
End Sub

Sub EXPORT_GROWTH_NOTES_REPORT()
    Call Utils.ExportReport(ThisWorkbook.Sheets("GROWTH NOTES REPORT"))
End Sub


Sub REGENERATE_GROWTH_NOTES_REPORT()
    Application.ScreenUpdating = False ' Turn off screen updating
    Application.Calculation = xlCalculationManual ' Turn off automatic calculation

    Dim reportName As String
    reportName = "GROWTH NOTES REPORT"

    Dim dataSheet As Worksheet
    Set dataSheet = ThisWorkbook.Sheets("DATA SHEET")

    Dim reportSheet As Worksheet
    Set reportSheet = ThisWorkbook.Sheets(reportName)
    
    ' Check if the data sheet is empty (i.e., only headers or completely empty)
    If Utils.IsEmptySheet(dataSheet) Then
        MsgBox "The DATA SHEET is empty. No data available to generate the report.", vbExclamation
        Application.ScreenUpdating = True ' Turn screen updating back on
        Application.Calculation = xlCalculationAutomatic ' Turn calculation back on
        Exit Sub
    End If
    
    ' Reset the report sheet to its initial state
    Call Utils.ResetReport(reportSheet)

    Dim reportColumns As Variant
    ' Get the structured report columns for a specific report name
    reportColumns = Utils.GetReportColumns(reportName)

    ' Draw report header (first and second rows in the report)
    Call Utils.DrawReportHeaders(reportSheet, reportName, reportColumns)

    Dim dataSheetColumns As Collection
    ' Get column headers from data sheet using the Collection method
    Set dataSheetColumns = Utils.GetDataSheetColumns(dataSheet)

    Dim dataSheetRows As Variant
    ' Get rows from data sheet
    dataSheetRows = Utils.GetDataSheetRows(dataSheet, dataSheetColumns, reportName)

    Dim cellValue As Variant
    Dim rowIndex As Long, columnIndex As Long, reportRow As Long, iterationRow As Long
    Dim haveUnderliers As Boolean

    reportRow = 3
    iterationRow = 3
    haveUnderliers = False

    ' Loop through data sheet rows
    For rowIndex = LBound(dataSheetRows) To UBound(dataSheetRows)
        ' Loop through report columns
        For columnIndex = LBound(reportColumns) To UBound(reportColumns)
            ' Fill the cell in report sheet (current cell)
            cellValue = Utils.FillReportCell(reportSheet, rowIndex, columnIndex, dataSheetRows, reportColumns(columnIndex), dataSheetColumns, reportRow)
            
            If reportColumns(columnIndex)(2) = "Underliers" Then
                reportRow = cellValue
                haveUnderliers = True
            End IF

            ' If current report sheet columnIndex "calculate total?" is True then update totalValue
            If reportColumns(columnIndex)(3) Then
                reportColumns(columnIndex)(6) = reportColumns(columnIndex)(6) + cellValue
            End If
        Next columnIndex

        ' If reportColumns(columnIndex)(2) = "Underliers"
        If haveUnderliers Then
             ' Merge cells for the columns that should be merged if empty
            For columnIndex = 1 To UBound(reportColumns)
                If reportColumns(columnIndex)(2) <> "Underliers" And iterationRow <> reportRow Then
                    reportSheet.Range(reportSheet.Cells(iterationRow, columnIndex), reportSheet.Cells(reportRow, columnIndex)).Merge
                End If
            Next columnIndex
        End IF

        reportRow = reportRow + 1
        iterationRow = reportRow
    Next rowIndex

    ' Draw report totals (last row in the report)
    Call Utils.DrawReportTotals(reportSheet, reportColumns, reportRow, UBound(dataSheetRows))

    ' Turn screen updating and calculation back on
    Application.ScreenUpdating = True
    Application.Calculation = xlCalculationAutomatic
End Sub

