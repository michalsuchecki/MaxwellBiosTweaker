using MaxwellBiosTweaker.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MaxwellBiosTweaker
{
    public class BiosTweakerForm : Form
    {
        [StructLayout(LayoutKind.Explicit, Size = 5, Pack = 1)]
        private struct Estrutura
        {
        }

        private NvFlashHelper nvFlashHelper = new NvFlashHelper();
        private readonly List<ComponenteDaTela> componentesDaTela;
        private Bios Bios;
        private GroupBox gbInfo;
        private TabControl tabControl;
        private TabPage tpClockStates;
        private Button btnOpen;
        private Button btnSave;
        private Button btnSaveAs;
        private TabPage tpBoostStates;
        private TabPage tpBoostTable;
        private TabPage tpCommon;
        private UCFanRange frcFanRange;
        private UCPerfTableControl ptcPerfTable;
        private Panel panel3;
        private GroupBox groupBox1;
        private GroupBox gbFanControl;
        private Panel panel2;
        private GroupBox groupBox2;
        private Panel panel4;
        private GroupBox groupBox4;
        private Panel panel5;
        private GroupBox groupBox5;
        private UCBoostClocks btcBoostClocks;
        private UCBoostConfigControl boostConfigControl;
        private UCBoostLimitHelperControl blhBoostLimiterHelperControl;
        private Button btnRead;
        private Button btnFlash;
        private UCMemoryClock mchMemoryClock;
        private Button btnGpuCLockOffsetHelper;
        private Button btnVoltToClock;
        private TabPage tpPowerTable;
        private Panel panel1;
        private GroupBox groupBox6;
        private UCPowerTable ptcPowerTable;
        private TabPage tpVoltageTable;
        private Panel panel6;
        private GroupBox groupBox3;
        private UCVoltageTable vtcVoltageTable;
        private Button btBITEntires;
        private Button btCompare;
        private UCCabecalho hcHeader;
        private UCBaseBoostControl bbcBaseBoost;
        private UCTempTargets ttcTempTargets;

        public BiosTweakerForm()
        {
            InitializeComponent();
            componentesDaTela = ComponentesDaTela();
            Application.Idle += HabilitaDesabilitaBtnGpuCLockOffsetHelper;
        }

        private void HabilitaDesabilitaBtnGpuCLockOffsetHelper(object param0, EventArgs param1)
        {
            btnGpuCLockOffsetHelper.Enabled = bbcBaseBoost.StepAllowed && btcBoostClocks.StepAllowed;
        }

        private List<ComponenteDaTela> ComponentesDaTela()
        {
            return new List<ComponenteDaTela>()
            {
                frcFanRange,
                bbcBaseBoost,
                ptcPerfTable,
                btcBoostClocks,
                boostConfigControl,
                ptcPowerTable,
                vtcVoltageTable,
                ttcTempTargets
            };
        }

        private void LimparTela()
        {
            tabControl.TabPages.Clear();
            hcHeader.ResetDisplay(false);
            foreach (ComponenteDaTela obj in componentesDaTela)
                obj.Reset();
            tabControl.Enabled = false;
        }

        private void AbrirBios(string path)
        {
            LimparTela();
            Bios = new Bios(path);

            if (Bios.PE000)
            {
                LinhaBios linhaBios = Bios.linhasDaBios[0];
                hcHeader.RomHeader = linhaBios.RomHeader;
                hcHeader.FileName = new FileInfo(path).Name;
                hcHeader.ImageChecksum = linhaBios.ImageChecksum;
                hcHeader.GeneratedChecksum = linhaBios.GenerateChecksum();

                if (linhaBios.BoostTable != null)
                    CE00D.E001(linhaBios.BoostTable.BoostClocks.Select(param0_2 => param0_2.Frequency).ToList());

                tabControl.TabPages.Add(tpCommon);
                bbcBaseBoost.PE000 = linhaBios.FE006;

                if (Bios.PE003)
                {
                    vtcVoltageTable.BoostTable = linhaBios.BoostTable;
                    vtcVoltageTable.PerfTable = linhaBios.PerfTable;
                    vtcVoltageTable.VoltageTable = linhaBios.VoltageTable;
                    tabControl.TabPages.Add(tpVoltageTable);
                }

                if (Bios.PossuiFanSettings)
                {
                    frcFanRange.FanSettings = linhaBios.FanSettings;
                    frcFanRange.FanSettings2 = linhaBios.FanSettings2;
                }

                if (Bios.PossuiPowerTable)
                {
                    ptcPowerTable.PowerTable = linhaBios.PowerTable;
                    tabControl.TabPages.Add(tpPowerTable);
                }

                tabControl.TabPages.Add(tpBoostTable);

                if (Bios.PossuiBoostProfile)
                {
                    boostConfigControl.BoostProfile = linhaBios.BoostProfile;
                    blhBoostLimiterHelperControl.BoostConfigControl = boostConfigControl;
                    tabControl.TabPages.Add(tpBoostStates);
                }

                ptcPerfTable.PerfTable = linhaBios.PerfTable;
                mchMemoryClock.PerfTableControl = ptcPerfTable;
                ttcTempTargets.TempTargets = linhaBios.TempTargets;
                tabControl.TabPages.Add(tpClockStates);
                btcBoostClocks.BoostTable = linhaBios.BoostTable;
                tabControl.Enabled = true;
            }
            else
                hcHeader.ResetDisplay(true);
        }

        private void SalvarArquivoBios(string param0)
        {
            if (Bios == null || !Bios.PE000)
                return;

            Bios.EscreverBios(param0);
        }

        private void AplicarAlteracoes()
        {
            try
            {
                if (Bios == null || !Bios.PE000)
                    return;
                foreach (ComponenteDaTela obj in componentesDaTela)
                    obj.ApplyChanges();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "Error - Not Applied");
            }
        }

        private void Load_Form(object param0, EventArgs param1)
        {
            LimparTela();
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Text = string.Format("Maxwell II BIOS Tweaker v{0}.{1}{2}", version.Major, version.Minor, version.Build);
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            if (commandLineArgs.Length > 1 && (commandLineArgs[1].ToLower().EndsWith(".bin") || commandLineArgs[1].ToLower().EndsWith(".rom")))
                AbrirBios(commandLineArgs[1]);
        }

        private void BtnOpen_Click(object param0, EventArgs param1)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = "*.rom",
                Filter = "BIOS Files|*.rom;*.bin"
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            AbrirBios(openFileDialog.FileName);
        }

        private void BtnSave_Click(object param0, EventArgs param1)
        {
            if (Bios == null || !Bios.PE000)
                return;
            AplicarAlteracoes();
            SalvarArquivoBios(Bios.caminhoArquivo);
            AbrirBios(Bios.caminhoArquivo);
        }

        private void BtnSaveAs_Click(object param0, EventArgs param1)
        {
            if (Bios == null || !Bios.PE000)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "*.rom",
                Filter = "BIOS ROM|*.rom"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            AplicarAlteracoes();
            SalvarArquivoBios(saveFileDialog.FileName);
            AbrirBios(saveFileDialog.FileName);
        }

        private void DragDrop_Event(object param0, DragEventArgs param1)
        {
            string[] strArray = (string[])param1.Data.GetData(DataFormats.FileDrop);
            if (strArray.Length != 1)
                return;
            AbrirBios(strArray[0]);
        }

        private void DragEnter_Event(object param0, DragEventArgs param1)
        {
            if (param1.Data.GetDataPresent(DataFormats.FileDrop))
                param1.Effect = DragDropEffects.Copy;
            else
                param1.Effect = DragDropEffects.None;
        }

        private void BtnFlash_Click(object param0, EventArgs param1)
        {
            if (Bios == null || !Bios.PE000)
                return;
            AplicarAlteracoes();
            SalvarArquivoBios(Bios.caminhoArquivo);
            nvFlashHelper.SubirBiosParaGPU(Bios.caminhoArquivo);
        }

        private void BtnRead_Click(object param0, EventArgs param1)
        {
            AbrirBios(nvFlashHelper.BaixarBiosDaGPU());
        }

        private void BtnGpuCLockOffsetHelper_Click(object param0, EventArgs param1)
        {
            if (!bbcBaseBoost.StepAllowed || !btcBoostClocks.StepAllowed)
                return;
            bbcBaseBoost.E006();
            btcBoostClocks.AddStep();
        }

        private void BtnVoltToClock_Click(object param0, EventArgs param1)
        {
            if (Bios == null || !Bios.PE000)
                return;
            
            LinhaBios obj = Bios.linhasDaBios[0];
            
            StringBuilder stringBuilder = new StringBuilder();

            for (int index = 0; index < obj.BoostTable.BoostClocks.Count; ++index)
                stringBuilder.AppendLine(ME010(index));

            int num = (int)MessageBox.Show(stringBuilder.ToString());
        }

        private string ME010(int param0)
        {
            //TODO: COMENTEI TUDO MÉTODO É USADO EM UM LUGAR AONDE O MÉTODO QUE USA ELE NÃO É USADO

            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            //frmMBT.Estrutura obj1 = new frmMBT.Estrutura();
            var obj2 = Bios.linhasDaBios[0];
            
            // ISSUE: reference to a compiler-generated field
            var E000 = obj2.BoostTable.BoostClocks[param0];
            
            // ISSUE: reference to a compiler-generated field
            var obj3 = obj2.VoltageTable.ListaVoltagens[E000.Index];
            
            // ISSUE: reference to a compiler-generated method
            Voltage obj4 = obj2.PerfTable.Voltages.Where(X).FirstOrDefault();
            
            string str = "";
            if (obj4 != null)
                str = string.Format(" => P{0:00}", obj4.Caption);
            
            // ISSUE: reference to a compiler-generated field
            return string.Format("[{0}] {1:0000.0}MHz @ [{2}] {3}mV - {4}mV {5}", (object)param0.ToString("00"), (object)(E000.Frequency / 2f).ToString("0000.0"), (object)E000.Index.ToString("00"), (object)(obj3.From / 1000f).ToString("0000.0000"), (object)(obj3.To / 1000f).ToString("0000.0000"), (object)str);
            
            //return "";
        }

        private bool X(Voltage arg)
        {
            return true;
        }
        
        private void HcHeader_DoubleClick(object param0, EventArgs param1)
        {
            int num = (int)MessageBox.Show("Header");
        }

        private void KeyDown_Event(object param0, KeyEventArgs param1)
        {
        }

        private void BtBITEntires_Click(object param0, EventArgs param1)
        {

        }

        private void BtCompare_Click(object param0, EventArgs param1)
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            gbInfo = new GroupBox();
            btCompare = new Button();
            btBITEntires = new Button();
            btnVoltToClock = new Button();
            tabControl = new TabControl();
            tpBoostTable = new TabPage();
            panel2 = new Panel();
            groupBox2 = new GroupBox();
            tpClockStates = new TabPage();
            panel4 = new Panel();
            groupBox4 = new GroupBox();
            tpBoostStates = new TabPage();
            panel5 = new Panel();
            groupBox5 = new GroupBox();
            tpCommon = new TabPage();
            panel3 = new Panel();
            groupBox1 = new GroupBox();
            gbFanControl = new GroupBox();
            btnGpuCLockOffsetHelper = new Button();
            tpPowerTable = new TabPage();
            panel1 = new Panel();
            groupBox6 = new GroupBox();
            tpVoltageTable = new TabPage();
            panel6 = new Panel();
            groupBox3 = new GroupBox();
            btnOpen = new Button();
            btnSave = new Button();
            btnSaveAs = new Button();
            btnRead = new Button();
            btnFlash = new Button();
            btcBoostClocks = new UCBoostClocks();
            ptcPerfTable = new UCPerfTableControl();
            boostConfigControl = new UCBoostConfigControl();
            frcFanRange = new UCFanRange();
            bbcBaseBoost = new MaxwellBiosTweaker.Controls.UCBaseBoostControl();
            ttcTempTargets = new UCTempTargets();
            mchMemoryClock = new UCMemoryClock();
            blhBoostLimiterHelperControl = new UCBoostLimitHelperControl();
            ptcPowerTable = new UCPowerTable();
            vtcVoltageTable = new UCVoltageTable();
            hcHeader = new UCCabecalho();
            gbInfo.SuspendLayout();
            tabControl.SuspendLayout();
            tpBoostTable.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            tpClockStates.SuspendLayout();
            panel4.SuspendLayout();
            groupBox4.SuspendLayout();
            tpBoostStates.SuspendLayout();
            panel5.SuspendLayout();
            groupBox5.SuspendLayout();
            tpCommon.SuspendLayout();
            panel3.SuspendLayout();
            groupBox1.SuspendLayout();
            gbFanControl.SuspendLayout();
            tpPowerTable.SuspendLayout();
            panel1.SuspendLayout();
            groupBox6.SuspendLayout();
            tpVoltageTable.SuspendLayout();
            panel6.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // gbInfo
            // 
            gbInfo.Anchor = (AnchorStyles.Top | AnchorStyles.Left) 
                                 | AnchorStyles.Right;
            gbInfo.Controls.Add(hcHeader);
            gbInfo.FlatStyle = FlatStyle.Flat;
            gbInfo.Location = new Point(9, 1);
            gbInfo.Name = "gbInfo";
            gbInfo.Size = new Size(466, 161);
            gbInfo.TabIndex = 8;
            gbInfo.TabStop = false;
            // 
            // btCompare
            // 
            btCompare.Location = new Point(181, 563);
            btCompare.Name = "btCompare";
            btCompare.Size = new Size(87, 23);
            btCompare.TabIndex = 28;
            btCompare.Text = "Compare";
            btCompare.UseVisualStyleBackColor = true;
            btCompare.Click += BtCompare_Click;
            // 
            // btBITEntires
            // 
            btBITEntires.Location = new Point(370, 563);
            btBITEntires.Name = "btBITEntires";
            btBITEntires.Size = new Size(104, 24);
            btBITEntires.TabIndex = 27;
            btBITEntires.Text = "BIT Entires";
            btBITEntires.UseVisualStyleBackColor = true;
            btBITEntires.Click += BtBITEntires_Click;
            // 
            // btnVoltToClock
            // 
            btnVoltToClock.Location = new Point(270, 563);
            btnVoltToClock.Name = "btnVoltToClock";
            btnVoltToClock.Size = new Size(95, 24);
            btnVoltToClock.TabIndex = 26;
            btnVoltToClock.Text = "VoltToClock";
            btnVoltToClock.UseVisualStyleBackColor = true;
            btnVoltToClock.Click += BtnVoltToClock_Click;
            // 
            // tabControl
            // 
            tabControl.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) 
                                      | AnchorStyles.Left) 
                                     | AnchorStyles.Right;
            tabControl.Controls.Add(tpBoostTable);
            tabControl.Controls.Add(tpClockStates);
            tabControl.Controls.Add(tpBoostStates);
            tabControl.Controls.Add(tpCommon);
            tabControl.Controls.Add(tpPowerTable);
            tabControl.Controls.Add(tpVoltageTable);
            tabControl.Enabled = false;
            tabControl.Location = new Point(9, 169);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(470, 392);
            tabControl.TabIndex = 9;
            // 
            // tpBoostTable
            // 
            tpBoostTable.Controls.Add(panel2);
            tpBoostTable.Location = new Point(4, 22);
            tpBoostTable.Name = "tpBoostTable";
            tpBoostTable.Padding = new Padding(3);
            tpBoostTable.Size = new Size(462, 366);
            tpBoostTable.TabIndex = 6;
            tpBoostTable.Text = "Boost Table";
            tpBoostTable.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) 
                                  | AnchorStyles.Left) 
                                 | AnchorStyles.Right;
            panel2.BackColor = SystemColors.Control;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(groupBox2);
            panel2.Location = new Point(1, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(460, 363);
            panel2.TabIndex = 25;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btcBoostClocks);
            groupBox2.Location = new Point(14, 13);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(429, 343);
            groupBox2.TabIndex = 39;
            groupBox2.TabStop = false;
            groupBox2.Text = "Boost Clocks";
            // 
            // tpClockStates
            // 
            tpClockStates.Controls.Add(panel4);
            tpClockStates.Location = new Point(4, 22);
            tpClockStates.Name = "tpClockStates";
            tpClockStates.Padding = new Padding(3);
            tpClockStates.Size = new Size(462, 366);
            tpClockStates.TabIndex = 0;
            tpClockStates.Text = "Clock States";
            tpClockStates.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) 
                                  | AnchorStyles.Left) 
                                 | AnchorStyles.Right;
            panel4.BackColor = SystemColors.Control;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(groupBox4);
            panel4.Location = new Point(1, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(460, 368);
            panel4.TabIndex = 25;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ptcPerfTable);
            groupBox4.Location = new Point(14, 13);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(429, 343);
            groupBox4.TabIndex = 39;
            groupBox4.TabStop = false;
            groupBox4.Text = "Clock States";
            // 
            // tpBoostStates
            // 
            tpBoostStates.Controls.Add(panel5);
            tpBoostStates.Location = new Point(4, 22);
            tpBoostStates.Name = "tpBoostStates";
            tpBoostStates.Padding = new Padding(3);
            tpBoostStates.Size = new Size(462, 366);
            tpBoostStates.TabIndex = 4;
            tpBoostStates.Tag = "";
            tpBoostStates.Text = "Boost States";
            tpBoostStates.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            panel5.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) 
                                  | AnchorStyles.Left) 
                                 | AnchorStyles.Right;
            panel5.BackColor = SystemColors.Control;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(groupBox5);
            panel5.Location = new Point(1, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(460, 368);
            panel5.TabIndex = 26;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(boostConfigControl);
            groupBox5.Location = new Point(14, 13);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(429, 346);
            groupBox5.TabIndex = 39;
            groupBox5.TabStop = false;
            groupBox5.Text = "Boost States";
            // 
            // tpCommon
            // 
            tpCommon.Controls.Add(panel3);
            tpCommon.Location = new Point(4, 22);
            tpCommon.Name = "tpCommon";
            tpCommon.Padding = new Padding(3);
            tpCommon.Size = new Size(462, 366);
            tpCommon.TabIndex = 7;
            tpCommon.Text = "Common";
            tpCommon.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) 
                                  | AnchorStyles.Left) 
                                 | AnchorStyles.Right;
            panel3.BackColor = SystemColors.Control;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(groupBox1);
            panel3.Controls.Add(gbFanControl);
            panel3.Location = new Point(1, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(460, 361);
            panel3.TabIndex = 24;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(frcFanRange);
            groupBox1.Location = new Point(14, 202);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(429, 148);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Fan Control (Experimental)";
            // 
            // gbFanControl
            // 
            gbFanControl.Controls.Add(bbcBaseBoost);
            gbFanControl.Controls.Add(ttcTempTargets);
            gbFanControl.Controls.Add(btnGpuCLockOffsetHelper);
            gbFanControl.Controls.Add(mchMemoryClock);
            gbFanControl.Controls.Add(blhBoostLimiterHelperControl);
            gbFanControl.Location = new Point(14, 13);
            gbFanControl.Name = "gbFanControl";
            gbFanControl.Size = new Size(429, 182);
            gbFanControl.TabIndex = 0;
            gbFanControl.TabStop = false;
            gbFanControl.Text = "Basic Clock Settings";
            // 
            // btnGpuCLockOffsetHelper
            // 
            btnGpuCLockOffsetHelper.Location = new Point(225, 155);
            btnGpuCLockOffsetHelper.Name = "btnGpuCLockOffsetHelper";
            btnGpuCLockOffsetHelper.Size = new Size(187, 21);
            btnGpuCLockOffsetHelper.TabIndex = 3;
            btnGpuCLockOffsetHelper.Text = "GPU Clock Offset + 13MHz";
            btnGpuCLockOffsetHelper.UseVisualStyleBackColor = true;
            btnGpuCLockOffsetHelper.Click += BtnGpuCLockOffsetHelper_Click;
            // 
            // tpPowerTable
            // 
            tpPowerTable.Controls.Add(panel1);
            tpPowerTable.Location = new Point(4, 22);
            tpPowerTable.Name = "tpPowerTable";
            tpPowerTable.Padding = new Padding(3);
            tpPowerTable.Size = new Size(462, 366);
            tpPowerTable.TabIndex = 8;
            tpPowerTable.Text = "Power Table";
            tpPowerTable.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) 
                                  | AnchorStyles.Left) 
                                 | AnchorStyles.Right;
            panel1.BackColor = SystemColors.Control;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(groupBox6);
            panel1.Location = new Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(460, 365);
            panel1.TabIndex = 24;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(ptcPowerTable);
            groupBox6.Location = new Point(14, 13);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(429, 344);
            groupBox6.TabIndex = 0;
            groupBox6.TabStop = false;
            groupBox6.Text = "Power Table";
            // 
            // tpVoltageTable
            // 
            tpVoltageTable.Controls.Add(panel6);
            tpVoltageTable.Location = new Point(4, 22);
            tpVoltageTable.Name = "tpVoltageTable";
            tpVoltageTable.Padding = new Padding(3);
            tpVoltageTable.Size = new Size(462, 366);
            tpVoltageTable.TabIndex = 9;
            tpVoltageTable.Text = "Voltage Table";
            tpVoltageTable.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) 
                                  | AnchorStyles.Left) 
                                 | AnchorStyles.Right;
            panel6.BackColor = SystemColors.Control;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(groupBox3);
            panel6.Location = new Point(1, 1);
            panel6.Name = "panel6";
            panel6.Size = new Size(460, 362);
            panel6.TabIndex = 25;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(vtcVoltageTable);
            groupBox3.Location = new Point(14, 13);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(429, 343);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Voltage Table";
            // 
            // btnOpen
            // 
            btnOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnOpen.Location = new Point(9, 589);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(80, 23);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "Open BIOS";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += BtnOpen_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Location = new Point(95, 589);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(80, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save BIOS";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnSaveAs
            // 
            btnSaveAs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveAs.Location = new Point(181, 589);
            btnSaveAs.Name = "btnSaveAs";
            btnSaveAs.Size = new Size(100, 23);
            btnSaveAs.TabIndex = 2;
            btnSaveAs.Text = "Save BIOS As";
            btnSaveAs.UseVisualStyleBackColor = true;
            btnSaveAs.Click += BtnSaveAs_Click;
            // 
            // btnRead
            // 
            btnRead.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRead.Location = new Point(341, 589);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(64, 23);
            btnRead.TabIndex = 3;
            btnRead.Text = "ReadBios";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += BtnRead_Click;
            // 
            // btnFlash
            // 
            btnFlash.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnFlash.Location = new Point(411, 589);
            btnFlash.Name = "btnFlash";
            btnFlash.Size = new Size(64, 23);
            btnFlash.TabIndex = 4;
            btnFlash.Text = "FlashBios";
            btnFlash.UseVisualStyleBackColor = true;
            btnFlash.Click += BtnFlash_Click;
            // 
            // btcBoostClocks
            // 
            btcBoostClocks.Location = new Point(6, 19);
            btcBoostClocks.Name = "btcBoostClocks";
            btcBoostClocks.Size = new Size(413, 318);
            btcBoostClocks.SliderEnabled = true;
            btcBoostClocks.SliderMaximum = 97;
            btcBoostClocks.SliderPosition = 67;
            btcBoostClocks.SliderText = "";
            btcBoostClocks.TabIndex = 0;
            // 
            // ptcPerfTable
            // 
            ptcPerfTable.Location = new Point(7, 14);
            ptcPerfTable.Name = "ptcPerfTable";
            ptcPerfTable.Size = new Size(415, 323);
            ptcPerfTable.TabIndex = 94;
            // 
            // boostConfigControl
            // 
            boostConfigControl.Location = new Point(7, 14);
            boostConfigControl.Name = "boostConfigControl";
            boostConfigControl.Size = new Size(415, 326);
            boostConfigControl.TabIndex = 0;
            // 
            // frcFanRange
            // 
            frcFanRange.Location = new Point(7, 19);
            frcFanRange.Name = "frcFanRange";
            frcFanRange.Size = new Size(409, 125);
            frcFanRange.TabIndex = 0;
            // 
            // bbcBaseBoost
            // 
            bbcBaseBoost.Location = new Point(7, 19);
            bbcBaseBoost.Name = "bbcBaseBoost";
            bbcBaseBoost.Size = new Size(409, 82);
            bbcBaseBoost.TabIndex = 0;
            // 
            // ttcTempTargets
            // 
            ttcTempTargets.Location = new Point(7, 99);
            ttcTempTargets.Name = "ttcTempTargets";
            ttcTempTargets.Size = new Size(409, 25);
            ttcTempTargets.TabIndex = 4;
            // 
            // mchMemoryClock
            // 
            mchMemoryClock.Location = new Point(7, 151);
            mchMemoryClock.Name = "mchMemoryClock";
            mchMemoryClock.Size = new Size(198, 28);
            mchMemoryClock.TabIndex = 2;
            // 
            // blhBoostLimiterHelperControl
            // 
            blhBoostLimiterHelperControl.Location = new Point(7, 125);
            blhBoostLimiterHelperControl.Name = "blhBoostLimiterHelperControl";
            blhBoostLimiterHelperControl.Size = new Size(405, 27);
            blhBoostLimiterHelperControl.TabIndex = 1;
            // 
            // ptcPowerTable
            // 
            ptcPowerTable.AutoScroll = true;
            ptcPowerTable.Location = new Point(7, 19);
            ptcPowerTable.Name = "ptcPowerTable";
            ptcPowerTable.Size = new Size(416, 319);
            ptcPowerTable.TabIndex = 0;
            // 
            // vtcVoltageTable
            // 
            vtcVoltageTable.AutoScroll = true;
            vtcVoltageTable.Location = new Point(7, 19);
            vtcVoltageTable.Name = "vtcVoltageTable";
            vtcVoltageTable.Size = new Size(416, 318);
            vtcVoltageTable.TabIndex = 0;
            // 
            // hcHeader
            // 
            hcHeader.FileName = null;
            hcHeader.GeneratedChecksum = 0;
            hcHeader.ImageChecksum = 0;
            hcHeader.Location = new Point(9, 12);
            hcHeader.Name = "hcHeader";
            hcHeader.Size = new Size(450, 143);
            hcHeader.TabIndex = 0;
            hcHeader.DoubleClick += HcHeader_DoubleClick;
            // 
            // frmMBT
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(485, 616);
            Controls.Add(btnSaveAs);
            Controls.Add(btCompare);
            Controls.Add(btnSave);
            Controls.Add(btnVoltToClock);
            Controls.Add(btBITEntires);
            Controls.Add(btnFlash);
            Controls.Add(btnRead);
            Controls.Add(btnOpen);
            Controls.Add(tabControl);
            Controls.Add(gbInfo);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(454, 528);
            Name = "frmMBT";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Maxwell BIOS Editor";
            Load += Load_Form;
            DragDrop += DragDrop_Event;
            DragEnter += DragEnter_Event;
            KeyDown += KeyDown_Event;
            gbInfo.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tpBoostTable.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            tpClockStates.ResumeLayout(false);
            panel4.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            tpBoostStates.ResumeLayout(false);
            panel5.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            tpCommon.ResumeLayout(false);
            panel3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            gbFanControl.ResumeLayout(false);
            tpPowerTable.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            tpVoltageTable.ResumeLayout(false);
            panel6.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);

        }
    }
}
