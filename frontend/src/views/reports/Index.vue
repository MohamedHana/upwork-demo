<script>
import * as am5 from "@amcharts/amcharts5"
import * as am5xy from "@amcharts/amcharts5/xy"
import am5themes_Animated from "@amcharts/amcharts5/themes/Animated"
import * as am5exporting from "@amcharts/amcharts5/plugins/exporting"

export default {
  name: "ReprotsPage",
  setup() {},
  components: {},
  created() {},
  mounted() {
    this.createChart()
  },
  beforeUnmount() {
    // Dispose of the chart when the component is destroyed
    if (this.root) {
      this.root.dispose()
    }
  },
  data() {
    return {
      message:
        "Hello Sanjay, let's meet, brainstorm your business, determine reports/features, and set a timeline.",
      chart: null,
      root: null,
      notes: [
        {
          issuer: "JP Morgan, 48134BNE9",
          term: "2Y",
          redemption: "05/11/2025",
          amountInvested: 138000,
          currentValue: 181539,
          percentage: 31.55,
          intrinsicValue: 193752,
          intrinsicPercentage: 40.4,
          protection: "10% Hard Buffer",
          upsideParticipation: "115%",
          indexPerformance: ["RTY +36.18%", "SPX +35.13%"],
          features: "Absolute Return to buffer",
        },
        {
          issuer: "Barclays, 06745NQB5",
          term: "3Y",
          redemption: "05/11/2026",
          amountInvested: 138000,
          currentValue: 171203,
          percentage: 24.06,
          intrinsicValue: 175936,
          intrinsicPercentage: 27.49,
          protection: "20% Hard Buffer",
          upsideParticipation: "115%",
          indexPerformance: ["INDU +23.91%", "SPX +35.13%"],
          features: "",
        },
        {
          issuer: "JP Morgan, 48134BFF5",
          term: "3Y",
          redemption: "05/02/2026",
          amountInvested: 138000,
          currentValue: 185099,
          percentage: 34.13,
          intrinsicValue: 209277,
          intrinsicPercentage: 51.65,
          protection: "30% Barrier",
          upsideParticipation: "147%",
          indexPerformance: ["NDX +41.56%", "RTY +36.18%", "SPX +35.13%"],
          features: "",
        },
        {
          issuer: "BNP Paribas, 05610PJG1",
          term: "5Y",
          redemption: "03/11/2028",
          amountInvested: 138000,
          currentValue: 186065,
          percentage: 34.83,
          intrinsicValue: 199375,
          intrinsicPercentage: 43.75,
          protection: "30% Barrier",
          upsideParticipation: "183%",
          indexPerformance: ["INDU +23.91%", "NDX +41.56%", "RTY +36.18%"],
          features: "",
        },
      ],
    }
  },
  computed: {
    totalInvested() {
      return this.notes.reduce((total, note) => total + note.amountInvested, 0)
    },
    totalCurrentValue() {
      return this.notes.reduce((total, note) => total + note.currentValue, 0)
    },
    totalPercentage() {
      const totalInvested = this.totalInvested
      const totalCurrentValue = this.totalCurrentValue
      return (
        ((totalCurrentValue - totalInvested) / totalInvested) *
        100
      ).toFixed(2)
    },
    totalIntrinsicValue() {
      return this.notes.reduce((total, note) => total + note.intrinsicValue, 0)
    },
    totalIntrinsicPercentage() {
      const totalInvested = this.totalInvested
      const totalIntrinsicValue = this.totalIntrinsicValue
      return (
        ((totalIntrinsicValue - totalInvested) / totalInvested) *
        100
      ).toFixed(2)
    },
  },
  watch: {},
  methods: {
    createChart() {
      let root = am5.Root.new("chartdiv")

      // Apply theme
      root.setThemes([am5themes_Animated.new(root)])

      // Create XY chart
      let chart = root.container.children.push(
        am5xy.XYChart.new(root, {
          panX: true,
          panY: true,
          wheelX: "panX",
          wheelY: "zoomX",
        }),
      )

      // Create X axis (categories)
      let xAxis = chart.xAxes.push(
        am5xy.CategoryAxis.new(root, {
          categoryField: "issuer",
          renderer: am5xy.AxisRendererX.new(root, {
            minGridDistance: 30,
          }),
        }),
      )

      xAxis.get("renderer").labels.template.setAll({
        rotation: -45,
        centerY: am5.p50,
        centerX: am5.p100,
        paddingRight: 15,
      })

      // Create Y axis (values)
      let yAxis = chart.yAxes.push(
        am5xy.ValueAxis.new(root, {
          renderer: am5xy.AxisRendererY.new(root, {}),
        }),
      )

      // Create series for Current Value
      let currentValueSeries = chart.series.push(
        am5xy.ColumnSeries.new(root, {
          name: "Current Value",
          xAxis: xAxis,
          yAxis: yAxis,
          valueYField: "currentValue",
          categoryXField: "issuer",
          clustered: true, // Ensure clustering for side-by-side presentation
          tooltip: am5.Tooltip.new(root, {
            labelText: "Current Value: {valueY}",
          }),
        }),
      )

      // Create series for Intrinsic Value
      let intrinsicValueSeries = chart.series.push(
        am5xy.ColumnSeries.new(root, {
          name: "Intrinsic Value",
          xAxis: xAxis,
          yAxis: yAxis,
          valueYField: "intrinsicValue",
          categoryXField: "issuer",
          clustered: true, // Ensure clustering for side-by-side presentation
          tooltip: am5.Tooltip.new(root, {
            labelText: "Intrinsic Value: {valueY}",
          }),
        }),
      )

      // Create series for Amount Invested
      let amtInvestedSeries = chart.series.push(
        am5xy.ColumnSeries.new(root, {
          name: "Amount Invested",
          xAxis: xAxis,
          yAxis: yAxis,
          valueYField: "amountInvested",
          categoryXField: "issuer",
          clustered: true, // Ensure clustering for side-by-side presentation
          tooltip: am5.Tooltip.new(root, {
            labelText: "Amount Invested: {valueY}",
          }),
        }),
      )

      // Add a legend
      let legend = chart.children.push(
        am5.Legend.new(root, {
          centerX: am5.p50,
          x: am5.p50,
          layout: root.horizontalLayout,
        }),
      )

      legend.data.setAll(chart.series.values)

      // Add a scrollbar
      chart.set(
        "scrollbarX",
        am5.Scrollbar.new(root, {
          orientation: "horizontal",
        }),
      )

      // Data for the chart
      const data = [
        {
          issuer: "JP Morgan, 48134BNE9",
          amountInvested: 138000,
          currentValue: 181539,
          intrinsicValue: 193752,
        },
        {
          issuer: "Barclays, 06745NQB5",
          amountInvested: 138000,
          currentValue: 171203,
          intrinsicValue: 175936,
        },
        {
          issuer: "JP Morgan, 48134BFF5",
          amountInvested: 138000,
          currentValue: 185099,
          intrinsicValue: 209277,
        },
        {
          issuer: "BNP Paribas, 05610PJG1",
          amountInvested: 138000,
          currentValue: 186065,
          intrinsicValue: 199375,
        },
      ]

      // Set data
      xAxis.data.setAll(data)
      currentValueSeries.data.setAll(data)
      intrinsicValueSeries.data.setAll(data)
      amtInvestedSeries.data.setAll(data)

      // Add exporting functionality
      let exporting = am5exporting.Exporting.new(root, {
        menu: am5exporting.ExportingMenu.new(root, {}),
        filePrefix: "growth_notes_chart",
        dataSource: data,
      })

      this.chart = chart
    },
    formatCurrency(value) {
      return `$${value.toLocaleString()}`
    },
  },
}
</script>

