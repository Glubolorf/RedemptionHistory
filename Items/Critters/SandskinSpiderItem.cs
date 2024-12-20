using System;
using Redemption.NPCs.Critters;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Critters
{
	public class SandskinSpiderItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sandskin Spider");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 20;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.rare = 1;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.consumable = true;
		}

		public override bool UseItem(Player player)
		{
			int num = NPC.NewNPC((int)(player.position.X + (float)Main.rand.Next(-20, 20)), (int)(player.position.Y - 0f), ModContent.NPCType<SandskinSpider>(), 0, 0f, 0f, 0f, 0f, 255);
			if (Main.netMode == 2 && num < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
			}
			return true;
		}
	}
}
