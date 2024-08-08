<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

export default {
  name: "BottomArea",
  setup() {},
  components: {},
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
      columnsCount: "columnsCount",
      rows: "rows",
      rowsCount: "rowsCount",
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
    exportData() {},
  },
}
</script>

<template>
  <div class="d-flex justify-content-between">
    <div class="d-flex">
      <button type="button" class="btn btn-secondary btn-sm" @click="addRow()">
        Add row
      </button>
    </div>
    <div>
      <button
        type="button"
        class="btn btn-secondary btn-sm"
        @click="exportData()"
      >
        Export
      </button>
    </div>
  </div>
</template>

<style></style>
