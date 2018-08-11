using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class UCPowerTable : UserControl, ComponenteDaTela
{
    private List<PowerEntryControl> _EntryControlList = new List<PowerEntryControl>();
    private PowerTable _PowerTable;
    private IContainer components;

    internal PowerTable PowerTable
    {
        get
        {
            return this._PowerTable;
        }
        set
        {
            this.Reset();
            this._PowerTable = value;
            this.UpdateData();
        }
    }

    public UCPowerTable()
    {
        this.InitializeComponent();
    }

    public void ApplyChanges()
    {
        if (this.PowerTable == null)
            return;
        foreach (PowerEntryControl obj in this._EntryControlList)
            obj.ApplyChanges();
    }

    public void Reset()
    {
        this._PowerTable = null;
        this.InternalReset();
    }

    private void InternalReset()
    {
        foreach (PowerEntryControl obj in this._EntryControlList)
            obj.Reset();
        this.Controls.Clear();
        this.Enabled = false;
    }

    private void UpdateData()
    {
        this.InternalReset();

        if (this.PowerTable == null)
            return;

        this.Enabled = true;
        int num = 4;
        this._EntryControlList = new List<PowerEntryControl>();

        foreach (PowerEntry powerEntry in PowerTable.ListPowerEntries.Where(p => (int)p.Def != 0).ToList())
        {
            PowerEntryControl obj2 = new PowerEntryControl();
            obj2.PowerEntry = powerEntry;
            obj2.Left = 0;
            obj2.Top = num;
            this.Controls.Add(obj2);
            this._EntryControlList.Add(obj2);
            num += obj2.Height + 8;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && this.components != null)
            this.components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // UCPowerTable
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoScroll = true;
        this.Name = "UCPowerTable";
        this.Size = new System.Drawing.Size(416, 238);
        this.ResumeLayout(false);

    }
}
