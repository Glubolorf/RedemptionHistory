﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KingSlayerIII
{
	[AutoloadBossHead]
	public class KSEntrance : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III");
			Main.npcFrameCount[base.npc.type] = 13;
		}

		public override void SetDefaults()
		{
			base.npc.width = 64;
			base.npc.height = 112;
			base.npc.friendly = false;
			base.npc.damage = 60;
			base.npc.defense = 35;
			base.npc.lifeMax = 42000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.value = (float)Item.buyPrice(0, 30, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[base.mod.BuffType("UltraFlameDebuff")] = true;
			base.npc.buffImmune[base.mod.BuffType("EnjoymentDebuff")] = true;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossSlayer");
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.bossBag = base.mod.ItemType("SlayerBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			RedeWorld.downedSlayer = true;
			Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 0f), base.mod.ProjectileType("KSExitPro"), 0, 0f, 255, 0f, 0f);
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("SlayerTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingSlayerMask"), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				this.player.QuickSpawnItem(base.mod.ItemType("SlayerFlamethrower"), 1);
			}
			if (num == 1)
			{
				this.player.QuickSpawnItem(base.mod.ItemType("SlayerNanogun"), 1);
			}
			if (num == 2)
			{
				this.player.QuickSpawnItem(base.mod.ItemType("SlayerFist"), 1);
			}
			if (num == 3)
			{
				this.player.QuickSpawnItem(base.mod.ItemType("SlayerGun"), 1);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CyberPlating"), Main.rand.Next(8, 12), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingCore"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("SlayerMedal"), 1, false, 0, false, false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void FindFrame(int frameHeight)
		{
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			this.Target();
			this.DespawnHandler();
			Vector2.Distance(base.npc.Center, player.Center);
			if (this.attackMode == 0)
			{
				base.npc.aiStyle = -1;
				this.aiType = 0;
				base.npc.dontTakeDamage = true;
				base.npc.netUpdate = true;
			}
			if (this.idle1)
			{
				this.idle1Counter++;
				if (this.idle1Counter > 3)
				{
					this.idle1Frame++;
					this.idle1Counter = 0;
				}
				if (this.idle1Frame >= 5)
				{
					this.idle1Frame = 0;
				}
			}
			if (!this.start)
			{
				if (base.npc.frame.Y < 1800)
				{
					base.npc.frameCounter += 1.0;
				}
				if (base.npc.frameCounter >= 3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 150;
				}
			}
			if (this.idle2)
			{
				this.idle2Counter++;
				if (this.idle2Counter > 3)
				{
					this.idle2Frame++;
					this.idle2Counter = 0;
				}
				if (this.idle2Frame >= 5)
				{
					this.idle2Frame = 0;
				}
			}
			if (this.blinkGun)
			{
				this.blinkGunCounter++;
				if (this.blinkGunCounter > 3)
				{
					this.gunblinkFrame++;
					this.blinkGunCounter = 0;
				}
				if (this.gunblinkFrame >= 6)
				{
					this.gunblinkFrame = 0;
				}
			}
			if (this.shieldUp)
			{
				this.shieldCounter++;
				if (this.shieldCounter > 3)
				{
					this.shieldFrame++;
					this.shieldCounter = 0;
				}
				if (this.shieldFrame >= 7)
				{
					this.shieldFrame = 0;
				}
			}
			if (this.chargeAttack)
			{
				this.chargeCounter++;
				if (this.chargeCounter > 3)
				{
					this.chargeFrame++;
					this.chargeCounter = 0;
				}
				if (this.chargeFrame >= 2)
				{
					this.chargeFrame = 0;
				}
			}
			if (this.fistRocket)
			{
				this.rocketCounter++;
				if (this.rocketCounter > 3)
				{
					this.rocketFrame++;
					this.rocketCounter = 0;
				}
				if (this.rocketFrame >= 13)
				{
					this.rocketFrame = 0;
				}
			}
			if (this.fistRocketDone)
			{
				this.rocketDoneCounter++;
				if (this.rocketDoneCounter > 3)
				{
					this.rocketDoneFrame++;
					this.rocketDoneCounter = 0;
				}
				if (this.rocketDoneFrame >= 4)
				{
					this.rocketDoneFrame = 0;
				}
			}
			if (this.aiCounter == 39)
			{
				this.idle1 = true;
				this.start = true;
			}
			if (RedeWorld.downedSlayer)
			{
				if (this.aiCounter == 60)
				{
					string text = "So you've come back for more?";
					Color rarityCyan = Colors.RarityCyan;
					byte r = rarityCyan.R;
					Color rarityCyan2 = Colors.RarityCyan;
					byte g = rarityCyan2.G;
					Color rarityCyan3 = Colors.RarityCyan;
					Main.NewText(text, r, g, rarityCyan3.B, false);
				}
				if (this.aiCounter == 220)
				{
					string text2 = "Interesting...";
					Color rarityCyan4 = Colors.RarityCyan;
					byte r2 = rarityCyan4.R;
					Color rarityCyan5 = Colors.RarityCyan;
					byte g2 = rarityCyan5.G;
					Color rarityCyan6 = Colors.RarityCyan;
					Main.NewText(text2, r2, g2, rarityCyan6.B, false);
				}
				if (this.aiCounter == 400)
				{
					string text3 = "However, I still won't go easy on you...";
					Color rarityCyan7 = Colors.RarityCyan;
					byte r3 = rarityCyan7.R;
					Color rarityCyan8 = Colors.RarityCyan;
					byte g3 = rarityCyan8.G;
					Color rarityCyan9 = Colors.RarityCyan;
					Main.NewText(text3, r3, g3, rarityCyan9.B, false);
					this.idle1 = false;
					this.blinkGun = true;
				}
				if (this.aiCounter == 418)
				{
					this.blinkGun = false;
					this.idle2 = true;
				}
				if (this.aiCounter == 500)
				{
					this.fightBegin = true;
					this.attackMode = 1;
					base.npc.dontTakeDamage = false;
					NPC npc2 = base.npc;
					npc2.velocity.X = npc2.velocity.X * 0.98f;
					NPC npc3 = base.npc;
					npc3.velocity.Y = npc3.velocity.Y * 0.98f;
					this.aiCounter = 0;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num = (float)Math.Atan2((double)(vector.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num) * 12.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num) * 12.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color = default(Color);
					Rectangle rectangle;
					rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num2 = 30;
					for (int i = 1; i <= num2; i++)
					{
						int num3 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 226, 0f, 0f, 100, color, 2.5f);
						Main.dust[num3].noGravity = false;
					}
					return;
				}
			}
			if (!RedeWorld.downedSlayer)
			{
				if (RedeWorld.deathBySlayer)
				{
					if (this.aiCounter == 60)
					{
						string text4 = "Ready for a rematch?";
						Color rarityCyan10 = Colors.RarityCyan;
						byte r4 = rarityCyan10.R;
						Color rarityCyan11 = Colors.RarityCyan;
						byte g4 = rarityCyan11.G;
						Color rarityCyan12 = Colors.RarityCyan;
						Main.NewText(text4, r4, g4, rarityCyan12.B, false);
					}
					if (this.aiCounter == 180)
					{
						this.idle1 = false;
						this.blinkGun = true;
					}
					if (this.aiCounter == 198)
					{
						this.blinkGun = false;
						this.idle2 = true;
					}
					if (this.aiCounter == 300)
					{
						this.fightBegin = true;
						this.attackMode = 1;
						base.npc.dontTakeDamage = false;
						NPC npc4 = base.npc;
						npc4.velocity.X = npc4.velocity.X * 0.98f;
						NPC npc5 = base.npc;
						npc5.velocity.Y = npc5.velocity.Y * 0.98f;
						this.aiCounter = 0;
						Vector2 vector2;
						vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num4 = (float)Math.Atan2((double)(vector2.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector2.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num4) * 12.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num4) * 12.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
						Color color2 = default(Color);
						Rectangle rectangle2;
						rectangle2..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num5 = 30;
						for (int j = 1; j <= num5; j++)
						{
							int num6 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 226, 0f, 0f, 100, color2, 2.5f);
							Main.dust[num6].noGravity = false;
						}
						return;
					}
				}
				if (!RedeWorld.deathBySlayer)
				{
					if (this.aiCounter == 60)
					{
						string text5 = "Well what do we have here?";
						Color rarityCyan13 = Colors.RarityCyan;
						byte r5 = rarityCyan13.R;
						Color rarityCyan14 = Colors.RarityCyan;
						byte g5 = rarityCyan14.G;
						Color rarityCyan15 = Colors.RarityCyan;
						Main.NewText(text5, r5, g5, rarityCyan15.B, false);
					}
					if (this.aiCounter == 220)
					{
						string text6 = "I was ordered to kill a ravaging Undead known as the Keeper...";
						Color rarityCyan16 = Colors.RarityCyan;
						byte r6 = rarityCyan16.R;
						Color rarityCyan17 = Colors.RarityCyan;
						byte g6 = rarityCyan17.G;
						Color rarityCyan18 = Colors.RarityCyan;
						Main.NewText(text6, r6, g6, rarityCyan18.B, false);
					}
					if (this.aiCounter == 400)
					{
						string text7 = "However, you killed it before I arrived...";
						Color rarityCyan19 = Colors.RarityCyan;
						byte r7 = rarityCyan19.R;
						Color rarityCyan20 = Colors.RarityCyan;
						byte g7 = rarityCyan20.G;
						Color rarityCyan21 = Colors.RarityCyan;
						Main.NewText(text7, r7, g7, rarityCyan21.B, false);
					}
					if (this.aiCounter == 680)
					{
						string text8 = "You stole my kill... You know what? I don't care if I'm a member of the Heroes! You're dead!";
						Color rarityCyan22 = Colors.RarityCyan;
						byte r8 = rarityCyan22.R;
						Color rarityCyan23 = Colors.RarityCyan;
						byte g8 = rarityCyan23.G;
						Color rarityCyan24 = Colors.RarityCyan;
						Main.NewText(text8, r8, g8, rarityCyan24.B, false);
						this.idle1 = false;
						this.blinkGun = true;
					}
					if (this.aiCounter == 698)
					{
						this.blinkGun = false;
						this.idle2 = true;
					}
					if (this.aiCounter == 900)
					{
						this.fightBegin = true;
						RedeWorld.deathBySlayer = true;
						this.attackMode = 1;
						base.npc.dontTakeDamage = false;
						NPC npc6 = base.npc;
						npc6.velocity.X = npc6.velocity.X * 0.98f;
						NPC npc7 = base.npc;
						npc7.velocity.Y = npc7.velocity.Y * 0.98f;
						this.aiCounter = 0;
						Vector2 vector3;
						vector3..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num7 = (float)Math.Atan2((double)(vector3.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector3.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num7) * 12.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num7) * 12.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
						Color color3 = default(Color);
						Rectangle rectangle3;
						rectangle3..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num8 = 30;
						for (int k = 1; k <= num8; k++)
						{
							int num9 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 226, 0f, 0f, 100, color3, 2.5f);
							Main.dust[num9].noGravity = false;
						}
						return;
					}
				}
			}
			if (this.attackMode == 0)
			{
				this.aiCounter++;
			}
			if (this.fightBegin)
			{
				base.npc.noTileCollide = false;
				base.npc.noGravity = false;
			}
			if (this.attackMode == 1)
			{
				base.npc.aiStyle = 3;
				this.aiType = 425;
				this.timer1++;
				if (this.timer1 <= 300)
				{
					this.lightningOrbTimer++;
					if (this.lightningOrbTimer == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 0f), 465, 40, 3f, 255, 0f, 0f);
					}
				}
				if (this.timer1 >= 300 && this.timer1 < 700)
				{
					this.lightningOrbTimer = 0;
					this.pewPew1Timer++;
					this.pewPew2Timer++;
					if (base.npc.life <= 38000 && base.npc.life > 30000 && this.pewPew1Timer >= 30)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 50, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 50, 3f, 255, 0f, 0f);
						}
						this.pewPew1Timer = 0;
					}
					if (base.npc.life > 38000 && this.pewPew1Timer >= 40)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 50, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 50, 3f, 255, 0f, 0f);
						}
						this.pewPew1Timer = 0;
					}
					if (base.npc.life <= 30000 && this.pewPew1Timer >= 20)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 50, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 50, 3f, 255, 0f, 0f);
						}
						this.pewPew1Timer = 0;
					}
					if (base.npc.life <= 38000 && base.npc.life > 30000 && this.pewPew2Timer >= 70 && this.timer1 < 600)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 40, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 40, 3f, 255, 0f, 0f);
						}
						this.pewPew2Timer = 0;
					}
					if (base.npc.life <= 30000 && this.pewPew2Timer >= 50 && this.timer1 < 600)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, 2f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, -2f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 40, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, 2f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, -2f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 40, 3f, 255, 0f, 0f);
						}
						this.pewPew2Timer = 0;
					}
					if (base.npc.life > 38000 && this.pewPew2Timer >= 100 && this.timer1 < 600)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 40, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 40, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 40, 3f, 255, 0f, 0f);
						}
						this.pewPew2Timer = 0;
					}
				}
				if (this.timer1 >= 600)
				{
					if (base.npc.life <= 38000 && base.npc.life > 30000)
					{
						if (this.pewPew1Timer >= 10)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 50, 3f, 255, 0f, 0f);
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 50, 3f, 255, 0f, 0f);
							}
							this.pewPew1Timer = 0;
						}
						if (this.timer1 >= 650)
						{
							this.timer1 = 0;
						}
					}
					if (base.npc.life <= 30000)
					{
						if (this.pewPew1Timer >= 8)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 50, 3f, 255, 0f, 0f);
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 50, 3f, 255, 0f, 0f);
							}
							this.pewPew1Timer = 0;
						}
						if (this.timer1 >= 700)
						{
							this.timer1 = 0;
						}
					}
					if (base.npc.life > 38000)
					{
						this.timer1 = 0;
					}
				}
			}
			if (base.npc.life <= 38000 && !this.shieldEvent1)
			{
				this.attackMode = 2;
				this.shieldTimer1++;
				if (this.shieldTimer1 <= 300)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
					if (this.shieldTimer1 == 60)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 465, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer1 == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 465, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 465, 40, 3f, 255, 0f, 0f);
					}
				}
				if (this.shieldTimer1 >= 300)
				{
					this.shieldTimer1 = 0;
					this.shieldEvent1 = true;
					this.timer1 = 0;
					this.attackMode = 1;
					this.shieldUp = false;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 30000 && !this.shieldEvent2)
			{
				this.attackMode = 2;
				this.shieldTimer2++;
				if (this.shieldTimer2 == 1)
				{
					string text9 = "Alright, you are really starting to piss me off!";
					Color rarityCyan25 = Colors.RarityCyan;
					byte r9 = rarityCyan25.R;
					Color rarityCyan26 = Colors.RarityCyan;
					byte g9 = rarityCyan26.G;
					Color rarityCyan27 = Colors.RarityCyan;
					Main.NewText(text9, r9, g9, rarityCyan27.B, false);
				}
				if (this.shieldTimer2 < 570)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
				}
				if (this.shieldTimer2 >= 120 && this.shieldTimer2 < 570)
				{
					if (this.shieldTimer2 == 180)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer2 == 240)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer2 == 300)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer2 == 360)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 465, 40, 3f, 255, 0f, 0f);
					}
				}
				if (this.shieldTimer2 >= 570)
				{
					this.shieldTimer2 = 0;
					this.shieldEvent2 = true;
					this.timer1 = 0;
					this.attackMode = 1;
					this.shieldUp = false;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 24000 && !this.shieldEvent3)
			{
				this.attackMode = 2;
				this.shieldTimer3++;
				if (this.shieldTimer3 < 480)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
					if (this.shieldTimer3 == 30)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 150)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 60)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 180)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 240)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 465, 40, 3f, 255, 0f, 0f);
					}
				}
				if (this.shieldTimer3 >= 480)
				{
					this.shieldTimer3 = 0;
					this.shieldEvent3 = true;
					this.timer1 = 0;
					this.attackMode = 1;
					this.shieldUp = false;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 22000 && !this.rocketEvent1)
			{
				this.attackMode = 2;
				this.idle2 = false;
				this.fistRocket = true;
				this.fistTimer++;
				if (this.fistTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
				}
				if (this.fistTimer >= 39 && this.fistTimer < 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = true;
				}
				if (this.fistTimer >= 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = false;
					this.fistTimer = 0;
					this.rocketEvent1 = true;
					this.attackMode = 1;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 20000 && !this.rocketEvent2)
			{
				this.attackMode = 2;
				this.idle2 = false;
				this.fistRocket = true;
				this.fistTimer++;
				if (this.fistTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
				}
				if (this.fistTimer >= 39 && this.fistTimer < 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = true;
				}
				if (this.fistTimer >= 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = false;
					this.fistTimer = 0;
					this.rocketEvent2 = true;
					this.attackMode = 1;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 17500 && !this.rocketEvent3)
			{
				this.attackMode = 2;
				this.idle2 = false;
				this.fistRocket = true;
				this.fistTimer++;
				if (this.fistTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
				}
				if (this.fistTimer >= 39 && this.fistTimer < 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = true;
				}
				if (this.fistTimer >= 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = false;
					this.fistTimer = 0;
					this.rocketEvent3 = true;
					this.attackMode = 1;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 16500 && !this.rocketEvent4)
			{
				this.attackMode = 2;
				this.idle2 = false;
				this.fistRocket = true;
				this.fistTimer++;
				if (this.fistTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
				}
				if (this.fistTimer >= 39 && this.fistTimer < 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = true;
				}
				if (this.fistTimer >= 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = false;
					this.fistTimer = 0;
					this.rocketEvent4 = true;
					this.attackMode = 1;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 15500 && !this.rocketEvent5)
			{
				this.attackMode = 2;
				this.idle2 = false;
				this.fistRocket = true;
				this.fistTimer++;
				if (this.fistTimer == 21)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 60, 3f, 255, 0f, 0f);
					}
				}
				if (this.fistTimer >= 39 && this.fistTimer < 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = true;
				}
				if (this.fistTimer >= 51)
				{
					this.fistRocket = false;
					this.fistRocketDone = false;
					this.fistTimer = 0;
					this.rocketEvent5 = true;
					this.attackMode = 1;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 12000 && !this.shieldEvent4)
			{
				this.attackMode = 2;
				this.shieldTimer4++;
				if (this.shieldTimer4 < 480)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
					if (this.shieldTimer4 == 30)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 60)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 90)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 150)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 40, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 60)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 180)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 465, 40, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 240)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 465, 40, 3f, 255, 0f, 0f);
					}
				}
				if (this.shieldTimer4 >= 480)
				{
					this.shieldTimer4 = 0;
					this.shieldEvent4 = true;
					this.timer1 = 0;
					this.attackMode = 1;
					this.shieldUp = false;
					this.idle2 = true;
				}
			}
			if (this.fightBegin)
			{
				if (base.npc.life <= 10000 && Main.rand.Next(500) == 0)
				{
					this.teleport = true;
				}
				if (base.npc.life > 10000 && Main.rand.Next(750) == 0)
				{
					this.teleport = true;
				}
			}
			if (this.teleport)
			{
				this.teleportTimer++;
				if (this.teleportTimer == 2)
				{
					for (int l = 0; l < 20; l++)
					{
						int num10 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num10].velocity *= 1.4f;
					}
				}
				if (this.teleportTimer == 4 && Main.netMode != 1)
				{
					int num11 = Main.rand.Next(2);
					if (num11 == 0)
					{
						Vector2 vector4;
						vector4..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-300, -200));
						base.npc.Center = Main.player[base.npc.target].Center + vector4;
						base.npc.netUpdate = true;
					}
					if (num11 == 1)
					{
						Vector2 vector5;
						vector5..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-300, -200));
						base.npc.Center = Main.player[base.npc.target].Center + vector5;
						base.npc.netUpdate = true;
					}
				}
				if (this.teleportTimer >= 6)
				{
					for (int m = 0; m < 20; m++)
					{
						int num12 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num12].velocity *= 1.4f;
					}
					this.teleport = false;
					this.teleportTimer = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSCharge");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle1");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle2");
			Texture2D texture4 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSRocket1");
			Texture2D texture5 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSRocket2");
			Texture2D texture6 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSShield");
			base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSExit");
			Texture2D texture7 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSGunBlink");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			if (!this.start)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			}
			if (this.blinkGun)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture7.Height / 6;
				int num2 = num * this.gunblinkFrame;
				Main.spriteBatch.Draw(texture7, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture7.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture7.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.idle1)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 5;
				int num4 = num3 * this.idle1Frame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.idle2)
			{
				Vector2 vector3;
				vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num5 = texture3.Height / 5;
				int num6 = num5 * this.idle2Frame;
				Main.spriteBatch.Draw(texture3, vector3 - Main.screenPosition, new Rectangle?(new Rectangle(0, num6, texture3.Width, num5)), drawColor, base.npc.rotation, new Vector2((float)texture3.Width / 2f, (float)num5 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.shieldUp)
			{
				Vector2 vector4;
				vector4..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num7 = texture6.Height / 7;
				int num8 = num7 * this.shieldFrame;
				Main.spriteBatch.Draw(texture6, vector4 - Main.screenPosition, new Rectangle?(new Rectangle(0, num8, texture6.Width, num7)), drawColor, base.npc.rotation, new Vector2((float)texture6.Width / 2f, (float)num7 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.chargeAttack)
			{
				Vector2 vector5;
				vector5..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num9 = texture.Height / 2;
				int num10 = num9 * this.chargeFrame;
				Main.spriteBatch.Draw(texture, vector5 - Main.screenPosition, new Rectangle?(new Rectangle(0, num10, texture.Width, num9)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num9 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.fistRocket)
			{
				Vector2 vector6;
				vector6..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num11 = texture4.Height / 13;
				int num12 = num11 * this.rocketFrame;
				Main.spriteBatch.Draw(texture4, vector6 - Main.screenPosition, new Rectangle?(new Rectangle(0, num12, texture4.Width, num11)), drawColor, base.npc.rotation, new Vector2((float)texture4.Width / 2f, (float)num11 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.fistRocketDone)
			{
				Vector2 vector7;
				vector7..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num13 = texture5.Height / 4;
				int num14 = num13 * this.rocketDoneFrame;
				Main.spriteBatch.Draw(texture5, vector7 - Main.screenPosition, new Rectangle?(new Rectangle(0, num14, texture5.Width, num13)), drawColor, base.npc.rotation, new Vector2((float)texture5.Width / 2f, (float)num13 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			return false;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		private Player player;

		public bool chargeAttack;

		public bool idle1;

		public bool idle2;

		public bool fistRocket;

		public bool fistRocketDone;

		public bool shieldUp;

		public bool blinkGun;

		public int timer1;

		public int timer2;

		public int lightningOrbTimer;

		public int pewPew1Timer;

		public int pewPew2Timer;

		public int shieldTimer1;

		public int shieldTimer2;

		public int shieldTimer3;

		public int shieldTimer4;

		public bool shieldEvent1;

		public bool shieldEvent2;

		public bool shieldEvent3;

		public bool shieldEvent4;

		public int fistTimer;

		public int teleportTimer;

		public bool teleport;

		public bool rocketEvent1;

		public bool rocketEvent2;

		public bool rocketEvent3;

		public bool rocketEvent4;

		public bool rocketEvent5;

		private int idle1Counter;

		private int idle2Counter;

		private int blinkGunCounter;

		private int shieldCounter;

		private int chargeCounter;

		private int rocketCounter;

		private int rocketDoneCounter;

		private int aiCounter;

		private int idle1Frame;

		private int idle2Frame;

		private int gunblinkFrame;

		private int shieldFrame;

		private int chargeFrame;

		private int rocketFrame;

		private int rocketDoneFrame;

		private int attackMode;

		private bool start;

		private bool fightBegin;
	}
}