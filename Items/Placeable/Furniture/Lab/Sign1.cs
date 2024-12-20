using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class Sign1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("'No Entry' Sign");
			base.Tooltip.SetDefault("'It does say no entry, trust me'");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<Sign1Tile>();
			base.item.placeStyle = 0;
		}
	}
}
