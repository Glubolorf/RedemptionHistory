using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class LabTankTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileLighted[(int)base.Type] = true;
			this.dustType = 226;
			this.minPick = 500;
			this.mineResist = 5f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(200, 250, 200), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				Main.LocalPlayer.AddBuff(ModContent.BuffType<RadioactiveFalloutDebuff>(), Main.rand.Next(10, 20), true);
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 0.2f;
			b = 0f;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
