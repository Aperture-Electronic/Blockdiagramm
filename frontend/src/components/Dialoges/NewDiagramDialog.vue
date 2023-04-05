<template>
  <q-dialog v-model="dialog" persistent>
    <q-card style="width: 500px">
      <q-card-section class="row items-center q-pb-md text-white bg-green-9">
        <q-icon name="mdi-file-import-outline" class="q-pr-sm" size="1.75em" />
        <div class="text-h6">
          New Diagram
          <template v-if="isTopDiagram">(Top)</template>
        </div>
        <q-space />
        <q-btn icon="close" flat round dense v-close-popup />
      </q-card-section>
      <q-card-section class="q-pt-none">
        <q-input
          label="Diagram Name"
          bottom-slots
          ref="inputDiagramName"
          clearable
          autofocus
          v-model="diagramName"
          :rules="inputDiagramNameRules"
        >
          <template v-slot:before>
            <q-icon name="mdi-file-document-multiple-outline" />
          </template>
        </q-input>
      </q-card-section>
      <q-card-actions align="right">
        <q-btn flat label="Cancel" color="primary" v-close-popup />
        <q-btn
          flat
          label="Create"
          color="primary"
          @click="buttonCreateDiagram"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import eventBus from 'src/event/EventBus';
import BlockDiagram from 'src/tools/diagram/BlockDiagram';
import { ref, defineComponent } from 'vue';

export default defineComponent({
  name: 'NewDiagramDialog',
  components: {},
  setup() {
    let dialog = ref(false);
    let isTopDiagram = ref(false);

    eventBus.on('new-diagram', () => {
      console.log('New diagram');
      dialog.value = true;
      isTopDiagram.value = false;
    });

    eventBus.on('new-diagram-top', () => {
      console.log('New diagram (top)');
      dialog.value = true;
      isTopDiagram.value = true;
    });

    return {
      dialog,
      isTopDiagram,
      diagramName: ref(''),
    };
  },
  data() {
    return {
      inputDiagramNameRules: [
        (v: string) => !!v || 'Diagram name is required',
        (v: string) =>
          v.length <= 50 || 'Diagram name must be less than 50 characters',
      ],
    };
  },
  methods: {
    buttonCreateDiagram() {
      console.log('Create diagram');

      // TODO: Create the diagram and let the server create the file
      let success = true;
      if (this.diagramName === '') {
        success = false;
      }

      let blockDiagram = new BlockDiagram(
        '',
        this.diagramName,
        this.isTopDiagram
      );

      if (success) {
        eventBus.emit('create-new-diagram', blockDiagram);

        // Close the dialog
        this.dialog = false;
      }
    },
  },
});
</script>
