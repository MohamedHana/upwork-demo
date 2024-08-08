const routerOptions = [
  {
    path: "/",
    name: "HomePage",
    component: "home/HomePage",
    meta: {},
  },
  {
    path: "/:pathMatch(.*)*",
    name: "PageNotFound",
    component: "error/Error404",
    meta: {},
  },
]

const routes = routerOptions.map((route) => {
  return {
    ...route,
    component: () => import(`@/views/${route.component}.vue`),
  }
})

export default routes
