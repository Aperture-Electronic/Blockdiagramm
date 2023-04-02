import {
  openFolderDialog,
  openMultiSourceFileDialog,
} from './electron-api-dialogs';
import { getSessionId } from './electron-api-session';
import {
  closeWindow,
  maximizeWindow,
  minimizeWindow,
} from './electron-api-window';
import { ipcMain } from 'electron';

export function initializeElectronApi() {
  ipcMain.handle('getSessionID', getSessionId);
  ipcMain.handle('openFolderDialog', openFolderDialog);
  ipcMain.handle('openMultiSourceFileDialog', openMultiSourceFileDialog);
  ipcMain.handle('minimizeWindow', minimizeWindow);
  ipcMain.handle('maximizeWindow', maximizeWindow);
  ipcMain.handle('closeWindow', closeWindow);
}
