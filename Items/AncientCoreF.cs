using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AncientCoreF : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Power Fragment");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 12;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
