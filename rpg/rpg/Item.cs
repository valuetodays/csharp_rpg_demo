using System.Drawing;

namespace rpg
{
    public class Item
    {
        public static Item[] item;
        public int num = 0;
        public string name = "";
        public string description = "";
        public Bitmap bitmap;
        public int isDepletion = 1;
        public int value1 = 0;
        public int value2 = 0;
        public int value3 = 0;
        public int value4 = 0;
        public int value5 = 0;
        public int cost = 100;

        public void set(string name, string description, string bitmap_path, int isDepletion, int value1, int value2,
            int value3, int value4, int value5)
        {
            this.name = name;
            this.description = description;
            if (Comm.isNotNullOrEmptyString(bitmap_path))
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }

            this.isDepletion = isDepletion;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
            this.value5 = value5;
        }

        public delegate void Use_event(Item item);

        public event Use_event use_event;

        public void use()
        {
            if (num <= 0)
            {
                return;
            }

            if (isDepletion != 0)
            {
                num--;
            }

            if (use_event != null)
            {
                use_event(this);
            }
        }

        public static void add_item(int index, int num)
        {
            if (item == null)
            {
                return;
            }

            if (index < 0)
            {
                return;
            }

            if (index >= item.Length)
            {
                return;
            }

            if (item[index] == null)
            {
                return;
            }

            item[index].num += num;
            if (item[index].num < 0)
            {
                item[index].num = 0;
            }
        }

        public static void add_hp(Item item)
        {
            Player p = Form1.player[Player.select_player];
            p.hp += item.value1;
            if (p.hp > p.max_hp)
            {
                p.hp = p.max_hp;
            }

            if (p.hp < 0)
            {
                p.hp = 0;
            }
        }
        public static void add_mp(Item item)
        {
            Player p = Form1.player[Player.select_player];
            p.mp += item.value1;
            if (p.mp > p.max_mp)
            {
                p.mp = p.max_mp;
            }

            if (p.mp < 0)
            {
                p.mp = 0;
            }
        }

        public static void unequip(int type)
        {
            int index;
            if (type == 1)
            {
                index = Form1.player[Player.select_player].equip_att;
                Form1.player[Player.select_player].equip_att = -1;
            } else if (type == 2)
            {
                index = Form1.player[Player.select_player].equip_def;
                Form1.player[Player.select_player].equip_def = -1;
            }
            else
            {
                return;
            }

            if (item == null)
            {
                return;
            }

            if (index < 0)
            {
                return;
            }

            if (index > item.Length)
            {
                return;
            }

            if (item[index] == null)
            {
                return;
            }
            add_item(index, 1);
        }

        public static void equip(Item item0)
        {
            if (item == null)
            {
                return;
            }

            int index = -1;
            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] != null)
                {
                    if (item0.name == item[i].name && item0.description == item[i].description)
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index < 0)
            {
                return;
            }

            if (index > item.Length)
            {
                return;
            }

            if (item[index] == null)
            {
                return;
            }
            
            unequip(item0.value1);
            if (item0.value1 == 1)
            {
                Form1.player[Player.select_player].equip_att = index;
            } else if (item0.value1 == 2)
            {
                Form1.player[Player.select_player].equip_def = index;
            }
            else
            {
                return;
            }
        }

        public static void lpybook(Item item0)
        {
            Message.show("", "传说世界混沌之际，虚实相照，融为一体，几万年过去，粒子稳定，世界渐渐真实，人们早已忘记曾经的虚幻……",
                "", Message.Face.LEFT);
            Task.block();
            Message.show("", "传说本书，如剑锋利，可开创一片虚幻世界，世人争抢，刀兵不断，为了世界和平，作者将本书藏于山中，只待有缘人…",
                "", Message.Face.LEFT);
            Task.block();
        }
    }
}