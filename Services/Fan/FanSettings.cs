using System;

internal class FanSettings : BiosTweakerBase
{
    private const int FE000 = 2;
    private const int FE001 = 3;
    private const int FE002 = 14;
    private const int FE003 = 16;

    public bool PE000
    {
        get
        {
            return true;
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
            return base.buffer[base.offset + this.PE001 + 2];
        }
        set
        {
            base.buffer[base.offset + FE001 + 2] = value;
        }
    }

    public byte PE003
    {
        get
        {
            return base.buffer[base.offset + this.PE001 + 3];
        }
        set
        {
            base.buffer[base.offset + FE001 + 3] = value;
        }
    }

    public ushort PE004
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + this.PE001 + 14);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + this.PE001 + 14);
        }
    }

    public ushort PE005
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + this.PE001 + 16);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + this.PE001 + 16);
        }
    }

    public FanSettings(byte[] param0, int param1) : base(param0, param1)
    {

    }
}
