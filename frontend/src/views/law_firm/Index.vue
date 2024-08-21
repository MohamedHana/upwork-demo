<script>
export default {
  name: "LowFirmPage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message: "Website for a small immigration law firm (solo practice).",
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
        <h3>Website (Re)Design for a Law Office</h3>
        <h4 class="text-center w-50 mb-4 mt-4">
          {{ message }}
        </h4>
        <h4 class="text-center w-50 mb-4 mt-4">
          Let's meet Danny, brainstorm your business and determine the website
          features and content
        </h4>
      </div>
    </div>
  </div>
  <footer-bar></footer-bar>
</template>

<style>
@import "styles.css";
</style>
