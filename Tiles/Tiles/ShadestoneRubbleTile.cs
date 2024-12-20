using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class ShadestoneRubbleTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneHSlabTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneMossyTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickMossyTile>()] = true;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.minPick = 300;
			this.mineResist = 6f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Shadestone Rubble");
			base.AddMapEntry(new Color(40, 40, 40), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
