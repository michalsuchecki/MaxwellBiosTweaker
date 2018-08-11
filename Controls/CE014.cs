using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class CE014 : UserControl, ComponenteDaTela
{
    private List<MaxVoltageControl> _EntryControlList = new List<MaxVoltageControl>();
    private PerfTable _PerfTable;
    private VoltageTable _VoltageTable;

    internal PerfTable PerfTable
    {
        get
        {
            return this._PerfTable;
        }
        set
        {
            this._PerfTable = value;
            this.UpdateData();
        }
    }

    internal VoltageTable VoltageTable
    {
        get
        {
            return this._VoltageTable;
        }
        set
        {
            this._VoltageTable = value;
            this.UpdateData();
        }
    }

    public CE014()
    {
        this.InitializeComponent();
    }

    public void ApplyChanges()
    {
        if (this.PerfTable == null || this.VoltageTable == null)
            return;
        foreach (MaxVoltageControl obj in this._EntryControlList)
            obj.ApplyChanges();
    }

    public void Reset()
    {
        this.PerfTable = null;
        this.VoltageTable = null;
        this.InternalReset();
    }

    private void InternalReset()
    {
        foreach (MaxVoltageControl obj in this._EntryControlList)
            obj.Reset();
        this.Controls.Clear();
        this.Enabled = false;
    }

    private void UpdateData()
    {
        this.InternalReset();
        if (this.PerfTable == null || this.VoltageTable == null)
            return;
        int num = 0;
        this._EntryControlList = new List<MaxVoltageControl>();
        List<byte> list = Enumerable.ToList<byte>(Enumerable.Distinct<byte>(Enumerable.Select<Voltage, byte>(Enumerable.OrderBy<Voltage, byte>(Enumerable.Where<Voltage>(this.PerfTable.Voltages, e => !e.PE004), e => e.Caption), e => e.Index)));
        if (list == null)
            return;
        foreach (byte vid in list)
        {
            if (vid < this.VoltageTable.ListaVoltagens.Count)
            {
                VoltageEntry obj1 = this.VoltageTable.ListaVoltagens[vid];
                MaxVoltageControl obj2 = new MaxVoltageControl();
                obj2.VoltageEntry = obj1;
                obj2.Left = 0;
                obj2.Top = num;
                obj2.Caption = this.GetCaption(vid);
                this.Controls.Add(obj2);
                this._EntryControlList.Add(obj2);
                num += obj2.Height;
            }
        }
        this.Enabled = true;
    }

    private string GetCaption(byte vid)
    {
        if (this.PerfTable == null)
            return "";
        List<string> list = Enumerable.ToList<string>(Enumerable.Select<Voltage, string>(Enumerable.OrderBy<Voltage, byte>(Enumerable.Where<Voltage>(this.PerfTable.Voltages, param0 => (int)param0.Index == (int)vid), p => p.Caption), p => string.Format("P{0:00}", p.Caption)));
        if (list.Count < 1)
            return "";
        return string.Join(",", list.ToArray()) + " - Voltage";
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // CE014
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Name = "CE014";
        this.Size = new System.Drawing.Size(414, 114);
        this.ResumeLayout(false);

    }
}
