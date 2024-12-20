﻿using System;
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
			this.dustType = 68;
			this.drop = base.mod.ItemType("SapphironOre");
			this.minPick = 200;
			this.mineResist = 3f;
			this.soundType = 21;
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Sapphiron Ore");
			base.AddMapEntry(new Color(50, 50, 100), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
