<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"
import addColumnModal from "./AddColumnModal.vue"
import Papa from "papaparse"

export default {
  name: "TopArea",
  setup() {},
  components: {
    addColumnModal,
  },
  created() {},
  async mounted() {
    document
      .getElementById("import-file-input")
      .addEventListener("change", this.handleImportedFileInput, false)
  },
  data() {
    return {
      imported_file_data: [],
    }
  },
  computed: {
    ...mapState(useDatasetStore, {
      columns: "columns",
      columns_count: "columns_count",
      rows: "rows",
      rows_count: "rows_count",
    }),
    imported_file_has_data: (state) => {
      return state.imported_file_data.length > 0
    },
  },
  watch: {},
  methods: {
    ...mapActions(useDatasetStore, {
      generateNewColumnConfigs: "generateNewColumnConfigs",
      generateNewRowConfigs: "generateNewRowConfigs",
      setImportedFileData: "setImportedFileData",
      setData: "setData",
      setColumns: "setColumns",
      setRows: "setRows",
      addNewColumnToDatasetRecords: "addNewColumnToDatasetRecords",
      addNewColumnToDatasetColumns: "addNewColumnToDatasetColumns",
      addRow: "addRow",
      appendImportedFileColumnsToDatasetColumns:
        "appendImportedFileColumnsToDatasetColumns",
      appendImportedFileRowsToDatasetRows:
        "appendImportedFileRowsToDatasetRows",
    }),
    appendFileDataToCurrentDataset() {
      if (this.imported_file_has_data) {
        const imported_file_first_row = this.imported_file_data[0]
        const imported_file_column_titles = Object.keys(imported_file_first_row)

        // Get imported file columns
        const imported_file_columns = []
        imported_file_column_titles.forEach(
          (imported_file_column_title, imported_file_column_title_index) => {
            const new_column_configs = this.generateNewColumnConfigs({
              name: imported_file_column_title,
              title: imported_file_column_title,
              is_primary: true,
              type: "text",
            })

            imported_file_columns[imported_file_column_title_index] =
              new_column_configs
          },
        )

        // Get imported file rows
        const imported_file_rows = []
        this.imported_file_data.forEach((row, row_index) => {
          const new_row_configs = this.generateNewRowConfigs({
            index: row_index,
          })

          // Fill row with data
          imported_file_columns.forEach(
            (imported_file_column, imported_file_column_index) => {
              new_row_configs.data[imported_file_column_index] = {
                column_name: imported_file_column.name,
                value: row[imported_file_column.title],
                is_loading: false,
              }
            },
          )

          imported_file_rows[row_index] = new_row_configs
        })

        // Append imported columns and rows to dataset
        this.appendImportedFileColumnsToDatasetColumns(imported_file_columns)
        this.appendImportedFileRowsToDatasetRows(imported_file_rows)

        // Close the modal
        document.getElementById("import-modal-close-button").click()
      }
    },
    replaceCurrentDatasetWithFileData() {
      if (this.imported_file_has_data) {
        const imported_file_first_row = this.imported_file_data[0]
        const imported_file_column_titles = Object.keys(imported_file_first_row)

        // Get imported file columns
        const imported_file_columns = []
        imported_file_column_titles.forEach(
          (imported_file_column_title, imported_file_column_title_index) => {
            const new_column_configs = this.generateNewColumnConfigs({
              name: imported_file_column_title,
              title: imported_file_column_title,
              is_primary: true,
              type: "text",
            })

            imported_file_columns[imported_file_column_title_index] =
              new_column_configs
          },
        )

        // Get imported file rows
        const imported_file_rows = []
        this.imported_file_data.forEach((row, row_index) => {
          const new_row_configs = this.generateNewRowConfigs({
            index: row_index,
          })

          // Fill row with data
          imported_file_columns.forEach(
            (imported_file_column, imported_file_column_index) => {
              new_row_configs.data[imported_file_column_index] = {
                column_name: imported_file_column.name,
                value: row[imported_file_column.title],
                is_loading: false,
              }
            },
          )

          imported_file_rows[row_index] = new_row_configs
        })

        // Set dataset columns and rows
        this.setImportedFileData(this.imported_file_data)
        this.setColumns(imported_file_columns)
        this.setRows(imported_file_rows)

        // Close the modal
        document.getElementById("import-modal-close-button").click()
      }
    },
    handleImportedFileInput(event) {
      const file = event.target.files[0]

      if (file) {
        Papa.parse(file, {
          header: true,
          dynamicTyping: true,
          skipEmptyLines: true,
          complete: (results) => {
            this.imported_file_data = results.data
          },
          error: (error) => {
            console.error("Error parsing CSV:", error)
          },
        })
      } else {
        console.error("No file selected")
      }
    },
  },
}
</script>

<template>
  <div class="d-flex justify-content-between">
    <div>
      <button
        type="button"
        class="btn btn-secondary btn-sm"
        data-bs-toggle="modal"
        data-bs-target="#import-modal"
      >
        Import
      </button>
    </div>
    <div class="d-flex">
      <div class="dropdown mx-1">
        <button
          type="button"
          class="btn btn-light btn-sm dropdown-toggle"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Rows
        </button>
        <div class="dropdown-menu p-2">Menu</div>
      </div>
      <div class="dropdown mx-1">
        <button
          type="button"
          class="btn btn-light btn-sm dropdown-toggle"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Columns
        </button>
        <div class="dropdown-menu p-2">Menu</div>
      </div>
      <div class="dropdown mx-1">
        <button
          type="button"
          class="btn btn-light btn-sm dropdown-toggle"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Filter
        </button>
        <div class="dropdown-menu p-2">Menu</div>
      </div>
      <div class="dropdown mx-1">
        <button
          type="button"
          class="btn btn-light btn-sm dropdown-toggle"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Sort
        </button>
        <div class="dropdown-menu p-2">Menu</div>
      </div>
    </div>
    <div class="d-flex">
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
    <div class="d-flex ms-2">
      <button
        type="button"
        class="btn btn-secondary btn-sm"
        data-bs-toggle="modal"
        data-bs-target="#add-column-modal"
      >
        Add column
      </button>
    </div>
  </div>
  <addColumnModal></addColumnModal>

  <!-- Import data file modal (csv) -->
  <div id="import-modal" class="modal" tabindex="-1">
    <div
      class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg"
    >
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Import your data file (.csv format)</h5>
          <button
            id="import-modal-close-button"
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <div class="mb-3">
            <label for="import-file-input" class="form-label"
              >Select a file</label
            >
            <input
              class="form-control"
              type="file"
              id="import-file-input"
              accept=".csv"
            />
          </div>
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Cancel
          </button>
          <button
            type="button"
            class="btn btn-primary"
            @click="appendFileDataToCurrentDataset()"
          >
            Append to current dataset
          </button>
          <button
            type="button"
            class="btn btn-primary"
            @click="replaceCurrentDatasetWithFileData()"
          >
            Replace current dataset
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style></style>
