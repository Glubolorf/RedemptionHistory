using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class KaniteOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Ore");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 2, 0);
			base.item.rare = 0;
		}
	}
}
