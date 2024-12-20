using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class StarliteOreTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.dustType = ModContent.DustType<XenoDust>();
			this.drop = ModContent.ItemType<Starlite>();
			this.minPick = 100;
			this.mineResist = 2f;
			this.soundType = 21;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Starlite");
			base.AddMapEntry(new Color(50, 120, 160), name);
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
