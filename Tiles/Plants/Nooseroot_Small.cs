using System;
using Redemption.Dusts;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Plants
{
	public class Nooseroot_Small : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.AnchorTop = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				ModContent.TileType<ShadestoneMossyTile>(),
				ModContent.TileType<ShadestoneBrickMossyTile>()
			};
			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				ModContent.TileType<ShadestoneTile>()
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile((int)base.Type);
			this.dustType = ModContent.DustType<VoidFlame>();
			this.soundType = 6;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 10;
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = -4;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.1f;
			g = 0f;
			b = 0.1f;
		}
	}
}
