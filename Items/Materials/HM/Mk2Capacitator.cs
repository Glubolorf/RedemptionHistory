using System;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class Mk2Capacitator : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-2 Capacitator");
			base.Tooltip.SetDefault("'Holds a high amount of energy...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 28;
			base.item.maxStack = 30;
			base.item.value = 300000;
			base.item.rare = 6;
		}
	}
}
