using System.Drawing;
using System.Windows.Forms;

namespace rpg
{
    public class StatusMenu
    {
        public static Panel status = new Panel();
        public static int menu = 0; // 0-物品，1-技能
        public static Bitmap bitmap_menu_item;
        public static Bitmap bitmap_menu_equip;

        public static int page = 1;
        public static int selnow = 1;
        public static Bitmap bitmap_sel;

        public static void init()
        {
            bitmap_menu_item = new Bitmap(ResourceContextDeterminer.GetAssetPath("item/sbt2_1.png"));
            bitmap_menu_item.SetResolution(96, 96);
            bitmap_menu_equip = new Bitmap(ResourceContextDeterminer.GetAssetPath("item/sbt2_2.png"));
            bitmap_menu_equip.SetResolution(96, 96);
            bitmap_sel = new Bitmap(ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"));
            bitmap_sel.SetResolution(96, 96);
            
            Panel.Button equip_att = new Panel.Button();
            equip_att.set(41, 55, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt9_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt9_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt9_2.png"),
                -1, -1, -1, -1);
            equip_att.click_event += new Panel.Button.Click_event(click_equip_att);
            
            Panel.Button equip_def = new Panel.Button();
            equip_def.set(41, 135, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt9_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt9_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt9_2.png"),
                -1, -1, -1, -1);
            equip_def.click_event += new Panel.Button.Click_event(click_equip_def);
            
            Panel.Button next_player = new Panel.Button();
            next_player.set(305, 296, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt1_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt1_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt1_2.png"),
                -1, -1, -1, -1);
            next_player.click_event += new Panel.Button.Click_event(click_next_player);
            
            Panel.Button item_menu = new Panel.Button();
            item_menu.set(634, 66, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt10.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt10.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt10.png"),
                -1, -1, -1, -1);
            item_menu.click_event  += new Panel.Button.Click_event(click_item_menu);
            
            Panel.Button skill_menu = new Panel.Button();
            skill_menu.set(634, 173, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt10.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt10.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt10.png"),
                -1, -1, -1, -1);
            skill_menu.click_event  += new Panel.Button.Click_event(click_skill_menu);         
            
            Panel.Button previous_page = new Panel.Button();
            previous_page.set(372, 326, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt3_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt3_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt3_2.png"),
                -1, -1, -1, -1);
            previous_page.click_event += new Panel.Button.Click_event(click_previous_page);   
            
            Panel.Button next_page = new Panel.Button();
            next_page.set(523, 326, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt5_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt5_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt5_2.png"),
                -1, -1, -1, -1);
            next_page.click_event += new Panel.Button.Click_event(click_next_page);
            
            Panel.Button use = new Panel.Button();
            use.set(447, 326, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt4_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt4_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt4_2.png"),
                -1, -1, -1, -1);
            use.click_event += new Panel.Button.Click_event(click_use);
            
            Panel.Button close = new Panel.Button();
            close.set(627, 16, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt6_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt6_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt6_2.png"),
                -1, -1, -1, -1);
            close.click_event += new Panel.Button.Click_event(click_close);
            
            Panel.Button sel1 = new Panel.Button();
            sel1.set(350, 38, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt7_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                -1, -1, -1, -1);
            sel1.click_event += new Panel.Button.Click_event(click_sel1);
            
            Panel.Button sel2 = new Panel.Button();
            sel2.set(350, 133, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt7_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                -1, -1, -1, -1);
            sel2.click_event += new Panel.Button.Click_event(click_sel2);
            
            Panel.Button sel3 = new Panel.Button();
            sel3.set(350, 228, 0, 0, ResourceContextDeterminer.GetAssetPath("item/sbt7_1.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                -1, -1, -1, -1);
            sel3.click_event += new Panel.Button.Click_event(click_sel3);
            
            Panel.Button under = new Panel.Button();
            under.set(-100, -100, 2000, 2000, "", "", "", -1, -1, -1, -1);
            
            status.button = new Panel.Button[13];
            status.button[0] = equip_att;
            status.button[1] = equip_def;
            status.button[2] = next_player;
            status.button[3] = item_menu;
            status.button[4] = skill_menu;
            status.button[5] = previous_page;
            status.button[6] = next_page;
            status.button[7] = use;
            status.button[8] = close;
            status.button[9] = sel1;
            status.button[10] = sel2;
            status.button[11] = sel3;
            status.button[12] = under;
            status.set(58, 71, ResourceContextDeterminer.GetAssetPath("item/status_bg.png"), 2, 8);
            status.draw_event += new Panel.Draw_event(draw);
            status.init();
        }

        public static void show()
        {
            menu = 0;
            page = 1;
            status.show();
        }

        public static void click_equip_att()
        {
            Item.unequip(1);
        }

        public static void click_equip_def()
        {
            Item.unequip(2);
        }

        public static void click_next_player()
        {
            Player.select_player = Player.select_player + 1;
            for (int i = Player.select_player; i< Form1.player.Length; i++)
            {
                if (Form1.player[i].is_active == 1)
                {
                    Player.select_player = i;
                    return;
                }
            }

            for (int i = 0; i < Player.select_player; i++)
            {
                if (Form1.player[i].is_active == 1)
                {
                    Player.select_player = i;
                    return;
                }
            }
        }

        public static void click_item_menu()
        {
            page = 1;
            selnow = 1;
            StatusMenu.menu = 0;
        }
        public static void click_skill_menu()
        {
            page = 1;
            selnow = 1;
            StatusMenu.menu = 1;
        }
        public static void click_previous_page()
        {
            page--;
            if (page < 1)
            {
                page = 1;
            }
        }
        public static void click_next_page()
        {
            page++;
        }
        public static void click_use()
        {
            if (menu == 0) // 物品
            {
                int index = -1;
                for (int i = 0, count = 0; i < Item.item.Length; i++)
                {
                    if (Item.item[i].num <= 0)
                    {
                        continue;
                    }

                    count++;
                    if (count <= (page-1)*3 + selnow - 1)
                    {
                        continue;
                    }

                    index = i;
                    break;
                }

                if (index >= 0)
                {
                    Item.item[index].use();
                }
            } else if (menu == 1) // 技能
            {
                int index = -1;
                int[] pskill = Form1.player[Player.select_player].skill;
                for (int i = 0, count = 0; i < pskill.Length; i++)
                {
                    if (pskill[i] < 0)
                    {
                        continue;
                    }

                    count++;
                    if (count <= (page-1)*3 + selnow -1)
                    {
                        continue;
                    }

                    index = i;
                    break;
                }

                if (index >= 0)
                {
                    Skill.skill[pskill[index]].use();
                }
            }
        }
        public static void click_close()
        {
            status.hide();
        }

        public static void click_sel1()
        {
            selnow = 1;
        }
        public static void click_sel2()
        {
            selnow = 2;
        }
        public static void click_sel3()
        {
            selnow = 3;
        }

        public static void draw(Graphics g, int x_offset, int y_offset)
        {
            Player p = Form1.player[Player.select_player];
            g.DrawImage(p.status_bitmap, x_offset, y_offset);

            Font font = new Font("黑体", 10);
            Brush brush = Brushes.Black;
            g.DrawString(p.hp.ToString(), font, brush, x_offset + 90, y_offset + 346, new StringFormat());
            g.DrawString(p.attack.ToString(), font, brush, x_offset + 90, y_offset + 366, new StringFormat());
            g.DrawString(p.fspeed.ToString(), font, brush, x_offset + 90, y_offset + 386, new StringFormat());
            g.DrawString(p.mp.ToString(), font, brush, x_offset + 225, y_offset + 346, new StringFormat());
            g.DrawString(p.defense.ToString(), font, brush, x_offset + 225, y_offset + 366, new StringFormat());
            g.DrawString(p.fortune.ToString(), font, brush, x_offset + 225, y_offset + 386, new StringFormat());

            // 装备加成
            int value1 = 0;
            int value2 = 0;
            int value3 = 0;
            int value4 = 0;
            if (p.equip_att >= 0)
            {
                value1 = Item.item[p.equip_att].value2;
                value2 = Item.item[p.equip_att].value3;
                value3 = Item.item[p.equip_att].value4;
                value4 = Item.item[p.equip_att].value5;
            }

            if (p.equip_def >= 0)
            {
                value1 = Item.item[p.equip_def].value2;
                value2 = Item.item[p.equip_def].value3;
                value3 = Item.item[p.equip_def].value4;
                value4 = Item.item[p.equip_def].value5;
            }
            
            Font font_eq = new Font("黑体", 10);
            Brush brush_eq = Brushes.Red;
            if (value1 != 0)
            {
                g.DrawString("+" + value1.ToString(), font_eq, brush_eq, x_offset + 110, y_offset+366, new StringFormat());
            }          
            if (value2 != 0)
            {
                g.DrawString("+" + value2.ToString(), font_eq, brush_eq, x_offset + 255, y_offset+366, new StringFormat());
            }
            if (value3 != 0)
            {
                g.DrawString("+" + value3.ToString(), font_eq, brush_eq, x_offset + 110, y_offset+386, new StringFormat());
            }
            if (value4 != 0)
            {
                g.DrawString("+" + value4.ToString(), font_eq, brush_eq, x_offset + 255, y_offset+386, new StringFormat());
            }

            if (p.equip_att >= 0 && Item.item[p.equip_att].bitmap != null)
            {
                g.DrawImage(Item.item[p.equip_att].bitmap, x_offset + 41, y_offset + 55);
            }

            if (p.equip_def >= 0 && Item.item[p.equip_def].bitmap != null)
            {
                g.DrawImage(Item.item[p.equip_def].bitmap, x_offset + 41, y_offset + 136);
            }

            if (menu == 0)
            {
                g.DrawImage(bitmap_menu_item, x_offset + 629, y_offset + 51);
            }
            else if (menu == 1)
            {
                g.DrawImage(bitmap_menu_equip, x_offset + 629, y_offset + 51);
            }
            
            // 显示物品
            if (menu == 0)
            {
                for (int i = 0, count = 0, showcount = 0; i < Item.item.Length && showcount < 3; i++)
                {
                    if (Item.item[i].num <= 0)
                    {
                        continue;
                    }

                    count++;
                    if (count <= (page-1)*3)
                    {
                        continue;
                    }

                    if (Item.item[i].bitmap != null)
                    {
                        g.DrawImage(Item.item[i].bitmap, x_offset + 360, y_offset + 48 + showcount * 96);
                    }

                    Font font_n = new Font("黑体", 12);
                    Brush brush_n = Brushes.GreenYellow;
                    g.DrawString(Item.item[i].name + "X" + Item.item[i].num.ToString(), font_n, brush_n, 
                        x_offset + 440, y_offset + 48 + showcount*96, new StringFormat());
                    Font font_d = new Font("黑体", 10);
                    Brush brush_d = Brushes.LawnGreen;
                    g.DrawString(Item.item[i].description, font_d, brush_d,
                        x_offset + 440, y_offset + 75 + showcount*96, new StringFormat());
                    showcount++;
                }
            } else if (menu == 1) // 显示技能
            {
                int[] pskill = Form1.player[Player.select_player].skill;
                for (int i = 0,count=0,showcount=0; i < pskill.Length && showcount < 3; i++)
                {
                    if (pskill[i] < 0)
                    {
                        continue;
                    }

                    count++;
                    if (count <= (page-1)*3)
                    {
                        continue;
                    }

                    if (Skill.skill[pskill[i]].bitmap != null)
                    {
                        g.DrawImage(Skill.skill[pskill[i]].bitmap,
                            x_offset + 360, y_offset+48 + showcount * 96);
                    }

                    Font font_n = new Font("黑体", 12);
                    Brush brush_n = Brushes.GreenYellow;
                    g.DrawString(Skill.skill[pskill[i]].name, font_n, brush_n,
                        x_offset + 440, y_offset+48 + showcount*96, new StringFormat());
                    Font font_d = new Font("黑体", 10);
                    Brush brush_d = Brushes.LawnGreen;
                    g.DrawString(Skill.skill[pskill[i]].description, font_d, brush_d,
                        x_offset + 440, y_offset + 75 + showcount*96, new StringFormat());
                    showcount++;
                }
            }
            
            g.DrawImage(StatusMenu.bitmap_sel, x_offset + 350, y_offset+38 + (selnow-1)*95);

        }
    }
}