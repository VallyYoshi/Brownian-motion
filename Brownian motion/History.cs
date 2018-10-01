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
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }

        Bitmap bitmap = new Bitmap("History/Robert Brown.jpg");

        private void History_Load(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = bitmap;
            richTextBox1.Text = File.ReadAllText("History/History.txt");
        }

        private void History_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
