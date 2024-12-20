﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class KaniteOreTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 210;
			this.dustType = 80;
			this.drop = base.mod.ItemType("KaniteOre");
			this.minPick = 30;
			this.mineResist = 1.2f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Kanite Ore");
			base.AddMapEntry(new Color(116, 138, 153), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
