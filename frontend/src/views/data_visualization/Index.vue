<script>
import * as am5 from "@amcharts/amcharts5"
import * as am5xy from "@amcharts/amcharts5/xy"
import am5themes_Animated from "@amcharts/amcharts5/themes/Animated"
import * as am5exporting from "@amcharts/amcharts5/plugins/exporting"

export default {
  name: "HomePage",
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
        "Let's meet, brainstorm your dashboard, determine features, and set a timeline",
      chart: null,
      root: null,
    }
  },
  computed: {},
  watch: {},
  methods: {
    createChart() {
      // Create root element
      const root = am5.Root.new("chartdiv")

      // Set themes
      root.setThemes([am5themes_Animated.new(root)])

      // Create chart
      const chart = root.container.children.push(
        am5xy.XYChart.new(root, {
          panX: true,
          panY: true,
          wheelX: "panX",
          wheelY: "zoomX",
          pinchZoomX: true,
          pinchZoomY: true,
        }),
      )

      // Adjust chart padding to ensure space for legend and export menu
      chart.set("paddingBottom", 50) // Increased padding at the bottom for legend
      chart.set("paddingTop", 10) // Padding at the top for export menu
      chart.set("paddingLeft", 80) // Padding on the left
      chart.set("paddingRight", 80) // Padding on the right

      // Add cursor
      const cursor = chart.set(
        "cursor",
        am5xy.XYCursor.new(root, {
          behavior: "zoomXY",
        }),
      )
      cursor.lineY.set("visible", false)

      // Create axes
      const xAxis = chart.xAxes.push(
        am5xy.DateAxis.new(root, {
          maxDeviation: 0.3,
          baseInterval: { timeUnit: "month", count: 1 },
          renderer: am5xy.AxisRendererX.new(root, {
            minGridDistance: 30,
            labels: {
              minDistance: 20, // Ensures that labels do not overlap
            },
          }),
          tooltip: am5.Tooltip.new(root, {}),
        }),
      )

      const yAxis = chart.yAxes.push(
        am5xy.ValueAxis.new(root, {
          renderer: am5xy.AxisRendererY.new(root, {}),
        }),
      )

      // Create series for Revenue
      const revenueColor = am5.color(0x007bff) // Color for Revenue series
      const revenueSeries = chart.series.push(
        am5xy.LineSeries.new(root, {
          name: "Revenue",
          xAxis: xAxis,
          yAxis: yAxis,
          valueYField: "revenue",
          valueXField: "date",
          tooltip: am5.Tooltip.new(root, {
            labelText: "{valueY}",
          }),
          stroke: revenueColor,
        }),
      )

      revenueSeries.strokes.template.setAll({
        strokeWidth: 3,
        stroke: revenueColor,
      })

      // Add bullets to Revenue series
      revenueSeries.bullets.push(function () {
        return am5.Bullet.new(root, {
          sprite: am5.Circle.new(root, {
            radius: 6,
            fill: revenueColor,
            stroke: root.interfaceColors.get("background"),
            strokeWidth: 2,
          }),
        })
      })

      // Create series for Profit
      const profitColor = am5.color(0x28a745) // Color for Profit series
      const profitSeries = chart.series.push(
        am5xy.LineSeries.new(root, {
          name: "Profit",
          xAxis: xAxis,
          yAxis: yAxis,
          valueYField: "profit",
          valueXField: "date",
          tooltip: am5.Tooltip.new(root, {
            labelText: "{valueY}",
          }),
          stroke: profitColor,
        }),
      )

      profitSeries.strokes.template.setAll({
        strokeWidth: 3,
        stroke: profitColor,
      })

      // Add bullets to Profit series
      profitSeries.bullets.push(function () {
        return am5.Bullet.new(root, {
          sprite: am5.Circle.new(root, {
            radius: 6,
            fill: profitColor,
            stroke: root.interfaceColors.get("background"),
            strokeWidth: 2,
          }),
        })
      })

      // Add a scrollbar
      chart.set(
        "scrollbarX",
        am5.Scrollbar.new(root, {
          orientation: "horizontal",
        }),
      )

      // Set data
      const data = [
        { date: new Date(2023, 0, 1).getTime(), revenue: 1000, profit: 400 },
        { date: new Date(2023, 1, 1).getTime(), revenue: 1200, profit: 450 },
        { date: new Date(2023, 2, 1).getTime(), revenue: 1400, profit: 500 },
        { date: new Date(2023, 3, 1).getTime(), revenue: 1300, profit: 480 },
        { date: new Date(2023, 4, 1).getTime(), revenue: 1500, profit: 550 },
        { date: new Date(2023, 5, 1).getTime(), revenue: 1600, profit: 600 },
        { date: new Date(2023, 6, 1).getTime(), revenue: 1700, profit: 620 },
        { date: new Date(2023, 7, 1).getTime(), revenue: 1800, profit: 650 },
        { date: new Date(2023, 8, 1).getTime(), revenue: 2000, profit: 700 },
        { date: new Date(2023, 9, 1).getTime(), revenue: 2200, profit: 750 },
        { date: new Date(2023, 10, 1).getTime(), revenue: 2400, profit: 800 },
        { date: new Date(2023, 11, 1).getTime(), revenue: 2600, profit: 850 },
      ]

      revenueSeries.data.setAll(data)
      profitSeries.data.setAll(data)

      // Add legend at the bottom with space to prevent overlapping
      const legend = chart.children.push(
        am5.Legend.new(root, {
          centerX: am5.percent(50),
          x: am5.percent(50),
          y: am5.percent(110),
          centerY: am5.percent(110),
        }),
      )
      legend.data.setAll(chart.series.values)

      // Add axis ranges for reference lines (e.g., target revenue)
      const range = yAxis.makeDataItem({
        value: 2000,
      })
      yAxis.createAxisRange(range)
      range.get("grid").setAll({
        stroke: am5.color(0xff0000),
        strokeDasharray: [5, 5],
        strokeWidth: 2,
      })
      range.get("label").setAll({
        text: "Target Revenue",
        inside: true,
        centerX: am5.p100,
        centerY: am5.p0,
        fill: am5.color(0xff0000),
      })

      // Add export menu with proper margins
      chart.set(
        "exporting",
        am5exporting.Exporting.new(root, {
          menu: am5exporting.ExportingMenu.new(root, {
            align: "right",
            valign: "top",
          }),
        }),
      )

      // Animate on load
      chart.appear(1000, 100)
      revenueSeries.appear(1000)
      profitSeries.appear(1000)

      this.root = root
      this.chart = chart
    },
  },
}
</script>

