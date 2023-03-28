const GenerateCard = [
  {
    type: 'big-button',
    name: 'insert-loop',
    title: 'Insert<br/>Loop',
    icon: 'mdi-checkbox-multiple-blank-outline',
    tooltip: {
      title: 'Insert Generate Loop',
      description:
        'Insert a generate loop.' +
        '<br/>The generate loop can be used to generate multiple instances of a module.',
    },
  },
  {
    type: 'big-button',
    name: 'insert-conditional',
    title: 'Insert<br/>Conditional',
    icon: 'mdi-checkbox-multiple-marked-outline',
    tooltip: {
      title: 'Insert Conditional Generate',
      description:
        'Insert a conditional generate.' +
        '<br/>The conditional generate can be used to generate a module based on a condition.',
    },
  },
];

export default GenerateCard;
