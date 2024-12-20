using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AncientGoldCoin : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Gold Coin");
			base.Tooltip.SetDefault("'A plain gold coin, these were originally Gathuram's currency until they started to use brass'");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 0, 10);
			base.item.rare = -1;
		}
	}
}
