using System;
using System.Drawing;
using System.Windows.Forms;

namespace RealEstate
{
    public partial class Notification : Form
    {
        public Notification(string msg,string adress, msgType type)
        {
            InitializeComponent();
            Animation.Start();
            switch (type)
            {
                case msgType.success:
                    this.BackColor = Color.SeaGreen;
                    pictureBox1.Image = imageList1.Images[1];
                    MessageBox.BackColor = Color.SeaGreen;
                    MessageBox.Text = msg;
                    break;
                case msgType.error:
                    this.BackColor = Color.Maroon;
                    pictureBox1.Image = imageList1.Images[0];
                    MessageBox.BackColor = Color.Maroon;
                    MessageBox.Text = msg;
                    Erroradress.Text = adress;
                    break;
                case msgType.Information:
                    this.BackColor = Color.Orange;
                    pictureBox1.Image = imageList1.Images[2];
                    MessageBox.BackColor = Color.Orange;
                    MessageBox.Text = msg;
                    break;
            }
        }
        public static void notiShow(string msg,string adress , msgType type)
        {
            new Notification(msg, adress, type).Show();
        }
        
        public enum msgType
        {
            success, error, Information
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close.Start();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            this.Top = 750;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 10;

            Animation.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close.Start();
        }
        int interval;
        private void Animation_Tick(object sender, EventArgs e)
        {
            if(this.Top < 750)
            {
                this.Top += interval;
                interval += 1;
            }
            else
            {
                Animation.Stop();
            }
        }

        private void Close_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.8;
            }
            else
            {
                this.Close();
            }
        }
    }
}
