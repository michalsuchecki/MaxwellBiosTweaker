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
    private readonly string[] sectionTitles = {"TDP Limit", "", "PCI-E Slot", "PCI Rail #1", "PCI Rail #2", "Power Limit"};

    internal PowerTable PowerTable
    {
        get
        {
            return _PowerTable;
        }
        set
        {
            Reset();
            _PowerTable = value;
            UpdateData();
        }
    }

    public UCPowerTable()
    {
        InitializeComponent();
    }

    public void ApplyChanges()
    {
        if (PowerTable == null)
            return;
        foreach (PowerEntryControl obj in _EntryControlList)
            obj.ApplyChanges();
    }

    public void Reset()
    {
        _PowerTable = null;
        InternalReset();
    }

    private void InternalReset()
    {
        foreach (PowerEntryControl obj in _EntryControlList)
            obj.Reset();
        Controls.Clear();
        Enabled = false;
    }

    private void UpdateData()
    {
        InternalReset();

        if (PowerTable == null)
            return;

        Enabled = true;
        int num = 4;
        int idx = 0;
        _EntryControlList = new List<PowerEntryControl>();

        foreach (PowerEntry powerEntry in PowerTable.ListPowerEntries.Where(p => (int)p.Def != 0).ToList())
        {
            PowerEntryControl obj2 = new PowerEntryControl
            {
                PowerEntry = powerEntry,
                Left = 0,
                Top = num
            };

            if (idx < sectionTitles.Length)
            {
                obj2.UpdateSectionTitle(sectionTitles[idx]);
            }
            else
            {
                obj2.UpdateSectionTitle("");
            }

            Controls.Add(obj2);
            _EntryControlList.Add(obj2);

            num += obj2.Height + 8;
            idx++;
        }
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // UCPowerTable
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Name = "UCPowerTable";
            this.Size = new Size(416, 238);
            this.ResumeLayout(false);

    }
}
