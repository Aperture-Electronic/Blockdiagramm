const SourceCard = [
  {
    type: 'big-button',
    name: 'add-file',
    title: 'Add<br/>File',
    icon: 'mdi-file-import-outline',
    tooltip: {
      title: 'Add File',
      description: 'Add a HDL source file to the project.',
    },
    action: undefined,
  },
  {
    type: 'big-button',
    name: 'add-directory',
    title: 'Add<br/>Directory',
    icon: 'mdi-folder-plus-outline',
    tooltip: {
      title: 'Add Directory',
      description: 'Add a directory of HDL source files to the project.',
    },
    action: undefined,
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'replace-source',
        title: '',
        icon: 'mdi-file-replace-outline',
        tooltip: {
          title: 'Replace Source',
          description: 'Replace the source file of a HDL entity.',
        },
        action: undefined,
      },
      {
        type: 'small-button',
        name: 'remove-invalid-source',
        title: '',
        icon: 'mdi-file-alert-outline',
        tooltip: {
          title: 'Remove Invalid Source',
          description:
            'Remove the source file of a HDL entity that is no longer valid.',
        },
        action: undefined,
      },
      {
        type: 'small-button',
        name: 'remove-source',
        title: '',
        icon: 'mdi-file-remove-outline',
        tooltip: {
          title: 'Remove Source',
          description: 'Remove the source file of a HDL entity.',
        },
        action: undefined,
      },
    ],
  },
];

export default SourceCard;
