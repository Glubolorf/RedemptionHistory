using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LivingTwig : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Twig");
			base.Tooltip.SetDefault("'It's moving... Oh nevermind, it's just the wind.'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 1, 0);
			base.item.rare = 0;
		}
	}
}
