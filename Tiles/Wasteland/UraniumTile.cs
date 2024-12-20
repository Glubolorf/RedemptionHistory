using System;
using Microsoft.Xna.Framework;
using Redemption.Items.LabThings;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class UraniumTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 660;
			this.dustType = 226;
			this.drop = base.mod.ItemType("Uranium");
			this.minPick = 210;
			this.mineResist = 7f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(77, 240, 107), null);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 0.4f;
			b = 0f;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			int dist = (int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j));
			if (dist <= 15 && dist > 8 && !modPlayer.hazmatPower && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller1").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(80000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 2)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
					return;
				}
			}
			else if (dist <= 8 && dist > 2 && !modPlayer.hazmatPower && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller2").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(40000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 2)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
					return;
				}
			}
			else if (dist <= 2 && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller3").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(8000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 2)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
				}
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
