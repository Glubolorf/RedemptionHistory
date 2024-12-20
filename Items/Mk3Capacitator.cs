using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Mk3Capacitator : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-3 Capacitator");
			base.Tooltip.SetDefault("'Holds an extreme amount of energy...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 30;
			base.item.maxStack = 30;
			base.item.value = 650000;
			base.item.rare = 7;
		}
	}
}
