
namespace Simulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runUntilEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.consoleText = new System.Windows.Forms.TextBox();
            this.panelRBUS = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelDBUS = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panelSBUS = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pDDBUS = new System.Windows.Forms.Panel();
            this.pSSBUS = new System.Windows.Forms.Panel();
            this.pPdALU = new System.Windows.Forms.Panel();
            this.tFLAGS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pPmFLAGSCond = new System.Windows.Forms.Panel();
            this.pPmFLAGS = new System.Windows.Forms.Panel();
            this.tRG = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.pPdFLAGS_DBUS = new System.Windows.Forms.Panel();
            this.pPdFLAGS_SBUS = new System.Windows.Forms.Panel();
            this.tSP = new System.Windows.Forms.TextBox();
            this.tT = new System.Windows.Forms.TextBox();
            this.tPC = new System.Windows.Forms.TextBox();
            this.tADR = new System.Windows.Forms.TextBox();
            this.tMDR = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.pPmRG = new System.Windows.Forms.Panel();
            this.pPmT = new System.Windows.Forms.Panel();
            this.pPmSP = new System.Windows.Forms.Panel();
            this.pPmPC = new System.Windows.Forms.Panel();
            this.pRD = new System.Windows.Forms.Panel();
            this.pPmMDR = new System.Windows.Forms.Panel();
            this.pPdSP_DBUS = new System.Windows.Forms.Panel();
            this.pPdT_DBUS = new System.Windows.Forms.Panel();
            this.pPdPC_DBUS = new System.Windows.Forms.Panel();
            this.pPdADR_DBUS = new System.Windows.Forms.Panel();
            this.pPdIR_SBUS = new System.Windows.Forms.Panel();
            this.pPdSP_SBUS = new System.Windows.Forms.Panel();
            this.pPdPC_SBUS = new System.Windows.Forms.Panel();
            this.pPdADR_SBUS = new System.Windows.Forms.Panel();
            this.pPdIR_DBUS = new System.Windows.Forms.Panel();
            this.pPdMDR = new System.Windows.Forms.Panel();
            this.pPdADR = new System.Windows.Forms.Panel();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.pPdRG_DBUS = new System.Windows.Forms.Panel();
            this.pPdRG_SBUS = new System.Windows.Forms.Panel();
            this.pPdT_SBUS = new System.Windows.Forms.Panel();
            this.tIR = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pPmIR1 = new System.Windows.Forms.Panel();
            this.pPmIR2 = new System.Windows.Forms.Panel();
            this.pPmADR = new System.Windows.Forms.Panel();
            this.pPdMDR_DBUS = new System.Windows.Forms.Panel();
            this.pPdMDR_SBUS = new System.Windows.Forms.Panel();
            this.lRegisters = new System.Windows.Forms.Label();
            this.lRegisterss = new System.Windows.Forms.Label();
            this.memoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tIVR = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pPmIVR = new System.Windows.Forms.Panel();
            this.pPdIVR_DBUS = new System.Windows.Forms.Panel();
            this.pPdIVR_SBUS = new System.Windows.Forms.Panel();
            this.cInterrupt = new System.Windows.Forms.CheckBox();
            this.pWR = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panelDBUS.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.runToolStripMenuItem,
            this.stepToolStripMenuItem,
            this.runUntilEndToolStripMenuItem,
            this.memoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(962, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.parseToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // parseToolStripMenuItem
            // 
            this.parseToolStripMenuItem.Name = "parseToolStripMenuItem";
            this.parseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.parseToolStripMenuItem.Text = "Parse";
            this.parseToolStripMenuItem.Click += new System.EventHandler(this.parseToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.runToolStripMenuItem.Text = "Compile";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // stepToolStripMenuItem
            // 
            this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
            this.stepToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.stepToolStripMenuItem.Text = "Step";
            this.stepToolStripMenuItem.Click += new System.EventHandler(this.stepToolStripMenuItem_Click);
            // 
            // runUntilEndToolStripMenuItem
            // 
            this.runUntilEndToolStripMenuItem.Name = "runUntilEndToolStripMenuItem";
            this.runUntilEndToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.runUntilEndToolStripMenuItem.Text = "Run until end";
            this.runUntilEndToolStripMenuItem.Click += new System.EventHandler(this.runUntilEndToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 841);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Console Log:";
            // 
            // consoleText
            // 
            this.consoleText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleText.Location = new System.Drawing.Point(1, 857);
            this.consoleText.Multiline = true;
            this.consoleText.Name = "consoleText";
            this.consoleText.ReadOnly = true;
            this.consoleText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleText.Size = new System.Drawing.Size(963, 96);
            this.consoleText.TabIndex = 4;
            // 
            // panelRBUS
            // 
            this.panelRBUS.BackColor = System.Drawing.Color.Black;
            this.panelRBUS.Location = new System.Drawing.Point(710, 62);
            this.panelRBUS.Name = "panelRBUS";
            this.panelRBUS.Size = new System.Drawing.Size(5, 640);
            this.panelRBUS.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(695, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "RBUS";
            // 
            // panelDBUS
            // 
            this.panelDBUS.BackColor = System.Drawing.Color.Black;
            this.panelDBUS.Controls.Add(this.panel11);
            this.panelDBUS.Location = new System.Drawing.Point(393, 62);
            this.panelDBUS.Name = "panelDBUS";
            this.panelDBUS.Size = new System.Drawing.Size(5, 640);
            this.panelDBUS.TabIndex = 8;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Black;
            this.panel11.Location = new System.Drawing.Point(0, 412);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(105, 2);
            this.panel11.TabIndex = 23;
            // 
            // panelSBUS
            // 
            this.panelSBUS.BackColor = System.Drawing.Color.Black;
            this.panelSBUS.Location = new System.Drawing.Point(298, 62);
            this.panelSBUS.Name = "panelSBUS";
            this.panelSBUS.Size = new System.Drawing.Size(5, 640);
            this.panelSBUS.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "DBUS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "SBUS";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(496, 80);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(109, 73);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "D\r\n\r\n             ALU          R\r\n\r\nS";
            // 
            // pDDBUS
            // 
            this.pDDBUS.BackColor = System.Drawing.Color.Black;
            this.pDDBUS.Location = new System.Drawing.Point(396, 89);
            this.pDDBUS.Name = "pDDBUS";
            this.pDDBUS.Size = new System.Drawing.Size(100, 2);
            this.pDDBUS.TabIndex = 0;
            // 
            // pSSBUS
            // 
            this.pSSBUS.BackColor = System.Drawing.Color.Black;
            this.pSSBUS.Location = new System.Drawing.Point(301, 133);
            this.pSSBUS.Name = "pSSBUS";
            this.pSSBUS.Size = new System.Drawing.Size(195, 2);
            this.pSSBUS.TabIndex = 12;
            // 
            // pPdALU
            // 
            this.pPdALU.BackColor = System.Drawing.Color.Black;
            this.pPdALU.Location = new System.Drawing.Point(604, 113);
            this.pPdALU.Name = "pPdALU";
            this.pPdALU.Size = new System.Drawing.Size(108, 2);
            this.pPdALU.TabIndex = 13;
            // 
            // tFLAGS
            // 
            this.tFLAGS.Location = new System.Drawing.Point(496, 184);
            this.tFLAGS.Name = "tFLAGS";
            this.tFLAGS.Size = new System.Drawing.Size(109, 20);
            this.tFLAGS.TabIndex = 14;
            this.tFLAGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(529, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "FLAGS";
            // 
            // pPmFLAGSCond
            // 
            this.pPmFLAGSCond.BackColor = System.Drawing.Color.Black;
            this.pPmFLAGSCond.Location = new System.Drawing.Point(549, 150);
            this.pPmFLAGSCond.Name = "pPmFLAGSCond";
            this.pPmFLAGSCond.Size = new System.Drawing.Size(2, 35);
            this.pPmFLAGSCond.TabIndex = 16;
            // 
            // pPmFLAGS
            // 
            this.pPmFLAGS.BackColor = System.Drawing.Color.Black;
            this.pPmFLAGS.Location = new System.Drawing.Point(604, 193);
            this.pPmFLAGS.Name = "pPmFLAGS";
            this.pPmFLAGS.Size = new System.Drawing.Size(108, 2);
            this.pPmFLAGS.TabIndex = 17;
            // 
            // tRG
            // 
            this.tRG.Location = new System.Drawing.Point(496, 223);
            this.tRG.Multiline = true;
            this.tRG.Name = "tRG";
            this.tRG.Size = new System.Drawing.Size(108, 148);
            this.tRG.TabIndex = 18;
            this.tRG.Text = "\r\n\r\n\r\n\r\nGENERAL REGISTERS";
            this.tRG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(516, 374);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(69, 13);
            this.label.TabIndex = 19;
            this.label.Text = "REGISTERS";
            // 
            // pPdFLAGS_DBUS
            // 
            this.pPdFLAGS_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdFLAGS_DBUS.Location = new System.Drawing.Point(393, 184);
            this.pPdFLAGS_DBUS.Name = "pPdFLAGS_DBUS";
            this.pPdFLAGS_DBUS.Size = new System.Drawing.Size(105, 2);
            this.pPdFLAGS_DBUS.TabIndex = 20;
            // 
            // pPdFLAGS_SBUS
            // 
            this.pPdFLAGS_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdFLAGS_SBUS.Location = new System.Drawing.Point(298, 202);
            this.pPdFLAGS_SBUS.Name = "pPdFLAGS_SBUS";
            this.pPdFLAGS_SBUS.Size = new System.Drawing.Size(200, 2);
            this.pPdFLAGS_SBUS.TabIndex = 21;
            // 
            // tSP
            // 
            this.tSP.Location = new System.Drawing.Point(496, 390);
            this.tSP.Name = "tSP";
            this.tSP.Size = new System.Drawing.Size(108, 20);
            this.tSP.TabIndex = 22;
            this.tSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tT
            // 
            this.tT.Location = new System.Drawing.Point(495, 429);
            this.tT.Name = "tT";
            this.tT.Size = new System.Drawing.Size(109, 20);
            this.tT.TabIndex = 23;
            this.tT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tPC
            // 
            this.tPC.Location = new System.Drawing.Point(495, 474);
            this.tPC.Name = "tPC";
            this.tPC.Size = new System.Drawing.Size(109, 20);
            this.tPC.TabIndex = 24;
            this.tPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tADR
            // 
            this.tADR.Location = new System.Drawing.Point(523, 516);
            this.tADR.Name = "tADR";
            this.tADR.Size = new System.Drawing.Size(109, 20);
            this.tADR.TabIndex = 25;
            this.tADR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tMDR
            // 
            this.tMDR.Location = new System.Drawing.Point(462, 555);
            this.tMDR.Name = "tMDR";
            this.tMDR.Size = new System.Drawing.Size(109, 20);
            this.tMDR.TabIndex = 26;
            this.tMDR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(537, 413);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "SP    ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(540, 452);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "T";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(537, 497);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "PC    ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(546, 539);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "ADR    ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(494, 578);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "MDR   ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(471, 661);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(161, 137);
            this.textBox7.TabIndex = 33;
            this.textBox7.Text = "\r\n\r\n\r\n\r\nMEMORY";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pPmRG
            // 
            this.pPmRG.BackColor = System.Drawing.Color.Black;
            this.pPmRG.Location = new System.Drawing.Point(604, 292);
            this.pPmRG.Name = "pPmRG";
            this.pPmRG.Size = new System.Drawing.Size(108, 2);
            this.pPmRG.TabIndex = 34;
            // 
            // pPmT
            // 
            this.pPmT.BackColor = System.Drawing.Color.Black;
            this.pPmT.Location = new System.Drawing.Point(604, 438);
            this.pPmT.Name = "pPmT";
            this.pPmT.Size = new System.Drawing.Size(108, 2);
            this.pPmT.TabIndex = 14;
            // 
            // pPmSP
            // 
            this.pPmSP.BackColor = System.Drawing.Color.Black;
            this.pPmSP.Location = new System.Drawing.Point(604, 399);
            this.pPmSP.Name = "pPmSP";
            this.pPmSP.Size = new System.Drawing.Size(108, 2);
            this.pPmSP.TabIndex = 15;
            // 
            // pPmPC
            // 
            this.pPmPC.BackColor = System.Drawing.Color.Black;
            this.pPmPC.Location = new System.Drawing.Point(604, 483);
            this.pPmPC.Name = "pPmPC";
            this.pPmPC.Size = new System.Drawing.Size(108, 2);
            this.pPmPC.TabIndex = 16;
            // 
            // pRD
            // 
            this.pRD.BackColor = System.Drawing.Color.Black;
            this.pRD.Location = new System.Drawing.Point(633, 671);
            this.pRD.Name = "pRD";
            this.pRD.Size = new System.Drawing.Size(78, 2);
            this.pRD.TabIndex = 17;
            // 
            // pPmMDR
            // 
            this.pPmMDR.BackColor = System.Drawing.Color.Black;
            this.pPmMDR.Location = new System.Drawing.Point(569, 564);
            this.pPmMDR.Name = "pPmMDR";
            this.pPmMDR.Size = new System.Drawing.Size(143, 2);
            this.pPmMDR.TabIndex = 18;
            // 
            // pPdSP_DBUS
            // 
            this.pPdSP_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdSP_DBUS.Location = new System.Drawing.Point(393, 390);
            this.pPdSP_DBUS.Name = "pPdSP_DBUS";
            this.pPdSP_DBUS.Size = new System.Drawing.Size(105, 2);
            this.pPdSP_DBUS.TabIndex = 21;
            // 
            // pPdT_DBUS
            // 
            this.pPdT_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdT_DBUS.Location = new System.Drawing.Point(393, 429);
            this.pPdT_DBUS.Name = "pPdT_DBUS";
            this.pPdT_DBUS.Size = new System.Drawing.Size(105, 2);
            this.pPdT_DBUS.TabIndex = 22;
            // 
            // pPdPC_DBUS
            // 
            this.pPdPC_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdPC_DBUS.Location = new System.Drawing.Point(393, 474);
            this.pPdPC_DBUS.Name = "pPdPC_DBUS";
            this.pPdPC_DBUS.Size = new System.Drawing.Size(105, 2);
            this.pPdPC_DBUS.TabIndex = 23;
            // 
            // pPdADR_DBUS
            // 
            this.pPdADR_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdADR_DBUS.Location = new System.Drawing.Point(393, 516);
            this.pPdADR_DBUS.Name = "pPdADR_DBUS";
            this.pPdADR_DBUS.Size = new System.Drawing.Size(132, 2);
            this.pPdADR_DBUS.TabIndex = 24;
            // 
            // pPdIR_SBUS
            // 
            this.pPdIR_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdIR_SBUS.Location = new System.Drawing.Point(208, 636);
            this.pPdIR_SBUS.Name = "pPdIR_SBUS";
            this.pPdIR_SBUS.Size = new System.Drawing.Size(90, 2);
            this.pPdIR_SBUS.TabIndex = 25;
            // 
            // pPdSP_SBUS
            // 
            this.pPdSP_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdSP_SBUS.Location = new System.Drawing.Point(298, 408);
            this.pPdSP_SBUS.Name = "pPdSP_SBUS";
            this.pPdSP_SBUS.Size = new System.Drawing.Size(200, 2);
            this.pPdSP_SBUS.TabIndex = 35;
            // 
            // pPdPC_SBUS
            // 
            this.pPdPC_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdPC_SBUS.Location = new System.Drawing.Point(298, 492);
            this.pPdPC_SBUS.Name = "pPdPC_SBUS";
            this.pPdPC_SBUS.Size = new System.Drawing.Size(200, 2);
            this.pPdPC_SBUS.TabIndex = 22;
            // 
            // pPdADR_SBUS
            // 
            this.pPdADR_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdADR_SBUS.Location = new System.Drawing.Point(298, 534);
            this.pPdADR_SBUS.Name = "pPdADR_SBUS";
            this.pPdADR_SBUS.Size = new System.Drawing.Size(226, 2);
            this.pPdADR_SBUS.TabIndex = 22;
            // 
            // pPdIR_DBUS
            // 
            this.pPdIR_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdIR_DBUS.Location = new System.Drawing.Point(208, 655);
            this.pPdIR_DBUS.Name = "pPdIR_DBUS";
            this.pPdIR_DBUS.Size = new System.Drawing.Size(190, 2);
            this.pPdIR_DBUS.TabIndex = 22;
            // 
            // pPdMDR
            // 
            this.pPdMDR.BackColor = System.Drawing.Color.Black;
            this.pPdMDR.Location = new System.Drawing.Point(486, 573);
            this.pPdMDR.Name = "pPdMDR";
            this.pPdMDR.Size = new System.Drawing.Size(2, 88);
            this.pPdMDR.TabIndex = 36;
            // 
            // pPdADR
            // 
            this.pPdADR.BackColor = System.Drawing.Color.Black;
            this.pPdADR.Location = new System.Drawing.Point(620, 534);
            this.pPdADR.Name = "pPdADR";
            this.pPdADR.Size = new System.Drawing.Size(2, 127);
            this.pPdADR.TabIndex = 37;
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.textBox8.Location = new System.Drawing.Point(70, 101);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(171, 581);
            this.textBox8.TabIndex = 38;
            this.textBox8.Text = "\r\n\r\n\r\n\r\n\r\n\r\n\r\nBGC";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pPdRG_DBUS
            // 
            this.pPdRG_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdRG_DBUS.Location = new System.Drawing.Point(393, 253);
            this.pPdRG_DBUS.Name = "pPdRG_DBUS";
            this.pPdRG_DBUS.Size = new System.Drawing.Size(105, 2);
            this.pPdRG_DBUS.TabIndex = 21;
            // 
            // pPdRG_SBUS
            // 
            this.pPdRG_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdRG_SBUS.Location = new System.Drawing.Point(298, 317);
            this.pPdRG_SBUS.Name = "pPdRG_SBUS";
            this.pPdRG_SBUS.Size = new System.Drawing.Size(200, 2);
            this.pPdRG_SBUS.TabIndex = 22;
            // 
            // pPdT_SBUS
            // 
            this.pPdT_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdT_SBUS.Location = new System.Drawing.Point(298, 447);
            this.pPdT_SBUS.Name = "pPdT_SBUS";
            this.pPdT_SBUS.Size = new System.Drawing.Size(200, 2);
            this.pPdT_SBUS.TabIndex = 36;
            // 
            // tIR
            // 
            this.tIR.Location = new System.Drawing.Point(103, 637);
            this.tIR.Name = "tIR";
            this.tIR.Size = new System.Drawing.Size(109, 20);
            this.tIR.TabIndex = 39;
            this.tIR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(144, 660);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "IR";
            // 
            // pPmIR1
            // 
            this.pPmIR1.BackColor = System.Drawing.Color.Black;
            this.pPmIR1.Location = new System.Drawing.Point(177, 745);
            this.pPmIR1.Name = "pPmIR1";
            this.pPmIR1.Size = new System.Drawing.Size(298, 2);
            this.pPmIR1.TabIndex = 41;
            // 
            // pPmIR2
            // 
            this.pPmIR2.BackColor = System.Drawing.Color.Black;
            this.pPmIR2.Location = new System.Drawing.Point(177, 660);
            this.pPmIR2.Name = "pPmIR2";
            this.pPmIR2.Size = new System.Drawing.Size(2, 85);
            this.pPmIR2.TabIndex = 42;
            // 
            // pPmADR
            // 
            this.pPmADR.BackColor = System.Drawing.Color.Black;
            this.pPmADR.Location = new System.Drawing.Point(634, 525);
            this.pPmADR.Name = "pPmADR";
            this.pPmADR.Size = new System.Drawing.Size(77, 2);
            this.pPmADR.TabIndex = 17;
            // 
            // pPdMDR_DBUS
            // 
            this.pPdMDR_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdMDR_DBUS.Location = new System.Drawing.Point(392, 555);
            this.pPdMDR_DBUS.Name = "pPdMDR_DBUS";
            this.pPdMDR_DBUS.Size = new System.Drawing.Size(72, 2);
            this.pPdMDR_DBUS.TabIndex = 25;
            // 
            // pPdMDR_SBUS
            // 
            this.pPdMDR_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdMDR_SBUS.Location = new System.Drawing.Point(298, 573);
            this.pPdMDR_SBUS.Name = "pPdMDR_SBUS";
            this.pPdMDR_SBUS.Size = new System.Drawing.Size(166, 2);
            this.pPdMDR_SBUS.TabIndex = 25;
            // 
            // lRegisters
            // 
            this.lRegisters.AutoSize = true;
            this.lRegisters.Location = new System.Drawing.Point(825, 24);
            this.lRegisters.Name = "lRegisters";
            this.lRegisters.Size = new System.Drawing.Size(0, 13);
            this.lRegisters.TabIndex = 43;
            // 
            // lRegisterss
            // 
            this.lRegisterss.AutoSize = true;
            this.lRegisterss.Location = new System.Drawing.Point(801, 62);
            this.lRegisterss.Name = "lRegisterss";
            this.lRegisterss.Size = new System.Drawing.Size(107, 13);
            this.lRegisterss.TabIndex = 44;
            this.lRegisterss.Text = "No program compiled";
            // 
            // memoryToolStripMenuItem
            // 
            this.memoryToolStripMenuItem.Name = "memoryToolStripMenuItem";
            this.memoryToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.memoryToolStripMenuItem.Text = "Memory";
            this.memoryToolStripMenuItem.Click += new System.EventHandler(this.memoryToolStripMenuItem_Click);
            // 
            // tIVR
            // 
            this.tIVR.Location = new System.Drawing.Point(496, 604);
            this.tIVR.Name = "tIVR";
            this.tIVR.Size = new System.Drawing.Size(109, 20);
            this.tIVR.TabIndex = 45;
            this.tIVR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(537, 625);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "IVR";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pPmIVR
            // 
            this.pPmIVR.BackColor = System.Drawing.Color.Black;
            this.pPmIVR.Location = new System.Drawing.Point(607, 613);
            this.pPmIVR.Name = "pPmIVR";
            this.pPmIVR.Size = new System.Drawing.Size(108, 2);
            this.pPmIVR.TabIndex = 16;
            // 
            // pPdIVR_DBUS
            // 
            this.pPdIVR_DBUS.BackColor = System.Drawing.Color.Black;
            this.pPdIVR_DBUS.Location = new System.Drawing.Point(396, 604);
            this.pPdIVR_DBUS.Name = "pPdIVR_DBUS";
            this.pPdIVR_DBUS.Size = new System.Drawing.Size(105, 2);
            this.pPdIVR_DBUS.TabIndex = 24;
            // 
            // pPdIVR_SBUS
            // 
            this.pPdIVR_SBUS.BackColor = System.Drawing.Color.Black;
            this.pPdIVR_SBUS.Location = new System.Drawing.Point(298, 622);
            this.pPdIVR_SBUS.Name = "pPdIVR_SBUS";
            this.pPdIVR_SBUS.Size = new System.Drawing.Size(200, 2);
            this.pPdIVR_SBUS.TabIndex = 37;
            // 
            // cInterrupt
            // 
            this.cInterrupt.AutoSize = true;
            this.cInterrupt.Location = new System.Drawing.Point(34, 46);
            this.cInterrupt.Name = "cInterrupt";
            this.cInterrupt.Size = new System.Drawing.Size(100, 17);
            this.cInterrupt.TabIndex = 47;
            this.cInterrupt.Text = "Interrupt enable";
            this.cInterrupt.UseVisualStyleBackColor = true;
            // 
            // pWR
            // 
            this.pWR.BackColor = System.Drawing.Color.Black;
            this.pWR.Location = new System.Drawing.Point(634, 697);
            this.pWR.Name = "pWR";
            this.pWR.Size = new System.Drawing.Size(78, 2);
            this.pWR.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 951);
            this.Controls.Add(this.pWR);
            this.Controls.Add(this.cInterrupt);
            this.Controls.Add(this.pPdIVR_SBUS);
            this.Controls.Add(this.pPdIVR_DBUS);
            this.Controls.Add(this.pPmIVR);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tIVR);
            this.Controls.Add(this.lRegisterss);
            this.Controls.Add(this.lRegisters);
            this.Controls.Add(this.pPdMDR_SBUS);
            this.Controls.Add(this.pPdMDR_DBUS);
            this.Controls.Add(this.pPmADR);
            this.Controls.Add(this.pPmIR2);
            this.Controls.Add(this.pPmIR1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tIR);
            this.Controls.Add(this.pPdT_SBUS);
            this.Controls.Add(this.pPdSP_SBUS);
            this.Controls.Add(this.pPdRG_SBUS);
            this.Controls.Add(this.pPdRG_DBUS);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.pPdADR);
            this.Controls.Add(this.pPdMDR);
            this.Controls.Add(this.pPdADR_SBUS);
            this.Controls.Add(this.pPdIR_DBUS);
            this.Controls.Add(this.pPdPC_SBUS);
            this.Controls.Add(this.pPdIR_SBUS);
            this.Controls.Add(this.pPdADR_DBUS);
            this.Controls.Add(this.pPdPC_DBUS);
            this.Controls.Add(this.pPdT_DBUS);
            this.Controls.Add(this.pPdSP_DBUS);
            this.Controls.Add(this.pPmMDR);
            this.Controls.Add(this.pRD);
            this.Controls.Add(this.pPmPC);
            this.Controls.Add(this.pPmT);
            this.Controls.Add(this.pPmSP);
            this.Controls.Add(this.pPmRG);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tMDR);
            this.Controls.Add(this.tADR);
            this.Controls.Add(this.tPC);
            this.Controls.Add(this.tT);
            this.Controls.Add(this.tSP);
            this.Controls.Add(this.pPdFLAGS_SBUS);
            this.Controls.Add(this.pPdFLAGS_DBUS);
            this.Controls.Add(this.label);
            this.Controls.Add(this.tRG);
            this.Controls.Add(this.pPmFLAGS);
            this.Controls.Add(this.pPmFLAGSCond);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tFLAGS);
            this.Controls.Add(this.pPdALU);
            this.Controls.Add(this.pSSBUS);
            this.Controls.Add(this.pDDBUS);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panelSBUS);
            this.Controls.Add(this.panelDBUS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelRBUS);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.consoleText);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelDBUS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox consoleText;
        private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
        private System.Windows.Forms.Panel panelRBUS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelDBUS;
        private System.Windows.Forms.Panel panelSBUS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel pDDBUS;
        private System.Windows.Forms.Panel pSSBUS;
        private System.Windows.Forms.Panel pPdALU;
        private System.Windows.Forms.TextBox tFLAGS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pPmFLAGSCond;
        private System.Windows.Forms.Panel pPmFLAGS;
        private System.Windows.Forms.TextBox tRG;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel pPdFLAGS_DBUS;
        private System.Windows.Forms.Panel pPdFLAGS_SBUS;
        private System.Windows.Forms.TextBox tSP;
        private System.Windows.Forms.TextBox tT;
        private System.Windows.Forms.TextBox tPC;
        private System.Windows.Forms.TextBox tADR;
        private System.Windows.Forms.TextBox tMDR;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel pPmRG;
        private System.Windows.Forms.Panel pPmT;
        private System.Windows.Forms.Panel pPmSP;
        private System.Windows.Forms.Panel pPmPC;
        private System.Windows.Forms.Panel pRD;
        private System.Windows.Forms.Panel pPmMDR;
        private System.Windows.Forms.Panel pPdSP_DBUS;
        private System.Windows.Forms.Panel pPdT_DBUS;
        private System.Windows.Forms.Panel pPdPC_DBUS;
        private System.Windows.Forms.Panel pPdADR_DBUS;
        private System.Windows.Forms.Panel pPdIR_SBUS;
        private System.Windows.Forms.Panel pPdSP_SBUS;
        private System.Windows.Forms.Panel pPdPC_SBUS;
        private System.Windows.Forms.Panel pPdADR_SBUS;
        private System.Windows.Forms.Panel pPdIR_DBUS;
        private System.Windows.Forms.Panel pPdMDR;
        private System.Windows.Forms.Panel pPdADR;
        private System.Windows.Forms.ToolStripMenuItem runUntilEndToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Panel pPdRG_DBUS;
        private System.Windows.Forms.Panel pPdRG_SBUS;
        private System.Windows.Forms.Panel pPdT_SBUS;
        private System.Windows.Forms.TextBox tIR;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pPmIR1;
        private System.Windows.Forms.Panel pPmIR2;
        private System.Windows.Forms.Panel pPmADR;
        private System.Windows.Forms.Panel pPdMDR_DBUS;
        private System.Windows.Forms.Panel pPdMDR_SBUS;
        private System.Windows.Forms.Label lRegisters;
        private System.Windows.Forms.Label lRegisterss;
        private System.Windows.Forms.ToolStripMenuItem memoryToolStripMenuItem;
        private System.Windows.Forms.TextBox tIVR;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pPmIVR;
        private System.Windows.Forms.Panel pPdIVR_DBUS;
        private System.Windows.Forms.Panel pPdIVR_SBUS;
        private System.Windows.Forms.CheckBox cInterrupt;
        private System.Windows.Forms.Panel pWR;
    }
}

