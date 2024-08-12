<script>
import * as am5 from "@amcharts/amcharts5"
import * as am5xy from "@amcharts/amcharts5/xy"
import am5themes_Animated from "@amcharts/amcharts5/themes/Animated"

export default {
  name: "HomePage",
  setup() {},
  components: {},
  created() {
    this.api.requests
      .restful(this.api.endpoints.newVisitor)
      .then((response) => {
        console.log(response)
        // this.message = response
      })
  },
  mounted() {
    // Create root element
    const root = am5.Root.new(this.$refs.chartdiv)

    // Set themes
    root.setThemes([am5themes_Animated.new(root)])

    const data = [
      { x: 1, value: 14 },
      { x: 2, value: 11 },
      { x: 3, value: 12 },
      { x: 4, value: 14 },
      { x: 5, value: 11 },
      { x: 6, value: 11 },
      { x: 7, value: 12 },
      { x: 8, value: 12 },
      { x: 9, value: 13 },
      { x: 10, value: 15 },
      { x: 11, value: 19 },
      { x: 12, value: 21 },
      { x: 13, value: 22 },
      { x: 14, value: 20 },
      { x: 15, value: 18 },
      { x: 16, value: 14 },
      { x: 17, value: 16 },
      { x: 18, value: 18 },
      { x: 19, value: 17 },
      { x: 20, value: 15 },
      { x: 21, value: 12 },
      { x: 22, value: 8 },
      { x: 23, value: 11 },
    ]

    // Create chart
    const chart = root.container.children.push(
      am5xy.XYChart.new(root, {
        panX: true,
        panY: true,
        wheelX: "panX",
        wheelY: "zoomX",
      }),
    )

    // Create axes
    const xAxis = chart.xAxes.push(
      am5xy.ValueAxis.new(root, {
        renderer: am5xy.AxisRendererX.new(root, {
          minGridDistance: 50,
        }),
        tooltip: am5.Tooltip.new(root, {}),
      }),
    )

    const yAxis = chart.yAxes.push(
      am5xy.ValueAxis.new(root, {
        renderer: am5xy.AxisRendererY.new(root, {}),
      }),
    )

    // Add series
    const series = chart.series.push(
      am5xy.LineSeries.new(root, {
        minBulletDistance: 10,
        xAxis: xAxis,
        yAxis: yAxis,
        valueYField: "value",
        valueXField: "x",
        tooltip: am5.Tooltip.new(root, {
          pointerOrientation: "horizontal",
          labelText: "{valueY}",
        }),
      }),
    )

    series.strokes.template.setAll({
      strokeWidth: 3,
    })

    series.data.setAll(data)

    series.bullets.push(() => {
      return am5.Bullet.new(root, {
        sprite: am5.Circle.new(root, {
          radius: 6,
          fill: series.get("fill"),
          stroke: root.interfaceColors.get("background"),
          strokeWidth: 2,
        }),
      })
    })

    // Add cursor
    const cursor = chart.set(
      "cursor",
      am5xy.XYCursor.new(root, {
        xAxis: xAxis,
      }),
    )
    cursor.lineY.set("visible", false)

    // Add scrollbar
    chart.set(
      "scrollbarX",
      am5.Scrollbar.new(root, {
        orientation: "horizontal",
      }),
    )

    // Make stuff animate on load
    series.appear(1000, 100)
    chart.appear(1000, 100)

    // Function to add process control ranges
    const addLimits = (lower, upper) => {
      // Add range fill
      createRange(lower, upper, undefined, am5.color(0xffce00))

      // Add upper/average/lower lines
      createRange(lower, undefined, "Lower control limit", am5.color(0x4d00ff))
      createRange(upper, undefined, "Upper control limit", am5.color(0x4d00ff))
      createRange(
        lower + (upper - lower) / 2,
        undefined,
        "Process average",
        am5.color(0x4d00ff),
        true,
      )
    }

    const createRange = (value, endValue, label, color, dashed) => {
      const rangeDataItem = yAxis.makeDataItem({
        value: value,
        endValue: endValue,
      })

      const range = yAxis.createAxisRange(rangeDataItem)

      if (endValue) {
        range.get("axisFill").setAll({
          fill: color,
          fillOpacity: 0.2,
          visible: true,
        })
      } else {
        range.get("grid").setAll({
          stroke: color,
          strokeOpacity: 1,
          strokeWidth: 2,
          location: 1,
        })

        if (dashed) {
          range.get("grid").set("strokeDasharray", [5, 3])
        }
      }

      if (label) {
        range.get("label").setAll({
          text: label,
          location: 1,
          fontSize: 19,
          inside: true,
          centerX: am5.p0,
          centerY: am5.p100,
        })
      }
    }

    // Add process control limits
    addLimits(10, 20)

    // Store references to dispose later
    this.chart = chart
    this.root = root
  },
  beforeUnmount() {
    // Dispose of the chart when the component is destroyed
    if (this.root) {
      this.root.dispose()
    }
  },
  data() {
    return {
      message: "Waiting for the figma designs to implement the charts!",
    }
  },
  computed: {},
  watch: {},
  methods: {},
}
</script>

<template>
  <div class="content container-fluid p-2">
    <div
      class="d-flex flex-column justify-content-center align-items-center h-100"
    >
      <h6 class="text-center w-50">
        {{ message }}
      </h6>

      <hr />
      <h6>Process Control Sample Chart</h6>

      <div id="chartdiv" ref="chartdiv"></div>
    </div>
  </div>
</template>

<style>
@import "styles.css";

#chartdiv {
  width: 100%;
  height: 500px;
  margin-top: 30px;
}
</style>
