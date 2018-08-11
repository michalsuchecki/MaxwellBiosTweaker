using MaxwellBiosTweaker.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class UCCabecalho : UserControl
{
    private RomHeader _RomHeader;
    private string _FileName;
    private byte _ImageChecksum;
    private byte _GeneratedChecksum;
    private IContainer components;
    private Label lblChkSum;
    private Label lChkSum;
    private Label lblFilename;
    private Label lFilename;
    private Label lblDate;
    private Label lDate;
    private Label lblSubVendor;
    private Label lSubVendor;
    private Label lblDevID;
    private Label lDevID;
    private Label lblBIOS;
    private Label lBIOS;
    private Label lblGPU;
    private Label lGPU;
    private Label lblName;
    private Label lName;
    private Label lImgBack;

    internal RomHeader RomHeader
    {
        get
        {
            return _RomHeader;
        }
        set
        {
            _RomHeader = value;
            UpdateDisplay();
        }
    }

    public string FileName
    {
        get
        {
            return _FileName;
        }
        set
        {
            _FileName = value;
            UpdateDisplay();
        }
    }

    public byte ImageChecksum
    {
        get
        {
            return _ImageChecksum;
        }
        set
        {
            _ImageChecksum = value;
            UpdateDisplay();
        }
    }

    public byte GeneratedChecksum
    {
        get
        {
            return _GeneratedChecksum;
        }
        set
        {
            _GeneratedChecksum = value;
            UpdateDisplay();
        }
    }

    public UCCabecalho()
    {
        InitializeComponent();
    }

    private string TranslateSubVendor(uint ID)
    {
        uint num = ID & ushort.MaxValue;
        if (num <= 5218U)
        {
            if (num <= 4221U)
            {
                if (num <= 4156U)
                {
                    if (num <= 4121U)
                    {
                        if ((int)num == 3601)
                            return "HP (0E11)";
                        if ((int)num == 4116)
                            return "IBM (1014)";
                        if ((int)num == 4121)
                            return "Elitegroup (1019)";
                    }
                    else
                    {
                        if ((int)num == 4133)
                            return "ACER (1025)";
                        if ((int)num == 4136)
                            return "DELL (1028)";
                        if ((int)num == 4156)
                            return "HP (103C)";
                    }
                }
                else if (num <= 4173U)
                {
                    if ((int)num == 4163)
                        return "ASUS (1043)";
                    if ((int)num == 4168)
                        return "ELSA (1048)";
                    if ((int)num == 4173)
                        return "SONY (104D)";
                }
                else
                {
                    if ((int)num == 4187)
                        return "Foxconn (105B)";
                    if ((int)num == 4203)
                        return "Apple (106B)";
                    if ((int)num == 4221)
                        return "Leadtek (107D)";
                }
            }
            else if (num <= 4354U)
            {
                if (num <= 4276U)
                {
                    if ((int)num == 4242)
                        return "Diamond (1092)";
                    if ((int)num == 4272)
                        return "Gainward (10B0)";
                    if ((int)num == 4276)
                        return "STB (10B4)";
                }
                else
                {
                    if ((int)num == 4303)
                        return "Fujitsu (10CF)";
                    if ((int)num == 4318)
                        return "NVIDIA (10DE)";
                    if ((int)num == 4354)
                        return "Creative (1102)";
                }
            }
            else if (num <= 5053U)
            {
                if ((int)num == 4473)
                    return "Toshiba (1179)";
                if ((int)num == 5020)
                    return "Quantum (139C)";
                if ((int)num == 5053)
                    return "SHARP (13BD)";
            }
            else
            {
                if ((int)num == 5197)
                    return "Samsung (144D)";
                if ((int)num == 5208)
                    return "Gigabyte (1458)";
                if ((int)num == 5218)
                    return "MSI (1462)";
            }
        }
        else if (num <= 6058U)
        {
            if (num <= 5464U)
            {
                if (num <= 5312U)
                {
                    if ((int)num == 5243)
                        return "ABit (147B)";
                    if ((int)num == 5295)
                        return "Guillemot (14AF)";
                    if ((int)num == 5312)
                        return "Compal (14C0)";
                }
                else
                {
                    if ((int)num == 5445)
                        return "Visiontek (1545)";
                    if ((int)num == 5460)
                        return "Prolink (1554)";
                    if ((int)num == 5464)
                        return "Schenker (1558)";
                }
            }
            else if (num <= 5565U)
            {
                if ((int)num == 5477)
                    return "Biostar (1565)";
                if ((int)num == 5481)
                    return "Palit (1569)";
                if ((int)num == 5565)
                    return "DFI (15BD)";
            }
            else
            {
                switch (num)
                {
                    case 5761U:
                        return "Herkules (1681)";
                    case 5762U:
                        return "XFX (1682)";
                    case 5963U:
                        return "Sapphire (174B)";
                    case 6058U:
                        return "Lenovo (17AA)";
                }
            }
        }
        else if (num <= 6860U)
        {
            if (num <= 6510U)
            {
                if ((int)num == 6080)
                    return "Wistron (17C0)";
                if ((int)num == 6217)
                    return "ASRock (1849)";
                if ((int)num == 6510)
                    return "PNY (196E)";
            }
            else
            {
                if ((int)num == 6618)
                    return "Zotac (19DA)";
                if ((int)num == 6641)
                    return "BFG (19F1)";
                if ((int)num == 6860)
                    return "Point of View (1ACC)";
            }
        }
        else if (num <= 14402U)
        {
            if ((int)num == 6931)
                return "Jaton (1B13)";
            if ((int)num == 6988)
                return "KFA\x00B2 (1B4C)";
            if ((int)num == 14402)
                return "EVGA (3842)";
        }
        else
        {
            if ((int)num == 19539)
                return "SBS (4C53)";
            if ((int)num == 29559)
                return "Colorful (7377)";
            if ((int)num == 41120)
                return "AOpen (A0A0)";
        }
        return "Unknown (" + (ID & ushort.MaxValue).ToString("X4") + ")";
    }

    private void UpdateDisplay()
    {
        if (RomHeader == null)
            return;
        lblName.Text = RomHeader.GetNameDescription.Replace("\r\n", " ");
        lblGPU.Text = RomHeader.GetBoardDescription;
        lblBIOS.Text = RomHeader.GetVersionDescription;
        lblDevID.Text = string.Format("{0:X4} - {1:X4}", RomHeader.E00B.PE001, RomHeader.E00B.PE002);
        lblSubVendor.Text = TranslateSubVendor(RomHeader.E003);
        lblDate.Text = RomHeader.GetReleaseDate;
        lblChkSum.Text = string.Format("{0:X2} - [{1:X2}]", ImageChecksum, GeneratedChecksum);
        if (ImageChecksum == GeneratedChecksum)
            lblChkSum.BackColor = Color.LightGreen;
        else
            lblChkSum.BackColor = Color.LightCoral;
        lblFilename.Text = FileName;
    }

    public void ResetDisplay(bool unsupported = false)
    {
        lblName.Text = unsupported ? "Unsupported Device" : "";
        lblGPU.Text = "";
        lblBIOS.Text = "";
        lblDevID.Text = "";
        lblSubVendor.Text = "";
        lblDate.Text = "";
        lblFilename.Text = "";
        lblChkSum.Text = "";
        lblChkSum.BackColor = SystemColors.Control;
    }

    private void lImgBack_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.DrawImage(Resources.GetNVidiaLogo, -1, -2, lImgBack.Width, lImgBack.Height);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            lblChkSum = new Label();
            lChkSum = new Label();
            lblFilename = new Label();
            lFilename = new Label();
            lblDate = new Label();
            lDate = new Label();
            lblSubVendor = new Label();
            lSubVendor = new Label();
            lblDevID = new Label();
            lDevID = new Label();
            lblBIOS = new Label();
            lBIOS = new Label();
            lblGPU = new Label();
            lGPU = new Label();
            lblName = new Label();
            lName = new Label();
            lImgBack = new Label();
            SuspendLayout();
            // 
            // lblChkSum
            // 
            lblChkSum.BackColor = SystemColors.Control;
            lblChkSum.BorderStyle = BorderStyle.Fixed3D;
            lblChkSum.Location = new Point(63, 117);
            lblChkSum.Name = "lblChkSum";
            lblChkSum.Size = new Size(86, 20);
            lblChkSum.TabIndex = 46;
            lblChkSum.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lChkSum
            // 
            lChkSum.Location = new Point(3, 121);
            lChkSum.Name = "lChkSum";
            lChkSum.Size = new Size(57, 13);
            lChkSum.TabIndex = 45;
            lChkSum.Text = "Checksum";
            lChkSum.TextAlign = ContentAlignment.TopRight;
            // 
            // lblFilename
            // 
            lblFilename.BorderStyle = BorderStyle.Fixed3D;
            lblFilename.Location = new Point(203, 117);
            lblFilename.Name = "lblFilename";
            lblFilename.Size = new Size(242, 20);
            lblFilename.TabIndex = 44;
            lblFilename.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lFilename
            // 
            lFilename.Location = new Point(150, 121);
            lFilename.Name = "lFilename";
            lFilename.Size = new Size(52, 13);
            lFilename.TabIndex = 43;
            lFilename.Text = "Filename";
            lFilename.TextAlign = ContentAlignment.TopRight;
            // 
            // lblDate
            // 
            lblDate.BorderStyle = BorderStyle.Fixed3D;
            lblDate.Location = new Point(63, 59);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(86, 20);
            lblDate.TabIndex = 42;
            lblDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lDate
            // 
            lDate.Location = new Point(30, 63);
            lDate.Name = "lDate";
            lDate.Size = new Size(30, 13);
            lDate.TabIndex = 41;
            lDate.Text = "Date";
            lDate.TextAlign = ContentAlignment.TopRight;
            // 
            // lblSubVendor
            // 
            lblSubVendor.BorderStyle = BorderStyle.Fixed3D;
            lblSubVendor.Location = new Point(203, 87);
            lblSubVendor.Name = "lblSubVendor";
            lblSubVendor.Size = new Size(136, 20);
            lblSubVendor.TabIndex = 40;
            lblSubVendor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lSubVendor
            // 
            lSubVendor.Location = new Point(150, 91);
            lSubVendor.Name = "lSubVendor";
            lSubVendor.Size = new Size(52, 13);
            lSubVendor.TabIndex = 39;
            lSubVendor.Text = "Vendor";
            lSubVendor.TextAlign = ContentAlignment.TopRight;
            // 
            // lblDevID
            // 
            lblDevID.BorderStyle = BorderStyle.Fixed3D;
            lblDevID.Location = new Point(63, 87);
            lblDevID.Name = "lblDevID";
            lblDevID.Size = new Size(86, 20);
            lblDevID.TabIndex = 38;
            lblDevID.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lDevID
            // 
            lDevID.Location = new Point(5, 91);
            lDevID.Name = "lDevID";
            lDevID.Size = new Size(55, 13);
            lDevID.TabIndex = 37;
            lDevID.Text = "Device ID";
            lDevID.TextAlign = ContentAlignment.TopRight;
            // 
            // lblBIOS
            // 
            lblBIOS.BorderStyle = BorderStyle.Fixed3D;
            lblBIOS.Location = new Point(203, 59);
            lblBIOS.Name = "lblBIOS";
            lblBIOS.Size = new Size(136, 20);
            lblBIOS.TabIndex = 36;
            lblBIOS.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lBIOS
            // 
            lBIOS.Location = new Point(150, 63);
            lBIOS.Name = "lBIOS";
            lBIOS.Size = new Size(52, 13);
            lBIOS.TabIndex = 35;
            lBIOS.Text = "Version";
            lBIOS.TextAlign = ContentAlignment.TopRight;
            // 
            // lblGPU
            // 
            lblGPU.BorderStyle = BorderStyle.Fixed3D;
            lblGPU.Location = new Point(63, 31);
            lblGPU.Name = "lblGPU";
            lblGPU.Size = new Size(276, 20);
            lblGPU.TabIndex = 34;
            lblGPU.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lGPU
            // 
            lGPU.Location = new Point(25, 35);
            lGPU.Name = "lGPU";
            lGPU.Size = new Size(35, 13);
            lGPU.TabIndex = 33;
            lGPU.Text = "Board";
            lGPU.TextAlign = ContentAlignment.TopRight;
            // 
            // lblName
            // 
            lblName.BorderStyle = BorderStyle.Fixed3D;
            lblName.Location = new Point(63, 3);
            lblName.Name = "lblName";
            lblName.Size = new Size(382, 20);
            lblName.TabIndex = 31;
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lName
            // 
            lName.Location = new Point(25, 7);
            lName.Name = "lName";
            lName.Size = new Size(35, 13);
            lName.TabIndex = 30;
            lName.Text = "Name";
            lName.TextAlign = ContentAlignment.TopRight;
            // 
            // lImgBack
            // 
            lImgBack.AllowDrop = true;
            lImgBack.BackColor = SystemColors.Control;
            lImgBack.BorderStyle = BorderStyle.Fixed3D;
            lImgBack.Location = new Point(345, 31);
            lImgBack.Name = "lImgBack";
            lImgBack.Size = new Size(100, 77);
            lImgBack.TabIndex = 32;
            lImgBack.TextAlign = ContentAlignment.MiddleCenter;
            lImgBack.Paint += new PaintEventHandler(lImgBack_Paint);
            // 
            // UCCabecalho
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblChkSum);
            Controls.Add(lChkSum);
            Controls.Add(lblFilename);
            Controls.Add(lFilename);
            Controls.Add(lblDate);
            Controls.Add(lDate);
            Controls.Add(lblSubVendor);
            Controls.Add(lSubVendor);
            Controls.Add(lblDevID);
            Controls.Add(lDevID);
            Controls.Add(lblBIOS);
            Controls.Add(lBIOS);
            Controls.Add(lblGPU);
            Controls.Add(lGPU);
            Controls.Add(lblName);
            Controls.Add(lName);
            Controls.Add(lImgBack);
            Name = "UCCabecalho";
            Size = new Size(450, 143);
            ResumeLayout(false);

    }
}
