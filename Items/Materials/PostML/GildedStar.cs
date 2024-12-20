using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class GildedStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Galaxy Star");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 32;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
