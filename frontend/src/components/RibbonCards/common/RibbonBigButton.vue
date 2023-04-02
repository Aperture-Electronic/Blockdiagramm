<template>
  <q-btn-group
    flat
    spread
    class="ribbon-button column"
    v-if="hasMenu"
    style="height: 100px; padding: 0 0"
  >
    <q-btn class="col-auto" @click="onClick">
      <div class="q-pb-xs">
        <q-icon size="2.8em" :name="icon" />
      </div>
      <RibbonToolTipBox :tooltip="tooltip" />
    </q-btn>
    <q-btn>
      <div class="column">
        <div class="col ribbon-button-text" v-html="title"></div>
        <q-icon
          class="col"
          size="1.25em"
          name="mdi-menu-down"
          style="align-self: center"
        />
      </div>
      <RibbonMenu :menu="menu" />
    </q-btn>
  </q-btn-group>
  <q-btn v-else class="ribbon-button" style="height: 100px" @click="onClick">
    <div class="column">
      <div class="col q-pb-xs">
        <q-icon size="2.8em" :name="icon" />
      </div>
      <div class="col ribbon-button-text" v-html="title"></div>
    </div>
    <RibbonToolTipBox :tooltip="tooltip" />
  </q-btn>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import RibbonToolTipBox from './RibbonToolTipBox.vue';
import RibbonMenu from './RibbonMenu.vue';
import eventBus from 'src/event/EventBus';

export default defineComponent({
  name: 'RibbonBigButton',
  components: { RibbonToolTipBox, RibbonMenu },
  props: ['title', 'icon', 'menu', 'tooltip', 'action'],
  setup(props) {
    return {
      hasMenu: typeof props.menu != 'undefined' && props.menu.length > 0,
    };
  },
  methods: {
    onClick() {
      eventBus.emit(this.action, null);
    },
  },
});
</script>
<style>
@import 'src/css/ribbon.css';
</style>
