using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CursedThorns : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorns");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 26;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
