using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class ShadestoneRubble : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadestone Rubble");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<ShadestoneRubbleTile>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
