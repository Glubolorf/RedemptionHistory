using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Tiles.Plants;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class ShadestoneBrickMossyTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneRubbleTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneHSlabTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneMossyTile>()] = true;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.minPick = 500;
			this.mineResist = 11f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Mossy Shadestone Brick");
			base.AddMapEntry(new Color(10, 10, 10), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override void RandomUpdate(int i, int j)
		{
			if (!Framing.GetTileSafely(i, j + 1).active() && Main.tile[i, j].active() && Main.rand.Next(5) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Nooseroot_Small>(), true, Main.rand.Next(3), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Nooseroot_Small>(), Main.rand.Next(3), 0, -1, -1);
			}
			if (!Framing.GetTileSafely(i, j + 1).active() && Main.tile[i, j].active() && Main.rand.Next(7) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Nooseroot_Medium>(), true, Main.rand.Next(3), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Nooseroot_Medium>(), Main.rand.Next(3), 0, -1, -1);
			}
			if (!Framing.GetTileSafely(i, j + 1).active() && Main.tile[i, j].active() && Main.rand.Next(12) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Nooseroot_Large>(), true, Main.rand.Next(3), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Nooseroot_Large>(), Main.rand.Next(3), 0, -1, -1);
			}
			if (Utils.NextBool(Main.rand, 20))
			{
				WorldGen.SpreadGrass(i + Main.rand.Next(-1, 1), j + Main.rand.Next(-1, 1), ModContent.TileType<ShadestoneBrickTile>(), ModContent.TileType<ShadestoneBrickMossyTile>(), false, 0);
			}
		}
	}
}
