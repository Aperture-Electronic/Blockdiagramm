<template>
  <q-dialog v-model="dialog" persistent>
    <q-card style="width: 300px; height: 300px">
      <q-inner-loading
        :showing="true"
        label="Please Wait for Elaboration..."
        label-class="text-teal"
        label-style="font-size: 1.1em"
    /></q-card>
  </q-dialog>
</template>

<script lang="ts">
import eventBus from 'src/event/EventBus';
import { ref, defineComponent } from 'vue';
import { fetchJsonGetObject } from 'src/tools/Fetch';
import { BackendServerAddress } from 'src/tools/server/BackendServer';

const elaborateAllPath = '/Elaborator/ElaborateAll';

export default defineComponent({
  name: 'ElaborateDialog',
  setup() {
    return {
      dialog: ref(false),
    };
  },
  mounted() {
    eventBus.on('elaborate-all', () => {
      this.dialog = true;
      this.elaborateAll();
    });
  },
  data() {
    return {
      sessionId: (this as unknown as { $CONTEXT_ID: string }).$CONTEXT_ID,
    };
  },
  methods: {
    async elaborateAll() {
      // Wait for the component update
      await this.$nextTick();

      // Send get request to backend server
      let response = await fetchJsonGetObject(
        BackendServerAddress + elaborateAllPath,
        this.sessionId
      );

      if ((response as unknown as { success: boolean }).success == true) {
        console.log('Elaborate all success');
      } else {
        console.log('Elaborate all failed');
      }

      this.dialog = false;

      // Wait for the component update
      await this.$nextTick();
    },
  },
});
</script>
