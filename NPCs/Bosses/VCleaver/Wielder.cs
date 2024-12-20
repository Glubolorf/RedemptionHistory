using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.VCleaver
{
	[AutoloadBossHead]
	public class Wielder : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wielder Bot");
			Main.npcFrameCount[base.npc.type] = 19;
		}

		public override void SetDefaults()
		{
			base.npc.width = 20;
			base.npc.height = 32;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 2;
			base.npc.lifeMax = 35000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.chaseable = false;
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.5;
			return true;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool CheckActive()
		{
			Player player = Main.player[base.npc.target];
			return !player.active || player.dead || Main.dayTime || base.npc.ai[0] == 11f;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.ID);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.ID = reader.ReadInt32();
			}
		}

		public int ID
		{
			get
			{
				return (int)base.npc.ai[1];
			}
			set
			{
				base.npc.ai[1] = (float)value;
			}
		}

		private void AttackChoice()
		{
			if (this.CopyList == null || this.CopyList.Count == 0)
			{
				this.CopyList = new List<int>(this.AttackList);
			}
			this.ID = this.CopyList[Main.rand.Next(0, this.CopyList.Count)];
			this.CopyList.Remove(this.ID);
			base.npc.netUpdate = true;
		}

		public override void AI()
		{
			switch (this.aniType)
			{
			case 0:
				if (base.npc.velocity.Length() == 0f)
				{
					base.npc.frame.Y = 0;
				}
				else
				{
					base.npc.frame.Y = 42;
				}
				break;
			case 1:
				if (base.npc.frame.Y < 84)
				{
					base.npc.frame.Y = 84;
				}
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 42;
					if (base.npc.frame.Y > 210)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
						this.aniType = 0;
					}
				}
				break;
			case 2:
				if (base.npc.frame.Y < 252)
				{
					base.npc.frame.Y = 252;
				}
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc2 = base.npc;
					npc2.frame.Y = npc2.frame.Y + 42;
					if (base.npc.frame.Y > 420)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
						this.aniType = 0;
					}
				}
				break;
			case 3:
				if (base.npc.frame.Y < 462)
				{
					base.npc.frame.Y = 462;
				}
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc3 = base.npc;
					npc3.frame.Y = npc3.frame.Y + 42;
					if (base.npc.frame.Y >= 798)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 462;
					}
				}
				break;
			case 4:
				if (base.npc.frame.Y < 210)
				{
					base.npc.frame.Y = 210;
				}
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc4 = base.npc;
					npc4.frame.Y = npc4.frame.Y + 42;
					if (base.npc.frame.Y > 294)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 294;
					}
				}
				break;
			}
			this.frameCounters++;
			if (this.frameCounters > 5)
			{
				this.boosterFrame++;
				this.frameCounters = 0;
			}
			if (this.boosterFrame >= 4)
			{
				this.boosterFrame = 0;
			}
			if (base.npc.ai[1] != 2f)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X + 3f, base.npc.position.Y + 16f), 10, 2, 235, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				Dust dust = Main.dust[dustIndex];
				dust.velocity.Y = 3f;
				dust.velocity.X = 0f;
			}
			this.DespawnHandler();
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (base.npc.ai[0] >= 3f && base.npc.ai[0] < 10f)
			{
				base.npc.dontTakeDamage = false;
			}
			else
			{
				base.npc.dontTakeDamage = true;
			}
			if (base.npc.ai[0] > 2f)
			{
				if (!this.barrierSpawn)
				{
					int degrees = 0;
					for (int i = 0; i < 36; i++)
					{
						degrees += 10;
						int N2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<WielderShield>(), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[N2].ai[0] = (float)base.npc.whoAmI;
						Main.npc[N2].ai[1] = (float)degrees;
					}
					this.barrierSpawn = true;
				}
				if (Vector2.Distance(base.npc.Center, player.Center) > 1500f && base.npc.ai[0] != 11f)
				{
					player.AddBuff(144, 10, true);
				}
			}
			if (this.cooldown < 0)
			{
				this.cooldown = 0;
			}
			Vector2 SwingPos = new Vector2((float)((base.npc.Center.X > player.Center.X) ? 150 : -150), -20f);
			Vector2 AwayPos = new Vector2((float)((base.npc.Center.X > player.Center.X) ? 500 : -500), -40f);
			player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
			float obj = base.npc.ai[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (!2f.Equals(obj))
					{
						if (!3f.Equals(obj))
						{
							if (!4f.Equals(obj))
							{
								if (10f.Equals(obj))
								{
									base.npc.LookAtPlayer();
									this.aniType = 0;
									base.npc.velocity *= 0.96f;
									base.npc.rotation = 0f;
									base.npc.ai[2] = 0f;
									base.npc.ai[0] = 11f;
									base.npc.ai[3] = 0f;
									base.npc.ai[1] = 0f;
									base.npc.netUpdate = true;
									return;
								}
								if (!11f.Equals(obj))
								{
									return;
								}
								base.npc.LookAtPlayer();
								if (base.npc.ai[2] < 260f)
								{
									player.GetModPlayer<ScreenPlayer>().lockScreen = true;
								}
								base.npc.velocity *= 0.96f;
								base.npc.ai[2] += 1f;
								if (base.npc.ai[2] == 180f)
								{
									CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "...Nah.", true, false);
								}
								if (base.npc.ai[2] > 260f)
								{
									this.aniType = 3;
									NPC npc5 = base.npc;
									npc5.velocity.Y = npc5.velocity.Y - 1f;
									if (base.npc.timeLeft > 10)
									{
										base.npc.timeLeft = 10;
									}
								}
							}
							else
							{
								if (!NPC.AnyNPCs(ModContent.NPCType<VlitchCleaver>()))
								{
									base.npc.ai[0] = 10f;
									base.npc.netUpdate = true;
									return;
								}
								switch (this.ID)
								{
								case 0:
									base.npc.LookAtPlayer();
									if (base.npc.ai[2] < 120f)
									{
										base.npc.ai[3] = 3f;
									}
									base.npc.ai[2] += 1f;
									if (base.npc.ai[2] < 80f)
									{
										base.npc.Move(SwingPos, 16f, 10f, true);
										return;
									}
									base.npc.velocity *= 0.94f;
									if (base.npc.ai[2] == 120f)
									{
										this.aniType = 1;
										base.npc.ai[3] = 4f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] == 125f)
									{
										base.npc.Dash(7, false, SoundID.Item1.WithVolume(0f), player.Center);
									}
									if (base.npc.ai[2] > 150f)
									{
										base.npc.ai[3] = 0f;
										base.npc.ai[0] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
										return;
									}
									break;
								case 1:
									base.npc.LookAtPlayer();
									if (base.npc.ai[2] < 80f)
									{
										base.npc.ai[3] = 3f;
										base.npc.Move(AwayPos, 16f, 10f, true);
									}
									base.npc.ai[2] += 1f;
									base.npc.velocity *= 0.94f;
									if (base.npc.ai[2] == 80f)
									{
										this.aniType = 2;
										base.npc.ai[3] = 5f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] == 90f)
									{
										base.npc.Dash(12, false, SoundID.Item1.WithVolume(0f), player.Center);
									}
									if (base.npc.ai[2] > 200f)
									{
										base.npc.ai[3] = 0f;
										base.npc.ai[0] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
										return;
									}
									break;
								case 2:
									if (base.npc.Distance(player.Center) > 700f && base.npc.ai[2] == 0f)
									{
										base.npc.ai[2] = 1f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] == 0f)
									{
										this.AttackChoice();
										base.npc.netUpdate = true;
										return;
									}
									base.npc.LookAtPlayer();
									if (base.npc.ai[2] < 80f)
									{
										base.npc.ai[3] = 3f;
										base.npc.Move(SwingPos, 6f, 10f, true);
									}
									base.npc.ai[2] += 1f;
									if (base.npc.ai[2] == 80f)
									{
										this.aniType = 3;
										base.npc.ai[3] = 6f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] >= 80f)
									{
										base.npc.Move(new Vector2(0f, 0f), 24f, 50f, true);
										base.npc.rotation = Utils.ToRotation(base.npc.velocity) + 1.57f;
									}
									if (base.npc.ai[2] > 400f)
									{
										base.npc.rotation = 0f;
										this.aniType = 0;
										base.npc.ai[3] = 0f;
										base.npc.ai[0] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
										return;
									}
									break;
								case 3:
									base.npc.LookAtPlayer();
									if (base.npc.ai[2] < 120f)
									{
										base.npc.ai[3] = 3f;
									}
									base.npc.ai[2] += 1f;
									if (base.npc.ai[2] < 80f)
									{
										base.npc.Move(SwingPos, 16f, 10f, true);
										return;
									}
									base.npc.velocity *= 0.94f;
									if (base.npc.ai[2] == 120f)
									{
										this.aniType = 1;
										base.npc.ai[3] = 7f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] == 125f)
									{
										base.npc.Dash(7, false, SoundID.Item1.WithVolume(0f), player.Center);
									}
									if (base.npc.ai[2] > 180f)
									{
										base.npc.ai[3] = 0f;
										base.npc.ai[0] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
										return;
									}
									break;
								case 4:
									if (base.npc.ai[2] == 0f)
									{
										if (this.cooldown == 0)
										{
											base.npc.ai[3] = 20f;
										}
										else
										{
											base.npc.ai[2] = -1f;
										}
										base.npc.netUpdate = true;
										return;
									}
									if (base.npc.ai[2] <= 0f)
									{
										this.AttackChoice();
										base.npc.netUpdate = true;
										return;
									}
									base.npc.LookAtPlayer();
									if (base.npc.ai[2] < 81f)
									{
										base.npc.ai[3] = 3f;
									}
									base.npc.ai[2] += 1f;
									base.npc.Move(AwayPos, 9f, 10f, true);
									if (base.npc.ai[2] == 81f)
									{
										this.aniType = 2;
										base.npc.ai[3] = 8f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] > 361f)
									{
										this.cooldown = 2;
										base.npc.ai[3] = 0f;
										base.npc.ai[0] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
										return;
									}
									break;
								case 5:
									if (base.npc.ai[2] == 0f)
									{
										if (this.cooldown == 0)
										{
											base.npc.ai[3] = 21f;
										}
										else
										{
											base.npc.ai[2] = -1f;
										}
										base.npc.netUpdate = true;
										return;
									}
									if (base.npc.ai[2] <= 0f)
									{
										this.AttackChoice();
										base.npc.netUpdate = true;
										return;
									}
									base.npc.LookAtPlayer();
									if (base.npc.ai[2] < 81f)
									{
										base.npc.ai[3] = 3f;
										base.npc.Move(AwayPos, 8f, 10f, true);
									}
									base.npc.ai[2] += 1f;
									if (base.npc.ai[2] == 81f)
									{
										this.aniType = 3;
										base.npc.ai[3] = 9f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] >= 81f)
									{
										base.npc.Move(AwayPos, 10f, 10f, true);
										base.npc.rotation = Utils.ToRotation(base.npc.velocity) + 1.57f;
									}
									if (base.npc.ai[2] > 321f)
									{
										this.cooldown = 2;
										base.npc.rotation = 0f;
										this.aniType = 0;
										base.npc.ai[3] = 0f;
										base.npc.ai[0] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
										return;
									}
									break;
								case 6:
									if (base.npc.ai[2] == 0f)
									{
										if (this.cooldown == 0)
										{
											base.npc.ai[3] = 22f;
										}
										else
										{
											base.npc.ai[2] = -1f;
										}
										base.npc.netUpdate = true;
										return;
									}
									if (base.npc.ai[2] <= 0f)
									{
										this.AttackChoice();
										base.npc.netUpdate = true;
										return;
									}
									base.npc.LookAtPlayer();
									if (base.npc.ai[2] < 81f)
									{
										base.npc.ai[3] = 3f;
									}
									base.npc.ai[2] += 1f;
									base.npc.Move(AwayPos, 13f, 10f, true);
									if (base.npc.ai[2] == 81f)
									{
										this.aniType = 2;
										base.npc.ai[3] = 10f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] >= 800f)
									{
										this.cooldown = 2;
										base.npc.ai[3] = 0f;
										base.npc.ai[0] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
										return;
									}
									break;
								case 7:
									base.npc.LookAtPlayer();
									base.npc.Move(AwayPos, 13f, 10f, true);
									if (base.npc.ai[2] < 100f)
									{
										if (base.npc.ai[2] < 40f)
										{
											base.npc.ai[3] = 3f;
										}
										base.npc.ai[2] += 1f;
										if (base.npc.ai[2] == 40f)
										{
											this.aniType = 1;
											base.npc.ai[3] = 11f;
											base.npc.ai[2] = 100f;
											base.npc.netUpdate = true;
										}
									}
									if (base.npc.ai[2] == 200f)
									{
										this.aniType = 1;
										base.npc.Dash(7, false, SoundID.Item1.WithVolume(0f), player.Center);
										base.npc.ai[2] = 100f;
									}
									if (base.npc.ai[2] == 300f)
									{
										this.aniType = 2;
										base.npc.Dash(7, false, SoundID.Item1.WithVolume(0f), player.Center);
										base.npc.ai[2] = 100f;
									}
									if (base.npc.ai[2] >= 1000f)
									{
										base.npc.ai[3] = 0f;
										base.npc.ai[0] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
										return;
									}
									break;
								default:
									return;
								}
							}
						}
						else
						{
							if (!NPC.AnyNPCs(ModContent.NPCType<VlitchCleaver>()))
							{
								base.npc.ai[0] = 10f;
								base.npc.netUpdate = true;
								return;
							}
							base.npc.LookAtPlayer();
							this.aniType = 0;
							base.npc.velocity *= 0.98f;
							if (base.npc.ai[3] == 2f)
							{
								base.npc.ai[2] += 1f;
								if (base.npc.ai[2] == 5f && this.cooldown > 0)
								{
									this.cooldown--;
								}
								if (base.npc.ai[2] > 60f)
								{
									base.npc.ai[2] = 0f;
									base.npc.ai[0] = 4f;
									base.npc.ai[3] = 0f;
									this.AttackChoice();
									base.npc.netUpdate = true;
									return;
								}
							}
						}
					}
					else
					{
						player.GetModPlayer<ScreenPlayer>().lockScreen = true;
						if (base.npc.ai[3] == 1f)
						{
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] > 60f)
							{
								if (Main.netMode != 1)
								{
									int degrees2 = 0;
									for (int j = 0; j < 4; j++)
									{
										degrees2 += 90;
										int p = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<WielderOrb>(), 33, 0f, Main.myPlayer, 0f, 0f);
										Main.projectile[p].ai[0] = (float)base.npc.whoAmI;
										Main.projectile[p].ai[1] = (float)degrees2;
									}
								}
								base.npc.ai[3] = 0f;
								base.npc.ai[0] = 3f;
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
								return;
							}
						}
					}
				}
				else
				{
					base.npc.ai[2] += 1f;
					player.GetModPlayer<ScreenPlayer>().lockScreen = true;
					if (base.npc.ai[2] > 80f)
					{
						if (!NPC.AnyNPCs(ModContent.NPCType<VlitchCleaver>()))
						{
							base.npc.SpawnNPC((base.npc.spriteDirection == 1) ? ((int)base.npc.Center.X - 1400) : ((int)base.npc.Center.X + 1400), (int)base.npc.Center.Y + 150, ModContent.NPCType<VlitchCleaver>(), 0f, 0f, 0f, (float)base.npc.whoAmI);
						}
						this.aniType = 4;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
				}
			}
			else
			{
				base.npc.ai[2] += 1f;
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				if (base.npc.ai[2] == 1f)
				{
					this.aniType = 3;
					NPC npc6 = base.npc;
					npc6.velocity.Y = npc6.velocity.Y - 8f;
				}
				if (base.npc.Center.Y < player.Center.Y + 80f || base.npc.ai[2] > 200f)
				{
					base.npc.LookAtPlayer();
					this.aniType = 0;
					base.npc.velocity *= 0.94f;
					if (base.npc.velocity.Length() < 3f)
					{
						base.npc.velocity *= 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = 1f;
						base.npc.netUpdate = true;
						return;
					}
				}
			}
		}

		private void DespawnHandler()
		{
			Player player = Main.player[base.npc.target];
			if (!player.active || player.dead)
			{
				base.npc.velocity *= 0.96f;
				NPC npc = base.npc;
				npc.velocity.Y = npc.velocity.Y - 1f;
				if (base.npc.timeLeft > 10)
				{
					base.npc.timeLeft = 10;
				}
				return;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/VCleaver/Wielder_Glow");
			Texture2D boosterAni = base.mod.GetTexture("NPCs/Bosses/VCleaver/Wielder_Booster");
			Texture2D boosterGlow = base.mod.GetTexture("NPCs/Bosses/VCleaver/Wielder_Booster_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			int num214 = boosterAni.Height / 4;
			int y6 = num214 * this.boosterFrame;
			Main.spriteBatch.Draw(boosterAni, base.npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, boosterAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)boosterAni.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
			Main.spriteBatch.Draw(boosterGlow, base.npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, boosterAni.Width, num214)), RedeColor.COLOR_WHITEFADE2, base.npc.rotation, new Vector2((float)boosterAni.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), RedeColor.COLOR_WHITEFADE2, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		private float opacity
		{
			get
			{
				return base.npc.ai[2];
			}
			set
			{
				base.npc.ai[2] = value;
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D flare = base.mod.GetTexture("ExtraTextures/RedEyeFlare");
			Rectangle rect = new Rectangle(0, 0, flare.Width, flare.Height);
			Vector2 origin = new Vector2((float)(flare.Width / 2), (float)(flare.Height / 2));
			Vector2 position = base.npc.Center - Main.screenPosition + new Vector2((float)((base.npc.spriteDirection == 1) ? 5 : -5), -10f);
			Color colour = Color.Lerp(Color.Red, Color.White, 1f / this.opacity * 10f) * (1f / this.opacity * 10f);
			if (base.npc.ai[0] == 2f && base.npc.ai[3] == 1f && base.npc.ai[2] < 60f)
			{
				spriteBatch.Draw(flare, position, new Rectangle?(rect), colour, base.npc.rotation, origin, 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(flare, position, new Rectangle?(rect), colour * 0.4f, base.npc.rotation, origin, 1f, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
		}

		public int aniType;

		public int boosterFrame;

		public int frameCounters;

		public int cooldown;

		public bool barrierSpawn;

		public List<int> AttackList = new List<int>
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7
		};

		public List<int> CopyList;
	}
}
