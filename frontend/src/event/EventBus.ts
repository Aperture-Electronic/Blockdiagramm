import { EventBus } from 'quasar';
import { InitializeProjectEvents } from './ProjectEvents';

const eventBus = new EventBus();

export default eventBus;

export function InitalizeEventBus() {
  InitializeProjectEvents();
}
