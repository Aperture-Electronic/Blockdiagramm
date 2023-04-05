<template>
  <NewProjectDialog />
  <AddFileDialog />
  <ElaborateDialog />
  <NewDiagramDialog />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import NewProjectDialog from 'src/components/Dialoges/NewProjectDialog.vue';
import AddFileDialog from 'src/components/Dialoges/AddFileDialog.vue';
import ElaborateDialog from 'src/components/Dialoges/ElaborateDialog.vue';
import NewDiagramDialog from 'src/components/Dialoges/NewDiagramDialog.vue';

export default defineComponent({
  name: 'DialogesContainer',
  components: {
    NewProjectDialog,
    AddFileDialog,
    ElaborateDialog,
    NewDiagramDialog,
  },
  setup() {
    return {};
  },
});
</script>
