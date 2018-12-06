using System.Drawing;
using System.Windows.Forms;

namespace rpg
{
    public class Fight
    {
        private struct Fenemy
        {
            public int id;
            public int hp;
            public int order;
            public int status;
        };
        
        private static Fenemy[] enemy = new Fenemy[3];

        private struct Fplayer
        {
            public int id;
            public int hp;
            public int mp;
            public int order;
            public int status;
        };
        private static Fplayer[] player = new Fplayer[3];

        private static Bitmap bg;
        private static int isgameover = 1;
        private static int winitem1 = -1;
        private static int winitem2 = -1;
        private static int winitem3 = -1;
        private static int lostmoney = 0;

        public static int isWin = 0;
        public static Player.Status player_last_status = Player.Status.WALK;
        public static int fighting = 0;

        public enum PLAYER_PTY
        {
            ATT = 1,
            DEF = 2,
            SPD = 3,
            FTE = 4
        };

        public static void start(int[] enemy,
            string bg_path, int isgameover,
            int winitem1, int winitem2, int winitem3,
            int losemoney)
        {
            if (enemy.Length < 3)
            {
                MessageBox.Show("enemy数组长度小于3");
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                if (enemy[i] > Enemy.enemy.Length)
                {
                    MessageBox.Show("enemy值大于敌人最大id");
                    return;
                }

                if (enemy[i] != -1 && Enemy.enemy[enemy[i]] == null)
                {
                    MessageBox.Show("敌人id" + enemy[i].ToString() + "未定义");
                    return;
                }

                Fight.enemy[i].id = enemy[i];
                if (Fight.enemy[i].id != -1)
                {
                    Fight.enemy[i].hp = Enemy.enemy[enemy[i]].maxhp;
                    Fight.enemy[i].order = -1 * Enemy.enemy[enemy[i]].fspeed;
                    Fight.enemy[i].status = 0;
                }
                else
                {
                    Fight.enemy[i].hp = 0;
                    Fight.enemy[i].order = -1;
                    Fight.enemy[i].status = 0;
                }
            }
            // bg
            if (Comm.isNotNullOrEmptyString(bg_path))
            {
                Fight.bg = new Bitmap(ResourceContextDeterminer.GetAssetPath(bg_path));
                Fight.bg.SetResolution(96, 96);
            }

            Fight.isgameover = isgameover;
            Fight.winitem1 = winitem1;
            Fight.winitem2 = winitem2;
            Fight.winitem3 = winitem3;
            Fight.lostmoney = losemoney;
            
            // player
            int[] player = get_fplayer();
            for (int i = 0; i < 3; i++)
            {
                if (Fight.player[i].id != -1)
                {
                    Fight.player[i].hp = Form1.player[player[i]].hp;
                    Fight.player[i].mp = Form1.player[player[i]].mp;
                    Fight.player[i].order = -1 * get_property(PLAYER_PTY.SPD, player[i]);
                    Fight.player[i].status = 0;
                }
                else
                {
                    Fight.player[i].hp = 0;
                    Fight.player[i].mp = 0;
                    Fight.player[i].order = -1;
                    Fight.player[i].status = 0;
                }
            }

            if (Player.status != Player.Status.FIGHT)
            {
                player_last_status = Player.status;
            }

            Player.status = Player.Status.FIGHT;
            fighting = 1;
        }

        private static int get_property(PLAYER_PTY pty, int index)
        {
            if (player[index].id < 0)
            {
                return 0;
            }

            if (player[index].id >= Form1.player.Length)
            {
                return 0;
            }

            Player p = Form1.player[player[index].id];
            if (p == null)
            {
                return 0;
            }

            
            int value = 0;
            if (pty == PLAYER_PTY.ATT)
            {
                value += p.attack;
            } else if (pty == PLAYER_PTY.DEF)
            {
                value += p.defense;
            } else if (pty == PLAYER_PTY.SPD)
            {
                value += p.fspeed;
            } else if (pty == PLAYER_PTY.FTE)
            {
                value += p.fortune;
            }

            if (p.equip_att >= 0)
            {
                if (pty == PLAYER_PTY.ATT)
                {
                    value += Item.item[p.equip_att].value2;
                } else if (pty == PLAYER_PTY.DEF)
                {
                    value += Item.item[p.equip_att].value3;
                } else if (pty == PLAYER_PTY.SPD)
                {
                    value += Item.item[p.equip_att].value4;
                } else if (pty == PLAYER_PTY.FTE)
                {
                    value += Item.item[p.equip_att].value5;
                }
            }

            if (p.equip_def >= 0)
            {
                if (pty == PLAYER_PTY.ATT)
                {
                    value += Item.item[p.equip_def].value2;
                } else if (pty == PLAYER_PTY.DEF)
                {
                    value += Item.item[p.equip_def].value3;
                } else if (pty == PLAYER_PTY.SPD)
                {
                    value += Item.item[p.equip_def].value4;
                } else if (pty == PLAYER_PTY.FTE)
                {
                    value += Item.item[p.equip_def].value5;
                }
            }

            return value;
        }

        public static void draw(Graphics g)
        {
            
        }


        private static int[] get_fplayer()
        {
            int[] ret = new int[] { -1, -1, -1};
            int start = Player.current_player;
            int start2 = 0;
            int end = Player.current_player;

            for (int i = 0; i < 3; i++)
            {
                // 前遍历
                int j = 0;
                for (j = start; j < Form1.player.Length; j++)
                {
                    if (Form1.player[j].is_active == 1)
                    {
                        ret[i] = j;
                        start = j + 1;
                        break;
                    }
                }

                if (j < Form1.player.Length)
                {
                    continue;
                }
                // 后遍历
                for (j = start2; j < end; j++)
                {
                    if (Form1.player[j].is_active == 1)
                    {
                        ret[i] = j;
                        start2 = j + 1;
                        break;
                    }
                }
            }

            return ret;
        }
    }
}