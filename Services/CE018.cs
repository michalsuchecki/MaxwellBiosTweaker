using System;

internal class CE018 : BiosTweakerBase
{
    private const int FE000 = 0;
    private const int FE001 = 3;
    private readonly uint FE002;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, (int)this.FE002);
        }
    }

    public byte PE001
    {
        get
        {
            return (byte)(15 - (BitConverter.ToUInt16(base.buffer, base.offset) >> 5));
        }
    }

    public byte PE002
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

    public CE018(byte[] param0, int param1, uint param2) : base(param0, param1)
    {
        this.FE002 = param2;
    }
}
