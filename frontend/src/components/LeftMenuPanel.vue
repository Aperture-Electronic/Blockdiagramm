<template>
  <!-- <q-drawer
    show-if-above
    v-model="leftMenuPanel"
    :mini="leftMenuMiniState"
    @mouseover="leftMenuMiniState = false"
    @mouseout="leftMenuMiniState = true"
    :width="200"
    :breakpoint="500"
    bordered
    class="bg-grey-3"
    mini-to-overlay
  > -->
  <q-drawer
    show-if-above
    v-model="leftMenuPanel"
    :mini="leftMenuMiniState"
    bordered
    class="bg-grey-3"
    mini-to-overlay
  >
    <q-list padding>
      <q-item
        v-for="item in leftMenu.items"
        :key="item.name"
        clickable
        v-ripple
        @click="onLeftMenuClick(item)"
      >
        <q-item-section avatar>
          <q-icon :name="item.icon" />
        </q-item-section>

        <q-item-section>{{ item.title }}</q-item-section>
      </q-item>
    </q-list>
  </q-drawer>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import eventBus from 'src/event/EventBus';
import leftMenu from '../layouts/LeftMenuLayout';

export default defineComponent({
  name: 'LeftMenuPanel',
  setup() {
    let leftMenuPanel = ref(false);
    let leftMenuMiniState = ref(true);

    return {
      leftMenu: leftMenu,
      leftMenuPanel,
      leftMenuMiniState,
    };
  },
  methods: {
    onLeftMenuClick(arg: unknown) {
      let item = arg as { component: string };
      eventBus.emit('left-menu-switch', item.component);
    },
  },
});
</script>
