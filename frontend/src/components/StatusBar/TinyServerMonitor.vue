<template>
  <div class="flex">
    <q-badge
      class="server-status-badge"
      rounded
      v-if="!serverStatus.GetServerUnworkable()"
      :color="serverStatus.GetServerStatusColor()"
    >
    </q-badge>
    <q-badge
      :class="{
        'inline-flex': true,
        absolute: !serverStatus.GetServerUnworkable(),
      }"
      rounded
      :color="serverStatus.GetServerStatusColor()"
    ></q-badge>
  </div>
  <div class="text-overline">
    Server(5000)::{{ serverStatus.GetServerStatusString() }}
  </div>
  <div class="text-grey-4">Tick: {{ serverStatus.ServerTick }}</div>
  <div class="text-grey-5 text-uppercase">{{ sessionId }}</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import {
  ServerStatus,
  GetServerStatusAsync,
} from '../../tools/server/monitor/ServerMonitor';

interface Data {
  serverStatusTimeout: ReturnType<typeof setInterval> | null;
  serverStatus: ServerStatus;
  sessionId: string;
}

export default defineComponent({
  name: 'TinyServerMonitor',
  data(): Data {
    let data: Data = {
      serverStatusTimeout: null,
      serverStatus: new ServerStatus(),
      sessionId: (this as unknown as { $CONTEXT_ID: string }).$CONTEXT_ID,
    };

    return data;
  },
  created() {
    this.serverStatusTimeout = setInterval(this.getServerStatus, 1000);
  },
  beforeUnmount() {
    if (this.serverStatusTimeout != null) {
      clearInterval(this.serverStatusTimeout);
    }
  },
  methods: {
    getServerStatus() {
      return GetServerStatusAsync(this.serverStatus);
    },
  },
});
</script>

<style>
.server-status-badge {
  animation: ping 1s cubic-bezier(0, 0, 0.2, 1) infinite;
}

@keyframes ping {
  75%,
  100% {
    transform: scale(2);
    opacity: 0;
  }
}
</style>
