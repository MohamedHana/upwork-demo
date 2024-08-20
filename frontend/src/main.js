import "./assets/main.css"
import * as bootstrap from "bootstrap/dist/js/bootstrap.bundle"
window.bootstrap = bootstrap

import { createApp } from "vue"
import { store } from "./stores/index"
import router from "./router/index"
import api from "@/api"
import App from "./App.vue"

import NavBar from "@/components/Navbar.vue"
import FooterBar from "@/components/Footerbar.vue"

const app = createApp(App)

app.use(store)
app.use(router)
app.config.globalProperties.api = api

// Register the global components
app.component("NavBar", NavBar)
app.component("FooterBar", FooterBar)

app.mount("#app")

// Track visitors
if (import.meta.env.VITE_ON_STAGING_SERVER !== "true") {
  let params = {
    visited_url: document.URL,
  }

  api.requests
    .restful(api.endpoints.newVisitor, {
      method: "POST",
      body: JSON.stringify(params),
    })
    .then((response) => {
      console.log(response)
    })
}
