using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class ClockSelectorControl : UserControl
{
    private bool updating;
    private IContainer components;
    private TextBox tbClock;
    private NumericUpDown numClockSelector;

    public new string Text
    {
        get
        {
            return tbClock.Text;
        }
        set
        {
            tbClock.Text = value;
            UpdateClockSelector();
        }
    }

    public bool StepAllowed
    {
        get
        {
            return numClockSelector.Enabled;
        }
    }

    public ClockSelectorControl()
    {
        InitializeComponent();
        InitClockSelector();
    }

    private void InitClockSelector()
    {
        int width = numClockSelector.Width;
        numClockSelector.Width = (int)Math.Floor((numClockSelector.Height + 16.0) / 2.0);
        numClockSelector.Left += width - numClockSelector.Width;
        tbClock.Width += width - numClockSelector.Width;
        numClockSelector.Maximum = CE00D.PE000.Count - 1;
        numClockSelector.Increment = new Decimal(1);
    }

    private void UpdateClockSelector()
    {
        updating = true;
        if (string.IsNullOrEmpty(tbClock.Text))
            numClockSelector.Value = new Decimal(0);
        ushort num1 = CE00D.E006(tbClock.Text);
        foreach (ushort num2 in CE00D.PE000)
        {
            if (num1 <= num2)
            {
                numClockSelector.Value = CE00D.PE000.IndexOf(num2);
                break;
            }
        }
        numClockSelector.Enabled = CE00D.PE000.IndexOf(num1) >= 0;
        updating = false;
    }

    private void numClockSelector_ValueChanged(object sender, EventArgs e)
    {
        if (updating)
            return;
        tbClock.Text = CE00D.E005(CE00D.PE000[(int)numClockSelector.Value]);
    }

    private void tbClock_TextChanged(object sender, EventArgs e)
    {
        UpdateClockSelector();
    }

    internal void AddStep()
    {
        if (!StepAllowed)
            return;
        ++numClockSelector.Value;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tbClock = new TextBox();
        numClockSelector = new NumericUpDown();
        ((ISupportInitialize)(numClockSelector)).BeginInit();
        SuspendLayout();
        // 
        // tbClock
        // 
        tbClock.Location = new Point(1, 1);
        tbClock.Name = "tbClock";
        tbClock.Size = new Size(66, 20);
        tbClock.TabIndex = 0;
        tbClock.TextChanged += new EventHandler(tbClock_TextChanged);
        // 
        // numClockSelector
        // 
        numClockSelector.Location = new Point(69, 1);
        numClockSelector.Name = "numClockSelector";
        numClockSelector.ReadOnly = true;
        numClockSelector.Size = new Size(18, 20);
        numClockSelector.TabIndex = 1;
        numClockSelector.TabStop = false;
        numClockSelector.ValueChanged += new EventHandler(numClockSelector_ValueChanged);
        // 
        // ClockSelectorControl
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        Controls.Add(numClockSelector);
        Controls.Add(tbClock);
        Name = "ClockSelectorControl";
        Size = new Size(90, 23);
        ((ISupportInitialize)(numClockSelector)).EndInit();
        ResumeLayout(false);
        PerformLayout();

    }
}
