using System;
using System.IO;
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
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			if (!RedeWorld.downedSlayer)
			{
				RedeWorld.redemptionPoints--;
				CombatText.NewText(this.player.getRect(), Color.Red, "-1", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						for (int j = 0; j < player.inventory.Length; j++)
						{
							if (player.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Oh dear, he seems to have a very short temper, and you winning probably made it worse... I hope he doesn't do anything stupid...", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedSlayer = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
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
				writer.Write(this.customAI[4]);
				writer.Write(this.shieldEvent);
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
				this.customAI[4] = reader.ReadFloat();
				this.shieldEvent = reader.ReadInt32();
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
			if (base.npc.ai[2] == 0f)
			{
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
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] == 39f)
			{
				this.idle1 = true;
				this.start = true;
				base.npc.netUpdate = true;
			}
			if (RedeWorld.downedSlayer)
			{
				if (base.npc.ai[1] == 60f)
				{
					string text = "So you've come back for more?";
					Color rarityCyan = Colors.RarityCyan;
					byte r = rarityCyan.R;
					rarityCyan = Colors.RarityCyan;
					byte g = rarityCyan.G;
					rarityCyan = Colors.RarityCyan;
					Main.NewText(text, r, g, rarityCyan.B, false);
				}
				if (base.npc.ai[1] == 220f)
				{
					string text2 = "Interesting...";
					Color rarityCyan = Colors.RarityCyan;
					byte r2 = rarityCyan.R;
					rarityCyan = Colors.RarityCyan;
					byte g2 = rarityCyan.G;
					rarityCyan = Colors.RarityCyan;
					Main.NewText(text2, r2, g2, rarityCyan.B, false);
				}
				if (base.npc.ai[1] == 400f)
				{
					string text3 = "However, I still won't go easy on you...";
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
				if (base.npc.ai[1] == 418f)
				{
					this.blinkGun = false;
					this.idle2 = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 500f)
				{
					this.fightBegin = true;
					base.npc.ai[2] = 1f;
					base.npc.dontTakeDamage = false;
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[1] == 0f && !this.shieldUp)
			{
				base.npc.dontTakeDamage = false;
				base.npc.netUpdate = true;
			}
			if (!RedeWorld.downedSlayer)
			{
				if (RedeWorld.deathBySlayer)
				{
					if (base.npc.ai[1] == 60f)
					{
						string text4 = "Ready for a rematch?";
						Color rarityCyan = Colors.RarityCyan;
						byte r4 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g4 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text4, r4, g4, rarityCyan.B, false);
					}
					if (base.npc.ai[1] == 180f)
					{
						base.npc.netUpdate = true;
						this.idle1 = false;
						this.blinkGun = true;
					}
					if (base.npc.ai[1] == 198f)
					{
						this.blinkGun = false;
						this.idle2 = true;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[1] == 300f)
					{
						this.fightBegin = true;
						base.npc.ai[2] = 1f;
						base.npc.dontTakeDamage = false;
						base.npc.ai[1] = 0f;
						base.npc.netUpdate = true;
					}
				}
				if (!RedeWorld.deathBySlayer)
				{
					if (base.npc.ai[1] == 60f)
					{
						string text5 = "Well what do we have here?";
						Color rarityCyan = Colors.RarityCyan;
						byte r5 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g5 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text5, r5, g5, rarityCyan.B, false);
					}
					if (base.npc.ai[1] == 220f)
					{
						string text6 = "I was ordered to kill a ravaging Undead known as the Keeper...";
						Color rarityCyan = Colors.RarityCyan;
						byte r6 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g6 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text6, r6, g6, rarityCyan.B, false);
					}
					if (base.npc.ai[1] == 400f)
					{
						if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).chickenPower)
						{
							string text7 = "However, y- ... Wait...";
							Color rarityCyan = Colors.RarityCyan;
							byte r7 = rarityCyan.R;
							rarityCyan = Colors.RarityCyan;
							byte g7 = rarityCyan.G;
							rarityCyan = Colors.RarityCyan;
							Main.NewText(text7, r7, g7, rarityCyan.B, false);
						}
						else
						{
							string text8 = "However, you killed it before I arrived...";
							Color rarityCyan = Colors.RarityCyan;
							byte r8 = rarityCyan.R;
							rarityCyan = Colors.RarityCyan;
							byte g8 = rarityCyan.G;
							rarityCyan = Colors.RarityCyan;
							Main.NewText(text8, r8, g8, rarityCyan.B, false);
						}
					}
					if (base.npc.ai[1] == 680f)
					{
						if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).chickenPower)
						{
							string text9 = "A chicken stole my kill... ? Well, it doesn't matter! You're dead!";
							Color rarityCyan = Colors.RarityCyan;
							byte r9 = rarityCyan.R;
							rarityCyan = Colors.RarityCyan;
							byte g9 = rarityCyan.G;
							rarityCyan = Colors.RarityCyan;
							Main.NewText(text9, r9, g9, rarityCyan.B, false);
						}
						else
						{
							string text10 = "You stole my kill... You know what? I don't care if I'm a member of the Heroes! You're dead!";
							Color rarityCyan = Colors.RarityCyan;
							byte r10 = rarityCyan.R;
							rarityCyan = Colors.RarityCyan;
							byte g10 = rarityCyan.G;
							rarityCyan = Colors.RarityCyan;
							Main.NewText(text10, r10, g10, rarityCyan.B, false);
						}
						this.idle1 = false;
						this.blinkGun = true;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[1] == 698f)
					{
						this.blinkGun = false;
						this.idle2 = true;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[1] == 900f)
					{
						base.npc.netUpdate = true;
						this.fightBegin = true;
						RedeWorld.deathBySlayer = true;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
						base.npc.ai[2] = 1f;
						base.npc.dontTakeDamage = false;
						base.npc.ai[1] = 0f;
					}
				}
			}
			if (base.npc.ai[2] == 0f)
			{
				base.npc.ai[1] += 1f;
			}
			if (this.fightBegin)
			{
				base.npc.ai[3] += 1f;
				base.npc.noTileCollide = true;
				base.npc.noGravity = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[2] == 1f)
			{
				if (this.shieldEvent == 0)
				{
					this.customAI[4] += 1f;
					if (this.customAI[4] == 120f)
					{
						int num2 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
						Main.projectile[num2].netUpdate = true;
					}
					if (this.customAI[4] >= 300f && this.customAI[4] < 700f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] == 40f || this.customAI[0] == 80f || this.customAI[0] == 120f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num3].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num4].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 120f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num5].netUpdate = true;
								Main.projectile[num6].netUpdate = true;
								Main.projectile[num7].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num8 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num9 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num10 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num8].netUpdate = true;
								Main.projectile[num9].netUpdate = true;
								Main.projectile[num10].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] == 800f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector;
						vector..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num11 = (float)Math.Atan2((double)(vector.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num11) * 40.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num11) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color = default(Color);
						Rectangle rectangle;
						rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num12 = 10;
						for (int i = 1; i <= num12; i++)
						{
							int num13 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 31, 0f, 0f, 100, color, 2.5f);
							Main.dust[num13].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 830f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 900f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector2;
						vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num14 = (float)Math.Atan2((double)(vector2.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector2.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num14) * 40.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num14) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color2 = default(Color);
						Rectangle rectangle2;
						rectangle2..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num15 = 10;
						for (int j = 1; j <= num15; j++)
						{
							int num16 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 31, 0f, 0f, 100, color2, 2.5f);
							Main.dust[num16].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 930f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] >= 1000f)
					{
						this.customAI[4] = 0f;
						base.npc.netUpdate = true;
					}
				}
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD1)
				{
					this.customAI[4] += 1f;
					if (this.customAI[4] >= 120f && this.customAI[4] < 520f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] == 30f || this.customAI[0] == 60f || this.customAI[0] == 90f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num17 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num17].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num18 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num18].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 90f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num19 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num20 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num21 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num19].netUpdate = true;
								Main.projectile[num20].netUpdate = true;
								Main.projectile[num21].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num22 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num23 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num24 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num22].netUpdate = true;
								Main.projectile[num23].netUpdate = true;
								Main.projectile[num24].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] >= 520f && this.customAI[4] < 620f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] >= 15f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num25 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num25].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num26 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num26].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] == 760f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector3;
						vector3..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num27 = (float)Math.Atan2((double)(vector3.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector3.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num27) * 50.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num27) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color3 = default(Color);
						Rectangle rectangle3;
						rectangle3..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num28 = 10;
						for (int k = 1; k <= num28; k++)
						{
							int num29 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 31, 0f, 0f, 100, color3, 2.5f);
							Main.dust[num29].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 730f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 800f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector4;
						vector4..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num30 = (float)Math.Atan2((double)(vector4.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector4.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num30) * 50.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num30) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color4 = default(Color);
						Rectangle rectangle4;
						rectangle4..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num31 = 10;
						for (int l = 1; l <= num31; l++)
						{
							int num32 = Dust.NewDust(base.npc.position, rectangle4.Width, rectangle4.Height, 31, 0f, 0f, 100, color4, 2.5f);
							Main.dust[num32].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 830f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] >= 880f)
					{
						this.customAI[4] = 0f;
						base.npc.netUpdate = true;
					}
				}
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD2)
				{
					this.customAI[4] += 1f;
					if (this.customAI[4] >= 120f && this.customAI[4] < 520f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] == 20f || this.customAI[0] == 40f || this.customAI[0] == 60f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num33 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num33].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num34 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num34].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 90f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num35 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num36 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int num37 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num38 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int num39 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num35].netUpdate = true;
								Main.projectile[num36].netUpdate = true;
								Main.projectile[num37].netUpdate = true;
								Main.projectile[num38].netUpdate = true;
								Main.projectile[num39].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num40 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num41 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int num42 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num43 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int num44 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num40].netUpdate = true;
								Main.projectile[num41].netUpdate = true;
								Main.projectile[num42].netUpdate = true;
								Main.projectile[num43].netUpdate = true;
								Main.projectile[num44].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] >= 520f && this.customAI[4] < 580f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] >= 10f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num45 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num45].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num46 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num46].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] == 700f)
					{
						int num47 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
						Main.projectile[num47].netUpdate = true;
					}
					if (this.customAI[4] == 820f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 900f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector5;
						vector5..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num48 = (float)Math.Atan2((double)(vector5.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector5.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num48) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num48) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color5 = default(Color);
						Rectangle rectangle5;
						rectangle5..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num49 = 10;
						for (int m = 1; m <= num49; m++)
						{
							int num50 = Dust.NewDust(base.npc.position, rectangle5.Width, rectangle5.Height, 31, 0f, 0f, 100, color5, 2.5f);
							Main.dust[num50].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 930f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 1000f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector6;
						vector6..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num51 = (float)Math.Atan2((double)(vector6.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector6.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num51) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num51) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color6 = default(Color);
						Rectangle rectangle6;
						rectangle6..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num52 = 10;
						for (int n = 1; n <= num52; n++)
						{
							int num53 = Dust.NewDust(base.npc.position, rectangle6.Width, rectangle6.Height, 31, 0f, 0f, 100, color6, 2.5f);
							Main.dust[num53].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 1030f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 1100f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector7;
						vector7..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num54 = (float)Math.Atan2((double)(vector7.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector7.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num54) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num54) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color7 = default(Color);
						Rectangle rectangle7;
						rectangle7..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num55 = 10;
						for (int num56 = 1; num56 <= num55; num56++)
						{
							int num57 = Dust.NewDust(base.npc.position, rectangle7.Width, rectangle7.Height, 31, 0f, 0f, 100, color7, 2.5f);
							Main.dust[num57].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 1130f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] >= 1260f)
					{
						this.customAI[4] = 0f;
						base.npc.netUpdate = true;
					}
				}
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD3)
				{
					this.customAI[4] += 1f;
					if (this.customAI[4] == 120f)
					{
						int num58 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
						Main.projectile[num58].netUpdate = true;
					}
					if (this.customAI[4] >= 300f && this.customAI[4] < 700f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] == 20f || this.customAI[0] == 40f || this.customAI[0] == 60f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num59 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num59].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num60 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num60].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 90f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num61 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num62 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int num63 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num64 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int num65 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num61].netUpdate = true;
								Main.projectile[num62].netUpdate = true;
								Main.projectile[num63].netUpdate = true;
								Main.projectile[num64].netUpdate = true;
								Main.projectile[num65].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num66 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num67 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int num68 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num69 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int num70 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num66].netUpdate = true;
								Main.projectile[num67].netUpdate = true;
								Main.projectile[num68].netUpdate = true;
								Main.projectile[num69].netUpdate = true;
								Main.projectile[num70].netUpdate = true;
							}
							this.customAI[0] = 0f;
							base.npc.netUpdate = true;
						}
					}
					if (this.customAI[4] >= 700f && this.customAI[4] < 740f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] >= 8f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num71 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num71].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num72 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num72].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] == 760f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 800f)
					{
						base.npc.netUpdate = true;
						this.rocketEvent = true;
					}
					if (this.customAI[4] == 900f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector8;
						vector8..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num73 = (float)Math.Atan2((double)(vector8.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector8.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num73) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num73) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color8 = default(Color);
						Rectangle rectangle8;
						rectangle8..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num74 = 10;
						for (int num75 = 1; num75 <= num74; num75++)
						{
							int num76 = Dust.NewDust(base.npc.position, rectangle8.Width, rectangle8.Height, 31, 0f, 0f, 100, color8, 2.5f);
							Main.dust[num76].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 930f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 940f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 1000f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector9;
						vector9..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num77 = (float)Math.Atan2((double)(vector9.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector9.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num77) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num77) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color9 = default(Color);
						Rectangle rectangle9;
						rectangle9..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num78 = 10;
						for (int num79 = 1; num79 <= num78; num79++)
						{
							int num80 = Dust.NewDust(base.npc.position, rectangle9.Width, rectangle9.Height, 31, 0f, 0f, 100, color9, 2.5f);
							Main.dust[num80].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 1030f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 1040f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 1100f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector10;
						vector10..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num81 = (float)Math.Atan2((double)(vector10.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector10.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num81) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num81) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color10 = default(Color);
						Rectangle rectangle10;
						rectangle10..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num82 = 10;
						for (int num83 = 1; num83 <= num82; num83++)
						{
							int num84 = Dust.NewDust(base.npc.position, rectangle10.Width, rectangle10.Height, 31, 0f, 0f, 100, color10, 2.5f);
							Main.dust[num84].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 1130f)
					{
						this.idle2 = true;
						this.chargeAttack = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] >= 1260f)
					{
						this.customAI[4] = 0f;
						base.npc.netUpdate = true;
					}
				}
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
				{
					this.customAI[4] += 1f;
					if (this.customAI[4] == 10f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector11;
						vector11..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num85 = (float)Math.Atan2((double)(vector11.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector11.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num85) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num85) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color11 = default(Color);
						Rectangle rectangle11;
						rectangle11..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num86 = 10;
						for (int num87 = 1; num87 <= num86; num87++)
						{
							int num88 = Dust.NewDust(base.npc.position, rectangle11.Width, rectangle11.Height, 235, 0f, 0f, 100, color11, 1.5f);
							Main.dust[num88].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 30f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 35f)
					{
						this.teleport = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 45f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector12;
						vector12..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num89 = (float)Math.Atan2((double)(vector12.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector12.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num89) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num89) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color12 = default(Color);
						Rectangle rectangle12;
						rectangle12..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num90 = 10;
						for (int num91 = 1; num91 <= num90; num91++)
						{
							int num92 = Dust.NewDust(base.npc.position, rectangle12.Width, rectangle12.Height, 235, 0f, 0f, 100, color12, 1.5f);
							Main.dust[num92].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 65f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 70f)
					{
						this.teleport = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 80f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector13;
						vector13..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num93 = (float)Math.Atan2((double)(vector13.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector13.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num93) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num93) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color13 = default(Color);
						Rectangle rectangle13;
						rectangle13..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num94 = 10;
						for (int num95 = 1; num95 <= num94; num95++)
						{
							int num96 = Dust.NewDust(base.npc.position, rectangle13.Width, rectangle13.Height, 235, 0f, 0f, 100, color13, 1.5f);
							Main.dust[num96].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 100f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 105f)
					{
						this.teleport = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 115f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector14;
						vector14..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float num97 = (float)Math.Atan2((double)(vector14.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector14.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)num97) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)num97) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color14 = default(Color);
						Rectangle rectangle14;
						rectangle14..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num98 = 10;
						for (int num99 = 1; num99 <= num98; num99++)
						{
							int num100 = Dust.NewDust(base.npc.position, rectangle14.Width, rectangle14.Height, 235, 0f, 0f, 100, color14, 1.5f);
							Main.dust[num100].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 135f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] >= 180f && this.customAI[4] < 480f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] == 20f || this.customAI[0] == 40f || this.customAI[0] == 60f || this.customAI[0] == 80f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num101 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num101].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num102 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num102].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 100f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num103 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num104 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int num105 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num106 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int num107 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num103].netUpdate = true;
								Main.projectile[num104].netUpdate = true;
								Main.projectile[num105].netUpdate = true;
								Main.projectile[num106].netUpdate = true;
								Main.projectile[num107].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num108 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int num109 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int num110 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int num111 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int num112 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[num108].netUpdate = true;
								Main.projectile[num109].netUpdate = true;
								Main.projectile[num110].netUpdate = true;
								Main.projectile[num111].netUpdate = true;
								Main.projectile[num112].netUpdate = true;
							}
							this.customAI[0] = 0f;
							base.npc.netUpdate = true;
						}
					}
					if (this.customAI[4] >= 480f && this.customAI[4] < 560f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] >= 6f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num113 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num113].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int num114 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[num114].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] == 590f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 628f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 666f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 704f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] >= 742f)
					{
						this.customAI[4] = 0f;
						base.npc.netUpdate = true;
					}
				}
			}
			if (base.npc.life <= 38000 && this.shieldEvent == 0)
			{
				base.npc.ai[2] = 2f;
				this.customAI[1] += 1f;
				this.chargeAttack = false;
				if (this.customAI[1] == 5f)
				{
					if (base.npc.ai[3] <= 600f && base.npc.ai[3] > 300f)
					{
						this.shield10Seconds = true;
						string text11 = "Hmm? You are taking me down quite quickly... Well whatever. Shield Boost!";
						Color rarityCyan = Colors.RarityCyan;
						byte r11 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g11 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text11, r11, g11, rarityCyan.B, false);
					}
					else if (base.npc.ai[3] <= 300f && base.npc.ai[3] > 120f)
					{
						this.shield5Seconds = true;
						string text12 = "In less than 5 seconds, I am already forced to use a shield!? Alright! Now I'll show you what I'm truely capable of!";
						Color rarityCyan = Colors.RarityCyan;
						byte r12 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g12 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text12, r12, g12, rarityCyan.B, false);
					}
					else if (base.npc.ai[3] <= 120f)
					{
						this.shield2Seconds = true;
						string text13 = "In mere SECONDS, I am already forced to use a shield!? Alright! MAX SHIELD BOOST!";
						Color rarityCyan = Colors.RarityCyan;
						byte r13 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g13 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text13, r13, g13, rarityCyan.B, false);
					}
					else
					{
						string text14 = "What a nuisance. Minions, take them down.";
						Color rarityCyan = Colors.RarityCyan;
						byte r14 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g14 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text14, r14, g14, rarityCyan.B, false);
					}
					base.npc.netUpdate = true;
				}
				if (this.customAI[1] < 500f)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
					if (this.customAI[1] == 260f)
					{
						int num115 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
						int num116 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
						Main.projectile[num115].netUpdate = true;
						Main.projectile[num116].netUpdate = true;
					}
					if (this.customAI[1] == 320f)
					{
						int num117 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
						int num118 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
						Main.projectile[num117].netUpdate = true;
						Main.projectile[num118].netUpdate = true;
					}
				}
				if (this.customAI[1] >= 500f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					string text15 = "I never needed them anyway.";
					Color rarityCyan = Colors.RarityCyan;
					byte r15 = rarityCyan.R;
					rarityCyan = Colors.RarityCyan;
					byte g15 = rarityCyan.G;
					rarityCyan = Colors.RarityCyan;
					Main.NewText(text15, r15, g15, rarityCyan.B, false);
					this.customAI[1] = 0f;
					this.shieldEvent = KSEntrance.AISTATE_SHIELD1;
					this.customAI[4] = 0f;
					base.npc.ai[2] = 1f;
					this.shieldUp = false;
					this.idle2 = true;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= 30000 && this.shieldEvent == KSEntrance.AISTATE_SHIELD1)
			{
				base.npc.ai[2] = 2f;
				this.customAI[1] += 1f;
				this.chargeAttack = false;
				if (this.customAI[1] == 5f)
				{
					string text16 = "Alright, you are really starting to piss me off!";
					Color rarityCyan = Colors.RarityCyan;
					byte r16 = rarityCyan.R;
					rarityCyan = Colors.RarityCyan;
					byte g16 = rarityCyan.G;
					rarityCyan = Colors.RarityCyan;
					Main.NewText(text16, r16, g16, rarityCyan.B, false);
					base.npc.netUpdate = true;
				}
				if (this.customAI[1] < 570f)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
				}
				if (this.customAI[1] == 180f)
				{
					int num119 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num119].netUpdate = true;
				}
				if (this.customAI[1] == 240f)
				{
					int num120 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num120].netUpdate = true;
				}
				if (this.customAI[1] == 300f)
				{
					int num121 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num121].netUpdate = true;
				}
				if (this.customAI[1] == 360f)
				{
					int num122 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num122].netUpdate = true;
				}
				if (this.customAI[1] >= 570f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					this.customAI[1] = 0f;
					this.shieldEvent = KSEntrance.AISTATE_SHIELD2;
					this.customAI[4] = 0f;
					base.npc.ai[2] = 1f;
					this.shieldUp = false;
					this.idle2 = true;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= 24000 && this.shieldEvent == KSEntrance.AISTATE_SHIELD2)
			{
				base.npc.ai[2] = 2f;
				this.customAI[1] += 1f;
				this.chargeAttack = false;
				if (this.customAI[1] < 570f)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
				}
				if (this.customAI[1] == 30f)
				{
					int num123 = 8;
					for (int num124 = 0; num124 < num123; num124++)
					{
						int num125 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[num125].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num124 / (float)num123 * 6.28f);
						Main.projectile[num125].netUpdate = true;
					}
					base.npc.netUpdate = true;
				}
				if (this.customAI[1] == 150f)
				{
					int num126 = 8;
					for (int num127 = 0; num127 < num126; num127++)
					{
						int num128 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[num128].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num127 / (float)num126 * 6.28f);
						Main.projectile[num128].netUpdate = true;
					}
				}
				if (this.customAI[1] == 60f)
				{
					int num129 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num129].netUpdate = true;
				}
				if (this.customAI[1] == 120f)
				{
					int num130 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num130].netUpdate = true;
				}
				if (this.customAI[1] == 180f)
				{
					int num131 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num131].netUpdate = true;
				}
				if (this.customAI[1] == 240f)
				{
					int num132 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num132].netUpdate = true;
				}
				if (this.customAI[1] >= 480f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					this.customAI[1] = 0f;
					this.shieldEvent = KSEntrance.AISTATE_SHIELD3;
					this.customAI[4] = 0f;
					base.npc.ai[2] = 1f;
					this.shieldUp = false;
					this.idle2 = true;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= 10000 && this.shieldEvent == KSEntrance.AISTATE_SHIELD3)
			{
				base.npc.ai[2] = 2f;
				this.customAI[1] += 1f;
				this.chargeAttack = false;
				if (this.customAI[1] < 580f)
				{
					this.shieldUp = true;
					this.idle2 = false;
					base.npc.dontTakeDamage = true;
				}
				if (this.customAI[1] == 5f)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
					{
						string text17 = "How am I losing to a WEAK LITTLE ANDROID!?";
						Color rarityCyan = Colors.RarityCyan;
						byte r17 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g17 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text17, r17, g17, rarityCyan.B, false);
					}
					else if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).chickenPower)
					{
						string text18 = "The frick!? HOW AM I LOSING TO A CHICKEN!?";
						Color rarityCyan = Colors.RarityCyan;
						byte r18 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g18 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text18, r18, g18, rarityCyan.B, false);
					}
					else
					{
						string text19 = "How am I losing to a WEAK TERRARIAN!?";
						Color rarityCyan = Colors.RarityCyan;
						byte r19 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g19 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text19, r19, g19, rarityCyan.B, false);
					}
					base.npc.netUpdate = true;
				}
				if (this.customAI[1] == 180f)
				{
					int num133 = 8;
					for (int num134 = 0; num134 < num133; num134++)
					{
						int num135 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[num135].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num134 / (float)num133 * 6.28f);
						Main.projectile[num135].netUpdate = true;
					}
				}
				if (this.customAI[1] == 220f)
				{
					int num136 = 8;
					for (int num137 = 0; num137 < num136; num137++)
					{
						int num138 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[num138].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num137 / (float)num136 * 6.28f);
						Main.projectile[num138].netUpdate = true;
					}
				}
				if (this.customAI[1] == 260f)
				{
					int num139 = 8;
					for (int num140 = 0; num140 < num139; num140++)
					{
						int num141 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[num141].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num140 / (float)num139 * 6.28f);
						Main.projectile[num141].netUpdate = true;
					}
				}
				if (this.customAI[1] == 300f)
				{
					int num142 = 8;
					for (int num143 = 0; num143 < num142; num143++)
					{
						int num144 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[num144].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num143 / (float)num142 * 6.28f);
						Main.projectile[num144].netUpdate = true;
					}
				}
				if (this.customAI[1] == 340f)
				{
					int num145 = 8;
					for (int num146 = 0; num146 < num145; num146++)
					{
						int num147 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[num147].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num146 / (float)num145 * 6.28f);
						Main.projectile[num147].netUpdate = true;
					}
				}
				if (this.customAI[1] == 180f)
				{
					int num148 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num148].netUpdate = true;
				}
				if (this.customAI[1] == 240f)
				{
					int num149 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num149].netUpdate = true;
				}
				if (this.customAI[1] == 300f)
				{
					int num150 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num150].netUpdate = true;
				}
				if (this.customAI[1] == 360f)
				{
					int num151 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[num151].netUpdate = true;
				}
				if (this.customAI[1] >= 580f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					this.musicChange = true;
					this.customAI[2] += 1f;
					if (this.customAI[2] == 5f)
					{
						string text20 = "No...";
						Color rarityCyan = Colors.RarityCyan;
						byte r20 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g20 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text20, r20, g20, rarityCyan.B, false);
						base.npc.netUpdate = true;
					}
					if (this.customAI[2] == 50f)
					{
						string text21 = "NO! HOW IS THIS HAPPENING!?";
						Color rarityCyan = Colors.RarityCyan;
						byte r21 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g21 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text21, r21, g21, rarityCyan.B, false);
					}
					if (this.customAI[2] == 150f)
					{
						if (NPC.downedPlantBoss)
						{
							string text22 = "HOW ARE YOU WINNING!? THIS IS UNACCEPTABLE!";
							Color rarityCyan = Colors.RarityCyan;
							byte r22 = rarityCyan.R;
							rarityCyan = Colors.RarityCyan;
							byte g22 = rarityCyan.G;
							rarityCyan = Colors.RarityCyan;
							Main.NewText(text22, r22, g22, rarityCyan.B, false);
						}
						else
						{
							string text23 = "YOU HAVEN'T EVEN DEFEATED THAT DAMN PLANT YET, HOW ARE YOU DEFEATING ME!?";
							Color rarityCyan = Colors.RarityCyan;
							byte r23 = rarityCyan.R;
							rarityCyan = Colors.RarityCyan;
							byte g23 = rarityCyan.G;
							rarityCyan = Colors.RarityCyan;
							Main.NewText(text23, r23, g23, rarityCyan.B, false);
						}
					}
					if (this.customAI[2] == 320f)
					{
						string text24 = "I'VE HAD ENOUGH OF YOU! LET'S FINISH THIS!";
						Color rarityCyan = Colors.RarityCyan;
						byte r24 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g24 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text24, r24, g24, rarityCyan.B, false);
					}
					if (this.customAI[2] >= 500f)
					{
						for (int num152 = 0; num152 < 40; num152++)
						{
							int num153 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
							Main.dust[num153].velocity *= 2.9f;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						this.customAI[1] = 0f;
						this.customAI[2] = 0f;
						this.shieldEvent = KSEntrance.AISTATE_SHIELD4;
						this.customAI[4] = 0f;
						base.npc.ai[2] = 1f;
						this.shieldUp = false;
						this.idle2R = true;
						this.idle2 = false;
						base.npc.netUpdate = true;
					}
				}
			}
			if (this.musicChange)
			{
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossSlayer2");
			}
			if (this.rocketEvent)
			{
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
				{
					if (this.customAI[3] < 36f)
					{
						this.idle2R = false;
						this.fistRocketR = true;
					}
					this.customAI[3] += 1f;
					if (this.customAI[3] == 18f)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							int num154 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFistR"), 35, 3f, 255, 0f, 0f);
							Main.projectile[num154].netUpdate = true;
						}
						else
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							int num155 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFistR"), 35, 3f, 255, 0f, 0f);
							Main.projectile[num155].netUpdate = true;
						}
						base.npc.netUpdate = true;
					}
					if (this.customAI[3] >= 36f)
					{
						this.fistRocketR = false;
						this.customAI[3] = 0f;
						this.idle2R = true;
						this.rocketEvent = false;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (this.customAI[3] < 36f)
					{
						this.idle2 = false;
						this.fistRocket = true;
					}
					this.customAI[3] += 1f;
					if (this.customAI[3] == 18f)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							int num156 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 35, 3f, 255, 0f, 0f);
							Main.projectile[num156].netUpdate = true;
						}
						else
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							int num157 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 35, 3f, 255, 0f, 0f);
							Main.projectile[num157].netUpdate = true;
						}
						base.npc.netUpdate = true;
					}
					if (this.customAI[3] >= 36f)
					{
						this.fistRocket = false;
						this.customAI[3] = 0f;
						this.idle2 = true;
						this.rocketEvent = false;
						base.npc.netUpdate = true;
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
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
				{
					base.npc.netUpdate = true;
					this.teleportTimer++;
					if (this.teleportTimer == 2)
					{
						for (int num158 = 0; num158 < 20; num158++)
						{
							int num159 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num159].velocity *= 1.4f;
						}
					}
					if (this.teleportTimer == 4 && Main.netMode != 1)
					{
						int num160 = Main.rand.Next(2);
						if (num160 == 0)
						{
							Vector2 vector15;
							vector15..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + vector15;
							base.npc.netUpdate = true;
						}
						if (num160 == 1)
						{
							Vector2 vector16;
							vector16..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + vector16;
							base.npc.netUpdate = true;
						}
					}
					if (this.teleportTimer >= 6)
					{
						for (int num161 = 0; num161 < 20; num161++)
						{
							int num162 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num162].velocity *= 1.4f;
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
						for (int num163 = 0; num163 < 20; num163++)
						{
							int num164 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num164].velocity *= 1.4f;
						}
					}
					if (this.teleportTimer == 4 && Main.netMode != 1)
					{
						int num165 = Main.rand.Next(2);
						if (num165 == 0)
						{
							Vector2 vector17;
							vector17..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + vector17;
							base.npc.netUpdate = true;
						}
						if (num165 == 1)
						{
							Vector2 vector18;
							vector18..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + vector18;
							base.npc.netUpdate = true;
						}
					}
					if (this.teleportTimer >= 6)
					{
						for (int num166 = 0; num166 < 20; num166++)
						{
							int num167 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num167].velocity *= 1.4f;
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
			if (base.npc.ai[2] >= 1f)
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
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSCharge");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSChargeR");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle1");
			Texture2D texture4 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle2");
			Texture2D texture5 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle2R");
			Texture2D texture6 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSRocket1");
			Texture2D texture7 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSRocket1R");
			Texture2D texture8 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSRocket2");
			Texture2D texture9 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSShield");
			base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSExit");
			Texture2D texture10 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSGunBlink");
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
			if (this.shield10Seconds)
			{
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
				{
					damage *= 0.3;
				}
				else
				{
					damage *= 0.5;
				}
			}
			else if (this.shield5Seconds)
			{
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
				{
					damage *= 0.2;
				}
				else
				{
					damage *= 0.25;
				}
			}
			else if (this.shield2Seconds)
			{
				damage *= 0.15;
			}
			else if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
			{
				damage *= 0.5;
			}
			else
			{
				damage *= 0.75;
			}
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
						if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
						{
							int num = Main.rand.Next(4);
							if (num == 0)
							{
								string text = "(Thank God)";
								Color rarityRed = Colors.RarityRed;
								byte r = rarityRed.R;
								Color rarityRed2 = Colors.RarityRed;
								byte g = rarityRed2.G;
								Color rarityRed3 = Colors.RarityRed;
								Main.NewText(text, r, g, rarityRed3.B, false);
							}
							if (num == 1)
							{
								string text2 = "HAH YES! Gotta be honest, you almost had me.";
								Color rarityRed4 = Colors.RarityRed;
								byte r2 = rarityRed4.R;
								Color rarityRed5 = Colors.RarityRed;
								byte g2 = rarityRed5.G;
								Color rarityRed6 = Colors.RarityRed;
								Main.NewText(text2, r2, g2, rarityRed6.B, false);
							}
							if (num == 2)
							{
								string text3 = "You were so close...";
								Color rarityRed7 = Colors.RarityRed;
								byte r3 = rarityRed7.R;
								Color rarityRed8 = Colors.RarityRed;
								byte g3 = rarityRed8.G;
								Color rarityRed9 = Colors.RarityRed;
								Main.NewText(text3, r3, g3, rarityRed9.B, false);
							}
							if (num == 3)
							{
								string text4 = "Congratulations... You actually made me worry for a second there!";
								Color rarityRed10 = Colors.RarityRed;
								byte r4 = rarityRed10.R;
								Color rarityRed11 = Colors.RarityRed;
								byte g4 = rarityRed11.G;
								Color rarityRed12 = Colors.RarityRed;
								Main.NewText(text4, r4, g4, rarityRed12.B, false);
							}
						}
						else
						{
							if (!NPC.downedPlantBoss)
							{
								int num2 = Main.rand.Next(5);
								if (num2 == 0)
								{
									string text5 = "Maybe try again when you're actually strong.";
									Color rarityCyan = Colors.RarityCyan;
									byte r5 = rarityCyan.R;
									Color rarityCyan2 = Colors.RarityCyan;
									byte g5 = rarityCyan2.G;
									Color rarityCyan3 = Colors.RarityCyan;
									Main.NewText(text5, r5, g5, rarityCyan3.B, false);
								}
								if (num2 == 1)
								{
									string text6 = "What a pushover...";
									Color rarityCyan4 = Colors.RarityCyan;
									byte r6 = rarityCyan4.R;
									Color rarityCyan5 = Colors.RarityCyan;
									byte g6 = rarityCyan5.G;
									Color rarityCyan6 = Colors.RarityCyan;
									Main.NewText(text6, r6, g6, rarityCyan6.B, false);
								}
								if (num2 == 2)
								{
									string text7 = "Y'know, you don't have to fight me right now... But it is fun beating you.";
									Color rarityCyan7 = Colors.RarityCyan;
									byte r7 = rarityCyan7.R;
									Color rarityCyan8 = Colors.RarityCyan;
									byte g7 = rarityCyan8.G;
									Color rarityCyan9 = Colors.RarityCyan;
									Main.NewText(text7, r7, g7, rarityCyan9.B, false);
								}
								if (num2 == 3)
								{
									string text8 = "You should've just gave up when you had the chance.";
									Color rarityCyan10 = Colors.RarityCyan;
									byte r8 = rarityCyan10.R;
									Color rarityCyan11 = Colors.RarityCyan;
									byte g8 = rarityCyan11.G;
									Color rarityCyan12 = Colors.RarityCyan;
									Main.NewText(text8, r8, g8, rarityCyan12.B, false);
								}
								if (num2 == 4)
								{
									string text9 = "Why did you try to fight me when you're still so weak? I want an actual challenge y'know!";
									Color rarityCyan13 = Colors.RarityCyan;
									byte r9 = rarityCyan13.R;
									Color rarityCyan14 = Colors.RarityCyan;
									byte g9 = rarityCyan14.G;
									Color rarityCyan15 = Colors.RarityCyan;
									Main.NewText(text9, r9, g9, rarityCyan15.B, false);
								}
							}
							if (NPC.downedPlantBoss)
							{
								int num3 = Main.rand.Next(4);
								if (num3 == 0)
								{
									string text10 = "Are you seriously that weak!?";
									Color rarityCyan16 = Colors.RarityCyan;
									byte r10 = rarityCyan16.R;
									Color rarityCyan17 = Colors.RarityCyan;
									byte g10 = rarityCyan17.G;
									Color rarityCyan18 = Colors.RarityCyan;
									Main.NewText(text10, r10, g10, rarityCyan18.B, false);
								}
								if (num3 == 1)
								{
									string text11 = "Come on, you can do better than that!";
									Color rarityCyan19 = Colors.RarityCyan;
									byte r11 = rarityCyan19.R;
									Color rarityCyan20 = Colors.RarityCyan;
									byte g11 = rarityCyan20.G;
									Color rarityCyan21 = Colors.RarityCyan;
									Main.NewText(text11, r11, g11, rarityCyan21.B, false);
								}
								if (num3 == 2)
								{
									string text12 = "Hah.";
									Color rarityCyan22 = Colors.RarityCyan;
									byte r12 = rarityCyan22.R;
									Color rarityCyan23 = Colors.RarityCyan;
									byte g12 = rarityCyan23.G;
									Color rarityCyan24 = Colors.RarityCyan;
									Main.NewText(text12, r12, g12, rarityCyan24.B, false);
								}
								if (num3 == 3)
								{
									string text13 = "A useless attempt...";
									Color rarityCyan25 = Colors.RarityCyan;
									byte r13 = rarityCyan25.R;
									Color rarityCyan26 = Colors.RarityCyan;
									byte g13 = rarityCyan26.G;
									Color rarityCyan27 = Colors.RarityCyan;
									Main.NewText(text13, r13, g13, rarityCyan27.B, false);
								}
							}
						}
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

		public float[] customAI = new float[5];

		public bool chargeAttack;

		public bool idle1;

		public bool idle2;

		public bool fistRocket;

		public bool fistRocketDone;

		public bool shieldUp;

		public bool blinkGun;

		public static int AISTATE_SHIELD1 = 1;

		public static int AISTATE_SHIELD2 = 2;

		public static int AISTATE_SHIELD3 = 3;

		public static int AISTATE_SHIELD4 = 4;

		public int shieldEvent;

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

		private bool musicChange;

		private bool shield10Seconds;

		private bool shield5Seconds;

		private bool shield2Seconds;
	}
}
