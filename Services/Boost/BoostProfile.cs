using System.Collections.Generic;

internal class BoostProfile : BiosTweakerBase
{
    private const int FE000 = 1;
    private const int FE001 = 2;
    private const int FE002 = 3;
    private const int FE003 = 4;
    private const int FE004 = 5;
    public readonly List<BoostProfileEntry> FE005;

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
            return base.buffer[base.offset + 5];
        }
    }

    public byte PE004
    {
        get
        {
            return base.buffer[base.offset + 3];
        }
    }

    public byte PE005
    {
        get
        {
            return base.buffer[base.offset + 4];
        }
    }

    public uint PE006
    {
        get
        {
            return this.PE002 + this.PE004 * (uint)this.PE005;
        }
    }

    public uint PE007
    {
        get
        {
            return this.PE001 + this.PE006 * this.PE003;
        }
    }

    public BoostProfile(byte[] param0, int param1) : base(param0, param1)
    {
        this.FE005 = this.E000();
    }

    private List<BoostProfileEntry> E000()
    {
        List<BoostProfileEntry> list = new List<BoostProfileEntry>();
        for (int index = 0; index < (int)this.PE003; ++index)
        {
            int num = base.offset + (int)(this.PE001 + this.PE006 * index);
            list.Add(new BoostProfileEntry(base.buffer, num, this.PE002, this.PE005, this.PE004));
        }
        return list;
    }
}
