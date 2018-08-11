using System;

internal class CE01D : BiosTweakerBase
{
    private readonly int FE000;

    public string PE000
    {
        get
        {
            return CE027.E002(this.buffer, base.offset, this.FE000);
        }
    }

    public ushort PE001
    {
        get
        {
            return BitConverter.ToUInt16(buffer, base.offset);
        }
        set
        {
            CE027.E004(value, buffer, base.offset);
        }
    }

    public CE01D(byte[] param0, int param1, int param2) : base(param0, param1)
    {
        this.FE000 = param2;
    }
}
