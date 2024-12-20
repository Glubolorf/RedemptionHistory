using System;
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
		}
	}
}
