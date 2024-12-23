﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Materials.PostML;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Ores
{
	public class ShinkiteTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][57] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 780;
			this.dustType = 6;
			this.drop = ModContent.ItemType<UnrefinedShinkite>();
			this.minPick = 300;
			this.mineResist = 10f;
			this.soundType = 21;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Unrefined Shinkite");
			base.AddMapEntry(new Color(41, 24, 32), name);
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
