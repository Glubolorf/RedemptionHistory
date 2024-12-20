using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	[AutoloadBossHead]
	public class OmegaOblitDamaged : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Obliterator");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = 0;
			this.aiType = 0;
			base.npc.lifeMax = 65500;
			base.npc.damage = 280;
			base.npc.defense = 20;
			base.npc.knockBackResist = 0f;
			base.npc.width = 112;
			base.npc.height = 178;
			base.npc.value = (float)Item.buyPrice(0, 25, 0, 0);
			base.npc.npcSlots = 2f;
			base.npc.boss = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[base.mod.BuffType("UltraFlameDebuff")] = true;
			base.npc.buffImmune[base.mod.BuffType("EnjoymentDebuff")] = true;
			base.npc.netAlways = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit42;
			base.npc.DeathSound = SoundID.NPCDeath14;
			this.bossBag = base.mod.ItemType("OmegaOblitBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/OmegaGore1"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/OmegaGore2"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/OmegaGore3"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/OmegaGore4"), 1f);
				for (int i = 0; i < 120; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.7f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				for (int j = 0; j < 45; j++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex2].velocity *= 1.8f;
				}
				for (int k = 0; k < 25; k++)
				{
					int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex3].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.5f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.2f);
			base.npc.defense = base.npc.defense + numPlayers;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			if (!RedeWorld.downedVlitch3)
			{
				RedeWorld.redemptionPoints++;
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Good job! All Vlitch Overlords have been... Wait...", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gold, "+1", true, false);
					}
				}
			}
			RedeWorld.downedVlitch3 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk3 && !NPC.AnyNPCs(base.mod.NPCType("VlitchWormHead")) && !NPC.AnyNPCs(base.mod.NPCType("VlitchCleaver")) && !RedeWorld.girusCloaked && !RedeConfigClient.Instance.NoBossText)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), base.mod.ProjectileType("GirusTalking3"), 0, 0f, 255, 0f, 0f);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.9;
			return true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PlasmaJawser"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("OmegaClaw"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GloopContainer"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(14) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusMask"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(25, 35), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchBattery"), Main.rand.Next(3, 5), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("OblitBrain"), 1, false, 0, false, false);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (Main.rand.Next(10) == 0)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.1f;
			}
			if (!this.charging)
			{
				this.Move(new Vector2(-340f, 0f));
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 15.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 158;
				if (base.npc.frame.Y > 316)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.ai[0] == 0f)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.ai[1] += 1f;
				base.npc.dontTakeDamage = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] == 1f)
			{
				if (base.npc.life > 35500)
				{
					if (Main.rand.Next(250) == 0)
					{
						for (int i = 0; i < 20; i++)
						{
							int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[dustIndex2].velocity *= 1.9f;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
					}
					if (Main.rand.Next(250) == 0)
					{
						Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p2 = Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
					}
					if (Main.rand.Next(250) == 0)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p3 = Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						Main.projectile[p3].netUpdate = true;
					}
				}
				if (base.npc.life <= 35500)
				{
					if (Main.rand.Next(150) == 0)
					{
						for (int j = 0; j < 20; j++)
						{
							int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[dustIndex3].velocity *= 1.9f;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
						Main.projectile[p4].netUpdate = true;
					}
					if (Main.rand.Next(200) == 0)
					{
						Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p5 = Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						Main.projectile[p5].netUpdate = true;
					}
					if (Main.rand.Next(50) == 0)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p6 = Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						Main.projectile[p6].netUpdate = true;
					}
				}
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] <= 155f)
				{
					this.charging = true;
				}
				if (base.npc.ai[2] > 155f)
				{
					this.charging = false;
				}
				if (base.npc.ai[2] == 5f)
				{
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float rotation = (float)Math.Atan2((double)(vector8.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector8.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)rotation) * 26.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)rotation) * 26.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color = default(Color);
					Rectangle rectangle = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int count = 30;
					for (int k = 1; k <= count; k++)
					{
						int dust = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 235, 0f, 0f, 100, color, 2.5f);
						Main.dust[dust].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 35f)
				{
					Vector2 vector9 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector9.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)rotation2) * 32.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)rotation2) * 28.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color2 = default(Color);
					Rectangle rectangle2 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int count2 = 30;
					for (int l = 1; l <= count2; l++)
					{
						int dust2 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 235, 0f, 0f, 100, color2, 2.5f);
						Main.dust[dust2].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 65f)
				{
					Vector2 vector10 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector10.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)rotation3) * 38.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)rotation3) * 30.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color3 = default(Color);
					Rectangle rectangle3 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int count3 = 30;
					for (int m = 1; m <= count3; m++)
					{
						int dust3 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 235, 0f, 0f, 100, color3, 2.5f);
						Main.dust[dust3].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 95f)
				{
					Vector2 vector11 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float rotation4 = (float)Math.Atan2((double)(vector11.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector11.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)rotation4) * 42.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)rotation4) * 32.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color4 = default(Color);
					Rectangle rectangle4 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int count4 = 30;
					for (int n = 1; n <= count4; n++)
					{
						int dust4 = Dust.NewDust(base.npc.position, rectangle4.Width, rectangle4.Height, 235, 0f, 0f, 100, color4, 2.5f);
						Main.dust[dust4].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 125f)
				{
					Vector2 vector12 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float rotation5 = (float)Math.Atan2((double)(vector12.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector12.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)rotation5) * 26.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)rotation5) * 20.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color5 = default(Color);
					Rectangle rectangle5 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int count5 = 30;
					for (int i2 = 1; i2 <= count5; i2++)
					{
						int dust5 = Dust.NewDust(base.npc.position, rectangle5.Width, rectangle5.Height, 235, 0f, 0f, 100, color5, 2.5f);
						Main.dust[dust5].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 205f)
				{
					for (int i3 = 0; i3 < 20; i3++)
					{
						int dustIndex4 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex4].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
					Main.projectile[p7].netUpdate = true;
				}
				if (base.npc.ai[2] == 235f)
				{
					for (int i4 = 0; i4 < 20; i4++)
					{
						int dustIndex5 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex5].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p8 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
					Main.projectile[p8].netUpdate = true;
				}
				if (base.npc.ai[2] == 265f)
				{
					for (int i5 = 0; i5 < 20; i5++)
					{
						int dustIndex6 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex6].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p9 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
					Main.projectile[p9].netUpdate = true;
				}
				if (base.npc.ai[2] == 305f || base.npc.ai[2] == 310f)
				{
					int pieCut = 8;
					for (int m2 = 0; m2 < pieCut; m2++)
					{
						int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
						Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)m2 / (float)pieCut * 6.28f);
						Main.projectile[projID].netUpdate = true;
					}
				}
				if (base.npc.ai[2] == 320f)
				{
					for (int i6 = 0; i6 < 20; i6++)
					{
						int dustIndex7 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex7].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p10 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p10].netUpdate = true;
				}
				if (base.npc.ai[2] == 322f)
				{
					for (int i7 = 0; i7 < 20; i7++)
					{
						int dustIndex8 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex8].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p11 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(5f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p11].netUpdate = true;
				}
				if (base.npc.ai[2] == 325f)
				{
					for (int i8 = 0; i8 < 20; i8++)
					{
						int dustIndex9 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex9].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p12 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-5f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p12].netUpdate = true;
				}
				if (base.npc.ai[2] == 326f)
				{
					for (int i9 = 0; i9 < 20; i9++)
					{
						int dustIndex10 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex10].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p13 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-2f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p13].netUpdate = true;
				}
				if (base.npc.ai[2] == 328f)
				{
					for (int i10 = 0; i10 < 20; i10++)
					{
						int dustIndex11 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex11].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p14 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(7f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p14].netUpdate = true;
				}
				if (base.npc.ai[2] == 331f)
				{
					for (int i11 = 0; i11 < 20; i11++)
					{
						int dustIndex12 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex12].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p15 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(1f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p15].netUpdate = true;
				}
				if (base.npc.ai[2] == 332f)
				{
					for (int i12 = 0; i12 < 20; i12++)
					{
						int dustIndex13 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex13].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p16 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-8f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p16].netUpdate = true;
				}
				if (base.npc.ai[2] == 335f)
				{
					for (int i13 = 0; i13 < 20; i13++)
					{
						int dustIndex14 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex14].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p17 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(6f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p17].netUpdate = true;
				}
				if (base.npc.ai[2] == 336f)
				{
					for (int i14 = 0; i14 < 20; i14++)
					{
						int dustIndex15 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex15].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p18 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(2f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p18].netUpdate = true;
				}
				if (base.npc.ai[2] == 340f)
				{
					for (int i15 = 0; i15 < 20; i15++)
					{
						int dustIndex16 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[dustIndex16].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p19 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[p19].netUpdate = true;
				}
				if (base.npc.ai[2] >= 400f)
				{
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[1] == 1f)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int i16 = 0; i16 < 100; i16++)
				{
					int dustIndex17 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex17].velocity *= 1.9f;
				}
				for (int i17 = 0; i17 < 50; i17++)
				{
					int dustIndex18 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex18].velocity *= 1.9f;
				}
				for (int i18 = 0; i18 < 50; i18++)
				{
					int dustIndex19 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex19].velocity *= 1.9f;
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] == 40f)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int i19 = 0; i19 < 100; i19++)
				{
					int dustIndex20 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex20].velocity *= 1.9f;
				}
				for (int i20 = 0; i20 < 50; i20++)
				{
					int dustIndex21 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex21].velocity *= 1.9f;
				}
				for (int i21 = 0; i21 < 50; i21++)
				{
					int dustIndex22 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex22].velocity *= 1.9f;
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] == 70f)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int i22 = 0; i22 < 100; i22++)
				{
					int dustIndex23 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex23].velocity *= 1.9f;
				}
				for (int i23 = 0; i23 < 50; i23++)
				{
					int dustIndex24 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex24].velocity *= 1.9f;
				}
				for (int i24 = 0; i24 < 50; i24++)
				{
					int dustIndex25 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex25].velocity *= 1.9f;
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] == 1f)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "ERROR: POWER OVERLOAD...", true, false);
			}
			if (base.npc.ai[1] == 130f)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "I REQUIRE ASSISTANCE, GIRUS...", true, false);
			}
			if (base.npc.ai[1] == 250f)
			{
				base.npc.ai[0] = 1f;
				base.npc.dontTakeDamage = false;
				base.npc.netUpdate = true;
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			this.speed = 12f;
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 10f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.npc.velocity = move;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					this.deadTimer++;
					if (this.deadTimer == 2)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "TARGET OBLITERATED... RETURNING TO GIRUS...", true, false);
					}
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		private Player player;

		private float speed;

		private bool charging;

		private bool start;

		private bool beginAttacks;

		private int deadTimer;
	}
}
