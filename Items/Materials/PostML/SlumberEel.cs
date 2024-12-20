using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class SlumberEel : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Slumber Eel");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 1, 26, 0);
			base.item.maxStack = 999;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
