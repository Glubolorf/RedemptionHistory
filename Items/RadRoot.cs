using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class RadRoot : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rad Root");
			base.Tooltip.SetDefault("Grows in the Corrupted or Crimson Wasteland");
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
