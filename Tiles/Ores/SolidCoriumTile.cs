using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Materials.PostML;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Ores
{
	public class SolidCoriumTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			Main.tileValue[(int)base.Type] = 900;
			this.dustType = 6;
			this.drop = ModContent.ItemType<Corium>();
			this.minPick = 500;
			this.mineResist = 10f;
			this.soundType = 21;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Corium");
			base.AddMapEntry(new Color(156, 75, 53), name);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.7f;
			g = 0.4f;
			b = 0f;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			int dist = (int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j));
			if (dist <= 25 && dist > 20 && !modPlayer.hazmatPower && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller1").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(80000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 5)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
					return;
				}
			}
			else if (dist <= 20 && dist > 14 && !modPlayer.hazmatPower && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller2").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(40000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 5)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
					return;
				}
			}
			else if (dist <= 14 && dist > 8 && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller4").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(2000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 5)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
					return;
				}
			}
			else if (dist <= 8 && dist > 2)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller5").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(500) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 5)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel += 2;
					return;
				}
			}
			else if (dist <= 2)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller5").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(10) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 5)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel += 2;
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
