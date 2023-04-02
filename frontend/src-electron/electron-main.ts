import { app, nativeTheme, BrowserWindow } from 'electron';
import path from 'path';
import os from 'os';
import {
  generateUUIDForSession,
  startBackendServer,
  stopBackendServer,
} from './electron-backend';
import { initializeElectronApi } from './api/electron-api-init';

// needed in case process is undefined under Linux
const platform = process.platform || os.platform();

try {
  if (platform === 'win32' && nativeTheme.shouldUseDarkColors === true) {
    require('fs').unlinkSync(
      path.join(app.getPath('userData'), 'DevTools Extensions')
    );
  }
} catch (_) {}

// let mainWindow: BrowserWindow | undefined;
export let mainWindow: BrowserWindow | undefined = undefined;

export const sessionId = generateUUIDForSession();
console.log('UUID of current session: ' + sessionId);

function createWindow() {
  /**
   * Initial window options
   */
  mainWindow = new BrowserWindow({
    icon: path.resolve(__dirname, 'icons/icon.png'), // tray icon
    width: 1000,
    height: 600,
    useContentSize: true,
    webPreferences: {
      contextIsolation: true,
      // More info: https://v2.quasar.dev/quasar-cli-vite/developing-electron-apps/electron-preload-script
      preload: path.resolve(__dirname, process.env.QUASAR_ELECTRON_PRELOAD),
    },
    frame: process.env.DEBUGGING ? true : false,
  });

  mainWindow.loadURL(process.env.APP_URL);

  mainWindow.resizable = true;

  if (process.env.DEBUGGING) {
    // if on DEV or Production with debug enabled
    mainWindow.webContents.openDevTools();
    mainWindow.menuBarVisible = true;
  } else {
    // we're on production; no access to devtools pls
    mainWindow.menuBarVisible = false;
    mainWindow.webContents.on('devtools-opened', () => {
      mainWindow?.webContents.closeDevTools();
    });
  }

  mainWindow.on('closed', () => {
    stopBackendServer();
    mainWindow = undefined;
  });
}

app.on('ready', () => {
  initializeElectronApi();

  createWindow();
  startBackendServer();
});

app.on('window-all-closed', () => {
  if (platform !== 'darwin') {
    app.quit();
  }
});

app.on('activate', () => {
  if (mainWindow === undefined) {
    createWindow();
  }
});
