using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class ShadestoneBrickTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneRubbleTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneHSlabTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneMossyTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickMossyTile>()] = true;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.minPick = 500;
			this.mineResist = 18f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Shadestone Brick");
			base.AddMapEntry(new Color(37, 37, 37), null);
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
	}
}
