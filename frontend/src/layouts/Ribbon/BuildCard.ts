const BuildCard = [
  {
    type: 'big-button',
    name: 'build-top-diagram',
    title: 'Build',
    icon: 'mdi-memory',
    tooltip: {
      title: 'Build Top Diagram',
      description:
        'Build the top diagram to a HDL wrapper file of the current project.',
    },
    action: undefined,
  },
  {
    type: 'big-button',
    name: 'build-current-diagram',
    title: 'Build<br/>Current',
    icon: 'mdi-chip',
    tooltip: {
      title: 'Build Current Diagram',
      description: 'Build the current diagram to a HDL file.',
    },
    action: undefined,
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'selective',
        name: 'target-language',
        title: 'Language',
        icon: '',
        selection: ['System Verilog', 'VHDL'],
        tooltip: {
          title: 'Target Language',
          description:
            'Select the target language for the build.' +
            '<br/><a class="text-italic text-accent">' +
            'If your organization or business has no special requirements, we recommend using System Verilog. This language is recognized and properly synthesized by most EDA tools.' +
            '</a>',
        },
        action: undefined,
      },
      {
        type: 'small-button',
        name: 'build-settings',
        title: 'Build Settings',
        icon: 'mdi-cogs',
        tooltip: {
          title: 'Build Settings',
          description:
            'Modify the build settings like the naming convention and the syntax style.',
        },
        action: undefined,
      },
    ],
  },
];

export default BuildCard;
