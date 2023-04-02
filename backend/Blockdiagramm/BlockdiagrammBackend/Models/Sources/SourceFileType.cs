namespace BlockdiagrammBackend.Models.Sources
{
    public enum SourceFileType
    {
        Auto                = 0x00,
        SystemVerilogSource = 0x01,
        VerilogSource       = 0x0100,
        VHDLSource          = 0x010000,
        SystemVerilogHeader = 0x02
    }
}
