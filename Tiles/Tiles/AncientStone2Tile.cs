using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class AncientStone2Tile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientHallBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientDirtTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneBrick2Tile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneTile>()] = true;
			this.drop = ModContent.ItemType<AncientStone2>();
			this.minPick = 0;
			this.mineResist = 2.5f;
			base.AddMapEntry(new Color(120, 91, 35), null);
		}
	}
}
