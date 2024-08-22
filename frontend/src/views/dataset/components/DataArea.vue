<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

export default {
  name: "DataArea",
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
      columns: "columns",
      columnsCount: "columnsCount",
      rows: "rows",
      rowsCount: "rowsCount",
      highlighted_cells: "highlighted_cells",
      datasetIsInitialized: "datasetIsInitialized",
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
      loadDataFile: "loadDataFile",
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
        this.loadDataFile(this.selectedFile)
      } else {
        this.errorMessage =
          "Invalid file type. Please upload a .xlsx or .csv file."
        this.selectedFile = null
      }
    },
  },
}
</script>

<template>
  <div class="my-2 flex-grow-1 overflow-auto">
    <div v-if="datasetIsInitialized" class="position-relative w-auto">
      <table class="table table-bordered dataset-table m-0">
        <thead>
          <tr>
            <th class="sticky-header sticky-column row-number-column mw-80px">
              #
            </th>
            <template
              v-for="(column, column_index) in columns"
              :key="'column-' + column_index"
            >
              <Column
                v-if="!column.is_hidden"
                :column="column"
                :column_index="column_index"
              ></Column>
            </template>
          </tr>
        </thead>
        <tbody class="dataset-table-body">
          <Row
            v-for="(row, row_index) in rows"
            :key="'row-' + row_index"
            :row="row"
            :row_index="row_index"
          ></Row>
        </tbody>
      </table>
    </div>
    <div v-else class="d-flex justify-content-center align-items-center h-100">
      <div class="container mt-5 w-50">
        <div
          class="drop-zone"
          :class="{ dragover: isDragOver }"
          @dragover.prevent="handleDragOver"
          @dragleave="handleDragLeave"
          @drop.prevent="handleFileDrop"
          @click="triggerFileInput"
        >
          <p>Drag & Drop your .xlsx or .csv data file here</p>
          <small>or click to select a file from you computer</small>
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
  <div
    v-if="highlighted_cells.dropdown.is_visible"
    :style="highlighted_cells.dropdown.style"
    ref="highlightedCellsDropdown"
    class="dropdown-menu highlighted-cells-dropdown-menu show"
  >
    <button class="dropdown-item" @click="handleDropdownAction('Action 1')">
      Action 1
    </button>
    <button class="dropdown-item" @click="handleDropdownAction('Action 2')">
      Action 2
    </button>
    <button class="dropdown-item" @click="handleDropdownAction('Action 3')">
      Action 3
    </button>
  </div>
</template>

<style></style>
