const PipelineCard = [
  {
    type: 'big-button',
    name: 'insert-pipeline',
    title: 'Insert<br/>Register',
    icon: 'mdi-view-carousel-outline',
    tooltip: {
      title: 'Insert Register',
      description: 'Insert a register on the wire.',
    },
    menu: [
      {
        type: 'menu-button-item',
        name: 'insert-latency',
        title: 'Insert Latency...',
        icon: 'mdi-view-column-outline',
        tooltip: {
          title: 'Insert Latency',
          description: 'Insert a SFR on the wire to make latency',
        },
      },
    ],
  },
];

export default PipelineCard;
