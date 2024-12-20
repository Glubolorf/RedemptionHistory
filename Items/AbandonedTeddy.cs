using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AbandonedTeddy : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Abandoned Teddy");
			base.Tooltip.SetDefault("'How did it get here?'");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 48;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.rare = -1;
		}
	}
}
