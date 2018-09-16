using System.Drawing;
using System.Windows.Forms;

namespace rpg
{
    public class Skill
    {
        public static Skill[] skill;

        public int mp = 10;
        public string name = "";
        public string description = "";
        public Bitmap bitmap;

        public int value1;
        public int value2;
        public int value3;
        public int value4;
        public int value5;

        public void set(string name, string description, string bitmap_path, int mp, int value1, int value2, int value3,
            int value4, int value5)
        {
            this.name = name;
            this.description = description;
            if (Comm.isNotNullOrEmptyString(bitmap_path))
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }

            this.mp = mp;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
            this.value5 = value5;
        }

        public delegate void Use_event(Skill skill);
        public event Use_event use_event;
        public void use()
        {
            if (Form1.player[Player.select_player].mp < mp)
            {
                return;
            }

            Form1.player[Player.select_player].mp -= mp;
            if (use_event != null)
            {
                use_event(this);
            }
        }

        public static void learn_skill(int player_index, int index, int type)
        {
            if (skill == null || index < 0 || index >= skill.Length || skill[index] == null)
            {
                return;
            }

            if (type == 0)
            {
                for (int i = 0; i < Form1.player[player_index].skill.Length;i++)
                {
                    if (Form1.player[player_index].skill[i] == index)
                    {
                        Form1.player[player_index].skill[i] = -1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Form1.player[player_index].skill.Length; i++)
                {
                    if (Form1.player[player_index].skill[i] == index) // 已经学会该技能
                    {
                        return;
                    }
                }

                for (int i = 0; i < Form1.player[player_index].skill.Length; i++)
                {
                    if (Form1.player[player_index].skill[i] == -1) // 如果是空，就学习技能
                    {
                        Form1.player[player_index].skill[i] = index;
                        return;
                    }
                }
            }
        }

        public static void add_hp(Skill skill0)
        {
            Player player = Form1.player[Player.select_player];
            player.hp += skill0.value1;
            if (player.hp > player.max_hp)
            {
                player.hp = player.max_hp;
            }

            if (player.hp < 0)
            {
                player.hp = 0;
            }
        }
    }
}