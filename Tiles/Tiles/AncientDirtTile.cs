using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Redemption.Tiles.Trees;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class AncientDirtTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientHallBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStoneBrick2Tile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<AncientStone2Tile>()] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.drop = ModContent.ItemType<AncientDirt>();
			base.AddMapEntry(new Color(84, 38, 0), null);
			base.SetModTree(new AncientTree());
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return ModContent.TileType<AncientSapling>();
		}
	}
}
