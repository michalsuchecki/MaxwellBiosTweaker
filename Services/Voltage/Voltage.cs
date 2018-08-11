using System.Collections.Generic;

internal class Voltage : BiosTweakerBase
{
    private new const int FE000 = 0;
    private const int FE001 = 2;
    private const int FE002 = 3;
    private readonly uint FE003;
    private readonly uint FE004;
    private readonly uint FE005;
    private readonly uint FE006;
    public readonly List<CE024> FE007;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, (int)this.PE003);
        }
    }

    public byte Index
    {
        get
        {
            return base.buffer[base.offset + 2];
        }
        set
        {
            base.buffer[base.offset + 2] = value;
        }
    }

    public byte Caption
    {
        get
        {
            return (byte)(15U - base.buffer[base.offset]);
        }
    }

    public byte PE003
    {
        get
        {
            return base.buffer[base.offset + 3];
        }
        set
        {
            base.buffer[base.offset + 3] = value;
        }
    }

    public bool PE004
    {
        get
        {
            return base.buffer[base.offset] == byte.MaxValue;
        }
    }

    public int PE005
    {
        get
        {
            return (int)this.FE004;
        }
    }

    public Voltage(byte[] param0, int param1, uint param2, uint param3, uint param4)
        : base(param0, param1)
    {
        this.FE003 = param2;
        this.FE004 = param3;
        this.FE005 = param4;
        this.FE006 = param2 + param3 * param4;
        this.FE007 = this.E000();
    }

    private List<CE024> E000()
    {
        List<CE024> list = new List<CE024>();

        for (int index = 0; (long)index < (long)this.FE004; ++index)
            list.Add(new CE024(base.buffer, base.offset + (int)this.FE003 + index * (int)this.FE005, (int)this.FE005));

        return list;
    }
}
