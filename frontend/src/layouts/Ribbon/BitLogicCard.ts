const BitLogicCard = [
  {
    type: 'big-button',
    name: 'add-gate',
    title: 'Gate',
    icon: 'mdi-gate-nand',
    tooltip: {
      title: 'Add Gate',
      description: 'Add a logic gate to the diagram.',
    },
    menu: [
      {
        type: 'menu-button-item',
        name: 'add-reduce-gate',
        title: 'Reduce Gate',
        icon: '',
        tooltip: {
          title: 'Add Reduce Gate',
          description:
            'Add a reduce gate that reduce multiple bits into 1 bit.',
        },
      },
      {
        type: 'menu-separator',
      },
      {
        type: 'menu-button-item',
        name: 'add-and-gate',
        title: 'AND Gate',
        icon: 'mdi-gate-and',
        tooltip: {
          title: 'Add AND Gate',
          description: 'Add an AND gate to the diagram.',
        },
      },
      {
        type: 'menu-button-item',
        name: 'add-not-gate',
        title: 'NOT Gate',
        icon: 'mdi-gate-not',
        tooltip: {
          title: 'Add NOT Gate',
          description: 'Add a NOT gate to the diagram.',
        },
      },
      {
        type: 'menu-separator',
      },
      {
        type: 'menu-button-item',
        name: 'add-reduce-and-gate',
        title: 'Reduce AND Gate',
        icon: 'mdi-gate-and',
        tooltip: {
          title: 'Add Reduce AND Gate',
          description: 'Add a reduce AND gate to the diagram.',
        },
      },
      {
        type: 'menu-button-item',
        name: 'add-reduce-not-gate',
        title: 'Reduce OR Gate',
        icon: 'mdi-gate-or',
        tooltip: {
          title: 'Add Reduce OR Gate',
          description: 'Add a reduce OR gate to the diagram.',
        },
      },
    ],
  },
];

export default BitLogicCard;
