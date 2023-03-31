<template>
  <div class="column progress-container">
    <q-linear-progress
      class="progress-bar"
      :value="process"
      :buffer="0"
      :color="go ? 'secondary' : 'primary'"
      rounded
      size="25px"
      :indeterminate="!go"
    >
    </q-linear-progress>
    <div class="text-center text-overline">
      {{ go ? 'Backend Server Up' : 'Checking the Backend Server' }}
    </div>
  </div>
</template>

<script lang="ts">
import {
  GetServerStatus,
  ServerStatus,
} from 'src/tools/server/monitor/ServerMonitor';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'LoadingPage',
  components: {},
  data() {
    return {
      process: 0,
      go: false,
    };
  },
  mounted() {
    this.checkServerStatus();
  },
  methods: {
    checkServerStatus() {
      // Check the server status and when it is done, jump to main page
      let serverStatus: Promise<ServerStatus> = GetServerStatus();
      serverStatus
        .then((data) => {
          if (!data.GetServerUnworkable()) {
            this.process = 100;
            this.go = true;

            setTimeout(this.letsGo, 500);
          } else {
            setTimeout(this.checkServerStatus, 1000);
          }
        })
        .catch((error) => {
          console.log(error);
          setTimeout(this.checkServerStatus, 1000);
        });
    },
    letsGo() {
      this.$router.push('/main');
    },
  },
  setup() {
    return {};
  },
});
</script>

<style scoped>
.progress-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

.progress-bar {
  width: 60vh;
}
</style>
