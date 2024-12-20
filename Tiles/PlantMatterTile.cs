using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class PlantMatterTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = true;
			this.dustType = 3;
			this.drop = base.mod.ItemType("PlantMatter");
			this.minPick = 0;
			this.mineResist = 1f;
			this.soundType = 6;
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Plant Matter");
			base.AddMapEntry(new Color(60, 200, 60), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
