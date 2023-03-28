<template>
  <q-card flat class="ribbon-panel">
    <q-card-section class="q-px-xs q-py-xs ribbon-panel-content">
      <RibbonCardContent v-if="hasContent" :columns="cardContent" />
      <div class="ribbon-panel-empty text-subtitle2" v-else>
        <section>Nothing<br />Here</section>
      </div>
    </q-card-section>
    <div class="ribbon-panel-title">
      <q-separator inset />
      <q-card-section class="q-py-xs justify-bottom">
        <div class="q-py-none text-caption text-weight-medium text-center">
          {{ title }}
        </div>
      </q-card-section>
    </div>
  </q-card>
</template>
<script lang="ts">
import RibbonCardContent from './RibbonCardContent.vue';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'RibbonCard',
  components: { RibbonCardContent },
  props: ['title', 'cardContent'],
  setup(props) {
    return {
      hasContent: typeof props.cardContent != 'undefined',
    };
  },
});
</script>

<style>
@import 'src/css/ribbon.css';
</style>
