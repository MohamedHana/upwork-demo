<script>
import TopArea from "./components/TopArea.vue"
import DataArea from "./components/DataArea.vue"
import BottomArea from "./components/BottomArea.vue"

export default {
  name: "DatasetPage",
  setup() {},
  components: {
    TopArea,
    DataArea,
    BottomArea,
  },
  created() {},
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
  <div class="content dataset-page">
    <div class="container-fluid h-100 p-2">
      <div class="d-flex flex-column h-100">
        <TopArea></TopArea>
        <DataArea></DataArea>
        <BottomArea></BottomArea>
      </div>
    </div>
  </div>
</template>

<style>
@import "styles.css";
</style>
