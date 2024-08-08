<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

export default {
  name: "Column",
  setup() {},
  components: {},
  created() {},
  mounted() {
    this.dropdown.instance = new bootstrap.Dropdown(
      this.$refs.dropdown_button_ref,
    )
    document.addEventListener("click", this.handleClickOutside)

    this.edit_modal.instance = new bootstrap.Modal(
      "#" + this.edit_modal.element_id,
      {},
    )
    this.delete_modal.instance = new bootstrap.Modal(
      "#" + this.delete_modal.element_id,
      {},
    )

    this.initColumnResizer()
  },
  beforeUnmount() {
    document.removeEventListener("click", this.handleClickOutside)

    if (this.dropdown.instance) {
      this.dropdown.instance.dispose()
    }

    if (this.edit_modal.instance) {
      this.edit_modal.instance.dispose()
    }

    if (this.delete_modal.instance) {
      this.delete_modal.instance.dispose()
    }
  },
  props: {
    column: {
      type: Object,
      required: true,
    },
    column_index: {
      type: Number,
      required: true,
    },
  },
  data() {
    return {
      dropdown: {
        element_id: "column_dropdown_" + this.column.key,
        ref: "column_dropdown_" + this.column.key,
        instance: null,
      },
      edit_modal: {
        element_id: "column_edit_modal_" + this.column.key,
        ref: "column_edit_modal_" + this.column.key,
        instance: null,
      },
      delete_modal: {
        element_id: "column_delete_modal_" + this.column.key,
        ref: "column_delete_modal_" + this.column.key,
        instance: null,
      },
      new_title: this.column.title,
      dragged_index: null,
    }
  },
  computed: {
    ...mapState(useDatasetStore, {
      columns: "columns",
      columnsCount: "columnsCount",
      rows: "rows",
      rowsCount: "rowsCount",
      configs: "configs",
    }),
  },
  watch: {},
  methods: {
    ...mapActions(useDatasetStore, {
      generateNewColumnConfigs: "generateNewColumnConfigs",
      addColumn: "addColumn",
      addNewColumnToDatasetColumns: "addNewColumnToDatasetColumns",
      addNewColumnToDatasetRows: "addNewColumnToDatasetRows",
      generateNewRow: "generateNewRow",
      addRow: "addRow",
      fillCell: "fillCell",
      datasetAddColumn: "datasetAddColumn",
      enableRetitleColumn: "enableRetitleColumn",
      retitleColumn: "retitleColumn",
      cancelRetitleColumn: "cancelRetitleColumn",
      deleteDatasetColumn: "deleteDatasetColumn",
      swapDatasetColumns: "swapDatasetColumns",
      setColumnsDragIndex: "setColumnsDragIndex",
      setColumnsPlaceholderIndex: "setColumnsPlaceholderIndex",
      hideColumn: "hideColumn",
      pinColumn: "pinColumn",
      unpinColumn: "unpinColumn",
      getPinnedColumnStyle: "getPinnedColumnStyle",
    }),
    initColumnResizer() {
      const column_resizer = document.getElementById(
        "column_resizer_" + this.column.key,
      )
      column_resizer.addEventListener("mousedown", function (e) {
        e.stopImmediatePropagation()
        e.stopPropagation()
        e.preventDefault()

        const th = e.target.closest("th")
        const startX = e.pageX
        const startWidth = th.offsetWidth

        document.body.classList.add("cursor-col-resize")

        const onMouseMove = (e) => {
          const newWidth = startWidth + (e.pageX - startX)
          th.style.width = `${newWidth}px`
        }

        const onMouseUp = () => {
          document.body.classList.remove("cursor-col-resize")
          document.removeEventListener("mousemove", onMouseMove)
          document.removeEventListener("mouseup", onMouseUp)
        }

        document.addEventListener("mousemove", onMouseMove)
        document.addEventListener("mouseup", onMouseUp)
      })
    },
    onDragStart(event, index) {
      this.setColumnsDragIndex(index)
      event.dataTransfer.effectAllowed = "move"
      document.body.classList.add("cursor-move")
    },
    onDragOver(event, index) {
      if (
        this.configs.columns.dragged_index !== null &&
        this.configs.columns.dragged_index !== index
      ) {
        this.setColumnsPlaceholderIndex(index)
      }
    },
    onDragLeave() {
      this.setColumnsPlaceholderIndex(null)
    },
    onDrop(event, index) {
      if (
        this.configs.columns.dragged_index !== null &&
        this.configs.columns.dragged_index !== index
      ) {
        this.swapDatasetColumns(this.configs.columns.dragged_index, index)
        this.setColumnsDragIndex(null)
        this.setColumnsPlaceholderIndex(null)
      }
    },
    onDragEnd() {
      this.setColumnsDragIndex(null)
      this.setColumnsPlaceholderIndex(null)
      document.body.classList.remove("cursor-move")
    },
    async chatGPTRegenerateTextForColumn(column, column_index) {
      // Turn on loading state for column records
      this.rows.forEach((row) => (row.data[column_index].is_loading = true))

      // Send streaming request
      try {
        const response = await fetch(
          "http://localhost:8000/dataset/chatgpt_regenerate_text_for_column/",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              dataset_id: 1,
              rows: this.rows,
              columns: this.columns,
              column: column,
              column_index: column_index,
            }),
          },
        )

        const reader = response.body.getReader()
        const decoder = new TextDecoder()

        while (true) {
          const { done, value } = await reader.read()
          if (done) break
          const text = decoder.decode(value, { stream: true })
          this.processChatGPTRegenerateTextForColumnStreamedData(text)
        }

        // Code to run after the streaming is complete
        this.onChatGPTRegenerateTextForColumnComplete()
      } catch (error) {
        console.error(
          "Error streaming data (chatGPTRegenerateTextForColumn):",
          error,
        )
      }
    },
    processChatGPTRegenerateTextForColumnStreamedData(response) {
      response = JSON.parse(response)
      console.log(response)

      this.fillCell(response)
      // this.rows[response.row_index].data[response.column_index].value = response.cell_value;
      // this.rows[response.row_index].data[response.column_index].is_loading = false;
    },
    onChatGPTRegenerateTextForColumnComplete() {
      // Any other code to run after the streaming is complete
      console.log("onChatGPTRegenerateTextForColumnComplete has completed.")
    },
    async sendEmails(column, column_index) {
      // Add a new column to the dataset
      let new_column_configs = this.generateNewColumnConfigs({
        title: column.title + " email status",
        type: "text",
        is_loading: true,
      })

      // Add the new column to dataset
      const new_column = this.datasetAddColumn(new_column_configs)

      // Stream sending emails operation
      try {
        const response = await fetch(
          "http://localhost:8000/dataset/send_emails/",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              dataset_id: 1,
              new_column: new_column,
              column: column,
              rows: this.rows, // # replace with dataaset id when implement db
              columns: this.columns, // # replace with dataaset id when implement db
            }),
          },
        )

        const reader = response.body.getReader()
        const decoder = new TextDecoder()

        while (true) {
          const { done, value } = await reader.read()
          if (done) break
          const text = decoder.decode(value, { stream: true })
          this.processSendEmailsStreamedData(text)
        }

        // Code to run after the streaming is complete
        this.onSendEmailsStreamingComplete()
      } catch (error) {
        console.error("Error streaming data:", error)
      }
    },
    processSendEmailsStreamedData(response) {
      response = JSON.parse(response)
      console.log(response)

      this.fillCell(response)
      // this.rows[response.row_index].data[this.column_index].value = response.cell_value;
      // this.rows[response.row_index].data[this.column_index].is_loading = false;
    },
    onSendEmailsStreamingComplete() {
      // Any other code to run after the streaming is complete
      console.log("onSendEmailsStreamingComplete has completed.")
    },
    toggleDropdown(event) {
      document.getElementsByTagName("body")[0].click()

      if (this.dropdown.instance) {
        if (!this.$refs.dropdown_button_ref.classList.contains("show")) {
          this.dropdown.instance.show()
        } else {
          this.dropdown.instance.hide()
        }
      }
    },
    hideDropdown() {
      if (this.dropdown.instance) {
        this.dropdown.instance.hide()
      }
    },
    handleClickOutside(event) {
      if (
        this.dropdown.instance &&
        !event.target.closest(".column-dropdown-" + this.column.key)
      ) {
        this.dropdown.instance.hide()
      }
    },
    enableRetitle() {
      this.enableRetitleColumn(this.column)

      this.hideDropdown()
    },
    retitle() {
      this.retitleColumn(this.column, this.new_title)
    },
    cancelRetitle() {
      this.cancelRetitleColumn(this.column)
    },
    showEditModal() {
      if (this.edit_modal.instance) {
        this.edit_modal.instance.show()

        this.hideDropdown()
      }
    },
    pin() {
      this.pinColumn(this.column)
      this.hideDropdown()
    },
    unpin() {
      this.unpinColumn(this.column)
      this.hideDropdown()
    },
    hide() {
      this.hideColumn(this.column)
    },
    showDeleteModal() {
      if (this.delete_modal.instance) {
        this.delete_modal.instance.show()

        this.hideDropdown()
      }
    },
    deleteColumn() {
      this.deleteDatasetColumn(this.column)
        .then((is_deleted) => {
          if (is_deleted) {
            this.delete_modal.instance.hide()
          }
        })
        .catch((error) => {
          console.error(error)
        })
    },
  },
}
</script>

