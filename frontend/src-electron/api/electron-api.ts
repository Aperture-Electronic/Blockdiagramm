import { ipcMain, ipcRenderer } from 'electron';

interface ElectronApi {
  getSessionID(): Promise<string>;
  minimizeWindow(): Promise<void>;
  maximizeWindow(): Promise<void>;
  closeWindow(): Promise<void>;
  openFolderDialog(): Promise<string[] | undefined>;
  openMultiSourceFileDialog(): Promise<string[] | undefined>;
}

export default {
  getSessionID: () => ipcRenderer.invoke('getSessionID'),
  openFolderDialog: () => ipcRenderer.invoke('openFolderDialog'),
  openMultiSourceFileDialog: () =>
    ipcRenderer.invoke('openMultiSourceFileDialog'),
  minimizeWindow: () => ipcRenderer.invoke('minimizeWindow'),
  maximizeWindow: () => ipcRenderer.invoke('maximizeWindow'),
  closeWindow: () => ipcRenderer.invoke('closeWindow'),
} as ElectronApi;
