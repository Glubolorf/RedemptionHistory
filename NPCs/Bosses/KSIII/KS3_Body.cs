using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Dusts;
using Redemption.Items;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.HM;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.NPCs.Bosses.Neb;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	[AutoloadBossHead]
	public class KS3_Body : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III");
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

		public override void BossLoot(ref string name, ref int potionType)
		{
			Main.player[base.npc.target].GetModPlayer<ScreenPlayer>().lockScreen = false;
			potionType = 499;
			if (!RedeWorld.downedSlayer)
			{
				if (base.npc.ai[0] == 12f)
				{
					for (int i = 0; i < 255; i++)
					{
						Player player2 = Main.player[i];
						if (player2.active)
						{
							for (int j = 0; j < player2.inventory.Length; j++)
							{
								if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
								{
									Main.NewText("<Chalice of Alignment> Good thing you left him be...", Color.DarkGoldenrod, false);
								}
							}
							CombatText.NewText(player2.getRect(), Color.Gray, "+0", true, false);
						}
					}
				}
				else
				{
					RedeWorld.redemptionPoints--;
					for (int k = 0; k < 255; k++)
					{
						Player player3 = Main.player[k];
						if (player3.active)
						{
							for (int l = 0; l < player3.inventory.Length; l++)
							{
								if (player3.inventory[l].type == ModContent.ItemType<RedemptionTeller>())
								{
									Main.NewText("<Chalice of Alignment> Oh dear, he seems to have a very short temper, and you winning probably made it worse... I hope he doesn't do anything stupid...", Color.DarkGoldenrod, false);
								}
							}
							CombatText.NewText(player3.getRect(), Color.Red, "-1", true, false);
						}
					}
				}
			}
			RedeWorld.downedSlayer = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Projectile.NewProjectile(new Vector2(base.npc.Center.X - 60f, base.npc.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<KSExitPro>(), 0, 0f, 255, 0f, 0f);
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

		public override void HitEffect(int hitDirection, double damage)
		{
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (this.phase >= 5)
			{
				damage *= 0.6;
			}
			else
			{
				damage *= 0.75;
			}
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
			if (!this.title)
			{
				if (RedeConfigClient.Instance.NoLoreElements && !Main.dedServ)
				{
					Redemption.Inst.TitleCardUIElement.DisplayTitle("King Slayer III", 60, 90, 0.8f, 0, new Color?(Color.Cyan), "Prototype Multium", true);
				}
				this.title = true;
			}
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
			float num = base.npc.ai[3];
			if (!0f.Equals(num))
			{
				if (!1f.Equals(num))
				{
					if (!2f.Equals(num))
					{
						if (!3f.Equals(num))
						{
							if (!4f.Equals(num))
							{
								if (!5f.Equals(num))
								{
									if (!6f.Equals(num))
									{
										if (!7f.Equals(num))
										{
											if (!8f.Equals(num))
											{
												if (!9f.Equals(num))
												{
													if (!10f.Equals(num))
													{
														if (!12f.Equals(num))
														{
															if (!13f.Equals(num))
															{
																if (!14f.Equals(num))
																{
																	if (!15f.Equals(num))
																	{
																		if (!16f.Equals(num))
																		{
																			if (!17f.Equals(num))
																			{
																				if (!18f.Equals(num))
																				{
																					if (!19f.Equals(num))
																					{
																						if (!20f.Equals(num))
																						{
																							if (21f.Equals(num))
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
			switch (this.headType)
			{
			case 0:
				this.headFrame = base.npc.frame.Y / 160;
				break;
			case 1:
				this.headFrame = base.npc.frame.Y / 160 + 4;
				break;
			case 2:
				this.headFrame = base.npc.frame.Y / 160 + 8;
				break;
			case 3:
				this.headFrame = base.npc.frame.Y / 160 + 12;
				break;
			case 4:
				this.headFrame = base.npc.frame.Y / 160 + 16;
				break;
			}
			if (this.phase >= 5)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Dust dust7 = Main.dust[dustIndex];
				dust7.velocity.X = 0f;
				dust7.velocity.Y = -5f;
			}
			if (this.teleGlow)
			{
				this.teleGlowTimer += 3f;
				if (this.teleGlowTimer > 60f)
				{
					this.teleGlow = false;
					this.teleGlowTimer = 0f;
				}
			}
			this.DespawnHandler();
			if (this.chance > 1f)
			{
				this.chance = 1f;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.75f) && this.phase < 1)
			{
				base.npc.ai[0] = 2f;
			}
			else if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && this.phase < 2)
			{
				base.npc.ai[0] = 2f;
			}
			else if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f) && this.phase < 3)
			{
				base.npc.ai[0] = 2f;
			}
			else if (base.npc.life < (int)((float)base.npc.lifeMax * 0.05f) && this.phase < 4 && !RedeConfigClient.Instance.NoLoreElements)
			{
				base.npc.ai[0] = 2f;
			}
			if ((base.npc.ai[0] > 1f && base.npc.ai[0] < 6f) || base.npc.ai[0] == 10f)
			{
				base.npc.dontTakeDamage = false;
			}
			else
			{
				base.npc.dontTakeDamage = true;
			}
			player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
			switch ((int)base.npc.ai[0])
			{
			case 0:
			{
				base.npc.LookAtPlayer();
				base.npc.ai[3] = 1f;
				player.GetModPlayer<ScreenPlayer>().Rumble(5, 5);
				this.teleVector = base.npc.Center;
				this.teleGlow = true;
				float[] ai = base.npc.ai;
				int num2 = 2;
				num = ai[num2] + 1f;
				ai[num2] = num;
				if (num > 5f)
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 1f;
					base.npc.netUpdate = true;
				}
				break;
			}
			case 1:
				base.npc.LookAtPlayer();
				this.gunRot = ((base.npc.spriteDirection == 1) ? 0f : 3.1415927f);
				base.npc.ai[2] += 1f;
				if (RedeConfigClient.Instance.NoLoreElements)
				{
					if (base.npc.ai[2] == 60f)
					{
						base.npc.ai[3] = 2f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] >= 160f)
					{
						if (RedeWorld.slayerDeath < 3)
						{
							RedeWorld.slayerDeath = 3;
						}
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-400, -300) : Main.rand.Next(300, 400)), (float)Main.rand.Next(-60, 60));
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						this.headType = 0;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = 3f;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					player.GetModPlayer<ScreenPlayer>().lockScreen = true;
					if (RedeWorld.slayerDeath < 3)
					{
						if (base.npc.ai[2] == 30f)
						{
							this.headType = 2;
							if (!Main.dedServ)
							{
								if (RedeHelper.AnyProjectiles(ModContent.ProjectileType<KS3_DroneKillCheck>()))
								{
									Main.PlaySound(12, -1, -1, 1, 1f, 0f);
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Did you seriously just destroy my drones?", 280, 1, 0.6f, "King Slayer III:", 1f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
								}
								else
								{
									Main.PlaySound(12, -1, -1, 1, 1f, 0f);
									if (RedeWorld.redemptionPoints >= 0)
									{
										if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower || player.IsFullTBot())
										{
											Redemption.Inst.DialogueUIElement.DisplayDialogue("Alright listen here you little scrap of metal.", 280, 1, 0.6f, "King Slayer III:", 1f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
										}
										else if (BasePlayer.HasAccessory(player, ModContent.ItemType<CrownOfTheKing>(), true, true))
										{
											Redemption.Inst.DialogueUIElement.DisplayDialogue("Alright listen here you little chicken nugget.", 280, 1, 0.6f, "King Slayer III:", 1f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
										}
										else
										{
											Redemption.Inst.DialogueUIElement.DisplayDialogue("Alright listen here you little fleshbag.", 280, 1, 0.6f, "King Slayer III:", 1f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
										}
									}
									else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower || player.IsFullTBot())
									{
										Redemption.Inst.DialogueUIElement.DisplayDialogue("Ah, this little scrap of metal decided to save me the trouble of finding it.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									}
									else if (BasePlayer.HasAccessory(player, ModContent.ItemType<CrownOfTheKing>(), true, true))
									{
										Redemption.Inst.DialogueUIElement.DisplayDialogue("Ah, this little chicken nugget decided to save me the trouble of finding it.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									}
									else
									{
										Redemption.Inst.DialogueUIElement.DisplayDialogue("Ah, this little fleshbag decided to save me the trouble of finding it.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									}
								}
							}
						}
						if (base.npc.ai[2] == 310f)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							this.headType = 1;
							if (!Main.dedServ)
							{
								if (RedeHelper.AnyProjectiles(ModContent.ProjectileType<KS3_DroneKillCheck>()))
								{
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Eh, not like I got a shortage of them, but I'm still gonna blast ya for it!", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
								}
								else if (RedeWorld.redemptionPoints >= 0)
								{
									Redemption.Inst.DialogueUIElement.DisplayDialogue("I warned you, so don't go crying to your mummy when I crush you into the ground!", 280, 1, 0.6f, "King Slayer III:", 1f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
								}
								else
								{
									Redemption.Inst.DialogueUIElement.DisplayDialogue("You were on my hitlist, so lets skip the small talk and get on with it!", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
								}
							}
						}
						if (base.npc.ai[2] == 590f)
						{
							base.npc.ai[3] = 0f;
							this.headType = 0;
							if (RedeWorld.downedKeeper)
							{
								Main.PlaySound(12, -1, -1, 1, 1f, 0f);
								if (!Main.dedServ)
								{
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Actually... You were the one that fought the Keeper, weren't you!", 240, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
								}
							}
							else
							{
								base.npc.ai[2] = 1140f;
							}
						}
						if (base.npc.ai[2] == 830f)
						{
							this.headType = 2;
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							if (!Main.dedServ)
							{
								Redemption.Inst.DialogueUIElement.DisplayDialogue("That was my job!", 120, 1, 0.6f, "King Slayer III:", 2f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
							}
						}
						if (base.npc.ai[2] == 950f)
						{
							this.headType = 0;
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							if (!Main.dedServ)
							{
								Redemption.Inst.DialogueUIElement.DisplayDialogue("Great, now I have even more reason to pummel you to ash!", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
							}
						}
						if (base.npc.ai[2] == 1150f)
						{
							base.npc.ai[3] = 2f;
							this.headType = 0;
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] >= 1250f)
						{
							if (!Main.dedServ)
							{
								Redemption.Inst.TitleCardUIElement.DisplayTitle("King Slayer III", 60, 90, 0.8f, 0, new Color?(Color.Cyan), "Prototype Multium", true);
							}
							this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-400, -300) : Main.rand.Next(300, 400)), (float)Main.rand.Next(-60, 60));
							if (RedeWorld.slayerDeath < 3)
							{
								RedeWorld.slayerDeath = 3;
							}
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							this.headType = 0;
							base.npc.ai[2] = 0f;
							base.npc.ai[0] = 3f;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						if (base.npc.ai[2] == 30f && !Main.dedServ)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							if (RedeWorld.downedSlayer)
							{
								switch (Main.rand.Next(4))
								{
								case 0:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("What? Do you want to fight me again?", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								case 1:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Why must you summon me again?", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								case 2:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Could you maybe possibly probably potentially LEAVE ME ALONE?", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								case 3:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Really, a rematch? Fine.", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								}
							}
							else
							{
								switch (Main.rand.Next(5))
								{
								case 0:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("You're quite a resilient fellow...", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								case 1:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Could've sworn you died...", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								case 2:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Ready for a rematch?", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								case 3:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Welp, time to win again!", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								case 4:
									Redemption.Inst.DialogueUIElement.DisplayDialogue("Still wanna fight?", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
									break;
								}
							}
						}
						if (base.npc.ai[2] == 180f)
						{
							base.npc.ai[3] = 2f;
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] >= 240f)
						{
							if (!Main.dedServ)
							{
								Redemption.Inst.TitleCardUIElement.DisplayTitle("King Slayer III", 60, 90, 0.8f, 0, new Color?(Color.Cyan), "Prototype Multium", true);
							}
							this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-400, -300) : Main.rand.Next(300, 400)), (float)Main.rand.Next(-60, 60));
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							this.headType = 0;
							base.npc.ai[2] = 0f;
							base.npc.ai[0] = 3f;
							base.npc.netUpdate = true;
						}
					}
				}
				break;
			case 2:
				base.npc.LookAtPlayer();
				base.npc.ai[2] = 0f;
				base.npc.ai[1] = 0f;
				this.gunRot = ((base.npc.spriteDirection == 1) ? 0f : 3.1415927f);
				base.npc.rotation = 0f;
				base.npc.velocity *= 0.9f;
				base.npc.netUpdate = true;
				if (base.npc.ai[3] == 2f)
				{
					base.npc.ai[3] = 4f;
					base.npc.netUpdate = true;
				}
				else if (base.npc.ai[3] != 0f && base.npc.ai[3] != 4f)
				{
					base.npc.ai[3] = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 0f && base.npc.velocity.Length() < 1f)
				{
					switch (this.phase)
					{
					case 0:
						this.phase = 1;
						base.npc.ai[0] = 6f;
						break;
					case 1:
						this.phase = 2;
						base.npc.ai[0] = 7f;
						break;
					case 2:
						this.phase = 3;
						base.npc.ai[0] = 8f;
						break;
					case 3:
						this.phase = 4;
						base.npc.ai[0] = 9f;
						break;
					}
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
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
							base.npc.netUpdate = true;
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
							base.npc.netUpdate = true;
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
					if (this.phase <= 1)
					{
						if (base.npc.ai[2] % 40f == 0f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 462, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 17 : 7), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun1", 0f, 0f);
							base.npc.ai[3] = 3f;
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] % 120f == 0f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot + MathHelper.ToRadians(25f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot - MathHelper.ToRadians(25f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.ai[3] = 3f;
						}
						if (base.npc.ai[2] > 370f)
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.1f, 0.5f);
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					else if (this.phase >= 5)
					{
						if (base.npc.ai[2] % 20f == 0f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 462, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 17 : 7), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun1", 0f, 0f);
							base.npc.ai[3] = 3f;
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] % 100f == 0f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot + MathHelper.ToRadians(25f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot - MathHelper.ToRadians(25f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot + MathHelper.ToRadians(50f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot - MathHelper.ToRadians(50f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.ai[3] = 3f;
						}
						if (base.npc.ai[2] > 310f)
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.1f, 0.5f);
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						if (base.npc.ai[2] % 35f == 0f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 462, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 17 : 7), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun1", 0f, 0f);
							base.npc.ai[3] = 3f;
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] % 105f == 0f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot + MathHelper.ToRadians(25f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot - MathHelper.ToRadians(25f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot + MathHelper.ToRadians(50f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 435, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 18 : 8), this.gunRot - MathHelper.ToRadians(50f)), true, SoundID.Item1, "Sounds/Custom/Gun3", 0f, 0f);
							base.npc.ai[3] = 3f;
						}
						if (base.npc.ai[2] > 330f)
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.1f, 0.5f);
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
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
						if (base.npc.Distance(this.ShootPos) < 100f || ((this.phase >= 5) ? (base.npc.ai[2] > 40f) : (base.npc.ai[2] > 80f)))
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
							for (int j = 0; j < 3; j++)
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
							for (int k = 0; k < Main.rand.Next(5, 8); k++)
							{
								base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 462, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? Main.rand.Next(17, 21) : Main.rand.Next(7, 11)), this.gunRot + Utils.NextFloat(Main.rand, -0.14f, 0.14f)), true, SoundID.Item1, "Sounds/Custom/ShotgunBlast1", 0f, 0f);
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
					if ((this.phase >= 5) ? (base.npc.ai[2] == 40f) : (base.npc.ai[2] == 60f))
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), ModContent.ProjectileType<ReboundShot>(), 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 25 : 15), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun2", 0f, 0f);
						base.npc.ai[3] = 3f;
						base.npc.netUpdate = true;
					}
					if ((this.phase >= 5) ? (base.npc.ai[2] > 60f) : (base.npc.ai[2] > 90f))
					{
						this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.1f);
						base.npc.ai[2] = 0f;
						base.npc.ai[1] = -1f;
						base.npc.netUpdate = true;
					}
					break;
				case 4:
					if (base.npc.ai[2] == 0f)
					{
						if (this.phase > 0)
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
						ref this.gunRot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), 0.05235988f);
						this.SnapGunToFiringArea();
						base.npc.ai[2] += 1f;
						this.ShootPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -450 : 450), -10f);
						base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) < 100f) ? 4f : ((base.npc.Distance(player.Center) > 800f) ? 20f : 12f), 14f, true);
						if (base.npc.ai[3] < 2f || base.npc.ai[3] > 4f)
						{
							base.npc.ai[3] = 2f;
						}
						if ((this.phase >= 5) ? (base.npc.ai[2] == 41f || base.npc.ai[2] == 44f || base.npc.ai[2] == 47f) : (base.npc.ai[2] == 61f || base.npc.ai[2] == 64f || base.npc.ai[2] == 67f))
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), ModContent.ProjectileType<ReboundShot>(), 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 25 : 15), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun2", 0f, 0f);
							base.npc.ai[3] = 3f;
							base.npc.netUpdate = true;
						}
						if ((this.phase >= 5) ? (base.npc.ai[2] > 61f) : (base.npc.ai[2] > 91f))
						{
							this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.1f);
							base.npc.ai[2] = 0f;
							base.npc.ai[1] = -1f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 5:
					if (base.npc.ai[2] == 0f)
					{
						if (this.phase > 0)
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
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(54f, this.gunRot) + RedeHelper.PolarVector((float)(13 * base.npc.spriteDirection), this.gunRot - 1.5707964f), 462, 72, RedeHelper.PolarVector((float)((base.npc.Distance(player.Center) > 800f) ? 16 : 6), this.gunRot), true, SoundID.Item1, "Sounds/Custom/Gun1", 0f, 0f);
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
								for (int l = 0; l < 3; l++)
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
								base.npc.Shoot(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 2f) : (base.npc.Center.X - 2f), base.npc.Center.Y - 16f), ModContent.ProjectileType<KSBeamCell>(), 96, new Vector2((float)((base.npc.spriteDirection == 1) ? 10 : -10), 0f), false, SoundID.Item103, "", (float)base.npc.whoAmI, 0f);
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
								for (int m = 0; m < 3; m++)
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
								base.npc.Shoot(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 2f) : (base.npc.Center.X - 2f), base.npc.Center.Y - 16f), ModContent.ProjectileType<KSSurge>(), 80, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Zap1", (float)base.npc.whoAmI, 0f);
								for (int n = 0; n < 16; n++)
								{
									int projID = Projectile.NewProjectile(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 2f) : (base.npc.Center.X - 2f), base.npc.Center.Y - 16f), Vector2.Zero, ModContent.ProjectileType<KSSurge2>(), 0, 0f, Main.myPlayer, 0f, 0f);
									Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)n / 16f * 6.28f);
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
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShield>()) && (player.HeldItem.magic || player.HeldItem.ranged) && Main.rand.Next(4) == 0)
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
							base.npc.Shoot(new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 48f) : (base.npc.Center.X - 48f), base.npc.Center.Y - 12f), ModContent.ProjectileType<KSReflect>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
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
						if (this.phase > 0 && !NPC.AnyNPCs(ModContent.NPCType<KSMissileDrone>()) && Main.rand.Next(4) == 0)
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
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_Call>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
							if (!NPC.AnyNPCs(ModContent.NPCType<KSMissileDrone>()))
							{
								for (int i3 = 0; i3 < Main.rand.Next(2, 5); i3++)
								{
									base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(750, 800), ModContent.NPCType<KSMissileDrone>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
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
						if (this.phase > 1 && !NPC.AnyNPCs(ModContent.NPCType<KS3Magnet>()) && Main.rand.Next(4) == 0)
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
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_Call>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
							if (!NPC.AnyNPCs(ModContent.NPCType<KS3Magnet>()))
							{
								for (int i4 = 0; i4 < 2; i4++)
								{
									base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(750, 800), ModContent.NPCType<KS3Magnet>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
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
							base.npc.Move(this.ShootPos, (base.npc.Distance(player.Center) > 800f) ? 34f : 26f, 3f, true);
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
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSHitbox>(), 120, Vector2.Zero, false, SoundID.Item74, "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSWave>(), 120, Vector2.Zero, false, SoundID.Item74, "", (float)base.npc.whoAmI, 0f);
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
						if (base.npc.Distance(this.ShootPos) < 50f || base.npc.ai[2] > 70f)
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
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSHitboxBash>(), 120, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
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
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSHitboxDropkick>(), 156, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
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
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSHitboxFist>(), 96, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
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
							if (RedeHelper.Chance(0.35f))
							{
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
							}
							else
							{
								base.npc.LookAtPlayer();
								this.chance -= Utils.NextFloat(Main.rand, 0.05f, 0.1f);
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
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_JojoFist>(), 132, Vector2.Zero, false, SoundID.Item60.WithVolume(0.3f), "", (float)base.npc.whoAmI, 0f);
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
			case 6:
				base.npc.LookAtPlayer();
				base.npc.velocity *= 0.9f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 5f)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield2>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				}
				if (RedeConfigClient.Instance.NoLoreElements || RedeWorld.slayerDeath >= 4)
				{
					if (base.npc.ai[2] == 30f)
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_Call>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
						this.headType = 0;
						if (!NPC.AnyNPCs(ModContent.NPCType<KSMissileDrone>()))
						{
							for (int i5 = 0; i5 < 4; i5++)
							{
								base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(750, 800), ModContent.NPCType<KSMissileDrone>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
							}
						}
					}
					if (base.npc.ai[2] > 80f)
					{
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = (float)Main.rand.Next(3, 5);
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShield>()))
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						base.npc.netUpdate = true;
					}
				}
				else
				{
					base.npc.ai[3] = 1f;
					if (base.npc.ai[2] == 20f && !Main.dedServ)
					{
						this.headType = 1;
						if (RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShield>()))
						{
							if (player.HeldItem.melee)
							{
								Main.PlaySound(12, -1, -1, 1, 1f, 0f);
								Redemption.Inst.DialogueUIElement.DisplayDialogue("What a nuisance. It would seem my Auto-Shield is ineffective to your blades.\n'Twas meant to protect from high velocity blasts, I should change tactics.", 400, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
								base.npc.ai[1] = 1f;
							}
							else
							{
								Main.PlaySound(12, -1, -1, 1, 1f, 0f);
								Redemption.Inst.DialogueUIElement.DisplayDialogue("What a nuisance. Your petty projectiles are going through my Auto-Shield.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
							}
						}
						else
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("What a nuisance. You are only wasting both of our efforts here.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
					}
					if ((base.npc.ai[1] == 1f) ? (base.npc.ai[2] == 420f) : (base.npc.ai[2] == 300f && !Main.dedServ))
					{
						this.headType = 2;
						if (this.teleportCount > 6)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("Why'd you summon me if you're just gonna run away the entire time?", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						else
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("Might as well blow you to pieces with a few missiles.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
					}
					if ((base.npc.ai[1] == 1f) ? (base.npc.ai[2] == 700f) : (base.npc.ai[2] == 580f))
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_Call>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
						this.headType = 0;
						if (!NPC.AnyNPCs(ModContent.NPCType<KSMissileDrone>()))
						{
							for (int i6 = 0; i6 < 4; i6++)
							{
								base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(750, 800), ModContent.NPCType<KSMissileDrone>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
							}
						}
					}
					if ((base.npc.ai[1] == 1f) ? (base.npc.ai[2] > 740f) : (base.npc.ai[2] > 620f))
					{
						if (RedeWorld.slayerDeath < 4)
						{
							RedeWorld.slayerDeath = 4;
						}
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = (float)Main.rand.Next(3, 5);
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShield>()))
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						base.npc.netUpdate = true;
					}
				}
				break;
			case 7:
				base.npc.LookAtPlayer();
				base.npc.velocity *= 0.9f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 5f)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield2>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				}
				if (RedeConfigClient.Instance.NoLoreElements || RedeWorld.slayerDeath >= 5)
				{
					if (base.npc.ai[2] == 30f)
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_Call>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
						this.headType = 0;
						if (!NPC.AnyNPCs(ModContent.NPCType<KS3Magnet>()))
						{
							for (int i7 = 0; i7 < 2; i7++)
							{
								base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(750, 800), ModContent.NPCType<KS3Magnet>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
							}
						}
					}
					if (base.npc.ai[2] > 80f)
					{
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = (float)Main.rand.Next(3, 5);
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShield>()))
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						base.npc.netUpdate = true;
					}
				}
				else
				{
					base.npc.ai[3] = 1f;
					if (base.npc.ai[2] == 20f && !Main.dedServ)
					{
						this.headType = 4;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower || player.IsFullTBot())
						{
							Redemption.Inst.DialogueUIElement.DisplayDialogue("This rusty little tincan is more persistent than I thought...", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						else if (BasePlayer.HasAccessory(player, ModContent.ItemType<CrownOfTheKing>(), true, true))
						{
							Redemption.Inst.DialogueUIElement.DisplayDialogue("The concept of losing to a chicken does not bode well with me...", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						else
						{
							Redemption.Inst.DialogueUIElement.DisplayDialogue("You pack more of a punch than I thought for such a small fleshbag...", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
					}
					if (base.npc.ai[2] == 300f && !Main.dedServ)
					{
						this.headType = 3;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("I might even have to take you seriously...", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 480f && !Main.dedServ)
					{
						this.headType = 0;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("PAH! What a joke!", 120, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 600f && !Main.dedServ)
					{
						this.headType = 2;
						if (player.HeldItem.ranged || player.HeldItem.magic)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("You like shooting things, correct? Well try shooting me now.", 240, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						else
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("Go ahead, shoot me if you can.", 240, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
					}
					if (base.npc.ai[2] == 840f)
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KS3_Call>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
						this.headType = 0;
						if (!NPC.AnyNPCs(ModContent.NPCType<KS3Magnet>()))
						{
							for (int i8 = 0; i8 < 2; i8++)
							{
								base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(750, 800), ModContent.NPCType<KS3Magnet>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
							}
						}
					}
					if (base.npc.ai[2] > 880f)
					{
						if (RedeWorld.slayerDeath < 5)
						{
							RedeWorld.slayerDeath = 5;
						}
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = (float)Main.rand.Next(3, 5);
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShield>()))
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						base.npc.netUpdate = true;
					}
				}
				break;
			case 8:
				base.npc.LookAtPlayer();
				base.npc.velocity *= 0.9f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 5f)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield2>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				}
				if (RedeConfigClient.Instance.NoLoreElements || RedeWorld.slayerDeath >= 6)
				{
					if (base.npc.ai[2] > 80f)
					{
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = (float)Main.rand.Next(3, 5);
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShield>()))
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						base.npc.netUpdate = true;
					}
				}
				else
				{
					base.npc.ai[3] = 1f;
					if (base.npc.ai[2] == 20f && !Main.dedServ)
					{
						this.headType = 2;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("This is getting ridiculous! Why can't I kill you?", 240, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 260f && !Main.dedServ)
					{
						this.headType = 3;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("*Ahem* Your persistence is admirable, I'll give you that.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 540f && !Main.dedServ)
					{
						this.headType = 2;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("But you better realise I'm hardly trying. I ain't bluffing either.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] > 840f)
					{
						if (RedeWorld.slayerDeath < 6)
						{
							RedeWorld.slayerDeath = 6;
						}
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = (float)Main.rand.Next(3, 5);
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<KSShield>()))
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						base.npc.netUpdate = true;
					}
				}
				break;
			case 9:
				base.npc.LookAtPlayer();
				base.npc.velocity *= 0.9f;
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 5f)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield2>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				}
				if (RedeWorld.slayerDeath >= 7)
				{
					if (base.npc.ai[2] == 20f && !Main.dedServ)
					{
						this.headType = 3;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("If you stop attacking, I'll go back to more IMPORTANT business.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] > 300f)
					{
						base.npc.life = 1;
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ProjDeath>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
						this.headType = 0;
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = 10f;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					base.npc.ai[3] = 1f;
					if (base.npc.ai[2] == 20f && !Main.dedServ)
					{
						this.headType = 0;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("Alright alright alright!", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 200f && !Main.dedServ)
					{
						this.headType = 1;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("We'll... call it a draw then.", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 380f && !Main.dedServ)
					{
						this.headType = 1;
						if (this.teleportCount > 16)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("You've just been flying away the entire fight. Seriously.", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						else
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("I'm too tired to get mad about this nonsense.", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
					}
					if (base.npc.ai[2] == 580f && !Main.dedServ)
					{
						this.headType = 2;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("If you stop attacking, I'll go back to more IMPORTANT business.", 260, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] == 840f && !Main.dedServ)
					{
						this.headType = 3;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("But, if you so choose, we can continue... But I won't be happy if I lose.", 280, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					if (base.npc.ai[2] > 1120f)
					{
						base.npc.life = 1;
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ProjDeath>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
						this.headType = 0;
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = 10f;
						base.npc.netUpdate = true;
					}
				}
				break;
			case 10:
				base.npc.LookAtPlayer();
				base.npc.chaseable = false;
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				base.npc.ai[2] += 1f;
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
				if (!Main.dedServ)
				{
					if (base.npc.ai[2] == 10f)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("5", 60, 1, 0.6f, null, 0f, null, null, null, null, 0, 1);
					}
					if (base.npc.ai[2] == 70f)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("4", 60, 1, 0.6f, null, 0f, null, null, null, null, 0, 1);
					}
					if (base.npc.ai[2] == 130f)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("3", 60, 1, 0.6f, null, 0f, null, null, null, null, 0, 1);
					}
					if (base.npc.ai[2] == 190f)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("2", 60, 1, 0.6f, null, 0f, null, null, null, null, 0, 1);
					}
					if (base.npc.ai[2] == 250f)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("1", 60, 1, 0.6f, null, 0f, null, null, null, null, 0, 1);
					}
				}
				if (base.npc.ai[2] >= 310f)
				{
					if (RedeWorld.slayerDeath < 7)
					{
						RedeWorld.slayerDeath = 7;
					}
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 12f;
					base.npc.netUpdate = true;
				}
				break;
			case 11:
				base.npc.LookAtPlayer();
				base.npc.chaseable = true;
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				base.npc.ai[2] += 1f;
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
				if (base.npc.ai[2] == 5f)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSShield2>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				}
				if (base.npc.ai[2] == 30f && !Main.dedServ)
				{
					this.headType = 1;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("I see how it is...", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
				}
				if (base.npc.ai[2] == 180f)
				{
					base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(800, 900), ModContent.NPCType<SpaceKeeper>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
				}
				if (base.npc.ai[2] == 190f)
				{
					base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(800, 900), ModContent.NPCType<SpaceKeeper>(), (float)base.npc.whoAmI, 1f, 0f, 0f);
				}
				if (base.npc.ai[2] == 200f)
				{
					base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(800, 900), ModContent.NPCType<SpaceKeeper>(), (float)base.npc.whoAmI, 2f, 0f, 0f);
				}
				if (base.npc.ai[2] == 210f)
				{
					base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-80, 80), (int)base.npc.Center.Y - Main.rand.Next(800, 900), ModContent.NPCType<SpaceKeeper>(), (float)base.npc.whoAmI, 3f, 0f, 0f);
				}
				if (base.npc.ai[2] > 400f)
				{
					if (base.npc.life < 10000)
					{
						int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<HealDust>(), 0f, 0f, 0, default(Color), 1f);
						Main.dust[dustIndex2].velocity.Y = -3f;
						Main.dust[dustIndex2].velocity.X = 0f;
						Main.dust[dustIndex2].noGravity = true;
						base.npc.life += 200;
						base.npc.HealEffect(200, true);
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = 13f;
						base.npc.life = 10000;
						base.npc.netUpdate = true;
					}
				}
				break;
			case 12:
				base.npc.LookAtPlayer();
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				base.npc.ai[2] += 1f;
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
				if (base.npc.ai[2] == 30f && !Main.dedServ)
				{
					this.headType = 0;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Tie it is then. Now don't distract me again.", 260, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
				}
				if (base.npc.ai[2] == 290f && !Main.dedServ)
				{
					this.headType = 1;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Adios, dingus.", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
				}
				if (base.npc.ai[2] > 470f)
				{
					base.npc.dontTakeDamage = false;
					if (RedeWorld.slayerDeath < 7)
					{
						RedeWorld.slayerDeath = 7;
					}
					player.ApplyDamageToNPC(base.npc, 9999, 0f, 0, false);
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
					base.npc.netUpdate = true;
				}
				break;
			case 13:
				base.npc.LookAtPlayer();
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] < 30f)
				{
					this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
				}
				if (base.npc.ai[2] == 30f && !Main.dedServ)
				{
					this.headType = 2;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (RedeWorld.slayerDeath >= 8)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("Once again, you really are eager to win...", 220, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					else
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("I'm disappointed I actually have to overclock this vessel...", 220, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
				}
				if (base.npc.ai[2] == 250f && !Main.dedServ)
				{
					this.headType = 4;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (RedeWorld.slayerDeath >= 8)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("... I guess you like doing things the hard way.", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower || player.IsFullTBot())
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("... And for a heap of scrap no less.", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					else if (BasePlayer.HasAccessory(player, ModContent.ItemType<CrownOfTheKing>(), true, true))
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("... And for what? A bloody chicken!?", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
					else
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("... And for an annoying brat no less.", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
					}
				}
				if (base.npc.ai[2] == 430f && !Main.dedServ)
				{
					this.headType = 0;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Let's begin.", 70, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.npc.Center), 0, 0);
				}
				if (base.npc.ai[2] >= 500f)
				{
					if (RedeWorld.slayerDeath < 8)
					{
						RedeWorld.slayerDeath = 8;
					}
					base.npc.chaseable = true;
					this.phase = 5;
					this.chance = Utils.NextFloat(Main.rand, 0.5f, 1f);
					base.npc.ai[1] = 1f;
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 3f;
					base.npc.netUpdate = true;
				}
				break;
			}
			if (MoRDialogueUI.Visible)
			{
				Vector2? pointPos = Redemption.Inst.DialogueUIElement.PointPos;
				Vector2 center = base.npc.Center;
				if (pointPos != null && (pointPos == null || pointPos.GetValueOrDefault() == center))
				{
					if (Redemption.Inst.DialogueUIElement.ID == 0)
					{
						Redemption.Inst.DialogueUIElement.PointPos = new Vector2?(base.npc.Center);
						Redemption.Inst.DialogueUIElement.TextColor = new Color?(RedeColor.SlayerColour);
					}
					else
					{
						Redemption.Inst.DialogueUIElement.PointPos = null;
						Redemption.Inst.DialogueUIElement.TextColor = null;
					}
				}
			}
			if (Vector2.Distance(base.npc.Center, player.Center) >= 1100f && base.npc.ai[0] > 0f && !player.GetModPlayer<ScreenPlayer>().lockScreen)
			{
				if (base.npc.ai[1] == 3f && base.npc.ai[0] == 5f)
				{
					return;
				}
				this.teleportCount++;
				this.Teleport(false, Vector2.Zero);
				base.npc.netUpdate = true;
			}
		}

		public override bool CheckDead()
		{
			if (this.phase >= 5 || RedeConfigClient.Instance.NoLoreElements || base.npc.ai[0] == 12f)
			{
				return true;
			}
			if (base.npc.ai[0] == 10f)
			{
				base.npc.ai[2] = 0f;
				base.npc.ai[0] = 11f;
				base.npc.netUpdate = true;
			}
			base.npc.life = 1;
			base.npc.netUpdate = true;
			return false;
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
			this.teleGlow = true;
			this.teleGlowTimer = 0f;
			this.teleVector = base.npc.Center;
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
			this.teleVector = base.npc.Center;
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
			Texture2D texture = (this.phase < 5) ? Main.npcTexture[base.npc.type] : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Body");
			Texture2D chargeAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_Dash") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Body_Dash");
			Texture2D headAni = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Heads");
			Texture2D armsAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_NoGunIdle") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Arms_NoGunIdle");
			Texture2D armsCrossAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_Crossed") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Arms_Crossed");
			Texture2D armsGunAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_Rifle") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Arms_Rifle");
			Texture2D armsRocketFistAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_RocketFist") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Arms_RocketFist");
			Texture2D armsTossAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_FlashThrow") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_FlashThrow");
			Texture2D armsChargingAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_Charging") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Arms_Charging");
			Texture2D armsShrugAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_Shrug") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Arms_Shrug");
			Texture2D armsShieldAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Arms_BucklerShield") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Arms_BucklerShield");
			Texture2D fightIdleAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_MeleePrepare") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Body_MeleePrepare");
			Texture2D fightWheelKickAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_GuillotineWheelKick") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Body_GuillotineWheelKick");
			Texture2D fightShouderBashAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_ShoulderBash") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Body_ShoulderBash");
			Texture2D fightDropkickAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_Dropkick") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Body_Dropkick");
			Texture2D fightPummelAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_Pummel") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Body_Pummel");
			Texture2D fightJojoAni = (this.phase < 5) ? base.mod.GetTexture("NPCs/Bosses/KSIII/KS3_Body_Jojo") : base.mod.GetTexture("NPCs/Bosses/KSIII/Overclock/KS3_Body_Jojo");
			SpriteEffects effects = (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 drawCenter = base.npc.Center;
			if (base.npc.ai[3] < 11f)
			{
				if (base.npc.velocity.Length() < 13f)
				{
					spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
					if (base.npc.ai[0] == 1f || base.npc.ai[0] >= 6f)
					{
						Vector2 headCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y - 35f);
						int numHead = headAni.Height / 20;
						int yHead = numHead * this.headFrame;
						Main.spriteBatch.Draw(headAni, headCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, yHead, headAni.Width, numHead)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)headAni.Width / 2f, (float)numHead / 2f), base.npc.scale, effects, 0f);
					}
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
					Main.spriteBatch.Draw(chargeAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, chargeAni.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)chargeAni.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
				}
				if (base.npc.ai[3] == 0f)
				{
					int num215 = armsAni.Height / 4;
					int y7 = num215 * this.armFrames[0];
					Main.spriteBatch.Draw(armsAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, armsAni.Width, num215)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsAni.Width / 2f, (float)num215 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 1f)
				{
					int num216 = armsCrossAni.Height / 4;
					int y8 = num216 * this.armFrames[1];
					Main.spriteBatch.Draw(armsCrossAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, armsCrossAni.Width, num216)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsCrossAni.Width / 2f, (float)num216 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 2f || base.npc.ai[3] == 3f || base.npc.ai[3] == 4f)
				{
					int num217 = armsGunAni.Height / 9;
					int y9 = num217 * this.armFrames[2];
					Main.spriteBatch.Draw(armsGunAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, armsGunAni.Width, num217)), drawColor * ((float)(255 - base.npc.alpha) / 255f), this.gunRot + ((base.npc.spriteDirection == -1) ? 3.1415927f : 0f), new Vector2((float)armsGunAni.Width / 2f, (float)num217 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 5f)
				{
					int num218 = armsRocketFistAni.Height / 8;
					int y10 = num218 * this.armFrames[3];
					Main.spriteBatch.Draw(armsRocketFistAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y10, armsRocketFistAni.Width, num218)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsRocketFistAni.Width / 2f, (float)num218 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 6f)
				{
					int num219 = armsTossAni.Height / 6;
					int y11 = num219 * this.armFrames[4];
					Main.spriteBatch.Draw(armsTossAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y11, armsTossAni.Width, num219)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsTossAni.Width / 2f, (float)num219 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 7f)
				{
					int num220 = armsChargingAni.Height / 6;
					int y12 = num220 * this.armFrames[5];
					Main.spriteBatch.Draw(armsChargingAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y12, armsChargingAni.Width, num220)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsChargingAni.Width / 2f, (float)num220 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 8f)
				{
					int num221 = armsShrugAni.Height / 7;
					int y13 = num221 * this.armFrames[6];
					Main.spriteBatch.Draw(armsShrugAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y13, armsShrugAni.Width, num221)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsShrugAni.Width / 2f, (float)num221 / 2f), base.npc.scale, effects, 0f);
				}
				else if (base.npc.ai[3] == 9f || base.npc.ai[3] == 10f)
				{
					int num222 = armsShieldAni.Height / 5;
					int y14 = num222 * this.armFrames[7];
					Main.spriteBatch.Draw(armsShieldAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y14, armsShieldAni.Width, num222)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsShieldAni.Width / 2f, (float)num222 / 2f), base.npc.scale, effects, 0f);
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
					Main.spriteBatch.Draw(fightIdleAni, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
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
					Main.spriteBatch.Draw(fightWheelKickAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y15, fightWheelKickAni.Width, num223)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightWheelKickAni.Width / 2f, (float)num223 / 2f), base.npc.scale, effects, 0f);
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
					Main.spriteBatch.Draw(fightShouderBashAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y16, fightShouderBashAni.Width, num224)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightShouderBashAni.Width / 2f, (float)num224 / 2f), base.npc.scale, effects, 0f);
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
					Main.spriteBatch.Draw(fightDropkickAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y17, fightDropkickAni.Width, num225)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightDropkickAni.Width / 2f, (float)num225 / 2f), base.npc.scale, effects, 0f);
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
					Main.spriteBatch.Draw(fightPummelAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y18, fightPummelAni.Width, num226)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightPummelAni.Width / 2f, (float)num226 / 2f), base.npc.scale, effects, 0f);
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
					Main.spriteBatch.Draw(fightJojoAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y19, fightJojoAni.Width, num227)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)fightJojoAni.Width / 2f, (float)num227 / 2f), base.npc.scale, effects, 0f);
				}
			}
			return false;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D teleportGlow = base.mod.GetTexture("ExtraTextures/WhiteGlow");
			Rectangle rect2 = new Rectangle(0, 0, teleportGlow.Width, teleportGlow.Height);
			Vector2 origin2 = new Vector2((float)(teleportGlow.Width / 2), (float)(teleportGlow.Height / 2));
			Vector2 position3 = this.teleVector - Main.screenPosition;
			Color colour2 = Color.Lerp(Color.White, Color.Cyan, 1f / this.teleGlowTimer * 10f) * (1f / this.teleGlowTimer * 10f);
			if (this.teleGlow)
			{
				spriteBatch.Draw(teleportGlow, position3, new Rectangle?(rect2), colour2, base.npc.rotation, origin2, 2f, SpriteEffects.None, 0f);
				spriteBatch.Draw(teleportGlow, position3, new Rectangle?(rect2), colour2 * 0.4f, base.npc.rotation, origin2, 2f, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
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

		public int headFrame;

		public int headType;

		public float gunRot;

		public int[] armFrames = new int[8];

		public int fightFrames;

		public int phase;

		private bool title;

		private const float gunRotLimit = 1.5707964f;

		public int teleportCount;

		public float teleGlowTimer;

		public bool teleGlow;

		public Vector2 teleVector;
	}
}
