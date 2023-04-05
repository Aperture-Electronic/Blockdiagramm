<template>
  <div style="flex: 1; display: flex; flex-direction: column">
    <q-bar dense class="bg-green-8 text-white" style="height: 2em">
      <q-icon name="mdi-cube-outline" size="1.25em" />
      <div>Components</div>
      <q-space />
      <q-btn
        dense
        flat
        icon="mdi-refresh"
        @click="buttonRefreshComponentsList"
      ></q-btn>
    </q-bar>
    <q-input
      dense
      ref="filterRef"
      class="left-menu-filter"
      filled
      v-model="filter"
      label="Filter"
    >
      <template v-slot:append>
        <q-icon
          v-if="filter !== ''"
          name="clear"
          class="cursor-pointer"
          @click="resetFilter"
        />
      </template>
    </q-input>
    <div class="q-pl-sm">
      <q-scroll-area class="left-menu-list with-filter">
        <div>
          <q-tree
            :nodes="componentsList"
            node-key="hash"
            :filter="filter"
            default-expand-all
            v-model:selected="selectedComponent"
            selected-color="primary"
            ref="componentsTree"
          />
        </div>
        <q-inner-loading
          :showing="loadingVisible"
          label="Please wait..."
          label-class="text-teal"
          label-style="font-size: 1.1em"
        />
      </q-scroll-area>
    </div>
  </div>
</template>

<script lang="ts">
import { QInput, QTree } from 'quasar';
import eventBus from 'src/event/EventBus';
import { fetchJsonGetObject } from 'src/tools/Fetch';
import { BackendServerAddress } from 'src/tools/server/BackendServer';
import { ref, defineComponent } from 'vue';

const getComponentsListPath = '/Elaborator/GetComponentsList';
const getSourcesListPath = '/Source/GetSourcesList';

export default defineComponent({
  name: 'LeftMenuSources',
  data() {
    return {
      loadingVisible: false,
      componentsList: [
        {
          label: 'Project',
          children: [],
          selectable: false,
        },
      ],
      selectedComponent: ref(null),
      sources: new Map<string, unknown>(),
      sessionId: (this as unknown as { $CONTEXT_ID: string }).$CONTEXT_ID,
    };
  },
  setup() {
    const filter = ref('');

    return {
      filter,
    };
  },
  mounted() {
    eventBus.on('refresh-components-list', this.refreshComponentsList);
    eventBus.on('m-add-component', () =>
      this.addComponent(this.selectedComponent)
    );
  },
  methods: {
    buttonRefreshComponentsList() {
      eventBus.emit('refresh-components-list');
    },
    async getFileList() {
      // We need to send a request for query the file names
      let response = await fetchJsonGetObject(
        BackendServerAddress + getSourcesListPath,
        this.sessionId
      );

      this.sources.clear();
      if (response as unknown as { sources: [] }) {
        let sourcesList = response.sources;
        for (var source of sourcesList) {
          let sourceHash = (source as unknown as { hash: string }).hash;
          this.sources.set(sourceHash, source);
        }
      }
    },
    async refreshComponentsList() {
      console.log('Refresh components list');

      // Clear the list and show loading
      this.componentsList = [
        {
          label: 'Project',
          children: [],
          selectable: false,
        },
      ];
      this.loadingVisible = true;

      // Wait for the page renderer
      await this.$nextTick();

      // Get the source list
      await this.getFileList();

      // Get the list of components by calling the backend
      let response = await fetchJsonGetObject(
        BackendServerAddress + getComponentsListPath,
        this.sessionId
      );

      if (response as unknown as { components: [] }) {
        let fileGroup = response.components;
        for (var file in fileGroup) {
          let fileHash = file;
          let fileComponents: Array<unknown> = fileGroup[file];

          // If the file contains only one component,
          // we don't need to create a node for the file
          // and we can add the component directly to the project node
          if (fileComponents.length == 1) {
            let component = fileComponents[0];
            let componentName = (component as unknown as { name: string }).name;
            let componentHash = (component as unknown as { hash: string }).hash;
            let fileName = (this.sources.get(fileHash) as { shortName: string })
              .shortName;

            let componentNode = {
              label: componentName,
              component: component,
              selectable: true,
              hash: componentHash,
              fileHash: fileHash,
              fikeName: fileName,
            };

            this.componentsList[0].children.push(componentNode as never);
            continue;
          }

          let fileNode = {
            label: (this.sources.get(fileHash) as { shortName: string })
              .shortName,
            hash: fileHash,
            children: Array<unknown>(),
            selectable: false,
          };

          // Create the component nodes
          for (var component of fileComponents) {
            let componentName = (component as unknown as { name: string }).name;
            let componentHash = (component as unknown as { hash: string }).hash;

            let componentNode = {
              label: componentName,
              component: component,
              selectable: true,
              hash: componentHash,
            };

            fileNode.children.push(componentNode);
          }

          // Add the file node to the tree
          this.componentsList[0].children.push(fileNode as never);
        }
      } else {
        console.log('Error: response is not a list of sources');
      }

      // Wait for the page renderer
      await this.$nextTick();

      // Hide the loading
      this.loadingVisible = false;
    },
    resetFilter() {
      this.filter = '';
      (this.$refs.filterRef as typeof QInput).focus();
    },
    addComponent(selectedComponent: unknown) {
      if (selectedComponent != null) {
        let node = (this.$refs.componentsTree as QTree).getNodeByKey(
          selectedComponent
        );

        let component = (node as unknown as { component: unknown }).component;

        eventBus.emit('add-component', component);
      }
    },
  },
});
</script>

<style scoped>
@import 'src/css/leftMenu.css';

:deep(.q-scrollarea__content) {
  width: inherit;
}
</style>
