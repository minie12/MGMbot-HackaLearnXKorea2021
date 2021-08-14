<template>
  <div class="wrapper">
    <div class="alert alert-warning">
      <div class="container">
        <button type="button" aria-hidden="true" class="close" @click="event => removeNotify(event,'alert-warning')">
          <md-icon>clear</md-icon>
        </button>
        <div class="alert-icon">
          <md-icon>warning</md-icon>
        </div>
        <b> 해상도 최적화 </b>
        <p>
            : 이 페이지는 PC 화면에 최적화 되어있습니다!
            모바일에서는 화면 배치가 일부 맞지 않는 점 양해 바랍니다.
        </p>
      </div>
    </div>
      <iframe
          src='https://webchat.botframework.com/embed/MGMbot?s=1yJdze_CsLw.L2e0hOiGvlXoX9kkxVP-mNjjA9Wsp9uoqYyRVP89Hdc'
          style='min-width: 200px; width: 100%; min-height: 800px;'></iframe>
  </div>
</template>

<script>
import { Pagination } from "@/components";
import { Slider } from "@/components";
import { Badge } from "@/components";

export default {
  components: {
  },
  methods: {
    removeNotify(e, notifyClass) {
      var target = e.target;
      while (target.className.indexOf(notifyClass) === -1) {
        target = target.parentNode;
      }
      return target.parentNode.removeChild(target);
    }
  },
  data() {
    return {
      defaultPagination: 3,
      infoPagination: 3,
      initial: null,
      floatingLabel: null,
      success: null,
      error: null,
      withMIcon: null,
      withFaIcon: null,
      checkbox1: true,
      checkbox2: null,
      checkbox3: true,
      checkbox4: null,
      radio1: true,
      radio2: false,
      switch1: true,
      switch2: null,
      amount: 30,
      amount2: 60,
      buffer: 40,
      sliders: {
        simple: 40,
        rangeSlider: [20, 60]
      }
    };
  }
};
</script>

<style lang="scss" scoped>
.vertical-center {
  display: flex;
  align-items: center;
}
.flex-column {
  display: flex;
  flex-direction: column;
}
.md-checkbox,
.md-radio {
  display: flex;
  margin: 0;
  margin-bottom: 0.5rem;
}
</style>
