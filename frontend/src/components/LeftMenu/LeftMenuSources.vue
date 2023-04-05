<template>
  <div
    class="q-gutter-sm"
    style="flex: 1; display: flex; flex-direction: column"
  >
    <q-bar dense class="bg-blue-8 text-white" style="height: 2em">
      <q-icon name="mdi-file-cabinet" size="1.25em" />
      <div>Sources</div>
      <q-space />
      <q-btn
        dense
        flat
        icon="mdi-refresh"
        @click="buttonRefreshSourceList"
      ></q-btn>
    </q-bar>
    <div class="q-pl-sm">
      <q-scroll-area class="left-menu-list">
        <div>
          <q-list separator>
            <SourceItem
              v-for="file in sourceFileList"
              :key="file.Hash"
              :file="file"
              :selected="sourceFileItemSelect.get(file.Hash)"
              @click="selectItem(file.Hash)"
            />
          </q-list>
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
import eventBus from 'src/event/EventBus';
import { fetchJsonGetObject } from 'src/tools/Fetch';
import { BackendServerAddress } from 'src/tools/server/BackendServer';
import SourceFile from 'src/tools/server/sources/SourceFile';
import { defineComponent } from 'vue';
import SourceItem from './SourceItem.vue';

const getSourcesListPath = '/Source/GetSourcesList';

export default defineComponent({
  name: 'LeftMenuSources',
  components: {
    SourceItem,
  },
  data() {
    return {
      loadingVisible: false,
      sourceFileList: new Array<SourceFile>(),
      sourceFileItemSelect: new Map<string, boolean>(),
      sessionId: (this as unknown as { $CONTEXT_ID: string }).$CONTEXT_ID,
    };
  },
  mounted() {
    eventBus.on('refresh-source-list', this.refreshSourceList);
  },
  setup() {
    return {};
  },
  methods: {
    buttonRefreshSourceList() {
      eventBus.emit('refresh-source-list');
    },
    async refreshSourceList() {
      console.log('Refresh source list');
      // Clear the list and show loading
      this.sourceFileList = new Array<SourceFile>();
      this.sourceFileItemSelect = new Map<string, boolean>();
      this.loadingVisible = true;

      // Wait for the page renderer
      await this.$nextTick();

      // Get the list of source files by calling the backend
      let response = await fetchJsonGetObject(
        BackendServerAddress + getSourcesListPath,
        this.sessionId
      );

      if (response as unknown as { sources: [] }) {
        let sourcesList = response.sources;

        // For each source file, create a local instance for it and add it to the list
        for (let i = 0; i < sourcesList.length; i++) {
          let source = sourcesList[i];
          let fileObject = new SourceFile(
            source.filePath,
            source.shortName,
            source.hash,
            source.exist,
            source.type
          );
          this.sourceFileList.push(fileObject);
          this.sourceFileItemSelect.set(fileObject.Hash, false);
        }

        // Sort the sources by the short name
        this.sourceFileList.sort((a, b) => {
          if (a.ShortName < b.ShortName) {
            return -1;
          }
          if (a.ShortName > b.ShortName) {
            return 1;
          }
          return 0;
        });
      } else {
        console.log('Error: response is not a list of sources');
      }

      // Wait for the page renderer
      await this.$nextTick();

      // Hide the loading
      this.loadingVisible = false;
    },
    selectItem(hash: string) {
      console.log('Select source file item: ' + hash);
      this.sourceFileItemSelect.forEach((_, key) => {
        this.sourceFileItemSelect.set(key, key == hash);
      });
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
