using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeStoneGate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadestone Gate");
			base.Tooltip.SetDefault("[c/ff0000:Unbreakable]");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 36;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<ShadeStoneGateTile>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
