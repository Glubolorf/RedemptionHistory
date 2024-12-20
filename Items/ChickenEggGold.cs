using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ChickenEggGold : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gold Chicken Egg");
			base.Tooltip.SetDefault("'Woah...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 20;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 4;
		}
	}
}
