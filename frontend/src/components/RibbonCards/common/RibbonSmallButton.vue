<template>
  <q-btn-group v-if="hasMenu" flat class="ribbon-button" style="padding: 0 0">
    <q-btn style="padding: 0 0.5rem" align="left">
      <div :class="{ flex: true, 'q-pr-sm': hasTitle }">
        <q-icon size="1.35em" :name="icon" />
      </div>
      <div v-if="hasTitle" class="flex ribbon-button-text" v-html="title"></div>
      <RibbonToolTipBox :tooltip="tooltip" />
    </q-btn>
    <q-btn class="q-px-none">
      <q-icon
        class="col"
        size="1.25em"
        name="mdi-menu-down"
        style="align-self: center"
      />
      <RibbonMenu :menu="menu" />
    </q-btn>
  </q-btn-group>
  <q-btn v-else class="ribbon-button" style="padding: 0 0.5rem" align="left">
    <div :class="{ flex: true, 'q-pr-sm': hasTitle }">
      <q-icon size="1.35em" :name="icon" />
    </div>
    <div v-if="hasTitle" class="flex ribbon-button-text" v-html="title"></div>
    <RibbonToolTipBox :tooltip="tooltip" />
  </q-btn>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import RibbonToolTipBox from './RibbonToolTipBox.vue';
import RibbonMenu from './RibbonMenu.vue';

export default defineComponent({
  name: 'RibbonSmallButton',
  components: { RibbonToolTipBox, RibbonMenu },
  props: ['title', 'icon', 'tooltip', 'menu'],
  setup(props) {
    return {
      hasTitle: props.title != '',
      hasMenu: typeof props.menu != 'undefined' && props.menu.length > 0,
    };
  },
});
</script>
<style>
@import 'src/css/ribbon.css';
</style>
