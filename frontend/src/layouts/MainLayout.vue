<template>
  <q-layout view="hHh lpr fFf">
    <q-header bordered class="bg-primary text-white">
      <q-toolbar style="-webkit-app-region: drag">
        <q-toolbar-title shrink>
          Block::<strong>diagramm</strong>
        </q-toolbar-title>
        <ProjectNameLabel :lblClass="['q-pt-sm', 'text-blue-grey-1']" />
        <q-space />

        <q-btn
          dense
          flat
          icon="minimize"
          style="-webkit-app-region: no-drag"
          @click="minimizeWindow"
        />
        <q-btn
          dense
          flat
          icon="crop_square"
          style="-webkit-app-region: no-drag"
          @click="maximizeWindow"
        />
        <q-btn
          dense
          flat
          icon="close"
          style="-webkit-app-region: no-drag"
          @click="closeWindow"
        />
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
            :before-class="['diagram-content']"
            :after-class="['diagram-content']"
          >
            <template v-slot:before>
              <keep-alive>
                <component v-bind:is="leftMenu" />
              </keep-alive>
            </template>
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
    <DialogesContainer />
  </q-layout>
</template>

<script lang="ts">
import { ref } from 'vue';
import { defineComponent } from 'vue';

import ribbon from '../layouts/RibbonLayout';
import eventBus from 'src/event/EventBus';
import ElectronApi from 'src-electron/api/electron-api';

import RibbonPanel from 'src/components/RibbonPanel.vue';
import LeftMenuPanel from 'src/components/LeftMenuPanel.vue';
import TinyServerMonitor from 'src/components/StatusBar/TinyServerMonitor.vue';
import DialogesContainer from 'src/components/DialogesContainer.vue';
import ProjectNameLabel from 'src/components/ProjectNameLabel.vue';
import LeftMenuSources from 'src/components/LeftMenu/LeftMenuSources.vue';
import LeftMenuComponents from 'src/components/LeftMenu/LeftMenuComponents.vue';
import leftMenu from '../layouts/LeftMenuLayout';

export default defineComponent({
  name: 'MainLayout',
  components: {
    RibbonPanel,
    LeftMenuPanel,
    TinyServerMonitor,
    DialogesContainer,
    ProjectNameLabel,
    LeftMenuSources,
    LeftMenuComponents,
  },
  data() {
    return {
      leftMenu: leftMenu.items[0].component,
    };
  },
  setup() {
    let defRibbonPanel = ribbon.panels.find((p) => p.default);
    if (defRibbonPanel == undefined) {
      defRibbonPanel = ribbon.panels[0];
    }
    return {
      tab: ref(defRibbonPanel.name),
      ribbon: ribbon,
      leftMenuMiniState: ref(true),
      leftMenuPanel: ref(false),
      resourceBoxWidth: ref(250),
    };
  },
  mounted() {
    eventBus.on('left-menu-switch', (component: string) => {
      this.leftMenu = component;
    });
  },
  methods: {
    closeWindow() {
      (
        window as unknown as { electron: typeof ElectronApi }
      ).electron.closeWindow();
    },
    minimizeWindow() {
      (
        window as unknown as { electron: typeof ElectronApi }
      ).electron.minimizeWindow();
    },
    maximizeWindow() {
      (
        window as unknown as { electron: typeof ElectronApi }
      ).electron.maximizeWindow();
    },
  },
});
</script>

<style>
@import 'src/css/ribbon.css';

.diagram-content {
  min-height: inherit;
  display: flex;
  flex-direction: column;
}
</style>
