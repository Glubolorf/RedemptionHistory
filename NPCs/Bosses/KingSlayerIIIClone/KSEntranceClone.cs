using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KingSlayerIIIClone
{
	[AutoloadBossHead]
	public class KSEntranceClone : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III... ?");
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
			base.npc.DeathSound = SoundID.NPCDeath14;
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
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 65; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num].velocity *= 1.8f;
				}
				for (int j = 0; j < 35; j++)
				{
					int num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num2].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A King Slayer III Clone";
			potionType = 499;
			RedeWorld.downedSlayer = true;
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
				base.npc.netUpdate = true;
			}
			if (this.aiCounter == 0 && !this.shieldUp)
			{
				base.npc.dontTakeDamage = false;
				base.npc.netUpdate = true;
			}
			if (this.aiCounter == 60)
			{
				string text = "KING SLAYER NO LONGER HAS TIME FOR YOU...";
				Color rarityCyan = Colors.RarityCyan;
				byte r = rarityCyan.R;
				Color rarityCyan2 = Colors.RarityCyan;
				byte g = rarityCyan2.G;
				Color rarityCyan3 = Colors.RarityCyan;
				Main.NewText(text, r, g, rarityCyan3.B, false);
			}
			if (this.aiCounter == 180)
			{
				base.npc.netUpdate = true;
				this.idle1 = false;
				this.blinkGun = true;
			}
			if (this.aiCounter == 198)
			{
				this.blinkGun = false;
				this.idle2 = true;
				base.npc.netUpdate = true;
			}
			if (this.aiCounter == 300)
			{
				base.npc.netUpdate = true;
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
				base.npc.netUpdate = true;
				base.npc.aiStyle = 3;
				this.aiType = 425;
				this.timer1++;
				if (this.timer1 <= 300)
				{
					this.lightningOrbTimer++;
					if (this.lightningOrbTimer == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
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
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 35, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 35, 3f, 255, 0f, 0f);
						}
						this.pewPew1Timer = 0;
					}
					if (base.npc.life > 38000 && this.pewPew1Timer >= 40)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 35, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 35, 3f, 255, 0f, 0f);
						}
						this.pewPew1Timer = 0;
					}
					if (base.npc.life <= 30000 && this.pewPew1Timer >= 20)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 35, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 35, 3f, 255, 0f, 0f);
						}
						this.pewPew1Timer = 0;
					}
					if (base.npc.life <= 38000 && base.npc.life > 30000 && this.pewPew2Timer >= 70 && this.timer1 < 600)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
						}
						this.pewPew2Timer = 0;
					}
					if (base.npc.life <= 30000 && this.pewPew2Timer >= 50 && this.timer1 < 600)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, 2f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, -2f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, 2f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, -2f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
						}
						this.pewPew2Timer = 0;
					}
					if (base.npc.life > 38000 && this.pewPew2Timer >= 100 && this.timer1 < 600)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
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
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 35, 3f, 255, 0f, 0f);
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 35, 3f, 255, 0f, 0f);
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
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 35, 3f, 255, 0f, 0f);
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 35, 3f, 255, 0f, 0f);
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
				base.npc.netUpdate = true;
				this.attackMode = 2;
				this.shieldTimer1++;
				if (this.shieldTimer1 <= 300)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.netUpdate = true;
					base.npc.dontTakeDamage = true;
					if (this.shieldTimer1 == 60)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 465, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer1 == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 465, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 465, 30, 3f, 255, 0f, 0f);
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
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= 30000 && !this.shieldEvent2)
			{
				base.npc.netUpdate = true;
				this.attackMode = 2;
				this.shieldTimer2++;
				if (this.shieldTimer2 == 1)
				{
					string text2 = "POWERING UP...";
					Color rarityCyan4 = Colors.RarityCyan;
					byte r2 = rarityCyan4.R;
					Color rarityCyan5 = Colors.RarityCyan;
					byte g2 = rarityCyan5.G;
					Color rarityCyan6 = Colors.RarityCyan;
					Main.NewText(text2, r2, g2, rarityCyan6.B, false);
				}
				if (this.shieldTimer2 < 570)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
					base.npc.netUpdate = true;
				}
				if (this.shieldTimer2 >= 120 && this.shieldTimer2 < 570)
				{
					if (this.shieldTimer2 == 180)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer2 == 240)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer2 == 300)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer2 == 360)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 465, 30, 3f, 255, 0f, 0f);
					}
				}
				if (this.shieldTimer2 >= 570)
				{
					this.shieldTimer2 = 0;
					this.shieldEvent2 = true;
					this.timer1 = 0;
					this.attackMode = 1;
					this.shieldUp = false;
					base.npc.netUpdate = true;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 24000 && !this.shieldEvent3)
			{
				base.npc.netUpdate = true;
				this.attackMode = 2;
				this.shieldTimer3++;
				if (this.shieldTimer3 < 480)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.netUpdate = true;
					base.npc.dontTakeDamage = true;
					if (this.shieldTimer3 == 30)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 150)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 60)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 180)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer3 == 240)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 465, 30, 3f, 255, 0f, 0f);
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
					base.npc.netUpdate = true;
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
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
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
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
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
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
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
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
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
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 50, 3f, 255, 0f, 0f);
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
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 60)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 90)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 150)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, -6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 6f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(6f, 0f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 435, 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 435, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 60)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 120)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 180)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.shieldTimer4 == 240)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), 465, 30, 3f, 255, 0f, 0f);
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
				if (base.npc.velocity.X == 0f && base.npc.velocity.Y == 0f)
				{
					this.noMoveTimer++;
					if (this.noMoveTimer >= 60)
					{
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					this.noMoveTimer = 0;
				}
				if (base.npc.life <= 10000 && Main.rand.Next(500) == 0)
				{
					this.teleport = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.life > 10000 && Main.rand.Next(750) == 0)
				{
					this.teleport = true;
					base.npc.netUpdate = true;
				}
			}
			if (this.teleport)
			{
				base.npc.netUpdate = true;
				this.teleportTimer++;
				if (this.teleportTimer == 2)
				{
					for (int j = 0; j < 20; j++)
					{
						int num4 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num4].velocity *= 1.4f;
					}
				}
				if (this.teleportTimer == 4 && Main.netMode != 1)
				{
					int num5 = Main.rand.Next(2);
					if (num5 == 0)
					{
						Vector2 vector2;
						vector2..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-300, -200));
						base.npc.Center = Main.player[base.npc.target].Center + vector2;
						base.npc.netUpdate = true;
					}
					if (num5 == 1)
					{
						Vector2 vector3;
						vector3..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-300, -200));
						base.npc.Center = Main.player[base.npc.target].Center + vector3;
						base.npc.netUpdate = true;
					}
				}
				if (this.teleportTimer >= 6)
				{
					for (int k = 0; k < 20; k++)
					{
						int num6 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num6].velocity *= 1.4f;
					}
					this.teleport = false;
					this.teleportTimer = 0;
					base.npc.netUpdate = true;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSCharge");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSIdle1");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSIdle2");
			Texture2D texture4 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSRocket1");
			Texture2D texture5 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSRocket2");
			Texture2D texture6 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSShield");
			base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSExit");
			Texture2D texture7 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSGunBlink");
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

		public int noMoveTimer;

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
