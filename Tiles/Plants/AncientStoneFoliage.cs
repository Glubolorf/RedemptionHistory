using System;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Plants
{
	public class AncientStoneFoliage : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileCut[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileWaterDeath[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.AnchorBottom = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				ModContent.TileType<AncientStoneBrickTile>(),
				ModContent.TileType<AncientStoneTile>(),
				ModContent.TileType<AncientHallBrickTile>()
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 7;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 2;
			this.soundType = 6;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 10;
		}
	}
}
