<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

import Cell from "./Cell.vue"

export default {
  name: "Row",
  setup() {},
  components: {
    Cell,
  },
  created() {},
  mounted() {},
  beforeUnmount() {},
  props: {
    row: {
      type: Object,
      required: true,
    },
    row_index: {
      type: Number,
      required: true,
    },
  },
  data() {
    return {}
  },
  computed: {
    ...mapState(useDatasetStore, {
      columns: "columns",
      columnsCount: "columnsCount",
      rows: "rows",
      rowsCount: "rowsCount",
    }),
  },
  watch: {},
  methods: {
    ...mapActions(useDatasetStore, {
      generateNewColumn: "generateNewColumn",
      addColumn: "addColumn",
      addNewColumnToDatasetRecords: "addNewColumnToDatasetRecords",
      addNewColumnToDatasetColumns: "addNewColumnToDatasetColumns",
      generateNewRow: "generateNewRow",
      addRow: "addRow",
    }),
  },
}
</script>

<template>
  <tr>
    <td class="sticky-column mw-80px">{{ row.index + 1 }}</td>
    <template
      v-for="(cell, cell_index) in row.data"
      :key="'cell-' + row_index + '-' + cell_index"
    >
      <Cell
        v-if="!columns[cell_index].is_hidden"
        :row="row"
        :row_index="row_index"
        :column="columns[cell_index]"
        :column_index="cell_index"
        :cell="cell"
      ></Cell>
    </template>
  </tr>
</template>

<style></style>
