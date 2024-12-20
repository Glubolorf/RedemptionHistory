using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class LabTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.dustType = 226;
			this.drop = ModContent.ItemType<LabPlating>();
			this.minPick = 0;
			this.mineResist = 1f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(200, 200, 200), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
