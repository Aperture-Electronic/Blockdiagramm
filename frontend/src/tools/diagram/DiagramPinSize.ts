export default class DiagramPinSize {
  public lsb: number;
  public msb: number;
  public lsbUnpacked: number | null;
  public msbUnpacked: number | null;

  constructor(
    lsb: number,
    msb: number,
    lsbUnpacked: number | null,
    msbUnpacked: number | null
  ) {
    this.lsb = lsb;
    this.msb = msb;
    this.lsbUnpacked = lsbUnpacked;
    this.msbUnpacked = msbUnpacked;
  }

  public getBitWidth(): number {
    return this.msb - this.lsb + 1;
  }

  public getBitWidthUnpacked(): number {
    if (this.lsbUnpacked != null && this.msbUnpacked != null) {
      return this.msbUnpacked - this.lsbUnpacked + 1;
    }

    return 0;
  }

  public hasUnpacked(): boolean {
    return this.lsbUnpacked != null && this.msbUnpacked != null;
  }
}
