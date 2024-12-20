using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class TerraBombaPart1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terraforma Bomba Tail");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 18;
			base.item.maxStack = 3;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
