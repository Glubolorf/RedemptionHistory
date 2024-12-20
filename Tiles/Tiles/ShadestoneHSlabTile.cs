using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class ShadestoneHSlabTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneRubbleTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneMossyTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickMossyTile>()] = true;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.drop = ModContent.ItemType<ShadestoneHSlab>();
			this.minPick = 500;
			this.mineResist = 18f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Shadestone Beam");
			base.AddMapEntry(new Color(50, 50, 50), null);
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
