const BitOperationCard = [
  {
    type: 'big-button',
    name: 'reverse',
    title: 'Reverse',
    icon: 'mdi-arrow-left-right',
    tooltip: {
      title: 'Reverse',
      description:
        'Reverse the bit order in the wire.' +
        "<br/><a class='text-itatic text-primary'>For example, 4'b0001 will be 4'b1000.</a>",
    },
  },
  {
    type: 'big-button',
    name: 'extend',
    title: 'Extend',
    icon: 'mdi-arrow-expand-horizontal',
    tooltip: {
      title: 'Extend',
      description: 'Extend the wire to the specified bit width.',
    },
    menu: [
      {
        type: 'menu-button-item',
        name: 'signed-extend',
        title: 'Signed Extend',
        icon: 'mdi-plus-minus-variant',
        tooltip: {
          title: 'Signed Extend',
          description:
            'Extend the wire to the specified bit width with sign bit.',
        },
      },
    ],
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'slice',
        title: 'Slice',
        icon: 'mdi-code-brackets',
        tooltip: {
          title: 'Slice',
          description: 'Slice the wire to the specified bit width and offset.',
        },
      },
      {
        type: 'small-button',
        name: 'concatenate',
        title: 'Concat',
        icon: 'mdi-code-braces',
        tooltip: {
          title: 'Concatenate',
          description: 'Concatenate multiple wires into one wire.',
        },
      },
      {
        type: 'small-button',
        name: 'constant',
        title: 'Const',
        icon: 'mdi-hexadecimal',
        tooltip: {
          title: 'Constant',
          description: 'Create a node with constant value.',
        },
      },
    ],
  },
];

export default BitOperationCard;
