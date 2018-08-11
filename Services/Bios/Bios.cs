using System.Collections.Generic;
using System.IO;

internal class Bios
{
    private byte[] PE0000 = new byte[2]
    {
        85, // 0x55
        170 // 0xAA
    };

    public readonly string caminhoArquivo;
    public readonly byte[] arquivoEmBytes;
    public readonly List<LinhaBios> linhasDaBios;

    public bool PE000
    {
        get
        {
            if (linhasDaBios.Count > 0)
                return linhasDaBios[0].PerfTable != null;
            return false;
        }
    }

    public bool PossuiFanSettings
    {
        get
        {
            if (linhasDaBios.Count > 0)
                return linhasDaBios[0].FanSettings != null;
            return false;
        }
    }

    public bool PossuiPowerTable
    {
        get
        {
            if (linhasDaBios.Count > 0)
                return linhasDaBios[0].PowerTable != null;
            return false;
        }
    }

    public bool PE003
    {
        get
        {
            if (linhasDaBios.Count > 0)
                return linhasDaBios[0].VoltageTable != null;
            return false;
        }
    }

    public bool PossuiBoostProfile
    {
        get
        {
            if (linhasDaBios.Count > 0)
                return linhasDaBios[0].BoostProfile != null;
            return false;
        }
    }

    public Bios(string param0)
    {
        caminhoArquivo = param0;
        arquivoEmBytes = File.ReadAllBytes(param0);
        linhasDaBios = LerBios();
    }

    private List<LinhaBios> LerBios()
    {
        List<LinhaBios> list = new List<LinhaBios>();
        
        int num = CE027.E000(arquivoEmBytes, PE0000, 0, false, new byte?());
        
        if (num > -1)
        {
            RomHeader obj;
            do
            {
                obj = new RomHeader(arquivoEmBytes, num);
                if (obj.E00B.PE000)
                {
                    list.Add(new LinhaBios(arquivoEmBytes, num));
                    num += obj.E00B.PE004;
                }
                else
                    break;
            }
            while (!obj.E00B.PE005);
        }
        return list;
    }

    public void EscreverBios(string param0)
    {
        foreach (LinhaBios obj in linhasDaBios)
            obj.ObterImageChecksum();

        File.WriteAllBytes(param0, arquivoEmBytes);
    }
}
