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
        Player player = new Player();
        int animation_ctrl = 0;
        public Form1()
        {
            InitializeComponent();
            Draw();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
            Graphics g1 = pictureBox1.CreateGraphics();
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
            animation_ctrl += 1;
            player.draw(g, animation_ctrl);
            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("form1.keydown");

            pictureBox1.Refresh();
            player.key_ctrl(e);
            Draw();
        }
    }
}
