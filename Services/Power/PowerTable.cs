using System.Collections.Generic;

internal class PowerTable : BiosTweakerBase
{
    private const int FE000 = 1;
    private const int FE001 = 2;
    private const int FE002 = 3;
    private const int FE003 = 10;
    private const int FE004 = 9;
    public readonly List<PowerEntry> ListPowerEntries;

    public string PE000
    {
        get
        {
            return CE027.E002(base.buffer, base.offset, this.PE001);
        }
    }

    public byte PE001
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
            return base.buffer[base.offset + 2];
        }
    }

    public byte E003
    {
        get
        {
            return base.buffer[base.offset + 3];
        }
    }

    public byte E004
    {
        get
        {
            return base.buffer[base.offset + 10];
        }
    }

    public byte E005
    {
        get
        {
            return base.buffer[base.offset + 9];
        }
    }

    public PowerTable(byte[] buffer, int offset) : base(buffer, offset)
    {
        this.ListPowerEntries = GetPowerEntries();
    }

    private List<PowerEntry> GetPowerEntries()
    {
        List<PowerEntry> list = new List<PowerEntry>();
        
        for (int index = 0; index < (int)this.E003; ++index)
        {
            int num = base.offset + (this.PE001 + this.E002 * index);
            list.Add(new PowerEntry(base.buffer, num, this.E002));
        }
        return list;
    }
}
