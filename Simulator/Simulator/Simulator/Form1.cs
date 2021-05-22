using System;
using System.IO;
using System.Windows.Forms;

namespace Simulator
{
    public partial class Form1 : Form
    {
        
        private string asmFile = "";
        private string binFile = "";
        Simulator simulator;


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
            Assembler assembler = new Assembler();
            if (!assembler.ParseAndTransform(asmFile, binFile))
            {
                LOG(assembler.m_errorMessage);
                LOG("Failed to parse: " + asmFile);
                return;
            }
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

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            simulator = new Simulator(binFile);

        }

        private void stepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Simulator.instructionCode == 57357) //HALT 
            {
                MessageBox.Show("END");
            }
            else if(simulator.compiled == true)
            {
                simulator.impulseGenerator();
                LOG(Simulator.currentPhase + " " + Simulator.currentImpulse);
            }
            else
            {
                LOG("Program not compiled!!");
            }

            Refresh();
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void runUntilEndToolStripMenuItem_Click(object sender, EventArgs e)
        {



            while(Simulator.instructionCode != 57357) //HALT 
            {
                if (simulator.compiled == true)
                {
                    simulator.impulseGenerator();
                    LOG(Simulator.currentPhase + " " + Simulator.currentImpulse);
                }

                
            
            }
            
            MessageBox.Show("END");

            Refresh();

        }
    }
}
