using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brownian_motion
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        ColorDialog Col = new ColorDialog();
        History history = new History();
        Science science = new Science();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                int num = Convert.ToInt32(textBox1.Text);
                if (num >= 500)
                {
                    textBox1.Text = "500";
                    num = 500;
                }

                Form1.Kol_part = num;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Col.ShowDialog() == DialogResult.OK)
                Form1.SlB.Color = Col.Color;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            trackBar1.Value = Form1.D;
            trackBar1.Minimum = 2;
            trackBar1.Maximum = 10;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Form1.D = trackBar1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Col.ShowDialog() == DialogResult.OK)
                Form1.BG = Col.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                history.Show();
            }
            catch
            {
                history = new History();
                history.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                science.Show();
            }
            catch
            {
                science = new Science();
                science.Show();
            }
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8)))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
