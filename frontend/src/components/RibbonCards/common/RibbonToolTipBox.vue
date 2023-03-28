<template>
  <q-tooltip
    :class="{ 'ribbon-tooltip': true, 'ribbon-tooltip-fixed': tooltipFixed }"
    :offset="offset"
    :delay="250"
    anchor="bottom start"
    self="top left"
    transition-show="scale"
    transition-hide="scale"
  >
    <q-card flat class="q-px-none q-py-none">
      <q-card-section class="q-py-xs">
        <div class="text-caption text-weight-bold">{{ tooltip.title }}</div>
      </q-card-section>
      <q-separator class="q-mx-sm" />
      <q-card-section class="q-py-sm">
        <div class="text-body2" v-html="tooltip.description"></div>
      </q-card-section>
    </q-card>
  </q-tooltip>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'RibbonToolTipBox',
  components: {},
  props: ['tooltip', 'fixed'],
  setup(prop) {
    let tooltipFixed = typeof prop.fixed == 'undefined' ? true : prop.fixed;

    return {
      tooltipFixed: tooltipFixed,
      offset: tooltipFixed ? [0, 0] : [0, 15],
    };
  },
});
</script>

<style>
@import 'src/css/ribbon.css';
</style>
