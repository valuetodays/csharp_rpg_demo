using System.Drawing;

namespace rpg
{
    public class Shop
    {
        public static Panel shop = new Panel();

        public static int page = 1;
        public static int selnow = 1;
        public static Bitmap bitmap_sel;
        public static int[] list;

        public static void init()
        {
            bitmap_sel = new Bitmap(ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"));
            bitmap_sel.SetResolution(96, 96);

            Panel.Button previous_page = new Panel.Button();
            previous_page.set(79, 335, 0, 0,
                ResourceContextDeterminer.GetAssetPath("item/sbt3_1.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt3_2.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt3_2.png"),
                -1, -1, -1, -1);
            previous_page.click_event += new Panel.Button.Click_event(click_previous_page);

            Panel.Button next_page = new Panel.Button();
            next_page.set(230, 335, 0, 0,
                ResourceContextDeterminer.GetAssetPath("item/sbt5_1.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt5_2.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt5_2.png"),
                -1, -1, -1, -1);
            next_page.click_event += new Panel.Button.Click_event(click_next_page);

            Panel.Button buy = new Panel.Button();
            buy.set(154, 335, 0, 0,
                ResourceContextDeterminer.GetAssetPath("item/sbt8_1.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt8_2.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt8_2.png"),
                -1, -1, -1, -1);
            buy.click_event += new Panel.Button.Click_event(click_buy);

            Panel.Button close = new Panel.Button();
            close.set(332, 23, 0, 0,
                ResourceContextDeterminer.GetAssetPath("item/sbt6_1.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt6_2.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt6_2.png"),
                -1, -1, -1, -1);
            close.click_event += new Panel.Button.Click_event(click_close);

            Panel.Button sel1 = new Panel.Button();
            sel1.set(57, 49, 0, 0,
                ResourceContextDeterminer.GetAssetPath("item/sbt7_1.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                -1, -1, -1, -1);
            sel1.click_event += new Panel.Button.Click_event(click_sel1);

            Panel.Button sel2 = new Panel.Button();
            sel2.set(57, 144, 0, 0,
                ResourceContextDeterminer.GetAssetPath("item/sbt7_1.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                -1, -1, -1, -1);
            sel2.click_event += new Panel.Button.Click_event(click_sel2);

            Panel.Button sel3 = new Panel.Button();
            sel3.set(57, 239, 0, 0,
                ResourceContextDeterminer.GetAssetPath("item/sbt7_1.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt7_2.png"),
                -1, -1, -1, -1);
            sel3.click_event += new Panel.Button.Click_event(click_sel3);

            Panel.Button under = new Panel.Button();
            under.set(-100, -100, 1000, 1000,
                ResourceContextDeterminer.GetAssetPath("item/sbt6_1.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt6_2.png"), 
                ResourceContextDeterminer.GetAssetPath("item/sbt6_2.png"),
                -1, -1, -1, -1);

            shop.button = new Panel.Button[8];
            shop.button[0] = previous_page;
            shop.button[1] = next_page;
            shop.button[2] = buy;
            shop.button[3] = close;
            shop.button[4] = sel1;
            shop.button[5] = sel2;
            shop.button[6] = sel3;
            shop.button[7] = under;
            shop.set(213, 65, ResourceContextDeterminer.GetAssetPath("item/shop_bg.png"), 7, 3);
            shop.draw_event += new Panel.Draw_event(draw);
            shop.init();
        }

        public static void show(int[] list)
        {
            Shop.list = list;
            page = 1;
            shop.show();
        }


        //----------------------------------------------------------------
        //     按钮回调
        //----------------------------------------------------------------
        public static void click_previous_page()
        {
            page--;
            if (page < 1) page = 1;
        }

        public static void click_next_page()
        {
            page++;
        }

        public static void click_buy()
        {
            int index = -1;
            for (int i = 0, count = 0; i < Shop.list.Length; i++)
            {
                if (Shop.list[i] < 0)
                    continue;
                count++;

                if (count <= (page - 1) * 3 + selnow - 1)
                    continue;

                index = i;
                break;
            }

            if (index >= 0)
            {
                if (Player.money > Item.item[index].cost)
                {
                    Player.money -= Item.item[index].cost;
                    Item.add_item(index, 1);
                }
            }
        }

        public static void click_close()
        {
            shop.hide();
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

        //----------------------------------------------------------------
        //     绘图回调
        //----------------------------------------------------------------
        public static void draw(Graphics g, int x_offset, int y_offset)
        {
            //金钱
            Font font_m = new Font("黑体", 16);
            Brush brush_m = Brushes.DarkOrange;
            g.DrawString(Player.money.ToString(), font_m, brush_m,
                x_offset + 171, y_offset + 385, new StringFormat());
            //显示商品

            int start = -1;
            for (int i = 0, count = 0; i < Shop.list.Length; i++)
            {
                if (Shop.list[i] < 0)
                    continue;
                count++;
                if (count <= (page - 1) * 3)
                    continue;
                start = i;
                break;
            }

            if (start >= 0)
            {
                for (int i = start, count = 0; i < Shop.list.Length && count < 3; i++)
                {
                    if (Shop.list[i] < 0)
                        continue;
                    int index = Shop.list[i];
                    if (Item.item[index].bitmap != null)
                        g.DrawImage(Item.item[i].bitmap, x_offset + 70, y_offset + 59 + count * 96);
                    Font font_n = new Font("黑体", 12);
                    Brush brush_n = Brushes.GreenYellow;
                    g.DrawString(Item.item[index].name + " $" + Item.item[i].cost.ToString(), font_n, brush_n,
                        x_offset + 150, y_offset + 59 + count * 96, new StringFormat());
                    Font font_d = new Font("黑体", 10);
                    Brush brush_d = Brushes.LawnGreen;
                    g.DrawString(Item.item[index].description, font_d, brush_d,
                        x_offset + 150, y_offset + 86 + count * 96, new StringFormat());
                    count++;
                }
            }

            //显示选择框
            g.DrawImage(StatusMenu.bitmap_sel, x_offset + 57, y_offset + 49 + (selnow - 1) * 95);
        }
    }
}