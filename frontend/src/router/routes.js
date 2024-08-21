const routerOptions = [
  {
    path: "/",
    name: "HomePage",
    component: "home/HomePage",
    meta: {},
  },
  {
    path: "/quiz",
    name: "QuizFunnelPage",
    component: "quizfunnel/Index",
    meta: {},
  },
  {
    path: "/workflow_app",
    name: "WorkflowAppPage",
    component: "workflow_app/Index",
    meta: {},
  },
  {
    path: "/jordan_project",
    name: "JordanProjectPage",
    component: "jordan_project/Index",
    meta: {},
  },
  {
    path: "/educate_clone",
    name: "EducateClonePage",
    component: "educate_clone/Index",
    meta: {},
  },
  {
    path: "/charity_project",
    name: "CharityProjectPage",
    component: "charity_project/Index",
    meta: {},
  },
  {
    path: "/data_visualization",
    name: "DataVisualizationPage",
    component: "data_visualization/Index",
    meta: {},
  },
  {
    path: "/law_firm",
    name: "DataVisualizationPage",
    component: "law_firm/Index",
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
