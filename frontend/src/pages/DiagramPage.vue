<template>
  <div
    class="flex"
    style="min-height: inherit; flex-direction: column"
    id="diagram-container"
  >
    <div style="flex: 1" id="diagram"></div>
  </div>
</template>

<script lang="ts">
import { Graph } from '@antv/x6';
import eventBus from 'src/event/EventBus';
import BlockDiagram from 'src/tools/diagram/BlockDiagram';
import DiagramComponent from 'src/tools/diagram/DiagramComponent';
import DiagramPin, {
  PinDirection,
  PinType,
} from 'src/tools/diagram/DiagramPin';
import { fetchJsonPostObject } from 'src/tools/Fetch';
import {
  addComponentToGraph,
  initializeGraph,
  newGraph,
} from 'src/tools/graph/GraphManage';
import { BackendServerAddress } from 'src/tools/server/BackendServer';
import ComponentMeta from 'src/tools/server/components/ComponentMeta';
import { ref, defineComponent, Ref } from 'vue';

const getComponentPath = '/Elaborator/GetComponent';

export default defineComponent({
  name: 'DiagramPage',
  components: {},
  setup() {
    return {};
  },
  data() {
    let graph: Ref<Graph | null> = ref(null);

    return {
      graph,
      diagram: undefined as HTMLElement | undefined,
      container: undefined as HTMLElement | undefined,
      resizeObserver: null as ResizeObserver | null,
      sessionId: (this as unknown as { $CONTEXT_ID: string }).$CONTEXT_ID,
    };
  },
  mounted() {
    let container = document.getElementById('diagram-container');
    let diagram = document.getElementById('diagram');
    if (container == null || diagram == null) {
      throw new Error('Diagram or its container not found');
    }

    this.container = container;
    this.diagram = diagram;
    this.graph = initializeGraph(diagram);

    // eventBus.off('create-new-diagram');
    eventBus.on('create-new-diagram', (blockDiagram: BlockDiagram) => {
      console.log('Create new diagram: Name = ', blockDiagram.name);
      if (this.graph != null) {
        newGraph(this.graph, blockDiagram);
      }
    });

    eventBus.on('add-component', async (c: unknown) => {
      let simplifiedComp = c as unknown as {
        hash: string;
        name: string;
      };

      console.log('Add component: ', simplifiedComp.name);

      // We need get the detail information of the component
      // by its hash, and then create a new DiagramComponent
      // and add it to the graph

      // Get the component detail information from the server
      let response = await fetchJsonPostObject(
        BackendServerAddress + getComponentPath,
        {
          sessionId: this.sessionId,
          hash: simplifiedComp.hash,
        }
      );

      if (!(response as unknown as { success: boolean })) {
        return;
      }

      if (!(response as unknown as { success: boolean }).success) {
        return;
      }

      let component = (
        response as unknown as {
          component: {
            hash: string;
            name: string;
            interfaces: [];
          };
        }
      ).component;

      let diagramComponent = new DiagramComponent(
        component.hash,
        component.name
      );

      for (var compInterface of component.interfaces) {
        if (compInterface as unknown as ComponentMeta) {
          let meta = compInterface as unknown as ComponentMeta;

          let direction =
            meta.bus.direction == 0
              ? PinDirection.OutputMaster
              : PinDirection.InputSlave;
          let pinName = '';
          let pinType = PinType.Single;
          if (meta.portCount == 1) {
            pinName = meta.modulePort[0].name;
          }

          diagramComponent.addPin(new DiagramPin(direction, pinType, pinName));
        }
      }

      if (this.graph != null) {
        addComponentToGraph(this.graph, diagramComponent);
      }
    });
  },
  methods: {},
});
</script>

<style>
.diagram-container {
  overflow: hidden;
}
</style>
