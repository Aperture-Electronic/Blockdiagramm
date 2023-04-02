import { SourceType } from './SourceFileType';

export default class SourceFile {
  public readonly Path: string;
  public readonly ShortName: string;
  public readonly Hash: string;
  public readonly Exist: boolean;
  public readonly Type: SourceType;

  public constructor(
    path: string,
    shortName: string,
    hash: string,
    exist: boolean,
    type: SourceType
  ) {
    this.Path = path;
    this.ShortName = shortName;
    this.Hash = hash;
    this.Exist = exist;
    this.Type = type;
  }
}
