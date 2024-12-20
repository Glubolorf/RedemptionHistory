using System;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class Mk1Capacitator : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-1 Capacitator");
			base.Tooltip.SetDefault("'Holds a small amount of energy...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 26;
			base.item.maxStack = 30;
			base.item.value = 150000;
			base.item.rare = 5;
		}
	}
}
