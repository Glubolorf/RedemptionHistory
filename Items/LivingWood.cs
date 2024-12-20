using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LivingWood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood");
			base.Tooltip.SetDefault("This item & Living Leaf has been replaced with the Living Twig.\nIf you still have this item, say from an old world, just use it to craft the Living Twig");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.rare = 0;
		}
	}
}
