import { defineStore } from "pinia"
import api from "@/api/index"

export const useDatasetStore = defineStore("dataset", {
  state: () => ({
    highlighted_cells: {
      is_mouse_down: false,
      start_cell: null,
      end_cell: null,
      dropdown: {
        is_visible: false,
        style: {},
      },
    },
    imported_file_data: [],
    columns: [
      {
        key: "first_name",
        name: "first name",
        title: "First name",
        type: "text",
        is_loading: false,
        is_primary: true,
        is_primitive: true,
        is_retitling: false,
        is_draggable: true,
        is_hidden: false,
        is_pinned: false,
      },
      {
        key: "last_name",
        name: "last name",
        title: "Last name",
        type: "text",
        is_loading: false,
        is_primary: true,
        is_primitive: true,
        is_retitling: false,
        is_draggable: true,
        is_hidden: false,
        is_pinned: false,
      },
      {
        key: "email",
        name: "email",
        title: "Email",
        type: "email",
        is_loading: false,
        is_primary: true,
        is_primitive: true,
        is_retitling: false,
        is_draggable: true,
        is_hidden: false,
        is_pinned: false,
      },
      {
        key: "job_title",
        name: "job title",
        title: "Job title",
        type: "text",
        is_loading: false,
        is_primary: true,
        is_primitive: true,
        is_retitling: false,
        is_draggable: true,
        is_hidden: false,
        is_pinned: false,
      },
    ],
    rows: [
      {
        index: 0,
        selected: false,
        is_loading: false,
        data: [
          { column_name: "first name", value: "Mohamed", is_loading: false },
          { column_name: "last name", value: "Hana", is_loading: false },
          {
            column_name: "email",
            value: "mohamed.hana0@gmail.com",
            is_loading: false,
          },
          {
            column_name: "job title",
            value: "Software Developer",
            is_loading: false,
          },
        ],
      },
      {
        index: 1,
        selected: false,
        is_loading: false,
        data: [
          { column_name: "first name", value: "Ahmed", is_loading: false },
          { column_name: "last name", value: "Ali", is_loading: false },
          {
            column_name: "email",
            value: "hana.pipeapps@gmail.com",
            is_loading: false,
          },
          {
            column_name: "job title",
            value: "System Archetict",
            is_loading: false,
          },
        ],
      },
    ],
    configs: {
      columns: {
        dragged_index: null,
        placeholder_index: null,
      },
      rows: {},
      filter: {},
      sort: {},
      search: {},
    },
  }),
  getters: {
    columnsCount: (state) => {
      return state.columns.length
    },
    rowsCount: (state) => {
      return state.rows.length
    },
    allData: (state) => {
      return state.imported_file_data
    },
    datasetIsInitialized: (state) => {
      return true
    },
  },
  actions: {
    // Setters
    setHighlightedCellsIsDragging(is_dragging) {
      this.highlighted_cells.is_dragging = is_dragging
    },
    setHighlightedCellsDropdownIsVisible(is_visible) {
      this.highlighted_cells.dropdown.is_visible = is_visible
    },
    setHighlightedCellsDropdownStyle(style) {
      this.highlighted_cells.dropdown.style = style
    },
    setHighlightedCellsStartCell(coordinates) {
      this.highlighted_cells.start_cell = coordinates
    },
    setHighlightedCellsEndCell(coordinates) {
      this.highlighted_cells.end_cell = coordinates
    },
    setImportedFileData(data) {
      this.imported_file_data = data
    },
    setData(data) {
      this.data = data
    },
    setRows(rows) {
      this.rows = rows
    },
    setColumns(columns) {
      this.columns = columns
    },
    setColumnsDragIndex(index) {
      this.configs.columns.dragged_index = index
    },
    setColumnsPlaceholderIndex(index) {
      this.configs.columns.placeholder_index = index
    },
    // Custom actions
    findColumnIndexByName(column_name) {
      return this.columns.findIndex((column) => column.name === column_name)
    },
    generateNewColumnConfigs(custom_configs = {}) {
      const key = Math.random().toString(36).slice(2, 13)

      let column_configs = {
        key: key,
        name: "column_" + key,
        title: "New Column",
        type: "text",
        is_loading: false,
        is_primary: false,
        is_primitive: false,
        is_retitling: false,
        is_draggable: true,
        is_hidden: false,
        is_pinned: false,
        ...custom_configs,
      }

      return column_configs
    },
    datasetAddColumn(configs) {
      let new_column_configs = this.generateNewColumnConfigs(configs)

      this.addNewColumnToDatasetColumns(new_column_configs)

      this.addNewColumnToDatasetRows(new_column_configs)

      return new_column_configs
    },
    addNewColumnToDatasetColumns(new_column) {
      this.columns.push(new_column)
    },
    addNewColumnToDatasetRows(new_column, value = "") {
      this.rows.forEach((row) => {
        row.data.push({
          column_name: new_column.name,
          is_loading: new_column.is_loading,
          value: value,
        })
      })
    },
    generateNewRowConfigs(custom_configs = {}) {
      let new_row = {
        selected: false,
        is_loading: false,
        data: [],
        ...custom_configs,
      }

      return new_row
    },
    addRow() {
      let new_row_configs = this.generateNewRowConfigs()

      this.columns.forEach((column) => {
        new_row_configs.data.push({
          column_name: column.name,
          is_loading: column.is_loading,
          value: "",
        })
      })

      this.rows.push(new_row_configs)
    },
    fillCell(cell) {
      const column_index = this.findColumnIndexByName(cell.column.name)

      this.rows[cell.row_index].data[column_index].value = cell.value
      this.rows[cell.row_index].data[column_index].is_loading = false
    },
    appendImportedFileColumnsToDatasetColumns(imported_file_columns) {
      imported_file_columns.forEach((column) => {
        this.columns.push(column)
      })
    },
    appendImportedFileRowsToDatasetRows(imported_file_rows) {
      this.rows.forEach((row, row_index) => {
        imported_file_rows[row_index].data.forEach((cell) => {
          row.data.push(cell)
        })
      })
    },
    enableRetitleColumn(column_to_retitle) {
      this.columns.forEach((column) => {
        if (column.name === column_to_retitle.name) {
          column.is_retitling = true
        }
      })
    },
    retitleColumn(column_to_retitle, new_title) {
      this.columns.forEach((column) => {
        if (column.name === column_to_retitle.name) {
          column.title = new_title
          column.is_retitling = false
        }
      })
    },
    cancelRetitleColumn(column_to_cancel_retitle) {
      this.columns.forEach((column) => {
        if (column.name === column_to_cancel_retitle.name) {
          column.is_retitling = false
        }
      })
    },
    deleteDatasetColumn(column_to_delete) {
      return new Promise((resolve) => {
        // Delete from columns
        this.columns = this.columns.filter(
          (column) => column.name !== column_to_delete.name,
        )

        // Delete from rows
        this.rows.forEach((row) => {
          row.data = row.data.filter(
            (record) => record.column_name !== column_to_delete.name,
          )
        })

        resolve(true)
      })
    },
    swapDatasetColumns(from_index, to_index) {
      // Swap columns
      const temp_column = this.columns[from_index]
      this.columns.splice(from_index, 1)
      this.columns.splice(to_index, 0, temp_column)

      // Swap each row's cells
      this.rows.forEach((row) => {
        const temp_cell = row.data[from_index]
        row.data.splice(from_index, 1)
        row.data.splice(to_index, 0, temp_cell)
      })
    },
    hideColumn(column_to_hide) {
      this.columns.forEach((column) => {
        if (column.name === column_to_hide.name) {
          column.is_hidden = true
        }
      })
    },
    showColumn(column_to_show) {
      this.columns.forEach((column) => {
        if (column.name === column_to_show.name) {
          column.is_hidden = false
        }
      })
    },
    pinColumn(column_to_pin) {
      this.columns.forEach((column) => {
        if (column.name === column_to_pin.name) {
          column.is_pinned = true
        }
      })
    },
    unpinColumn(column_to_unpin) {
      this.columns.forEach((column) => {
        if (column.name === column_to_unpin.name) {
          column.is_pinned = false
        }
      })
    },
    getColumnWidth(column_index) {
      const column_element = document.getElementById(
        "column_th_" + this.columns[column_index].key,
      )
      return column_element ? column_element.offsetWidth : 0
    },
    getPinnedColumnStyle(column_index, offset = 0) {
      if (this.columns[column_index].is_pinned) {
        const left = this.columns
          .slice(0, column_index)
          .filter((column) => column.is_pinned)
          .reduce(
            (total, _, colIndex) => total + this.getColumnWidth(colIndex),
            0,
          )
        return { left: `${left + 80}px`, zIndex: column_index + offset + 2 }
      }

      return {}
    },
    async reloadDataset() {
      try {
        let response = await api.requests.restful(
          api.endpoints.dataset.reload,
          {
            body: JSON.stringify({
              imported_file_data: this.imported_file_data,
              all_data: this.allData,
              columns: this.columns,
              rows: this.rows,
              configs: this.configs,
            }),
          },
        )

        console.log(response)
      } catch (error) {
        console.error(error)
      }
    },
  },
})
