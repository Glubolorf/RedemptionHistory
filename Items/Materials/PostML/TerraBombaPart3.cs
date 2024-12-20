using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class TerraBombaPart3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terraforma Bomba Nose");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 3;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
