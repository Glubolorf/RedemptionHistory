﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class DangerTape2Tile : ModTile
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
			this.drop = ModContent.ItemType<DangerTape2>();
			this.minPick = 50;
			this.mineResist = 3f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(255, 255, 200), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
