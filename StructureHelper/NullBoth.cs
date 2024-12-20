using System;
using Terraria.ModLoader;

namespace Redemption.StructureHelper
{
	internal class NullBoth : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Null Tile Place-O-Matic");
			base.Tooltip.SetDefault("Places a null tile and null wall at the same time!");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 2;
			base.item.useTime = 2;
			base.item.useStyle = 1;
			base.item.rare = 8;
			base.item.createTile = ModContent.TileType<NullBlock>();
			base.item.createWall = ModContent.WallType<NullWall>();
		}
	}
}
