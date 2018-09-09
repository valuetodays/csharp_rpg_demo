﻿using System;
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
        public string shade_path;
        public Bitmap shade;
        public string block_path;
        public Bitmap block;
        public string back_path;
        public Bitmap back;
        public string music;

        public static void draw(Map[] map, Graphics g)
        {
            Map m = map[current_map];
            g.DrawImage(m.bitmap, 0, 0);
        }

        public static void change_map(Map[] map, Player[] player, Npc[] npc, 
            int newIndex, int x, int y, int face, 
            WMPLib.WindowsMediaPlayer music_player)
        {
            if (map[current_map].bitmap != null)
            {
                map[current_map].bitmap = null;
            }
            if (map[current_map].shade != null)
            {
                map[current_map].shade = null;
            }
            if (map[current_map].block != null)
            {
                map[current_map].block = null;
            }
            if (map[current_map].back != null)
            {
                map[current_map].back = null;
            }

            if (Comm.isNotNullOrEmptyString(map[newIndex].bitmap_path))
            {
                map[newIndex].bitmap = new Bitmap(map[newIndex].bitmap_path);
                map[newIndex].bitmap.SetResolution(96, 96);
            }
            if (Comm.isNotNullOrEmptyString(map[newIndex].shade_path))
            {
                map[newIndex].shade = new Bitmap(map[newIndex].shade_path);
                map[newIndex].shade.SetResolution(96, 96);
            }
            if (Comm.isNotNullOrEmptyString(map[newIndex].block_path))
            {
                map[newIndex].block = new Bitmap(map[newIndex].block_path);
                map[newIndex].block.SetResolution(96, 96);
            }
            if (Comm.isNotNullOrEmptyString(map[newIndex].back_path))
            {
                map[newIndex].back = new Bitmap(map[newIndex].back_path);
                map[newIndex].back.SetResolution(96, 96);
            }

            for (int i = 0; i < npc.Length; i++)
            {
                if (npc[i] != null)
                {
                    if (npc[i].map == current_map)
                    {
                        npc[i].unload();
                    }
                    if (npc[i].map == newIndex)
                    {
                        npc[i].load();
                    }
                }
            }


            current_map = newIndex;

            Player.set_pos(player, x, y, face);

            music_player.URL = map[current_map].music;
        }

        public static void draw(Map[] map, Player[] player, Npc[] npc, Graphics g, Rectangle stage)
        {
            Map m = map[current_map];
            int map_w = m.bitmap.Width;
            int map_h = m.bitmap.Height;
            int p_x = Player.get_pos_x(player);
            int p_y = Player.get_pos_y(player);
            int map_sx = 0;
            int map_sy = 0;

            if (p_x <= stage.Width/2)
            {
                map_sx = 0;
            } else if (p_x >= map_w - stage.Width/2)
            {
                map_sx = stage.Width - map_w;
            } else
            {
                map_sx = stage.Width / 2 - p_x;
            }

            if (p_y <= stage.Height / 2)
            {
                map_sy = 0;
            } else if (p_y >= map_h - stage.Height/2)
            {
                map_sy = stage.Height - map_h;
            } else
            {
                map_sy = stage.Height / 2 - p_y;
            }

            if (m.back != null)
            {
                g.DrawImage(m.back, 0, 0);
            }
            g.DrawImage(m.bitmap, map_sx, map_sy);
            Player.draw(player, g, map_sx, map_sy);
            for (int i = 0; i < npc.Length; i++)
            {
                if (npc[i] != null)
                {
                    if (npc[i].map == current_map)
                    {
                        npc[i].draw(g, map_sx, map_sy);
                    }
                }
            }
            g.DrawImage(m.shade, map_sx, map_sy);
        }

        public static bool can_through(Map[] map, int x, int y)
        {
            Map m = map[current_map];

            if (x < 0)
            {
                return false;
            } else if (x >= m.block.Width)
            {
                return false;
            }
            if (y < 0)
            {
                return false;
            } else if (y >= m.block.Height)
            {
                return false;
            }

            if (m.block.GetPixel(x, y).B == 0)
            {
                return false;
            }
            return true;
        }
    }
}
