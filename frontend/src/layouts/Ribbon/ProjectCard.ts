const ProjectCard = [
  {
    type: 'big-button',
    name: 'save-project',
    title: 'Save',
    icon: 'mdi-database-check-outline',
    menu: [
      {
        type: 'menu-button-item',
        name: 'save-as-project',
        title: 'Save As...',
        icon: 'mdi-database-export-outline',
        tooltip: {
          title: 'Save As',
          description: 'Save the current project with a new name.',
        },
      },
    ],
    tooltip: {
      title: 'Save Project',
      description: 'Save the current project.',
    },
    action: undefined,
  },
  {
    type: 'list-3rows',
    items: [
      {
        type: 'small-button',
        name: 'new-project',
        title: 'New',
        icon: 'mdi-database-plus-outline',
        tooltip: {
          title: 'New Project',
          description: 'Create a new project.',
        },
        action: undefined,
      },
      {
        type: 'small-button',
        name: 'open-project',
        title: 'Open',
        icon: 'mdi-database-edit-outline',
        tooltip: {
          title: 'Open Project',
          description: 'Open an existing project.',
        },
        action: undefined,
      },
      {
        type: 'small-button',
        name: 'close-project',
        title: 'Close',
        icon: 'mdi-database-remove-outline',
        tooltip: {
          title: 'Close Project',
          description: 'Close the current project.',
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
        name: 'project-settings',
        title: '',
        tooltip: {
          title: 'Project Settings',
          description: 'Edit the project settings.',
        },
        icon: 'mdi-database-cog-outline',
      },
    ],
  },
];

export default ProjectCard;
