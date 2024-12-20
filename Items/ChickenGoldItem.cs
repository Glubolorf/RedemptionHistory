using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ChickenGoldItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gold Chicken");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 1;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.consumable = true;
			base.item.makeNPC = (short)ModContent.NPCType<ChickenGold>();
		}
	}
}
