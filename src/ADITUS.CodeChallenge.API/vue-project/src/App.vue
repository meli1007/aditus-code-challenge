
<template>
  <header>
    <div id="app">
    </div>
    <div class="wrapper">
      <nav>
        <RouterLink to="/statistics">Statistics</RouterLink>
        <RouterLink to="/reservation">Hardware Reservieren</RouterLink>
        <RouterLink to="/release">Freigabe zur Reservierung</RouterLink>
      </nav>
    </div>
  </header>
  <RouterView />
</template>


<script lang="ts">
  import { defineComponent } from 'vue';
  
  export default defineComponent({});
</script>




<style scoped>
  .container {
    max-width: 1000px;
    margin: 0 auto;
  }
  header {
    width: 100%;
    position: fixed;
    top: 0;
    left: 0;
    background-color: #f9f9f9;
    z-index: 1000;
  }
  .wrapper {
    display: flex;
    justify-content: center;
  }

  nav {
    display: flex;
    gap: 1rem;
  }

  nav a.router-link-exact-active:hover {
    color: #007bff;
    font-weight: bold;
  }

  nav a:hover {
    background-color: #e0e0e0;
    border-radius: 4px;
  }

  @media (min-width: 1024px) {
    header {
      display: flex;
      place-items: center;
      padding-right: calc(var(--section-gap) / 2);
    }

    .logo {
      margin: 0 2rem 0 0;
    }

    header .wrapper {
      display: flex;
      place-items: flex-start;
      flex-wrap: wrap;
    }

    nav {
      display: flex;
      margin: 0px;
      padding: 0px;
      font-size: 1rem;
    }
  }
</style>
