using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Bile : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Toxic Bile");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 9, 0);
			base.item.rare = 7;
		}
	}
}
