<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

import Column from "./Column.vue"
import Row from "./Row.vue"

export default {
  name: "DataArea",
  setup() {},
  components: {
    Column,
    Row,
  },
  created() {},
  mounted() {
    document.addEventListener(
      "click",
      this.handleHighlightedCellsDropdownClosing,
    )
  },
  beforeUnmount() {
    document.removeEventListener(
      "click",
      this.handleHighlightedCellsDropdownClosing,
    )
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
      highlighted_cells: "highlighted_cells",
      datasetIsInitialized: "datasetIsInitialized",
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
      setHighlightedCellsIsDragging: "setHighlightedCellsIsDragging",
      setHighlightedCellsDropdownIsVisible:
        "setHighlightedCellsDropdownIsVisible",
      setHighlightedCellsDropdownStyle: "setHighlightedCellsDropdownStyle",
      setHighlightedCellsStartCell: "setHighlightedCellsStartCell",
      setHighlightedCellsEndCell: "setHighlightedCellsEndCell",
    }),
    highlightedCellsStartDragging(event) {
      event.stopImmediatePropagation()
      event.stopPropagation()
      event.preventDefault()

      this.setHighlightedCellsIsDragging(true)
      this.setHighlightedCellsDropdownIsVisible(false)

      const start_cell = {
        row: parseInt(event.target.getAttribute("data-row-index")),
        col: parseInt(event.target.getAttribute("data-column-index")),
      }
      this.setHighlightedCellsStartCell({ ...start_cell })
      this.setHighlightedCellsEndCell({ ...start_cell })
    },
    highlightedCellsStopDragging(event) {
      if (this.highlighted_cells.is_dragging) {
        this.setHighlightedCellsIsDragging(false)
        this.setHighlightedCellsDropdownIsVisible(true)

        const end_cell = {
          row: parseInt(event.target.getAttribute("data-row-index")),
          col: parseInt(event.target.getAttribute("data-column-index")),
        }
        this.setHighlightedCellsEndCell(end_cell)

        // Calculate dropdown position
        const rect = event.target.getBoundingClientRect()
        const dropdownWidth = 150 // Adjust this to match your dropdown width
        const dropdownHeight = 100 // Adjust this to match your dropdown height

        let top = rect.bottom
        let left = rect.left

        // Adjust position to prevent overflow
        if (rect.bottom + dropdownHeight > window.innerHeight) {
          top = rect.top - dropdownHeight
        }
        if (rect.left + dropdownWidth > window.innerWidth) {
          left = rect.right - dropdownWidth
        }

        this.setHighlightedCellsDropdownStyle({
          top: `${top}px`,
          left: `${left}px`,
        })
      }
    },
    highlightedCellsOnMouseOver(event) {
      if (this.highlighted_cells.is_dragging) {
        const end_cell = {
          row: parseInt(event.target.getAttribute("data-row-index")),
          col: parseInt(event.target.getAttribute("data-column-index")),
        }
        this.setHighlightedCellsEndCell(end_cell)
      }
    },
    isHighlightedCell(row, col) {
      if (
        !this.highlighted_cells.start_cell ||
        !this.highlighted_cells.end_cell
      )
        return false

      const startRow = Math.min(
        this.highlighted_cells.start_cell.row,
        this.highlighted_cells.end_cell.row,
      )
      const endRow = Math.max(
        this.highlighted_cells.start_cell.row,
        this.highlighted_cells.end_cell.row,
      )
      const startCol = Math.min(
        this.highlighted_cells.start_cell.col,
        this.highlighted_cells.end_cell.col,
      )
      const endCol = Math.max(
        this.highlighted_cells.start_cell.col,
        this.highlighted_cells.end_cell.col,
      )

      return (
        row >= startRow && row <= endRow && col >= startCol && col <= endCol
      )
    },
    handleDropdownAction(action) {
      alert(`Selected action: ${action}`)
      this.setHighlightedCellsDropdownIsVisible(false)
    },
    handleHighlightedCellsDropdownClosing(event) {
      const highlightedCellsDropdown = this.$refs.highlightedCellsDropdown
      if (
        highlightedCellsDropdown &&
        !highlightedCellsDropdown.contains(event.target) &&
        !event.target.closest(".dataset-table-body")
      ) {
        this.setHighlightedCellsDropdownIsVisible(false)
        this.clearHighlightFromCells()
      }
    },
    clearHighlightFromCells() {
      this.setHighlightedCellsStartCell(null)
      this.setHighlightedCellsEndCell(null)
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
      <video controls>
        <source src="@/assets/videos/starter.mp4" type="video/mp4" />
        Your browser does not support the video tag.
      </video>
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
