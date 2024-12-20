using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class CursedThornsF : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Piece");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
