export enum ServerStatusCode {
  Stopped = 0,
  Idle = 1,
  Busy = 2,
  Error = 3,
}

export class ServerStatus {
  public StatusCode: ServerStatusCode;
  public ServerTick: number | string;
  public ServerStartDate: Date;

  public constructor(
    statusCode?: ServerStatusCode,
    serverTick?: number,
    serverStartDate?: Date
  ) {
    if (statusCode && serverTick && serverStartDate) {
      this.StatusCode = statusCode;
      this.ServerTick = serverTick;
      this.ServerStartDate = serverStartDate;

      return;
    }

    // Default
    this.StatusCode = ServerStatusCode.Stopped;
    this.ServerTick = 0;
    this.ServerStartDate = new Date();
  }

  public GetServerStatusColor(): string {
    switch (this.StatusCode) {
      case ServerStatusCode.Idle:
        return 'green';
      case ServerStatusCode.Stopped:
        return 'red';
      case ServerStatusCode.Busy:
        return 'yellow';
      default:
        return 'red';
    }
  }

  public GetServerStatusString(): string {
    switch (this.StatusCode) {
      case ServerStatusCode.Idle:
        return 'Running';
      case ServerStatusCode.Stopped:
        return 'Stopped';
      case ServerStatusCode.Busy:
        return 'Busy';
      default:
        return 'Error';
    }
  }

  public GetServerUnworkable(): boolean {
    return (
      this.StatusCode == ServerStatusCode.Stopped ||
      this.StatusCode == ServerStatusCode.Error
    );
  }
}

import { fetchJSONWithTimeout } from 'src/tools/Fetch';
import { BackendServerAddress } from '../BackendServer';

const checkServerStatusPath = '/ServerMonitor/CheckServerStatus';

export function GetServerStatus(): Promise<ServerStatus> {
  return fetchJSONWithTimeout(
    fetch(BackendServerAddress + checkServerStatusPath, {
      method: 'GET',
    }),
    500
  )
    .then((data) => {
      if (typeof data === 'object') {
        return new ServerStatus(
          data.statusCode,
          data.serverTick,
          data.startTime
        );
      }
      return new ServerStatus(ServerStatusCode.Error, 0, new Date(Date.now()));
    })
    .catch((ex) => {
      throw new Error(ex);
    });
}

export function GetServerStatusAsync(obj: ServerStatus) {
  fetchJSONWithTimeout(
    fetch(BackendServerAddress + checkServerStatusPath, {
      method: 'GET',
    }),
    500
  )
    .then((data) => {
      if (typeof data === 'object') {
        obj.StatusCode = data.statusCode;
        obj.ServerTick =
          data.statusCode == ServerStatusCode.Idle ||
          data.statusCode == ServerStatusCode.Busy
            ? data.serverTick
            : 'N/A';
      }
    })
    .catch((error: Error) => {
      console.error(error.message);
      obj.StatusCode = ServerStatusCode.Stopped;
      obj.ServerTick = 'N/A';
    });

  return {};
}
