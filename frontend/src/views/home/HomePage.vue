<script>
export default {
  name: "HomePage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message: "Waiting for you Dr Abderisak to meet online!",
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
        <h3>The Quran Web App (Arabic/Swedish)</h3>
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
