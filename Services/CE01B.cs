using System;

internal class CE01B : BiosTweakerBase
{
    private const int FE000 = 0;
    private const int FE001 = 1;
    private const int FE002 = 2;
    private const int FE003 = 4;
    private readonly int FE004;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, this.FE004);
        }
    }

    public byte PE001
    {
        get
        {
            return base.buffer[base.offset];
        }
        set
        {
            base.buffer[base.offset] = value;
        }
    }

    public byte PE002
    {
        get
        {
            return base.buffer[base.offset + 1];
        }
        set
        {
            base.buffer[base.offset + 1] = value;
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

    public CE01B(byte[] param0, int param1, int param2) : base(param0, param1)
    {
        this.FE004 = param2;
    }
}
