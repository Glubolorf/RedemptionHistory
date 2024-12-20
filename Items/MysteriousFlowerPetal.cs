using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class MysteriousFlowerPetal : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mysterious Flower Petal");
			base.Tooltip.SetDefault("Comes from a flower that grows upside-down...");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 30;
			base.item.maxStack = 999;
			base.item.value = 500;
			base.item.rare = 5;
		}
	}
}
