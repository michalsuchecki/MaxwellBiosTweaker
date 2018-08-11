using System;

internal class PowerEntry : BiosTweakerBase
{
    private const int FE000 = 2;
    private const int FE001 = 6;
    private const int FE002 = 10;
    private readonly int FE003;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, this.FE003);
        }
    }

    public uint Min
    {
        get
        {
            return BitConverter.ToUInt32(base.buffer, base.offset + 2);
        }
        set
        {
            CE027.ConvertToBytes(value, base.buffer, base.offset + 2);
        }
    }

    public uint Def
    {
        get
        {
            return BitConverter.ToUInt32(base.buffer, base.offset + 6);
        }
        set
        {
            CE027.ConvertToBytes(value, base.buffer, base.offset + 6);
        }
    }

    public uint Max
    {
        get
        {
            uint value = 0;
            try
            {
                value = BitConverter.ToUInt32(base.buffer, base.offset + 10);
            }
            catch(Exception ex)
            {
                var test = "";
            }
            finally
            {
            }

            return value;

            //return BitConverter.ToUInt32(base.E001, base.E000 + 10);
        }
        set
        {
            CE027.ConvertToBytes(value, base.buffer, base.offset + 10);
        }
    }

    public PowerEntry(byte[] buffer, int offset, int param2) : base(buffer, offset)
    {
        this.FE003 = param2;
    }
}
