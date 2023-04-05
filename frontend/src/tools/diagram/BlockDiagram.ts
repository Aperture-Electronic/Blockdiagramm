export default class BlockDiagram {
  public hash: string;
  public name: string;
  public isTop: boolean;

  constructor(hash: string, name: string, isTop: boolean) {
    this.hash = hash;
    this.name = name;
    this.isTop = isTop;
  }
}
