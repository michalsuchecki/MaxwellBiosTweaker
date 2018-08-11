using System;

internal class BoostClock : BiosTweakerBase
{
    private const int FE000 = 0;
    private const int FE001 = 4;
    private readonly int FE002;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, this.FE002);
        }
    }

    public ushort Frequency
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset);
        }
    }

    public byte Index
    {
        get
        {
            return base.buffer[base.offset + 4];
        }
        set
        {
            base.buffer[base.offset + 4] = value;
        }
    }

    public BoostClock(byte[] param0, int param1, int param2) : base(param0, param1)
    {
        this.FE002 = param2;
    }
}
