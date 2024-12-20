using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class OblitBrain : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Obliterator Brain");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 32;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.rare = 10;
		}
	}
}
