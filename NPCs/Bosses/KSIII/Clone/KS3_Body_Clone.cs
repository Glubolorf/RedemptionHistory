using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Items;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.HM;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII.Clone
{
	[AutoloadBossHead]
	public class KS3_Body_Clone : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/KSIII/KS3_Body";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III... ?");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 42000;
			base.npc.defense = 35;
			base.npc.damage = 120;
			base.npc.width = 42;
			base.npc.height = 106;
			base.npc.aiStyle = -1;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 10, 0, 0);
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.netAlways = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[ModContent.BuffType<UltraFlameDebuff>()] = true;
			this.bossBag = ModContent.ItemType<SlayerBag>();
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.velocity.Length() >= 13f && base.npc.ai[0] != 5f;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 65; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.8f;
				}
				for (int j = 0; j < 35; j++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex2].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A King Slayer III Clone";
			potionType = 499;
			RedeWorld.downedSlayer = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SlayerTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KingSlayerMask>(), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SlayerFlamethrower>(), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SlayerNanogun>(), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SlayerFist>(), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SlayerGun>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CyberPlating>(), Main.rand.Next(8, 12), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KingCore>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SlayerMedal>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Holokey>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<StarcruiserRadar>(), 1, false, 0, false, false);
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.6;
			return true;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.chance);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.chance = reader.ReadFloat();
			}
		}

		public override void AI()
		{
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (base.npc.velocity.Length() < 13f && base.npc.ai[3] <= 11f)
			{
				this.chargeCounter = 0;
				this.chargeFrame = 0;
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 160;
					if (base.npc.frame.Y > 480)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
			}
			else if (base.npc.ai[0] != 5f)
			{
				this.chargeCounter++;
				if (this.chargeCounter >= 5)
				{
					this.chargeFrame++;
					this.chargeCounter = 0;
				}
				if (this.chargeFrame >= 6)
				{
					this.chargeFrame = 2;
				}
			}
			float obj = base.npc.ai[3];
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
								if (!5f.Equals(obj))
								{
									if (!6f.Equals(obj))
									{
										if (!7f.Equals(obj))
										{
											if (!8f.Equals(obj))
											{
												if (!9f.Equals(obj))
												{
													if (!10f.Equals(obj))
													{
														if (!12f.Equals(obj))
														{
															if (!13f.Equals(obj))
															{
																if (!14f.Equals(obj))
																{
																	if (!15f.Equals(obj))
																	{
																		if (!16f.Equals(obj))
																		{
																			if (!17f.Equals(obj))
																			{
																				if (!18f.Equals(obj))
																				{
																					if (!19f.Equals(obj))
																					{
																						if (!20f.Equals(obj))
																						{
																							if (21f.Equals(obj))
																							{
																								this.frameCounters++;
																								if (this.frameCounters >= 5)
																								{
																									this.fightFrames++;
																									this.frameCounters = 0;
																								}
																								if (this.fightFrames >= 6)
																								{
																									this.fightFrames = 2;
																								}
																							}
																						}
																						else
																						{
																							if (this.fightFrames < 3)
																							{
																								this.fightFrames = 3;
																							}
																							this.frameCounters++;
																							if (this.frameCounters >= 5)
																							{
																								this.fightFrames++;
																								this.frameCounters = 0;
																							}
																							if (this.fightFrames >= 6)
																							{
																								this.fightFrames = 0;
																								base.npc.ai[3] = 11f;
																							}
																						}
																					}
																					else
																					{
																						this.frameCounters++;
																						if (this.frameCounters >= 5)
																						{
																							this.fightFrames++;
																							this.frameCounters = 0;
																						}
																						if (this.fightFrames >= 3)
																						{
																							this.fightFrames = 0;
																							base.npc.ai[3] = 11f;
																						}
																					}
																				}
																				else
																				{
																					this.fightFrames = 4;
																					this.frameCounters = 0;
																					Vector2 position = base.npc.Center + Vector2.Normalize(base.npc.velocity) * 30f;
																					Dust dust5 = Main.dust[Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 92, 0f, 0f, 0, default(Color), 2f)];
																					dust5.position = position;
																					dust5.velocity = Utils.RotatedBy(base.npc.velocity, 1.5708, default(Vector2)) * 0.33f + base.npc.velocity / 4f;
																					dust5.position += Utils.RotatedBy(base.npc.velocity, 1.5708, default(Vector2));
																					dust5.fadeIn = 0.5f;
																					dust5.noGravity = true;
																					Dust dust6 = Main.dust[Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 92, 0f, 0f, 0, default(Color), 2f)];
																					dust6.position = position;
																					dust6.velocity = Utils.RotatedBy(base.npc.velocity, -1.5708, default(Vector2)) * 0.33f + base.npc.velocity / 4f;
																					dust6.position += Utils.RotatedBy(base.npc.velocity, -1.5708, default(Vector2));
																					dust6.fadeIn = 0.5f;
																					dust6.noGravity = true;
																				}
																			}
																			else
																			{
																				this.frameCounters++;
																				if (this.frameCounters >= 5)
																				{
																					this.fightFrames++;
																					this.frameCounters = 0;
																				}
																				if (this.fightFrames >= 4)
																				{
																					this.fightFrames = 0;
																				}
																			}
																		}
																		else
																		{
																			this.frameCounters++;
																			if (this.frameCounters >= 5)
																			{
																				this.fightFrames--;
																				this.frameCounters = 0;
																			}
																			if (this.fightFrames < 0)
																			{
																				this.fightFrames = 0;
																				base.npc.ai[3] = 11f;
																			}
																		}
																	}
																	else
																	{
																		this.frameCounters++;
																		if (this.frameCounters >= 5)
																		{
																			this.fightFrames++;
																			this.frameCounters = 0;
																		}
																		if (this.fightFrames >= 6)
																		{
																			this.fightFrames = 2;
																		}
																	}
																}
																else
																{
																	this.frameCounters++;
																	if (this.frameCounters >= 5)
																	{
																		this.fightFrames++;
																		this.frameCounters = 0;
																	}
																	if (this.fightFrames >= 8)
																	{
																		this.fightFrames = 0;
																		base.npc.ai[3] = 11f;
																	}
																}
															}
															else
															{
																this.frameCounters = 0;
																this.fightFrames = 5;
															}
														}
														else
														{
															this.frameCounters++;
															if (this.frameCounters >= 5)
															{
																this.fightFrames++;
																this.frameCounters = 0;
															}
															if (this.fightFrames >= 5)
															{
																this.fightFrames = 4;
															}
														}
													}
													else
													{
														this.frameCounters++;
														if (this.frameCounters >= 5)
														{
															this.armFrames[7]--;
															this.frameCounters = 0;
														}
														if (this.armFrames[7] <= 0)
														{
															this.armFrames[7] = 0;
															base.npc.ai[3] = 0f;
														}
													}
												}
												else
												{
													this.frameCounters++;
													if (this.frameCounters >= 5)
													{
														this.armFrames[7]++;
														this.frameCounters = 0;
													}
													if (this.armFrames[7] >= 5)
													{
														this.armFrames[7] = 3;
													}
												}
											}
											else
											{
												this.frameCounters++;
												if (this.frameCounters >= 5)
												{
													this.armFrames[6]++;
													this.frameCounters = 0;
												}
												if (this.armFrames[6] >= 7)
												{
													this.armFrames[6] = 0;
													base.npc.ai[3] = 0f;
												}
											}
										}
										else
										{
											this.frameCounters++;
											if (this.frameCounters >= 5)
											{
												this.armFrames[5]++;
												this.frameCounters = 0;
											}
											if (this.armFrames[5] >= 5)
											{
												this.armFrames[5] = 2;
											}
										}
									}
									else
									{
										this.frameCounters++;
										if (this.frameCounters >= 5)
										{
											this.armFrames[4]++;
											this.frameCounters = 0;
										}
										if (this.armFrames[4] >= 6)
										{
											this.armFrames[4] = 0;
											base.npc.ai[3] = 0f;
										}
									}
								}
								else
								{
									this.frameCounters++;
									if (this.frameCounters >= 5)
									{
										this.armFrames[3]++;
										this.frameCounters = 0;
									}
									if (this.armFrames[3] >= 8)
									{
										this.armFrames[3] = 0;
										base.npc.ai[3] = 0f;
									}
								}
							}
							else
							{
								this.frameCounters++;
								if (this.frameCounters >= 5)
								{
									this.armFrames[2]--;
									this.frameCounters = 0;
								}
								if (this.armFrames[2] <= 0)
								{
									this.armFrames[2] = 0;
									base.npc.ai[3] = 0f;
								}
							}
						}
						else
						{
							this.frameCounters++;
							if (this.frameCounters >= 5)
							{
								this.armFrames[2]++;
								this.frameCounters = 0;
							}
							if (this.armFrames[2] >= 9)
							{
								this.armFrames[2] = 6;
								base.npc.ai[3] = 2f;
							}
						}
					}
					else
					{
						this.frameCounters++;
						if (this.frameCounters >= 5)
						{
							this.armFrames[2]++;
							this.frameCounters = 0;
						}
						if (this.armFrames[2] >= 7)
						{
							this.armFrames[2] = 6;
						}
					}
				}
				else
				{
					this.frameCounters++;
					if (this.frameCounters >= 5)
					{
						this.armFrames[1]++;
						this.frameCounters = 0;
					}
					if (this.armFrames[1] >= 4)
					{
						this.armFrames[1] = 0;
					}
				}
			}
			else
			{
				this.frameCounters++;
				if (this.frameCounters >= 5)
				{
					this.armFrames[0]++;
					this.frameCounters = 0;
				}
				if (this.armFrames[0] >= 4)
				{
					this.armFrames[0] = 0;
				}
			}
			this.DespawnHandler();
			if (this.chance > 1f)
			{
				this.chance = 1f;
			}
			if ((base.npc.ai[0] > 1f && base.npc.ai[0] < 6f) || base.npc.ai[0] == 10f)
			{
				base.npc.dontTakeDamage = false;
			}
			else
			{
				base.npc.dontTakeDamage = true;
			}
			switch ((int)base.npc.ai[0])
			{
			case 0:
				base.npc.LookAtPlayer();
				base.npc.ai[3] = 1f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 1f)
				{
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int j = 0; j < 30; j++)
					{
						int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 92, 0f, 0f, 100, default(Color), 3f);
						Main.dust[dustIndex].velocity *= 6f;
						Main.dust[dustIndex].noGravity = true;
					}
				}
				player.GetModPlayer<ScreenPlayer>().Rumble(5, 5);
				base.npc.netUpdate = true;
				if (base.npc.ai[2] > 5f)
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 1f;
					base.npc.netUpdate = true;
				}
				break;
			case 1:
				base.npc.LookAtPlayer();
				this.gunRot = ((base.npc.spriteDirection == 1) ? 0f : 3.1415927f);
				base.npc.ai[2] += 1f;
				if (RedeConfigClient.Instance.NoLoreElements)
				{
					if (base.npc.ai[2] == 60f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 160f)
					{
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-400, -300) : Main.rand.Next(300, 400)), (float)Main.rand.Next(-60, 60));
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShieldC>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = 3f;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (base.npc.ai[2] == 60f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("SCANNING TARGET...", 160, 1, 0.6f, "King Slayer III Clone:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 220f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("TARGET DEEMED: 'A WASTE OF TIME'", 180, 1, 0.6f, "King Slayer III Clone:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 400f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("RELAYING MESSAGE: 'KING SLAYER NO LONGER HAS TIME FOR YOU'", 220, 1, 0.6f, "King Slayer III Clone:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 420f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 500f)
					{
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-400, -300) : Main.rand.Next(300, 400)), (float)Main.rand.Next(-60, 60));
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShieldC>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = 3f;
						base.npc.netUpdate = true;
					}
				}
				break;
			case 3:
				base.npc.LookAtPlayer();
				if (base.npc.ai[1] == 0f)
				{
					base.npc.ai[1] = (float)Main.rand.Next(1, 6);
					this.chance = Utils.NextFloat(Main.rand, 0.5f, 1f);
					base.npc.netUpdate = true;
				}
				base.npc.rotation = base.npc.velocity.X * 0.01f;
				switch ((int)base.npc.ai[1])
				{
				case -1:
					if (RedeHelper.Chance(this.chance) && base.npc.ai[2] == 0f)
					{
						base.npc.ai[1] = (float)Main.rand.Next(1, 6);
						base.npc.netUpdate = true;
					}
					else
					{
						this.gunRot = ((base.npc.spriteDirection == 1) ? 0f : 3.1415927f);
						if (base.npc.ai[2] == 0f)
						{
							if (Main.rand.Next(2) == 0)
							{
								base.npc.ai[2] = 1f;
							}
							else
							{
								base.npc.ai[2] = 2f;
							}
							base.npc.netUpdate = true;
						}
						base.npc.velocity *= 0.9f;
						if (base.npc.ai[2] == 1f)
						{
							if (base.npc.ai[3] == 2f)
							{
								base.npc.ai[3] = 4f;
							}
							if (base.npc.ai[3] == 0f && base.npc.velocity.Length() < 1f)
							{
								base.npc.ai[2] = 0f;
								base.npc.ai[0] = 4f;
								base.npc.ai[1] = 0f;
								base.npc.netUpdate = true;
							}
						}
						else if (base.npc.ai[2] == 2f)
						{
							if (base.npc.ai[3] == 2f)
							{
								base.npc.ai[3] = 4f;
							}
							if (base.npc.ai[3] == 0f && base.npc.velocity.Length() < 1f)
							{
								base.npc.ai[3] = 11f;
								base.npc.ai[2] = 0f;
								base.npc.ai[0] = 5f;
								base.npc.ai[1] = 0f;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				case 1:
					ref this.gunRot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), 0.05235988f);
					this.SnapGunToFiringArea();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 20f == 0f)
					{
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-400, -300) : Main.rand.Next(300, 400)), (float)Main.rand.Next(-60, 60));
					}
					base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 12f), 14f, true);
					if (base.npc.ai[3] < 2f || base.npc.ai[3] > 4f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] % 20f == 0f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 462, 72, RedeHelper.PolarVector(7f, this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun1", 0f, 0f);
						base.npc.ai[3] = 3f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] % 100f == 0f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector(8f, this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector(8f, this.gunRot + MathHelper.ToRadians(25f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector(8f, this.gunRot - MathHelper.ToRadians(25f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector(8f, this.gunRot + MathHelper.ToRadians(50f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector(8f, this.gunRot - MathHelper.ToRadians(50f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
						base.npc.ai[3] = 3f;
					}
					if (base.npc.ai[2] > 310f)
					{
						this.chance -= Utils.NextFloat(Main.rand, 0.1f, 0.5f);
						base.npc.ai[2] = 0f;
						base.npc.ai[1] = -1f;
						base.npc.netUpdate = true;
					}
					break;
				case 2:
					if (base.npc.ai[2] < 245f)
					{
						ref this.gunRot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(player.Center + player.velocity * 20f)), 0.05235988f);
					}
					this.SnapGunToFiringArea();
					base.npc.ai[2] += 1f;
					this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -300 : 300), 10f);
					if (base.npc.ai[3] < 2f || base.npc.ai[3] > 4f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] < 200f)
					{
						if (base.npc.Distance(this.ShootPos) < 100f || base.npc.ai[2] > 40f)
						{
							base.npc.ai[2] = 200f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSTeleLine1>(), 0, RedeHelper.PolarVector(10f, this.gunRot), false, SoundID.Item1.WithVolume(0f), "", 0f, (float)base.npc.whoAmI);
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 13f), 14f, true);
						}
					}
					else
					{
						if (base.npc.ai[2] < 260f)
						{
							for (int k = 0; k < 3; k++)
							{
								double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
								this.vector.X = (float)(Math.Sin(angle) * 40.0);
								this.vector.Y = (float)(Math.Cos(angle) * 40.0);
								Dust dust2 = Main.dust[Dust.NewDust(base.npc.Center + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f) + this.vector, 2, 2, 92, 0f, 0f, 100, default(Color), 2f)];
								dust2.noGravity = true;
								dust2.velocity = -base.npc.DirectionTo(dust2.position) * 10f;
							}
						}
						base.npc.velocity *= 0.96f;
						if (base.npc.ai[2] == 260f)
						{
							base.npc.velocity.X = (float)((player.Center.X > base.npc.Center.X) ? -9 : 9);
							for (int l = 0; l < Main.rand.Next(5, 8); l++)
							{
								base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 462, 72, RedeHelper.PolarVector((float)Main.rand.Next(7, 11), this.gunRot + Utils.NextFloat(Main.rand, -0.14f, 0.14f)), true, SoundID.Item1, "Sounds/Custom/ShotgunBlast1", 0f, 0f);
							}
							base.npc.ai[3] = 3f;
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 300f)
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.3f);
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 3:
					ref this.gunRot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), 0.05235988f);
					this.SnapGunToFiringArea();
					base.npc.ai[2] += 1f;
					this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -450 : 450), -10f);
					base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 12f), 14f, true);
					if (base.npc.ai[3] < 2f || base.npc.ai[3] > 4f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] == 40f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), ModContent.ProjectileType<ReboundShot>(), 72, RedeHelper.PolarVector(15f, this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun2", 0f, 0f);
						base.npc.ai[3] = 3f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] > 60f)
					{
						this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.1f);
						base.npc.ai[2] = 0f;
						base.npc.ai[1] = -1f;
						base.npc.netUpdate = true;
					}
					break;
				case 4:
					ref this.gunRot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), 0.05235988f);
					this.SnapGunToFiringArea();
					base.npc.ai[2] += 1f;
					this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -450 : 450), -10f);
					base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 12f), 14f, true);
					if (base.npc.ai[3] < 2f || base.npc.ai[3] > 4f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] == 41f || base.npc.ai[2] == 44f || base.npc.ai[2] == 47f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), ModContent.ProjectileType<ReboundShot>(), 72, RedeHelper.PolarVector(15f, this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun2", 0f, 0f);
						base.npc.ai[3] = 3f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] > 61f)
					{
						this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.1f);
						base.npc.ai[2] = 0f;
						base.npc.ai[1] = -1f;
						base.npc.netUpdate = true;
					}
					break;
				case 5:
					ref this.gunRot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), 0.05235988f);
					this.SnapGunToFiringArea();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 20f == 0f)
					{
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-400, -300) : Main.rand.Next(300, 400)), (float)Main.rand.Next(-60, 60));
					}
					base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 12f), 14f, true);
					if (base.npc.ai[3] < 2f || base.npc.ai[3] > 4f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] % 10f == 0f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 462, 72, RedeHelper.PolarVector(6f, this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun1", 0f, 0f);
						base.npc.ai[3] = 3f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] > 61f)
					{
						this.chance -= Utils.NextFloat(Main.rand, 0.02f, 0.2f);
						base.npc.ai[2] = 0f;
						base.npc.ai[1] = -1f;
						base.npc.netUpdate = true;
					}
					break;
				}
				break;
			case 4:
				base.npc.LookAtPlayer();
				if (base.npc.ai[1] == 0f)
				{
					base.npc.ai[1] = (float)Main.rand.Next(1, 9);
					this.chance = Utils.NextFloat(Main.rand, 0.5f, 1f);
					base.npc.netUpdate = true;
				}
				base.npc.rotation = base.npc.velocity.X * 0.01f;
				switch ((int)base.npc.ai[1])
				{
				case -1:
					if (RedeHelper.Chance(this.chance) && base.npc.ai[2] == 0f)
					{
						base.npc.ai[1] = (float)Main.rand.Next(1, 9);
						base.npc.netUpdate = true;
					}
					else
					{
						if (base.npc.ai[2] == 0f)
						{
							if (Main.rand.Next(2) == 0)
							{
								base.npc.ai[2] = 1f;
							}
							else
							{
								base.npc.ai[2] = 2f;
							}
							base.npc.netUpdate = true;
						}
						base.npc.velocity *= 0.9f;
						if (base.npc.ai[2] == 1f)
						{
							if (base.npc.ai[3] == 0f)
							{
								base.npc.ai[3] = 11f;
							}
							if (base.npc.ai[3] == 11f && base.npc.velocity.Length() < 1f)
							{
								base.npc.ai[2] = 0f;
								base.npc.ai[0] = 5f;
								base.npc.ai[1] = 0f;
								base.npc.netUpdate = true;
							}
						}
						else if (base.npc.ai[2] == 2f)
						{
							if (base.npc.ai[3] == 0f)
							{
								base.npc.ai[3] = 2f;
							}
							if (base.npc.ai[3] == 2f && base.npc.velocity.Length() < 1f)
							{
								base.npc.ai[2] = 0f;
								base.npc.ai[0] = 3f;
								base.npc.ai[1] = 0f;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				case 1:
					base.npc.ai[2] += 1f;
					this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -300 : 300), -60f);
					if (base.npc.ai[2] < 100f)
					{
						if (base.npc.Distance(this.ShootPos) < 160f || base.npc.ai[2] > 40f)
						{
							base.npc.ai[2] = 100f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 17f), 14f, true);
						}
					}
					else
					{
						base.npc.velocity *= 0.96f;
						if (base.npc.ai[2] == 120f)
						{
							base.npc.ai[3] = 5f;
						}
						if (base.npc.ai[2] == 135f)
						{
							base.npc.Shoot(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 15f) : (base.npc.Center.X - 15f), base.npc.Center.Y - 11f), ModContent.ProjectileType<KSFist>(), 102, new Vector2((float)((base.npc.spriteDirection == 1) ? 10 : -10), 0f), true, SoundID.Item1, "Sounds/Custom/MissileFire1", 0f, 0f);
						}
						if (base.npc.ai[2] > 170f)
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.03f, 0.1f);
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 2:
					if (base.npc.ai[2] == 0f)
					{
						if (Main.rand.Next(3) == 0)
						{
							base.npc.ai[2] = 1f;
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(1, 9);
							base.npc.ai[2] = 0f;
						}
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.ai[2] += 1f;
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -200 : 200), 0f);
						if (base.npc.ai[2] < 100f)
						{
							if (base.npc.Distance(this.ShootPos) < 160f || base.npc.ai[2] > 50f)
							{
								base.npc.ai[2] = 100f;
								base.npc.netUpdate = true;
							}
							else
							{
								base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 18f), 14f, true);
							}
						}
						else
						{
							base.npc.velocity *= 0.9f;
							if (base.npc.ai[2] == 120f)
							{
								base.npc.ai[3] = 6f;
							}
							if (base.npc.ai[2] == 140f)
							{
								base.npc.Shoot(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 21f) : (base.npc.Center.X - 21f), base.npc.Center.Y - 17f), ModContent.ProjectileType<KS3_FlashGrenadeProj>(), 78, new Vector2((float)((base.npc.spriteDirection == 1) ? 10 : -10), -6f), false, SoundID.Item1, "", 0f, 0f);
							}
							if (base.npc.ai[2] > 180f)
							{
								this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.8f);
								base.npc.ai[2] = 0f;
								base.npc.ai[1] = -1f;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				case 3:
					if (base.npc.ai[2] == 0f)
					{
						if (Main.rand.Next(2) == 0)
						{
							base.npc.ai[2] = 1f;
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(1, 9);
							base.npc.ai[2] = 0f;
						}
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.ai[2] += 1f;
						base.npc.ai[3] = 7f;
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -320 : 320), 0f);
						if (base.npc.ai[2] < 100f)
						{
							if (base.npc.Distance(this.ShootPos) < 160f || base.npc.ai[2] > 60f)
							{
								base.npc.ai[2] = 100f;
								base.npc.netUpdate = true;
							}
							else
							{
								for (int m = 0; m < 3; m++)
								{
									double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
									this.vector.X = (float)(Math.Sin(angle2) * 100.0);
									this.vector.Y = (float)(Math.Cos(angle2) * 100.0);
									Dust dust3 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 92, 0f, 0f, 100, default(Color), 1f)];
									dust3.noGravity = true;
									dust3.velocity = -base.npc.DirectionTo(dust3.position) * 5f;
								}
								base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 8f), 14f, true);
							}
						}
						else
						{
							base.npc.Move(this.ShootPos, 4f, 14f, true);
							if (base.npc.ai[2] == 101f)
							{
								base.npc.Shoot(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 2f) : (base.npc.Center.X - 2f), base.npc.Center.Y - 16f), ModContent.ProjectileType<KSBeamCellC>(), 96, new Vector2((float)((base.npc.spriteDirection == 1) ? 10 : -10), 0f), false, SoundID.Item103, "", (float)base.npc.whoAmI, 0f);
							}
							if (base.npc.ai[2] > 240f)
							{
								base.npc.ai[3] = 0f;
								this.chance -= Utils.NextFloat(Main.rand, 0.5f, 1f);
								base.npc.ai[2] = 0f;
								base.npc.ai[1] = -1f;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				case 4:
					if (base.npc.ai[2] == 0f)
					{
						if (RedeHelper.Chance(0.75f) && base.npc.Distance(player.Center) < 300f)
						{
							base.npc.ai[2] = 1f;
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(1, 9);
							base.npc.ai[2] = 0f;
						}
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.ai[2] += 1f;
						base.npc.ai[3] = 7f;
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -80 : 80), 0f);
						if (base.npc.ai[2] < 200f)
						{
							if (base.npc.Distance(this.ShootPos) < 160f || base.npc.ai[2] > 120f)
							{
								base.npc.ai[2] = 200f;
								base.npc.netUpdate = true;
							}
							else
							{
								for (int n = 0; n < 3; n++)
								{
									double angle3 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
									this.vector.X = (float)(Math.Sin(angle3) * 100.0);
									this.vector.Y = (float)(Math.Cos(angle3) * 100.0);
									Dust dust4 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 92, 0f, 0f, 100, default(Color), 1f)];
									dust4.noGravity = true;
									dust4.velocity = -base.npc.DirectionTo(dust4.position) * 5f;
								}
								base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 7f), 14f, true);
							}
						}
						else
						{
							base.npc.velocity *= 0.9f;
							if (base.npc.ai[2] == 202f)
							{
								base.npc.Shoot(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 2f) : (base.npc.Center.X - 2f), base.npc.Center.Y - 16f), ModContent.ProjectileType<KSSurgeC>(), 80, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Zap1", (float)base.npc.whoAmI, 0f);
								for (int m2 = 0; m2 < 16; m2++)
								{
									int projID = Projectile.NewProjectile(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 2f) : (base.npc.Center.X - 2f), base.npc.Center.Y - 16f), Vector2.Zero, ModContent.ProjectileType<KSSurge2>(), 0, 0f, Main.myPlayer, 0f, 0f);
									Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)m2 / 16f * 6.28f);
									Main.projectile[projID].netUpdate = true;
								}
							}
							if (base.npc.ai[2] > 232f)
							{
								base.npc.ai[3] = 0f;
								this.chance -= Utils.NextFloat(Main.rand, 0.4f, 0.7f);
								base.npc.ai[2] = 0f;
								base.npc.ai[1] = -1f;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				case 5:
					if (base.npc.ai[2] == 0f)
					{
						if (base.npc.NPCHasAnyBuff())
						{
							base.npc.ai[2] = 1f;
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(1, 9);
							base.npc.ai[2] = 0f;
						}
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.ai[2] += 1f;
						base.npc.ai[3] = 8f;
						base.npc.velocity *= 0.98f;
						if (base.npc.ai[2] == 21f)
						{
							for (int k2 = 0; k2 < base.npc.buffImmune.Length; k2++)
							{
								base.npc.buffImmune[k2] = true;
							}
							if (Main.netMode != 1)
							{
								for (int i2 = 0; i2 < base.npc.buffTime.Length; i2++)
								{
									base.npc.buffTime[i2] = 0;
									base.npc.buffType[i2] = 0;
								}
								if (Main.netMode == 2)
								{
									NetMessage.SendData(54, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
								}
							}
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 31f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 6:
					if (base.npc.ai[2] == 0f)
					{
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShieldC>()) && (player.HeldItem.magic || player.HeldItem.ranged) && Main.rand.Next(4) == 0)
						{
							base.npc.ai[2] = 1f;
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(1, 9);
							base.npc.ai[2] = 0f;
						}
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] % 20f == 0f)
						{
							this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-400, -300) : Main.rand.Next(300, 400)), (float)Main.rand.Next(-60, 60));
						}
						base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 12f), 14f, true);
						if (base.npc.ai[2] == 16f)
						{
							base.npc.Shoot(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 48f) : (base.npc.Center.X - 48f), base.npc.Center.Y - 12f), ModContent.ProjectileType<KSReflectC>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] > 231f)
						{
							base.npc.ai[3] = 10f;
						}
						else
						{
							base.npc.ai[3] = 9f;
						}
						if (base.npc.ai[2] > 261f)
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.2f, 1f);
							base.npc.ai[3] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 7:
					if (base.npc.ai[2] == 0f)
					{
						if (!NPC.AnyNPCs(ModContent.NPCType<KSMissileDroneC>()) && Main.rand.Next(4) == 0)
						{
							base.npc.ai[2] = 1f;
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(1, 9);
							base.npc.ai[2] = 0f;
						}
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.ai[2] += 1f;
						base.npc.velocity *= 0.98f;
						if (base.npc.ai[2] == 16f)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_CallC>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
							if (!NPC.AnyNPCs(ModContent.NPCType<KSMissileDroneC>()))
							{
								for (int i3 = 0; i3 < Main.rand.Next(2, 5); i3++)
								{
									base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(750, 800), ModContent.NPCType<KSMissileDroneC>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
								}
							}
						}
						if (base.npc.ai[2] > 91f)
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.2f, 1f);
							base.npc.ai[3] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 8:
					if (base.npc.ai[2] == 0f)
					{
						if (!NPC.AnyNPCs(ModContent.NPCType<KS3MagnetC>()) && Main.rand.Next(4) == 0)
						{
							base.npc.ai[2] = 1f;
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(1, 9);
							base.npc.ai[2] = 0f;
						}
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.ai[2] += 1f;
						base.npc.velocity *= 0.98f;
						if (base.npc.ai[2] == 16f)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_CallC>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
							if (!NPC.AnyNPCs(ModContent.NPCType<KS3MagnetC>()))
							{
								for (int i4 = 0; i4 < 2; i4++)
								{
									base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(750, 800), ModContent.NPCType<KS3MagnetC>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
								}
							}
						}
						if (base.npc.ai[2] > 91f)
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.7f, 1f);
							base.npc.ai[3] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				}
				break;
			case 5:
				if (base.npc.ai[1] == 0f)
				{
					base.npc.ai[1] = (float)Main.rand.Next(1, 6);
					this.chance = Utils.NextFloat(Main.rand, 0.5f, 1f);
					base.npc.netUpdate = true;
				}
				switch ((int)base.npc.ai[1])
				{
				case -1:
					base.npc.LookAtPlayer();
					if (RedeHelper.Chance(this.chance) && base.npc.ai[2] == 0f)
					{
						base.npc.ai[1] = (float)Main.rand.Next(1, 6);
						base.npc.netUpdate = true;
					}
					else
					{
						if (base.npc.ai[2] == 0f)
						{
							if (Main.rand.Next(2) == 0)
							{
								base.npc.ai[2] = 1f;
							}
							else
							{
								base.npc.ai[2] = 2f;
							}
							base.npc.netUpdate = true;
						}
						base.npc.velocity *= 0.9f;
						if (base.npc.ai[2] == 1f)
						{
							if (base.npc.ai[3] == 11f)
							{
								base.npc.ai[3] = 0f;
							}
							if (base.npc.ai[3] == 0f && base.npc.velocity.Length() < 1f)
							{
								base.npc.ai[2] = 0f;
								base.npc.ai[0] = 4f;
								base.npc.ai[1] = 0f;
								base.npc.netUpdate = true;
							}
						}
						else if (base.npc.ai[2] == 2f)
						{
							if (base.npc.ai[3] == 11f)
							{
								base.npc.ai[3] = 2f;
							}
							if (base.npc.ai[3] == 2f && base.npc.velocity.Length() < 1f)
							{
								base.npc.ai[2] = 0f;
								base.npc.ai[0] = 3f;
								base.npc.ai[1] = 0f;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				case 1:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] <= 40f)
					{
						base.npc.rotation = base.npc.velocity.X * 0.01f;
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -200 : 200), -60f);
						base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 800f) ? 20f : 17f, 5f, true);
					}
					if (base.npc.ai[2] == 40f)
					{
						base.npc.ai[3] = 12f;
					}
					if (base.npc.ai[2] > 40f && base.npc.ai[2] < 100f)
					{
						if (base.npc.ai[2] % 15f == 0f)
						{
							Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						base.npc.rotation += base.npc.velocity.Y / 30f;
						this.ShootPos = new Vector2(player.velocity.X * 30f, -600f);
						if (base.npc.Center.Y < player.Center.Y - 600f || base.npc.ai[2] > 80f)
						{
							base.npc.ai[3] = 13f;
							base.npc.velocity *= 0.2f;
							base.npc.ai[2] = 100f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 800f) ? 20f : 17f, 3f, true);
						}
					}
					else if (base.npc.ai[2] >= 100f && base.npc.ai[2] < 200f)
					{
						if (base.npc.ai[2] >= 100f && base.npc.ai[2] < 110f)
						{
							base.npc.rotation = 0f;
							NPC npc2 = base.npc;
							npc2.velocity.Y = npc2.velocity.Y - 0.01f;
						}
						if (base.npc.ai[2] == 110f)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSHitboxC>(), 120, Vector2.Zero, false, SoundID.Item74, "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSWaveC>(), 120, Vector2.Zero, false, SoundID.Item74, "", (float)base.npc.whoAmI, 0f);
							NPC npc3 = base.npc;
							npc3.velocity.Y = npc3.velocity.Y + 40f;
						}
						if (base.npc.ai[2] > 130f || base.npc.Center.Y > player.Center.Y + 400f)
						{
							base.npc.ai[2] = 200f;
							base.npc.ai[3] = 14f;
							base.npc.netUpdate = true;
						}
					}
					else if (base.npc.ai[2] >= 200f)
					{
						base.npc.rotation = base.npc.velocity.X * 0.01f;
						if (base.npc.ai[2] > 220f)
						{
							this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -200 : 200), -60f);
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 800f) ? 30f : 22f, 8f, true);
						}
						else
						{
							base.npc.velocity *= 0.8f;
						}
					}
					if (base.npc.ai[2] > 280f)
					{
						base.npc.rotation = 0f;
						this.chance -= Utils.NextFloat(Main.rand, 0.1f, 0.3f);
						base.npc.ai[2] = 0f;
						base.npc.ai[1] = -1f;
						base.npc.netUpdate = true;
					}
					break;
				case 2:
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 100f)
					{
						base.npc.rotation = base.npc.velocity.X * 0.01f;
						base.npc.LookAtPlayer();
						if (base.npc.Distance(this.ShootPos) < 50f || base.npc.ai[2] > 40f)
						{
							base.npc.ai[2] = 100f;
							base.npc.ai[3] = 15f;
							base.npc.netUpdate = true;
						}
						else
						{
							this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -100 : 100), 0f);
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 800f) ? 20f : 17f, 5f, true);
						}
					}
					else if (base.npc.ai[2] >= 100f)
					{
						base.npc.rotation = 0f;
						base.npc.velocity *= 0.8f;
						if (base.npc.ai[2] == 101f)
						{
							base.npc.velocity.X = (float)((player.Center.X > base.npc.Center.X) ? -6 : 6);
						}
						if (base.npc.ai[2] == 110f)
						{
							base.npc.Dash(60, false, SoundID.Item74, player.Center);
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSHitboxBashC>(), 120, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] == 130f)
						{
							base.npc.velocity.X = (float)((base.npc.spriteDirection == 1) ? -15 : 15);
							base.npc.ai[3] = 16f;
						}
						if (base.npc.ai[2] > 160f)
						{
							base.npc.LookAtPlayer();
							this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.2f);
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 3:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 100f)
					{
						base.npc.rotation = 0f;
						base.npc.LookAtPlayer();
						if (base.npc.Distance(this.ShootPos) < 100f || base.npc.ai[2] > 50f)
						{
							this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -150 : 150), 200f);
							base.npc.ai[2] = 100f;
							base.npc.velocity.X = 0f;
							base.npc.velocity.Y = -25f;
							base.npc.ai[3] = 17f;
							Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSTeleLine2>(), 0, base.npc.DirectionTo(player.Center + player.velocity * 20f), false, SoundID.Item1.WithVolume(0f), "", 0f, (float)base.npc.whoAmI);
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 800f) ? 20f : 17f, 5f, true);
						}
					}
					else if (base.npc.ai[2] >= 100f && base.npc.ai[2] < 200f)
					{
						base.npc.velocity *= 0.97f;
						if (base.npc.velocity.Length() < 6f || base.npc.ai[2] > 160f)
						{
							base.npc.ai[2] = 200f;
							base.npc.velocity *= 0f;
							base.npc.ai[3] = 18f;
							base.npc.rotation = Utils.ToRotation(player.Center + player.velocity * 20f - base.npc.Center) + -1.5707964f;
							base.npc.netUpdate = true;
						}
					}
					else if (base.npc.ai[2] >= 200f)
					{
						if (base.npc.ai[2] == 204f)
						{
							base.npc.rotation = Utils.ToRotation(player.Center + player.velocity * 20f - base.npc.Center) + -1.5707964f;
						}
						if (base.npc.ai[2] == 205f)
						{
							base.npc.Dash(40, true, SoundID.Item74, player.Center + player.velocity * 20f);
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSHitboxDropkickC>(), 156, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] > 260f || base.npc.Center.Y > player.Center.Y + 400f)
						{
							base.npc.rotation = 0f;
							base.npc.velocity *= 0f;
							this.chance -= Utils.NextFloat(Main.rand, 0.2f, 0.5f);
							base.npc.ai[3] = 11f;
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 4:
					base.npc.ai[2] += 1f;
					base.npc.rotation = base.npc.velocity.X * 0.01f;
					this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -60 : 60), 20f);
					if (base.npc.ai[2] < 100f)
					{
						base.npc.LookAtPlayer();
						if (base.npc.Distance(this.ShootPos) < 50f || base.npc.ai[2] > 40f)
						{
							base.npc.ai[2] = 100f;
							base.npc.ai[3] = (float)((Main.rand.Next(2) == 0) ? 19 : 20);
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 800f) ? 20f : 17f, 5f, true);
						}
					}
					else if (base.npc.ai[2] >= 100f)
					{
						base.npc.velocity *= 0.9f;
						if (base.npc.ai[2] == 105f)
						{
							base.npc.Dash(10, false, SoundID.Item74, player.Center);
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSHitboxFistC>(), 96, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] == 125f && RedeHelper.Chance(0.4f))
						{
							base.npc.ai[2] = 140f;
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 125f)
						{
							base.npc.LookAtPlayer();
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 800f) ? 20f : 17f, 5f, true);
						}
						if (base.npc.ai[2] > 140f)
						{
							if (RedeHelper.Chance(0.65f))
							{
								base.npc.ai[2] = 0f;
							}
							else
							{
								base.npc.LookAtPlayer();
								this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.1f);
								base.npc.ai[2] = 0f;
								base.npc.ai[1] = -1f;
							}
							base.npc.netUpdate = true;
						}
					}
					break;
				case 5:
					if (base.npc.ai[2] == 0f)
					{
						if (Main.rand.Next(6) == 0)
						{
							base.npc.ai[2] = 1f;
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(1, 6);
							base.npc.ai[2] = 0f;
						}
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.LookAtPlayer();
						base.npc.rotation = base.npc.velocity.X * 0.01f;
						base.npc.ai[2] += 1f;
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -80 : 80), 20f);
						if (base.npc.ai[2] == 5f)
						{
							base.npc.ai[3] = 21f;
						}
						base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 300f) ? 20f : 9f, 8f, true);
						if (base.npc.ai[2] >= 15f && base.npc.ai[2] % 3f == 0f)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_JojoFistC>(), 132, Vector2.Zero, false, SoundID.Item60.WithVolume(0.3f), "", (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] > 240f)
						{
							base.npc.ai[3] = 11f;
							this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.2f);
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				}
				break;
			}
			if (MoRDialogueUI.Visible)
			{
				Redemption.Inst.DialogueUIElement.PointPos = new Vector2?(base.npc.Center);
				Redemption.Inst.DialogueUIElement.TextColor = new Color?(RedeColor.SlayerColour);
			}
			if (Vector2.Distance(base.npc.Center, player.Center) >= 1100f && base.npc.ai[0] > 0f)
			{
				if (base.npc.ai[1] == 3f && base.npc.ai[0] == 5f)
				{
					return;
				}
				this.Teleport(false, Vector2.Zero);
				base.npc.netUpdate = true;
			}
		}

		public override bool CheckDead()
		{
			return true;
		}

		private void SnapGunToFiringArea()
		{
			float num = (base.npc.spriteDirection == 1) ? 0f : 3.1415927f;
			float minFiringRegion = num - 0.7853982f;
			float maxFiringRegion = num + 0.7853982f;
			while (this.gunRot < -1.5707964f)
			{
				this.gunRot += 6.2831855f;
			}
			while (this.gunRot > 4.712389f)
			{
				this.gunRot -= 6.2831855f;
			}
			if (this.gunRot > maxFiringRegion || this.gunRot < minFiringRegion)
			{
				float num2 = RedeHelper.AngularDifference(minFiringRegion, this.gunRot);
				float distFromMax = RedeHelper.AngularDifference(maxFiringRegion, this.gunRot);
				if (num2 < distFromMax)
				{
					this.gunRot = minFiringRegion;
					return;
				}
				this.gunRot = maxFiringRegion;
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

		public void Teleport(bool specialPos, Vector2 teleportPos)
		{
			if (Main.netMode != 1)
			{
				if (!specialPos)
				{
					int num = Main.rand.Next(2);
					if (num != 0)
					{
						if (num == 1)
						{
							Vector2 newPos2 = new Vector2((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-200, 50));
							base.npc.Center = Main.player[base.npc.target].Center + newPos2;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						Vector2 newPos3 = new Vector2((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + newPos3;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					base.npc.Center = Main.player[base.npc.target].Center + teleportPos;
					base.npc.netUpdate = true;
				}
			}
			Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
			for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 92, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].velocity *= 6f;
				Main.dust[dustIndex].noGravity = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D chargeAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_Dash");
			Texture2D armsAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_NoGunIdle");
			Texture2D armsCrossAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_Crossed");
			Texture2D armsGunAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_Rifle");
			Texture2D armsRocketFistAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_RocketFist");
			Texture2D armsTossAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_FlashThrow");
			Texture2D armsChargingAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_Charging");
			Texture2D armsShrugAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_Shrug");
			Texture2D armsShieldAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_BucklerShield");
			Texture2D fightIdleAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_MeleePrepare");
			Texture2D fightWheelKickAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_GuillotineWheelKick");
			Texture2D fightShouderBashAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_ShoulderBash");
			Texture2D fightDropkickAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_Dropkick");
			Texture2D fightPummelAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_Pummel");
			Texture2D fightJojoAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_Jojo");
			SpriteEffects effects = (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 drawCenter = base.npc.Center;
			if (base.npc.ai[3] < 11f)
			{
				if (base.npc.velocity.Length() < 13f)
				{
					spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
				}
				else
				{
					int num214 = chargeAni.Height / 6;
					int y6 = num214 * this.chargeFrame;
					for (int i = this.oldPos.Length - 1; i >= 0; i--)
					{
						float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
						spriteBatch.Draw(chargeAni, this.oldPos[i] - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, chargeAni.Width, num214)), RedeColor.SlayerColour * (0.5f * alpha), this.oldrot[i], new Vector2((float)chargeAni.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
					}
					Main.spriteBatch.Draw(chargeAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, chargeAni.Width, num214)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)chargeAni.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
				}
				if (base.npc.ai[3] == 0f)
				{
					int num215 = armsAni.Height / 4;
					int y7 = num215 * this.armFrames[0];
					Main.spriteBatch.Draw(armsAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, armsAni.Width, num215)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsAni.Width / 2f, (float)num215 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 1f)
				{
					int num216 = armsCrossAni.Height / 4;
					int y8 = num216 * this.armFrames[1];
					Main.spriteBatch.Draw(armsCrossAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, armsCrossAni.Width, num216)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsCrossAni.Width / 2f, (float)num216 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 2f || base.npc.ai[3] == 3f || base.npc.ai[3] == 4f)
				{
					int num217 = armsGunAni.Height / 9;
					int y9 = num217 * this.armFrames[2];
					Main.spriteBatch.Draw(armsGunAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, armsGunAni.Width, num217)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), this.gunRot + ((base.npc.spriteDirection == -1) ? 3.1415927f : 0f), new Vector2((float)armsGunAni.Width / 2f, (float)num217 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 5f)
				{
					int num218 = armsRocketFistAni.Height / 8;
					int y10 = num218 * this.armFrames[3];
					Main.spriteBatch.Draw(armsRocketFistAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y10, armsRocketFistAni.Width, num218)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsRocketFistAni.Width / 2f, (float)num218 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 6f)
				{
					int num219 = armsTossAni.Height / 6;
					int y11 = num219 * this.armFrames[4];
					Main.spriteBatch.Draw(armsTossAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y11, armsTossAni.Width, num219)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsTossAni.Width / 2f, (float)num219 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 7f)
				{
					int num220 = armsChargingAni.Height / 6;
					int y12 = num220 * this.armFrames[5];
					Main.spriteBatch.Draw(armsChargingAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y12, armsChargingAni.Width, num220)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsChargingAni.Width / 2f, (float)num220 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 8f)
				{
					int num221 = armsShrugAni.Height / 7;
					int y13 = num221 * this.armFrames[6];
					Main.spriteBatch.Draw(armsShrugAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y13, armsShrugAni.Width, num221)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsShrugAni.Width / 2f, (float)num221 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 9f || base.npc.ai[3] == 10f)
				{
					int num222 = armsShieldAni.Height / 5;
					int y14 = num222 * this.armFrames[7];
					Main.spriteBatch.Draw(armsShieldAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y14, armsShieldAni.Width, num222)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsShieldAni.Width / 2f, (float)num222 / 2f), base.npc.scale, effects, 0f);
				}
			}
			else
			{
				if (base.npc.ai[3] == 11f)
				{
					for (int j = this.oldPos.Length - 1; j >= 0; j--)
					{
						float alpha2 = 1f - (float)(j + 1) / (float)(this.oldPos.Length + 2);
						spriteBatch.Draw(fightIdleAni, this.oldPos[j] - Main.screenPosition, new Rectangle?(base.npc.frame), RedeColor.SlayerColour * (0.5f * alpha2), this.oldrot[j], Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
					}
					Main.spriteBatch.Draw(fightIdleAni, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
				}
				if (base.npc.ai[3] >= 12f && base.npc.ai[3] <= 14f)
				{
					int num223 = fightWheelKickAni.Height / 8;
					int y15 = num223 * this.fightFrames;
					for (int k = this.oldPos.Length - 1; k >= 0; k--)
					{
						float alpha3 = 1f - (float)(k + 1) / (float)(this.oldPos.Length + 2);
						spriteBatch.Draw(fightWheelKickAni, this.oldPos[k] - Main.screenPosition, new Rectangle?(new Rectangle(0, y15, fightWheelKickAni.Width, num223)), RedeColor.SlayerColour * (0.5f * alpha3), this.oldrot[k], new Vector2((float)fightWheelKickAni.Width / 2f, (float)num223 / 2f), base.npc.scale, effects, 0f);
					}
					Main.spriteBatch.Draw(fightWheelKickAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y15, fightWheelKickAni.Width, num223)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightWheelKickAni.Width / 2f, (float)num223 / 2f), base.npc.scale, effects, 0f);
				}
				if (base.npc.ai[3] == 15f || base.npc.ai[3] == 16f)
				{
					int num224 = fightShouderBashAni.Height / 6;
					int y16 = num224 * this.fightFrames;
					for (int l = this.oldPos.Length - 1; l >= 0; l--)
					{
						float alpha4 = 1f - (float)(l + 1) / (float)(this.oldPos.Length + 2);
						spriteBatch.Draw(fightShouderBashAni, this.oldPos[l] - Main.screenPosition, new Rectangle?(new Rectangle(0, y16, fightShouderBashAni.Width, num224)), RedeColor.SlayerColour * (0.5f * alpha4), this.oldrot[l], new Vector2((float)fightShouderBashAni.Width / 2f, (float)num224 / 2f), base.npc.scale, effects, 0f);
					}
					Main.spriteBatch.Draw(fightShouderBashAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y16, fightShouderBashAni.Width, num224)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightShouderBashAni.Width / 2f, (float)num224 / 2f), base.npc.scale, effects, 0f);
				}
				if (base.npc.ai[3] == 17f || base.npc.ai[3] == 18f)
				{
					int num225 = fightDropkickAni.Height / 5;
					int y17 = num225 * this.fightFrames;
					for (int m = this.oldPos.Length - 1; m >= 0; m--)
					{
						float alpha5 = 1f - (float)(m + 1) / (float)(this.oldPos.Length + 2);
						spriteBatch.Draw(fightDropkickAni, this.oldPos[m] - Main.screenPosition, new Rectangle?(new Rectangle(0, y17, fightDropkickAni.Width, num225)), RedeColor.SlayerColour * (0.5f * alpha5), this.oldrot[m], new Vector2((float)fightDropkickAni.Width / 2f, (float)num225 / 2f), base.npc.scale, effects, 0f);
					}
					Main.spriteBatch.Draw(fightDropkickAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y17, fightDropkickAni.Width, num225)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightDropkickAni.Width / 2f, (float)num225 / 2f), base.npc.scale, effects, 0f);
				}
				if (base.npc.ai[3] == 19f || base.npc.ai[3] == 20f)
				{
					int num226 = fightPummelAni.Height / 6;
					int y18 = num226 * this.fightFrames;
					for (int n = this.oldPos.Length - 1; n >= 0; n--)
					{
						float alpha6 = 1f - (float)(n + 1) / (float)(this.oldPos.Length + 2);
						spriteBatch.Draw(fightPummelAni, this.oldPos[n] - Main.screenPosition, new Rectangle?(new Rectangle(0, y18, fightPummelAni.Width, num226)), RedeColor.SlayerColour * (0.5f * alpha6), this.oldrot[n], new Vector2((float)fightPummelAni.Width / 2f, (float)num226 / 2f), base.npc.scale, effects, 0f);
					}
					Main.spriteBatch.Draw(fightPummelAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y18, fightPummelAni.Width, num226)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightPummelAni.Width / 2f, (float)num226 / 2f), base.npc.scale, effects, 0f);
				}
				if (base.npc.ai[3] == 21f)
				{
					int num227 = fightJojoAni.Height / 7;
					int y19 = num227 * this.fightFrames;
					for (int k2 = this.oldPos.Length - 1; k2 >= 0; k2--)
					{
						float alpha7 = 1f - (float)(k2 + 1) / (float)(this.oldPos.Length + 2);
						spriteBatch.Draw(fightJojoAni, this.oldPos[k2] - Main.screenPosition, new Rectangle?(new Rectangle(0, y19, fightJojoAni.Width, num227)), RedeColor.SlayerColour * (0.5f * alpha7), this.oldrot[k2], new Vector2((float)fightJojoAni.Width / 2f, (float)num227 / 2f), base.npc.scale, effects, 0f);
					}
					Main.spriteBatch.Draw(fightJojoAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y19, fightJojoAni.Width, num227)), RedeColor.SlayerColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightJojoAni.Width / 2f, (float)num227 / 2f), base.npc.scale, effects, 0f);
				}
			}
			return false;
		}

		public Vector2[] oldPos = new Vector2[3];

		public float[] oldrot = new float[3];

		public Vector2 MoveVector2;

		private Vector2 ShootPos;

		private Vector2 vector;

		public int frameCounters;

		public int chargeCounter;

		public int chargeFrame;

		public float chance = 0.8f;

		public float gunRot;

		public int[] armFrames = new int[8];

		public int fightFrames;

		private const float gunRotLimit = 1.5707964f;
	}
}
