using System.Drawing;

namespace rpg
{
    public class Define
    {

        public static void define(Player[] player, Map[] map, Npc[] npc)
        {
            player[0] = new rpg.Player();
            player[0].bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r1.png"));
            player[0].bitmap.SetResolution(96, 96);
            player[0].is_active = 1;
            player[0].status_bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("item/face1.png"));
            player[0].status_bitmap.SetResolution(96, 96);

            player[1] = new rpg.Player();
            player[1].bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r2.png"));
            player[1].bitmap.SetResolution(96, 96);
            player[1].is_active = 1;
            player[1].status_bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("item/face2.png"));
            player[1].status_bitmap.SetResolution(96, 96);

            player[2] = new rpg.Player();
            player[2].bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r3.png"));
            player[2].bitmap.SetResolution(96, 96);
            player[2].is_active = 1;
            player[2].status_bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("item/face3.png"));
            player[2].status_bitmap.SetResolution(96, 96);
            
            
            map[0] = new Map();
            map[0].bitmap_path = ResourceContextDeterminer.GetAssetPath("map1.png");
            map[0].shade_path = ResourceContextDeterminer.GetAssetPath("map1_shade.png");
            map[0].block_path = ResourceContextDeterminer.GetAssetPath("map1_block.png");
            map[0].back_path = ResourceContextDeterminer.GetAssetPath("map1_back.png");
            map[0].music = ResourceContextDeterminer.GetAssetPath("1.mp3");

            map[1] = new Map();
            map[1].bitmap_path = ResourceContextDeterminer.GetAssetPath("map2.png");
            map[1].shade_path = ResourceContextDeterminer.GetAssetPath("map2_shade.png");
            map[1].block_path = ResourceContextDeterminer.GetAssetPath("map2_block.png");
            map[1].music = ResourceContextDeterminer.GetAssetPath("2.mp3");

            npc[0] = new rpg.Npc();
            npc[0].map = 0;
            npc[0].x = 700;
            npc[0].y = 300;
            npc[0].bitmap_path = ResourceContextDeterminer.GetAssetPath("npc1.png");
            npc[1] = new rpg.Npc();
            npc[1].map = 0;
            npc[1].x = 900;
            npc[1].y = 300;
            npc[1].bitmap_path = ResourceContextDeterminer.GetAssetPath("npc2.png");

            npc[2] = new Npc();
            npc[2].map = 0;
            npc[2].x = 20;
            npc[2].y = 600;
            npc[2].region_x = 40;
            npc[2].region_y = 400;
            npc[2].collision_type = Npc.Collision_type.ENTER;
            npc[3] = new Npc();
            npc[3].map = 1;
            npc[3].x = 980;
            npc[3].y = 600;
            npc[3].region_x = 40;
            npc[3].region_y = 400;
            npc[3].collision_type = Npc.Collision_type.ENTER;

            npc[4] = new Npc();
            npc[4].map = 1;
            npc[4].x = 700;
            npc[4].y = 350;
            npc[4].bitmap_path = ResourceContextDeterminer.GetAssetPath("npc3.png");
            npc[4].collision_type = Npc.Collision_type.KEY;
            Animation npc4anm1 = new Animation();
            npc4anm1.bitmap_path = ResourceContextDeterminer.GetAssetPath("anm1.png");
            npc4anm1.row = 2;
            npc4anm1.col = 2;
            npc4anm1.max_frame = 3;
            npc4anm1.anm_rate = 4;
            npc[4].anm = new Animation[1];
            npc[4].anm[0] = npc4anm1;

            npc[5] = new Npc();
            npc[5].map = 1;
            npc[5].x = 450;
            npc[5].y = 300;
            npc[5].bitmap_path = ResourceContextDeterminer.GetAssetPath("npc4.png");
            npc[5].collision_type = Npc.Collision_type.KEY;
            npc[5].npc_type = Npc.Npc_type.CHARACTER;
            npc[5].idle_walk_direction = Comm.Direction.LEFT;
            npc[5].idle_walk_time = 20;
            
            // item
            Item.item = new Item[16];
            
            Item.item[0] = new Item();
            Item.item[0].set("红药水", "恢复少量hp", ResourceContextDeterminer.GetAssetPath("item/item1.png"), 1,
                30, 0, 0, 0, 0);
            Item.item[0].cost = 30;
            Item.item[0].use_event += new Item.Use_event(Item.add_hp);
            
            Item.item[1] = new Item();
            Item.item[1].set("蓝药水", "恢复少量mp", ResourceContextDeterminer.GetAssetPath("item/item2.png"), 1,
                30, 0, 0, 0, 0);
            Item.item[1].use_event += new Item.Use_event(Item.add_mp);
            
            Item.item[2] = new Item();
            Item.item[2].set("短剑", "一把钢质短剑", ResourceContextDeterminer.GetAssetPath("item/item3.png"), 1,
                1, 10, 0, 0, 0);
            Item.item[2].use_event += new Item.Use_event(Item.equip);
            
            Item.item[3] = new Item();
            Item.item[3].set("斧头", "传说这是一把能够劈开阴\n气的斧头，但无人亲眼见\n过它的威力", ResourceContextDeterminer.GetAssetPath("item/item4.png"), 1,
                1, 3, 0, 0, 50);
            Item.item[3].use_event += new Item.Use_event(Item.equip);
            
            Item.item[4] = new Item();
            Item.item[4].set("钢盾", "钢质盾牌，没有矛可以穿\n破它", ResourceContextDeterminer.GetAssetPath("item/item5.png"), 1,
                2, 0, 20, 5, 0);
            Item.item[4].use_event += new Item.Use_event(Item.equip);
            
            Item.item[5] = new Item();
            Item.item[5].set("罗培羽书", "一本游记，记录世间\n奇事，可打开阅览", ResourceContextDeterminer.GetAssetPath("item/item6.png"), 0,
                30, 0, 0, 0, 0);
            Item.item[5].use_event += new Item.Use_event(Item.lpybook);
            
            Item.add_item(0, 3);
            Item.add_item(1, 3);
            Item.add_item(2, 2);
            Item.add_item(3, 1);
            Item.add_item(4, 1);
            Item.add_item(5, 1);

            Skill.skill = new Skill[2];
            
            Skill.skill[0] = new Skill();
            Skill.skill[0].set("治疗术", "恢复少量hp\nmp:20", ResourceContextDeterminer.GetAssetPath("item/skill2.png"), 20,
                20, 0, 0, 0, 0);
            Skill.skill[0].use_event += new Skill.Use_event(Skill.add_hp);
            
            Skill.skill[1] = new Skill();
            Skill.skill[1].set("黑暗旋涡", "攻击型技能，将敌人吸入旋涡\nmp:20", ResourceContextDeterminer.GetAssetPath("item/skill1.png"), 20,
                0, 0, 0, 0, 0);
            Skill.skill[1].use_event += new Skill.Use_event(Skill.add_hp);
            
            Skill.learn_skill(0, 0, 1);
            Skill.learn_skill(0, 1, 1);
            Skill.learn_skill(1, 0, 1);
        }
    }
}