using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class GloomShroomTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.dustType = base.mod.DustType("ShroomDust1");
			this.drop = base.mod.ItemType("GloomMushroom");
			this.minPick = 0;
			this.mineResist = 1f;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(0, 100, 200), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
