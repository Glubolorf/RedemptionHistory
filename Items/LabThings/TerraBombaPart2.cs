using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class TerraBombaPart2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terraforma Bomba Core");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 14;
			base.item.maxStack = 3;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
