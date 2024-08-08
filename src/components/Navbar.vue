<template>
  <nav class="navbar navbar-expand-lg bg-body-tertiary">
    <div class="container-fluid">
      <a class="navbar-brand" :href="'/'">The GDrive Bridge</a>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0"></ul>
        <div class="d-flex align-items-center">
          <form class="d-flex me-2" role="search">
            <button type="button" class="btn btn-success btn-sm">
              Connect your Google Drive
            </button>
          </form>
          <div class="dropdown flex-shrink-0 mx-2">
            <a
              href="#"
              class="d-block link-body-emphasis text-decoration-none dropdown-toggle"
              data-bs-toggle="dropdown"
              data-bs-auto-close="outside"
              aria-expanded="false"
            >
              <span>
                <i class="far fa-user"></i>
              </span>
            </a>
            <div
              class="dropdown-menu text-small shadow dropdown-menu-end user-info-dropdown"
            >
              <div class="row m-2">
                <div class="col-6">
                  <input
                    class="form-control form-control-sm mb-2"
                    type="text"
                    placeholder="First name"
                    aria-label="First name"
                  />
                </div>
                <div class="col-6">
                  <input
                    class="form-control form-control-sm mb-2"
                    type="text"
                    placeholder="Last name"
                    aria-label="Last name"
                  />
                </div>
                <div class="col-12">
                  <input
                    type="email"
                    class="form-control form-control-sm mb-2"
                    id="email"
                    placeholder="Email"
                  />
                </div>
                <div class="col-12">
                  <input
                    type="text"
                    class="form-control form-control-sm mb-2"
                    id="phone"
                    placeholder="Phone"
                  />
                </div>
                <div class="col-12">
                  <button
                    type="button"
                    class="btn btn-primary btn-sm float-end"
                  >
                    Sign up
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: "nav-bar",
}
</script>

<style scoped>
.user-info-dropdown {
  width: 320px;
}
</style>
