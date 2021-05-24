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
        public MemoryDump()
        {
            InitializeComponent();
        }

        private void MemoryDump_Load(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Text = string.Join(" ", Simulator.Memory.Select(x => x.ToString("X2")));

        }
    }
}
