using System;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class Mk3Plating : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-3 Plating");
			base.Tooltip.SetDefault("Resistant to extreme impacts...");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 32;
			base.item.maxStack = 30;
			base.item.value = 650000;
			base.item.rare = 7;
		}
	}
}
