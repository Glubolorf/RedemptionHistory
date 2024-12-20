using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Redemption.Tiles.Plants;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class AncientStoneBrickTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientHallBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientDirtTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneBrick2Tile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStone2Tile>()] = true;
			this.drop = ModContent.ItemType<AncientStoneBrick>();
			this.minPick = 0;
			this.mineResist = 2.5f;
			base.AddMapEntry(new Color(120, 91, 35), null);
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
