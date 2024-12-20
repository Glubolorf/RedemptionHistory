using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class BluePrints : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Unidentified Blueprints");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 18;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
