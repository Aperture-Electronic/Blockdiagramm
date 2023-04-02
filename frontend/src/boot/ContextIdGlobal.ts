import { boot } from 'quasar/wrappers';
import ElectronApi from 'src-electron/api/electron-api';

async function getSessionID() {
  return await (
    window as unknown as { electron: typeof ElectronApi }
  ).electron.getSessionID();
}

export default boot(async ({ app }) => {
  // We neet to get and set context id to global object
  app.config.globalProperties.$CONTEXT_ID = await getSessionID();
});
