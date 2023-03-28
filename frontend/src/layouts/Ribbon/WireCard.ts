const WireCard = [
  {
    type: 'big-button',
    name: 'connect-to',
    title: 'Connect',
    icon: 'mdi-vector-line',
    tooltip: {
      title: 'Connect to',
      description: 'Connect the pin to another pin.',
    },
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'disconnect-pin',
        title: '',
        icon: 'mdi-timeline-remove-outline',
        tooltip: {
          title: 'Disconnect Pin',
          description: 'Disconnect the selected pin.',
        },
      },
      {
        type: 'small-button',
        name: 'remove-wire',
        title: '',
        icon: 'mdi-source-branch-remove',
        tooltip: {
          title: 'Remove Wire',
          description:
            'Remove the selected wire and disconnect all of its pins.',
        },
      },
      {
        type: 'small-button',
        name: 'wire-property',
        title: '',
        icon: 'mdi-timeline-text-outline',
        tooltip: {
          title: 'Wire Property',
          description:
            'Show the property (width, protocol, etc.) of the selected wire.',
        },
      },
    ],
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'find-source',
        title: '',
        icon: 'mdi-relation-many-to-only-one',
        tooltip: {
          title: 'Find Source',
          description: 'Find the source pin of the selected wire.',
        },
      },
      {
        type: 'small-button',
        name: 'find-destination',
        title: '',
        icon: 'mdi-relation-only-one-to-many',
        tooltip: {
          title: 'Find Destination',
          description: 'Find the destination pin of the selected wire.',
        },
      },
      {
        type: 'small-button',
        name: 'find-associated',
        title: '',
        icon: 'mdi-relation-many-to-many',
        tooltip: {
          title: 'Find Associated',
          description: 'Find all the associated pins of the selected wire.',
        },
      },
    ],
  },
];

export default WireCard;
