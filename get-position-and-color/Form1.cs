using System;
using System.Windows.Forms;
using System.Drawing;

namespace get_position_and_color
{
    public partial class Form1 : Form
    {
        private uint pixel;
        private Bitmap myImage;
        private Color color;
        private bool colorType = false;

        public Form1()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                ShowImage();
            }
            if (e.KeyCode == Keys.Down)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                ShowImage();
            }
            if (e.KeyCode == Keys.Left)
            {
                Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                ShowImage();
            }
            if (e.KeyCode == Keys.Right)
            {
                Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                ShowImage();
            }

        }


        
        private void ShowInformation()
        {
            pixel = Win32.GetPixelColor(Cursor.Position.X, Cursor.Position.Y);
            ShowPosition.Text = "x="+ Cursor.Position.X + " y=" + Cursor.Position.Y;
            if (colorType)
            {
                Color arbPixel = Color.FromArgb((int)(pixel & 0x000000FF),
                (int)(pixel & 0x0000FF00) >> 8,
                (int)(pixel & 0x00FF0000) >> 16);
                ShowColor.Text = ConvertColor.ToString(arbPixel);

            }
            else
            { 
                ShowColor.Text = Convert.ToString(pixel);
            }
        }

        private void ShowImage()
        {
            myImage = MakeImage.GetImage();
            pictureBox1.Image = myImage;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowInformation();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ShowImage();
        }

        private void ShowColor_Click(object sender, EventArgs e)
        {
            if (colorType)
                colorType = false;
            else colorType = true;
        }

    }
}


