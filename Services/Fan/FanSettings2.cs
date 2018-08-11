using System;
internal class FanSettings2 : BiosTweakerBase
{
    public bool E000
    {
        get
        {
            return true;
        }
    }

    public byte E001
    {
        get
        {
            return base.buffer[base.offset + 1];
        }
    }

    public byte E002
    {
        get
        {
            return base.buffer[base.offset + this.E001 + 17];
        }
        set
        {
            base.buffer[base.offset + this.E001 + 17] = value;
        }
    }

    public byte E003
    {
        get
        {
            return base.buffer[base.offset + this.E001 + 18];
        }
        set
        {
            base.buffer[base.offset + this.E001 + 18] = value;
        }
    }

    public byte E004
    {
        get
        {
            return base.buffer[base.offset + this.E001 + 19];
        }
        set
        {
            base.buffer[base.offset + this.E001 + 19] = value;
        }
    }

    public ushort E005
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + this.E001 + 21);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + this.E001 + 21);
        }
    }

    public ushort E006
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + this.E001 + 23);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + this.E001 + 23);
        }
    }

    public ushort E007
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + this.E001 + 25);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + this.E001 + 25);
        }
    }

    public ushort E008
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + this.E001 + 27);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + this.E001 + 27);
        }
    }

    public ushort E009
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + this.E001 + 29);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + this.E001 + 29);
        }
    }

    public ushort E00A
    {
        get
        {
            return BitConverter.ToUInt16(base.buffer, base.offset + this.E001 + 31);
        }
        set
        {
            CE027.E004(value, base.buffer, base.offset + this.E001 + 31);
        }
    }

    public FanSettings2(byte[] param0, int param1) : base(param0, param1)
    {

    }
}
