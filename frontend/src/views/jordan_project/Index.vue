<script>
export default {
  name: "JordanProjectPage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message: "Waiting for you Jordan to meet online!",
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
        <h3>Logo design and van design branding</h3>
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
</style>
