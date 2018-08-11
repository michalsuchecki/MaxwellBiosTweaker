using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class PowerEntryControl : UserControl, ComponenteDaTela
{
    private PowerEntry _PowerEntry;
    private Label lblMax;
    private Label lblMin;
    private Label lblDef;
    private Label lblMinPercent;
    private NumericUpDown numMin;
    private Label lblDefPercent;
    private Label lblMaxPercent;
    private NumericUpDown numDef;
    private NumericUpDown numMax;

    static readonly decimal INCREMENT = 1000;
    static readonly decimal MAXIMUM = 24000000; //9000000

    internal PowerEntry PowerEntry
    {
        get
        {
            return _PowerEntry;
        }
        set
        {
            _PowerEntry = value;
            UpdateData();
        }
    }

    public PowerEntryControl()
    {
        InitializeComponent();

        numMin.Increment = INCREMENT;
        numMin.Maximum = MAXIMUM;

        numDef.Increment = INCREMENT;
        numDef.Maximum = MAXIMUM;

        numMax.Increment = INCREMENT;
        numMax.Maximum = MAXIMUM;
    }

    public void ApplyChanges()
    {
        if (PowerEntry == null) return;

        PowerEntry.Min = Convert.ToUInt32(numMin.Value);
        PowerEntry.Def = Convert.ToUInt32(numDef.Value);
        PowerEntry.Max = Convert.ToUInt32(numMax.Value);
    }

    public void Reset()
    {
        PowerEntry = null;
        InternalReset();
    }

    private void InternalReset()
    {
        numMin.Value = new Decimal(0);
        numDef.Value = new Decimal(0);
        numMax.Value = new Decimal(0);
        lblMinPercent.Text = "";
        lblDefPercent.Text = "";
        lblMaxPercent.Text = "";
        Enabled = false;
    }

    private void UpdateData()
    {
        InternalReset();

        if (PowerEntry == null) return;

        Enabled = true;
        numMin.Value = PowerEntry.Min;
        numDef.Value = PowerEntry.Def;
        numMax.Value = PowerEntry.Max;

        CalculatePercentages();
    }

    private void CalculatePercentages()
    {
        int minPercent = 0;
        int maxPercent = 0;

        if (numMin.Value > new Decimal(0) && numDef.Value > new Decimal(0))
            minPercent = (int)Math.Round((double)numMin.Value / (double)numDef.Value * 100.0);

        if (numMax.Value > new Decimal(0) && numDef.Value > new Decimal(0))
            maxPercent = (int)Math.Round((double)numMax.Value / (double)numDef.Value * 100.0);

        lblMinPercent.Text = string.Format("{0}%", minPercent);
        lblDefPercent.Text = "100%";
        lblMaxPercent.Text = string.Format("{0}%", maxPercent);
    }

    private void Num_ValueChanged(object sender, EventArgs e)
    {
        CalculatePercentages();
    }

    private void LblMin_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblMax = new Label();
        lblMin = new Label();
        lblDef = new Label();
        lblMinPercent = new Label();
        numMin = new NumericUpDown();
        lblDefPercent = new Label();
        lblMaxPercent = new Label();
        numDef = new NumericUpDown();
        numMax = new NumericUpDown();
        ((ISupportInitialize)(numMin)).BeginInit();
        ((ISupportInitialize)(numDef)).BeginInit();
        ((ISupportInitialize)(numMax)).BeginInit();
        SuspendLayout();
        // 
        // lblMax
        // 
        lblMax.AutoSize = true;
        lblMax.Location = new Point(3, 55);
        lblMax.Name = "lblMax";
        lblMax.Size = new Size(55, 13);
        lblMax.TabIndex = 36;
        lblMax.Text = "Max (mW)";
        // 
        // lblMin
        // 
        lblMin.AutoSize = true;
        lblMin.Location = new Point(3, 5);
        lblMin.Name = "lblMin";
        lblMin.Size = new Size(52, 13);
        lblMin.TabIndex = 37;
        lblMin.Text = "Min (mW)";
        lblMin.Click += new EventHandler(LblMin_Click);
        // 
        // lblDef
        // 
        lblDef.AutoSize = true;
        lblDef.Location = new Point(3, 30);
        lblDef.Name = "lblDef";
        lblDef.Size = new Size(52, 13);
        lblDef.TabIndex = 38;
        lblDef.Text = "Def (mW)";
        // 
        // lblMinPercent
        // 
        lblMinPercent.Location = new Point(62, 5);
        lblMinPercent.Name = "lblMinPercent";
        lblMinPercent.Size = new Size(39, 13);
        lblMinPercent.TabIndex = 37;
        lblMinPercent.Text = "71%";
        lblMinPercent.TextAlign = ContentAlignment.TopRight;
        // 
        // numMin
        // 
        numMin.Location = new Point(104, 3);
        numMin.Name = "numMin";
        numMin.Size = new Size(85, 20);
        numMin.TabIndex = 0;
        numMin.ValueChanged += new EventHandler(Num_ValueChanged);
        // 
        // lblDefPercent
        // 
        lblDefPercent.Location = new Point(62, 30);
        lblDefPercent.Name = "lblDefPercent";
        lblDefPercent.Size = new Size(39, 13);
        lblDefPercent.TabIndex = 37;
        lblDefPercent.Text = "100%";
        lblDefPercent.TextAlign = ContentAlignment.TopRight;
        // 
        // lblMaxPercent
        // 
        lblMaxPercent.Location = new Point(62, 55);
        lblMaxPercent.Name = "lblMaxPercent";
        lblMaxPercent.Size = new Size(39, 13);
        lblMaxPercent.TabIndex = 37;
        lblMaxPercent.Text = "130%";
        lblMaxPercent.TextAlign = ContentAlignment.TopRight;
        // 
        // numDef
        // 
        numDef.Location = new Point(104, 28);
        numDef.Name = "numDef";
        numDef.Size = new Size(85, 20);
        numDef.TabIndex = 1;
        numDef.ValueChanged += new EventHandler(Num_ValueChanged);
        // 
        // numMax
        // 
        numMax.Location = new Point(104, 53);
        numMax.Name = "numMax";
        numMax.Size = new Size(85, 20);
        numMax.TabIndex = 2;
        numMax.ValueChanged += new EventHandler(Num_ValueChanged);
        // 
        // PowerEntryControl
        // 
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(numMax);
        Controls.Add(numDef);
        Controls.Add(numMin);
        Controls.Add(lblMax);
        Controls.Add(lblMaxPercent);
        Controls.Add(lblDefPercent);
        Controls.Add(lblMinPercent);
        Controls.Add(lblMin);
        Controls.Add(lblDef);
        Name = "PowerEntryControl";
        Size = new Size(193, 77);
        ((ISupportInitialize)(numMin)).EndInit();
        ((ISupportInitialize)(numDef)).EndInit();
        ((ISupportInitialize)(numMax)).EndInit();
        ResumeLayout(false);
        PerformLayout();

    }
}
