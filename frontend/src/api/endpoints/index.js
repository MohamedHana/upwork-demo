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
  test: endpoint({ path: "test/" }),
  newVisitor: endpoint({ path: "new_visitor/" }),
  // Dataset
  dataset: {
    list: (requestParameters) => {
      return endpoint({
        path: "products/" + constructQueryParameters(requestParameters),
      })
    },
    update: (id) => {
      return endpoint({ path: "products/" + id + "/" })
    },
  },
}

export default endpoints
