using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class UndownerRedePoints : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Alignment Resetter");
			base.Tooltip.SetDefault("Sets alignment to 0.\r\nNon-Consumable");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.rare = 2;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
		}

		public override bool UseItem(Player player)
		{
			RedeWorld.redemptionPoints = 0;
			return true;
		}
	}
}
