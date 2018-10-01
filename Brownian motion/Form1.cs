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
    public partial class Form1 : Form
    {

        class Particles
        {
            public double x;
            public double y;
            public double NX;
            public double NY;
        }

        SettingForm set = new SettingForm();
        static Random rand = new Random();
        Graphics graph;
        BufferedGraphics buffer;
        BufferedGraphicsContext cont;       
        static Particles[] MassPart;
        static public int Kol_part = 500;
        static public SolidBrush SlB = new SolidBrush(Color.Cyan);
        static public Color BG = Color.Black;
        static public int D = 7;
      

        public Form1()
        {
            InitializeComponent();
            FullScrean();

            set = new SettingForm();
            ControlBox = false;
        }

        public void FullScrean()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                ControlBox = true;
            }
        }
        

        // Массив из частиц
        public void RandMassPart()
        {
            MassPart = new Particles[Kol_part];

            for (int i = 0; i < Kol_part; i++)
            {
                MassPart[i] = new Particles();

                MassPart[i].x = rand.Next(D, pictureBox1.Width - D);
                MassPart[i].y = rand.Next(D, pictureBox1.Height - D);
                MassPart[i].NX = rand.Next(-1, 1);
                if (MassPart[i].NX == 0)
                    MassPart[i].NX = 1;
                MassPart[i].NY = MassPart[i].NX;
            }
        }
    

        //Подготовка к отрисовке
        public void draw()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Timer timer = new Timer();
            cont = BufferedGraphicsManager.Current;
            graph = pictureBox1.CreateGraphics();
            buffer = cont.Allocate(graph, pictureBox1.DisplayRectangle);
            graph = Graphics.FromImage(bmp);

            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            timer.Start();
            pictureBox1.BackgroundImage = bmp;
        }
        

        //Столкнавения
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                buffer.Graphics.Clear(BG);

                for (int i = 0; i < Kol_part; i++)
                {
                    buffer.Graphics.FillEllipse(SlB, (int)MassPart[i].x, (int)MassPart[i].y, D, D);

                    for (int j = 0; j < Kol_part; j++)
                    {
                        if (i != j)
                        {
                            double dd = (MassPart[i].x - MassPart[j].x) * (MassPart[i].x - MassPart[j].x) +
                                    (MassPart[i].y - MassPart[j].y) * (MassPart[i].y - MassPart[j].y);
                            double d_ = D * D;
                            if (dd < d_)
                            {
                                double d = Math.Sqrt(dd);
                                double inv = 1.0 / d / 5;
                                MassPart[i].NX += (MassPart[i].x - MassPart[j].x) * inv;
                                MassPart[i].NY += (MassPart[i].y - MassPart[j].y) * inv;
                                MassPart[j].NX -= (MassPart[i].x - MassPart[j].x) * inv;
                                MassPart[j].NY -= (MassPart[i].y - MassPart[j].y) * inv;
                            }
                        }
                    }

                    MassPart[i].x += MassPart[i].NX;
                    MassPart[i].y += MassPart[i].NY;

                    if (MassPart[i].x <= 0)
                        MassPart[i].NX *= -1;

                    if (MassPart[i].x + D >= pictureBox1.Width)
                        MassPart[i].NX *= -1;

                    if (MassPart[i].y <= 0)
                        MassPart[i].NY *= -1;

                    if (MassPart[i].y + D >= pictureBox1.Height)
                        MassPart[i].NY *= -1;
                }
                buffer.Render();
            }
            catch
            {
                RandMassPart();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            set.Show();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            RandMassPart();
            draw();

            if (ControlBox == true)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    ControlBox = false;
                    FormBorderStyle = FormBorderStyle.None;
                }
            }
        }
    }
}
