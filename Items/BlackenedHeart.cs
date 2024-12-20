using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class BlackenedHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blackened Heart");
			base.Tooltip.SetDefault("'May cause instant death'");
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.maxStack = 99;
			base.item.value = 3000;
			base.item.rare = 4;
		}
	}
}
