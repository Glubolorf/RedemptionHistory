using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class ChakrogAngler : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chakrog Angler");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 1, 16, 0);
			base.item.maxStack = 999;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
