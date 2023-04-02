<template>
  <div :style="lblStyle" :class="lblClass">{{ projectName }}</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import eventBus from 'src/event/EventBus';
import { fetchGetObject } from 'src/tools/Fetch';
import { BackendServerAddress } from 'src/tools/server/BackendServer';

const getProjectNamePath = '/Project/GetProjectName';

export default defineComponent({
  name: 'ProjectTitle',
  props: ['lblStyle', 'lblClass'],
  data() {
    return {
      projectName: '',
      sessionId: (this as unknown as { $CONTEXT_ID: string }).$CONTEXT_ID,
    };
  },
  created() {
    eventBus.on('get-project-name', () => this.getProjectName());
  },
  methods: {
    async getProjectName() {
      let response = await fetchGetObject(
        BackendServerAddress + getProjectNamePath,
        this.sessionId
      );

      let name = await response.text();
      if (!!name) {
        this.projectName = name;
      }
    },
  },
});
</script>
