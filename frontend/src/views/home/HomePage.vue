<script>
export default {
  name: "HomePage",
  setup() {},
  components: {},
  created() {
    this.api.requests.restful(this.api.endpoints.test).then((response) => {
      console.log(response)
      // this.message = response
    })
  },
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message: "Waiting for the figma designs to implement the charts!",
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
      <h6 class="text-center w-50">
        {{ message }}
      </h6>
    </div>
  </div>
</template>

<style>
@import "styles.css";
</style>
