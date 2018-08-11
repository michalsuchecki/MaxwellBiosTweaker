using System;

internal class PCIRChunk : BiosTweakerBase
{
    private readonly byte[] PCIRHeader = new byte[4] // PCIR
    {
    80, // 0x50
    67, // 0x43
    73, // 0x49
    82  // 0x52
    };
    private const int FE000 = 4;
    private const int FE001 = 6;
    private const int FE002 = 16;
    private const int FE003 = 21;
    private const int FE004 = 295;

    public bool PE000 => CE027.E000(base.buffer, PCIRHeader, base.offset, false, new byte?()) == base.offset;

    public ushort PE001 => BitConverter.ToUInt16(base.buffer, base.offset + 4);

    public ushort PE002 => BitConverter.ToUInt16(base.buffer, base.offset + 6);

    public ushort PE003 => BitConverter.ToUInt16(base.buffer, base.offset + 295);

    public int PE004 => BitConverter.ToUInt16(base.buffer, base.offset + 16) * 512;

    public bool PE005 => (base.buffer[base.offset + 21] & 128) == 128;

    public PCIRChunk(byte[] buffer, int offset) : base(buffer, offset)
    {

    }
}
