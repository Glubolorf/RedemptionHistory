using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class FlyBait : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fly");
		}

		public override void SetDefaults()
		{
			base.item.width = 10;
			base.item.height = 8;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 0, 0, 0);
			base.item.rare = 1;
			base.item.bait = 5;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.consumable = true;
			base.item.makeNPC = (short)base.mod.NPCType<Fly>();
		}
	}
}
