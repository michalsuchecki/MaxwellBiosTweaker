using System;

internal class CE024 : BiosTweakerBase
{
    private readonly int FE000;
    private readonly int FE001;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, this.FE000);
        }
    }

    public uint PE001
    {
        get
        {
            return this.PE002 & 8191U;
        }
        set
        {
            this.PE002 = this.PE002 & 57344U | value;
        }
    }

    public uint PE002
    {
        get
        {
            return BitConverter.ToUInt32(base.buffer, base.offset + this.FE001);
        }
        set
        {
            CE027.ConvertToBytes(value, base.buffer, base.offset + this.FE001);
        }
    }

    public bool PE003
    {
        get
        {
            return (int)this.PE001 == (int)this.PE002;
        }
    }

    public CE024(byte[] param0, int param1, int param2)
        : base(param0, param1)
    {
        this.FE000 = param2;
        this.FE001 = param2 - 4;
    }
}
