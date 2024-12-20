using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class KingCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("K.I.N.G Core");
			base.Tooltip.SetDefault("'Core of the Slayer'");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.maxStack = 30;
			base.item.value = Item.sellPrice(0, 75, 0, 0);
			base.item.rare = 9;
		}
	}
}
