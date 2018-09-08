using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    class Player
    {
        int x = 50;
        int y = 50;
        int face = 1;
        Bitmap bitmap;

        public Player()
        {
            bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r1.png"));
            bitmap.SetResolution(96, 96);
        }

        public void key_ctrl(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                y -= 5;
                face = 4;
            }
            else if (e.KeyCode == Keys.Down)
            {
                y += 5;
                face = 1;
            }
            else if (e.KeyCode == Keys.Left)
            {
                x -= 5;
                face = 2;
            }
            else if (e.KeyCode == Keys.Right)
            {
                x += 5;
                face = 3;
            }
        }

        public void draw(Graphics g, int animation_ctrl) 
        {
            Rectangle crazycoderRgl = new Rectangle(bitmap.Width / 4 * (animation_ctrl % 4), bitmap.Height / 4 * (face - 1), bitmap.Width / 4, bitmap.Height / 4);
            Bitmap bitmap0 = bitmap.Clone(crazycoderRgl, bitmap.PixelFormat);
            g.DrawImage(bitmap0, x, y);
        }
    }
}
