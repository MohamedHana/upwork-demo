import "./assets/main.css"
import * as bootstrap from "bootstrap/dist/js/bootstrap.bundle"
window.bootstrap = bootstrap

import { createApp } from "vue"
import { store } from "./stores/index"
import router from "./router/index"
import api from "@/api"
import App from "./App.vue"

const app = createApp(App)

app.use(store)
app.use(router)
app.config.globalProperties.api = api

app.mount("#app")

// Track visitors
if (import.meta.env.VITE_ON_STAGING_SERVER === "true") {
  api.requests.restful(api.endpoints.newVisitor).then((response) => {
    console.log(response)
  })
}