<template>
  <nav-bar></nav-bar>
  <div class="content-wrapper">
    <div class="content container-fluid p-2">
      <div class="d-flex flex-column justify-content-center align-items-center">
        <h3 class="mt-3">The data visualization dashboard project</h3>
        <h4 class="text-center w-50 mb-4 mt-2">
          {{ message }}
        </h4>

        <h5 class="">Revenue and Profit Trends Line Chart Sample</h5>
        <div id="chartdiv" ref="chartdiv"></div>

        <h5 class="my-4">Financial Analysis Report/Chart Types</h5>
        <ol class="list-group m-5 mt-0">
          <li class="list-group-item">
            <h5>Revenue and Profit Trends</h5>
            <p>
              Line Charts or Bar Charts might be used to show revenue and profit
              trends over time. This could include:
            </p>
            <ul>
              <li>Monthly, quarterly, or annual revenue trends.</li>
              <li>Comparison of actual vs. projected revenue.</li>
              <li>Profit margins and net income trends.</li>
            </ul>
          </li>
          <li class="list-group-item">
            <h5>Expense Breakdown</h5>
            <p>
              Pie Charts or Stacked Bar Charts might be used to show the
              breakdown of expenses by category (e.g., salaries, rent,
              utilities, marketing). This helps visualize how much each category
              contributes to total expenses.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Cash Flow Analysis</h5>
            <p>
              Waterfall Charts could be used to illustrate the components of
              cash flow, including operating activities, investing activities,
              and financing activities. This chart helps in understanding the
              inflows and outflows of cash over a period of time.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Budget vs. Actual Performance</h5>
            <p>
              Comparison Bar Charts or Line Charts might be used to compare
              budgeted figures against actual performance, helping to identify
              variances in revenue, expenses, and profit.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Key Financial Ratios</h5>
            <p>
              Radar Charts or Line Charts might be used to present key financial
              ratios such as Return on Investment (ROI), Return on Equity (ROE),
              Debt-to-Equity Ratio, and Current Ratio over time. These ratios
              provide insights into the financial health of the company.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Investment Portfolio Performance</h5>
            <p>
              Line Charts or Area Charts might be used to track the performance
              of investment portfolios over time. This could include tracking
              the value of stocks, bonds, and other assets.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Balance Sheet Components</h5>
            <p>
              Stacked Bar Charts or Tree Maps could be used to visualize the
              components of a balance sheet, such as assets, liabilities, and
              equity. This helps in understanding the financial position of the
              company.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Profit & Loss Statement</h5>
            <p>
              Multi-series Line Charts or Stacked Area Charts might be used to
              display components of the profit & loss statement, such as
              revenue, cost of goods sold (COGS), gross profit, operating
              expenses, and net income over time.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Trend Analysis</h5>
            <p>
              Moving Average Charts or Trend Line Charts might be used to
              analyze trends in financial metrics such as sales growth, expense
              reduction, or profit growth over time. This helps in identifying
              patterns or anomalies in financial performance.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Financial Forecasting</h5>
            <p>
              Line Charts or Scenario Analysis Charts could be used to present
              financial forecasts, showing expected future revenue, expenses,
              and profit based on different scenarios (e.g., best case, worst
              case).
            </p>
          </li>
          <li class="list-group-item">
            <h5>Risk Management</h5>
            <p>
              Heat Maps or Risk Matrix Charts might be used to visualize
              financial risks and their potential impact on the company. This
              could include credit risk, market risk, and operational risk.
            </p>
          </li>
          <li class="list-group-item">
            <h5>Capital Allocation</h5>
            <p>
              Donut Charts or Bar Charts might be used to show how capital is
              allocated across different departments or projects, helping to
              visualize where the company is investing its resources.
            </p>
          </li>
        </ol>
      </div>
    </div>
  </div>
  <footer-bar></footer-bar>
</template>

<style>
@import "styles.css";

#chartdiv {
  width: 100%;
  height: 500px;
}
</style>
