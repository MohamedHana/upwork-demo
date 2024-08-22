<template>
  <div>
    <!-- Button to trigger the modal -->
    <button
      type="button"
      class="btn btn-primary"
      data-bs-toggle="modal"
      data-bs-target="#fullscreenModal"
    >
      Open Table in Fullscreen Modal
    </button>

    <!-- Fullscreen Modal -->
    <div
      class="modal fade"
      id="fullscreenModal"
      tabindex="-1"
      aria-labelledby="fullscreenModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-fullscreen">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="fullscreenModalLabel">
              Fullscreen Modal with Scrollable Table
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body p-0">
            <!-- Scrollable Table Wrapper -->
            <div class="table-responsive-wrapper h-100">
              <div class="table-responsive h-100">
                <table class="table table-bordered">
                  <thead class="table-light">
                    <tr>
                      <th
                        v-for="header in headers"
                        :key="header"
                        style="min-width: 120px"
                      >
                        {{ header }}
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(row, rowIndex) in data" :key="rowIndex">
                      <td
                        v-for="(cell, cellIndex) in row"
                        :key="cellIndex"
                        style="min-width: 120px"
                      >
                        {{ cell }}
                      </td>
                    </tr>
                  </tbody>
                  <tfoot class="table-light">
                    <tr>
                      <td :colspan="headers.length" class="text-center">
                        Footer Content Here
                      </td>
                    </tr>
                  </tfoot>
                </table>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Close
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      headers: [
        "Issuer/CUSIP",
        "Term",
        "Redemption",
        "Amt Invested",
        "Current Value",
        "%",
        "Intrinsic Value",
        "%",
        "Protection",
        "Protection Level",
        "Max Return",
        "Upside Participation",
        "Underlying Index Performance",
        "Features",
      ],
      data: [
        [
          "Morgan Stanley, 61774XG",
          "48M",
          "04-Mar-2027",
          "$433,000",
          "$53,655,195",
          "23.91%",
          "$15,768,244.12",
          "28.79%",
          "0% Barrier",
          "Unknown",
          "unlimited",
          "Unknown",
          "Unknown",
          "Uncapped ATM Digital Worst Of Barrier Note",
        ],
        [
          "Morgan Stanley, 61774TA",
          "48M",
          "04-Mar-2027",
          "$433,000",
          "$53,655,195",
          "23.91%",
          "$15,768,244.12",
          "28.79%",
          "0% Barrier",
          "Unknown",
          "unlimited",
          "Unknown",
          "Unknown",
          "Point to Point Worst Of Buffer Note",
        ],
        // Add more rows as needed...
      ],
    }
  },
}
</script>

<style scoped>
/* Fullscreen Modal adjustments */
.reports-modal .modal-fullscreen .modal-body {
  display: flex;
  flex-direction: column;
  padding: 0;
  height: 100%;
}

/* Wrapper to ensure table takes full height of modal body */
.reports-modal .table-responsive-wrapper {
  display: flex;
  flex-direction: column;
  height: 100%;
}

/* Ensure the table is scrollable horizontally and vertically */
.reports-modal .table-responsive {
  height: 100%;
  overflow-x: auto; /* Enable horizontal scrolling */
  overflow-y: auto; /* Enable vertical scrolling */
}

/* Fixed header and footer */
.reports-modal table thead {
  position: sticky;
  top: 0;
  z-index: 1000;
  background-color: white; /* Add background to prevent overlap with content when sticky */
}

.reports-modal table tfoot {
  position: sticky;
  bottom: 0;
  z-index: 1000;
  background-color: white; /* Add background to prevent overlap with content when sticky */
}

/* Ensure table rows and header/footer take full width */
.reports-modal table {
  width: max-content; /* Ensure table takes the width based on its content */
  table-layout: auto; /* Allows the table to expand based on content */
}

/* Ensure columns have a minimum width */
.reports-modal table td,
.reports-modal table th {
  min-width: 150px;
}
</style>
