import Vue from "vue"
import moment from "moment"

// Gglobal filter for date formatting
const timeAgo = Vue.filter("timeAgo", function (value) {
  if (!value) return ""

  return moment(value).fromNow()
})

const updatedOnDate = Vue.filter("updatedOnDate", function (value) {
  if (!value) return ""

  return moment(value).format("MMM, D YYYY")
})

export default {
  timeAgo,
  updatedOnDate,
}
