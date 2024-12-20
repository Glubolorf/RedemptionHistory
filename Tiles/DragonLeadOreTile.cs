using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class DragonLeadOreTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			this.dustType = 6;
			this.drop = base.mod.ItemType("DragonLeadChunk");
			this.minPick = 100;
			this.mineResist = 1.4f;
			this.soundType = 21;
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Dragon-Lead Chuck");
			base.AddMapEntry(new Color(251, 177, 81), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
