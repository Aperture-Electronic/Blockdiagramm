export default interface ComponentMeta {
  bus: {
    direction: number;
    name: string;
  };
  modulePort: [
    {
      direction: number;
      name: string;
      packedSize: object;
      unpackedSize: object | null;
    }
  ];
  portCount: number;
}
