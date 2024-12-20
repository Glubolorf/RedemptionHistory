﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class AncientStoneTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.drop = ModContent.ItemType<AncientStone>();
			this.minPick = 0;
			this.mineResist = 2.5f;
			base.AddMapEntry(new Color(120, 91, 35), null);
		}
	}
}
