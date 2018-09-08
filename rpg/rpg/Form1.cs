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
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
            Bitmap bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("role.png"));
            bitmap.SetResolution(96, 96);
            Graphics g1 = pictureBox1.CreateGraphics();
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
            g.DrawImage(bitmap, x, y);
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
            } else if (e.KeyCode ==  Keys.Down)
            {
                y += 5;
            } else if (e.KeyCode == Keys.Left)
            {
                x -= 5;
            } else if (e.KeyCode == Keys.Right)
            {
                x += 5;
            }
            Draw();
        }
    }
}
