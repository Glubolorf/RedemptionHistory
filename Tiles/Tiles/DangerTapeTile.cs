using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class DangerTapeTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<LabTileUnsafe>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<LabTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<OvergrownLabTile>()] = true;
			this.dustType = 226;
			this.drop = ModContent.ItemType<DangerTape>();
			this.minPick = 500;
			this.mineResist = 3f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(255, 255, 200), null);
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
			return RedeWorld.downedPatientZero;
		}
	}
}
