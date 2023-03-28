const BusCard = [
  {
    type: 'big-button',
    name: 'define-bus',
    title: 'Define Bus',
    icon: 'mdi-view-day-outline',
    tooltip: {
      title: 'Define Bus',
      description:
        'Define a new bus.' +
        '<br/>A bus is a collection of wires that can be used to connect components together. ' +
        'If you defined a bus, we can reconize the bus on the component and connect them together.',
    },
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'import-bus',
        title: 'Import',
        icon: 'mdi-view-grid-plus',
        tooltip: {
          title: 'Import Bus',
          description: 'Import a bus definition.',
        },
      },
      {
        type: 'small-button',
        name: 'manage-buses',
        title: 'Manage',
        icon: 'mdi-view-grid-outline',
        tooltip: {
          title: 'Manage Buses',
          description: 'Manage the buses.',
        },
      },
    ],
  },
];

export default BusCard;
