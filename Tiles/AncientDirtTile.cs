using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class AncientDirtTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
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
