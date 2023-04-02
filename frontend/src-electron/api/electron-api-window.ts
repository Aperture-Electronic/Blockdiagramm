import { mainWindow } from '../electron-main';

export function closeWindow() {
  mainWindow?.close();
}

export function minimizeWindow() {
  mainWindow?.minimize();
}

export function maximizeWindow() {
  if (mainWindow?.isMaximized()) {
    mainWindow?.unmaximize();
  } else {
    mainWindow?.maximize();
  }
}
