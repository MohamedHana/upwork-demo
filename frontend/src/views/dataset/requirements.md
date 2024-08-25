I need someone that can take data from a CSV or excel file and create custom reports. Here is a loom video describing what I need and I have sample reports attached as well

thank you Mohamed. I'm looking for something where if I repeatedly dump data in the format of the "Sample Data Dump Template" that I can get reports automatically made that look like the "Sample Report with Comments" excel sheet below. I have put in comments in all the fields for the Report that tell you exactly how to come up with the numbers we need to display and they link back to the Sample Data Dump template. I can pay you to try and come up with an excel sheet that can automate this process for me. Its much simpler than the projects you are used to. Should be easy - if this works well there are many other projects we can work on

Issuer/CUSIP -> Column C and column A

Term -> You are going to come up with how many months long this note was for by taking the Issue Date in Column S and subtracting how many months from Column F "Maturity Date" - That will tell you how many months long until maturity. You can also figure out how many months ago the trade date was in column R "Trade Date" and then add in Column I "Time to Maturity"

Redemption -> You will get this from Column F the "Maturity Date"

Amt Invested -> This is column H "Total Notional"

Current Value -> You are going to get this by multiplying Column E "Mark to Market" by column H "Total Notional"

Current Value % -> Take the "mark to Market" number in column E and subtract 100%. This will be the return %

Intrinsic Value -> This is the Total Notional from column H multiplied by "Intrinsic Value" in column V (error)

Intrinsic Value % -> This is column V "Intrinsic Value" minus 100%

Protection -> This one is going to entail a little work. There are two types of Protection - "Buffers" and "Barriers".  
In the "structure type" in column D the. "Trigger" is the same thing as "Buffer".  
Once you determine whether the note is a Buffer or a Barrier we are going to determine how much protection there is.
We will do that by subtracting Column P "Protection Proximity" by Column K "Underlier Performances".
Round to the nearest whole number and display that Percentage and the "Buffer" or "Barrier" designation

Protection Level -> This will be from Column P "Protection Proximity"

Max Return -> Some notes may have a max Return. Check in Column AC for "Max Return". If there isn't anything there then put "unlimited"

Upside Participation -> This will be in column AD "Upside Participation"

Underliers -> Here you are going to list off the Underliers from Column L "List of Underliers". Whatever the "Active Underlier" is from column M you are going to highlight and also add in the performance % from Column K "Underlier Performance"

Features -> Just put in the "structure type" from column D in these cells

' INCOME NOTE REPORT

Annual Yield -> This will be in Column O "Coupon Rate Per Annum"

Yield -> Take "Coupon Rate Per Annum" in Column O and divide by 12

% Paid So Far -> This will be in Column Z "Payments Received So far"

$ Paid So Far -> Take the "amount Invested in Column D" and multiply by "% Paid so far in Column K"

Underlier Performance Percent, List Of Underliers, Active Underlier
-8.7901%, [MXEF, SX5E], MXEF
26.004%, [MXEF, SX5E], MXEF
7.0371%, [INDU, NDX, RTY], INDU

' Underliers (Highlight Active Underlier and add performance)
Dim underliers As String
underliers = dataSheet.Cells(i, 12).Value

        If dataSheet.Cells(i, 13).Value <> "" Then
            underliers = Replace(underliers, dataSheet.Cells(i, 13).Value, dataSheet.Cells(i, 13).Value & " " & dataSheet.Cells(i, 11).Value & "%")
        End If

        reportSheet.Cells(reportRow, 13).Value = underliers

INCOME NOTE REPORT MAPPING

Issuer/CUSIP -> Column C and column A

Term -> You are going to come up with how many months long this note was for by taking the Issue Date in Column S and subtracting how many months from Column F "Maturity Date" - That will tell you how many months long until maturity. You can also figure out how many months ago the trade date was in column R "Trade Date" and then add in Column I "Time to Maturity"

Redemption -> You will get this from Column F the "Maturity Date"

Amt Invested -> This is column H "Total Notional"

' Get the list of underliers from Column L and split them by comma
underliers = Replace(Replace(dataSheet.Cells(i, 12).Value, "[", ""), "]", "")
activeUnderlier = dataSheet.Cells(i, 13).Value
underlierList = Split(underliers, ",")

        ' Initialize final underlier string to populate with sub-rows
        finalUnderlierString = ""

        ' Loop through each underlier in the list
        For j = LBound(underlierList) To UBound(underlierList)

            ' Trim any extra spaces around the underlier
            underlierList(j) = Trim(underlierList(j))

            ' Check if this underlier matches the active underlier
            If underlierList(j) = activeUnderlier Then
                ' Append performance to active underlier and highlight it
                finalUnderlierString = finalUnderlierString & underlierList(j) & " " & dataSheet.Cells(i, 11).Value & "%" & vbCrLf
                reportSheet.Cells(reportRow, 13).Characters(InStr(finalUnderlierString, underlierList(j)), Len(underlierList(j))).Font.Color = RGB(255, 255, 255) ' White text
                reportSheet.Cells(reportRow, 13).Characters(InStr(finalUnderlierString, underlierList(j)), Len(underlierList(j))).Font.Bold = True ' Bold text
                reportSheet.Cells(reportRow, 13).Interior.Color = RGB(169, 208, 142) ' Highlight in light green
            Else
                ' Just add the underlier to the final string
                finalUnderlierString = finalUnderlierString & underlierList(j) & vbCrLf
            End If

        Next j

        ' Populate the Underliers column with the final string
        reportSheet.Cells(reportRow, 13).Value = finalUnderlierString
        reportSheet.Cells(reportRow, 13).WrapText = True ' Enable wrap text to show sub-rows inside the cell
