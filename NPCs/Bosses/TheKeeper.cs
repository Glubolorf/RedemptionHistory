using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class TheKeeper : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = 0;
			base.npc.lifeMax = 2450;
			base.npc.damage = 30;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 150;
			base.npc.height = 182;
			base.npc.value = (float)Item.buyPrice(0, 1, 50, 0);
			base.npc.npcSlots = 1f;
			base.npc.boss = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossKeeper");
			this.bossBag = base.mod.ItemType("TheKeeperBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 100; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[dustIndex].velocity *= 2.6f;
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
			base.npc.defense = base.npc.defense + numPlayers;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 188;
			if (!RedeWorld.downedTheKeeper)
			{
				RedeWorld.redemptionPoints++;
				CombatText.NewText(this.player.getRect(), Color.Gold, "+1", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> An undead... disgusting. Good thing you killed it.", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedTheKeeper = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TheKeeperTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("OldGathicWaraxe"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TheKeeperMask"), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(5);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersBow"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersStaff"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersClaw"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersKnife"), 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersSummon"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("DarkShard"), Main.rand.Next(2, 3), false, 0, false, false);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 184;
				if (base.npc.frame.Y > 1288)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (this.shriekStart)
			{
				this.shriekCounter++;
				if (this.shriekCounter > 10)
				{
					this.shriekFrame++;
					this.shriekCounter = 0;
				}
				if (this.shriekFrame >= 3)
				{
					this.shriekFrame = 1;
				}
			}
			if (this.teddy1Event)
			{
				this.teddyCounter++;
				if (this.teddyCounter > 15)
				{
					this.teddyFrame++;
					this.teddyCounter = 0;
				}
				if (this.teddyFrame >= 3)
				{
					this.teddyFrame = 1;
				}
			}
			if (this.peaceful)
			{
				this.peaceCounter++;
				if (this.peaceCounter > 20)
				{
					this.peaceFrame++;
					this.peaceCounter = 0;
				}
				if (this.peaceFrame >= 3)
				{
					this.peaceFrame = 1;
				}
			}
			if (Main.dayTime)
			{
				NPC npc2 = base.npc;
				npc2.position.Y = npc2.position.Y - 300f;
			}
			this.Target();
			this.DespawnHandler();
			base.npc.ai[0] += 1f;
			if (base.npc.ai[0] >= 160f)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						for (int j = 0; j < player.inventory.Length; j++)
						{
							if (player.inventory[j].type == base.mod.ItemType("AbandonedTeddy"))
							{
								this.teddyEvent = true;
							}
						}
					}
				}
			}
			if (this.teddyEvent)
			{
				if (base.npc.ai[2] < 540f)
				{
					this.teddy1Event = true;
				}
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
				base.npc.dontTakeDamage = true;
				NPC npc3 = base.npc;
				npc3.velocity.X = npc3.velocity.X - 0.7f;
				NPC npc4 = base.npc;
				npc4.velocity.Y = npc4.velocity.Y - 0.7f;
				if (base.npc.velocity.X < 0f)
				{
					base.npc.velocity.X = 0f;
				}
				if (base.npc.velocity.Y < 0f)
				{
					base.npc.velocity.Y = 0f;
				}
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 60f)
				{
					string text = "The Keeper noticed the abandoned teddy you're holding...";
					Color rarityPurple = Colors.RarityPurple;
					byte r = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text, r, g, rarityPurple.B, false);
				}
				if (base.npc.ai[2] == 320f)
				{
					string text2 = "She starts to remember something...";
					Color rarityPurple = Colors.RarityPurple;
					byte r2 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g2 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text2, r2, g2, rarityPurple.B, false);
				}
				if (base.npc.ai[2] == 540f)
				{
					this.teddy1Event = false;
					this.peaceful = true;
					base.npc.netUpdate = true;
					string text3 = "Pain... Anger... Sadness... All those feelings were washed away...";
					Color rarityPurple = Colors.RarityPurple;
					byte r3 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g3 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text3, r3, g3, rarityPurple.B, false);
				}
				if (base.npc.ai[2] == 750f)
				{
					string text4 = "She only feels... at peace...";
					Color rarityPurple = Colors.RarityPurple;
					byte r4 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g4 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text4, r4, g4, rarityPurple.B, false);
				}
				if (base.npc.ai[2] >= 1000f)
				{
					base.npc.alpha++;
					if (Main.rand.Next(5) == 0)
					{
						Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 20, 0f, 0f, 0, default(Color), 1f);
					}
				}
				if (base.npc.alpha >= 255)
				{
					for (int k = 0; k < 50; k++)
					{
						int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 20, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[dustIndex].velocity *= 2.6f;
					}
					string text5 = "The Keeper's Spirit fades away...";
					Color rarityPurple = Colors.RarityPurple;
					byte r5 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g5 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text5, r5, g5, rarityPurple.B, false);
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeeperAcc"), 1, false, 0, false, false);
					if (!RedeWorld.keeperSaved)
					{
						RedeWorld.redemptionPoints += 3;
						CombatText.NewText(this.player.getRect(), Color.Gold, "+3", true, false);
						for (int l = 0; l < 255; l++)
						{
							Player player2 = Main.player[l];
							if (player2.active)
							{
								for (int m = 0; m < player2.inventory.Length; m++)
								{
									if (player2.inventory[m].type == base.mod.ItemType("RedemptionTeller"))
									{
										Main.NewText("<Chalice of Alignment> You've redeemed yourself, The Keeper may rest in undisturbed peace...", Color.DarkGoldenrod, false);
									}
								}
							}
						}
					}
					base.npc.netUpdate = true;
					RedeWorld.keeperSaved = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					base.npc.active = false;
				}
			}
			else
			{
				this.Move(new Vector2(240f, 0f));
				base.npc.ai[1] -= 1f;
				if (base.npc.ai[1] <= 0f)
				{
					this.Shoot();
				}
				if (Main.rand.Next(400) == 0)
				{
					int Minion = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul4"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion].netUpdate = true;
				}
				if (!Main.expertMode && base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
				{
					base.npc.ai[3] += 1f;
					if (base.npc.ai[3] == 20f)
					{
						this.shriekStart = true;
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
						}
					}
					if (base.npc.ai[3] == 40f)
					{
						int Minion2 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion2].netUpdate = true;
					}
					if (base.npc.ai[3] == 45f)
					{
						int Minion3 = NPC.NewNPC((int)base.npc.position.X + 60, (int)base.npc.position.Y + 80, base.mod.NPCType("DarkSoul4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion3].netUpdate = true;
					}
					if (base.npc.ai[3] == 50f)
					{
						int Minion4 = NPC.NewNPC((int)base.npc.position.X + 80, (int)base.npc.position.Y + 75, base.mod.NPCType("DarkSoul4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion4].netUpdate = true;
					}
					if (base.npc.ai[3] == 55f)
					{
						int Minion5 = NPC.NewNPC((int)base.npc.position.X + 50, (int)base.npc.position.Y + 60, base.mod.NPCType("DarkSoul4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion5].netUpdate = true;
						int Minion6 = NPC.NewNPC((int)base.npc.position.X + 85, (int)base.npc.position.Y + 50, base.mod.NPCType("DarkSoul4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion6].netUpdate = true;
					}
					if (base.npc.ai[3] == 60f)
					{
						int Minion7 = NPC.NewNPC((int)base.npc.position.X + 65, (int)base.npc.position.Y + 65, base.mod.NPCType("DarkSoul4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion7].netUpdate = true;
						int Minion8 = NPC.NewNPC((int)base.npc.position.X + 45, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion8].netUpdate = true;
					}
					if (base.npc.ai[3] >= 220f)
					{
						this.shriekStart = false;
					}
					if (Main.rand.Next(250) == 0)
					{
						int Minion9 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 160, base.mod.NPCType("SkeletonMinion"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion9].netUpdate = true;
					}
				}
				if (Main.expertMode && base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
				{
					base.npc.ai[3] += 1f;
					if (base.npc.ai[3] == 20f)
					{
						this.shriekStart = true;
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
						}
					}
					if (base.npc.ai[3] == 40f)
					{
						float distance = 90f;
						float n = 1.26f;
						for (int count = 0; count < 1; count++)
						{
							Vector2 spawn = base.npc.Center + distance * Utils.ToRotationVector2((float)count * n);
							int Minion10 = NPC.NewNPC((int)spawn.X, (int)spawn.Y, base.mod.NPCType("DarkSoul5"), 0, (float)base.npc.whoAmI, 0f, (float)count, 0f, 255);
							Main.npc[Minion10].netUpdate = true;
						}
					}
					if (base.npc.ai[3] == 60f)
					{
						float distance2 = 90f;
						float k2 = 1.26f;
						for (int count2 = 0; count2 < 1; count2++)
						{
							Vector2 spawn2 = base.npc.Center + distance2 * Utils.ToRotationVector2((float)count2 * k2);
							int Minion11 = NPC.NewNPC((int)spawn2.X, (int)spawn2.Y, base.mod.NPCType("DarkSoul5"), 0, (float)base.npc.whoAmI, 0f, (float)count2, 0f, 255);
							Main.npc[Minion11].netUpdate = true;
						}
					}
					if (base.npc.ai[3] == 80f)
					{
						float distance3 = 90f;
						float k3 = 1.26f;
						for (int count3 = 0; count3 < 1; count3++)
						{
							Vector2 spawn3 = base.npc.Center + distance3 * Utils.ToRotationVector2((float)count3 * k3);
							int Minion12 = NPC.NewNPC((int)spawn3.X, (int)spawn3.Y, base.mod.NPCType("DarkSoul5"), 0, (float)base.npc.whoAmI, 0f, (float)count3, 0f, 255);
							Main.npc[Minion12].netUpdate = true;
						}
					}
					if (base.npc.ai[3] == 100f)
					{
						float distance4 = 90f;
						float k4 = 1.26f;
						for (int count4 = 0; count4 < 1; count4++)
						{
							Vector2 spawn4 = base.npc.Center + distance4 * Utils.ToRotationVector2((float)count4 * k4);
							int Minion13 = NPC.NewNPC((int)spawn4.X, (int)spawn4.Y, base.mod.NPCType("DarkSoul5"), 0, (float)base.npc.whoAmI, 0f, (float)count4, 0f, 255);
							Main.npc[Minion13].netUpdate = true;
						}
					}
					if (base.npc.ai[3] == 120f)
					{
						float distance5 = 90f;
						float k5 = 1.26f;
						for (int count5 = 0; count5 < 1; count5++)
						{
							Vector2 spawn5 = base.npc.Center + distance5 * Utils.ToRotationVector2((float)count5 * k5);
							int Minion14 = NPC.NewNPC((int)spawn5.X, (int)spawn5.Y, base.mod.NPCType("DarkSoul5"), 0, (float)base.npc.whoAmI, 0f, (float)count5, 0f, 255);
							Main.npc[Minion14].netUpdate = true;
						}
					}
					if (base.npc.ai[3] >= 220f)
					{
						this.shriekStart = false;
					}
					if (Main.rand.Next(400) == 0 && NPC.CountNPCS(base.mod.NPCType("BoneWorm")) <= 3)
					{
						int Minion15 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 160, base.mod.NPCType("BoneWorm"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion15].netUpdate = true;
					}
				}
			}
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 1f);
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			if (Main.expertMode)
			{
				this.speed = 20f;
			}
			else
			{
				this.speed = 12f;
			}
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 25f;
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
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		private void Shoot()
		{
			int type = base.mod.ProjectileType("TheKeeperPro");
			Vector2 velocity = this.player.Center - base.npc.Center;
			float magnitude = this.Magnitude(velocity);
			if (magnitude > 0f)
			{
				velocity *= 10f / magnitude;
			}
			else
			{
				velocity = new Vector2(0f, 5f);
			}
			int ProjID = Projectile.NewProjectile(base.npc.Center, velocity, type, base.npc.damage / 2, 2f, 255, 0f, 0f);
			Main.projectile[ProjID].netUpdate = true;
			base.npc.ai[1] = 80f;
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/TheKeeper_Glow");
			Texture2D shriekAni = base.mod.GetTexture("NPCs/Bosses/TheKeeperShriek");
			Texture2D shriekGlow = base.mod.GetTexture("NPCs/Bosses/TheKeeperShriek_Glow");
			Texture2D teddy1Ani = base.mod.GetTexture("NPCs/Bosses/TheKeeperSpecial1");
			Texture2D teddy1Glow = base.mod.GetTexture("NPCs/Bosses/TheKeeperSpecial1_Glow");
			Texture2D teddy2Ani = base.mod.GetTexture("NPCs/Bosses/TheKeeperSpecial2");
			Texture2D teddy2Glow = base.mod.GetTexture("NPCs/Bosses/TheKeeperSpecial2_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (!this.shriekStart && !this.teddy1Event && !this.peaceful)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			if (this.shriekStart && !this.teddy1Event && !this.peaceful)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = shriekAni.Height / 3;
				int y6 = num214 * this.shriekFrame;
				Main.spriteBatch.Draw(shriekAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, shriekAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)shriekAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(shriekGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, shriekAni.Width, num214)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)shriekAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.teddy1Event)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = teddy1Ani.Height / 3;
				int y7 = num215 * this.teddyFrame;
				Main.spriteBatch.Draw(teddy1Ani, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, teddy1Ani.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)teddy1Ani.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(teddy1Glow, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, teddy1Glow.Width, num215)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)teddy1Glow.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.peaceful)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num216 = teddy2Ani.Height / 3;
				int y8 = num216 * this.peaceFrame;
				Main.spriteBatch.Draw(teddy2Ani, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, teddy2Ani.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)teddy2Ani.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(teddy2Glow, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, teddy2Glow.Width, num216)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)teddy2Glow.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private Player player;

		private float speed;

		private bool shriekStart;

		private int shriekFrame;

		private int shriekCounter;

		private bool teddyEvent;

		private bool peaceful;

		private int teddyFrame;

		private int peaceFrame;

		private int teddyCounter;

		private int peaceCounter;

		private bool teddy1Event;
	}
}
