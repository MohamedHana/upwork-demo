<script>
export default {
  name: "DataVisualizationPage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message: "data visualization dashboard needed within a 2 week time frame",
    }
  },
  computed: {},
  watch: {},
  methods: {},
}
</script>

<template>
  <nav-bar></nav-bar>
  <div class="content-wrapper">
    <div class="content container-fluid p-2">
      <div
        class="d-flex flex-column justify-content-center align-items-center h-100"
      >
        <h3>The data visualization dashboard project</h3>
        <h4 class="text-center w-50 mb-4 mt-4">
          {{ message }}
        </h4>
        <h4 class="text-center w-50 mb-4 mt-4">
          Let's meet, brainstorm your dashboard, determine features, and set a
          timeline
        </h4>
      </div>
    </div>
  </div>
  <footer-bar></footer-bar>
</template>

<style>
@import "styles.css";
</style>
