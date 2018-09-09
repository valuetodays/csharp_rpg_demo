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
        Player[] player = new Player[3];
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            player[0] = new rpg.Player();
            player[0].bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r1.png"));
            player[0].bitmap.SetResolution(96, 96);
            player[0].is_active = 1;

            player[1] = new rpg.Player();
            player[1].bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r2.png"));
            player[1].bitmap.SetResolution(96, 96);
            player[1].is_active = 1;

            player[2] = new rpg.Player();
            player[2].bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r3.png"));
            player[2].bitmap.SetResolution(96, 96);
            player[2].is_active = 1;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
            Graphics g1 = stage.CreateGraphics();
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
            Player.draw(player, g);
            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("form1.keydown");

            stage.Refresh();
            Player.key_ctrl(player, e);
            Draw();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Player.key_ctrl_up(player, e);
            Draw();
        }
    }
}
