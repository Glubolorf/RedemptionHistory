using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GrandLarvaBait : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Grand Larva");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 30;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 0, 2, 0);
			base.item.rare = 1;
			base.item.bait = 55;
		}
	}
}
