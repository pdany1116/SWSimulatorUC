using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace Simulator
{
    public partial class Form1 : Form
    {
        Thread thread;
        
        private string asmFile = "";
        private string binFile = "";
        Simulator simulator;
        MemoryDump memoryDump;
        Assembler assembler;

        private bool parsed = false;
        private bool compiled = false;
        public static bool interruptCheck;

        public Form1()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    asmFile = ofd.FileName;
                    int index = asmFile.LastIndexOf('.');
                    binFile = "." + binFile;
                    binFile = asmFile.Remove(index);
                    binFile += ".bin";
                    LOG("File loaded succesfully: " + asmFile);
                }
                else
                {
                    LOG("Failed to load: " + asmFile);
                }
            }
        }

        private void parseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (asmFile == "")
            {
                LOG("Nothing to parse! Please load a file!");
                return;
            }
            assembler = new Assembler();
            if (!assembler.ParseAndTransform(asmFile, binFile))
            {
                LOG(assembler.m_errorMessage);
                LOG("Failed to parse: " + asmFile);
                return;
            }
            parsed = true;
            LOG("Parse completed succesfully!");
            LOG("Binary file created: " + binFile);
        }

        public void LOG(string message)
        {
            consoleText.AppendText(message + "\r\n");
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            asmFile = "";
            binFile = "";
        }

        private void stepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(compiled == false)
            {
                MessageBox.Show("Program not compiled!");
                return;
            }
            resetColors();
            
            interruptCheck = cInterrupt.Checked;

            if(Simulator.instructionCode == 57357) //HALT 
            {
                MessageBox.Show("END");
            }
            else if(simulator.compiled == true)
            {
                LOG(Simulator.currentPhase + " " + Simulator.currentImpulse);
                simulator.impulseGenerator();
                refreshValues();
                
                setRedLines();
            }
            else
            {
                LOG("Program not compiled!!");
            }

            Refresh();
        
        }

        private void runUntilEndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (compiled == false)
            {
                MessageBox.Show("Program not compiled!");
                return;
            }


            while (Simulator.instructionCode != 57357) //HALT 
            {
                if (simulator.compiled == true)
                {
                    simulator.impulseGenerator();
                    LOG(Simulator.currentPhase + " " + Simulator.currentImpulse);
                }

                
            
            }

            refreshValues();

            MessageBox.Show("END");

            Refresh();

        }

        private void setRedLines()
        {
            if(Simulator.PdALU == true)
            {
                panelRBUS.BackColor = Color.Red;
                pPdALU.BackColor = Color.Red;
            }
            if(Simulator.PmFLAG == true)
            {
                panelRBUS.BackColor = Color.Red;
                pPmFLAGS.BackColor = Color.Red;
            }
            if(Simulator.PmFLAGCond == true)
            {
                pPmFLAGSCond.BackColor = Color.Red;
            }
            if(Simulator.PdFLAG_DBUS == true)
            {
                pPdFLAGS_DBUS.BackColor = Color.Red;
            }
            if(Simulator.PdFLAG_SBUS == true)
            {
                pPdFLAGS_SBUS.BackColor = Color.Red;
            }

            if(Simulator.PdRG_DBUS == true)
            {
                pPdRG_DBUS.BackColor = Color.Red;
            }
            if (Simulator.PdRG_SBUS == true)
            {
                pPdRG_SBUS.BackColor = Color.Red;
            }
            if(Simulator.PmRG == true)
            {
                panelRBUS.BackColor = Color.Red;
                pPmRG.BackColor = Color.Red;
            }
            if(Simulator.PmSP == true)
            {
                panelRBUS.BackColor = Color.Red;
                pPmSP.BackColor = Color.Red;
            }
            if(Simulator.PdSP_DBUS == true)
            {
                pPdSP_DBUS.BackColor = Color.Red;
            }
            if(Simulator.PdSP_SBUS == true)
            {
                pPdSP_DBUS.BackColor = Color.Red;
            }
            if(Simulator.PmT == true)
            {
                panelRBUS.BackColor = Color.Red;
                pPmT.BackColor = Color.Red;
            }
            if(Simulator.PdT_DBUS == true)
            {
                pPdT_DBUS.BackColor = Color.Red;
            }
            if(Simulator.PdT_SBUS == true)
            {
                pPdT_SBUS.BackColor = Color.Red;
            }
            if(Simulator.PdPC_SBUS == true)
            {
                pPdPC_SBUS.BackColor = Color.Red;
            }
            if (Simulator.PdPC_DBUS == true) 
            {
                pPdPC_DBUS.BackColor = Color.Red;
            }
            if(Simulator.PmPC == true)
            {
                panelRBUS.BackColor = Color.Red;
                pPmPC.BackColor = Color.Red;
            }
            if(Simulator.PmADR == true)
            {
                panelRBUS.BackColor = Color.Red;
                pPmADR.BackColor = Color.Red;
            }
            if(Simulator.PdADR_DBUS == true)
            {
                pPdADR_DBUS.BackColor = Color.Red;
            }
            if(Simulator.PdADR_SBUS == true)
            {
                pPdADR_SBUS.BackColor = Color.Red;
            }
            if(Simulator.PmMDR == true)
            {
                panelRBUS.BackColor = Color.Red;
                pPmMDR.BackColor = Color.Red;
            }
            if(Simulator.PdMDR_DBUS == true)
            {
                pPdMDR_DBUS.BackColor = Color.Red;
            }
            if(Simulator.PdMDR_SBUS == true)
            {
                pPdMDR_SBUS.BackColor = Color.Red;
            }
            if(Simulator.PmIR == true)
            {
                pPmIR1.BackColor = Color.Red;
                pPmIR2.BackColor = Color.Red;
            }
            if(Simulator.PdIR_SBUS == true)
            {
                pPdIR_SBUS.BackColor = Color.Red;
            }
            if(Simulator.PdIR_DBUS == true)
            {
                pPdIR_DBUS.BackColor = Color.Red;
            }
            if(Simulator.RD == true)
            {
                pRD.BackColor = Color.Red;
            }
            if(Simulator.DDBUS == true)
            {
                panelDBUS.BackColor = Color.Red;
                pDDBUS.BackColor = Color.Red;
            }
            if(Simulator.SSBUS == true)
            {
                panelSBUS.BackColor = Color.Red;
                pSSBUS.BackColor = Color.Red;
            }
            if(Simulator.PdADR == true)
            {
                pPdADR.BackColor = Color.Red;
            }
            if(Simulator.PdIVR_DBUS == true)
            {
                pPdIVR_DBUS.BackColor = Color.Red;
            }
            if (Simulator.PdIVR_DBUS == true)
            {
                pPdIVR_SBUS.BackColor = Color.Red;
            }
            if(Simulator.PmFLAGCond == true)
            {
                pPmFLAGSCond.BackColor = Color.Red;
            }
            if(Simulator.WR == true)
            {
                panelRBUS.BackColor = Color.Red;
            }
        }

        private void resetColors()
        {
            
                pPdALU.BackColor = Color.Black;
 
                pPmFLAGS.BackColor = Color.Black;
            
                pPmFLAGSCond.BackColor = Color.Black;
           
                pPdFLAGS_DBUS.BackColor = Color.Black;
            
                pPdFLAGS_SBUS.BackColor = Color.Black;
                 
                pPdRG_DBUS.BackColor = Color.Black;
           
                pPdRG_SBUS.BackColor = Color.Black;
            
                pPmRG.BackColor = Color.Black;
            
                pPmSP.BackColor = Color.Black;
            
                pPdSP_DBUS.BackColor = Color.Black;
            
                pPdSP_DBUS.BackColor = Color.Black;
            
                pPmT.BackColor = Color.Black;
            
                pPdT_DBUS.BackColor = Color.Black;
            
                pPdT_SBUS.BackColor = Color.Black;
            
                pPdPC_SBUS.BackColor = Color.Black;
            
                pPdPC_DBUS.BackColor = Color.Black;
           
                pPmPC.BackColor = Color.Black;
           
                pPmADR.BackColor = Color.Black;
            
                pPdADR_DBUS.BackColor = Color.Black;
            
                pPdADR_SBUS.BackColor = Color.Black;
            
                pPmMDR.BackColor = Color.Black;
           
                pPdMDR_DBUS.BackColor = Color.Black;
           
                pPdMDR_SBUS.BackColor = Color.Black;
           
                pPmIR1.BackColor = Color.Black;
                pPmIR2.BackColor = Color.Black;
           
                pPdIR_SBUS.BackColor = Color.Black;
           
                pPdIR_DBUS.BackColor = Color.Black;
           
                pRD.BackColor = Color.Black;
           
                pDDBUS.BackColor = Color.Black;
                        
                pSSBUS.BackColor = Color.Black;
            
                pPdADR.BackColor = Color.Black;

                pPdIVR_SBUS.BackColor = Color.Black;

                pPdIVR_DBUS.BackColor = Color.Black;

                panelDBUS.BackColor = Color.Black;

                panelSBUS.BackColor = Color.Black;

                panelRBUS.BackColor = Color.Black;

        }

        private void refreshValues()
        {
            tADR.Text = Convert.ToString(Simulator.ADR);
            tMDR.Text = Convert.ToString(Simulator.MDR);
            tPC.Text = Convert.ToString(Simulator.PC);
            tIR.Text = Convert.ToString(Simulator.IR);
            tFLAGS.Text = Convert.ToString(Simulator.FLAG);
            tSP.Text = Convert.ToString(Simulator.SP);
            tT.Text = Convert.ToString(Simulator.T);
            tIVR.Text = Convert.ToString(Simulator.IVR);

            lRegisterss.Text = "R0 = " + Simulator.Register[0] + "\n" +
                              "R1 = " + Simulator.Register[1] + "\n" +
                              "R2 = " + Simulator.Register[2] + "\n" +
                              "R3 = " + Simulator.Register[3] + "\n" +
                              "R4 = " + Simulator.Register[4] + "\n" +
                              "R5 = " + Simulator.Register[5] + "\n" +
                              "R6 = " + Simulator.Register[6] + "\n" +
                              "R7 = " + Simulator.Register[7] + "\n" +
                              "R8 = " + Simulator.Register[8] + "\n" +
                              "R9 = " + Simulator.Register[9] + "\n" +
                              "R10 = " + Simulator.Register[10] + "\n" +
                              "R11 = " + Simulator.Register[11] + "\n" +
                              "R12 = " + Simulator.Register[12] + "\n" +
                              "R13 = " + Simulator.Register[13] + "\n" +
                              "R14 = " + Simulator.Register[14] + "\n" +
                              "R15 = " + Simulator.Register[15] + "\n" + "\n" + "\n" +
                              "PC = " + Simulator.PC + "\n" +
                              "ADR = " + Simulator.ADR + "\n" +
                              "MDR = " + Simulator.MDR + "\n" +
                              "SP = " + Simulator.SP + "\n" + "\n" + "\n" +
                              "DBUS = " + Simulator.DBUS + "\n" +
                              "SBUS = " + Simulator.SBUS + "\n" +
                              "RBUS = " + Simulator.RBUS + "\n" + "\n" + "\n" +
                              "SF = " + (Simulator.FLAG & 1) + "\n" +
                              "CF = " + ((Simulator.FLAG >> 1) & 1) + "\n" +
                              "ZF = " + ((Simulator.FLAG >> 2) & 1) + "\n" +
                              "OF = " + ((Simulator.FLAG >> 3) & 1) + "\n";

            if (interruptCheck == false)
            {
                cInterrupt.Checked = false;
            }

            memoryDump = new MemoryDump();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(asmFile == "")
            {
                MessageBox.Show("No input file!");
                return;
            }
            if(parsed == false)
            {
                MessageBox.Show("Parse the file first!");
                return;
            }

            if (assembler.ParseAndTransform(asmFile, binFile))
            {
                
                simulator = new Simulator(binFile);
                compiled = true;

                thread = new Thread(MemoryDump.buildText);
                thread.Start();
                refreshValues();
            }
            else
            {
                MessageBox.Show("Parsin error!");
            }

           
        }

        private void memoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(thread.ThreadState == 0)
            {
                MessageBox.Show("Memory not ready. Try again in 5 seconds!");
            }
            else
            {
                memoryDump = new MemoryDump();
                memoryDump.Show();
            }
           
        }
    }
}
