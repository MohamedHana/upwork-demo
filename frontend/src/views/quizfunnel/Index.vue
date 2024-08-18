<script>
export default {
  name: "QuizFunnelPage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message: "Message",
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
    <div class="content container-fluid p-2 h-100">
      <div class="d-flex justify-content-center align-items-center h-100">
        <div class="diet-container">
          <div class="diet-info">
            <h1 class="diet-header">
              Get your personalized training course in AI
            </h1>
            <p class="diet-subtext mt-5">
              Complete this 20-questions quiz and get your personalized training
              course in AI and start using AI to gain profit according to your
              <strong>skills</strong> and
              <strong>experience.</strong>
            </p>
            <div class="mt-5">
              <h6>Select your gender:</h6>
              <div class="gender-buttons mt-3">
                <button type="button" class="btn btn-primary">
                  Male
                  <i class="fa fa-arrow-right"></i>
                </button>
                <button type="button" class="btn btn-primary">
                  Female
                  <i class="fa fa-arrow-right"></i>
                </button>
              </div>
            </div>
          </div>
          <div class="diet-image">
            <div class="stacked-boxes">
              <div class="stacked-box stacked-box-2"></div>
              <div class="stacked-box stacked-box-1"></div>
              <img
                src="@/assets/images/ai.png"
                alt="Main Image"
                class="main-image"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <footer-bar></footer-bar>
</template>

<style>
@import "styles.css";
</style>
