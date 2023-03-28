const ZoomCard = [
  {
    type: 'big-button',
    title: 'Zoom In',
    icon: 'mdi-magnify-plus-outline',
    tooltip: {
      title: 'Zoom In',
      description: 'Zoom in the diagram canvas.',
    },
  },
  {
    type: 'big-button',
    title: 'Zoom Out',
    icon: 'mdi-magnify-minus-outline',
    tooltip: {
      title: 'Zoom Out',
      description: 'Zoom out the diagram canvas.',
    },
  },
  {
    type: 'big-button',
    title: 'Zoom to Fit',
    icon: 'mdi-fit-to-page-outline',
    tooltip: {
      title: 'Zoom to Fit',
      description: 'Zoom the diagram canvas to fit the diagram.',
    },
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        title: 'Zoom to 100%',
        icon: 'mdi-magnify',
        tooltip: {
          title: 'Zoom to 100%',
          description: 'Zoom the diagram canvas to 100%.',
        },
      },
      {
        type: 'small-button',
        title: 'Fit Select',
        icon: 'mdi-magnify-scan',
        tooltip: {
          title: 'Fit Select',
          description: 'Zoom the diagram canvas to fit the selected nodes.',
        },
      },
      {
        type: 'small-button',
        title: 'Center Select',
        icon: 'mdi-crosshairs-gps',
        tooltip: {
          title: 'Center Select',
          description: 'Center the diagram canvas to the selected nodes.',
        },
      },
    ],
  },
];

export default ZoomCard;
