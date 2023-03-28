<template>
  <div style="min-height: inherit" id="diagram-container">
    <div id="diagram"></div>
  </div>
</template>

<script lang="ts">
import { Graph, Shape, CellView } from '@antv/x6';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'DiagramPage',
  components: {},
  setup() {
    return {};
  },
  data() {
    return {
      graph: null as Graph | null,
      diagram: undefined as HTMLElement | undefined,
      container: undefined as HTMLElement | undefined,
      resizeObserver: null as ResizeObserver | null,
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

    // Get container size and set to graph
    let style = window.getComputedStyle(container, null);
    let width = style.width;
    let height = style.minHeight;

    console.log('width: ' + width, 'height: ' + height);

    // Set the resize observer
    this.resizeObserver = new ResizeObserver(this.onDiagramContainerResize);
    this.resizeObserver.observe(container);

    this.graph = new Graph({
      container: this.diagram,
      width: 150,
      height: 150,
      grid: true,
      connecting: {
        snap: true,
        allowBlank: false,
        allowMulti: 'withPort',
        allowLoop: false,
        allowNode: false,
        allowEdge: false,
        allowPort: true,
        highlight: true,
      },
      interacting: function (cellView: CellView) {
        return {
          magnetConnectable: true,
        };
      },
    });

    // TODO: Test
    let node = new Shape.Rect({
      x: 100,
      y: 100,
      width: 150,
      height: 100,
      angle: 0,
      attrs: {
        label: {
          text: 'Demo',
          fill: 'white',
        },
      },
      ports: {
        groups: {
          in: { position: 'left', label: { position: 'right' } },
          out: { position: 'right', label: { position: 'left' } },
        },
        items: [
          { id: 'clk', group: 'in', attrs: { text: { text: 'clk' } } },
          { id: 'dout', group: 'out', attrs: { text: { text: 'dout' } } },
        ],
      },
    });

    let node2 = new Shape.Rect({
      x: 150,
      y: 150,
      width: 200,
      height: 100,
      angle: 0,
      attrs: {
        label: {
          text: 'Demo',
          fill: 'white',
        },
      },
      ports: {
        groups: {
          in: { position: 'left', label: { position: 'right' } },
          out: { position: 'right', label: { position: 'left' } },
        },
        items: [
          { id: 'clk', group: 'in', attrs: { text: { text: 'clk' } } },
          { id: 'dout', group: 'out', attrs: { text: { text: 'dout' } } },
        ],
      },
    });

    this.graph.addNode(node);
    this.graph.addNode(node2);

    this.graph.addEdge(
      new Shape.Edge()
        .setSource(node, { port: 'dout' })
        .setTarget(node2, { port: 'clk' })
        .setRouter('manhattan')
    );
    this.graph.enableMouseWheel();

    return {
      graph: this.graph,
    };
  },
  beforeUnmount() {
    if (this.resizeObserver != null && this.container != null) {
      this.resizeObserver.unobserve(this.container);
    }
  },
  methods: {
    onDiagramContainerResize() {
      // TODO: Check if this is the right way to do it
      if (this.container == null || this.graph == null) {
        return;
      }

      this.graph.resize(
        this.container.clientWidth,
        this.container.clientHeight
      );
    },
  },
});
</script>

<style>
/* TODO: This is a temporary method to disable scroll bar display */
body {
  overflow: hidden;
}
</style>
