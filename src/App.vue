<script setup>
import { RouterView } from "vue-router"
import NavBar from "@/components/Navbar.vue"
import FooterBar from "@/components/Footerbar.vue"
</script>

<template>
  <nav-bar></nav-bar>
  <RouterView />
  <footer-bar></footer-bar>
</template>

<style></style>
