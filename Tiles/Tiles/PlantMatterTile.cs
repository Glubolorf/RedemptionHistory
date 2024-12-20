using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class PlantMatterTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][59] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.dustType = 3;
			this.drop = ModContent.ItemType<PlantMatter>();
			this.minPick = 0;
			this.mineResist = 1f;
			this.soundType = 6;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Plant Matter");
			base.AddMapEntry(new Color(60, 200, 60), name);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
