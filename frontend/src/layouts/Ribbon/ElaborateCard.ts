const ElaborateCard = [
  {
    type: 'big-button',
    name: 'elaborate',
    title: 'Elaborate',
    icon: 'mdi-cube-scan',
    tooltip: {
      title: 'Elaborate',
      description:
        'Elaborate current HDL entity. ' +
        'This will get its generics (with default value) and ports, ' +
        'and create a component in the project.',
    },
    action: undefined,
  },
  {
    type: 'big-button',
    name: 'elaborate-update',
    title: 'Update',
    icon: 'mdi-cube-send',
    tooltip: {
      title: 'Update',
      description: 'Elaborate any HDL entity that has changed.',
    },
    action: undefined,
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'elaborate-all',
        title: 'Elaborate All',
        icon: 'mdi-data-matrix-scan',
        tooltip: {
          title: 'Elaborate All',
          description: 'Elaborate all HDL entities in the project.',
        },
        action: undefined,
      },
      {
        type: 'small-button',
        name: 'elaborate-clean',
        title: 'Clean',
        icon: 'mdi-broom',
        tooltip: {
          title: 'Clean',
          description: 'Remove all elaborated components from the project.',
        },
        action: undefined,
      },
      {
        type: 'small-button',
        name: 'search-folder',
        title: 'Search Folder',
        icon: 'mdi-folder-search-outline',
        tooltip: {
          title: 'Search Folder',
          description:
            'Set the search folder for elaborator. The elaborator will search for headers in these folders.',
        },
        action: undefined,
      },
    ],
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'global-macro',
        title: 'Global Macros',
        icon: 'mdi-application-variable-outline',
        tooltip: {
          title: 'Global Macros',
          description:
            'Set global macros for elaborator.' +
            'These macros will be available in all elaborated components.' +
            '<br/>' +
            '<a class="text-italic text-secondary">Note: These macros will overwrite any macros with the same name in the sources.</a>',
        },
        action: undefined,
      },
      {
        type: 'small-button',
        name: 'highlight-errors',
        title: 'Highlight Errors',
        icon: 'mdi-lightbulb-alert-outline',
        tooltip: {
          title: 'Highlight Errors',
          description: 'Highlight all HDL entities can not be elaborated.',
        },
        action: undefined,
      },
    ],
  },
];

export default ElaborateCard;
