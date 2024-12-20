using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class WardensKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Warden's Key");
			base.Tooltip.SetDefault("Used to open a gate in the temple");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.maxStack = 1;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
