using System;

internal class RomHeader : BiosTweakerBase
{
    private static byte[] PMIDHeader = new byte[4] // PMID
    {
        80, // 0x50
        77, // 0x4D
        73, // 0x49
        68  // 0x44
    };

    private new const int FE000 = 24;
    private const int FE001 = 84;
    private const int FE002 = 56;
    private const int FE003 = 8;
    private const int FE004 = 134;
    private const int FE005 = 57;
    private const int FE006 = 223;
    private const int FE007 = 14;
    private const int FE008 = 290;
    private const int FE009 = 35;
    public readonly PCIRChunk E00B;
    private int? E00C;

    private new int E000
    {
        get
        {
            if (!this.E00C.HasValue)
                this.E00C = new int?(CE027.E000(base.buffer, RomHeader.PMIDHeader, base.offset, false, new byte?()) - (114 + base.offset));
            return this.E00C.Value;
        }
    }

    public int GetChunkSize
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + 24);
        }
    }

    public string GetReleaseDate
    {
        get
        {
            return CE027.ReadBlock(base.buffer, base.offset + 56, 8);
        }
    }

    public ushort E003 // DataSize
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + 84);
        }
    }

    public string GetNameDescription
    {
        get
        {
            return CE027.ReadBlock(base.buffer, base.offset + this.E000 + 134, 57);
        }
    }

    public string GetVersionDescription
    {
        get
        {
            return CE027.ReadBlock(base.buffer, base.offset + this.E000 + 223, 14);
        }
    }

    public string GetBoardDescription
    {
        get
        {
            return CE027.ReadBlock(base.buffer, base.offset + this.E000 + 290, 35);
        }
    }

    public RomHeader(byte[] buffer, int offset) : base(buffer, offset)
    {
        this.E00B = new PCIRChunk(buffer, offset + this.GetChunkSize);
    }
}
