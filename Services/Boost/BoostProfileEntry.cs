using System;
using System.Collections.Generic;

internal class BoostProfileEntry : BiosTweakerBase
{
    private const int FE000 = 0;
    private const int FE001 = 1;
    private const int FE002 = 2;
    private const int FE003 = 4;
    private readonly uint FE004;
    private readonly uint FE005;
    private readonly uint FE006;
    private readonly uint FE007;
    public readonly List<CE01B> FE008;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, (int)this.FE004);
        }
    }

    public bool PE001
    {
        get
        {
            return PE002 == byte.MaxValue;
        }
    }

    public byte PE002
    {
        get
        {
            return (byte)(15 - (BitConverter.ToUInt16(base.buffer, base.offset) >> 5));
        }
    }

    public ushort PE003
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + 2);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + 2);
        }
    }

    public ushort PE004
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + 4);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + 4);
        }
    }

    public BoostProfileEntry(byte[] param0, int param1, uint param2, uint param3, uint param4) : base(param0, param1)
    {
        this.FE004 = param2;
        this.FE005 = param3;
        this.FE006 = param4;
        this.FE007 = param2 + param3 * param4;
        this.FE008 = this.E000();
    }

    private List<CE01B> E000()
    {
        List<CE01B> list = new List<CE01B>();
        
        for (int index = 0; (long)index < (long)this.FE005; ++index)
            list.Add(new CE01B(base.buffer, base.offset + (int)this.FE004 + index * (int)this.FE006, (int)this.FE006));

        return list;
    }
}
