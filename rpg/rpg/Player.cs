using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    public class Player
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
        public int x_offset = -120;
        public int y_offset = -220;
        public int collision_ray = 50;

        public static int target_x = -1;
        public static int target_y = -1;
        public static Bitmap move_flag;
        public static long FLAG_SHOW_TIME = 3000;
        public static long flag_start_time = 0;
            
        // 
        public static int select_player = 0;
        public int max_hp = 100;
        public int hp = 100;
        public int max_mp = 100;
        public int mp = 100;
        public int attack = 10;
        public int defense = 10;
        public int fspeed = 10;
        public int fortune = 10;
        public int equip_att = -1;
        public int equip_def = -1;
        public Bitmap status_bitmap;
        public int[] skill = {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
        public static int money = 200;
        
        public enum Status
        {
            WALK = 1,
            PANEL = 2,
            TASK = 3,
            FIGHT = 4,
        }

        public static Status status = Status.WALK;

        public Player()
        {
            bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r1.png"));
            bitmap.SetResolution(96, 96);
            move_flag = new Bitmap(ResourceContextDeterminer.GetAssetPath("move_flag.png"));
            move_flag.SetResolution(96, 96);
        }

        public static void key_ctrl(Player[] player, Map[] map, Npc[] npc, KeyEventArgs e)
        {
            if (Player.status != Status.WALK)
            {
                return;                
            }
            Player p = player[current_player];

            if (e.KeyCode == Keys.Tab)
            {
                key_change_player(player);
            }

            if (e.KeyCode == Keys.Up)
            {
                walk(player, map, Comm.Direction.UP);
            }
            else if (e.KeyCode == Keys.Down)
            {
                walk(player, map, Comm.Direction.DOWN);
            }
            else if (e.KeyCode == Keys.Left)
            {
                walk(player, map, Comm.Direction.LEFT);
            }
            else if (e.KeyCode == Keys.Right)
            {
                walk(player, map, Comm.Direction.RIGHT);
            } else if (e.KeyCode == Keys.Escape)
            {
                StatusMenu.show();
                Task.block();
            }
            npc_collision(player, map, npc, e);
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
            g.DrawImage(bitmap0, map_sx + p.x + p.x_offset, map_sy + p.y + p.y_offset);
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
            stop_walk(player);
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

        public static void walk(Player[] player, Map[] map, Comm.Direction direction)
        {
            Player p = player[current_player];
            p.face = (int)direction;
            if (Comm.Time() - p.last_walk_time <= p.walk_interval)
            {
                return;
            }

            if (direction == Comm.Direction.UP && Map.can_through(map, p.x, p.y - p.speed))
            {
                p.y = p.y - p.speed;
            } else if (direction == Comm.Direction.DOWN && Map.can_through(map, p.x, p.y + p.speed))
            {
                p.y = p.y + p.speed;
            } else if (direction == Comm.Direction.LEFT && Map.can_through(map, p.x - p.speed, p.y))
            {
                p.x = p.x - p.speed;
            } else if (direction == Comm.Direction.RIGHT && Map.can_through(map, p.x + p.speed, p.y))
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

        public static Point get_collision_point(Player[] player)
        {
            Player p = player[current_player];
            int collision_x = 0;
            int collision_y = 0;
            collision_x = p.x;
            collision_y = p.y;

            if (p.face == (int)Comm.Direction.UP)
            {
                collision_y = p.y - p.collision_ray;
            } else if (p.face == (int)Comm.Direction.DOWN) 
            {
                collision_y = p.y + p.collision_ray;
            } else if (p.face == (int)Comm.Direction.LEFT)
            {
                collision_x = p.x - p.collision_ray;
            } else if (p.face == (int)Comm.Direction.RIGHT)
            {
                collision_x = p.x + p.collision_ray;
            }
            return new Point(collision_x, collision_y);
        }

        public static void npc_collision(Player[] player, Map[] map, Npc[] npc, KeyEventArgs e)        {
            Player p = player[current_player];
            Point p1 = new Point(p.x, p.y);
            Point p2 = get_collision_point(player);

            for (int i = 0; i < npc.Length; i++)
            {
                Npc n = npc[i];
                Console.WriteLine(">>" + i);
                if (n == null)
                {
                    continue;
                }
                if (n.map != Map.current_map)
                {
                    continue;
                }
                
                if (n.is_line_collision(p1, p2))
                {
                    if (n.collision_type == Npc.Collision_type.ENTER)
                    {
                        
                        Task.story(i);
                        break;
                    } else if (n.collision_type == Npc.Collision_type.KEY)
                    {
                        if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                        {
                            Task.story(i);
                            break;
                        }
                    }
                }
            }
        } // end of method 

        public static int is_reach_x(Player[] player, int target_x)
        {
            Player p = player[current_player];
            if (p.x - target_x > p.speed / 2)
            {
                return 1;
            }

            if (p.x - target_x < -p.speed/2)
            {
                return -1;
            }

            return 0; // 到达目的地
        }

        public static int is_reach_y(Player[] player, int target_y)
        {
            Player p = player[current_player];
            if (p.y - target_y > p.speed/2)
            {
                return 1;
            }

            if (p.y - target_y < -p.speed/2)
            {
                return -1;
            }

            return 0;
        }

        public static void mouse_click(Map[] map, Player[] player, Rectangle stage, MouseEventArgs e)
        {
            if (Player.status != Status.WALK)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                target_x = e.X - Map.get_map_sx(map, player, stage);
                target_y = e.Y - Map.get_map_sy(map, player, stage);
                flag_start_time = Comm.Time();
            } else if (e.Button == MouseButtons.Right)
            {
                StatusMenu.show();
                Task.block();
            }
        }

        public static void timer_logic(Player[] player, Map[] map)
        {
            move_logic(player, map);
        }

        public static void move_logic(Player[] player, Map[] map)
        {
            if (target_x < 0 || target_y < 0)
            {
                return;      
            }

            step_to(player, map, target_x, target_y);
        }

        public static void stop_walk(Player[] player)
        {
            Player p = player[current_player];
            p.anm_frame = 0;
            p.last_walk_time = 0;

            target_x = -1;
            target_y = -1;
        }

        public static void step_to(Player[] player, Map[] map, int target_x, int target_y)
        {
            if (is_reach_x(player, target_x) == 0
                && is_reach_y(player, target_y) == 0)
            {
                stop_walk(player);
                return;
            }

            Player p = player[current_player];
            if (is_reach_x(player, target_x)>0 && Map.can_through(map, p.x - p.speed, p.y))
            {
                walk(player, map, Comm.Direction.LEFT);
                return;
            }
            if (is_reach_x(player, target_x)<0 && Map.can_through(map, p.x + p.speed, p.y))
            {
                walk(player, map, Comm.Direction.RIGHT);
                return;
            }
            if (is_reach_y(player, target_x)>0 && Map.can_through(map, p.x, p.y - p.speed))
            {
                walk(player, map, Comm.Direction.UP);
                return;
            }
            if (is_reach_y(player, target_x)<0 && Map.can_through(map, p.x, p.y + p.speed))
            {
                walk(player, map, Comm.Direction.DOWN);
                return;
            }
            stop_walk(player);
        }

        public static void draw_flag(Graphics g, int map_sx, int map_sy)
        {
            if (target_x < 0 || target_y < 0)
            {
                return;
            }

            if (move_flag == null)
            {
                return;
            }

            if (Comm.Time() - flag_start_time > FLAG_SHOW_TIME)
            {
                return;
            }
            g.DrawImage(move_flag, map_sx + target_x - 16, map_sy + target_y - 25);
            
        }
        
    } // end of class
}
