const EditCard = [
  {
    type: 'big-button',
    name: 'paste',
    title: 'Paste',
    icon: 'mdi-content-paste',
    tooltip: {
      title: 'Paste',
      description: 'Paste the clipboard content.',
    },
    menu: [
      {
        type: 'menu-button-item',
        name: 'paste-with-default-parameter',
        title: 'Paste with Default Parameter',
        icon: 'mdi-clipboard-file-outline',
        tooltip: {
          title: 'Paste with Default Parameter',
          description:
            'Paste the component in the clipboard with default parameter.',
        },
      },
    ],
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'copy',
        title: '',
        icon: 'mdi-content-copy',
        tooltip: {
          title: 'Copy',
          description: 'Copy the selected content.',
        },
      },
      {
        type: 'small-button',
        name: 'cut',
        title: '',
        icon: 'mdi-content-cut',
        tooltip: {
          title: 'Cut',
          description: 'Cut the selected content.',
        },
      },
      {
        type: 'small-button',
        name: 'delete',
        title: '',
        icon: 'mdi-delete',
        tooltip: {
          title: 'Delete',
          description: 'Delete the selected content.',
        },
      },
    ],
  },
];

export default EditCard;
