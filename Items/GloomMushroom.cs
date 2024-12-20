using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GloomMushroom : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloom Mushroom");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nUsed to craft Gloom Druid equipment");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 0, 0, 50);
			base.item.rare = 3;
		}
	}
}
