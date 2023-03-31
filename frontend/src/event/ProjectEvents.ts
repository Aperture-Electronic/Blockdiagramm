import eventBus from 'src/event/EventBus';

export function InitializeProjectEvents() {
  eventBus.on('new-project', () => {
    console.log('New Project!');
  });

  console.log('Project events initialized.');
}
