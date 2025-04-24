<script>
export default {
  name: "CleManagerPage",
  setup() {},
  components: {},
  created() {},
  mounted() {},
  beforeUnmount() {},
  data() {
    return {
      message:
        "Waiting for Louis to meet, brainstorm, and start collaboration!",
    }
  },
  computed: {},
  watch: {},
  methods: {},
}
</script>

<template>
  <nav-bar></nav-bar>
  <div class="content-wrapper">
    <div class="content container-fluid p-2">
      <div
        class="d-flex flex-column justify-content-center align-items-center h-100"
      >
        <h3>CleManager SaaS (MVP) for real estate professionals</h3>
        <h4 class="text-center w-50 mb-4 mt-2">
          {{ message }}
        </h4>
        <section class="mt-4">
          <h2>Architecture</h2>
          <ul>
            <li>
              <strong>Frontend:</strong> Built with <em>Next.js</em> using the
              App Router and <em>Tailwind CSS</em> for a fast, mobile-first UI.
            </li>
            <li>
              <strong>Backend:</strong> RESTful API developed with
              <em>Express.js</em>. Each domain (auth, key rings, users, etc.)
              will be modularized.
            </li>
            <li>
              <strong>Database:</strong> <em>PostgreSQL</em> schema designed for
              relational integrity and scalability (optionally using Prisma ORM
              or Sequelize).
            </li>
            <li>
              <strong>Authentication & Roles:</strong> JWT-based authentication
              with role-based middleware (Admin, Agent, Receptionist).
            </li>
          </ul>
        </section>
        <section>
          <h2>DevOps & Collaboration</h2>
          <ul>
            <li>
              <strong>GitHub:</strong> Source control with feature-based
              branching.
            </li>
            <li>
              <strong>CI/CD:</strong> Automated deployment to the staging server
              for fast iteration.
            </li>
            <li>
              <strong>Slack & Trello:</strong> Streamlined team communication
              and task tracking.
            </li>
            <li>
              <strong>Weekly Sprints:</strong> Clear deliverables with demos at
              the end of each sprint.
            </li>
          </ul>
        </section>
      </div>
    </div>
  </div>
  <footer-bar></footer-bar>
</template>

<style>
@import "styles.css";

#chartdiv {
  width: 100%;
  height: 500px;
}
</style>
