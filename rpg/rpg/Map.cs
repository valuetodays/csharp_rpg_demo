using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace rpg
{
    class Map
    {
        public static int current_map = 0;
        public string bitmap_path;
        public Bitmap bitmap;

        public static void draw(Map[] map, Graphics g)
        {
            Map m = map[current_map];
            g.DrawImage(m.bitmap, 0, 0);
        }

        public static void change_map(Map[] map, Player[] player, int newIndex, int x, int y, int face)
        {
            if (map[current_map].bitmap != null)
            {
                map[current_map].bitmap = null;
            }
            map[newIndex].bitmap = new Bitmap(map[newIndex].bitmap_path);
            map[newIndex].bitmap.SetResolution(96, 96);

            current_map = newIndex;

            Player.set_pos(player, x, y, face);
        }
    }
}
