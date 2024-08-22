import { defineStore } from "pinia"
import api from "@/api/index"
import * as XLSX from "xlsx"

export const useDatasetStore = defineStore("dataset", {
  state: () => ({
    datasets: [],
  }),
  getters: {
    haveDatasets: (state) => {
      return state.datasets.length > 0
    },
    growthNotesReportData: (state) => {
      let data = []

      state.datasets.forEach((dataset) => {
        if (dataset.is_dumped) {
          const processedData = dataset.json_data.map((row) => {
            // Issuer/CUSIP
            const issuerCusip = `${row["Issuer"] || "Unknown"}, ${row["Cusip"] || "Unknown"}`

            // Term in months
            const issueDate = new Date(row["Issue Date"])
            const maturityDate = new Date(row["Maturity Date"])
            const termMonths =
              (maturityDate.getFullYear() - issueDate.getFullYear()) * 12 +
              (maturityDate.getMonth() - issueDate.getMonth())

            // Redemption (Maturity Date)
            const redemption = row["Maturity Date"] || "Unknown"

            // Amt Invested (Total Notional)
            const amtInvested = row["Total Notional (USD)"]
              ? parseFloat(row["Total Notional (USD)"].toFixed(2))
              : 0

            // Current Value (Mark to Market * Total Notional)
            const currentValue =
              row["Mark To Market Value"] && row["Total Notional (USD)"]
                ? parseFloat(
                    (
                      row["Mark To Market Value"] * row["Total Notional (USD)"]
                    ).toFixed(2),
                  )
                : 0

            // Current Value % (Mark to Market - 100)
            const currentValuePercent = row["Mark To Market Value"]
              ? parseFloat((row["Mark To Market Value"] - 100).toFixed(2))
              : 0

            // Intrinsic Value (Total Notional * Intrinsic Value)
            const intrinsicValue =
              row["Total Notional (USD)"] && row["Intrinsic Value"]
                ? parseFloat(
                    (
                      row["Total Notional (USD)"] * row["Intrinsic Value"]
                    ).toFixed(2),
                  )
                : 0

            // Intrinsic Value % (Intrinsic Value - 100)
            const intrinsicValuePercent = row["Intrinsic Value"]
              ? parseFloat((row["Intrinsic Value"] - 100).toFixed(2))
              : 0

            // Protection (Protection Proximity - Underlier Performance)
            const protectionType =
              row["Structure Type"] && row["Structure Type"].includes("Trigger")
                ? "Buffer"
                : "Barrier"
            const protectionPercent =
              row["Protection Proximity"] && row["Underlier Performance"]
                ? parseFloat(
                    (
                      row["Protection Proximity"] - row["Underlier Performance"]
                    ).toFixed(2),
                  )
                : 0

            // Protection Level (from Protection Proximity)
            const protectionLevel = row["Protection Proximity"] || "Unknown"

            // Max Return (from Column AC, or "unlimited")
            const maxReturn = row["Max Return"] || "unlimited"

            // Upside Participation (from Column AD)
            const upsideParticipation = row["Upside Participation"] || "Unknown"

            // Underliers (list of underliers with performance highlighted for the active one)
            const underliers = row["List of Underliers"] || ""
            const activeUnderlier = row["Active Underlier"] || ""
            const underlierPerformance = row["Underlier Performance"] || 0
            const underliersWithPerformance = underliers
              ? underliers
                  .split(", ")
                  .map((underlier) =>
                    underlier === activeUnderlier
                      ? `${underlier} (${parseFloat(underlierPerformance.toFixed(2))}%)`
                      : underlier,
                  )
              : ["Unknown"]

            // Features (Structure Type)
            const features = row["Structure Type"] || "Unknown"

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
              Underliers: underliersWithPerformance.join(", "),
              Features: features,
            }
          })

          data = data.concat(processedData)
        }
      })

      return data
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
    exportReport(processedData) {
      const worksheet = XLSX.utils.json_to_sheet(processedData)
      const workbook = XLSX.utils.book_new()
      XLSX.utils.book_append_sheet(workbook, worksheet, "Report")
      XLSX.writeFile(workbook, "Generated_Report.xlsx")
    },
  },
})
