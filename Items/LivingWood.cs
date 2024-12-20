using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LivingWood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood");
			base.Tooltip.SetDefault("'It's moving...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 1, 0);
			base.item.rare = 0;
		}
	}
}
