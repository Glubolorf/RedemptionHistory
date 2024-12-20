using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Dusts;
using Redemption.NPCs.LabNPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class HardenedSludge3Tile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileBouncy[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.dustType = ModContent.DustType<SludgeSpoonDust>();
			this.minPick = 300;
			this.mineResist = 8f;
			base.CreateMapEntryName(null).SetDefault("Hardened Sludge");
			base.AddMapEntry(new Color(20, 50, 20), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				Main.LocalPlayer.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(10, 20), true);
			}
		}

		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (Main.rand.Next(4) == 0)
			{
				i *= 16;
				j *= 16;
				NPC.NewNPC(i, j, ModContent.NPCType<SludgyBlob>(), 0, 0f, 0f, 0f, 0f, 255);
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
	}
}