<template>
  <nav-bar></nav-bar>
  <div class="content-wrapper">
    <div class="content container-fluid p-2">
      <div class="d-flex flex-column justify-content-center align-items-center">
        <h4 class="text-center w-50 mt-2">
          {{ message }}
        </h4>

        <div class="container mt-2 mb-4">
          <h2 class="text-center mb-4">GROWTH NOTES</h2>
          <table class="table table-bordered table-hover">
            <thead class="table-primary text-center">
              <tr>
                <th scope="col">Issuer/CUSIP</th>
                <th scope="col">Term</th>
                <th scope="col">Redemption</th>
                <th scope="col">Amt Invested</th>
                <th scope="col">Current Value</th>
                <th scope="col">%</th>
                <th scope="col">Intrinsic Value</th>
                <th scope="col">%</th>
                <th scope="col">Protection</th>
                <th scope="col">Upside Participation</th>
                <th scope="col">Underlying Index Performance</th>
                <th scope="col">Features</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(note, index) in notes" :key="index">
                <td>{{ note.issuer }}</td>
                <td>{{ note.term }}</td>
                <td>{{ note.redemption }}</td>
                <td>{{ formatCurrency(note.amountInvested) }}</td>
                <td>{{ formatCurrency(note.currentValue) }}</td>
                <td>{{ note.percentage }}%</td>
                <td>{{ formatCurrency(note.intrinsicValue) }}</td>
                <td>{{ note.intrinsicPercentage }}%</td>
                <td>{{ note.protection }}</td>
                <td>{{ note.upsideParticipation }}</td>
                <td>
                  <ul>
                    <li
                      v-for="performance in note.indexPerformance"
                      :key="performance"
                    >
                      {{ performance }}
                    </li>
                  </ul>
                </td>
                <td>{{ note.features }}</td>
              </tr>
              <tr class="table-primary text-center">
                <td colspan="3"><strong>TOTAL</strong></td>
                <td>
                  <strong>{{ formatCurrency(totalInvested) }}</strong>
                </td>
                <td>
                  <strong>{{ formatCurrency(totalCurrentValue) }}</strong>
                </td>
                <td>
                  <strong>{{ totalPercentage }}%</strong>
                </td>
                <td>
                  <strong>{{ formatCurrency(totalIntrinsicValue) }}</strong>
                </td>
                <td>
                  <strong>{{ totalIntrinsicPercentage }}%</strong>
                </td>
                <td colspan="4"></td>
              </tr>
            </tbody>
          </table>
        </div>
        <div id="chartdiv" ref="chartdiv"></div>
      </div>
    </div>
  </div>
  <footer-bar></footer-bar>
</template>

<style>
@import "styles.css";

.table-primary {
  background-color: #c6e0f5;
}

.table-hover tbody tr:hover {
  background-color: #f5f5f5;
}

ul {
  padding-left: 0;
  list-style: none;
}

#chartdiv {
  width: 80%;
  height: 600px;
}
</style>
