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
        public int anm_frame = 0;
        public long last_walk_time = 0;
        public long walk_interval = 100;
        public int speed = 20;
        public Bitmap bitmap;
        public static int current_player = 0;
        public int is_active = 0;

        public Player()
        {
            bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r1.png"));
            bitmap.SetResolution(96, 96);
        }

        public static void key_ctrl(Player[] player, Map[] map, KeyEventArgs e)
        {
            Player p = player[current_player];

            if (e.KeyCode == Keys.Tab)
            {
                key_change_player(player);
            }

            if (e.KeyCode == Keys.Up && p.face != 4)
            {
                p.face = 4;
            }
            else if (e.KeyCode == Keys.Down && p.face != 1)
            {
                p.face = 1;
            }
            else if (e.KeyCode == Keys.Left && p.face != 2)
            {
                p.face = 2;
            }
            else if (e.KeyCode == Keys.Right && p.face != 3)
            {
                p.face = 3;
            }
            // 间隔判定
            if (e.KeyCode == Keys.Up && Map.can_through(map, p.x, p.y - p.speed))
            {
                p.y = p.y - p.speed;
            } else if (e.KeyCode == Keys.Down && Map.can_through(map, p.x, p.y + p.speed))
            {
                p.y = p.y + p.speed;
            } else if (e.KeyCode == Keys.Left && Map.can_through(map, p.x - p.speed, p.y))
            {
                p.x = p.x - p.speed;
            } else if (e.KeyCode == Keys.Right && Map.can_through(map, p.x + p.speed, p.y))
            {
                p.x = p.x + p.speed;
            }

            p.anm_frame++;
            if (p.anm_frame >= int.MaxValue)
            {
                p.anm_frame = 0;
            }
            p.last_walk_time = Comm.Time();
        }

        public static void set_pos(Player[] player, int x, int y, int face)
        {
            player[current_player].x = x;
            player[current_player].y = y;
            player[current_player].face = face;
        }

        public static void draw(Player[] player, Graphics g, int map_sx, int map_sy)
        {
            Player p = player[current_player];

            Rectangle crazycoderRgl = new Rectangle(p.bitmap.Width / 4 * (p.anm_frame % 4),
                p.bitmap.Height / 4 * (p.face - 1),
                p.bitmap.Width / 4,
                p.bitmap.Height / 4);
            Bitmap bitmap0 = p.bitmap.Clone(crazycoderRgl, p.bitmap.PixelFormat);
            g.DrawImage(bitmap0, map_sx + p.x, map_sy + p.y);
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

        public static void key_ctrl_up(Player[] player, KeyEventArgs e)
        {
            Player p = player[current_player];
            p.anm_frame = 0;
            p.last_walk_time = 0;
        }

        public static int get_pos_x(Player[] player)
        {
            return player[current_player].x;
        }

        public static int get_pos_y(Player[] player)
        {
            return player[current_player].y;
        }

        public static int get_pos_face(Player[] player)
        {
            return player[current_player].face;
        }
    }
}
