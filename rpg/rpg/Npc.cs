using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace rpg
{
    class Npc
    {
        public int map = -1;
        public int x = 0;
        public int y = 0;
        public int x_offset = -120;
        public int y_offset = -220;

        public string bitmap_path;
        public Bitmap bitmap;
        bool visible = true;

        public void load()
        {
            if (Comm.isNotNullOrEmptyString(bitmap_path))
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }
        }

        public void unload()
        {
            if (bitmap != null)
            {
                bitmap = null;
            }
        }

        public void draw(Graphics g, int map_sx, int map_sy)
        {
            if (!visible)
            {
                return;
            }

            if (bitmap != null)
            {
                g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);
            }
        }
    }
}
