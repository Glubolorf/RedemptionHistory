using System;
using Redemption.NPCs.Varients;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ChickenLeghornItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Leghorn Chicken");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 30;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 0, 7, 0);
			base.item.rare = 1;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.consumable = true;
			base.item.makeNPC = (short)ModContent.NPCType<LeghornChicken>();
		}
	}
}
