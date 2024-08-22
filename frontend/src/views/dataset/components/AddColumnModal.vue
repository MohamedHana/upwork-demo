<script>
import { mapState, mapActions } from "pinia"
import { useDatasetStore } from "@/stores/dataset"

export default {
  name: "AddColumnModal",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      title: "New Column",
      column_type: "chatgpt_generation_text",
      prompt: "",
      context: "",
    }
  },
  computed: {
    ...mapState(useDatasetStore, {
      columns: "columns",
      columns_count: "columns_count",
      rows: "rows",
      rows_count: "rows_count",
    }),
    templates() {
      return {
        generate_email: {
          column_type: "chatgpt_generation_text",
          name: "generate_email",
          title: "Generate an email",
          requirements: "Requires an email column",
          description:
            "Generate an email to sell a new listing property (The Big Family Property).",
          prompt:
            "Write an email to sell our new listing property we call it (The Big Family Property) to the (/job title): (/first name) (/last name). (property features: 6 BEDROOMS, 6 BATHROOMS, (4,429 SQ.FT) HOME SIZE, (5.89 ACRES) LOT SIZE, 1986 YEAR BUILT) and it is located in (6515 CHERRY BLOSSOM LN), and mention a relation between the person's job title (/job title) and the property to boost the sale. End the email with [My Name: Mohamed Hana] [My Title: GoPros Sales Agent] [My Contact Information: mohamed.hana0@gmail.com]",
          context:
            "You are an expert persuader. Your job is to start sales conversations with potential customers as a representative of the company. You speak in a professional tone and manner with the experience of a 20 year veteran in the business field. You always are talking about how we would be able to help the person we are reaching out to and not just talking about the things the company does. Write messages that are under 100 words to potential customers. Every message you write follows the message template and is under 100 words. Your message template: First Line: [mention something that you noticed about their company. Don't make it too complimentary] Second line: [bridge what you noticed about their company with a question that makes them think thoughtfully about the problem we can solve and if they have that problem] [a case study that tells a story of how we helped another customer like them. Include the story and the quantitative results] [why should they care about whatever your intention is] [show social proof, be specific not vague] [a soft call to action]. End of template.",
        },
        browse_url: {
          column_type: "chatgpt_generation_text",
          name: "browse_url",
          title: "Browse a URL",
          requirements: "Requires a URL column",
          description: "Go to URL, browse its content and summarize it.",
          prompt: `Perform the following tasks:
- Got to "/url" 
- Summarize the page content into 50 words`,
          context:
            "You are a url/website summerizer, your task is to go to urls and summarize its content. Make sure to mention the business category/context of the url/website.",
        },
        generate_thank_you_postcard: {
          column_type: "chatgpt_generation_image",
          name: "generate_thank_you_postcard",
          title: "Generate a thank you postcard",
          requirements: "",
          description:
            "Generate a thank you postcard to send to a person on facebook.",
          prompt:
            "Design a simple and creative postcard (only the front part) to send on social networks.",
          context:
            "You are a graphic designer. Your job is to design a simple and creative postcard to send on social networks.",
        },
      }
    },
    templateNamesArray() {
      return Object.keys(this.templates)
    },
  },
  watch: {},
  methods: {
    ...mapActions(useDatasetStore, {
      datasetAddColumn: "datasetAddColumn",
      fillCell: "fillCell",
    }),
    useTemplate(template) {
      this.prompt = template.prompt
      this.context = template.context
      this.column_type = template.column_type
    },
    async addColumn() {
      const column_configs = {
        title: this.title,
        type: this.column_type,
        prompt: this.prompt,
        context: this.context,
        is_loading: true,
        is_primary: false,
        is_primitive: false,
        is_retitling: false,
        is_draggable: true,
        is_hidden: false,
        is_pinned: false,
      }

      // Add the new column to dataset
      const new_column = this.datasetAddColumn(column_configs)

      // Close the modal
      document.getElementById("add-column-modal-close-button").click()

      // Send the new column to backend to process
      try {
        const response = await fetch(
          "http://localhost:8000/dataset/add_column/",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              dataset_id: 1,
              columns: this.columns,
              rows: this.rows,
              new_column: new_column,
            }),
          },
        )

        const reader = response.body.getReader()
        const decoder = new TextDecoder()

        while (true) {
          const { done, value } = await reader.read()
          if (done) break
          const text = decoder.decode(value, { stream: true })
          this.processStreamedData(text)
        }

        // Code to run after the streaming is complete
        this.onStreamingComplete()
      } catch (error) {
        console.error("Error streaming data:", error)
      }
    },
    processStreamedData(response) {
      response = JSON.parse(response)
      console.log(response)

      this.fillCell(response)
    },
    onStreamingComplete() {
      // Any other code to run after the streaming is complete
      console.log("Streaming has completed.")
    },
  },
}
</script>

<template>
  <div id="add-column-modal" class="modal" tabindex="-1">
    <div
      class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl"
    >
      <div class="modal-content">
        <div class="modal-header">
          <input
            class="form-control w-50"
            type="text"
            placeholder="Type column title..."
            aria-label="column title"
            v-model="title"
          />
          <button
            id="add-column-modal-close-button"
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <div class="row">
            <div class="col-4">
              <span class="fw-semibold mb-2 d-block">Templates</span>
              <div class="list-group">
                <div
                  v-for="templateName in templateNamesArray"
                  :key="'template-' + templateName"
                  class="list-group-item"
                >
                  <h6 class="mb-1">{{ templates[templateName].title }}</h6>
                  <small>{{ templates[templateName].requirements }}</small>
                  <p class="my-2">{{ templates[templateName].description }}</p>
                  <button
                    class="btn btn-secondary btn-sm float-end"
                    @click="useTemplate(templates[templateName])"
                  >
                    Use template
                  </button>
                </div>
              </div>
            </div>
            <div class="col-8">
              <div class="mb-3">
                <label
                  for="chatgpt-prompt-template-textarea"
                  class="form-label d-flex justify-content-between align-items-center"
                >
                  <span class="fw-semibold">
                    Write a prompt template to perform a specific task
                  </span>
                </label>
                <textarea
                  class="form-control"
                  id="chatgpt-prompt-template-textarea"
                  rows="8"
                  placeholder='Write a ChatGPT prompt to use to generate the new column content or perform a specific task on columns. Mention a column by typing "/column title"'
                  v-model="prompt"
                ></textarea>
              </div>
              <div class="mb-3">
                <label
                  for="chatgpt-generate-text-context-textarea"
                  class="form-label d-flex justify-content-between align-items-center"
                >
                  <div>
                    <span class="fw-semibold">
                      Provide context for the task (System Prompt)
                    </span>
                    <span class="text-muted"> (optional) </span>
                  </div>
                </label>
                <textarea
                  class="form-control"
                  id="chatgpt-generate-text-context-textarea"
                  rows="8"
                  placeholder="Provide a background information or details about the task. This is to help clarify and better understand the requirements or purpose of the task so that it can be completed effectively."
                  v-model="context"
                ></textarea>
              </div>
            </div>
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
          <button type="button" class="btn btn-primary" @click="addColumn()">
            Add column
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style></style>
