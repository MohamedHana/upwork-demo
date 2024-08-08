<script>
export default {
  name: "Datasets",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  data() {
    return {}
  },
  computed: {},
  watch: {},
  methods: {},
}
</script>

<template>
  <div class="container mt-3">
    <div class="d-flex align-items-center justify-content-between">
      <div>Showing 1 to 10 of 25 datasets</div>
      <div>
        <nav aria-label="Page navigation example">
          <ul class="pagination justify-content-center mb-0">
            <li class="page-item disabled">
              <a class="page-link">Previous</a>
            </li>
            <li class="page-item"><a class="page-link" href="#">1</a></li>
            <li class="page-item"><a class="page-link" href="#">2</a></li>
            <li class="page-item"><a class="page-link" href="#">3</a></li>
            <li class="page-item">
              <a class="page-link" href="#">Next</a>
            </li>
          </ul>
        </nav>
      </div>
      <div class="d-flex">
        <input
          class="form-control"
          type="text"
          placeholder="Search"
          aria-label="search datasets"
        />
        <button type="button" class="btn btn-secondary ms-2">Search</button>
      </div>
    </div>
    <div class="mt-3">
      <div class="table-responsive">
        <table class="table">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">ID</th>
              <th scope="col">Name</th>
              <th scope="col">Created At</th>
              <th scope="col">Updated At</th>
              <th scope="col"></th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>1</td>
              <td>1</td>
              <td>First dataset</td>
              <td>2024-06-07 07:54 AM</td>
              <td>2024-06-07 07:54 AM</td>
              <td>
                <a
                  class="btn btn-link text-decoration-none p-0"
                  href="/dataset/1"
                >
                  <span class="badge text-bg-primary">Open</span>
                </a>
                <a class="btn btn-link text-decoration-none p-0">
                  <span class="badge text-bg-danger ms-2">Delete</span>
                </a>
              </td>
            </tr>
            <tr>
              <td>1</td>
              <td>1</td>
              <td>First dataset</td>
              <td>2024-06-07 07:54 AM</td>
              <td>2024-06-07 07:54 AM</td>
              <td>
                <span class="badge text-bg-secondary">Enter</span>
                <span class="badge text-bg-danger ms-2">Delete</span>
              </td>
            </tr>
            <tr>
              <td>1</td>
              <td>1</td>
              <td>First dataset</td>
              <td>2024-06-07 07:54 AM</td>
              <td>2024-06-07 07:54 AM</td>
              <td>
                <span class="badge text-bg-secondary">Enter</span>
                <span class="badge text-bg-danger ms-2">Delete</span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="mt-3">
        <button class="btn btn-primary">
          <i class="fa-solid fa-plus me-2"></i>
          Create new dataset
        </button>
      </div>
    </div>
  </div>
</template>

<style></style>
