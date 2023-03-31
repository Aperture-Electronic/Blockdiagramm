let serverChild: {
  kill(): unknown;
  stderr: { on: (arg0: string, arg1: (data: unknown) => void) => void };
  on: (arg0: string, arg1: (code: unknown) => void) => void;
  exitCode: number;
};

export function startBackendServer() {
  // TODO: Backend application dll path
  const { spawn } = require('child_process');
  const path = [
    '../backend/Blockdiagramm/BlockdiagrammBackend/bin/Debug/net7.0/BlockdiagrammBackend.dll',
  ];
  const dotnetCommand = 'dotnet';

  console.log('Starting backend server in path: ' + path[0] + '...');

  serverChild = spawn(dotnetCommand, path, { stdio: 'ignore' });

  serverChild.stderr?.on('data', (data: unknown) => {
    console.log('.NET Backend server error: ', data);
  });

  serverChild.on('close', (code: unknown) => {
    console.log('The .NET backend server has been closed: ', code);
  });
}

export function generateUUIDForSession(): string {
  const uuid = require('uuid');
  const id = uuid.v4();
  return id;
}

export function stopBackendServer() {
  if (serverChild?.exitCode === null) {
    console.log('Stopping backend server...');
    serverChild?.kill();
  }
}
