import { defineStore } from "pinia"

export const useAppStore = defineStore("app", {
  state: () => ({
    app: {
      initiated: false,
      loading: false,
    },
  }),
  getters: {
    // app getters
    appIsInitiated: (state) => state.app.initiated,
    appIsLoading: (state) => state.app.loading,
  },
  actions: {
    initaiteApp() {
      // Only when not initiated and not loading
      if (!this.app.initiated && !this.app.loading) {
        console.log("app store: initaiteApp()")

        setTimeout(() => {
          this.app.initiated = true
          this.app.loading = false
        }, 500)
      }
    },
  },
})
