<template>
  <footer
    class="footer"
    :class="{ [`footer-${type}`]: type }"
    :data-background-color="backgroundColor"
  >
    <div class="container">
      <nav>
        <ul>
          <li>
            <a href="https://github.com/solidcellaMoon/chat-web-frame">
              Github
            </a>
          </li>
          <li>
            <a href="https://github.com/devrel-kr/HackaLearn/blob/main/teams/KING.md">
              About Us
            </a>
          </li>
        </ul>
      </nav>
      <p class="copyright">
        <md-icon>computer</md-icon> {{ year }}, made by
        <a href="https://github.com/devrel-kr/HackaLearn/blob/main/teams/KING.md" target="_blank">Team: KING</a>
      </p>
    </div>
  </footer>
</template>
<script>
export default {
  props: {
    backgroundColor: String,
    type: String
  },
  data() {
    return {
      year: new Date().getFullYear()
    };
  }
};
</script>
<style></style>
