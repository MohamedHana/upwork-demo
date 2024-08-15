<script>
export default {
  name: "WorkflowAppPage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message:
        "Make an application to automise my workflow whit api connection to WhatsApp and SnelStart (administration system). GPS function, clock in and out function",
    }
  },
  computed: {},
  watch: {},
  methods: {},
}
</script>

<template>
  <div class="content container-fluid p-2">
    <div
      class="d-flex flex-column justify-content-center align-items-center h-100"
    >
      <h3>Auto Workflow App</h3>
      <h4 class="text-center w-50 mb-4 mt-4">
        {{ message }}
      </h4>
      <h4 class="text-center w-50 mb-4 mt-4">
        Let's meet, brainstorm your workflow and determine the application
        features
      </h4>
    </div>
  </div>
</template>

<style>
@import "styles.css";

#chartdiv {
  width: 100%;
  height: 500px;
}
</style>
