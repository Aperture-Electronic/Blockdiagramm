<template>
  <q-banner
    v-if="errorMessageVisible"
    inline-actions
    class="text-white bg-orange-9"
  >
    {{ errorMessage }}
    <template v-slot:action>
      <q-btn flat color="white" label="Dismiss" @click="dismissErrorMessage" />
    </template>
  </q-banner>
</template>

<script lang="ts">
import { ref, defineComponent } from 'vue';

export default defineComponent({
  name: 'DialogErrorBanner',
  data() {
    return {
      errorMessage: '',
    };
  },
  setup() {
    return {
      errorMessageVisible: ref(false),
    };
  },
  methods: {
    dismissErrorMessage() {
      this.errorMessageVisible = false;
    },
    showErrorMessage(message: string) {
      this.errorMessage = message;
      this.errorMessageVisible = true;
    },
  },
});
</script>
