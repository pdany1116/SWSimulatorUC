
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
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileAndSimulateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.consoleText = new System.Windows.Forms.TextBox();
            this.panelRBUS = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelDBUS = new System.Windows.Forms.Panel();
            this.panelSBUS = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pdDBUS = new System.Windows.Forms.Panel();
            this.pdSBUS = new System.Windows.Forms.Panel();
            this.pdALU = new System.Windows.Forms.Panel();
            this.FlagsText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.generalRegisters = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.pdFlagDBUS = new System.Windows.Forms.Panel();
            this.pdFlagSBUS = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.runUntilEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox8 = new System.Windows.Forms.TextBox();
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
            this.runUntilEndToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(850, 24);
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
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // parseToolStripMenuItem
            // 
            this.parseToolStripMenuItem.Name = "parseToolStripMenuItem";
            this.parseToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.parseToolStripMenuItem.Text = "Parse";
            this.parseToolStripMenuItem.Click += new System.EventHandler(this.parseToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileToolStripMenuItem,
            this.compileAndSimulateToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // compileAndSimulateToolStripMenuItem
            // 
            this.compileAndSimulateToolStripMenuItem.Name = "compileAndSimulateToolStripMenuItem";
            this.compileAndSimulateToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.compileAndSimulateToolStripMenuItem.Text = "Compile and Simulate";
            // 
            // stepToolStripMenuItem
            // 
            this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
            this.stepToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.stepToolStripMenuItem.Text = "Step";
            this.stepToolStripMenuItem.Click += new System.EventHandler(this.stepToolStripMenuItem_Click);
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
            this.consoleText.Size = new System.Drawing.Size(851, 96);
            this.consoleText.TabIndex = 4;
            // 
            // panelRBUS
            // 
            this.panelRBUS.BackColor = System.Drawing.Color.Black;
            this.panelRBUS.Location = new System.Drawing.Point(710, 62);
            this.panelRBUS.Name = "panelRBUS";
            this.panelRBUS.Size = new System.Drawing.Size(5, 620);
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
            this.panelDBUS.Size = new System.Drawing.Size(5, 620);
            this.panelDBUS.TabIndex = 8;
            // 
            // panelSBUS
            // 
            this.panelSBUS.BackColor = System.Drawing.Color.Black;
            this.panelSBUS.Location = new System.Drawing.Point(298, 62);
            this.panelSBUS.Name = "panelSBUS";
            this.panelSBUS.Size = new System.Drawing.Size(5, 620);
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
            // pdDBUS
            // 
            this.pdDBUS.BackColor = System.Drawing.Color.Black;
            this.pdDBUS.Location = new System.Drawing.Point(396, 89);
            this.pdDBUS.Name = "pdDBUS";
            this.pdDBUS.Size = new System.Drawing.Size(100, 2);
            this.pdDBUS.TabIndex = 0;
            // 
            // pdSBUS
            // 
            this.pdSBUS.BackColor = System.Drawing.Color.Black;
            this.pdSBUS.Location = new System.Drawing.Point(301, 133);
            this.pdSBUS.Name = "pdSBUS";
            this.pdSBUS.Size = new System.Drawing.Size(195, 2);
            this.pdSBUS.TabIndex = 12;
            // 
            // pdALU
            // 
            this.pdALU.BackColor = System.Drawing.Color.Black;
            this.pdALU.Location = new System.Drawing.Point(604, 113);
            this.pdALU.Name = "pdALU";
            this.pdALU.Size = new System.Drawing.Size(108, 2);
            this.pdALU.TabIndex = 13;
            // 
            // FlagsText
            // 
            this.FlagsText.Location = new System.Drawing.Point(496, 184);
            this.FlagsText.Name = "FlagsText";
            this.FlagsText.Size = new System.Drawing.Size(109, 20);
            this.FlagsText.TabIndex = 14;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(549, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 35);
            this.panel1.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(604, 193);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(108, 2);
            this.panel2.TabIndex = 17;
            // 
            // generalRegisters
            // 
            this.generalRegisters.Location = new System.Drawing.Point(496, 223);
            this.generalRegisters.Multiline = true;
            this.generalRegisters.Name = "generalRegisters";
            this.generalRegisters.Size = new System.Drawing.Size(108, 148);
            this.generalRegisters.TabIndex = 18;
            this.generalRegisters.Text = "R0              R10\r\nR1              R11\r\nR2              R12\r\nR3              R1" +
    "3\r\nR4              R14\r\nR5              R15\r\nR6\r\nR7\r\nR8\r\nR9";
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
            // pdFlagDBUS
            // 
            this.pdFlagDBUS.BackColor = System.Drawing.Color.Black;
            this.pdFlagDBUS.Location = new System.Drawing.Point(393, 184);
            this.pdFlagDBUS.Name = "pdFlagDBUS";
            this.pdFlagDBUS.Size = new System.Drawing.Size(105, 2);
            this.pdFlagDBUS.TabIndex = 20;
            // 
            // pdFlagSBUS
            // 
            this.pdFlagSBUS.BackColor = System.Drawing.Color.Black;
            this.pdFlagSBUS.Location = new System.Drawing.Point(298, 202);
            this.pdFlagSBUS.Name = "pdFlagSBUS";
            this.pdFlagSBUS.Size = new System.Drawing.Size(200, 2);
            this.pdFlagSBUS.TabIndex = 21;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(496, 390);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(108, 20);
            this.textBox2.TabIndex = 22;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(495, 429);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(109, 20);
            this.textBox3.TabIndex = 23;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(495, 474);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(109, 20);
            this.textBox4.TabIndex = 24;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(523, 516);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(109, 20);
            this.textBox5.TabIndex = 25;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(462, 555);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(109, 20);
            this.textBox6.TabIndex = 26;
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
            this.label7.Location = new System.Drawing.Point(537, 458);
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
            this.label8.Location = new System.Drawing.Point(537, 500);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "PC    ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(529, 539);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "ADR    ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(510, 578);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "MDR   ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(471, 619);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(161, 137);
            this.textBox7.TabIndex = 33;
            this.textBox7.Text = "\r\n\r\n\r\n\r\nMEMORY";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(604, 292);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(108, 2);
            this.panel3.TabIndex = 34;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(604, 438);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(108, 2);
            this.panel4.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(604, 399);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(108, 2);
            this.panel5.TabIndex = 15;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Black;
            this.panel6.Location = new System.Drawing.Point(604, 483);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(108, 2);
            this.panel6.TabIndex = 16;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Black;
            this.panel7.Location = new System.Drawing.Point(632, 525);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(78, 2);
            this.panel7.TabIndex = 17;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Black;
            this.panel8.Location = new System.Drawing.Point(569, 564);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(143, 2);
            this.panel8.TabIndex = 18;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Black;
            this.panel9.Location = new System.Drawing.Point(393, 390);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(105, 2);
            this.panel9.TabIndex = 21;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Black;
            this.panel10.Location = new System.Drawing.Point(393, 429);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(105, 2);
            this.panel10.TabIndex = 22;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Black;
            this.panel11.Location = new System.Drawing.Point(0, 412);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(105, 2);
            this.panel11.TabIndex = 23;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Black;
            this.panel12.Location = new System.Drawing.Point(393, 474);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(105, 2);
            this.panel12.TabIndex = 23;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.Black;
            this.panel13.Location = new System.Drawing.Point(393, 516);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(132, 2);
            this.panel13.TabIndex = 24;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Black;
            this.panel14.Location = new System.Drawing.Point(393, 555);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(70, 2);
            this.panel14.TabIndex = 25;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Black;
            this.panel15.Location = new System.Drawing.Point(298, 408);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(200, 2);
            this.panel15.TabIndex = 35;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.Black;
            this.panel16.Location = new System.Drawing.Point(298, 492);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(200, 2);
            this.panel16.TabIndex = 22;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.Black;
            this.panel17.Location = new System.Drawing.Point(298, 534);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(226, 2);
            this.panel17.TabIndex = 22;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.Black;
            this.panel18.Location = new System.Drawing.Point(298, 573);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(165, 2);
            this.panel18.TabIndex = 22;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Black;
            this.panel19.Location = new System.Drawing.Point(486, 573);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(2, 46);
            this.panel19.TabIndex = 36;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Black;
            this.panel20.Location = new System.Drawing.Point(602, 535);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(2, 84);
            this.panel20.TabIndex = 37;
            // 
            // runUntilEndToolStripMenuItem
            // 
            this.runUntilEndToolStripMenuItem.Name = "runUntilEndToolStripMenuItem";
            this.runUntilEndToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.runUntilEndToolStripMenuItem.Text = "Run until end";
            this.runUntilEndToolStripMenuItem.Click += new System.EventHandler(this.runUntilEndToolStripMenuItem_Click);
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.textBox8.Location = new System.Drawing.Point(40, 113);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(171, 483);
            this.textBox8.TabIndex = 38;
            this.textBox8.Text = "\r\n\r\n\r\n\r\n\r\n\r\n\r\nBGC";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 951);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.panel20);
            this.Controls.Add(this.panel19);
            this.Controls.Add(this.panel17);
            this.Controls.Add(this.panel18);
            this.Controls.Add(this.panel16);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.pdFlagSBUS);
            this.Controls.Add(this.pdFlagDBUS);
            this.Controls.Add(this.label);
            this.Controls.Add(this.generalRegisters);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.FlagsText);
            this.Controls.Add(this.pdALU);
            this.Controls.Add(this.pdSBUS);
            this.Controls.Add(this.pdDBUS);
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
            this.Text = "Simulator";
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
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileAndSimulateToolStripMenuItem;
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
        private System.Windows.Forms.Panel pdDBUS;
        private System.Windows.Forms.Panel pdSBUS;
        private System.Windows.Forms.Panel pdALU;
        private System.Windows.Forms.TextBox FlagsText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox generalRegisters;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel pdFlagDBUS;
        private System.Windows.Forms.Panel pdFlagSBUS;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.ToolStripMenuItem runUntilEndToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox8;
    }
}

