using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class BrothersPaintingAlt : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("'Brothers?'");
			base.Tooltip.SetDefault("Alternative");
		}

		public override void SetDefaults()
		{
			base.item.width = 46;
			base.item.height = 46;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<BrothersPaintingTileBig>();
			base.item.placeStyle = 0;
		}
	}
}
