using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class PrisonBarsTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickTile>()] = true;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.minPick = 500;
			this.mineResist = 18f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Prison Bars");
			base.AddMapEntry(new Color(80, 80, 80), null);
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
