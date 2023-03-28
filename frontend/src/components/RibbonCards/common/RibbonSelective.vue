<template>
  <div
    class="ribbon-item row justify-start"
    style="padding: 0 0.5rem"
    align="left"
  >
    <div :class="{ flex: true, 'q-pr-sm': hasIcon }" style="align-self: center">
      <q-icon v-if="hasIcon" size="1.35em" :name="icon" />
    </div>

    <div v-if="hasTitle" class="flex q-pr-sm ribbon-button-text">
      <div v-html="title" class="non-selectable"></div>
      <RibbonToolTipBox :tooltip="tooltip" />
    </div>
    <q-select
      class="flex col"
      v-model="model"
      :options="selection"
      dense
      options-dense
      hide-bottom-space
    >
    </q-select>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import RibbonToolTipBox from './RibbonToolTipBox.vue';

export default defineComponent({
  name: 'RibbonSelective',
  components: { RibbonToolTipBox },
  props: ['title', 'icon', 'selection', 'tooltip'],
  setup(props) {
    return {
      model: ref(props.selection[0]),
      hasTitle: props.title != '',
      hasIcon: props.icon != '',
    };
  },
});
</script>

<style scoped>
:deep(.q-field__control) {
  min-height: 30px !important;
  height: 32px !important;
}

:deep(.q-field__native) {
  padding: 0;
  min-height: 0 !important;
  height: 30px !important;
}

:deep(.q-field__marginal) {
  height: 30px !important;
}

.q-select:deep(.q-field__native > span) {
  font-size: 0.9em;
  min-width: 85px;
  width: '100%';
}
</style>
