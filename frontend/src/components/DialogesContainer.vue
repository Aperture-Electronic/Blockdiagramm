<template>
  <NewProjectDialog />
  <AddFileDialog />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import NewProjectDialog from 'src/components/Dialoges/NewProjectDialog.vue';
import AddFileDialog from 'src/components/Dialoges/AddFileDialog.vue';

export default defineComponent({
  name: 'DialogesContainer',
  components: {
    NewProjectDialog,
    AddFileDialog,
  },
  setup() {
    return {};
  },
});
</script>
