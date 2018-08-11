using System;

internal class TempTargets : BiosTweakerBase
{
    public byte E000
    {
        get
        {
            return this.buffer[base.offset + 1];
        }
    }

    public bool PE001
    {
        get
        {
            return this.buffer[this.E000 + this.E000] != 0;
        }
    }

    public ushort E002
    {
        get
        {
            return BitConverter.ToUInt16(this.buffer, this.E000 + base.offset + 2);
        }
        set
        {
            CE027.E004(value, this.buffer, this.E000 + base.offset + 2);
        }
    }

    public ushort E003
    {
        get
        {
            return BitConverter.ToUInt16(this.buffer, this.E000 + base.offset + 4);
        }
        set
        {
            CE027.E004(value, this.buffer, this.E000 + base.offset + 4);
        }
    }

    public ushort E004
    {
        get
        {
            return BitConverter.ToUInt16(this.buffer, this.E000 + base.offset + 6);
        }
        set
        {
            CE027.E004(value, this.buffer, this.E000 + base.offset + 6);
        }
    }

    public TempTargets(byte[] param0, int param1) : base(param0, param1)
    {
    }
}
