using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class SapphironOreTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 660;
			this.dustType = 68;
			this.drop = base.mod.ItemType("SapphironOre");
			this.minPick = 200;
			this.mineResist = 3f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Sapphiron Ore");
			base.AddMapEntry(new Color(50, 50, 100), null);
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
