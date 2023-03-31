<template>
  <q-layout view="hHh lpr fFf">
    <q-header bordered class="bg-primary text-white">
      <q-toolbar>
        <q-toolbar-title>Blockdiagramm</q-toolbar-title>
        <q-space />

        <q-btn dense flat icon="minimize" />
        <q-btn dense flat icon="crop_square" />
        <q-btn dense flat icon="close" />
      </q-toolbar>
      <RibbonPanel :ribbon="ribbon" />
    </q-header>
    <LeftMenuPanel />
    <q-page-container>
      <q-page>
        <div style="min-height: inherit">
          <q-splitter
            v-model="resourceBoxWidth"
            unit="px"
            style="min-height: inherit"
            :limits="[150, 350]"
            :after-class="['diagram-content']"
          >
            <template v-slot:before> </template>
            <template v-slot:after>
              <router-view />
            </template>
          </q-splitter>
        </div>
      </q-page>
    </q-page-container>
    <q-footer elevated class="bg-grey-8 text-white">
      <q-bar dense>
        <TinyServerMonitor />
      </q-bar>
    </q-footer>
  </q-layout>
</template>

<script lang="ts">
import { ref } from 'vue';
import { defineComponent } from 'vue';

import ribbon from '../layouts/RibbonLayout';
import leftMenu from '../layouts/LeftMenuLayout';

import RibbonPanel from 'src/components/RibbonPanel.vue';
import LeftMenuPanel from 'src/components/LeftMenuPanel.vue';
import TinyServerMonitor from 'src/components/StatusBar/TinyServerMonitor.vue';
import { InitalizeEventBus } from 'src/event/EventBus';

export default defineComponent({
  name: 'MainLayout',
  components: { RibbonPanel, LeftMenuPanel, TinyServerMonitor },
  setup() {
    InitalizeEventBus();
    let defRibbonPanel = ribbon.panels.find((p) => p.default);
    if (defRibbonPanel == undefined) {
      defRibbonPanel = ribbon.panels[0];
    }
    return {
      tab: ref(defRibbonPanel.name),
      ribbon: ribbon,
      leftMenu: leftMenu,
      leftMenuMiniState: ref(true),
      leftMenuPanel: ref(false),
      resourceBoxWidth: ref(250),
    };
  },
});
</script>

<style>
@import 'src/css/ribbon.css';

.diagram-content {
  min-height: inherit;
}
</style>
