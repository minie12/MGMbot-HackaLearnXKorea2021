<template>
  <div class="wrapper">

    <div v-if="isMobile">

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
            : 이 페이지는 PC 화면에 최적화 되어있습니다! <br>
            모바일에서는 화면이 일부 겹쳐보일 수 있습니다.
          </p>
        </div>
      </div>
        <iframe
            src='https://webchat.botframework.com/embed/MGMbot?s=1yJdze_CsLw.L2e0hOiGvlXoX9kkxVP-mNjjA9Wsp9uoqYyRVP89Hdc'
            style='min-width: 200px; width: 100%; min-height: 600px;'></iframe>

      </div>

    <div v-else>

      <div class="alert alert-success">
        <div class="container">
          <button type="button" aria-hidden="true" class="close" @click="event => removeNotify(event,'alert-success')">
            <md-icon>clear</md-icon>
          </button>
          <div class="alert-icon">
            <md-icon>toys</md-icon>
          </div>
          안녕하세요! MGM 서비스입니다.
          <p>
            운전면허 시험에 대해 궁금한 것을 질문해주세요!
          </p>
        </div>
      </div>

      <iframe
          src='https://webchat.botframework.com/embed/MGMbot?s=1yJdze_CsLw.L2e0hOiGvlXoX9kkxVP-mNjjA9Wsp9uoqYyRVP89Hdc'
          style='min-width: 200px; width: 100%; min-height: 800px;'></iframe>

    </div>


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
    },
    onResize(){
      this.isMobile = window.innerWidth < 600
    }
  },
  data() {
    return {
      isMobile: false,
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
  },
  beforeDestroy() {
    if(typeof window !== 'undefined'){
      window.removeEventListener('resize', this.onResize, {passive: true})
    }
  },
  mounted() {
    this.onResize()
    window.addEventListener('resize', this.onResize, {passive: true})
  },
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
