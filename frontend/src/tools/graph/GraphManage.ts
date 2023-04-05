import { CellView, Graph } from '@antv/x6';
import BlockDiagram from '../diagram/BlockDiagram';
import DiagramComponent from '../diagram/DiagramComponent';
import { PinDirection } from '../diagram/DiagramPin';

export function initializeGraph(container: HTMLElement) {
  const graph = new Graph({
    container: container,
    grid: true,
    autoResize: true,
    connecting: {
      snap: true,
      allowBlank: false,
      allowMulti: 'withPort',
      allowLoop: false,
      allowNode: false,
      allowEdge: false,
      allowPort: true,
      highlight: true,
      router: {
        name: 'manhattan',
        args: {
          padding: 20,
        },
      },
      // Set the default style of the edge
      createEdge() {
        return this.createEdge({
          shape: 'edge',
          attrs: {
            line: {
              sourceMarker: {
                // No arrow
                tagName: 'path',
                d: '',
              },
              targetMarker: {
                // No arrow
                tagName: 'path',
                d: '',
              },
            },
          },
        });
      },
    },
    interacting: function (cellView: CellView) {
      return {
        magnetConnectable: true,
      };
    },
  });

  graph.enableMouseWheel();

  return graph;
}

export function newGraph(graph: Graph, blockDiagram: BlockDiagram) {
  // Remove all cells on the graph
  graph.clearCells();

  // Reset the graph zoom
  graph.zoomTo(1);

  // TODO
  console.log('New graph');
}

export function addComponentToGraph(graph: Graph, component: DiagramComponent) {
  console.log('Add node (' + component.hash + ') to graph');

  const label = component.name;

  // Get pins
  const ports = Array<object>();
  const pins = component.pins;
  let maxLeftPinNameLength = 0,
    maxRightPinNameLength = 0;
  let leftPinCount = 0,
    rightPinCount = 0;

  for (const pin of pins) {
    ports.push({
      id: pin.name, // TODO
      group: pin.direction == PinDirection.InputSlave ? 'in' : 'out',
      attrs: {
        text: {
          text: pin.name,
        },
      },
    });

    // Statistics the max pin name length for intelligent size
    if (pin.direction == PinDirection.InputSlave) {
      if (pin.name.length > maxLeftPinNameLength) {
        maxLeftPinNameLength = pin.name.length;
      }
      leftPinCount++;
    } else {
      if (pin.name.length > maxRightPinNameLength) {
        maxRightPinNameLength = pin.name.length;
      }
      rightPinCount++;
    }
  }

  // IntelliSize of the node
  // Get label length
  const labelLength = label.length;
  const labelWidth = 8 * labelLength;

  // Get pin name length
  const leftPinNameWidth = 8 * maxLeftPinNameLength + 10;
  const rightPinNameWidth = 8 * maxRightPinNameLength + 10;
  let width = labelWidth + leftPinNameWidth + rightPinNameWidth + 20;
  if (width < 100) width = 100;

  // Get pin count
  const leftPinHeight = 20 * leftPinCount;
  const rightPinHeight = 20 * rightPinCount;
  let height = Math.max(leftPinHeight, rightPinHeight);
  if (height < 40) height = 40;

  const nodeMeta = {
    shape: 'rect',
    width: width,
    height: height,
    x: 100, // TODO
    y: 100, // TODO
    label: label,
    ports: {
      groups: {
        in: {
          position: 'left',
          label: {
            position: 'right',
          },
          attrs: {
            circle: {
              r: 4,
              magnet: true,
            },
          },
        },
        out: {
          position: 'right',
          label: {
            position: 'left',
          },
          attrs: {
            circle: {
              r: 4,
              magnet: true,
            },
          },
        },
      },
    },
  };

  const node = graph.addNode(nodeMeta);
  node.addPorts(ports);

  // TODO
}
