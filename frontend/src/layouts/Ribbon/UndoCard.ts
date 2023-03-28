const UndoCard = [
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'undo',
        title: '',
        icon: 'mdi-undo',
        tooltip: {
          title: 'Undo',
          description: 'Undo the last action.',
        },
      },
      {
        type: 'small-button',
        name: 'redo',
        title: '',
        icon: 'mdi-redo',
        tooltip: {
          title: 'Redo',
          description: 'Redo the last action.',
        },
      },
    ],
  },
];

export default UndoCard;
