using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class NebDefeated : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus, Angel of the Cosmos");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 90;
			base.projectile.height = 78;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = true;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 20)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.X = 0f;
			if (RedeWorld.downedNebuleus)
			{
				if (base.projectile.localAI[0] >= 120f)
				{
					base.projectile.Kill();
					return;
				}
			}
			else
			{
				if (base.projectile.localAI[0] == 60f)
				{
					string text = "... Damn.";
					Color rarityPink = Colors.RarityPink;
					byte r = rarityPink.R;
					Color rarityPink2 = Colors.RarityPink;
					byte g = rarityPink2.G;
					Color rarityPink3 = Colors.RarityPink;
					Main.NewText(text, r, g, rarityPink3.B, false);
					Redemption.GirusSilence = true;
				}
				if (base.projectile.localAI[0] == 160f)
				{
					string text2 = "Oh what did I expect... I was only delaying the inevitable.";
					Color rarityPink4 = Colors.RarityPink;
					byte r2 = rarityPink4.R;
					Color rarityPink5 = Colors.RarityPink;
					byte g2 = rarityPink5.G;
					Color rarityPink6 = Colors.RarityPink;
					Main.NewText(text2, r2, g2, rarityPink6.B, false);
				}
				if (base.projectile.localAI[0] == 310f)
				{
					string text3 = "You've won this fight. I will leave you.";
					Color rarityPink7 = Colors.RarityPink;
					byte r3 = rarityPink7.R;
					Color rarityPink8 = Colors.RarityPink;
					byte g3 = rarityPink8.G;
					Color rarityPink9 = Colors.RarityPink;
					Main.NewText(text3, r3, g3, rarityPink9.B, false);
				}
				if (base.projectile.localAI[0] == 420f)
				{
					string text4 = "But... If the Demigod ever notices you, and comes for you...";
					Color rarityPink10 = Colors.RarityPink;
					byte r4 = rarityPink10.R;
					Color rarityPink11 = Colors.RarityPink;
					byte g4 = rarityPink11.G;
					Color rarityPink12 = Colors.RarityPink;
					Main.NewText(text4, r4, g4, rarityPink12.B, false);
				}
				if (base.projectile.localAI[0] == 600f)
				{
					string text5 = "Don't tell him I'm still alive, It would be best for him to believe that robot had killed me...";
					Color rarityPink13 = Colors.RarityPink;
					byte r5 = rarityPink13.R;
					Color rarityPink14 = Colors.RarityPink;
					byte g5 = rarityPink14.G;
					Color rarityPink15 = Colors.RarityPink;
					Main.NewText(text5, r5, g5, rarityPink15.B, false);
				}
				if (base.projectile.localAI[0] == 900f)
				{
					string text6 = "And I'm too ashamed to face him.";
					Color rarityPink16 = Colors.RarityPink;
					byte r6 = rarityPink16.R;
					Color rarityPink17 = Colors.RarityPink;
					byte g6 = rarityPink17.G;
					Color rarityPink18 = Colors.RarityPink;
					Main.NewText(text6, r6, g6, rarityPink18.B, false);
				}
				if (base.projectile.localAI[0] == 1200f)
				{
					string text7 = "Well, this is a goodbye, Terrarian.";
					Color rarityPink19 = Colors.RarityPink;
					byte r7 = rarityPink19.R;
					Color rarityPink20 = Colors.RarityPink;
					byte g7 = rarityPink20.G;
					Color rarityPink21 = Colors.RarityPink;
					Main.NewText(text7, r7, g7, rarityPink21.B, false);
				}
				if (base.projectile.localAI[0] >= 1300f)
				{
					base.projectile.Kill();
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0f)
			{
				base.projectile.velocity.X = oldVelocity.X * --0f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * --0f;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("NebDefeated2"), 0, 3f, 255, 0f, 0f);
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.downedNebuleus)
			{
				RedeWorld.redemptionPoints -= 4;
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						CombatText.NewText(player.getRect(), Color.Red, "-4", true, false);
						for (int j = 0; j < player.inventory.Length; j++)
						{
							if (player.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> ... You... Oh no... I don't know if you can redeem yourself from that.", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedNebuleus = true;
			Redemption.GirusSilence = false;
		}
	}
}
