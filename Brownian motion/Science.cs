using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Brownian_motion
{
    public partial class Science : Form
    {
        public Science()
        {
            InitializeComponent();
        }

        private void Science_Load(object sender, EventArgs e)
        {
            axAcroPDF1.LoadFile("Brown.pdf");
        }
    }
}
