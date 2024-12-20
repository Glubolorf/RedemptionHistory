using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Nightshade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade");
			base.Tooltip.SetDefault("'A purple plant the blooms in the night'");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 99;
			base.item.width = 16;
			base.item.height = 20;
			base.item.value = 150;
			base.item.rare = 1;
		}
	}
}
