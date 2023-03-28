import { shell } from 'electron';

export function startBackendServer() {
  // TODO: Backend application dll path
  const child = require('child_process').execFile;
  const path = [
    '../backend/Blockdiagramm/BlockdiagrammBackend/bin/Debug/net7.0/BlockdiagrammBackend.dll',
  ];
  const dotnetCommand = 'dotnet';

  console.log('Starting backend server in path: ' + path[0] + '...');

  child(dotnetCommand, path, function (err: unknown, data: unknown) {
    console.log(err);
    console.log(data?.toString());
  });
}
