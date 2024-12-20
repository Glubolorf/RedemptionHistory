using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Wasteland
{
	public class DeadGrassA : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileCut[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileWaterDeath[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 5;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 206;
			this.soundType = 6;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 10;
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 4;
		}
	}
}
