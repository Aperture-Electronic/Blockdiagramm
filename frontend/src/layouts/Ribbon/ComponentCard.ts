const ComponentCard = [
  {
    type: 'big-button',
    name: 'add-component',
    title: 'Add',
    icon: 'mdi-package-variant-closed-plus',
    tooltip: {
      title: 'Add Component',
      description: 'Add a new component to the diagram.',
    },
    action: 'm-add-component',
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'update-component',
        title: '',
        icon: 'mdi-book-refresh-outline',
        tooltip: {
          title: 'Update Component',
          description: 'Update the selected component.',
        },
        menu: [
          {
            type: 'menu-button-item',
            name: 'update-all-component',
            title: 'Update All',
            icon: '',
            tooltip: {
              title: 'Update All Component',
              description: 'Update all components in the diagram.',
            },
          },
        ],
      },
      {
        type: 'small-button',
        name: 'custom-parameter',
        title: '',
        icon: 'mdi-pencil-box-outline',
        tooltip: {
          title: 'Custom Parameter',
          description: 'Customize the parameter of the selected component.',
        },
      },
      {
        type: 'small-button',
        name: 'rename-instance',
        title: '',
        icon: 'mdi-form-textbox',
        tooltip: {
          title: 'Rename Instance',
          description: 'Rename the instance of the selected component.',
        },
      },
    ],
  },
];

export default ComponentCard;
