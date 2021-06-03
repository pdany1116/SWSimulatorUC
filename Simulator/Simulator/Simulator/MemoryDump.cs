using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    public partial class MemoryDump : Form
    {
        public static string text;
        public MemoryDump()
        {
            InitializeComponent();
            
        }
        
        public static void buildText()
        {
            
            for (int i = 0; i < 2048; i++)
            {
                text = text + String.Format("{0:X5}", i * 32) + " - " + String.Format("{0:X5}", i * 32 + 31) + ": ";
                //textBox1.Text = string.Join(textBox1.Text, String.Format("{0:X5}", i * 64), " - ", String.Format("{0:X5}", i * 64 + 64), ": ");
                for (int j = 0; j < 32; j++)
                {
                    text = text + Simulator.Memory[32 * i + j].ToString("X2") + " ";
                    //textBox1.Text = string.Join(textBox1.Text, Simulator.Memory[32 * i + j].ToString("X2"), " ");
                }
                text = text + Environment.NewLine;
                //textBox1.Text = string.Join(textBox1.Text, Environment.NewLine);
            }

            
        }

        private void MemoryDump_Load(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Text = string.Join(" ", Simulator.Memory.Select(x => x.ToString("X2")));

           // textBox1.Text = text;

        }
    }
}
