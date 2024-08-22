<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

import TopArea from "./components/TopArea.vue"
import DataArea from "./components/DataArea.vue"
import BottomArea from "./components/BottomArea.vue"

export default {
  name: "DatasetsPage",
  setup() {},
  components: {
    TopArea,
    DataArea,
    BottomArea,
  },
  created() {},
  mounted() {},
  data() {
    return {
      isDragOver: false,
      selectedFile: null,
      errorMessage: "",
    }
  },
  computed: {
    ...mapState(useDatasetStore, {
      haveDatasets: "haveDatasets",
      datasets: "datasets",
      reportsData: "reportsData",
    }),
    fileSize() {
      if (this.selectedFile) {
        return `${(this.selectedFile.size / 1024).toFixed(2)} KB`
      }
      return ""
    },
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
      if (this.totalInvested === 0) return 0 // Avoid division by zero
      const percentage = (this.totalCurrentValue / this.totalInvested - 1) * 100
      return parseFloat(percentage.toFixed(2))
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
      if (this.totalInvested === 0) return 0 // Avoid division by zero
      const percentage =
        (this.totalIntrinsicValue / this.totalInvested - 1) * 100
      return parseFloat(percentage.toFixed(2))
    },
  },
  watch: {},
  methods: {
    ...mapActions(useDatasetStore, {
      loadNewDataFile: "loadNewDataFile",
      dumpDataset: "dumpDataset",
    }),
    handleDragOver() {
      this.isDragOver = true
    },
    handleDragLeave() {
      this.isDragOver = false
    },
    handleFileDrop(event) {
      this.isDragOver = false
      const files = event.dataTransfer.files
      if (files.length > 0) {
        this.validateAndSetFile(files[0])
      }
    },
    triggerFileInput() {
      this.$refs.fileInput.click()
    },
    handleFileSelect(event) {
      const files = event.target.files
      if (files.length > 0) {
        this.validateAndSetFile(files[0])
      }
    },
    validateAndSetFile(file) {
      if (file.name.endsWith(".xlsx") || file.name.endsWith(".csv")) {
        this.selectedFile = file
        this.errorMessage = ""
        this.loadNewDataFile(file)
      } else {
        this.errorMessage =
          "Invalid file type. Please upload a .xlsx or .csv file."
        this.selectedFile = null
      }
    },
    viewDataset(dataset) {
      alert("This feature is not implemented yet.")
    },
    unloadDataset(dataset) {
      alert("This feature is not implemented yet.")
    },
    deleteDataset(dataset) {
      alert("This feature is not implemented yet.")
    },
    formatCurrency(value) {
      return `$${value.toLocaleString()}`
    },
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
  <nav-bar></nav-bar>
  <div class="content-wrapper">
    <div class="content">
      <div class="container-fluid h-100 p-3">
        <div class="d-flex flex-column h-100">
          <div class="my-2 flex-grow-1 overflow-auto">
            <div v-if="haveDatasets">
              <div class="container my-2">
                <div class="d-flex align-items-center justify-content-between">
                  <div>
                    <button
                      type="button"
                      class="btn btn-secondary btn-sm"
                      @click="triggerFileInput"
                    >
                      Import a new data file
                    </button>
                  </div>
                  <div>
                    <button
                      type="button"
                      class="btn btn-primary btn-sm"
                      @click="viewReports"
                      data-bs-toggle="modal"
                      data-bs-target="#growth-notes-modal"
                    >
                      GROWTH NOTES
                    </button>
                  </div>
                  <div v-if="false" class="d-flex">
                    <input
                      class="form-control form-control-sm"
                      type="text"
                      placeholder="Search"
                      aria-label="search datasets"
                    />
                    <button type="button" class="btn btn-secondary btn-sm ms-2">
                      Search
                    </button>
                  </div>
                </div>
                <div class="mt-3">
                  <div class="table-responsive">
                    <table class="table">
                      <thead>
                        <tr>
                          <th scope="col" width="5%">#</th>
                          <th scope="col" width="40%">File name</th>
                          <th scope="col" width="5%">Number of rows</th>
                          <th scope="col" width="25%">Imported At</th>
                          <th scope="col" width="25%"></th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr
                          v-for="(dataset, datasetIndex) in datasets"
                          :key="`row-${dataset.code}`"
                        >
                          <td>{{ datasetIndex + 1 }}</td>
                          <td>{{ dataset.source.name }}</td>
                          <td>{{ dataset.json_data.length }}</td>
                          <td>{{ dataset.imported_at }}</td>
                          <td>
                            <button
                              type="button"
                              class="btn btn-secondary btn-sm"
                              @click="viewDataset(dataset)"
                            >
                              View
                            </button>
                            <button
                              type="button"
                              class="btn btn-primary btn-sm ms-2"
                              @click="unloadDataset(dataset)"
                              v-if="dataset.is_dumped"
                            >
                              Unload from reports
                            </button>
                            <button
                              type="button"
                              class="btn btn-primary btn-sm ms-2"
                              @click="dumpDataset(dataset)"
                              v-else
                            >
                              Dump to reports
                            </button>
                            <button
                              type="button"
                              class="btn btn-danger btn-sm ms-2"
                              @click="deleteDataset(dataset)"
                            >
                              Delete
                            </button>
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
            </div>
            <div
              class="d-flex justify-content-center align-items-center h-100"
              :class="{ 'd-none': haveDatasets }"
            >
              <div class="container w-50">
                <div
                  class="drop-zone"
                  :class="{ dragover: isDragOver }"
                  @dragover.prevent="handleDragOver"
                  @dragleave="handleDragLeave"
                  @drop.prevent="handleFileDrop"
                  @click="triggerFileInput"
                >
                  <p>Drag & Drop your .xlsx or .csv data file here</p>
                  <small>or click to select a file from your computer</small>
                </div>
                <input
                  type="file"
                  ref="fileInput"
                  class="d-none"
                  @change="handleFileSelect"
                  accept=".xlsx, .csv"
                />
                <div id="fileInfo" class="mt-3 text-center">
                  <span v-if="selectedFile"
                    ><strong>Selected File:</strong> {{ selectedFile.name }} ({{
                      fileSize
                    }})</span
                  >
                  <span v-else class="text-danger">{{ errorMessage }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <footer-bar></footer-bar>

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
                    <th scope="col">Underlying Index Performance</th>
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
                    <td>{{ row.Underliers }}</td>
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

<style>
@import "styles.css";
.drop-zone {
  border: 2px dashed #0d6efd;
  border-radius: 5px;
  padding: 40px;
  text-align: center;
  cursor: pointer;
  background-color: #f8f9fa;
  transition: background-color 0.3s;
  min-height: 250px;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.drop-zone.dragover {
  background-color: #e9ecef;
}
</style>
