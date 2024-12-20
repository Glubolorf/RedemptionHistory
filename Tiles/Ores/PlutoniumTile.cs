using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Materials.PostML;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Ores
{
	public class PlutoniumTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 720;
			this.dustType = 226;
			this.drop = ModContent.ItemType<Plutonium>();
			this.minPick = 220;
			this.mineResist = 6f;
			this.soundType = 21;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Plutonium");
			base.AddMapEntry(new Color(133, 253, 255), name);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.2f;
			g = 0.2f;
			b = 0.4f;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			int dist = (int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j));
			if (dist <= 30 && dist > 18 && !modPlayer.hazmatPower && !modPlayer.HEVPower)
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
			else if (dist <= 18 && dist > 10 && !modPlayer.hazmatPower && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller2").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(40000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 3)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
					return;
				}
			}
			else if (dist <= 10 && dist > 4 && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller3").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(8000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 3)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
					return;
				}
			}
			else if (dist <= 4)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller4").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(2000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 3)
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
