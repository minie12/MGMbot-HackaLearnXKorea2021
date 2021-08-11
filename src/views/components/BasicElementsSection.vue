<template>
  <div class="wrapper">
    <p align="center">

      <iframe
          src='https://webchat.botframework.com/embed/trial0810?s=rz523UjS6Jg.B912gVkOIKg7isVBL_JwAiGAbJEHX0FCKwnQVImVHIk'
          style='min-width: 400px; width: 800px; min-height: 700px;'></iframe>

    </p>
  </div>
</template>

<script>
import { Pagination } from "@/components";
import { Slider } from "@/components";
import { Badge } from "@/components";

export default {
  components: {
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
