<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

export default {
  name: "GrowthReportModal",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      isDragOver: false,
      selectedFile: null,
      errorMessage: "",
    }
  },
  computed: {
    ...mapState(useDatasetStore, {
      reportsData: "reportsData",
    }),
    // Total Amt Invested
    totalInvested() {
      const total = this.reportsData.reduce(
        (sum, row) => sum + (parseFloat(row["Amt Invested"]) || 0),
        0,
      )
      return parseFloat(total.toFixed(2))
    },
    // Total Current Value
    totalCurrentValue() {
      const total = this.reportsData.reduce(
        (sum, row) => sum + (parseFloat(row["Current Value"]) || 0),
        0,
      )
      return parseFloat(total.toFixed(2))
    },
    // Total Current Value %
    totalPercentage() {
      const count = this.reportsData.length
      if (count === 0) return 0 // Avoid division by zero
      const total = this.reportsData.reduce(
        (sum, row) => sum + (parseFloat(row["Current Value %"]) || 0),
        0,
      )
      return parseFloat(total / count).toFixed(2)
    },
    // Total Intrinsic Value
    totalIntrinsicValue() {
      const total = this.reportsData.reduce(
        (sum, row) => sum + (parseFloat(row["Intrinsic Value"]) || 0),
        0,
      )
      return parseFloat(total.toFixed(2))
    },
    // Total Intrinsic Value %
    totalIntrinsicPercentage() {
      const count = this.reportsData.length
      if (count === 0) return 0 // Avoid division by zero
      const total = this.reportsData.reduce(
        (sum, row) => sum + (parseFloat(row["Intrinsic Value %"]) || 0),
        0,
      )
      return parseFloat(total / count).toFixed(2)
    },
  },
  watch: {},
  methods: {
    formatCurrency(value) {
      let output = 0

      if (Math.abs(value) >= 1.0e9) {
        output = (value / 1.0e9).toFixed(1) + "B" // Billions
      } else if (Math.abs(value) >= 1.0e6) {
        output = (value / 1.0e6).toFixed(1) + "M" // Millions
      } else if (Math.abs(value) >= 1.0e3) {
        output = (value / 1.0e3).toFixed(1) + "K" // Thousands
      } else {
        output = value.toFixed(2) // Default to 2 decimal places for smaller numbers
      }

      return `$${output.toLocaleString()}`
    },
  },
}
</script>

<template>
  <div id="growth-notes-modal" class="modal reports-modal" tabindex="-1">
    <div
      class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-fullscreen"
    >
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">GROWTH NOTES</h5>
          <button
            id="growth-notes-modal-close-button"
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body p-0">
          <div class="table-responsive-wrapper">
            <div class="table-responsive mb-0">
              <table class="table table-bordered mb-0">
                <thead class="table-primary">
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
                    <th scope="col">Protection Level</th>
                    <th scope="col">Max Return</th>
                    <th scope="col">Upside Participation</th>
                    <th scope="col">Underliers</th>
                    <th scope="col">Features</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row, rowIndex) in reportsData" :key="rowIndex">
                    <td>{{ row["Issuer/CUSIP"] }}</td>
                    <td>{{ row.Term }}</td>
                    <td>{{ row.Redemption }}</td>
                    <td>{{ formatCurrency(row["Amt Invested"]) }}</td>
                    <td>{{ formatCurrency(row["Current Value"]) }}</td>
                    <td>{{ row["Current Value %"] }}%</td>
                    <td>{{ formatCurrency(row["Intrinsic Value"]) }}</td>
                    <td>{{ row["Intrinsic Value %"] }}%</td>
                    <td>{{ row.Protection }}</td>
                    <td>{{ row["Protection Level"] }}</td>
                    <td>{{ row["Max Return"] }}</td>
                    <td>{{ row["Upside Participation"] }}</td>
                    <td>
                      <span
                        v-for="underlier in row['Underliers']"
                        class="badge d-block mb-1"
                        :class="{
                          'text-bg-primary': underlier.active,
                          'text-bg-secondary': !underlier.active,
                        }"
                      >
                        {{ underlier.name }}
                        <span class="ms-1 d-block" v-if="underlier.active"
                          >({{ underlier.performance }})</span
                        >
                      </span>
                    </td>
                    <td>{{ row.Features }}</td>
                  </tr>
                </tbody>
                <tfoot class="table-primary">
                  <tr class="text-center">
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
                    <td colspan="6"></td>
                  </tr>
                </tfoot>
              </table>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Go back to data files
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Fullscreen Modal adjustments */
.reports-modal .modal-fullscreen .modal-body {
  display: flex;
  flex-direction: column;
  padding: 0;
  height: 100%;
  overflow: hidden;
}

/* Wrapper to ensure table takes full height of modal body */
.reports-modal .table-responsive-wrapper {
  display: flex;
  flex-direction: column;
  height: 100%;
}

/* Ensure the table is scrollable horizontally and vertically */
.reports-modal .table-responsive {
  height: 100%;
  overflow: auto; /* Handles both horizontal and vertical scrolling */
}

/* Fixed header and footer */
.reports-modal table thead {
  position: sticky;
  top: 0;
  z-index: 1000;
  background-color: white; /* Add background to prevent overlap with content when sticky */
}

.reports-modal table tfoot {
  position: sticky;
  bottom: 0;
  z-index: 1000;
  background-color: white; /* Add background to prevent overlap with content when sticky */
}

/* Ensure table rows and header/footer take full width */
.reports-modal table {
  table-layout: fixed;
  width: 100%;
}

/* Ensure columns have a minimum width */
.reports-modal table td,
.reports-modal table th {
  text-align: center;
  vertical-align: middle;
}
</style>