<template>
  <th
    :id="'column_th_' + column.key"
    class="sticky-header mw-200px"
    :draggable="true"
    @dragstart="onDragStart($event, column_index)"
    @dragend="onDragEnd"
    @dragover.prevent="onDragOver($event, column_index)"
    @dragleave="onDragLeave"
    @drop="onDrop($event, column_index)"
    :class="{
      'column-is-dragging': column_index === configs.columns.dragged_index,
      'column-dragging-placeholder':
        column_index === configs.columns.placeholder_index,
      'column-pinned': column.is_pinned,
    }"
    :style="getPinnedColumnStyle(column_index, 1)"
  >
    <div class="dropdown" :class="'column-dropdown-' + column.key">
      <div ref="dropdown_button_ref" @contextmenu.prevent="toggleDropdown">
        <div class="d-flex align-items-center" v-if="column.is_retitling">
          <input
            class="form-control form-control-sm"
            type="text"
            placeholder="Column title"
            aria-label="retitle column"
            v-model="new_title"
          />
          <button
            type="button"
            class="btn btn-primary btn-sm ms-1"
            @click="retitle"
          >
            Retitle
          </button>
          <button
            type="button"
            class="btn btn-secondary btn-sm ms-1"
            @click="cancelRetitle"
          >
            Cancel
          </button>
        </div>
        <span class="d-block" v-else>
          {{ column.title }}
        </span>
      </div>
      <div class="dropdown-menu p-2 column-actions-dropdown-menu">
        <template v-if="column.type === 'chatgpt_generation_text'">
          <button
            class="dropdown-item"
            @click="chatGPTRegenerateTextForColumn(column, column_index)"
          >
            ChatGPT: Regenerate text
          </button>
          <button
            class="dropdown-item"
            @click="sendEmails(column, column_index)"
          >
            Send emails
          </button>
          <hr class="dropdown-divider" />
        </template>
        <button class="dropdown-item" @click="enableRetitle">Retitle</button>
        <button class="dropdown-item" @click="showEditModal">Edit</button>
        <hr class="dropdown-divider" />
        <button class="dropdown-item" @click="unpin" v-if="column.is_pinned">
          Unpin
        </button>
        <button class="dropdown-item" @click="pin" v-else>Pin</button>
        <button class="dropdown-item" @click="hide">Hide</button>
        <hr class="dropdown-divider" />
        <button class="dropdown-item" @click="showDeleteModal">Delete</button>
      </div>
    </div>
    <div :id="'column_resizer_' + column.key" class="column-resizer"></div>
  </th>
  <Teleport to="body">
    <div
      class="modal fade"
      :id="'column_edit_modal_' + column.key"
      tabindex="-1"
      :aria-labelledby="'column_edit_modal_' + column.key"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-body">Edit column modal</div>
        </div>
      </div>
    </div>
    <div
      class="modal fade"
      :id="'column_delete_modal_' + column.key"
      tabindex="-1"
      :aria-labelledby="'column_delete_modal_' + column.key"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-body">
            <h5>Are you sure?</h5>
            <span>
              Are you sure you want to delete this column? You can't undo this.
            </span>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Cancel
            </button>
            <button type="button" class="btn btn-danger" @click="deleteColumn">
              Delete column
            </button>
          </div>
        </div>
      </div>
    </div>
  </Teleport>
</template>

<style></style>
