<template>
  <div class="ribbon-item row justify-start">
    <div :class="{ flex: true, 'q-pr-sm': hasIcon }" style="align-self: center">
      <q-icon v-if="hasIcon" size="1.35em" :name="icon" />
    </div>
    <div
      v-if="hasTitle"
      class="flex q-pr-sm ribbon-button-text"
      style="align-self: center"
    >
      <div v-html="title" class="non-selectable"></div>
      <RibbonToolTipBox :tooltip="tooltip" />
    </div>
    <q-btn-toggle
      v-model="model"
      class="ribbon-toggle"
      toggle-color="secondary"
      style="align-self: center"
      size="sm"
      :options="[
        { label: 'On', value: '1' },
        { label: 'Off', value: '0' },
      ]"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import RibbonToolTipBox from './RibbonToolTipBox.vue';

export default defineComponent({
  name: 'RibbonToggle',
  components: { RibbonToolTipBox },
  props: ['title', 'icon', 'tooltip'],
  setup(props) {
    return {
      hasTitle: props.title != '',
      hasIcon: props.icon != '',
      model: ref('1'),
    };
  },
});
</script>

<style scoped>
:deep(.q-field__control) {
  min-height: 30px !important;
  height: 32px !important;
}
</style>
