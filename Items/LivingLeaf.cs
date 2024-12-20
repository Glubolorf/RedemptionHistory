using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LivingLeaf : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Leaf");
			base.Tooltip.SetDefault("'It's moving... Oh, nevermind, that's just the wind.'");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 10;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 0, 50);
			base.item.rare = 0;
		}
	}
}
