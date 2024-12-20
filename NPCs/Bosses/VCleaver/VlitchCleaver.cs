using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.HM;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.NPCs.Bosses.OmegaOblit;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.VCleaver
{
	[AutoloadBossHead]
	public class VlitchCleaver : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Cleaver");
			Main.npcFrameCount[base.npc.type] = 5;
			NPCID.Sets.TrailCacheLength[base.npc.type] = 6;
			NPCID.Sets.TrailingMode[base.npc.type] = 1;
		}

		public override void SetDefaults()
		{
			base.npc.width = 98;
			base.npc.height = 280;
			base.npc.friendly = false;
			base.npc.damage = 160;
			base.npc.defense = 60;
			base.npc.lifeMax = 55000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = 600f;
			base.npc.boss = true;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.bossBag = ModContent.ItemType<VlitchCleaverBag>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 80; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
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
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VlitchTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
			}
			else
			{
				if (Main.rand.Next(14) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GirusMask>(), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GirusDagger>(), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GirusLance>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(12, 24), false, 0, false, false);
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VlitchBattery>(), Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<Wielder>()))
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SwordRemote>(), 1, false, 0, false, false);
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			if (!RedeWorld.downedVlitch1)
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
								Main.NewText("<Chalice of Alignment> The first Vlitch Overlord is gone, only... 2 more to go? Maybe?", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gray, "+0", true, false);
					}
				}
			}
			RedeWorld.downedVlitch1 = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk1 && !NPC.AnyNPCs(ModContent.NPCType<VlitchWormHead>()) && !NPC.AnyNPCs(ModContent.NPCType<OO>()) && !RedeWorld.girusCloaked && !RedeConfigClient.Instance.NoLoreElements)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), ModContent.ProjectileType<GirusTalking1>(), 0, 0f, 255, 0f, 0f);
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override bool CheckActive()
		{
			Player player = Main.player[base.npc.target];
			return !player.active || player.dead || Main.dayTime;
		}

		public override void AI()
		{
			for (int i = base.npc.oldPos.Length - 1; i > 0; i--)
			{
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldrot[0] = base.npc.rotation;
			if (!this.title)
			{
				if (!Main.dedServ)
				{
					Redemption.Inst.TitleCardUIElement.DisplayTitle("Vlitch Cleaver", 60, 90, 0.8f, 0, new Color?(Color.Red), "1st Vlitch Overlord", true);
				}
				this.title = true;
			}
			NPC host = Main.npc[(int)base.npc.ai[3]];
			if (host.ai[3] == 5f || host.ai[3] == 9f || host.ai[3] == 10f || (host.ai[3] == 11f && (base.npc.ai[1] == 4f || base.npc.ai[1] == 8f)))
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 10.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 282;
					if (base.npc.frame.Y >= 1410)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 282;
					}
				}
			}
			else
			{
				base.npc.frame.Y = 0;
			}
			this.DespawnHandler();
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			Vector2 DefaultPos = new Vector2((float)((host.spriteDirection == 1) ? -180 : 180), -60f);
			Vector2 PosLeft = RedeHelper.PolarVector(-200f, host.rotation);
			Vector2 PosRight = RedeHelper.PolarVector(200f, host.rotation);
			Vector2 PosPlayer = new Vector2((float)((base.npc.Center.X > player.Center.X) ? 300 : -300), -80f);
			Vector2 PosPlayer2 = new Vector2((float)((base.npc.Center.X > player.Center.X) ? 600 : -600), -80f);
			Vector2 PosPlayer3 = new Vector2((float)((base.npc.Center.X > player.Center.X) ? 200 : -200), -160f);
			Vector2 PosPlayer3Check = new Vector2((base.npc.Center.X > player.Center.X) ? (player.Center.X + 200f) : (player.Center.X - 200f), player.Center.Y - 160f);
			if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<CleaverHitbox>()) && Main.netMode != 1)
			{
				Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CleaverHitbox>(), base.npc.damage / 3, 0f, Main.myPlayer, (float)base.npc.whoAmI, 0f);
			}
			if (base.npc.ai[0] >= 2f && base.npc.ai[0] < 10f)
			{
				base.npc.dontTakeDamage = false;
			}
			else
			{
				base.npc.dontTakeDamage = true;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<Wielder>()))
			{
				float obj = base.npc.ai[0];
				if (!0f.Equals(obj))
				{
					if (!1f.Equals(obj))
					{
						if (!2f.Equals(obj))
						{
							return;
						}
						if (host.ai[0] == 3f)
						{
							base.npc.ai[1] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.MoveToNPC(host, DefaultPos, 24f, 20f);
							if (base.npc.Distance(host.Center) < 200f || host.ai[3] == 2f)
							{
								host.ai[3] = 2f;
								ref this.rot.SlowRotation(0f, 0.05235988f);
								base.npc.rotation = this.rot;
							}
							else
							{
								base.npc.rotation += base.npc.velocity.X / 50f;
								this.rot = base.npc.rotation;
							}
						}
						float obj2 = host.ai[3];
						if (!3f.Equals(obj2))
						{
							if (!4f.Equals(obj2))
							{
								if (!5f.Equals(obj2))
								{
									if (!6f.Equals(obj2))
									{
										if (!7f.Equals(obj2))
										{
											if (!8f.Equals(obj2))
											{
												if (!9f.Equals(obj2))
												{
													if (!10f.Equals(obj2))
													{
														if (!11f.Equals(obj2))
														{
															if (!20f.Equals(obj2))
															{
																if (!21f.Equals(obj2))
																{
																	if (!22f.Equals(obj2))
																	{
																		return;
																	}
																	if (base.npc.life < (int)((float)base.npc.lifeMax * 0.9f))
																	{
																		host.ai[2] = 1f;
																		return;
																	}
																	host.ai[2] = -1f;
																	return;
																}
																else
																{
																	if (base.npc.life < (int)((float)base.npc.lifeMax * 0.3f))
																	{
																		host.ai[2] = 1f;
																		return;
																	}
																	host.ai[2] = -1f;
																	return;
																}
															}
															else
															{
																if (base.npc.life < (int)((float)base.npc.lifeMax * 0.75f))
																{
																	host.ai[2] = 1f;
																	return;
																}
																host.ai[2] = -1f;
																return;
															}
														}
														else
														{
															float obj3 = base.npc.ai[1];
															if (0f.Equals(obj3))
															{
																base.npc.ai[2] = 0f;
																base.npc.ai[1] = 1f;
																base.npc.netUpdate = true;
																return;
															}
															if (!1f.Equals(obj3))
															{
																if (!2f.Equals(obj3))
																{
																	if (!3f.Equals(obj3))
																	{
																		if (!4f.Equals(obj3))
																		{
																			if (!5f.Equals(obj3))
																			{
																				if (!6f.Equals(obj3))
																				{
																					if (!7f.Equals(obj3))
																					{
																						if (!8f.Equals(obj3))
																						{
																							if (!9f.Equals(obj3))
																							{
																								if (!10f.Equals(obj3))
																								{
																									return;
																								}
																								base.npc.ai[2] += 1f;
																								if (base.npc.ai[2] == 1f)
																								{
																									base.npc.velocity.X = (float)((this.repeat == 0) ? 17 : -17);
																								}
																								if (base.npc.ai[2] == 16f)
																								{
																									base.npc.velocity.X = (float)((this.repeat == 0) ? -17 : 17);
																								}
																								base.npc.velocity.Y = 20f;
																								base.npc.rotation += ((this.repeat == 0) ? 0.12f : -0.12f);
																								if (base.npc.ai[2] > 30f)
																								{
																									this.repeat = 0;
																									base.npc.velocity *= 0f;
																									host.ai[2] = 1000f;
																									base.npc.ai[2] = 0f;
																									base.npc.netUpdate = true;
																									return;
																								}
																							}
																							else
																							{
																								base.npc.ai[2] += 1f;
																								ref this.rot.SlowRotation((this.repeat == 0) ? 0.78f : 5.49f, 0.15707964f);
																								base.npc.rotation = this.rot;
																								if (base.npc.ai[2] > 10f)
																								{
																									host.ai[2] = 200f;
																									Main.PlaySound(SoundID.Item71, base.npc.position);
																									base.npc.rotation = ((player.Center.X > base.npc.Center.X) ? 0.78f : 5.49f);
																									base.npc.ai[1] = 10f;
																									base.npc.ai[2] = 0f;
																									base.npc.netUpdate = true;
																									return;
																								}
																							}
																						}
																						else
																						{
																							ref this.rot.SlowRotation(0f, 0.15707964f);
																							base.npc.rotation = this.rot;
																							if (base.npc.rotation == 0f)
																							{
																								if (base.npc.ai[2] == 0f)
																								{
																									Main.PlaySound(SoundID.Item74, base.npc.position);
																									base.npc.ai[2] = 1f;
																									base.npc.netUpdate = true;
																								}
																								base.npc.velocity.Y = -26f;
																								if (base.npc.Center.Y < player.Center.Y - 160f)
																								{
																									base.npc.velocity *= 0f;
																									base.npc.ai[2] = 0f;
																									base.npc.ai[1] = 9f;
																									base.npc.netUpdate = true;
																									return;
																								}
																							}
																						}
																					}
																					else
																					{
																						base.npc.ai[2] += 1f;
																						if (base.npc.ai[2] == 1f)
																						{
																							base.npc.velocity.X = (float)((this.repeat == 0) ? 15 : -15);
																						}
																						if (base.npc.ai[2] == 11f)
																						{
																							base.npc.velocity.X = (float)((this.repeat == 0) ? -15 : 15);
																						}
																						base.npc.velocity.Y = 26f;
																						base.npc.rotation += ((this.repeat == 0) ? 0.1f : -0.1f);
																						if (base.npc.ai[2] > 20f)
																						{
																							if (base.npc.life < (int)((float)base.npc.lifeMax * 0.6f))
																							{
																								this.rot = base.npc.rotation;
																								base.npc.velocity *= 0f;
																								base.npc.ai[2] = 0f;
																								base.npc.ai[1] = 8f;
																								base.npc.netUpdate = true;
																								return;
																							}
																							this.repeat = 0;
																							base.npc.velocity *= 0f;
																							host.ai[2] = 1000f;
																							base.npc.ai[2] = 0f;
																							base.npc.netUpdate = true;
																							return;
																						}
																					}
																				}
																				else
																				{
																					base.npc.ai[2] += 1f;
																					base.npc.velocity *= 0f;
																					if (base.npc.ai[2] > 10f)
																					{
																						host.ai[2] = 200f;
																						Main.PlaySound(SoundID.Item71, base.npc.position);
																						base.npc.ai[1] = 7f;
																						base.npc.ai[2] = 0f;
																						base.npc.netUpdate = true;
																						return;
																					}
																				}
																			}
																			else
																			{
																				base.npc.ai[2] += 1f;
																				if (base.npc.ai[2] < 30f)
																				{
																					base.npc.rotation += base.npc.velocity.X / 60f;
																					this.rot = base.npc.rotation;
																				}
																				else
																				{
																					ref this.rot.SlowRotation((this.repeat == 0) ? 5.49f : 0.78f, 0.15707964f);
																					base.npc.rotation = this.rot;
																				}
																				if (base.npc.Distance(PosPlayer3Check) < 30f || base.npc.ai[2] > 60f)
																				{
																					base.npc.rotation = ((this.repeat == 0) ? 5.49f : 0.78f);
																					this.repeat = ((player.Center.X > base.npc.Center.X) ? 0 : 1);
																					base.npc.ai[1] = 6f;
																					base.npc.ai[2] = 0f;
																					base.npc.netUpdate = true;
																					return;
																				}
																				base.npc.Move(PosPlayer3, (float)((base.npc.Distance(player.Center) < 700f) ? 16 : 35), 3f, true);
																				return;
																			}
																		}
																		else
																		{
																			base.npc.ai[2] += 1f;
																			base.npc.velocity.X = (float)((this.repeat == 0) ? 40 : -40);
																			base.npc.rotation = (float)Math.Atan2((double)base.npc.velocity.Y, (double)base.npc.velocity.X) + 1.57f;
																			if (this.repeat == 0)
																			{
																				if (base.npc.Center.X > player.Center.X + 200f)
																				{
																					base.npc.ai[1] = 5f;
																					base.npc.ai[2] = 0f;
																					base.npc.netUpdate = true;
																					return;
																				}
																			}
																			else if (base.npc.Center.X < player.Center.X - 200f)
																			{
																				base.npc.ai[1] = 5f;
																				base.npc.ai[2] = 0f;
																				base.npc.netUpdate = true;
																				return;
																			}
																		}
																	}
																	else
																	{
																		base.npc.ai[2] += 1f;
																		if (base.npc.ai[2] == 1f)
																		{
																			base.npc.velocity.X = (float)((this.repeat == 0) ? 15 : -15);
																		}
																		if (base.npc.ai[2] == 11f)
																		{
																			base.npc.velocity.X = (float)((this.repeat == 0) ? -15 : 15);
																		}
																		base.npc.velocity.Y = 26f;
																		base.npc.rotation += ((this.repeat == 0) ? 0.1f : -0.1f);
																		if (base.npc.ai[2] > 20f)
																		{
																			host.ai[2] = 300f;
																			Main.PlaySound(SoundID.Item74, base.npc.position);
																			this.rot = base.npc.rotation;
																			base.npc.velocity *= 0f;
																			base.npc.ai[1] = 4f;
																			base.npc.ai[2] = 0f;
																			base.npc.netUpdate = true;
																			return;
																		}
																	}
																}
																else
																{
																	base.npc.ai[2] += 1f;
																	base.npc.velocity *= 0f;
																	if (base.npc.ai[2] > 10f)
																	{
																		Main.PlaySound(SoundID.Item71, base.npc.position);
																		host.ai[2] = 200f;
																		base.npc.ai[1] = 3f;
																		base.npc.ai[2] = 0f;
																		base.npc.netUpdate = true;
																		return;
																	}
																}
															}
															else
															{
																base.npc.ai[2] += 1f;
																ref this.rot.SlowRotation((player.Center.X > base.npc.Center.X) ? 0.78f : 5.49f, 0.15707964f);
																base.npc.rotation = this.rot;
																if (base.npc.Distance(PosPlayer3Check) < 30f || base.npc.ai[2] > 60f)
																{
																	base.npc.rotation = ((player.Center.X > base.npc.Center.X) ? 0.78f : 5.49f);
																	this.repeat = ((player.Center.X > base.npc.Center.X) ? 0 : 1);
																	base.npc.ai[1] = 2f;
																	base.npc.ai[2] = 0f;
																	base.npc.netUpdate = true;
																	return;
																}
																base.npc.Move(PosPlayer3, (float)((base.npc.Distance(player.Center) < 700f) ? 16 : 35), 3f, true);
																return;
															}
														}
													}
													else
													{
														float obj3 = base.npc.ai[1];
														if (0f.Equals(obj3))
														{
															base.npc.ai[2] = 0f;
															base.npc.ai[1] = 1f;
															base.npc.netUpdate = true;
															return;
														}
														if (!1f.Equals(obj3))
														{
															if (!2f.Equals(obj3))
															{
																return;
															}
															base.npc.ai[2] += 1f;
															if (base.npc.ai[2] < 50f)
															{
																base.npc.velocity *= 0.9f;
															}
															if (base.npc.ai[2] == 20f)
															{
																base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<CleaverClone2_Spawner>(), base.npc.damage, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
															}
															if (base.npc.ai[2] == 50f)
															{
																base.npc.velocity.Y = 20f;
															}
															if (base.npc.Center.Y > player.Center.Y + 1000f)
															{
																base.npc.velocity *= 0f;
																host.ai[2] = 800f;
																base.npc.netUpdate = true;
																return;
															}
														}
														else
														{
															base.npc.ai[2] += 1f;
															if (base.npc.ai[2] < 60f)
															{
																base.npc.rotation += base.npc.velocity.X / 30f;
																this.rot = base.npc.rotation;
															}
															else
															{
																ref this.rot.SlowRotation(3.1415927f, 0.15707964f);
																base.npc.rotation = this.rot;
															}
															if (base.npc.ai[2] > 80f)
															{
																base.npc.rotation = 3.1415927f;
																base.npc.ai[1] = 2f;
																base.npc.ai[2] = 0f;
																base.npc.netUpdate = true;
																return;
															}
															base.npc.Move(new Vector2(0f, -400f), (float)((base.npc.Distance(player.Center) < 700f) ? 18 : 35), 3f, true);
															return;
														}
													}
												}
												else if (base.npc.ai[1] == 0f)
												{
													base.npc.ai[2] = 0f;
													if (player.Center.X > base.npc.Center.X)
													{
														base.npc.ai[1] = 1f;
													}
													else
													{
														base.npc.ai[1] = 2f;
													}
													if (!Main.dedServ)
													{
														Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/NebSound2").WithVolume(0.9f), (int)base.npc.position.X, (int)base.npc.position.Y);
														return;
													}
												}
												else
												{
													base.npc.ai[2] += 1f;
													if (base.npc.ai[2] >= 100f)
													{
														if (base.npc.ai[1] == 1f)
														{
															base.npc.rotation += 0.01f;
														}
														else
														{
															base.npc.rotation -= 0.01f;
														}
														base.npc.rotation *= 1.01f;
														base.npc.velocity *= 0.9f;
														return;
													}
													ref this.rot.SlowRotation(0f, 0.07853982f);
													base.npc.rotation = this.rot;
													if (base.npc.Distance(PosPlayer) < 300f || base.npc.ai[2] > 40f)
													{
														base.npc.rotation = 0f;
														base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(134f, base.npc.rotation + -1.5707964f), ModContent.ProjectileType<RedPrism>(), base.npc.damage, RedeHelper.PolarVector(9f, base.npc.rotation + -1.5707964f), false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
														base.npc.ai[2] = 100f;
														return;
													}
													base.npc.Move(PosPlayer, 18f, 5f, true);
													return;
												}
											}
											else
											{
												if ((base.npc.ai[2] == 0f || base.npc.ai[2] >= 100f) && base.npc.ai[1] == 0f)
												{
													base.npc.ai[2] = 1f;
													base.npc.ai[1] = 1f;
													base.npc.netUpdate = true;
													return;
												}
												base.npc.ai[2] += 1f;
												ref this.rot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(player.Center)) + 1.57f, 0.10471976f);
												base.npc.rotation = this.rot;
												if (base.npc.ai[2] < 100f)
												{
													if (base.npc.Distance(PosPlayer2) < 300f || base.npc.ai[2] > 80f)
													{
														base.npc.ai[2] = 100f;
														base.npc.velocity *= 0.96f;
														return;
													}
													base.npc.Move(PosPlayer2, 15f, 5f, true);
													return;
												}
												else
												{
													base.npc.velocity *= 0.96f;
													if (base.npc.ai[2] >= 130f && base.npc.ai[2] % 5f == 0f && base.npc.ai[2] < 200f)
													{
														base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<CleaverClone1>(), base.npc.damage, new Vector2(Utils.NextFloat(Main.rand, -6f, 7f), Utils.NextFloat(Main.rand, -6f, 7f)), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
														return;
													}
												}
											}
										}
										else
										{
											this.rot = base.npc.rotation;
											base.npc.rotation = Utils.ToRotation(base.npc.DirectionTo(host.Center)) - 1.57f;
											if ((base.npc.ai[2] == 0f || base.npc.ai[2] >= 100f) && base.npc.ai[1] == 0f)
											{
												Main.PlaySound(SoundID.Item71, base.npc.position);
												base.npc.velocity = Utils.RotatedBy(base.npc.DirectionTo(host.Center), (base.npc.spriteDirection == 1) ? -1.5707963267948966 : 1.5707963267948966, default(Vector2)) * 30f;
												base.npc.ai[2] = 1f;
												base.npc.ai[1] = 1f;
												return;
											}
											base.npc.ai[2] += 1f;
											if (base.npc.ai[2] < 40f)
											{
												base.npc.velocity -= Utils.RotatedBy(base.npc.velocity, 1.5707963267948966, default(Vector2)) * base.npc.velocity.Length() / base.npc.Distance(host.Center);
												return;
											}
											base.npc.velocity *= 0.7f;
											return;
										}
									}
									else
									{
										if (base.npc.ai[2] == 0f || base.npc.ai[2] >= 100f)
										{
											base.npc.ai[2] = 1f;
											base.npc.netUpdate = true;
											return;
										}
										base.npc.ai[2] += 1f;
										if (base.npc.ai[2] % 10f == 0f)
										{
											base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(134f, base.npc.rotation + -1.5707964f), ModContent.ProjectileType<OmegaBlast3>(), 92, RedeHelper.PolarVector(5f, base.npc.rotation + -1.5707964f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
										}
										if (base.npc.ai[2] < 60f)
										{
											base.npc.MoveToNPC(host, PosLeft, 18f, 20f);
										}
										else
										{
											base.npc.MoveToNPC(host, PosRight, 18f, 20f);
										}
										if (base.npc.ai[2] > 120f)
										{
											base.npc.ai[2] = 1f;
											base.npc.netUpdate = true;
										}
										base.npc.rotation += base.npc.velocity.X / 30f;
										return;
									}
								}
								else
								{
									if (base.npc.ai[2] == 0f || base.npc.ai[2] >= 100f)
									{
										base.npc.ai[2] = 1f;
										base.npc.netUpdate = true;
										return;
									}
									base.npc.ai[2] += 1f;
									if (base.npc.ai[2] < 50f)
									{
										base.npc.velocity *= 0.94f;
										ref this.rot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(player.Center)) + 1.57f, 0.10471976f);
										base.npc.rotation = this.rot;
									}
									else if (base.npc.ai[2] <= 70f)
									{
										base.npc.rotation = (float)Math.Atan2((double)base.npc.velocity.Y, (double)base.npc.velocity.X) + 1.57f;
									}
									if (base.npc.ai[2] == 50f)
									{
										base.npc.Dash(40, true, SoundID.Item74, player.Center);
									}
									if (base.npc.ai[2] == 70f && RedeHelper.Chance(0.4f))
									{
										host.ai[2] = 60f;
										base.npc.ai[2] = 0f;
										base.npc.netUpdate = true;
									}
									if (base.npc.ai[2] > 70f)
									{
										base.npc.velocity *= 0.94f;
										return;
									}
								}
							}
							else
							{
								this.rot = base.npc.rotation;
								base.npc.rotation = Utils.ToRotation(base.npc.DirectionTo(host.Center)) - 1.57f;
								if (base.npc.ai[2] == 0f || base.npc.ai[2] >= 100f)
								{
									Main.PlaySound(SoundID.Item71, base.npc.position);
									base.npc.velocity = Utils.RotatedBy(base.npc.DirectionTo(host.Center), (base.npc.spriteDirection == 1) ? -1.5707963267948966 : 1.5707963267948966, default(Vector2)) * 40f;
									base.npc.ai[2] = 1f;
									return;
								}
								base.npc.ai[2] += 1f;
								if (base.npc.ai[2] < 20f)
								{
									base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y) + RedeHelper.PolarVector(134f, base.npc.rotation + -1.5707964f), ModContent.ProjectileType<OmegaBlast3>(), 92, RedeHelper.PolarVector(2f, base.npc.rotation + -1.5707964f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
									base.npc.velocity -= Utils.RotatedBy(base.npc.velocity, 1.5707963267948966, default(Vector2)) * base.npc.velocity.Length() / base.npc.Distance(host.Center);
									return;
								}
								base.npc.velocity *= 0.7f;
								return;
							}
						}
						else
						{
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] < 80f)
							{
								base.npc.MoveToNPC(host, DefaultPos, 8f, 2f);
								ref this.rot.SlowRotation(Utils.ToRotation(base.npc.DirectionTo(host.Center)) - 1.57f, 0.10471976f);
								base.npc.rotation = this.rot;
								return;
							}
							base.npc.MoveToNPC(host, DefaultPos, 16f, 1f);
							base.npc.ai[2] = 100f;
							base.npc.rotation = Utils.ToRotation(base.npc.DirectionTo(host.Center)) - 1.57f;
							return;
						}
					}
					else
					{
						base.npc.ai[2] += 1f;
						player.GetModPlayer<ScreenPlayer>().Rumble(20, 7);
						this.rot = base.npc.rotation;
						if (base.npc.ai[2] > 20f)
						{
							base.npc.ai[0] = 2f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
					}
				}
				else
				{
					base.npc.rotation = ((host.spriteDirection == 1) ? -1.5707964f : 1.5707964f);
					base.npc.velocity.X = (float)((host.spriteDirection == 1) ? 40 : -40);
					if (base.npc.Distance(host.Center) < 200f)
					{
						for (int j = 0; j < 30; j++)
						{
							int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 3f);
							Main.dust[dustIndex].velocity *= 10f;
							Main.dust[dustIndex].noGravity = true;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						host.ai[3] = 1f;
						base.npc.velocity *= 0f;
						base.npc.ai[0] = 1f;
						base.npc.netUpdate = true;
						return;
					}
				}
			}
			else if (player.active && !player.dead)
			{
				if (base.npc.ai[0] < 10f)
				{
					base.npc.ai[0] = 10f;
					base.npc.ai[2] = 0f;
					this.rot = base.npc.rotation;
					base.npc.netUpdate = true;
					return;
				}
				player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				base.npc.ai[2] += 1f;
				ref this.rot.SlowRotation(0f, 0.05235988f);
				base.npc.rotation = this.rot;
				base.npc.velocity *= 0.9f;
				base.npc.velocity = new Vector2(Utils.NextFloat(Main.rand, -2f, 2f), Utils.NextFloat(Main.rand, -2f, 2f));
				if (base.npc.ai[2] == 60f)
				{
					base.npc.life = 1;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 120f)
				{
					base.npc.dontTakeDamage = false;
					player.ApplyDamageToNPC(base.npc, 9999, 0f, 0, false);
					base.npc.netUpdate = true;
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
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
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/VCleaver/VlitchCleaver_Glow");
			Texture2D trail = base.mod.GetTexture("NPCs/Bosses/VCleaver/VlitchCleaver_Trail");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Rectangle rectangle = base.npc.frame;
			Vector2 origin2 = Utils.Size(rectangle) / 2f;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			for (int i = 0; i < NPCID.Sets.TrailCacheLength[base.npc.type]; i++)
			{
				Vector2 value4 = base.npc.oldPos[i];
				Main.spriteBatch.Draw(trail, value4 + base.npc.Size / 2f - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY), new Rectangle?(rectangle), RedeColor.VlitchGlowColour * 0.5f, this.oldrot[i], origin2, base.npc.scale, effects, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), RedeColor.COLOR_WHITEFADE2, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public override void BossHeadRotation(ref float rotation)
		{
			rotation = base.npc.rotation;
		}

		public float[] oldrot = new float[6];

		public int floatTimer;

		public float rot;

		public float dist;

		public int repeat;

		private bool title;
	}
}
