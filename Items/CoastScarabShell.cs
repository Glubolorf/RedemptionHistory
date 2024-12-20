using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CoastScarabShell : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Coast Scarab Shell");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 0, 75);
			base.item.rare = 0;
		}
	}
}
