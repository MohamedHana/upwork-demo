import { defineStore } from "pinia"
import api from "@/api/index"
import * as XLSX from "xlsx"
import ExcelJS from "exceljs"
import { saveAs } from "file-saver"

export const useDatasetStore = defineStore("dataset", {
  state: () => ({
    datasets: [],
  }),
  getters: {
    haveDatasets: (state) => {
      return state.datasets.length > 0
    },
    reportsData: (state) => {
      let data = []

      state.datasets.forEach((dataset) => {
        if (dataset.is_dumped) {
          const processedData = dataset.json_data.map((row) => {
            // Issuer/CUSIP
            const issuerCusip = `${row["Issuer"] || "Undetermined Issuer"}, ${row["Cusip"] || "Undetermined Cusip"}`

            // Validate and calculate Term in months only if both Issue Date and Maturity Date exist
            let termMonths = "Undetermined"
            if (row["Issue Date"] && row["Maturity Date"]) {
              const issueDate = new Date(row["Issue Date"])
              const maturityDate = new Date(row["Maturity Date"])
              termMonths =
                (maturityDate.getFullYear() - issueDate.getFullYear()) * 12 +
                (maturityDate.getMonth() - issueDate.getMonth())
            }

            // Redemption (Maturity Date)
            const redemption = row["Maturity Date"] || "Undetermined"

            // Amt Invested (Total Notional) - Use 0 if it's explicitly set to 0, otherwise use the parsed number
            const amtInvested =
              row["Total Notional (USD)"] !== undefined
                ? parseFloat(row["Total Notional (USD)"].toFixed(2))
                : 0

            // Current Value (Mark to Market * Total Notional)
            const currentValue =
              row["Mark To Market Value"] !== undefined &&
              row["Total Notional (USD)"] !== undefined
                ? parseFloat(
                    (
                      row["Mark To Market Value"] * row["Total Notional (USD)"]
                    ).toFixed(2),
                  )
                : 0

            // Current Value % (Mark to Market - 100)
            const currentValuePercent =
              row["Mark To Market Value"] !== undefined
                ? parseFloat((row["Mark To Market Value"] - 100).toFixed(2))
                : 0

            // Intrinsic Value (Total Notional * Intrinsic Value)
            const intrinsicValue =
              row["Total Notional (USD)"] !== undefined &&
              row["Intrinsic Value"] !== undefined
                ? parseFloat(
                    (
                      row["Total Notional (USD)"] * row["Intrinsic Value"]
                    ).toFixed(2),
                  )
                : 0

            // Intrinsic Value % (Intrinsic Value - 100)
            const intrinsicValuePercent =
              row["Intrinsic Value"] !== undefined
                ? parseFloat((row["Intrinsic Value"] - 100).toFixed(2))
                : 0

            // Protection (Protection Proximity - Underlier Performance)
            const protectionType =
              row["Structure Type"] &&
              (row["Structure Type"].toLowerCase().includes("trigger") ||
                row["Structure Type"].toLowerCase().includes("buffer"))
                ? "Hard Buffer"
                : "Barrier"

            const protectionPercent =
              row["Protection Proximity Level Abs"] !== undefined &&
              row["Underlier Performance Percent"] !== undefined
                ? parseFloat(
                    (
                      row["Protection Proximity Level Abs"] -
                      row["Underlier Performance Percent"]
                    ).toFixed(2),
                  )
                : 0

            // Protection Level (from Protection Proximity)
            const protectionLevel =
              row["Protection Proximity Level Abs"] !== undefined
                ? parseFloat(row["Protection Proximity Level Abs"].toFixed(2)) +
                  "%"
                : "Undetermined"

            // Max Return (from Column AC, or "unlimited")
            const maxReturn = row["Max Return"]
              ? parseFloat(row["Max Return"].toFixed(2))
              : "unlimited"

            // Upside Participation (from Column AD)
            const upsideParticipation =
              row["Upside Participation Rate"] !== undefined
                ? parseFloat(row["Upside Participation Rate"].toFixed(2)) + "%"
                : "Undetermined"

            // Underliers (list of underliers with performance highlighted for the active one)
            let underliers = []
            if (row["List Of Underliers"]) {
              const activeUnderlier = row["Active Underlier"]
                ? row["Active Underlier"].trim()
                : ""
              const underlierPerformance =
                row["Underlier Performance Percent"] !== undefined
                  ? parseFloat(row["Underlier Performance Percent"].toFixed(2))
                  : 0

              // Remove the brackets and split the string into an array
              underliers = row["List Of Underliers"]
                .replace(/[\[\]]/g, "")
                .split(", ")
                .map((underlier) => {
                  const name = underlier.trim()
                  const isActive = name === activeUnderlier

                  return {
                    name: name,
                    performance: isActive ? `${underlierPerformance}%` : "",
                    active: isActive,
                  }
                })
            }

            // Features (Structure Type)
            const features = row["Structure Type"] || "Undetermined"

            // Construct processed row
            return {
              dataset_code: dataset.code,
              "Issuer/CUSIP": issuerCusip,
              Term: `${termMonths}M`,
              Redemption: redemption,
              "Amt Invested": amtInvested,
              "Current Value": currentValue,
              "Current Value %": currentValuePercent,
              "Intrinsic Value": intrinsicValue,
              "Intrinsic Value %": intrinsicValuePercent,
              Protection: `${protectionPercent}% ${protectionType}`,
              "Protection Level": protectionLevel,
              "Max Return": maxReturn,
              "Upside Participation": upsideParticipation,
              Underliers: underliers,
              Features: features,
            }
          })

          data = data.concat(processedData)
        }
      })

      return data
    },
    growthNotesReportData: (state) => {
      return state.reportsData.map((row) => {
        const underliers = row["Underliers"]
          .map((underlier) => {
            if (underlier.active) {
              return `(${underlier.name}: ${underlier.performance} performance)`
            }
            return `${underlier.name}`
          })
          .join(" • ")

        return {
          "Issuer/CUSIP": row["Issuer/CUSIP"],
          Term: row["Term"],
          Redemption: row["Redemption"],
          "Amt Invested": row["Amt Invested"],
          "Current Value": row["Current Value"],
          "Current Value %": row["Current Value %"],
          "Intrinsic Value": row["Intrinsic Value"],
          "Intrinsic Value %": row["Intrinsic Value %"],
          Protection: row["Protection"],
          "Protection Level": row["Protection Level"],
          "Max Return": row["Max Return"],
          "Upside Participation": row["Upside Participation"],
          Underliers: underliers,
          Features: row["Features"],
        }
      })
    },
  },
  actions: {
    // Setters
    appendDataToReports(data) {},
    // Methods
    async loadNewDataFile(file) {
      // Construct a workbook from file
      const code = Math.random().toString(36).slice(2, 13)
      const data = await file.arrayBuffer()
      const workbook = XLSX.read(data, { type: "array" })
      const json_data = XLSX.utils.sheet_to_json(
        workbook.Sheets[workbook.SheetNames[0]],
      )

      // Construct a new dataset
      const dataset = {
        code: code,
        source: file,
        workbook: workbook,
        json_data: json_data,
        imported_at: new Date(),
        is_dumped: false,
      }

      this.datasets.push(dataset)
      console.log(this.datasets)
    },
    dumpDataset(ds) {
      ds = this.datasets.find((dataset) => dataset.code === ds.code)

      ds.is_dumped = true
    },
    unloadDataset(ds) {
      ds = this.datasets.find((dataset) => dataset.code === ds.code)

      ds.is_dumped = false
    },
    deleteDataset(ds) {
      this.datasets = this.datasets.filter(
        (dataset) => dataset.code !== ds.code,
      )
    },
    // exportGrowthNotesReport() {
    //   const worksheet = XLSX.utils.json_to_sheet(this.growthNotesReportData)

    //   // Apply wrapText to each cell in column M
    //   this.growthNotesReportData.forEach((row, index) => {
    //     const cellAddress = `M${index + 2}` // Adjust for the header (starts from row 2)
    //     if (worksheet[cellAddress]) {
    //       // Enable wrapText for this cell
    //       worksheet[cellAddress].s = { alignment: { wrapText: true } }
    //     }
    //   })

    //   const workbook = XLSX.utils.book_new()
    //   XLSX.utils.book_append_sheet(workbook, worksheet, "GROWTH_NOTES_DATA")
    //   XLSX.writeFile(workbook, "GROWTH_NOTES_REPORT.xlsx")
    // },
    async exportGrowthNotesReport() {
      // Create a new workbook and worksheet
      const workbook = new ExcelJS.Workbook()
      const worksheet = workbook.addWorksheet("Growth Notes")

      // Add the header row
      worksheet.addRow([
        "Issuer/CUSIP",
        "Term",
        "Redemption",
        "Amt Invested",
        "Current Value",
        "%",
        "Intrinsic Value",
        "%",
        "Protection",
        "Protection Level",
        "Max Return",
        "Upside Participation",
        "Underliers",
        "Features",
      ])

      // Add data rows
      worksheet.addRow([
        "JP Morgan, 48134R",
        "24M",
        "05/11/2025",
        138000,
        181539,
        0.3155,
        193752,
        0.404,
        "10% Hard Buffer",
        "",
        "",
        1.15,
        "RTY",
        "Uncapped ATM Digital Worst Of Barrier Note",
      ])
      worksheet.addRow([
        "Barclays, 0674",
        "36M",
        "05/11/2026",
        138000,
        171203,
        0.2406,
        175936,
        0.2749,
        "20% Hard Buffer",
        "",
        "",
        1.15,
        "INDU +23.91%",
        "SPX",
      ])
      worksheet.addRow([
        "JP Morgan, 4813",
        "36M",
        "05/02/2026",
        138000,
        185099,
        0.3413,
        209277,
        0.5165,
        "30% Barrier",
        "",
        "",
        1.47,
        "NDX",
        "RTY",
      ])
      worksheet.addRow([
        "BNP Paribas, 05610",
        "5Y",
        "03/11/2028",
        138000,
        186065,
        0.3483,
        198375,
        0.4375,
        "30% Barrier",
        "",
        "",
        1.83,
        "SPX +35.13%",
        "INDU +23.91%",
      ])
      worksheet.addRow([
        "TOTAL",
        "",
        "",
        552000,
        723907,
        0.3114,
        777340,
        0.4082,
        "",
        "",
        "",
        "",
        "",
        "",
      ])

      // Apply basic styling to header row
      worksheet.getRow(1).font = { bold: true }
      worksheet.getRow(1).alignment = {
        vertical: "middle",
        horizontal: "center",
      }
      worksheet.getRow(1).fill = {
        type: "pattern",
        pattern: "solid",
        fgColor: { argb: "FFB3C6E7" }, // Light blue color
      }

      // Set column width for better presentation
      worksheet.columns = [
        { key: "Issuer/CUSIP", width: 30 },
        { key: "Term", width: 10 },
        { key: "Redemption", width: 15 },
        { key: "Amt Invested", width: 15 },
        { key: "Current Value", width: 15 },
        { key: "Percentage", width: 10 },
        { key: "Intrinsic Value", width: 15 },
        { key: "Intrinsic %", width: 10 },
        { key: "Protection", width: 20 },
        { key: "Protection Level", width: 20 },
        { key: "Max Return", width: 15 },
        { key: "Upside Participation", width: 20 },
        { key: "Underliers", width: 20 },
        { key: "Features", width: 40 },
      ]

      // Format specific columns as currency and percentage
      worksheet.getColumn(4).numFmt = "$#,##0"
      worksheet.getColumn(5).numFmt = "$#,##0"
      worksheet.getColumn(6).numFmt = "0.00%"
      worksheet.getColumn(7).numFmt = "$#,##0"
      worksheet.getColumn(8).numFmt = "0.00%"

      // Generate the Excel file and save it
      const buffer = await workbook.xlsx.writeBuffer()
      const blob = new Blob([buffer], {
        type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      })
      saveAs(blob, "growth_notes_exported.xlsx")
    },
    exportReport(processedData) {
      const worksheet = XLSX.utils.json_to_sheet(processedData)
      const workbook = XLSX.utils.book_new()
      XLSX.utils.book_append_sheet(workbook, worksheet, "Report")
      XLSX.writeFile(workbook, "Generated_Report.xlsx")
    },
  },
})
