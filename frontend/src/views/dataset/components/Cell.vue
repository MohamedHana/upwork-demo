<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

export default {
  name: "Cell",
  setup() {},
  components: {},
  created() {},
  mounted() {
    if (!this.column.is_loading) {
      this.dropdown.instance = new bootstrap.Dropdown(
        this.$refs.cellDropdownTrigger,
      )
      document.addEventListener("click", this.handleClickOutside)
    }

    this.edit_modal.instance = new bootstrap.Modal(
      "#" + this.edit_modal.element_id,
      {},
    )
    this.view_modal.instance = new bootstrap.Modal(
      "#" + this.view_modal.element_id,
      {},
    )
  },
  beforeUnmount() {
    document.removeEventListener("click", this.handleClickOutside)

    if (this.dropdown.instance) {
      this.dropdown.instance.dispose()
    }

    if (this.edit_modal.instance) {
      this.edit_modal.instance.dispose()
    }

    if (this.view_modal.instance) {
      this.view_modal.instance.dispose()
    }
  },
  props: {
    row: {
      type: Object,
      required: true,
    },
    row_index: {
      type: Number,
      required: true,
    },
    column: {
      type: Object,
      required: true,
    },
    column_index: {
      type: Number,
      required: true,
    },
    cell: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      dropdown: {
        instance: null,
      },
      edit_modal: {
        element_id:
          "cell_edit_modal_" + this.row_index + "_" + this.column_index,
        ref: "cell_edit_modal_" + this.row_index + "_" + this.column_index,
        instance: null,
      },
      view_modal: {
        element_id:
          "cell_view_modal_" + this.row_index + "_" + this.column_index,
        ref: "cell_view_modal_" + this.row_index + "_" + this.column_index,
        instance: null,
      },
    }
  },
  computed: {
    ...mapState(useDatasetStore, {
      columns: "columns",
      columnsCount: "columnsCount",
      rows: "rows",
      rowsCount: "rowsCount",
      highlighted_cells: "highlighted_cells",
    }),
    cIsLoading() {
      return this.column.is_loading
    },
  },
  watch: {
    "cell.is_loading": function (is_loading) {
      console.log("init dropdown")
      if (!is_loading) {
        this.$nextTick(() => {
          this.dropdown.instance = new bootstrap.Dropdown(
            this.$refs.cellDropdownTrigger,
          )
          document.addEventListener("click", this.handleClickOutside)
        })
      }
    },
  },
  methods: {
    ...mapActions(useDatasetStore, {
      generateNewColumn: "generateNewColumn",
      addColumn: "addColumn",
      addNewColumnToDatasetRecords: "addNewColumnToDatasetRecords",
      addNewColumnToDatasetColumns: "addNewColumnToDatasetColumns",
      generateNewRow: "generateNewRow",
      addRow: "addRow",
      getPinnedColumnStyle: "getPinnedColumnStyle",
    }),
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
    toggleDropdown(event) {
      document.getElementsByTagName("body")[0].click()

      if (this.dropdown.instance) {
        if (!this.$refs.cellDropdownTrigger.classList.contains("show")) {
          this.dropdown.instance.show()
        } else {
          this.dropdown.instance.hide()
        }
      }
    },
    handleClickOutside(event) {
      if (this.dropdown.instance && !event.target.closest(".dropdown-menu")) {
        this.dropdown.instance.hide()
      }
    },
    hideDropdown() {
      if (this.dropdown.instance) {
        this.dropdown.instance.hide()
      }
    },
    openCellForEdit() {
      if (this.edit_modal.instance) {
        this.edit_modal.instance.show()

        this.hideDropdown()
      }
    },
    editCellValue() {},
    viewCellValue() {
      if (this.view_modal.instance) {
        this.view_modal.instance.show()

        this.hideDropdown()
      }
    },
  },
}
</script>

<template>
  <td
    :class="[
      'cell mw-200px',
      {
        'cell-highlight': isHighlightedCell(row_index, column_index),
        'column-pinned': column.is_pinned,
      },
    ]"
    :data-row-index="row_index"
    :data-column-index="column_index"
    :style="getPinnedColumnStyle(column_index)"
  >
    <span v-if="cell.is_loading">Loading...</span>
    <div v-else class="dropdown">
      <span
        class="cell-value-span"
        aria-expanded="false"
        ref="cellDropdownTrigger"
        @contextmenu.prevent="toggleDropdown"
      >
        {{ cell.value }}
      </span>
      <ul class="dropdown-menu" ref="cellDropdownMenu">
        <li>
          <a class="dropdown-item" href="#" @click="openCellForEdit">Edit</a>
        </li>
        <li>
          <a
            class="dropdown-item"
            href="#"
            v-if="column.type === 'chatgpt_generation_image'"
            @click="viewCellValue"
            >View</a
          >
        </li>
      </ul>
    </div>
  </td>
  <Teleport to="body">
    <div
      class="modal fade"
      :id="'cell_edit_modal_' + row_index + '_' + column_index"
      tabindex="-1"
      :aria-labelledby="'cell_edit_modal_' + row_index + '_' + column_index"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-body">
            <textarea class="form-control" rows="3">{{ cell.value }}</textarea>
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
              @click="editCellValue"
            >
              Edit
            </button>
          </div>
        </div>
      </div>
    </div>
    <div
      class="modal fade"
      :id="'cell_view_modal_' + row_index + '_' + column_index"
      tabindex="-1"
      :aria-labelledby="'cell_view_modal_' + row_index + '_' + column_index"
      aria-hidden="true"
    >
      <div
        class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg"
      >
        <div class="modal-content">
          <div class="modal-body">
            <div class="d-flex justify-content-center align-items-center w-100">
              <img :src="cell.value" width="512" height="512" alt="" />
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Close
            </button>
          </div>
        </div>
      </div>
    </div>
  </Teleport>
</template>

<style></style>
