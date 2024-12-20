using System;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class Mk2Plating : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-2 Plating");
			base.Tooltip.SetDefault("'Resistant to heavy impacts...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 32;
			base.item.maxStack = 30;
			base.item.value = 300000;
			base.item.rare = 6;
		}
	}
}
