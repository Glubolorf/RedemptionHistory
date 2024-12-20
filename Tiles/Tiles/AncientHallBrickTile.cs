using System;
using Microsoft.Xna.Framework;
using Redemption.Tiles.Plants;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class AncientHallBrickTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileLighted[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientDirtTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneBrick2Tile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStone2Tile>()] = true;
			this.dustType = 1;
			this.minPick = 500;
			this.mineResist = 10f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(120, 91, 35), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override void RandomUpdate(int i, int j)
		{
			if (!Framing.GetTileSafely(i, j - 1).active() && Main.tile[i, j].active() && Main.rand.Next(15) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<AncientStoneFoliage>(), true, Main.rand.Next(7), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<AncientStoneFoliage>(), Main.rand.Next(7), 0, -1, -1);
			}
			if (!Framing.GetTileSafely(i, j + 1).active() && Main.tile[i, j].active() && Main.rand.Next(25) == 0 && Main.tile[i, j + 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j + 1, ModContent.TileType<AncientStoneFoliageC>(), true, Main.rand.Next(7), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j + 1, ModContent.TileType<AncientStoneFoliageC>(), Main.rand.Next(7), 0, -1, -1);
			}
		}
	}
}
