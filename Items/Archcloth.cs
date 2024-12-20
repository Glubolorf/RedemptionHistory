using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Archcloth : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Archcloth");
			base.Tooltip.SetDefault("'Expensive, purple cloth only used by the Nobles of Anglon'");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 50, 0);
			base.item.rare = 4;
		}
	}
}
