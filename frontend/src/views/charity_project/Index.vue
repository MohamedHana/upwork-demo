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
      message: "Need a website made for a charity. Around 8-9 pages.",
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
        <h4 class="text-center w-50 mb-4 mt-4">
          {{ message }}
        </h4>
        <h4 class="text-center w-50 mb-4 mt-4">
          Let's meet Huzaifa, brainstorm your workflow and determine the website
          features
        </h4>
      </div>
    </div>
  </div>
  <footer-bar></footer-bar>
</template>

<style>
@import "styles.css";

#chartdiv {
  width: 100%;
  height: 500px;
}
</style>
