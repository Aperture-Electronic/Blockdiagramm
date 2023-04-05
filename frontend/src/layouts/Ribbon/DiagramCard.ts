const DiagramCard = [
  {
    type: 'big-button',
    name: 'create-diagram',
    title: 'Create',
    icon: 'mdi-plus',
    tooltip: {
      title: 'Create Diagram',
      description: 'Create a new diagram in the current project.',
    },
    menu: [
      {
        type: 'menu-button-item',
        name: 'create-top-diagram',
        title: 'New Top Diagram',
        icon: 'mdi-arrow-up-bold-box-outline',
        tooltip: {
          title: 'Create Top Diagram',
          description: 'Create a new diagram and set it as the top entity.',
        },
        action: 'new-diagram-top',
      },
    ],
    action: 'new-diagram',
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'set-as-top-diagram',
        title: 'Set as Top',
        icon: 'mdi-arrange-bring-to-front',
        tooltip: {
          title: 'Set as Top Diagram',
          description: 'Set the current diagram as the top entity.',
        },
      },
      {
        type: 'small-button',
        name: 'create-sub-diagram',
        title: 'Group',
        icon: 'mdi-group',
        tooltip: {
          title: 'Create Sub Diagram',
          description: 'Group the selected components into a new diagram.',
        },
      },
      {
        type: 'small-button',
        name: 'ungroup-sub-diagram',
        title: 'Ungroup',
        icon: 'mdi-ungroup',
        tooltip: {
          title: 'Ungroup Sub Diagram',
          description: 'Ungroup the selected sub diagram.',
        },
      },
    ],
  },
];

export default DiagramCard;
