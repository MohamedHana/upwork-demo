// Generate api endpoint urls
export function endpoint({
  domain = import.meta.env.VITE_API_DOMAIN,
  prefix = import.meta.env.VITE_API_PREFIX,
  path = "",
}) {
  let endpoint = ""

  if (domain) {
    endpoint += domain + "/"
  }

  if (prefix) {
    endpoint += prefix + "/"
  }

  if (path) {
    endpoint += path
  }

  return endpoint
}

export function constructQueryParameters(requestParameters) {
  return "?" + new URLSearchParams(requestParameters).toString()
}

// Application endpoints
const endpoints = {
  // Logging
  test: endpoint({ path: "test" }),
  // Dataset
  dataset: {
    list: (requestParameters) => {
      return endpoint({
        path: "products/" + constructQueryParameters(requestParameters),
      })
    },
    reload: endpoint({ path: "dataset/reload/" }),
    update: (id) => {
      return endpoint({ path: "products/" + id + "/" })
    },
    copy: (id) => {
      return endpoint({ path: "products/" + id + "/copy/" })
    },
    delete: (id) => {
      return endpoint({ path: "products/" + id + "/" })
    },
    search: (requestParameters) => {
      return endpoint({
        path: "products/search/" + constructQueryParameters(requestParameters),
      })
    },
    changeAvailablity: (id) => {
      return endpoint({ path: "products/" + id + "/change_availablity/" })
    },
    generateMarketingPostContentUsingAI: (id) => {
      return endpoint({
        path: "products/" + id + "/generate_marketing_post_content_using_ai/",
      })
    },
  },
}

export default endpoints
