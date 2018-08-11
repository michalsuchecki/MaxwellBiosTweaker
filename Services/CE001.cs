using System;
using System.Collections.Generic;
using System.Linq;

internal class CE001 : BiosTweakerBase
{
    private const int FE000 = 8;
    private const int FE001 = 9;
    private const int FE002 = 10;
    public readonly List<CE002> FE003;
    public readonly CE004 FE004;

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
            return base.buffer[base.offset + 8];
        }
    }

    public byte PE002
    {
        get
        {
            return base.buffer[base.offset + 9];
        }
    }

    public byte PE003
    {
        get
        {
            return base.buffer[base.offset + 10];
        }
    }

    public bool PE004
    {
        get
        {
            if (this.FE004 != null)
                return this.FE004.PE008 > -1;
            return false;
        }
    }

    public CE001(byte[] param0, int param1, int param2) : base(param0, param1)
    {
        this.FE003 = this.E000();
        this.FE004 = this.E001(param2);
    }

    private List<CE002> E000()
    {
        List<CE002> list = new List<CE002>();
        for (int index = 0; index < (int)this.PE003; ++index)
        {
            int num = base.offset + (this.PE001 + this.PE002 * index);
            list.Add(new CE002(base.buffer, num, this.PE002));
        }
        return list;
    }

    private CE004 E001(int param0)
    {
        CE002 obj = Enumerable.FirstOrDefault<CE002>(this.FE003, param0_2 => (int)param0_2.PE001 == 80);

        if (obj != null)
            return new CE004(base.buffer, param0 + obj.PE004, obj.PE003, param0);

        return null;
    }
}
