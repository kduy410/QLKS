using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class SplashScreenForm : Form
    {
        Timer t = new Timer();

        //pb = ProgressBar
        double pbUnit;
        int pbWIDTH, pbHEIGHT, pbComplete;

        Bitmap bmp;
        Graphics g;

        private void SplashScreenForm_Load(object sender, EventArgs e)
        {
            //get picboxPB dimension
            pbWIDTH = picboxPB.Width;
            pbHEIGHT = picboxPB.Height;

            pbUnit = pbWIDTH / 100.0;

            //pbComplete - This is equal to work completed in % [min = 0 max = 100]
            pbComplete = 0;

            //create bitmap
            bmp = new Bitmap(pbWIDTH, pbHEIGHT);

            //timer
            t.Interval = 50;    //in millisecond
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        public SplashScreenForm()
        {
            InitializeComponent();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            //color
            Color c = Color.FromArgb(229, 229, 190);
            //brush
            SolidBrush brushes = new SolidBrush(c);
            //graphics
            g = Graphics.FromImage(bmp);

            //clear graphics
            g.Clear(Color.Black);

            //draw progressbar
            g.FillRectangle(brushes, new Rectangle(0, 0, (int)(pbComplete * pbUnit), pbHEIGHT));

            //draw % complete
            g.DrawString(pbComplete + "%", new Font("Segoe UI", pbHEIGHT / 2), Brushes.BlueViolet, new PointF(pbWIDTH / 2 - pbHEIGHT, pbHEIGHT / 10));

            //load bitmap in picturebox picboxPB
            picboxPB.Image = bmp;

            //update pbComplete
            //Note!
            //To keep things simple I am adding +1 to pbComplete every 50ms
            //You can change this as per your requirement :)
            pbComplete+=5;
            if (pbComplete > 100)
            {
                //dispose
                g.Dispose();
                t.Stop();
                //
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
        }
    }
}

