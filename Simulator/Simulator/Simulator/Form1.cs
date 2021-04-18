using System;
using System.Windows.Forms;

namespace Simulator
{
    public partial class Form1 : Form
    {
        private string asmFile = "";
        private string binFile = "";
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
    }
}
