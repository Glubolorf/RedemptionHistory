using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class LabTileUnsafe : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.dustType = 226;
			this.drop = base.mod.ItemType("LabPlating");
			this.minPick = 500;
			this.mineResist = 3f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(200, 200, 200), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player localPlayer = Main.LocalPlayer;
			int num = (int)Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i, (float)j));
			if (num <= 15)
			{
				localPlayer.AddBuff(base.mod.BuffType("RadioactiveFalloutDebuff"), Main.rand.Next(10, 20), true);
				localPlayer.enemySpawns = false;
			}
			if (num <= 1 && !RedeWorld.labSafe)
			{
				localPlayer.AddBuff(base.mod.BuffType("XenomiteSkullDebuff"), Main.rand.Next(10, 20), true);
			}
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
			return RedeWorld.downedPatientZero;
		}

		public override bool Slope(int i, int j)
		{
			return false;
		}
	}
}
