const controller = new AbortController()
const signal = controller.signal

const default_configs = {
  method: "POST", // *GET, POST, PUT, DELETE, etc.
  mode: "cors", // no-cors, *cors, same-origin
  cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
  credentials: "same-origin", // include, *same-origin, omit
  headers: {
    "Content-Type": "application/json",
    // 'Content-Type': 'application/x-www-form-urlencoded',
  },
  redirect: "follow", // manual, *follow, error
  referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
  body: "", // body data type must match "Content-Type" header
  signal: signal,
}

// RESTFUL API request
export async function restful(url = "", configs = {}) {
  let response = await fetch(url, {
    ...default_configs,
    ...configs,
  })

  if (!response.ok) {
    const error = await response.json()
    throw new Error(error || "Something went wrong")
  }

  return response.json()
}

// Streaming API request
export async function streaming(url = "", configs = {}) {
  try {
    let response = await fetch(url, {
      ...default_configs,
      ...configs,
    })

    const json_response = await response.json()

    console.log(json_response)
    return json_response
  } catch (error) {
    console.log(error)
    return error
  }
}
