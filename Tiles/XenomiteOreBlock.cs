using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class XenomiteOreBlock : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			this.dustType = 80;
			this.drop = base.mod.ItemType("XenomiteShard");
			this.minPick = 30;
			this.mineResist = 1.5f;
			this.soundType = 21;
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Xenomite Ore");
			base.AddMapEntry(new Color(120, 100, 255), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(10, 20), true);
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
