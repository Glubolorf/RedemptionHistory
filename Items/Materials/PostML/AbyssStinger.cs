using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class AbyssStinger : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Abyss Stinger");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.maxStack = 999;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
