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
    private GroupBox groupBox;

    static readonly decimal INCREMENT = 1000;
    private Label LabelMaxW;
    private Label LabelDefW;
    private Label LabelMinW;
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

    public void UpdateSectionTitle(string title)
    {
        if (String.IsNullOrEmpty(title))
            groupBox.Text = "Unknown";
        else
            groupBox.Text = title;
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

        LabelMinW.Text = String.Format($"{PowerEntry.Min  /1000} W");
        LabelDefW.Text = String.Format($"{PowerEntry.Def / 1000} W");
        LabelMaxW.Text = String.Format($"{PowerEntry.Max / 1000} W");

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
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblDef = new System.Windows.Forms.Label();
            this.lblMinPercent = new System.Windows.Forms.Label();
            this.numMin = new System.Windows.Forms.NumericUpDown();
            this.lblDefPercent = new System.Windows.Forms.Label();
            this.lblMaxPercent = new System.Windows.Forms.Label();
            this.numDef = new System.Windows.Forms.NumericUpDown();
            this.numMax = new System.Windows.Forms.NumericUpDown();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.LabelMinW = new System.Windows.Forms.Label();
            this.LabelDefW = new System.Windows.Forms.Label();
            this.LabelMaxW = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(8, 71);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(55, 13);
            this.lblMax.TabIndex = 36;
            this.lblMax.Text = "Max (mW)";
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(8, 21);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(52, 13);
            this.lblMin.TabIndex = 37;
            this.lblMin.Text = "Min (mW)";
            this.lblMin.Click += new System.EventHandler(this.LblMin_Click);
            // 
            // lblDef
            // 
            this.lblDef.AutoSize = true;
            this.lblDef.Location = new System.Drawing.Point(8, 46);
            this.lblDef.Name = "lblDef";
            this.lblDef.Size = new System.Drawing.Size(52, 13);
            this.lblDef.TabIndex = 38;
            this.lblDef.Text = "Def (mW)";
            // 
            // lblMinPercent
            // 
            this.lblMinPercent.Location = new System.Drawing.Point(67, 21);
            this.lblMinPercent.Name = "lblMinPercent";
            this.lblMinPercent.Size = new System.Drawing.Size(39, 13);
            this.lblMinPercent.TabIndex = 37;
            this.lblMinPercent.Text = "71%";
            this.lblMinPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numMin
            // 
            this.numMin.Location = new System.Drawing.Point(109, 19);
            this.numMin.Name = "numMin";
            this.numMin.Size = new System.Drawing.Size(85, 20);
            this.numMin.TabIndex = 0;
            this.numMin.ValueChanged += new System.EventHandler(this.Num_ValueChanged);
            // 
            // lblDefPercent
            // 
            this.lblDefPercent.Location = new System.Drawing.Point(67, 46);
            this.lblDefPercent.Name = "lblDefPercent";
            this.lblDefPercent.Size = new System.Drawing.Size(39, 13);
            this.lblDefPercent.TabIndex = 37;
            this.lblDefPercent.Text = "100%";
            this.lblDefPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMaxPercent
            // 
            this.lblMaxPercent.Location = new System.Drawing.Point(67, 71);
            this.lblMaxPercent.Name = "lblMaxPercent";
            this.lblMaxPercent.Size = new System.Drawing.Size(39, 13);
            this.lblMaxPercent.TabIndex = 37;
            this.lblMaxPercent.Text = "130%";
            this.lblMaxPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numDef
            // 
            this.numDef.Location = new System.Drawing.Point(109, 44);
            this.numDef.Name = "numDef";
            this.numDef.Size = new System.Drawing.Size(85, 20);
            this.numDef.TabIndex = 1;
            this.numDef.ValueChanged += new System.EventHandler(this.Num_ValueChanged);
            // 
            // numMax
            // 
            this.numMax.Location = new System.Drawing.Point(109, 69);
            this.numMax.Name = "numMax";
            this.numMax.Size = new System.Drawing.Size(85, 20);
            this.numMax.TabIndex = 2;
            this.numMax.ValueChanged += new System.EventHandler(this.Num_ValueChanged);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.LabelMaxW);
            this.groupBox.Controls.Add(this.LabelDefW);
            this.groupBox.Controls.Add(this.LabelMinW);
            this.groupBox.Controls.Add(this.numMin);
            this.groupBox.Controls.Add(this.lblDef);
            this.groupBox.Controls.Add(this.numMax);
            this.groupBox.Controls.Add(this.lblMin);
            this.groupBox.Controls.Add(this.numDef);
            this.groupBox.Controls.Add(this.lblMinPercent);
            this.groupBox.Controls.Add(this.lblDefPercent);
            this.groupBox.Controls.Add(this.lblMax);
            this.groupBox.Controls.Add(this.lblMaxPercent);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(270, 100);
            this.groupBox.TabIndex = 40;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "groupBox1";
            // 
            // LabelMinW
            // 
            this.LabelMinW.Location = new System.Drawing.Point(200, 21);
            this.LabelMinW.Name = "LabelMinW";
            this.LabelMinW.Size = new System.Drawing.Size(64, 13);
            this.LabelMinW.TabIndex = 39;
            this.LabelMinW.Text = "0 W";
            this.LabelMinW.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelDefW
            // 
            this.LabelDefW.Location = new System.Drawing.Point(200, 46);
            this.LabelDefW.Name = "LabelDefW";
            this.LabelDefW.Size = new System.Drawing.Size(64, 13);
            this.LabelDefW.TabIndex = 40;
            this.LabelDefW.Text = "0 W";
            this.LabelDefW.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelMaxW
            // 
            this.LabelMaxW.Location = new System.Drawing.Point(200, 71);
            this.LabelMaxW.Name = "LabelMaxW";
            this.LabelMaxW.Size = new System.Drawing.Size(64, 13);
            this.LabelMaxW.TabIndex = 41;
            this.LabelMaxW.Text = "0 W";
            this.LabelMaxW.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PowerEntryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "PowerEntryControl";
            this.Size = new System.Drawing.Size(336, 105);
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

    }
}
