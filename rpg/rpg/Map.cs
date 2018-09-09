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
            // 不播放音乐了
           //  music_player.URL = map[current_map].music;
        }

        public static void draw(Map[] map, Player[] player, Npc[] npc, Graphics g, Rectangle stage)
        {
            Map m = map[current_map];
            int map_w = m.bitmap.Width;
            int map_h = m.bitmap.Height;
            int p_x = Player.get_pos_x(player);
            int p_y = Player.get_pos_y(player);
            int map_sx = get_map_sx(map, player, stage);
            int map_sy = get_map_sy(map, player, stage);

            if (m.back != null)
            {
                g.DrawImage(m.back, 0, 0);
            }
            g.DrawImage(m.bitmap, map_sx, map_sy);
            draw_player_npc(map, player, npc, g, map_sx, map_sy);
            g.DrawImage(m.shade, map_sx, map_sy);
        }

        private static void draw_player_npc(Map[] map, Player[] player, Npc[] npc, Graphics g, int map_sx, int map_sy)
        {
            Layer_sort[] layer_sort = new Layer_sort[npc.Length + 1];

            for (int i = 0; i < npc.Length; i++)
            {
                layer_sort[i] = new Layer_sort();
                if (npc[i] != null)
                {
                    layer_sort[i].y = npc[i].y;
                } else
                {
                    layer_sort[i].y = int.MaxValue;
                }
                layer_sort[i].index = i;
                layer_sort[i].type = 1;
            }
            layer_sort[npc.Length] = new Layer_sort();
            layer_sort[npc.Length].y = Player.get_pos_y(player);
            layer_sort[npc.Length].index = 0;
            layer_sort[npc.Length].type = 0;

            System.Array.Sort(layer_sort, new Layer_sort_comparer());

            for (int i = 0; i < layer_sort.Length; i++)
            {
                if (layer_sort[i].type == 0)
                {
                    Player.draw(player, g, map_sx, map_sy);
                }
                else if (layer_sort[i].type == 1)
                {
                    int index = layer_sort[i].index;
                    if (npc[index] != null && npc[index].map == current_map)
                    {
                        npc[index].draw(g, map_sx, map_sy);
                    }
                }
            }
        }

        private static int get_map_sy(Map[] map, Player[] player, Rectangle stage)
        {
            Map m = map[current_map];
            if (m.bitmap == null)
            {
                return 0;
            }
            int map_h = m.bitmap.Height;
            int p_y = Player.get_pos_y(player);


            int map_sy = 0;
            if (p_y <= stage.Height / 2)
            {
                map_sy = 0;
            }
            else if (p_y >= map_h - stage.Height / 2)
            {
                map_sy = stage.Height - map_h;
            }
            else
            {
                map_sy = stage.Height / 2 - p_y;
            }
            return map_sy;
        }

        private static int get_map_sx(Map[] map, Player[] player, Rectangle stage)
        {
            Map m = map[current_map];
            if (m.bitmap == null)
            {
                return 0;
            }
            int map_w = m.bitmap.Width;
            int p_x = Player.get_pos_x(player);
            int map_sx = 0;

            if (p_x <= stage.Width / 2)
            {
                map_sx = 0;
            }
            else if (p_x >= map_w - stage.Width / 2)
            {
                map_sx = stage.Width - map_w;
            }
            else
            {
                map_sx = stage.Width / 2 - p_x;
            }

            return map_sx;
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
