const PortCard = [
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'add-in-port',
        title: 'Input',
        icon: 'mdi-import',
        tooltip: {
          title: 'Add Input Port',
          description: 'Add an input port to the diagram.',
        },
      },
      {
        type: 'small-button',
        name: 'add-out-port',
        title: 'Output',
        icon: 'mdi-export',
        tooltip: {
          title: 'Add Output Port',
          description: 'Add an output port to the diagram.',
        },
      },
      {
        type: 'small-button',
        name: 'add-port',
        title: 'Add Port',
        icon: '', // TODO: mdi-vector-point-plus
        tooltip: {
          title: 'Add Port',
          description: 'Add a port to the diagram.',
        },
      },
    ],
  },
];

export default PortCard;
