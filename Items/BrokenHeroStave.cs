using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class BrokenHeroStave : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Broken Hero Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 8;
		}
	}
}
