<template>
  <q-dialog v-model="dialog" persistent>
    <q-card style="width: 500px">
      <q-card-section class="row items-center q-pb-md text-white bg-green-9">
        <q-icon name="mdi-file-import-outline" class="q-pr-sm" size="1.75em" />
        <div class="text-h6">Add Source</div>
        <q-space />
        <q-btn icon="close" flat round dense v-close-popup />
      </q-card-section>
      <DialogErrorBanner ref="errorBanner" />
      <q-card-section class="q-pt-none">
        <q-input
          label="Source Path"
          bottom-slots
          ref="inputSourcePath"
          clearable
          autofocus
          v-model="sourcePath"
          :rules="inputSourcePathRules"
        >
          <template v-slot:before>
            <q-icon name="mdi-file-document-multiple-outline" />
          </template>
          <template v-slot:after>
            <q-btn
              icon="mdi-dots-horizontal"
              round
              dense
              flat
              @click="openFileDialog"
            />
          </template>
        </q-input>
        <q-select
          label="Source Type"
          class="flex col"
          v-model="sourceType"
          :options="sourceTypeSelection"
          hide-bottom-space
        >
          <template v-slot:before>
            <q-icon name="mdi-code-brackets" />
          </template>
        </q-select>
      </q-card-section>
      <q-card-actions align="right">
        <q-btn flat label="Cancel" color="primary" v-close-popup />
        <q-btn flat label="Add" color="primary" @click="buttonAddSource" />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import eventBus from 'src/event/EventBus';
import { ref, defineComponent } from 'vue';
import ElectronApi from 'src-electron/api/electron-api';
import { fetchJSONPostObject } from 'src/tools/Fetch';
import { BackendServerAddress } from 'src/tools/server/BackendServer';
import DialogErrorBanner from './DialogErrorBanner.vue';
import { SourceType } from 'src/tools/server/sources/SourceFileType';

const addSourcePath = '/Source/AddSource';

function getFileTypeByExtension(fileName: string) {
  let fileExtension = fileName.split('.').pop();
  if (fileExtension != undefined) {
    fileExtension = fileExtension.toLowerCase();
    if (fileExtension == 'sv') {
      return 1;
    } else if (fileExtension == 'svh') {
      return 2;
    } else if (fileExtension == 'v') {
      return 3;
    } else if (fileExtension == 'vhd' || fileExtension == 'vhdl') {
      return 4;
    } else {
      return 0;
    }
  }
  return 0;
}

export default defineComponent({
  name: 'AddFileDialog',
  components: {
    DialogErrorBanner,
  },
  setup() {
    let dialog = ref(false);
    eventBus.on('add-source-file', () => {
      console.log('Add Source File!');
      dialog.value = true;
    });

    const sourceTypeSelection = [
      { label: 'Auto', value: SourceType.Auto },
      { label: 'System Verilog', value: SourceType.SystemVerilog },
      { label: 'System Verilog Header', value: SourceType.SystemVerilogHeader },
      { label: 'Verilog', value: SourceType.Verilog },
      { label: 'VHDL', value: SourceType.VHDL },
    ];

    return {
      dialog,
      sourcePath: ref(''),
      sourceTypeSelection,
      sourceType: ref(sourceTypeSelection[0]),
      allowAddSource: ref(false),
      errorMessage: ref(''),
    };
  },
  data() {
    return {
      inputSourcePathRules: [
        // Length > 0
        (value: string) => !!value || 'Required',
      ],
      sessionId: (this as unknown as { $CONTEXT_ID: string }).$CONTEXT_ID,
    };
  },
  methods: {
    async openFileDialog() {
      let dialogSelected = await (
        window as unknown as { electron: typeof ElectronApi }
      ).electron.openMultiSourceFileDialog();

      if (dialogSelected != undefined) {
        if (dialogSelected.length == 1) {
          // When select only 1 file, set the file type by the file extension
          let sourceType = getFileTypeByExtension(dialogSelected[0]);
          this.sourceType = this.sourceTypeSelection[sourceType];

          // Set the file path
          this.sourcePath = dialogSelected[0];
        } else if (dialogSelected.length > 1) {
          // When select multiple files, set the file type to Auto
          // And combine the file path with a ';' separator
          this.sourceType = this.sourceTypeSelection[0];
          this.sourcePath = dialogSelected.join(';');
        }
      }
    },
    async buttonAddSource() {
      console.log('Add Source: ' + this.sourcePath);

      // Get the file list (split by ';')
      let fileList = this.sourcePath.split(';');

      // For each file, send a request to the backend
      let fileType = this.sourceType.value;

      // Set a promise array to wait for all the requests
      let promiseArray = new Array<Promise<Response>>();

      for (let i = 0; i < fileList.length; i++) {
        let file = fileList[i];
        // Check if the file is empty string
        if (file == '') {
          continue;
        }

        let response = fetchJSONPostObject(
          BackendServerAddress + addSourcePath,
          {
            sessionId: this.sessionId,
            path: file,
            type: fileType,
          }
        );
        promiseArray.push(response);
      }

      // Wait for all the requests with async wait
      let responses = await Promise.all(promiseArray);

      // Check the responses
      let anySuccess = false;
      for (let i = 0; i < responses.length; i++) {
        let response = responses[i];
        if ((response as unknown as { success: boolean }).success == true) {
          console.log('Add Source Success');
          anySuccess = true;
        } else {
          let reason = (response as unknown as { errorReason: string })
            .errorReason;
          console.log('Add Source Failed! Reason: ' + reason);
        }
      }

      // Set the error message to empty
      // then the error banner will be able to show again
      this.errorMessage = '';
      if (anySuccess) {
        // Close the dialog
        this.dialog = false;
      } else if (responses.length == 0) {
        (this.$refs.errorBanner as typeof DialogErrorBanner).showErrorMessage(
          'Please add at least one source file!'
        );
      } else {
        (this.$refs.errorBanner as typeof DialogErrorBanner).showErrorMessage(
          'All of source files are already added!'
        );
      }
    },
  },
});
</script>
