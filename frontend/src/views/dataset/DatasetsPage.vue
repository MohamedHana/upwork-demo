<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

import GrowthNotesReportModal from "./components/GrowthNotesReportModal.vue"

export default {
  name: "DatasetsPage",
  setup() {},
  components: {
    GrowthNotesReportModal,
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
                    <button type="button" class="btn btn-primary btn-sm ms-2">
                      INCOME NOTE
                    </button>
                    <button type="button" class="btn btn-primary btn-sm ms-2">
                      DIGITAL NOTE
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
                          <th scope="col" width="35%">File name</th>
                          <th scope="col" width="15%">Number of rows</th>
                          <th scope="col" width="20%">Imported At</th>
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
                              class="btn btn-primary btn-sm"
                              @click="unloadDataset(dataset)"
                              v-if="dataset.is_dumped"
                            >
                              Unload from reports
                            </button>
                            <button
                              type="button"
                              class="btn btn-primary btn-sm"
                              @click="dumpDataset(dataset)"
                              v-else
                            >
                              Dump data to reports
                            </button>
                            <button
                              type="button"
                              class="btn btn-secondary btn-sm ms-2"
                              @click="viewDataset(dataset)"
                            >
                              View
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

  <GrowthNotesReportModal></GrowthNotesReportModal>
  <footer-bar></footer-bar>
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
