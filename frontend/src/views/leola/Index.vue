<script>
export default {
  name: "LeolaPage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message:
        "Waiting for you Leola to meet online and brainstorm your project!",
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
        <h3>Leola's full-featured social network website staging server</h3>
        <h4 class="text-center w-50 mb-4 mt-2">
          {{ message }}
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
