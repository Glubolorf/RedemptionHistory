using System;
using System.IO;
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
			Main.npcFrameCount[base.npc.type] = 12;
		}

		public override void SetDefaults()
		{
			base.npc.width = 64;
			base.npc.height = 112;
			base.npc.friendly = false;
			base.npc.damage = 80;
			base.npc.defense = 35;
			base.npc.lifeMax = 42000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.value = (float)Item.buyPrice(0, 20, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[base.mod.BuffType("UltraFlameDebuff")] = true;
			base.npc.buffImmune[base.mod.BuffType("EnjoymentDebuff")] = true;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
			base.npc.aiStyle = 0;
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
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
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
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.55f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
				writer.Write(this.customAI[1]);
				writer.Write(this.customAI[2]);
				writer.Write(this.customAI[3]);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
				this.customAI[1] = reader.ReadFloat();
				this.customAI[2] = reader.ReadFloat();
				this.customAI[3] = reader.ReadFloat();
			}
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			this.Target();
			this.DespawnHandler();
			if (!this.chargeAttack && !this.chargeAttackR)
			{
				if (player.Center.X > base.npc.Center.X)
				{
					this.Move(new Vector2(-250f, 0f));
				}
				else
				{
					this.Move(new Vector2(250f, 0f));
				}
			}
			if (this.attackMode == 0)
			{
				base.npc.aiStyle = 0;
				this.aiType = 0;
				base.npc.dontTakeDamage = true;
			}
			if (this.idle1)
			{
				this.idle1Counter++;
				if (this.idle1Counter > 3)
				{
					this.idle1Frame++;
					this.idle1Counter = 0;
				}
				if (this.idle1Frame >= 4)
				{
					this.idle1Frame = 0;
				}
			}
			if (!this.start)
			{
				if (base.npc.frame.Y < 1650)
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
				if (this.idle2Frame >= 4)
				{
					this.idle2Frame = 0;
				}
			}
			if (this.idle2R)
			{
				this.idle2RCounter++;
				if (this.idle2RCounter > 3)
				{
					this.idle2RFrame++;
					this.idle2RCounter = 0;
				}
				if (this.idle2RFrame >= 4)
				{
					this.idle2RFrame = 0;
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
				if (this.gunblinkFrame >= 4)
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
			if (this.chargeAttackR)
			{
				this.chargeRCounter++;
				if (this.chargeRCounter > 3)
				{
					this.chargeRFrame++;
					this.chargeRCounter = 0;
				}
				if (this.chargeRFrame >= 2)
				{
					this.chargeRFrame = 0;
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
				if (this.rocketFrame >= 12)
				{
					this.rocketFrame = 0;
				}
			}
			if (this.fistRocketR)
			{
				this.rocketRCounter++;
				if (this.rocketRCounter > 3)
				{
					this.rocketRFrame++;
					this.rocketRCounter = 0;
				}
				if (this.rocketRFrame >= 12)
				{
					this.rocketRFrame = 0;
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
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num >= 1200f && !this.teleport)
			{
				this.teleport = true;
			}
			if (this.customAI[0] == 39f)
			{
				this.idle1 = true;
				this.start = true;
				base.npc.netUpdate = true;
			}
			if (this.customAI[0] == 60f)
			{
				string text = "SCANNING TARGET...";
				Color rarityCyan = Colors.RarityCyan;
				byte r = rarityCyan.R;
				rarityCyan = Colors.RarityCyan;
				byte g = rarityCyan.G;
				rarityCyan = Colors.RarityCyan;
				Main.NewText(text, r, g, rarityCyan.B, false);
			}
			if (this.customAI[0] == 220f)
			{
				string text2 = "TARGET DEEMED: 'A WASTE OF TIME'";
				Color rarityCyan = Colors.RarityCyan;
				byte r2 = rarityCyan.R;
				rarityCyan = Colors.RarityCyan;
				byte g2 = rarityCyan.G;
				rarityCyan = Colors.RarityCyan;
				Main.NewText(text2, r2, g2, rarityCyan.B, false);
			}
			if (this.customAI[0] == 400f)
			{
				string text3 = "RELAYING MESSAGE: 'KING SLAYER NO LONGER HAS TIME FOR YOU'";
				Color rarityCyan = Colors.RarityCyan;
				byte r3 = rarityCyan.R;
				rarityCyan = Colors.RarityCyan;
				byte g3 = rarityCyan.G;
				rarityCyan = Colors.RarityCyan;
				Main.NewText(text3, r3, g3, rarityCyan.B, false);
				this.idle1 = false;
				this.blinkGun = true;
				base.npc.netUpdate = true;
			}
			if (this.customAI[0] == 418f)
			{
				this.blinkGun = false;
				this.idle2 = true;
				base.npc.netUpdate = true;
			}
			if (this.customAI[0] == 500f)
			{
				this.fightBegin = true;
				this.attackMode = 1;
				base.npc.dontTakeDamage = false;
				this.customAI[0] = 0f;
			}
			if (this.customAI[0] == 0f && !this.shieldUp)
			{
				base.npc.dontTakeDamage = false;
			}
			if (this.attackMode == 0)
			{
				this.customAI[0] += 1f;
			}
			if (this.fightBegin)
			{
				base.npc.noTileCollide = true;
				base.npc.noGravity = true;
			}
			if (this.attackMode == 1)
			{
				if (!this.shieldEvent1 && !this.shieldEvent2 && !this.shieldEvent3 && !this.shieldEvent4)
				{
					this.customAI[1] += 1f;
					if (this.customAI[1] == 120f)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.customAI[1] >= 300f && this.customAI[1] < 700f)
					{
						this.pewPew1Timer++;
						if (this.pewPew1Timer == 40 || this.pewPew1Timer == 80 || this.pewPew1Timer == 120)
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
						}
						if (this.pewPew1Timer >= 120)
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
							this.pewPew1Timer = 0;
						}
					}
					if (this.customAI[1] == 800f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector;
						vector..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num2 = (float)Math.Atan2((double)(vector.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num2) * 40.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num2) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color = default(Color);
						Rectangle rectangle;
						rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num3 = 10;
						for (int i = 1; i <= num3; i++)
						{
							int num4 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 31, 0f, 0f, 100, color, 2.5f);
							Main.dust[num4].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 830f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] == 900f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector2;
						vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num5 = (float)Math.Atan2((double)(vector2.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector2.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num5) * 40.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num5) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color2 = default(Color);
						Rectangle rectangle2;
						rectangle2..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num6 = 10;
						for (int j = 1; j <= num6; j++)
						{
							int num7 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 31, 0f, 0f, 100, color2, 2.5f);
							Main.dust[num7].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 930f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] >= 1000f)
					{
						this.customAI[1] = 0f;
					}
				}
				if (this.shieldEvent1 && !this.shieldEvent2 && !this.shieldEvent3 && !this.shieldEvent4)
				{
					this.customAI[1] += 1f;
					if (this.customAI[1] == 120f)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.customAI[1] >= 300f && this.customAI[1] < 700f)
					{
						this.pewPew1Timer++;
						if (this.pewPew1Timer == 30 || this.pewPew1Timer == 60 || this.pewPew1Timer == 90)
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
						}
						if (this.pewPew1Timer >= 90)
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
							this.pewPew1Timer = 0;
						}
					}
					if (this.customAI[1] >= 700f && this.customAI[1] < 800f)
					{
						this.pewPew1Timer++;
						if (this.pewPew1Timer >= 15)
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
					}
					if (this.customAI[1] == 860f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector3;
						vector3..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num8 = (float)Math.Atan2((double)(vector3.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector3.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num8) * 50.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num8) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color3 = default(Color);
						Rectangle rectangle3;
						rectangle3..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num9 = 10;
						for (int k = 1; k <= num9; k++)
						{
							int num10 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 31, 0f, 0f, 100, color3, 2.5f);
							Main.dust[num10].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 830f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] == 900f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector4;
						vector4..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num11 = (float)Math.Atan2((double)(vector4.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector4.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num11) * 50.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num11) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color4 = default(Color);
						Rectangle rectangle4;
						rectangle4..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num12 = 10;
						for (int l = 1; l <= num12; l++)
						{
							int num13 = Dust.NewDust(base.npc.position, rectangle4.Width, rectangle4.Height, 31, 0f, 0f, 100, color4, 2.5f);
							Main.dust[num13].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 930f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] >= 980f)
					{
						this.customAI[1] = 0f;
					}
				}
				if (this.shieldEvent2 && !this.shieldEvent3 && !this.shieldEvent4)
				{
					this.customAI[1] += 1f;
					if (this.customAI[1] == 120f)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.customAI[1] >= 300f && this.customAI[1] < 700f)
					{
						this.pewPew1Timer++;
						if (this.pewPew1Timer == 20 || this.pewPew1Timer == 40 || this.pewPew1Timer == 60)
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
						}
						if (this.pewPew1Timer >= 90)
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
							this.pewPew1Timer = 0;
						}
					}
					if (this.customAI[1] >= 700f && this.customAI[1] < 760f)
					{
						this.pewPew1Timer++;
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
					}
					if (this.customAI[1] == 820f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] == 900f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector5;
						vector5..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num14 = (float)Math.Atan2((double)(vector5.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector5.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num14) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num14) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color5 = default(Color);
						Rectangle rectangle5;
						rectangle5..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num15 = 10;
						for (int m = 1; m <= num15; m++)
						{
							int num16 = Dust.NewDust(base.npc.position, rectangle5.Width, rectangle5.Height, 31, 0f, 0f, 100, color5, 2.5f);
							Main.dust[num16].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 930f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] == 1000f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector6;
						vector6..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num17 = (float)Math.Atan2((double)(vector6.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector6.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num17) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num17) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color6 = default(Color);
						Rectangle rectangle6;
						rectangle6..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num18 = 10;
						for (int n = 1; n <= num18; n++)
						{
							int num19 = Dust.NewDust(base.npc.position, rectangle6.Width, rectangle6.Height, 31, 0f, 0f, 100, color6, 2.5f);
							Main.dust[num19].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 1030f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] == 1100f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector7;
						vector7..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num20 = (float)Math.Atan2((double)(vector7.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector7.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num20) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num20) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color7 = default(Color);
						Rectangle rectangle7;
						rectangle7..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num21 = 10;
						for (int num22 = 1; num22 <= num21; num22++)
						{
							int num23 = Dust.NewDust(base.npc.position, rectangle7.Width, rectangle7.Height, 31, 0f, 0f, 100, color7, 2.5f);
							Main.dust[num23].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 1130f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] >= 1260f)
					{
						this.customAI[1] = 0f;
					}
				}
				if (this.shieldEvent3 && !this.shieldEvent4)
				{
					this.customAI[1] += 1f;
					if (this.customAI[1] == 120f)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
					}
					if (this.customAI[1] >= 300f && this.customAI[1] < 700f)
					{
						this.pewPew1Timer++;
						if (this.pewPew1Timer == 20 || this.pewPew1Timer == 40 || this.pewPew1Timer == 60)
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
						}
						if (this.pewPew1Timer >= 90)
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
							this.pewPew1Timer = 0;
						}
					}
					if (this.customAI[1] >= 700f && this.customAI[1] < 740f)
					{
						this.pewPew1Timer++;
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
					}
					if (this.customAI[1] == 760f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] == 800f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] == 900f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector8;
						vector8..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num24 = (float)Math.Atan2((double)(vector8.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector8.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num24) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num24) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color8 = default(Color);
						Rectangle rectangle8;
						rectangle8..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num25 = 10;
						for (int num26 = 1; num26 <= num25; num26++)
						{
							int num27 = Dust.NewDust(base.npc.position, rectangle8.Width, rectangle8.Height, 31, 0f, 0f, 100, color8, 2.5f);
							Main.dust[num27].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 930f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] == 940f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] == 1000f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector9;
						vector9..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num28 = (float)Math.Atan2((double)(vector9.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector9.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num28) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num28) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color9 = default(Color);
						Rectangle rectangle9;
						rectangle9..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num29 = 10;
						for (int num30 = 1; num30 <= num29; num30++)
						{
							int num31 = Dust.NewDust(base.npc.position, rectangle9.Width, rectangle9.Height, 31, 0f, 0f, 100, color9, 2.5f);
							Main.dust[num31].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 1030f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] == 1040f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] == 1100f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector10;
						vector10..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num32 = (float)Math.Atan2((double)(vector10.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector10.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num32) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num32) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color10 = default(Color);
						Rectangle rectangle10;
						rectangle10..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num33 = 10;
						for (int num34 = 1; num34 <= num33; num34++)
						{
							int num35 = Dust.NewDust(base.npc.position, rectangle10.Width, rectangle10.Height, 31, 0f, 0f, 100, color10, 2.5f);
							Main.dust[num35].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 1130f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
					}
					if (this.customAI[1] >= 1260f)
					{
						this.customAI[1] = 0f;
					}
				}
				if (this.shieldEvent4)
				{
					this.customAI[1] += 1f;
					if (this.customAI[1] == 10f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector11;
						vector11..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num36 = (float)Math.Atan2((double)(vector11.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector11.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num36) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num36) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color11 = default(Color);
						Rectangle rectangle11;
						rectangle11..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num37 = 10;
						for (int num38 = 1; num38 <= num37; num38++)
						{
							int num39 = Dust.NewDust(base.npc.position, rectangle11.Width, rectangle11.Height, 235, 0f, 0f, 100, color11, 1.5f);
							Main.dust[num39].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 40f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
					}
					if (this.customAI[1] == 50f)
					{
						this.teleport = true;
					}
					if (this.customAI[1] == 60f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector12;
						vector12..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num40 = (float)Math.Atan2((double)(vector12.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector12.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num40) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num40) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color12 = default(Color);
						Rectangle rectangle12;
						rectangle12..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num41 = 10;
						for (int num42 = 1; num42 <= num41; num42++)
						{
							int num43 = Dust.NewDust(base.npc.position, rectangle12.Width, rectangle12.Height, 235, 0f, 0f, 100, color12, 1.5f);
							Main.dust[num43].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 90f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
					}
					if (this.customAI[1] == 100f)
					{
						this.teleport = true;
					}
					if (this.customAI[1] == 110f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector13;
						vector13..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num44 = (float)Math.Atan2((double)(vector13.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector13.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num44) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num44) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color13 = default(Color);
						Rectangle rectangle13;
						rectangle13..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num45 = 10;
						for (int num46 = 1; num46 <= num45; num46++)
						{
							int num47 = Dust.NewDust(base.npc.position, rectangle13.Width, rectangle13.Height, 235, 0f, 0f, 100, color13, 1.5f);
							Main.dust[num47].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 140f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
					}
					if (this.customAI[1] == 150f)
					{
						this.teleport = true;
					}
					if (this.customAI[1] == 160f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector14;
						vector14..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num48 = (float)Math.Atan2((double)(vector14.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector14.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num48) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num48) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color14 = default(Color);
						Rectangle rectangle14;
						rectangle14..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num49 = 10;
						for (int num50 = 1; num50 <= num49; num50++)
						{
							int num51 = Dust.NewDust(base.npc.position, rectangle14.Width, rectangle14.Height, 235, 0f, 0f, 100, color14, 1.5f);
							Main.dust[num51].noGravity = false;
						}
						return;
					}
					if (this.customAI[1] == 190f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
					}
					if (this.customAI[1] >= 300f && this.customAI[1] < 700f)
					{
						this.pewPew1Timer++;
						if (this.pewPew1Timer == 20 || this.pewPew1Timer == 40 || this.pewPew1Timer == 60 || this.pewPew1Timer == 80)
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
						}
						if (this.pewPew1Timer >= 100)
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
							this.pewPew1Timer = 0;
						}
					}
					if (this.customAI[1] >= 700f && this.customAI[1] < 760f)
					{
						this.pewPew1Timer++;
						if (this.pewPew1Timer >= 6)
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
					}
					if (this.customAI[1] == 760f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] == 798f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] == 836f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] == 874f)
					{
						this.rocketEvent = true;
					}
					if (this.customAI[1] >= 920f)
					{
						this.customAI[1] = 0f;
					}
				}
			}
			if (base.npc.life <= 38000 && !this.shieldEvent1)
			{
				this.attackMode = 2;
				this.customAI[2] += 1f;
				this.chargeAttack = false;
				if (this.customAI[2] == 5f)
				{
					string text4 = "DEPLOYING MINIONS...";
					Color rarityCyan = Colors.RarityCyan;
					byte r4 = rarityCyan.R;
					rarityCyan = Colors.RarityCyan;
					byte g4 = rarityCyan.G;
					rarityCyan = Colors.RarityCyan;
					Main.NewText(text4, r4, g4, rarityCyan.B, false);
				}
				if (this.customAI[2] < 500f)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
					if (this.customAI[2] == 260f)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					}
					if (this.customAI[2] == 320f)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					}
				}
				if (this.customAI[2] >= 500f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					this.customAI[2] = 0f;
					this.shieldEvent1 = true;
					this.customAI[1] = 0f;
					this.attackMode = 1;
					this.shieldUp = false;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 30000 && !this.shieldEvent2)
			{
				this.attackMode = 2;
				this.customAI[2] += 1f;
				this.chargeAttack = false;
				if (this.customAI[2] == 5f)
				{
					string text5 = "POWERING UP...";
					Color rarityCyan = Colors.RarityCyan;
					byte r5 = rarityCyan.R;
					rarityCyan = Colors.RarityCyan;
					byte g5 = rarityCyan.G;
					rarityCyan = Colors.RarityCyan;
					Main.NewText(text5, r5, g5, rarityCyan.B, false);
				}
				if (this.customAI[2] < 570f)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
				}
				if (this.customAI[2] == 180f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 240f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 300f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 360f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] >= 570f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					this.customAI[2] = 0f;
					this.shieldEvent2 = true;
					this.customAI[1] = 0f;
					this.attackMode = 1;
					this.shieldUp = false;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 24000 && !this.shieldEvent3)
			{
				this.attackMode = 2;
				this.customAI[2] += 1f;
				this.chargeAttack = false;
				if (this.customAI[2] < 570f)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
				}
				if (this.customAI[2] == 30f)
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
				if (this.customAI[2] == 150f)
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
				if (this.customAI[2] == 60f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 120f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 180f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 240f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] >= 480f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					this.customAI[2] = 0f;
					this.shieldEvent3 = true;
					this.customAI[1] = 0f;
					this.attackMode = 1;
					this.shieldUp = false;
					this.idle2 = true;
				}
			}
			if (base.npc.life <= 10000 && !this.shieldEvent4)
			{
				this.attackMode = 2;
				this.customAI[2] += 1f;
				this.chargeAttack = false;
				if (this.customAI[2] < 580f)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
				}
				if (this.customAI[2] == 180f)
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
				if (this.customAI[2] == 220f)
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
				if (this.customAI[2] == 260f)
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
				if (this.customAI[2] == 300f)
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
				if (this.customAI[2] == 340f)
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
				if (this.customAI[2] == 180f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 240f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 300f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] == 360f)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
				}
				if (this.customAI[2] >= 580f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					this.customAI[3] += 1f;
					if (this.customAI[3] == 50f)
					{
						string text6 = "WARNING: SYSTEM MALFUNCTIONING...";
						Color rarityCyan = Colors.RarityCyan;
						byte r6 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g6 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text6, r6, g6, rarityCyan.B, false);
					}
					if (this.customAI[3] == 200f)
					{
						string text7 = "ACTIVATING OFFENSIVE MODE...";
						Color rarityCyan = Colors.RarityCyan;
						byte r7 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g7 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text7, r7, g7, rarityCyan.B, false);
					}
					if (this.customAI[3] >= 340f)
					{
						for (int num52 = 0; num52 < 40; num52++)
						{
							int num53 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
							Main.dust[num53].velocity *= 2.9f;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						this.customAI[2] = 0f;
						this.customAI[3] = 0f;
						this.shieldEvent4 = true;
						this.customAI[1] = 0f;
						this.attackMode = 1;
						this.shieldUp = false;
						this.idle2R = true;
						this.idle2 = false;
					}
				}
			}
			if (this.rocketEvent)
			{
				if (this.shieldEvent4)
				{
					if (this.fistTimer < 36)
					{
						this.idle2R = false;
						this.fistRocketR = true;
					}
					this.fistTimer++;
					if (this.fistTimer == 18)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFistR"), 50, 3f, 255, 0f, 0f);
						}
						else
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFistR"), 50, 3f, 255, 0f, 0f);
						}
					}
					if (this.fistTimer >= 36)
					{
						this.fistRocketR = false;
						this.fistTimer = 0;
						this.idle2R = true;
						this.rocketEvent = false;
					}
				}
				else
				{
					if (this.fistTimer < 36)
					{
						this.idle2 = false;
						this.fistRocket = true;
					}
					this.fistTimer++;
					if (this.fistTimer == 18)
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
					if (this.fistTimer >= 36)
					{
						this.fistRocket = false;
						this.fistTimer = 0;
						this.idle2 = true;
						this.rocketEvent = false;
					}
				}
			}
			if (this.fightBegin)
			{
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
				if (this.shieldEvent4)
				{
					base.npc.netUpdate = true;
					this.teleportTimer++;
					if (this.teleportTimer == 2)
					{
						for (int num54 = 0; num54 < 20; num54++)
						{
							int num55 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num55].velocity *= 1.4f;
						}
					}
					if (this.teleportTimer == 4 && Main.netMode != 1)
					{
						int num56 = Main.rand.Next(2);
						if (num56 == 0)
						{
							Vector2 vector15;
							vector15..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + vector15;
							base.npc.netUpdate = true;
						}
						if (num56 == 1)
						{
							Vector2 vector16;
							vector16..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + vector16;
							base.npc.netUpdate = true;
						}
					}
					if (this.teleportTimer >= 6)
					{
						for (int num57 = 0; num57 < 20; num57++)
						{
							int num58 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num58].velocity *= 1.4f;
						}
						this.teleport = false;
						this.teleportTimer = 0;
						base.npc.netUpdate = true;
						return;
					}
				}
				else
				{
					base.npc.netUpdate = true;
					this.teleportTimer++;
					if (this.teleportTimer == 2)
					{
						for (int num59 = 0; num59 < 20; num59++)
						{
							int num60 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num60].velocity *= 1.4f;
						}
					}
					if (this.teleportTimer == 4 && Main.netMode != 1)
					{
						int num61 = Main.rand.Next(2);
						if (num61 == 0)
						{
							Vector2 vector17;
							vector17..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + vector17;
							base.npc.netUpdate = true;
						}
						if (num61 == 1)
						{
							Vector2 vector18;
							vector18..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + vector18;
							base.npc.netUpdate = true;
						}
					}
					if (this.teleportTimer >= 6)
					{
						for (int num62 = 0; num62 < 20; num62++)
						{
							int num63 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num63].velocity *= 1.4f;
						}
						this.teleport = false;
						this.teleportTimer = 0;
						base.npc.netUpdate = true;
					}
				}
			}
		}

		private void Move(Vector2 offset)
		{
			if (this.attackMode >= 1)
			{
				this.speed = 12f;
			}
			Vector2 vector = this.player.Center + offset;
			Vector2 vector2 = vector - base.npc.Center;
			float num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			float num2 = 10f;
			vector2 = (base.npc.velocity * num2 + vector2) / (num2 + 1f);
			num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			base.npc.velocity = vector2;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSChargeClone");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSChargeRClone");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSIdle1Clone");
			Texture2D texture4 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSIdle2Clone");
			Texture2D texture5 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSIdle2RClone");
			Texture2D texture6 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSRocket1Clone");
			Texture2D texture7 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSRocket1RClone");
			Texture2D texture8 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSRocket2Clone");
			Texture2D texture9 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSShieldClone");
			base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSExitClone");
			Texture2D texture10 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIIIClone/KSGunBlinkClone");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			if (!this.start)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			}
			if (this.blinkGun)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture10.Height / 4;
				int num2 = num * this.gunblinkFrame;
				Main.spriteBatch.Draw(texture10, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture10.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture10.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.idle1)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture3.Height / 4;
				int num4 = num3 * this.idle1Frame;
				Main.spriteBatch.Draw(texture3, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture3.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture3.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.idle2)
			{
				Vector2 vector3;
				vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num5 = texture4.Height / 4;
				int num6 = num5 * this.idle2Frame;
				Main.spriteBatch.Draw(texture4, vector3 - Main.screenPosition, new Rectangle?(new Rectangle(0, num6, texture4.Width, num5)), drawColor, base.npc.rotation, new Vector2((float)texture4.Width / 2f, (float)num5 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.idle2R)
			{
				Vector2 vector4;
				vector4..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num7 = texture5.Height / 4;
				int num8 = num7 * this.idle2RFrame;
				Main.spriteBatch.Draw(texture5, vector4 - Main.screenPosition, new Rectangle?(new Rectangle(0, num8, texture5.Width, num7)), drawColor, base.npc.rotation, new Vector2((float)texture5.Width / 2f, (float)num7 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.shieldUp)
			{
				Vector2 vector5;
				vector5..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num9 = texture9.Height / 7;
				int num10 = num9 * this.shieldFrame;
				Main.spriteBatch.Draw(texture9, vector5 - Main.screenPosition, new Rectangle?(new Rectangle(0, num10, texture9.Width, num9)), drawColor, base.npc.rotation, new Vector2((float)texture9.Width / 2f, (float)num9 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.chargeAttack)
			{
				Vector2 vector6;
				vector6..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num11 = texture.Height / 2;
				int num12 = num11 * this.chargeFrame;
				Main.spriteBatch.Draw(texture, vector6 - Main.screenPosition, new Rectangle?(new Rectangle(0, num12, texture.Width, num11)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num11 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.chargeAttackR)
			{
				Vector2 vector7;
				vector7..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num13 = texture2.Height / 2;
				int num14 = num13 * this.chargeRFrame;
				Main.spriteBatch.Draw(texture2, vector7 - Main.screenPosition, new Rectangle?(new Rectangle(0, num14, texture2.Width, num13)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num13 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.fistRocket)
			{
				Vector2 vector8;
				vector8..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num15 = texture6.Height / 12;
				int num16 = num15 * this.rocketFrame;
				Main.spriteBatch.Draw(texture6, vector8 - Main.screenPosition, new Rectangle?(new Rectangle(0, num16, texture6.Width, num15)), drawColor, base.npc.rotation, new Vector2((float)texture6.Width / 2f, (float)num15 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.fistRocketR)
			{
				Vector2 vector9;
				vector9..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num17 = texture7.Height / 12;
				int num18 = num17 * this.rocketRFrame;
				Main.spriteBatch.Draw(texture7, vector9 - Main.screenPosition, new Rectangle?(new Rectangle(0, num18, texture7.Width, num17)), drawColor, base.npc.rotation, new Vector2((float)texture7.Width / 2f, (float)num17 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			if (this.fistRocketDone)
			{
				Vector2 vector10;
				vector10..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num19 = texture8.Height / 4;
				int num20 = num19 * this.rocketDoneFrame;
				Main.spriteBatch.Draw(texture8, vector10 - Main.screenPosition, new Rectangle?(new Rectangle(0, num20, texture8.Width, num19)), drawColor, base.npc.rotation, new Vector2((float)texture8.Width / 2f, (float)num19 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			return false;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.75;
			return true;
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
						string text = "TARGET DEFEATED...";
						Color rarityCyan = Colors.RarityCyan;
						byte r = rarityCyan.R;
						Color rarityCyan2 = Colors.RarityCyan;
						byte g = rarityCyan2.G;
						Color rarityCyan3 = Colors.RarityCyan;
						Main.NewText(text, r, g, rarityCyan3.B, false);
					}
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		private Player player;

		private float speed;

		public float[] customAI = new float[4];

		public bool chargeAttack;

		public bool idle1;

		public bool idle2;

		public bool fistRocket;

		public bool fistRocketDone;

		public bool shieldUp;

		public bool blinkGun;

		public int pewPew1Timer;

		public bool shieldEvent1;

		public bool shieldEvent2;

		public bool shieldEvent3;

		public bool shieldEvent4;

		public int fistTimer;

		public int teleportTimer;

		public bool teleport;

		public bool rocketEvent;

		private int idle1Counter;

		private int idle2Counter;

		private int blinkGunCounter;

		private int shieldCounter;

		private int chargeCounter;

		private int rocketCounter;

		private int rocketDoneCounter;

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

		private int deadTimer;

		private bool phase2;

		private bool idle2R;

		private int idle2RFrame;

		private int idle2RCounter;

		private bool chargeAttackR;

		private int chargeRFrame;

		private bool fistRocketR;

		private int rocketRFrame;

		private int chargeRCounter;

		private int rocketRCounter;
	}
}
