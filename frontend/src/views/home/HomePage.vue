<script>
export default {
  name: "HomePage",
  setup() {},
  components: {},
  created() {
    this.api.requests
      .restful(this.api.endpoints.test, {
        method: "GET",
        body: null,
      })
      .then((response) => {
        console.log(response)
      })
  },
  mounted() {},
  beforeUnmount() {},
  data() {
    return {}
  },
  computed: {},
  watch: {},
  methods: {},
}
</script>

<template>
  <div class="content container-fluid p-2">
    <div class="d-flex flex-column">Home Page</div>
  </div>
</template>

<style>
@import "styles.css";
</style>
