using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    public partial class Form1 : Form
    {
        int x = 50, y = 50;
        int face = 1;
        int animation_ctrl = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
            Bitmap bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r1.png"));
            bitmap.SetResolution(96, 96);
            Graphics g1 = pictureBox1.CreateGraphics();
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
            animation_ctrl += 1;
            Rectangle crazycoderRgl = new Rectangle(bitmap.Width/4*(animation_ctrl%4), bitmap.Height/4*(face-1), bitmap.Width/4, bitmap.Height/4);
            Bitmap bitmap0 = bitmap.Clone(crazycoderRgl, bitmap.PixelFormat);
            g.DrawImage(bitmap0, x, y);
            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("for1.keydown");

            pictureBox1.Refresh();
            if (e.KeyCode == Keys.Up)
            {
                y -= 5;
                face = 4;
            } else if (e.KeyCode ==  Keys.Down)
            {
                y += 5;
                face = 1;
            } else if (e.KeyCode == Keys.Left)
            {
                x -= 5;
                face = 2;
            } else if (e.KeyCode == Keys.Right)
            {
                x += 5;
                face = 3;
            }
            Draw();
        }
    }
}
