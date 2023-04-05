import DiagramPin from './DiagramPin';

export default class DiagramComponent {
  public hash: string;
  public name: string;
  public pins: Array<DiagramPin>;

  constructor(hash: string, name: string) {
    this.hash = hash;
    this.name = name;
    this.pins = new Array<DiagramPin>();
  }

  public addPin(pin: DiagramPin): void {
    this.pins.push(pin);
  }

  public removePin(pin: DiagramPin): void {
    const index = this.pins.indexOf(pin);
    if (index > -1) {
      this.pins.splice(index, 1);
    }
  }
}
