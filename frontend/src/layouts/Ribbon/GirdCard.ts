const GirdCard = [
  {
    type: 'list-3rows',
    items: [
      {
        type: 'toggle',
        name: 'switch-gird',
        title: 'Gird',
        icon: 'mdi-grid-large',
        tooltip: {
          title: 'Switch Gird',
          description: 'Switch the gird on or off.',
        },
      },
      {
        type: 'selective',
        name: 'gird-size',
        title: 'Gird Size',
        icon: 'mdi-grid',
        tooltip: {
          title: 'Gird Size',
          description: 'Set the gird size.',
        },
        selection: ['1', '2', '5', '10'],
      },
      {
        type: 'small-button',
        title: 'Gird Setting',
        icon: 'mdi-cog-outline',
        tooltip: {
          title: 'Gird Setting',
          description: 'Set the gird.',
        },
      },
    ],
  },
];

export default GirdCard;
