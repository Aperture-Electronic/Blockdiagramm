<template>
  <q-dialog v-model="dialog" persistent>
    <q-card style="width: 500px">
      <q-card-section
        class="row items-center q-pb-md text-white bg-deep-purple-8"
      >
        <q-icon
          name="mdi-database-plus-outline"
          class="q-pr-sm"
          size="1.75em"
        />
        <div class="text-h6">New Project</div>
        <q-space />
        <q-btn icon="close" flat round dense v-close-popup />
      </q-card-section>
      <q-card-section class="q-pt-none">
        <q-input
          label="Project Name"
          bottom-slots
          counter
          ref="inputProjectName"
          maxlength="64"
          clearable
          v-model="projectName"
          autofocus
          @keyup.enter="focusToPath"
          :rules="inputProjectNameRules"
        >
          <template v-slot:hint>
            At least one character, without whitespace
          </template>
          <template v-slot:before>
            <q-icon name="mdi-database-outline" />
          </template>
        </q-input>
        <q-input
          label="Project Path"
          bottom-slots
          ref="inputProjectPath"
          clearable
          v-model="projectPath"
          :rules="inputProjectPathRules"
        >
          <template v-slot:hint> Path not existed will be created </template>
          <template v-slot:before>
            <q-icon name="mdi-folder-outline" />
          </template>
          <template v-slot:after>
            <q-btn
              icon="mdi-dots-horizontal"
              round
              dense
              flat
              @click="openFolderDialog"
            />
          </template>
        </q-input>
      </q-card-section>
      <q-card-actions align="right">
        <q-btn flat label="Cancel" color="primary" v-close-popup />
        <q-btn
          flat
          label="Create"
          color="primary"
          @click="buttonCreateProject"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { QInput } from 'quasar';
import eventBus from 'src/event/EventBus';
import { ref, defineComponent } from 'vue';
import ElectronApi from 'src-electron/api/electron-api';
import { fetchJSONPostObject } from 'src/tools/Fetch';
import { BackendServerAddress } from 'src/tools/server/BackendServer';

const newProjectPath = '/Project/NewProject';

export default defineComponent({
  name: 'NewProjectDialog',
  setup() {
    let dialog = ref(false);
    eventBus.on('new-project', () => {
      dialog.value = true;
    });

    return {
      dialog,
      projectName: ref(''),
      projectPath: ref(''),
      allowCreateProject: ref(false),
    };
  },
  data() {
    return {
      inputProjectNameRules: [
        // Length > 0 and no whitespace
        (value: string) => !!value || 'Required',
        (value: string) =>
          !/\s/.test(value) || 'Project name must be no whitespace',
      ],
      inputProjectPathRules: [
        // Length > 0
        (value: string) => !!value || 'Required',
      ],
      sessionId: (this as unknown as { $CONTEXT_ID: string }).$CONTEXT_ID,
    };
  },
  methods: {
    focusToPath() {
      (this.$refs.inputProjectPath as QInput).focus();
    },
    async openFolderDialog() {
      let dialogSelected = await (
        window as unknown as { electron: typeof ElectronApi }
      ).electron.openFolderDialog();

      if (dialogSelected != undefined && dialogSelected.length > 0) {
        this.projectPath = dialogSelected[0];
      } else {
        this.projectPath = '';
      }
    },
    async buttonCreateProject() {
      console.log(
        'Create Project at ' +
          this.projectPath +
          ' with name ' +
          this.projectName
      );

      // Make the request
      let request = {
        name: this.projectName,
        path: this.projectPath,
        sessionId: this.sessionId,
      };

      let response = await fetchJSONPostObject(
        BackendServerAddress + newProjectPath,
        request
      );

      if ((response as unknown as { success: boolean }).success == true) {
        console.log('Create Project Success!');
        // Close the dialog
        this.dialog = false;
      } else {
        let reason = (response as unknown as { errorReason: string })
          .errorReason;
        console.log('Create Project Failed! Reason: ' + reason);
      }

      eventBus.emit('get-project-name');
    },
  },
});
</script>
