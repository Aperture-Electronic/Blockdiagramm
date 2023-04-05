import DiagramPinSize from './DiagramPinSize';

export enum PinDirection {
  InputSlave,
  OutputMaster,
}

export enum PinType {
  Single,
  Multiple,
  Bus,
}

export default class DiagramPin {
  public direction: PinDirection;
  public type: PinType;
  public name: string;
  public size: DiagramPinSize | null;
  public isClock: boolean;
  public isReset: boolean;

  public pinContactDeterminer = '';

  public constructor(direction: PinDirection, type: PinType, name: string) {
    this.direction = direction;
    this.type = type;
    this.name = name;
    this.size = new DiagramPinSize(0, 0, null, null);
    this.isClock = false;
    this.isReset = false;
  }
}
