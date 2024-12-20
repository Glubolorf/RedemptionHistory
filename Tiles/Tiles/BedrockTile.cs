using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class BedrockTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			this.dustType = 82;
			this.minPick = 500;
			this.mineResist = 50f;
			this.soundType = 21;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Bedrock");
			base.AddMapEntry(new Color(0, 0, 0), name);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}
	}
}
