using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class ScarlionOreTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			this.dustType = 68;
			this.drop = base.mod.ItemType("ScarlionOre");
			this.minPick = 200;
			this.mineResist = 3f;
			this.soundType = 21;
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Scarlion Ore");
			base.AddMapEntry(new Color(100, 50, 50), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
