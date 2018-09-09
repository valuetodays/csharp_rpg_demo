using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    class Player
    {
        public int x = 50;
        public int y = 50;
        public int face = 1;
        public Bitmap bitmap;
        public static int current_player = 0;
        public int is_active = 0;

        public Player()
        {
            bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r1.png"));
            bitmap.SetResolution(96, 96);
        }

        public static void key_ctrl(Player[] player, KeyEventArgs e)
        {
            Player p = player[current_player];

            if (e.KeyCode == Keys.Tab)
            {
                key_change_player(player);
            }

            if (e.KeyCode == Keys.Up)
            {
                p.y -= 5;
                p.face = 4;
            }
            else if (e.KeyCode == Keys.Down)
            {
                p.y += 5;
                p.face = 1;
            }
            else if (e.KeyCode == Keys.Left)
            {
                p.x -= 5;
                p.face = 2;
            }
            else if (e.KeyCode == Keys.Right)
            {
                p.x += 5;
                p.face = 3;
            }
        }

        public static void draw(Player[] player, Graphics g, int animation_ctrl) 
        {
            Player p = player[current_player];

            Rectangle crazycoderRgl = new Rectangle(p.bitmap.Width / 4 * (animation_ctrl % 4), 
                p.bitmap.Height / 4 * (p.face - 1), 
                p.bitmap.Width / 4, 
                p.bitmap.Height / 4);
            Bitmap bitmap0 = p.bitmap.Clone(crazycoderRgl, p.bitmap.PixelFormat);
            g.DrawImage(bitmap0, p.x, p.y);
        }


        public static void key_change_player(Player[] player)
        {
            for (int i = current_player+1; i < player.Length; i++)
            {
                if (player[i].is_active == 1)
                {
                    set_player(player, current_player, i);
                    return;
                }
            }

            for (int i = 0; i < current_player; i++)
            {
                if (player[i].is_active == 1)
                {
                    set_player(player, current_player, i);
                }
            }
        }

        private static void set_player(Player[] player, int oldIndex, int newIndex)
        {
            current_player = newIndex;
            player[newIndex].x = player[oldIndex].x;
            player[newIndex].y = player[oldIndex].y;
            player[newIndex].face = player[oldIndex].face;
        }
    }
}
