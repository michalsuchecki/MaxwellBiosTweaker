using System.Collections.Generic;

internal class VoltageTable : BiosTweakerBase
{
    private new const int FE000 = 1;
    private const int FE001 = 2;
    private const int FE002 = 3;
    private const int FE003 = 8;
    private const int FE004 = 7;
    public readonly List<VoltageEntry> ListaVoltagens;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, this.PE001);
        }
    }

    public byte PE001
    {
        get
        {
            return base.buffer[base.offset + 1];
        }
    }

    public byte PE002
    {
        get
        {
            return base.buffer[base.offset + 2];
        }
    }

    public byte PE003
    {
        get
        {
            return base.buffer[base.offset + 3];
        }
    }

    public byte PE004
    {
        get
        {
            if (8 >= this.PE001)
                return byte.MaxValue;
            return base.buffer[base.offset + 8];
        }
    }

    public byte PE005
    {
        get
        {
            if (7 >= this.PE001)
                return byte.MaxValue;
            return base.buffer[base.offset + 7];
        }
    }

    public VoltageTable(byte[] param0, int param1)
        : base(param0, param1)
    {
        this.ListaVoltagens = this.E000();
    }

    private List<VoltageEntry> E000()
    {
        List<VoltageEntry> list = new List<VoltageEntry>();
        for (int index = 0; index < (int)this.PE003; ++index)
        {
            int num = base.offset + (this.PE001 + this.PE002 * index);
            list.Add(new VoltageEntry(base.buffer, num, this.PE002));
        }
        return list;
    }
}
