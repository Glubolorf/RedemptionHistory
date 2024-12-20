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
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
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
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Holokey"), 1, false, 0, false, false);
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
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
			if (!this.chargeAttack && !this.chargeAttackR)
			{
				if (this.shieldUp)
				{
					if (player.Center.X > base.npc.Center.X)
					{
						this.Move(new Vector2(-400f, -100f));
					}
					else
					{
						this.Move(new Vector2(400f, -100f));
					}
				}
				else if (player.Center.X > base.npc.Center.X)
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
			if (base.npc.Distance(Main.player[base.npc.target].Center) >= 1200f && !this.teleport)
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
			if (base.npc.ai[1] == 0f && !this.shieldUp)
			{
				base.npc.dontTakeDamage = false;
				base.npc.netUpdate = true;
			}
			if (RedeConfigClient.Instance.NoBossText)
			{
				if (base.npc.ai[1] == 60f)
				{
					this.idle1 = false;
					this.blinkGun = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 78f)
				{
					this.blinkGun = false;
					this.idle2 = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 120f)
				{
					this.fightBegin = true;
					base.npc.ai[2] = 1f;
					base.npc.dontTakeDamage = false;
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
			else
			{
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
							if (Main.LocalPlayer.GetModPlayer<RedePlayer>().chickenPower)
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
							if (Main.LocalPlayer.GetModPlayer<RedePlayer>().chickenPower)
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
						int p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
					}
					if (this.customAI[4] >= 300f && this.customAI[4] < 700f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] == 40f || this.customAI[0] == 80f || this.customAI[0] == 120f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p2].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p3].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 120f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p4].netUpdate = true;
								Main.projectile[p5].netUpdate = true;
								Main.projectile[p6].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p8 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p9 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p7].netUpdate = true;
								Main.projectile[p8].netUpdate = true;
								Main.projectile[p9].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] == 800f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector8 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation = (float)Math.Atan2((double)(vector8.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector8.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation) * 40.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color = default(Color);
						Rectangle rectangle = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count = 10;
						for (int j = 1; j <= count; j++)
						{
							int dust = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 31, 0f, 0f, 100, color, 2.5f);
							Main.dust[dust].noGravity = false;
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
						Vector2 vector9 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation2 = (float)Math.Atan2((double)(vector9.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector9.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation2) * 40.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation2) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color2 = default(Color);
						Rectangle rectangle2 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count2 = 10;
						for (int k = 1; k <= count2; k++)
						{
							int dust2 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 31, 0f, 0f, 100, color2, 2.5f);
							Main.dust[dust2].noGravity = false;
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
								int p10 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p10].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p11 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p11].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 90f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p12 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p13 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p14 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p12].netUpdate = true;
								Main.projectile[p13].netUpdate = true;
								Main.projectile[p14].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p15 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p16 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p17 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p15].netUpdate = true;
								Main.projectile[p16].netUpdate = true;
								Main.projectile[p17].netUpdate = true;
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
								int p18 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p18].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p19 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p19].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] == 760f)
					{
						this.idle2 = false;
						this.chargeAttack = true;
						Vector2 vector10 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation3 = (float)Math.Atan2((double)(vector10.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector10.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation3) * 50.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation3) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color3 = default(Color);
						Rectangle rectangle3 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count3 = 10;
						for (int l = 1; l <= count3; l++)
						{
							int dust3 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 31, 0f, 0f, 100, color3, 2.5f);
							Main.dust[dust3].noGravity = false;
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
						Vector2 vector11 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation4 = (float)Math.Atan2((double)(vector11.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector11.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation4) * 50.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation4) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color4 = default(Color);
						Rectangle rectangle4 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count4 = 10;
						for (int m = 1; m <= count4; m++)
						{
							int dust4 = Dust.NewDust(base.npc.position, rectangle4.Width, rectangle4.Height, 31, 0f, 0f, 100, color4, 2.5f);
							Main.dust[dust4].noGravity = false;
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
								int p20 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p20].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p21 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p21].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 90f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p22 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p23 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int p24 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p25 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int p26 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p22].netUpdate = true;
								Main.projectile[p23].netUpdate = true;
								Main.projectile[p24].netUpdate = true;
								Main.projectile[p25].netUpdate = true;
								Main.projectile[p26].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p27 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p28 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int p29 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p30 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int p31 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p27].netUpdate = true;
								Main.projectile[p28].netUpdate = true;
								Main.projectile[p29].netUpdate = true;
								Main.projectile[p30].netUpdate = true;
								Main.projectile[p31].netUpdate = true;
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
								int p32 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p32].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p33 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p33].netUpdate = true;
							}
							this.customAI[0] = 0f;
						}
					}
					if (this.customAI[4] == 700f)
					{
						int p34 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
						Main.projectile[p34].netUpdate = true;
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
						Vector2 vector12 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation5 = (float)Math.Atan2((double)(vector12.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector12.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation5) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation5) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color5 = default(Color);
						Rectangle rectangle5 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count5 = 10;
						for (int n = 1; n <= count5; n++)
						{
							int dust5 = Dust.NewDust(base.npc.position, rectangle5.Width, rectangle5.Height, 31, 0f, 0f, 100, color5, 2.5f);
							Main.dust[dust5].noGravity = false;
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
						Vector2 vector13 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation6 = (float)Math.Atan2((double)(vector13.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector13.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation6) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation6) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color6 = default(Color);
						Rectangle rectangle6 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count6 = 10;
						for (int i2 = 1; i2 <= count6; i2++)
						{
							int dust6 = Dust.NewDust(base.npc.position, rectangle6.Width, rectangle6.Height, 31, 0f, 0f, 100, color6, 2.5f);
							Main.dust[dust6].noGravity = false;
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
						Vector2 vector14 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation7 = (float)Math.Atan2((double)(vector14.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector14.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation7) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation7) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color7 = default(Color);
						Rectangle rectangle7 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count7 = 10;
						for (int i3 = 1; i3 <= count7; i3++)
						{
							int dust7 = Dust.NewDust(base.npc.position, rectangle7.Width, rectangle7.Height, 31, 0f, 0f, 100, color7, 2.5f);
							Main.dust[dust7].noGravity = false;
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
						int p35 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), 465, 30, 3f, 255, 0f, 0f);
						Main.projectile[p35].netUpdate = true;
					}
					if (this.customAI[4] >= 300f && this.customAI[4] < 700f)
					{
						this.customAI[0] += 1f;
						if (this.customAI[0] == 20f || this.customAI[0] == 40f || this.customAI[0] == 60f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p36 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p36].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p37 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p37].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 90f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p38 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p39 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int p40 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p41 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int p42 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p38].netUpdate = true;
								Main.projectile[p39].netUpdate = true;
								Main.projectile[p40].netUpdate = true;
								Main.projectile[p41].netUpdate = true;
								Main.projectile[p42].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p43 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p44 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int p45 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p46 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int p47 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p43].netUpdate = true;
								Main.projectile[p44].netUpdate = true;
								Main.projectile[p45].netUpdate = true;
								Main.projectile[p46].netUpdate = true;
								Main.projectile[p47].netUpdate = true;
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
								int p48 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p48].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p49 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p49].netUpdate = true;
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
						Vector2 vector15 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation8 = (float)Math.Atan2((double)(vector15.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector15.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation8) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation8) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color8 = default(Color);
						Rectangle rectangle8 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count8 = 10;
						for (int i4 = 1; i4 <= count8; i4++)
						{
							int dust8 = Dust.NewDust(base.npc.position, rectangle8.Width, rectangle8.Height, 31, 0f, 0f, 100, color8, 2.5f);
							Main.dust[dust8].noGravity = false;
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
						Vector2 vector16 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation9 = (float)Math.Atan2((double)(vector16.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector16.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation9) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation9) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color9 = default(Color);
						Rectangle rectangle9 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count9 = 10;
						for (int i5 = 1; i5 <= count9; i5++)
						{
							int dust9 = Dust.NewDust(base.npc.position, rectangle9.Width, rectangle9.Height, 31, 0f, 0f, 100, color9, 2.5f);
							Main.dust[dust9].noGravity = false;
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
						Vector2 vector17 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation10 = (float)Math.Atan2((double)(vector17.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector17.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation10) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation10) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color10 = default(Color);
						Rectangle rectangle10 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count10 = 10;
						for (int i6 = 1; i6 <= count10; i6++)
						{
							int dust10 = Dust.NewDust(base.npc.position, rectangle10.Width, rectangle10.Height, 31, 0f, 0f, 100, color10, 2.5f);
							Main.dust[dust10].noGravity = false;
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
						Vector2 vector18 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation11 = (float)Math.Atan2((double)(vector18.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector18.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation11) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation11) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color11 = default(Color);
						Rectangle rectangle11 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count11 = 10;
						for (int i7 = 1; i7 <= count11; i7++)
						{
							int dust11 = Dust.NewDust(base.npc.position, rectangle11.Width, rectangle11.Height, 235, 0f, 0f, 100, color11, 1.5f);
							Main.dust[dust11].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 20f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 22f)
					{
						this.teleport = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 30f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector19 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation12 = (float)Math.Atan2((double)(vector19.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector19.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation12) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation12) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color12 = default(Color);
						Rectangle rectangle12 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count12 = 10;
						for (int i8 = 1; i8 <= count12; i8++)
						{
							int dust12 = Dust.NewDust(base.npc.position, rectangle12.Width, rectangle12.Height, 235, 0f, 0f, 100, color12, 1.5f);
							Main.dust[dust12].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 40f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 42f)
					{
						this.teleport = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 50f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector20 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation13 = (float)Math.Atan2((double)(vector20.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector20.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation13) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation13) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color13 = default(Color);
						Rectangle rectangle13 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count13 = 10;
						for (int i9 = 1; i9 <= count13; i9++)
						{
							int dust13 = Dust.NewDust(base.npc.position, rectangle13.Width, rectangle13.Height, 235, 0f, 0f, 100, color13, 1.5f);
							Main.dust[dust13].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 60f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 62f)
					{
						this.teleport = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 70f)
					{
						this.idle2R = false;
						this.chargeAttackR = true;
						Vector2 vector21 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						float rotation14 = (float)Math.Atan2((double)(vector21.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector21.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = (float)(Math.Cos((double)rotation14) * 60.0) * -1f;
						base.npc.velocity.Y = (float)(Math.Sin((double)rotation14) * 10.0) * -1f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Color color14 = default(Color);
						Rectangle rectangle14 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int count14 = 10;
						for (int i10 = 1; i10 <= count14; i10++)
						{
							int dust14 = Dust.NewDust(base.npc.position, rectangle14.Width, rectangle14.Height, 235, 0f, 0f, 100, color14, 1.5f);
							Main.dust[dust14].noGravity = false;
						}
						base.npc.netUpdate = true;
						return;
					}
					if (this.customAI[4] == 80f)
					{
						this.idle2R = true;
						this.chargeAttackR = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 100f)
					{
						this.rocketEvent = true;
						base.npc.netUpdate = true;
					}
					if (this.customAI[4] == 140f)
					{
						this.rocketEvent = true;
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
								int p50 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p50].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p51 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p51].netUpdate = true;
							}
							base.npc.netUpdate = true;
						}
						if (this.customAI[0] >= 100f)
						{
							if (base.npc.direction == -1)
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p52 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p53 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int p54 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p55 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int p56 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p52].netUpdate = true;
								Main.projectile[p53].netUpdate = true;
								Main.projectile[p54].netUpdate = true;
								Main.projectile[p55].netUpdate = true;
								Main.projectile[p56].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p57 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, 4f), 435, 30, 3f, 255, 0f, 0f);
								int p58 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, 2f), 435, 30, 3f, 255, 0f, 0f);
								int p59 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 435, 30, 3f, 255, 0f, 0f);
								int p60 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(9f, -2f), 435, 30, 3f, 255, 0f, 0f);
								int p61 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(8f, -4f), 435, 30, 3f, 255, 0f, 0f);
								Main.projectile[p57].netUpdate = true;
								Main.projectile[p58].netUpdate = true;
								Main.projectile[p59].netUpdate = true;
								Main.projectile[p60].netUpdate = true;
								Main.projectile[p61].netUpdate = true;
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
								int p62 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(-10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p62].netUpdate = true;
							}
							else
							{
								Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
								int p63 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 54f), new Vector2(10f, 0f), 462, 30, 3f, 255, 0f, 0f);
								Main.projectile[p63].netUpdate = true;
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
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.75f) && this.shieldEvent == 0)
			{
				base.npc.ai[2] = 2f;
				this.customAI[1] += 1f;
				this.chargeAttack = false;
				if (this.customAI[1] == 5f && !RedeConfigClient.Instance.NoBossText)
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
						int p64 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
						Main.projectile[p64].netUpdate = true;
					}
					if (this.customAI[1] == 320f)
					{
						int p65 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
						Main.projectile[p65].netUpdate = true;
					}
				}
				if (this.customAI[1] >= 500f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					if (!RedeConfigClient.Instance.NoBossText)
					{
						string text15 = "I never needed them anyway.";
						Color rarityCyan = Colors.RarityCyan;
						byte r15 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g15 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text15, r15, g15, rarityCyan.B, false);
					}
					this.customAI[1] = 0f;
					this.shieldEvent = KSEntrance.AISTATE_SHIELD1;
					this.customAI[4] = 0f;
					base.npc.ai[2] = 1f;
					this.shieldUp = false;
					this.idle2 = true;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.55f) && this.shieldEvent == KSEntrance.AISTATE_SHIELD1)
			{
				base.npc.ai[2] = 2f;
				this.customAI[1] += 1f;
				this.chargeAttack = false;
				if (this.customAI[1] == 5f && !RedeConfigClient.Instance.NoBossText)
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
					int p66 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p66].netUpdate = true;
				}
				if (this.customAI[1] == 240f)
				{
					int p67 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p67].netUpdate = true;
				}
				if (this.customAI[1] == 300f)
				{
					int p68 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p68].netUpdate = true;
				}
				if (this.customAI[1] == 360f)
				{
					int p69 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p69].netUpdate = true;
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
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.35f) && this.shieldEvent == KSEntrance.AISTATE_SHIELD2)
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
					int pieCut = 8;
					for (int m2 = 0; m2 < pieCut; m2++)
					{
						int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m2 / (float)pieCut * 6.28f);
						Main.projectile[projID].netUpdate = true;
					}
					base.npc.netUpdate = true;
				}
				if (this.customAI[1] == 150f)
				{
					int pieCut2 = 8;
					for (int m3 = 0; m3 < pieCut2; m3++)
					{
						int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m3 / (float)pieCut2 * 6.28f);
						Main.projectile[projID2].netUpdate = true;
					}
				}
				if (this.customAI[1] == 60f)
				{
					int p70 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p70].netUpdate = true;
				}
				if (this.customAI[1] == 120f)
				{
					int p71 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p71].netUpdate = true;
				}
				if (this.customAI[1] == 180f)
				{
					int p72 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p72].netUpdate = true;
				}
				if (this.customAI[1] == 240f)
				{
					int p73 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p73].netUpdate = true;
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
				if (this.customAI[1] == 5f && !RedeConfigClient.Instance.NoBossText)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
					{
						string text17 = "How am I losing to a WEAK LITTLE ANDROID!?";
						Color rarityCyan = Colors.RarityCyan;
						byte r17 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g17 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text17, r17, g17, rarityCyan.B, false);
					}
					else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().chickenPower)
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
					int pieCut3 = 8;
					for (int m4 = 0; m4 < pieCut3; m4++)
					{
						int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m4 / (float)pieCut3 * 6.28f);
						Main.projectile[projID3].netUpdate = true;
					}
				}
				if (this.customAI[1] == 220f)
				{
					int pieCut4 = 8;
					for (int m5 = 0; m5 < pieCut4; m5++)
					{
						int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m5 / (float)pieCut4 * 6.28f);
						Main.projectile[projID4].netUpdate = true;
					}
				}
				if (this.customAI[1] == 260f)
				{
					int pieCut5 = 8;
					for (int m6 = 0; m6 < pieCut5; m6++)
					{
						int projID5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m6 / (float)pieCut5 * 6.28f);
						Main.projectile[projID5].netUpdate = true;
					}
				}
				if (this.customAI[1] == 300f)
				{
					int pieCut6 = 8;
					for (int m7 = 0; m7 < pieCut6; m7++)
					{
						int projID6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[projID6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m7 / (float)pieCut6 * 6.28f);
						Main.projectile[projID6].netUpdate = true;
					}
				}
				if (this.customAI[1] == 340f)
				{
					int pieCut7 = 8;
					for (int m8 = 0; m8 < pieCut7; m8++)
					{
						int projID7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 435, 30, 3f, 255, 0f, 0f);
						Main.projectile[projID7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m8 / (float)pieCut7 * 6.28f);
						Main.projectile[projID7].netUpdate = true;
					}
				}
				if (this.customAI[1] == 180f)
				{
					int p74 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p74].netUpdate = true;
				}
				if (this.customAI[1] == 240f)
				{
					int p75 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p75].netUpdate = true;
				}
				if (this.customAI[1] == 300f)
				{
					int p76 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(4f, -4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p76].netUpdate = true;
				}
				if (this.customAI[1] == 360f)
				{
					int p77 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(-4f, 4f), base.mod.ProjectileType("KSOrb1"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p77].netUpdate = true;
				}
				if (this.customAI[1] >= 580f && !NPC.AnyNPCs(base.mod.NPCType("SpaceKeeper")))
				{
					this.musicChange = true;
					this.customAI[2] += 1f;
					if (this.customAI[2] == 5f && !RedeConfigClient.Instance.NoBossText)
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
					if (this.customAI[2] == 50f && !RedeConfigClient.Instance.NoBossText)
					{
						string text21 = "NO! HOW IS THIS HAPPENING!?";
						Color rarityCyan = Colors.RarityCyan;
						byte r21 = rarityCyan.R;
						rarityCyan = Colors.RarityCyan;
						byte g21 = rarityCyan.G;
						rarityCyan = Colors.RarityCyan;
						Main.NewText(text21, r21, g21, rarityCyan.B, false);
					}
					if (this.customAI[2] == 150f && !RedeConfigClient.Instance.NoBossText)
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
					if (this.customAI[2] == 320f && !RedeConfigClient.Instance.NoBossText)
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
						for (int i11 = 0; i11 < 40; i11++)
						{
							int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
							Main.dust[dustIndex].velocity *= 2.9f;
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
							int p78 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFistR"), 35, 3f, 255, 0f, 0f);
							Main.projectile[p78].netUpdate = true;
						}
						else
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							int p79 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFistR"), 35, 3f, 255, 0f, 0f);
							Main.projectile[p79].netUpdate = true;
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
							int p80 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(-10f, 0f), base.mod.ProjectileType("KSFist"), 35, 3f, 255, 0f, 0f);
							Main.projectile[p80].netUpdate = true;
						}
						else
						{
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							int p81 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 46f, base.npc.position.Y + 52f), new Vector2(10f, 0f), base.mod.ProjectileType("KSFist"), 35, 3f, 255, 0f, 0f);
							Main.projectile[p81].netUpdate = true;
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
			if (this.teleport)
			{
				if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
				{
					base.npc.netUpdate = true;
					this.teleportTimer++;
					if (this.teleportTimer == 2)
					{
						for (int i12 = 0; i12 < 20; i12++)
						{
							int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[dustIndex2].velocity *= 1.4f;
						}
					}
					if (this.teleportTimer == 4 && Main.netMode != 1)
					{
						int num = Main.rand.Next(2);
						if (num == 0)
						{
							Vector2 newPos = new Vector2((float)Main.rand.Next(-500, -400), 0f);
							base.npc.Center = Main.player[base.npc.target].Center + newPos;
							base.npc.netUpdate = true;
						}
						if (num == 1)
						{
							Vector2 newPos2 = new Vector2((float)Main.rand.Next(400, 500), 0f);
							base.npc.Center = Main.player[base.npc.target].Center + newPos2;
							base.npc.netUpdate = true;
						}
					}
					if (this.teleportTimer >= 6)
					{
						for (int i13 = 0; i13 < 20; i13++)
						{
							int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[dustIndex3].velocity *= 1.4f;
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
						for (int i14 = 0; i14 < 20; i14++)
						{
							int dustIndex4 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[dustIndex4].velocity *= 1.4f;
						}
					}
					if (this.teleportTimer == 4 && Main.netMode != 1)
					{
						int num2 = Main.rand.Next(2);
						if (num2 == 0)
						{
							Vector2 newPos3 = new Vector2((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + newPos3;
							base.npc.netUpdate = true;
						}
						if (num2 == 1)
						{
							Vector2 newPos4 = new Vector2((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-300, -200));
							base.npc.Center = Main.player[base.npc.target].Center + newPos4;
							base.npc.netUpdate = true;
						}
					}
					if (this.teleportTimer >= 6)
					{
						for (int i15 = 0; i15 < 20; i15++)
						{
							int dustIndex5 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[dustIndex5].velocity *= 1.4f;
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

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D charge = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSCharge");
			Texture2D chargeR = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSChargeR");
			Texture2D idlesprite = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle1");
			Texture2D idlesprite2 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle2");
			Texture2D idlesprite2R = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle2R");
			Texture2D fist = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSRocket1");
			Texture2D fist1R = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSRocket1R");
			Texture2D fist2 = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSRocket2");
			Texture2D shield = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSShield");
			base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSExit");
			Texture2D gunblink = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSGunBlink");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (!this.start)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			if (this.blinkGun)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = gunblink.Height / 4;
				int y6 = num214 * this.gunblinkFrame;
				Main.spriteBatch.Draw(gunblink, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, gunblink.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)gunblink.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.idle1)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = idlesprite.Height / 4;
				int y7 = num215 * this.idle1Frame;
				Main.spriteBatch.Draw(idlesprite, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, idlesprite.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)idlesprite.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.idle2)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num216 = idlesprite2.Height / 4;
				int y8 = num216 * this.idle2Frame;
				Main.spriteBatch.Draw(idlesprite2, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, idlesprite2.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)idlesprite2.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.idle2R)
			{
				Vector2 drawCenter4 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num217 = idlesprite2R.Height / 4;
				int y9 = num217 * this.idle2RFrame;
				Main.spriteBatch.Draw(idlesprite2R, drawCenter4 - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, idlesprite2R.Width, num217)), drawColor, base.npc.rotation, new Vector2((float)idlesprite2R.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.shieldUp)
			{
				Vector2 drawCenter5 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num218 = shield.Height / 7;
				int y10 = num218 * this.shieldFrame;
				Main.spriteBatch.Draw(shield, drawCenter5 - Main.screenPosition, new Rectangle?(new Rectangle(0, y10, shield.Width, num218)), drawColor, base.npc.rotation, new Vector2((float)shield.Width / 2f, (float)num218 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.chargeAttack)
			{
				Vector2 drawCenter6 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num219 = charge.Height / 2;
				int y11 = num219 * this.chargeFrame;
				Main.spriteBatch.Draw(charge, drawCenter6 - Main.screenPosition, new Rectangle?(new Rectangle(0, y11, charge.Width, num219)), drawColor, base.npc.rotation, new Vector2((float)charge.Width / 2f, (float)num219 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				new Vector2((float)charge.Width * 0.5f, (float)charge.Height * 0.5f);
				for (int i = this.oldPos.Length - 1; i >= 0; i--)
				{
					float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(charge, this.oldPos[i] - Main.screenPosition, new Rectangle?(new Rectangle(0, y11, charge.Width, num219)), drawColor * (0.5f * alpha), this.oldrot[i], new Vector2((float)charge.Width / 2f, (float)num219 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			if (this.chargeAttackR)
			{
				Vector2 drawCenter7 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num220 = chargeR.Height / 2;
				int y12 = num220 * this.chargeRFrame;
				Main.spriteBatch.Draw(chargeR, drawCenter7 - Main.screenPosition, new Rectangle?(new Rectangle(0, y12, chargeR.Width, num220)), drawColor, base.npc.rotation, new Vector2((float)chargeR.Width / 2f, (float)num220 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				new Vector2((float)chargeR.Width * 0.5f, (float)chargeR.Height * 0.5f);
				for (int j = this.oldPos.Length - 1; j >= 0; j--)
				{
					float alpha2 = 1f - (float)(j + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(chargeR, this.oldPos[j] - Main.screenPosition, new Rectangle?(new Rectangle(0, y12, chargeR.Width, num220)), drawColor * (0.5f * alpha2), this.oldrot[j], new Vector2((float)chargeR.Width / 2f, (float)num220 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			if (this.fistRocket)
			{
				Vector2 drawCenter8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num221 = fist.Height / 12;
				int y13 = num221 * this.rocketFrame;
				Main.spriteBatch.Draw(fist, drawCenter8 - Main.screenPosition, new Rectangle?(new Rectangle(0, y13, fist.Width, num221)), drawColor, base.npc.rotation, new Vector2((float)fist.Width / 2f, (float)num221 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.fistRocketR)
			{
				Vector2 drawCenter9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num222 = fist1R.Height / 12;
				int y14 = num222 * this.rocketRFrame;
				Main.spriteBatch.Draw(fist1R, drawCenter9 - Main.screenPosition, new Rectangle?(new Rectangle(0, y14, fist1R.Width, num222)), drawColor, base.npc.rotation, new Vector2((float)fist1R.Width / 2f, (float)num222 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.fistRocketDone)
			{
				Vector2 drawCenter10 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num223 = fist2.Height / 4;
				int y15 = num223 * this.rocketDoneFrame;
				Main.spriteBatch.Draw(fist2, drawCenter10 - Main.screenPosition, new Rectangle?(new Rectangle(0, y15, fist2.Width, num223)), drawColor, base.npc.rotation, new Vector2((float)fist2.Width / 2f, (float)num223 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
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
					if (this.deadTimer == 2 && !RedeConfigClient.Instance.NoBossText)
					{
						if (this.shieldEvent == KSEntrance.AISTATE_SHIELD4)
						{
							int num = Main.rand.Next(4);
							if (num == 0)
							{
								string text = "(Thank God)";
								Color color = Colors.RarityRed;
								byte r = color.R;
								color = Colors.RarityRed;
								byte g = color.G;
								color = Colors.RarityRed;
								Main.NewText(text, r, g, color.B, false);
							}
							if (num == 1)
							{
								string text2 = "HAH YES! Gotta be honest, you almost had me.";
								Color color = Colors.RarityRed;
								byte r2 = color.R;
								color = Colors.RarityRed;
								byte g2 = color.G;
								color = Colors.RarityRed;
								Main.NewText(text2, r2, g2, color.B, false);
							}
							if (num == 2)
							{
								string text3 = "You were so close...";
								Color color = Colors.RarityRed;
								byte r3 = color.R;
								color = Colors.RarityRed;
								byte g3 = color.G;
								color = Colors.RarityRed;
								Main.NewText(text3, r3, g3, color.B, false);
							}
							if (num == 3)
							{
								string text4 = "Congratulations... You actually made me worry for a second there!";
								Color color = Colors.RarityRed;
								byte r4 = color.R;
								color = Colors.RarityRed;
								byte g4 = color.G;
								color = Colors.RarityRed;
								Main.NewText(text4, r4, g4, color.B, false);
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
									Color color = Colors.RarityCyan;
									byte r5 = color.R;
									color = Colors.RarityCyan;
									byte g5 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text5, r5, g5, color.B, false);
								}
								if (num2 == 1)
								{
									string text6 = "What a pushover...";
									Color color = Colors.RarityCyan;
									byte r6 = color.R;
									color = Colors.RarityCyan;
									byte g6 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text6, r6, g6, color.B, false);
								}
								if (num2 == 2)
								{
									string text7 = "Y'know, you don't have to fight me right now... But it is fun beating you.";
									Color color = Colors.RarityCyan;
									byte r7 = color.R;
									color = Colors.RarityCyan;
									byte g7 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text7, r7, g7, color.B, false);
								}
								if (num2 == 3)
								{
									string text8 = "You should've just gave up when you had the chance.";
									Color color = Colors.RarityCyan;
									byte r8 = color.R;
									color = Colors.RarityCyan;
									byte g8 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text8, r8, g8, color.B, false);
								}
								if (num2 == 4)
								{
									string text9 = "Why did you try to fight me when you're still so weak? I want an actual challenge y'know!";
									Color color = Colors.RarityCyan;
									byte r9 = color.R;
									color = Colors.RarityCyan;
									byte g9 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text9, r9, g9, color.B, false);
								}
							}
							if (NPC.downedPlantBoss)
							{
								int num3 = Main.rand.Next(4);
								if (num3 == 0)
								{
									string text10 = "Are you seriously that weak!?";
									Color color = Colors.RarityCyan;
									byte r10 = color.R;
									color = Colors.RarityCyan;
									byte g10 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text10, r10, g10, color.B, false);
								}
								if (num3 == 1)
								{
									string text11 = "Come on, you can do better than that!";
									Color color = Colors.RarityCyan;
									byte r11 = color.R;
									color = Colors.RarityCyan;
									byte g11 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text11, r11, g11, color.B, false);
								}
								if (num3 == 2)
								{
									string text12 = "Hah.";
									Color color = Colors.RarityCyan;
									byte r12 = color.R;
									color = Colors.RarityCyan;
									byte g12 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text12, r12, g12, color.B, false);
								}
								if (num3 == 3)
								{
									string text13 = "A useless attempt...";
									Color color = Colors.RarityCyan;
									byte r13 = color.R;
									color = Colors.RarityCyan;
									byte g13 = color.G;
									color = Colors.RarityCyan;
									Main.NewText(text13, r13, g13, color.B, false);
								}
							}
						}
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

		private Player player;

		private float speed;

		private Vector2[] oldPos = new Vector2[3];

		private float[] oldrot = new float[3];

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
