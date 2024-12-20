using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PostML.Druid.Staves;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	[AutoloadBossHead]
	public class WardenIdle : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Warden");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 260000;
			base.npc.damage = 170;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 72;
			base.npc.height = 102;
			base.npc.value = (float)Item.buyPrice(0, 10, 0, 0);
			base.npc.npcSlots = 1f;
			base.npc.boss = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.alpha = 255;
			base.npc.dontTakeDamage = true;
			base.npc.netAlways = true;
			base.npc.HitSound = SoundID.NPCHit49;
			base.npc.DeathSound = SoundID.NPCDeath51;
			base.npc.chaseable = true;
			this.bossBag = ModContent.ItemType<TheWardenBag>();
			for (int i = 0; i < base.npc.buffImmune.Length; i++)
			{
				base.npc.buffImmune[i] = true;
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			if (!RedeWorld.downedTheWarden)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						Main.NewText("There is nothing past this boss... Yet.", Color.White, false);
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
							{
								Main.NewText("<Chalice of Alignment> Slaying a soulless is all well and good, but they can't escape this dimension so his defeat does not affect Epidotra.", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gray, "+0", true, false);
					}
				}
			}
			RedeWorld.downedTheWarden = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<WardenTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<WardenMask>(), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(2);
			if (num != 0)
			{
				if (num == 1)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ShadeCandleSpear>(), 1, false, 0, false, false);
				}
			}
			else
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ShadeStave>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VesselFrag>(), Main.rand.Next(13, 24), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<WardensKey>(), 1, false, 0, false, false);
		}

		public override bool CheckActive()
		{
			Player player = Main.player[base.npc.target];
			return !player.active || player.dead || Vector2.Distance(base.npc.Center, player.Center) > 3000f;
		}

		private void DespawnHandler()
		{
			Player player = Main.player[base.npc.target];
			if (!player.active || player.dead)
			{
				base.npc.alpha += 10;
				if (base.npc.alpha >= 255)
				{
					base.npc.velocity = new Vector2(0f, -20f);
				}
				if (base.npc.timeLeft > 10)
				{
					base.npc.timeLeft = 10;
				}
				return;
			}
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.repeat);
				writer.Write(this.ID);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.repeat = reader.ReadBool();
				this.ID = reader.ReadInt32();
			}
		}

		public int ID
		{
			get
			{
				return (int)base.npc.ai[0];
			}
			set
			{
				base.npc.ai[0] = (float)value;
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

		private void AttackChoice2()
		{
			int attempts = 0;
			while (attempts == 0)
			{
				if (this.CopyList2 == null || this.CopyList2.Count == 0)
				{
					this.CopyList2 = new List<int>(this.AttackList2);
				}
				this.ID = this.CopyList2[Main.rand.Next(0, this.CopyList2.Count)];
				this.CopyList2.Remove(this.ID);
				base.npc.netUpdate = true;
				if ((this.ID != 33 || !RedeHelper.AnyProjectiles(ModContent.ProjectileType<GiantMask>())) && (this.ID != 34 || (NPC.CountNPCS(ModContent.NPCType<CagedSoul>()) <= 0 && !RedeHelper.AnyProjectiles(ModContent.ProjectileType<GiantMask>()))) && (this.ID != 35 || !RedeHelper.AnyProjectiles(ModContent.ProjectileType<GiantMask>())))
				{
					attempts++;
				}
			}
		}

		public override void AI()
		{
			if (this.floatTimer == 0)
			{
				NPC npc = base.npc;
				npc.velocity.Y = npc.velocity.Y + 0.007f;
				if (base.npc.velocity.Y > 0.3f)
				{
					this.floatTimer = 1;
					base.npc.netUpdate = true;
				}
			}
			else if (this.floatTimer == 1)
			{
				NPC npc2 = base.npc;
				npc2.velocity.Y = npc2.velocity.Y - 0.007f;
				if (base.npc.velocity.Y < -0.3f)
				{
					this.floatTimer = 0;
					base.npc.netUpdate = true;
				}
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			switch (this.aniType)
			{
			case 0:
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc3 = base.npc;
					npc3.frame.Y = npc3.frame.Y + 108;
					if (base.npc.frame.Y >= 432)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				break;
			case 1:
				this.frameCounters++;
				if (this.frameCounters >= 5)
				{
					this.swingFrame++;
					this.frameCounters = 0;
				}
				if (this.swingFrame >= 12)
				{
					this.swingFrame = 0;
					this.aniType = 0;
				}
				break;
			case 2:
				this.frameCounters++;
				if (this.frameCounters >= 5)
				{
					this.burstFrame++;
					this.frameCounters = 0;
				}
				if (this.burstFrame >= 5)
				{
					this.burstFrame = 0;
					this.aniType = 0;
				}
				break;
			}
			this.DespawnHandler();
			if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<WardenBall>()))
			{
				base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<WardenBall>(), 130, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
			}
			player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
			if (base.npc.alpha < 200 && ((base.npc.ai[0] > 2f && base.npc.ai[0] < 20f) || base.npc.ai[0] > 25f))
			{
				base.npc.dontTakeDamage = false;
			}
			else
			{
				base.npc.dontTakeDamage = true;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.8f) && base.npc.ai[3] < 1f && !this.finalPhase)
			{
				base.npc.ai[3] = 1f;
				base.npc.netUpdate = true;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.6f) && base.npc.ai[3] < 2f && !this.finalPhase)
			{
				base.npc.ai[3] = 2f;
				base.npc.netUpdate = true;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.4f) && base.npc.ai[3] < 3f && !this.finalPhase)
			{
				base.npc.ai[3] = 3f;
				base.npc.netUpdate = true;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.2f) && base.npc.ai[3] < 4f && !this.finalPhase)
			{
				base.npc.ai[3] = 4f;
				base.npc.netUpdate = true;
			}
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
								if (!5f.Equals(obj))
								{
									if (!6f.Equals(obj))
									{
										if (!7f.Equals(obj))
										{
											if (!23f.Equals(obj))
											{
												if (!24f.Equals(obj))
												{
													if (!25f.Equals(obj))
													{
														if (!26f.Equals(obj))
														{
															if (!27f.Equals(obj))
															{
																if (!28f.Equals(obj))
																{
																	if (!29f.Equals(obj))
																	{
																		if (!30f.Equals(obj))
																		{
																			if (!31f.Equals(obj))
																			{
																				if (!32f.Equals(obj))
																				{
																					if (!33f.Equals(obj))
																					{
																						if (!34f.Equals(obj))
																						{
																							if (!35f.Equals(obj))
																							{
																								return;
																							}
																							float obj2 = base.npc.ai[1];
																							if (0f.Equals(obj2))
																							{
																								this.Teleport(true, new Vector2((float)((player.Center.X < 17616f) ? 100 : -100), -100f));
																								player.AddBuff(ModContent.BuffType<Stunned2Debuff>(), 60, true);
																								for (int i = 0; i < 40; i++)
																								{
																									double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
																									this.vector.X = (float)(Math.Sin(angle) * 100.0);
																									this.vector.Y = (float)(Math.Cos(angle) * 100.0);
																									Dust dust2 = Main.dust[Dust.NewDust(player.Center + this.vector, 2, 2, 261, 0f, 0f, 100, default(Color), 3f)];
																									dust2.noGravity = true;
																									dust2.velocity = -player.DirectionTo(dust2.position) * 10f;
																								}
																								base.npc.ai[1] = 1f;
																								base.npc.velocity *= 0f;
																								base.npc.netUpdate = true;
																								return;
																							}
																							if (!1f.Equals(obj2))
																							{
																								if (!2f.Equals(obj2))
																								{
																									return;
																								}
																								base.npc.LookAtPlayer();
																								if (base.npc.ai[2] < 60f)
																								{
																									base.npc.MoveToVector2(new Vector2((player.Center.X < 17616f) ? (player.Center.X + 100f) : (player.Center.X - 100f), player.Center.Y - 100f), 10f);
																								}
																								else
																								{
																									base.npc.MoveToVector2(new Vector2(player.Center.X - 300f, player.Center.Y - 100f), 10f);
																								}
																								base.npc.ai[2] += 1f;
																								if (base.npc.ai[2] < 35f)
																								{
																									int dustIndex = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y - 34f), 10, 10, 261, 0f, 0f, 100, default(Color), 2f);
																									Dust dust6 = Main.dust[dustIndex];
																									dust6.noGravity = true;
																									dust6.velocity = base.npc.DirectionTo(player.Center) * 8f;
																								}
																								if (base.npc.ai[2] == 30f)
																								{
																									for (int j = 0; j < 20; j++)
																									{
																										double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
																										this.vector.X = (float)(Math.Sin(angle2) * 60.0);
																										this.vector.Y = (float)(Math.Cos(angle2) * 60.0);
																										Dust dust3 = Main.dust[Dust.NewDust(player.Center + this.vector, 2, 2, 261, 0f, 0f, 100, default(Color), 3f)];
																										dust3.noGravity = true;
																										dust3.velocity = -player.DirectionTo(dust3.position) * 5f;
																										int dustIndex2 = Dust.NewDust(player.position, player.width, player.height, 261, 0f, 0f, 100, default(Color), 2f);
																										Dust dust7 = Main.dust[dustIndex2];
																										dust7.noGravity = true;
																										dust7.velocity.Y = -15f;
																									}
																								}
																								if (base.npc.ai[2] == 35f)
																								{
																									for (int k = 0; k < 20; k++)
																									{
																										int dustIndex3 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 20, 0f, 0f, 100, default(Color), 4f);
																										Main.dust[dustIndex3].velocity *= 10f;
																									}
																									base.npc.SpawnNPC((int)player.Center.X, (int)player.Center.Y - 200, ModContent.NPCType<PlayerMarionette>(), (float)player.whoAmI, 0f, 0f, 0f);
																								}
																								if (base.npc.ai[2] > 200f && !RedeHelper.AnyProjectiles(ModContent.ProjectileType<ShadowMaw>()))
																								{
																									base.npc.ai[2] = 0f;
																									base.npc.ai[1] = 0f;
																									base.npc.ai[0] = 26f;
																									base.npc.netUpdate = true;
																								}
																							}
																							else
																							{
																								base.npc.LookAtPlayer();
																								base.npc.alpha -= 10;
																								if (base.npc.alpha <= 0)
																								{
																									base.npc.ai[1] = 2f;
																									base.npc.netUpdate = true;
																									return;
																								}
																							}
																						}
																						else
																						{
																							float obj2 = base.npc.ai[1];
																							if (0f.Equals(obj2))
																							{
																								this.Teleport(true, new Vector2((float)((player.Center.X < 17616f) ? 100 : -100), 0f));
																								player.AddBuff(ModContent.BuffType<Stunned2Debuff>(), 180, true);
																								for (int l = 0; l < 40; l++)
																								{
																									double angle3 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
																									this.vector.X = (float)(Math.Sin(angle3) * 100.0);
																									this.vector.Y = (float)(Math.Cos(angle3) * 100.0);
																									Dust dust4 = Main.dust[Dust.NewDust(player.Center + this.vector, 2, 2, 261, 0f, 0f, 100, default(Color), 3f)];
																									dust4.noGravity = true;
																									dust4.velocity = -player.DirectionTo(dust4.position) * 10f;
																								}
																								base.npc.ai[1] = 1f;
																								base.npc.velocity *= 0f;
																								base.npc.netUpdate = true;
																								return;
																							}
																							if (!1f.Equals(obj2))
																							{
																								if (!2f.Equals(obj2))
																								{
																									return;
																								}
																								base.npc.LookAtPlayer();
																								base.npc.MoveToVector2(new Vector2((player.Center.X < 17616f) ? (player.Center.X + 100f) : (player.Center.X - 100f), player.Center.Y), 10f);
																								base.npc.ai[2] += 1f;
																								if (base.npc.ai[2] < 35f)
																								{
																									int dustIndex4 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y - 34f), 10, 10, 261, 0f, 0f, 100, default(Color), 2f);
																									Dust dust8 = Main.dust[dustIndex4];
																									dust8.noGravity = true;
																									dust8.velocity = base.npc.DirectionTo(player.Center) * 8f;
																								}
																								if (base.npc.ai[2] == 30f)
																								{
																									for (int m = 0; m < 20; m++)
																									{
																										double angle4 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
																										this.vector.X = (float)(Math.Sin(angle4) * 60.0);
																										this.vector.Y = (float)(Math.Cos(angle4) * 60.0);
																										Dust dust5 = Main.dust[Dust.NewDust(player.Center + this.vector, 2, 2, 261, 0f, 0f, 100, default(Color), 3f)];
																										dust5.noGravity = true;
																										dust5.velocity = -player.DirectionTo(dust5.position) * 5f;
																									}
																								}
																								if (base.npc.ai[2] == 35f)
																								{
																									for (int n = 0; n < 20; n++)
																									{
																										int dustIndex5 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 20, 0f, 0f, 100, default(Color), 4f);
																										Main.dust[dustIndex5].velocity *= 10f;
																									}
																									for (int i2 = 0; i2 < 4; i2++)
																									{
																										base.npc.Shoot(player.Center, ModContent.ProjectileType<PlayerSoulPro>(), 0, RedeHelper.PolarVector((float)Main.rand.Next(9, 11), Utils.NextFloat(Main.rand, 0f, 6.2831855f)), false, SoundID.NPCDeath39, "", 0f, (float)player.whoAmI);
																									}
																									player.AddBuff(ModContent.BuffType<SoulSplitDebuff>(), 600, true);
																								}
																								if (base.npc.ai[2] > 180f)
																								{
																									base.npc.ai[2] = 0f;
																									base.npc.ai[1] = 0f;
																									base.npc.ai[0] = 26f;
																									base.npc.netUpdate = true;
																									return;
																								}
																							}
																							else
																							{
																								base.npc.LookAtPlayer();
																								base.npc.alpha -= 10;
																								if (base.npc.alpha <= 0)
																								{
																									base.npc.ai[1] = 2f;
																									base.npc.netUpdate = true;
																									return;
																								}
																							}
																						}
																					}
																					else
																					{
																						float obj2 = base.npc.ai[1];
																						if (0f.Equals(obj2))
																						{
																							this.Teleport(false, Vector2.Zero);
																							base.npc.ai[1] = 1f;
																							base.npc.velocity *= 0f;
																							base.npc.netUpdate = true;
																							return;
																						}
																						if (!1f.Equals(obj2))
																						{
																							if (!2f.Equals(obj2))
																							{
																								return;
																							}
																							base.npc.LookAtPlayer();
																							base.npc.velocity *= 0.96f;
																							base.npc.ai[2] += 1f;
																							if (base.npc.ai[2] == 30f)
																							{
																								base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<GiantMask>(), 220, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
																							}
																							if (base.npc.ai[2] == 35f)
																							{
																								base.npc.Dash(-7, false, SoundID.Item1.WithVolume(0f), player.Center);
																								this.aniType = 2;
																								this.frameCounters = 0;
																								base.npc.netUpdate = true;
																							}
																							if (this.aniType == 0 && base.npc.ai[2] > 140f)
																							{
																								base.npc.ai[2] = 0f;
																								base.npc.ai[1] = 0f;
																								base.npc.ai[0] = 26f;
																								base.npc.netUpdate = true;
																								return;
																							}
																						}
																						else
																						{
																							base.npc.LookAtPlayer();
																							base.npc.alpha -= 10;
																							if (base.npc.alpha <= 0)
																							{
																								base.npc.ai[1] = 2f;
																								base.npc.netUpdate = true;
																								return;
																							}
																						}
																					}
																				}
																				else
																				{
																					float obj2 = base.npc.ai[1];
																					if (0f.Equals(obj2))
																					{
																						base.npc.Center = new Vector2(1099f, 1239f) * 16f;
																						base.npc.ai[1] = 1f;
																						base.npc.velocity *= 0f;
																						base.npc.netUpdate = true;
																						return;
																					}
																					if (!1f.Equals(obj2))
																					{
																						if (!2f.Equals(obj2))
																						{
																							return;
																						}
																						base.npc.LookAtPlayer();
																						base.npc.ai[2] += 1f;
																						if (base.npc.ai[2] == 75f || base.npc.ai[2] == 135f || base.npc.ai[2] == 195f)
																						{
																							this.aniType = 2;
																							this.frameCounters = 0;
																							base.npc.netUpdate = true;
																						}
																						if (base.npc.ai[2] == 120f)
																						{
																							base.npc.Shoot(new Vector2(1100f, 1170f) * 16f, ModContent.ProjectileType<ShadeburstTele2>(), 0, new Vector2(0f, 40f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
																						}
																						if (base.npc.ai[2] == 60f)
																						{
																							base.npc.Shoot(new Vector2(1037f, 1330f) * 16f, ModContent.ProjectileType<ShadeburstTele2>(), 0, new Vector2(0f, -40f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
																							base.npc.Shoot(new Vector2(1165f, 1330f) * 16f, ModContent.ProjectileType<ShadeburstTele2>(), 0, new Vector2(0f, -40f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
																						}
																						if (base.npc.ai[2] == 80f || base.npc.ai[2] == 200f)
																						{
																							for (int i3 = 0; i3 < 30; i3++)
																							{
																								base.npc.Shoot(new Vector2((float)Main.rand.Next(1069, 1130), 1170f) * 16f, ModContent.ProjectileType<ShadeburstProj>(), 160, new Vector2(Utils.NextFloat(Main.rand, -2f, 2f), Utils.NextFloat(Main.rand, 14f, 26f)), false, SoundID.NPCDeath52.WithVolume(0.4f), "", 5f, 0f);
																							}
																						}
																						if (base.npc.ai[2] == 140f)
																						{
																							for (int i4 = 0; i4 < 30; i4++)
																							{
																								base.npc.Shoot(new Vector2((float)Main.rand.Next(1140, 1187), 1330f) * 16f, ModContent.ProjectileType<ShadeburstProj>(), 160, new Vector2(Utils.NextFloat(Main.rand, -2f, 2f), Utils.NextFloat(Main.rand, -26f, -14f)), false, SoundID.NPCDeath52.WithVolume(0.2f), "", 5f, 0f);
																							}
																							for (int i5 = 0; i5 < 30; i5++)
																							{
																								base.npc.Shoot(new Vector2((float)Main.rand.Next(1015, 1059), 1330f) * 16f, ModContent.ProjectileType<ShadeburstProj>(), 160, new Vector2(Utils.NextFloat(Main.rand, -2f, 2f), Utils.NextFloat(Main.rand, -26f, -14f)), false, SoundID.NPCDeath52.WithVolume(0.2f), "", 5f, 0f);
																							}
																						}
																						if (this.aniType == 0 && base.npc.ai[2] > 260f)
																						{
																							base.npc.ai[2] = 0f;
																							base.npc.ai[1] = 0f;
																							base.npc.ai[0] = 26f;
																							base.npc.netUpdate = true;
																							return;
																						}
																					}
																					else
																					{
																						base.npc.LookAtPlayer();
																						base.npc.alpha -= 10;
																						if (base.npc.alpha <= 0)
																						{
																							base.npc.Shoot(new Vector2(1100f, 1170f) * 16f, ModContent.ProjectileType<ShadeburstTele2>(), 0, new Vector2(0f, 40f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
																							base.npc.ai[1] = 2f;
																							base.npc.netUpdate = true;
																							return;
																						}
																					}
																				}
																			}
																			else
																			{
																				float obj2 = base.npc.ai[1];
																				if (0f.Equals(obj2))
																				{
																					this.auraDirection = true;
																					this.auraPercent = 0f;
																					base.npc.velocity *= 0f;
																					base.npc.Center = this.PosPick();
																					base.npc.ai[3] = (float)Main.rand.Next(1, 11);
																					Main.PlaySound(29, player.position, 83);
																					Vector2 spawn = this.PosPick();
																					for (int i6 = 0; i6 < 11; i6++)
																					{
																						base.npc.SpawnNPC((int)spawn.X, (int)spawn.Y, ModContent.NPCType<WardenIdle_Fake>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
																					}
																					for (int p = 0; p < 255; p++)
																					{
																						if (Main.player[p].active && !Main.player[p].dead)
																						{
																							if (Main.netMode != 1)
																							{
																								Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<WardenFacesFront2>(), 0, 0f, Main.player[p].whoAmI, base.npc.ai[3] - 1f, 0f);
																							}
																							Main.player[p].AddBuff(23, 120, true);
																						}
																					}
																					base.npc.ai[1] = 1f;
																					base.npc.netUpdate = true;
																					return;
																				}
																				if (!1f.Equals(obj2))
																				{
																					if (!2f.Equals(obj2))
																					{
																						if (3f.Equals(obj2))
																						{
																							base.npc.chaseable = true;
																							for (int p2 = 0; p2 < 255; p2++)
																							{
																								if (Main.player[p2].active && !Main.player[p2].dead && Main.netMode != 1)
																								{
																									Projectile.NewProjectile(Main.player[p2].Center, Vector2.Zero, ModContent.ProjectileType<WardenCandleHitbox>(), 0, 0f, Main.player[p2].whoAmI, 0f, 0f);
																								}
																							}
																							base.npc.ai[3] = 0f;
																							base.npc.ai[2] = 0f;
																							base.npc.ai[1] = 0f;
																							base.npc.ai[0] = 26f;
																							base.npc.netUpdate = true;
																							return;
																						}
																						if (!4f.Equals(obj2))
																						{
																							return;
																						}
																						base.npc.chaseable = true;
																						base.npc.ai[3] = 0f;
																						base.npc.ai[2] = 0f;
																						base.npc.ai[1] = 0f;
																						base.npc.ai[0] = 26f;
																						base.npc.netUpdate = true;
																						return;
																					}
																					else
																					{
																						base.npc.LookAtPlayer();
																						base.npc.ai[2] += 1f;
																						if (base.npc.ai[2] > 1200f)
																						{
																							base.npc.ai[2] = 0f;
																							base.npc.ai[1] = 3f;
																							base.npc.netUpdate = true;
																							return;
																						}
																					}
																				}
																				else
																				{
																					base.npc.LookAtPlayer();
																					base.npc.dontTakeDamage = true;
																					base.npc.chaseable = false;
																					base.npc.alpha -= 2;
																					if (base.npc.alpha <= 0)
																					{
																						base.npc.ai[1] = 2f;
																						base.npc.netUpdate = true;
																						if (Main.netMode == 2 && base.npc.whoAmI < 200)
																						{
																							NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
																							return;
																						}
																					}
																				}
																			}
																		}
																		else
																		{
																			float obj2 = base.npc.ai[1];
																			if (0f.Equals(obj2))
																			{
																				base.npc.velocity *= 0f;
																				base.npc.Center = this.PosPick();
																				base.npc.ai[1] = 1f;
																				base.npc.netUpdate = true;
																				return;
																			}
																			if (!1f.Equals(obj2))
																			{
																				if (!2f.Equals(obj2))
																				{
																					return;
																				}
																				base.npc.LookAtPlayer();
																				base.npc.ai[2] += 1f;
																				if (base.npc.ai[2] == 10f)
																				{
																					for (int i7 = 0; i7 < 12; i7++)
																					{
																						base.npc.Shoot(this.PosPick(), ModContent.ProjectileType<ShadeFlames>(), 160, new Vector2(0f, 5f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
																					}
																				}
																				if (base.npc.ai[2] == 70f && !Main.dedServ)
																				{
																					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/FlameRise"), -1, -1);
																				}
																				if (base.npc.ai[2] == 125f)
																				{
																					Main.PlaySound(SoundID.NPCDeath52, -1, -1);
																				}
																				if (base.npc.ai[2] > 260f)
																				{
																					base.npc.ai[2] = 0f;
																					base.npc.ai[1] = 0f;
																					base.npc.ai[0] = 26f;
																					base.npc.netUpdate = true;
																					return;
																				}
																			}
																			else
																			{
																				base.npc.LookAtPlayer();
																				base.npc.alpha -= 10;
																				if (base.npc.alpha <= 0)
																				{
																					base.npc.ai[1] = 2f;
																					base.npc.netUpdate = true;
																					return;
																				}
																			}
																		}
																	}
																	else
																	{
																		float obj2 = base.npc.ai[1];
																		if (0f.Equals(obj2))
																		{
																			int num = Main.rand.Next(2);
																			if (num != 0)
																			{
																				if (num == 1)
																				{
																					base.npc.Center = this.PosPickRight();
																					base.npc.ai[1] = 3f;
																				}
																			}
																			else
																			{
																				base.npc.Center = this.PosPickLeft();
																				base.npc.ai[1] = 1f;
																			}
																			base.npc.velocity *= 0f;
																			base.npc.netUpdate = true;
																			return;
																		}
																		if (!1f.Equals(obj2))
																		{
																			if (!2f.Equals(obj2))
																			{
																				if (!3f.Equals(obj2))
																				{
																					if (!4f.Equals(obj2))
																					{
																						return;
																					}
																					base.npc.ai[2] += 1f;
																					if (base.npc.ai[2] == 20f)
																					{
																						this.aniType = 2;
																						base.npc.netUpdate = true;
																					}
																					if (base.npc.ai[2] >= 25f && base.npc.ai[2] < 30f)
																					{
																						for (int i8 = 0; i8 < 10; i8++)
																						{
																							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstProj>(), 160, new Vector2(Utils.NextFloat(Main.rand, 14f, 26f), Utils.NextFloat(Main.rand, -8f, 8f)), false, SoundID.NPCDeath52.WithVolume(0.4f), "", 2f, 0f);
																						}
																					}
																					if (this.aniType == 0 && base.npc.ai[2] > 100f)
																					{
																						base.npc.ai[2] = 0f;
																						base.npc.ai[1] = 0f;
																						base.npc.ai[0] = 26f;
																						base.npc.netUpdate = true;
																						return;
																					}
																				}
																				else
																				{
																					base.npc.spriteDirection = -1;
																					base.npc.alpha -= 10;
																					if (base.npc.alpha <= 0)
																					{
																						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstTele>(), 0, new Vector2(-40f, 0f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
																						base.npc.ai[1] = 4f;
																						base.npc.netUpdate = true;
																						return;
																					}
																				}
																			}
																			else
																			{
																				base.npc.ai[2] += 1f;
																				if (base.npc.ai[2] == 20f)
																				{
																					this.aniType = 2;
																					base.npc.netUpdate = true;
																				}
																				if (base.npc.ai[2] == 25f)
																				{
																					for (int i9 = 0; i9 < 30; i9++)
																					{
																						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstProj>(), 160, new Vector2(Utils.NextFloat(Main.rand, -26f, -14f), Utils.NextFloat(Main.rand, -8f, 8f)), false, SoundID.NPCDeath52.WithVolume(0.4f), "", 0f, 0f);
																					}
																				}
																				if (this.aniType == 0 && base.npc.ai[2] > 100f)
																				{
																					base.npc.ai[2] = 0f;
																					base.npc.ai[1] = 0f;
																					base.npc.ai[0] = 26f;
																					base.npc.netUpdate = true;
																					return;
																				}
																			}
																		}
																		else
																		{
																			base.npc.spriteDirection = 1;
																			base.npc.alpha -= 10;
																			if (base.npc.alpha <= 0)
																			{
																				base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstTele>(), 0, new Vector2(40f, 0f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
																				base.npc.ai[1] = 2f;
																				base.npc.netUpdate = true;
																				return;
																			}
																		}
																	}
																}
																else
																{
																	float obj2 = base.npc.ai[1];
																	if (0f.Equals(obj2))
																	{
																		base.npc.Center = this.PosPick();
																		base.npc.ai[1] = 1f;
																		base.npc.netUpdate = true;
																		return;
																	}
																	if (!1f.Equals(obj2))
																	{
																		if (!2f.Equals(obj2))
																		{
																			return;
																		}
																		base.npc.LookAtPlayer();
																		base.npc.ai[2] += 1f;
																		base.npc.MoveToVector2(player.Center, 1f);
																		if (base.npc.ai[2] > 300f)
																		{
																			base.npc.ai[2] = 0f;
																			base.npc.ai[1] = 0f;
																			base.npc.ai[0] = 26f;
																			base.npc.netUpdate = true;
																			return;
																		}
																	}
																	else
																	{
																		base.npc.LookAtPlayer();
																		base.npc.alpha -= 10;
																		if (base.npc.alpha <= 0)
																		{
																			base.npc.ai[1] = 2f;
																			base.npc.netUpdate = true;
																			return;
																		}
																	}
																}
															}
															else
															{
																float obj2 = base.npc.ai[1];
																if (0f.Equals(obj2))
																{
																	this.Teleport(false, Vector2.Zero);
																	base.npc.ai[1] = 1f;
																	base.npc.netUpdate = true;
																	return;
																}
																if (!1f.Equals(obj2))
																{
																	if (!2f.Equals(obj2))
																	{
																		if (!3f.Equals(obj2))
																		{
																			return;
																		}
																		if (base.npc.ai[2] <= 25f)
																		{
																			base.npc.LookAtPlayer();
																		}
																		base.npc.ai[2] += 1f;
																		base.npc.velocity *= 0.98f;
																		if (base.npc.ai[2] == 25f)
																		{
																			base.npc.Dash(5, false, SoundID.Item1.WithVolume(0f), player.Center);
																			base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<WardenSwingHitbox>(), 0, Vector2.Zero, false, SoundID.Item1, "", (float)base.npc.whoAmI, 0f);
																		}
																		if (this.aniType == 0 || base.npc.ai[2] > 60f)
																		{
																			if (!this.repeat && !player.HasBuff(22))
																			{
																				base.npc.ai[1] = 2f;
																				base.npc.ai[2] = 0f;
																				this.repeat = true;
																				base.npc.netUpdate = true;
																				return;
																			}
																			base.npc.velocity *= 0f;
																			this.aniType = 0;
																			base.npc.ai[2] = 0f;
																			base.npc.ai[1] = 0f;
																			base.npc.ai[0] = 26f;
																			base.npc.netUpdate = true;
																			return;
																		}
																	}
																	else
																	{
																		base.npc.LookAtPlayer();
																		this.MoveVector2 = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 50f) : (player.Center.X + 50f), player.Center.Y - 40f);
																		base.npc.ai[2] += 1f;
																		if (base.npc.ai[2] > 60f || base.npc.Distance(this.MoveVector2) < 30f)
																		{
																			this.aniType = 1;
																			base.npc.ai[1] = 3f;
																			base.npc.ai[2] = 0f;
																			base.npc.velocity *= 0f;
																			base.npc.netUpdate = true;
																			return;
																		}
																		base.npc.Move(this.MoveVector2, 20f, 2f, false);
																		return;
																	}
																}
																else
																{
																	base.npc.LookAtPlayer();
																	base.npc.alpha -= 10;
																	if (base.npc.alpha <= 0)
																	{
																		base.npc.ai[1] = 2f;
																		base.npc.netUpdate = true;
																		return;
																	}
																}
															}
														}
														else
														{
															base.npc.alpha += 5;
															base.npc.velocity *= 0.98f;
															if (base.npc.alpha >= 255)
															{
																this.AttackChoice2();
																base.npc.ai[2] = 0f;
																base.npc.ai[3] = 0f;
																base.npc.netUpdate = true;
																return;
															}
														}
													}
													else
													{
														player.GetModPlayer<ScreenPlayer>().lockScreen = true;
														base.npc.ai[2] += 1f;
														if (base.npc.ai[2] == 10f && !Main.dedServ)
														{
															Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/WardenFear"), -1, -1);
														}
														if (base.npc.ai[2] > 80f && base.npc.ai[2] < 420f)
														{
															this.finalPhase = true;
															base.npc.ai[3] = 0f;
															base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstProj>(), 0, RedeHelper.PolarVector(10f, Utils.NextFloat(Main.rand, 0f, 6.2831855f)), false, SoundID.Item1.WithVolume(0f), "", 4f, 0f);
															player.GetModPlayer<ScreenPlayer>().Rumble(5, 5);
															base.npc.netUpdate = true;
														}
														if (base.npc.ai[2] == 20f)
														{
															base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShockwaveBoom3>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
															for (int p3 = 0; p3 < 1000; p3++)
															{
																if (Main.projectile[p3].active || Main.projectile[p3].type == ModContent.ProjectileType<WardenCandle>())
																{
																	Main.projectile[p3].Kill();
																}
															}
															for (int p4 = 0; p4 < 255; p4++)
															{
																if (Main.player[p4].active && Main.netMode != 1)
																{
																	Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<WardenCandle>(), 0, 0f, Main.player[p4].whoAmI, (float)base.npc.whoAmI, 0f);
																}
															}
														}
														if (base.npc.ai[2] >= 480f)
														{
															base.npc.life = base.npc.lifeMax;
															base.npc.ai[3] = 0f;
															base.npc.ai[2] = 0f;
															base.npc.ai[0] = 34f;
															base.npc.netUpdate = true;
															return;
														}
													}
												}
												else
												{
													base.npc.alpha -= 10;
													if (base.npc.alpha <= 0)
													{
														base.npc.ai[0] = 25f;
														base.npc.netUpdate = true;
														return;
													}
												}
											}
											else
											{
												base.npc.dontTakeDamage = true;
												base.npc.netUpdate = true;
												base.npc.velocity *= 0f;
												base.npc.ai[2] = 0f;
												base.npc.ai[1] = 0f;
												base.npc.alpha += 5;
												if (base.npc.alpha >= 255)
												{
													base.npc.Center = new Vector2(1099f, 1239f) * 16f;
													base.npc.ai[0] = 24f;
													base.npc.netUpdate = true;
													if (Main.netMode == 2 && base.npc.whoAmI < 200)
													{
														NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
														return;
													}
												}
											}
										}
										else
										{
											float obj2 = base.npc.ai[1];
											if (0f.Equals(obj2))
											{
												base.npc.velocity *= 0f;
												base.npc.Center = this.PosPick();
												Main.PlaySound(29, player.position, 83);
												Vector2 spawn2 = this.PosPick();
												for (int i10 = 0; i10 < 3 + (int)(base.npc.ai[3] * 2f); i10++)
												{
													base.npc.SpawnNPC((int)spawn2.X, (int)spawn2.Y, ModContent.NPCType<WardenIdle_Fake>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
												}
												for (int p5 = 0; p5 < 255; p5++)
												{
													if (Main.player[p5].active && !Main.player[p5].dead)
													{
														if (Main.netMode != 1)
														{
															Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<WardenFacesFront>(), 0, 0f, Main.player[p5].whoAmI, base.npc.ai[3], 0f);
														}
														Main.player[p5].AddBuff(23, 120, true);
													}
												}
												base.npc.ai[1] = 1f;
												base.npc.netUpdate = true;
												return;
											}
											if (!1f.Equals(obj2))
											{
												if (!2f.Equals(obj2))
												{
													if (3f.Equals(obj2))
													{
														base.npc.chaseable = true;
														for (int p6 = 0; p6 < 255; p6++)
														{
															if (Main.player[p6].active && !Main.player[p6].dead && Main.netMode != 1)
															{
																Projectile.NewProjectile(Main.player[p6].Center, Vector2.Zero, ModContent.ProjectileType<WardenCandleHitbox>(), 0, 0f, Main.player[p6].whoAmI, 0f, 0f);
															}
														}
														base.npc.ai[2] = 0f;
														base.npc.ai[1] = 0f;
														base.npc.ai[0] = 2f;
														base.npc.netUpdate = true;
														return;
													}
													if (!4f.Equals(obj2))
													{
														return;
													}
													base.npc.chaseable = true;
													base.npc.ai[2] = 0f;
													base.npc.ai[1] = 0f;
													base.npc.ai[0] = 2f;
													base.npc.netUpdate = true;
													return;
												}
												else
												{
													base.npc.LookAtPlayer();
													base.npc.ai[2] += 1f;
													if (base.npc.ai[2] > 1200f)
													{
														base.npc.ai[2] = 0f;
														base.npc.ai[1] = 3f;
														base.npc.netUpdate = true;
														return;
													}
												}
											}
											else
											{
												base.npc.LookAtPlayer();
												base.npc.dontTakeDamage = true;
												base.npc.chaseable = false;
												base.npc.alpha -= 2;
												if (base.npc.alpha <= 0)
												{
													base.npc.ai[1] = 2f;
													base.npc.netUpdate = true;
													if (Main.netMode == 2 && base.npc.whoAmI < 200)
													{
														NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
														return;
													}
												}
											}
										}
									}
									else
									{
										float obj2 = base.npc.ai[1];
										if (0f.Equals(obj2))
										{
											base.npc.velocity *= 0f;
											base.npc.Center = this.PosPick();
											base.npc.ai[1] = 1f;
											base.npc.netUpdate = true;
											return;
										}
										if (!1f.Equals(obj2))
										{
											if (!2f.Equals(obj2))
											{
												return;
											}
											base.npc.LookAtPlayer();
											base.npc.ai[2] += 1f;
											if (base.npc.ai[2] == 10f)
											{
												int i11 = 0;
												while ((float)i11 < 8f + base.npc.ai[3])
												{
													base.npc.Shoot(this.PosPick(), ModContent.ProjectileType<ShadeFlames>(), 90, new Vector2(0f, 5f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
													i11++;
												}
											}
											if (base.npc.ai[2] == 70f && !Main.dedServ)
											{
												Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/FlameRise"), -1, -1);
											}
											if (base.npc.ai[2] == 125f)
											{
												Main.PlaySound(SoundID.NPCDeath52, -1, -1);
											}
											if (base.npc.ai[2] > 260f)
											{
												base.npc.ai[2] = 0f;
												base.npc.ai[1] = 0f;
												base.npc.ai[0] = 2f;
												base.npc.netUpdate = true;
												return;
											}
										}
										else
										{
											base.npc.LookAtPlayer();
											base.npc.alpha -= 10;
											if (base.npc.alpha <= 0)
											{
												base.npc.ai[1] = 2f;
												base.npc.netUpdate = true;
												return;
											}
										}
									}
								}
								else
								{
									float obj2 = base.npc.ai[1];
									if (0f.Equals(obj2))
									{
										int num = Main.rand.Next(2);
										if (num != 0)
										{
											if (num == 1)
											{
												base.npc.Center = this.PosPickRight();
												base.npc.ai[1] = 3f;
											}
										}
										else
										{
											base.npc.Center = this.PosPickLeft();
											base.npc.ai[1] = 1f;
										}
										base.npc.velocity *= 0f;
										base.npc.netUpdate = true;
										return;
									}
									if (!1f.Equals(obj2))
									{
										if (!2f.Equals(obj2))
										{
											if (!3f.Equals(obj2))
											{
												if (!4f.Equals(obj2))
												{
													return;
												}
												base.npc.ai[2] += 1f;
												if (base.npc.ai[2] == 20f)
												{
													this.aniType = 2;
													base.npc.netUpdate = true;
												}
												if (base.npc.ai[2] >= 25f && base.npc.ai[2] < 30f)
												{
													for (int i12 = 0; i12 < 10; i12++)
													{
														base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstProj>(), 160, new Vector2(Utils.NextFloat(Main.rand, 14f, 26f), Utils.NextFloat(Main.rand, -4f - base.npc.ai[3], 4f + base.npc.ai[3])), false, SoundID.NPCDeath52.WithVolume(0.4f), "", 2f, 0f);
													}
												}
												if (this.aniType == 0 && base.npc.ai[2] > 100f)
												{
													base.npc.ai[2] = 0f;
													base.npc.ai[1] = 0f;
													base.npc.ai[0] = 2f;
													base.npc.netUpdate = true;
													return;
												}
											}
											else
											{
												base.npc.spriteDirection = -1;
												base.npc.alpha -= 10;
												if (base.npc.alpha <= 0)
												{
													base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstTele>(), 0, new Vector2(-40f, 0f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
													base.npc.ai[1] = 4f;
													base.npc.netUpdate = true;
													return;
												}
											}
										}
										else
										{
											base.npc.ai[2] += 1f;
											if (base.npc.ai[2] == 20f)
											{
												this.aniType = 2;
												base.npc.netUpdate = true;
											}
											if (base.npc.ai[2] == 25f)
											{
												for (int i13 = 0; i13 < 30; i13++)
												{
													base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstProj>(), 160, new Vector2(Utils.NextFloat(Main.rand, -26f, -14f), Utils.NextFloat(Main.rand, -4f - base.npc.ai[3], 4f + base.npc.ai[3])), false, SoundID.NPCDeath52.WithVolume(0.4f), "", 0f, 0f);
												}
											}
											if (this.aniType == 0 && base.npc.ai[2] > 100f)
											{
												base.npc.ai[2] = 0f;
												base.npc.ai[1] = 0f;
												base.npc.ai[0] = 2f;
												base.npc.netUpdate = true;
												return;
											}
										}
									}
									else
									{
										base.npc.spriteDirection = 1;
										base.npc.alpha -= 10;
										if (base.npc.alpha <= 0)
										{
											base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ShadeburstTele>(), 0, new Vector2(40f, 0f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
											base.npc.ai[1] = 2f;
											base.npc.netUpdate = true;
											return;
										}
									}
								}
							}
							else
							{
								float obj2 = base.npc.ai[1];
								if (0f.Equals(obj2))
								{
									base.npc.Center = this.PosPick();
									base.npc.ai[1] = 1f;
									base.npc.netUpdate = true;
									return;
								}
								if (!1f.Equals(obj2))
								{
									if (!2f.Equals(obj2))
									{
										return;
									}
									base.npc.LookAtPlayer();
									base.npc.ai[2] += 1f;
									base.npc.MoveToVector2(player.Center, 1f);
									if (base.npc.ai[2] > 300f)
									{
										base.npc.ai[2] = 0f;
										base.npc.ai[1] = 0f;
										base.npc.ai[0] = 2f;
										base.npc.netUpdate = true;
										return;
									}
								}
								else
								{
									base.npc.LookAtPlayer();
									base.npc.alpha -= 10;
									if (base.npc.alpha <= 0)
									{
										base.npc.ai[1] = 2f;
										base.npc.netUpdate = true;
										return;
									}
								}
							}
						}
						else
						{
							float obj2 = base.npc.ai[1];
							if (0f.Equals(obj2))
							{
								this.Teleport(false, Vector2.Zero);
								base.npc.ai[1] = 1f;
								base.npc.netUpdate = true;
								return;
							}
							if (!1f.Equals(obj2))
							{
								if (!2f.Equals(obj2))
								{
									if (!3f.Equals(obj2))
									{
										return;
									}
									if (base.npc.ai[2] <= 25f)
									{
										base.npc.LookAtPlayer();
									}
									base.npc.ai[2] += 1f;
									base.npc.velocity *= 0.98f;
									if (base.npc.ai[2] == 25f)
									{
										base.npc.Dash(5, false, SoundID.Item1.WithVolume(0f), player.Center);
										base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<WardenSwingHitbox>(), 0, Vector2.Zero, false, SoundID.Item1, "", (float)base.npc.whoAmI, 0f);
									}
									if (this.aniType == 0 || base.npc.ai[2] > 60f)
									{
										this.repeat = false;
										base.npc.velocity *= 0f;
										this.aniType = 0;
										base.npc.ai[2] = 0f;
										base.npc.ai[1] = 0f;
										base.npc.ai[0] = 2f;
										base.npc.netUpdate = true;
										return;
									}
								}
								else
								{
									base.npc.LookAtPlayer();
									this.MoveVector2 = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 50f) : (player.Center.X + 50f), player.Center.Y - 40f);
									base.npc.ai[2] += 1f;
									if (base.npc.ai[2] > 60f || base.npc.Distance(this.MoveVector2) < 30f)
									{
										this.aniType = 1;
										base.npc.ai[1] = 3f;
										base.npc.ai[2] = 0f;
										base.npc.velocity *= 0f;
										base.npc.netUpdate = true;
										return;
									}
									base.npc.Move(this.MoveVector2, 20f, 2f, false);
									return;
								}
							}
							else
							{
								base.npc.LookAtPlayer();
								base.npc.alpha -= 10;
								if (base.npc.alpha <= 0)
								{
									base.npc.ai[1] = 2f;
									base.npc.netUpdate = true;
									return;
								}
							}
						}
					}
					else
					{
						base.npc.alpha += 5;
						base.npc.velocity *= 0.98f;
						if (base.npc.alpha >= 255)
						{
							this.AttackChoice();
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
					}
				}
				else
				{
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] > 80f)
					{
						base.npc.alpha -= 3;
					}
					if (base.npc.alpha <= 0)
					{
						if (!Main.dedServ)
						{
							Redemption.Inst.TitleCardUIElement.DisplayTitle("The Warden", 60, 90, 0.8f, 0, new Color?(Color.GhostWhite), "Husk of Sorrow", true);
						}
						for (int p7 = 0; p7 < 255; p7++)
						{
							if (Main.player[p7].active && Main.netMode != 1)
							{
								Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<WardenCandle>(), 0, 0f, Main.player[p7].whoAmI, (float)base.npc.whoAmI, 0f);
							}
						}
						base.npc.alpha = 0;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
				}
				return;
			}
			ArenaWorld.arenaActive = true;
			ArenaWorld.arenaBoss = "Warden";
			base.npc.position = new Vector2(1097f, 1234f) * 16f;
			base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y + 60f), ModContent.ProjectileType<ShadeVortex>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/SpookyNoise", 0f, 0f);
			base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<WardenFaces>(), (float)base.npc.whoAmI, 0f, 0f, 0f);
			base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<WardenFaces>(), (float)base.npc.whoAmI, 1f, 0f, 0f);
			base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<WardenFaces>(), (float)base.npc.whoAmI, 2f, 0f, 0f);
			base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<WardenFaces>(), (float)base.npc.whoAmI, 3f, 0f, 0f);
			base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<WardenFaces>(), (float)base.npc.whoAmI, 4f, 0f, 0f);
			player.GetModPlayer<ScreenPlayer>().Rumble(120, 1);
			base.npc.ai[0] = 1f;
			base.npc.netUpdate = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life < 0 && this.finalPhase)
			{
				for (int i = 0; i < 5; i++)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<WardenDeath>(), 0, RedeHelper.PolarVector((float)Main.rand.Next(7, 9), Utils.NextFloat(Main.rand, 0f, 6.2831855f)), false, SoundID.Item1.WithVolume(0f), "", (float)i, 0f);
				}
			}
			if (base.npc.ai[0] == 7f && base.npc.ai[1] < 3f)
			{
				base.npc.ai[1] = 4f;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] == 31f && base.npc.ai[1] < 3f)
			{
				base.npc.ai[1] = 4f;
				base.npc.netUpdate = true;
			}
		}

		public override bool CheckDead()
		{
			if (this.finalPhase)
			{
				return true;
			}
			base.npc.life = 1;
			base.npc.ai[0] = 23f;
			base.npc.netUpdate = true;
			for (int p = 0; p < 1000; p++)
			{
				if (Main.projectile[p].active || Main.projectile[p].type == ModContent.ProjectileType<WardenCandle>())
				{
					Main.projectile[p].Kill();
				}
			}
			return false;
		}

		public void Teleport(bool specialPos, Vector2 teleportPos)
		{
			Player player = Main.player[base.npc.target];
			if (Main.netMode != 1)
			{
				if (!specialPos)
				{
					int num = Main.rand.Next(2);
					if (num == 0)
					{
						Vector2 newPos = new Vector2((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + newPos;
						base.npc.netUpdate = true;
						return;
					}
					if (num != 1)
					{
						return;
					}
					Vector2 newPos2 = new Vector2((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-200, 50));
					base.npc.Center = Main.player[base.npc.target].Center + newPos2;
					base.npc.netUpdate = true;
					return;
				}
				else
				{
					base.npc.Center = Main.player[base.npc.target].Center + teleportPos;
					base.npc.netUpdate = true;
				}
			}
		}

		public Vector2 PosPick()
		{
			Vector2[] pickArray = new Vector2[]
			{
				new Vector2(1064f, 1184f),
				new Vector2(1033f, 1202f),
				new Vector2(1069f, 1215f),
				new Vector2(1166f, 1203f),
				new Vector2(1135f, 1184f),
				new Vector2(1133f, 1217f),
				new Vector2(1131f, 1253f),
				new Vector2(1168f, 1268f),
				new Vector2(1132f, 1290f),
				new Vector2(1037f, 1270f),
				new Vector2(1069f, 1290f),
				new Vector2(1067f, 1252f)
			};
			return pickArray[Main.rand.Next(Enumerable.Count<Vector2>(pickArray))] * 16f;
		}

		public Vector2 PosPickLeft()
		{
			Vector2[] pickArray = new Vector2[]
			{
				new Vector2(1033f, 1202f),
				new Vector2(1037f, 1270f)
			};
			return pickArray[Main.rand.Next(Enumerable.Count<Vector2>(pickArray))] * 16f;
		}

		public Vector2 PosPickRight()
		{
			Vector2[] pickArray = new Vector2[]
			{
				new Vector2(1166f, 1203f),
				new Vector2(1168f, 1268f)
			};
			return pickArray[Main.rand.Next(Enumerable.Count<Vector2>(pickArray))] * 16f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D faces = base.mod.GetTexture("NPCs/Bosses/Warden/WardenFaces");
			Texture2D faces2 = base.mod.GetTexture("NPCs/Bosses/Warden/WardenFalseFaces2");
			Texture2D swingAttack = base.mod.GetTexture("NPCs/Bosses/Warden/Warden_CandleExtinguish");
			Texture2D shadeburstAttack = base.mod.GetTexture("NPCs/Bosses/Warden/Warden_ShadeBurst");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			if (this.finalPhase)
			{
				if (this.auraDirection)
				{
					this.auraPercent += 0.1f;
					this.auraDirection = (this.auraPercent < 1f);
				}
				else
				{
					this.auraPercent -= 0.1f;
					this.auraDirection = (this.auraPercent <= 0f);
				}
				switch (this.aniType)
				{
				case 0:
					BaseDrawing.DrawAura(spriteBatch, Main.npcTexture[base.npc.type], 0, base.npc.position, base.npc.width, base.npc.height, this.auraPercent, 4f, base.npc.scale, base.npc.rotation, base.npc.spriteDirection, 4, base.npc.frame, 0f, 0f, new Color?(RedeColor.COLOR_GLOWPULSE));
					break;
				case 1:
				{
					int num216 = swingAttack.Height / 12;
					int num217 = this.swingFrame;
					break;
				}
				case 2:
				{
					int num218 = shadeburstAttack.Height / 5;
					int num219 = this.burstFrame;
					break;
				}
				}
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			switch (this.aniType)
			{
			case 0:
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
				break;
			case 1:
			{
				int height = swingAttack.Height / 12;
				int y = height * this.swingFrame;
				Main.spriteBatch.Draw(swingAttack, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y, swingAttack.Width, height)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)swingAttack.Width / 2f, (float)height / 2f), base.npc.scale, effects, 0f);
				break;
			}
			case 2:
			{
				int height2 = shadeburstAttack.Height / 5;
				int y2 = height2 * this.burstFrame;
				Main.spriteBatch.Draw(shadeburstAttack, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y2, shadeburstAttack.Width, height2)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)shadeburstAttack.Width / 2f, (float)height2 / 2f), base.npc.scale, effects, 0f);
				break;
			}
			}
			Vector2 maskCenter = new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 5f) : (base.npc.Center.X - 5f), base.npc.Center.Y - 35f);
			int num214 = faces.Height / 5;
			int y3 = num214 * (int)base.npc.ai[3];
			if (!this.finalPhase)
			{
				Main.spriteBatch.Draw(faces, maskCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y3, faces.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)faces.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
			}
			if (this.finalPhase && base.npc.ai[3] != 0f)
			{
				Vector2 maskCenter2 = new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 5f) : (base.npc.Center.X - 5f), base.npc.Center.Y - 35f);
				int num215 = faces2.Height / 14;
				int y4 = num215 * ((int)base.npc.ai[3] - 1);
				Main.spriteBatch.Draw(faces2, maskCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y4, faces2.Width, num215)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)faces2.Width / 2f, (float)num215 / 2f), base.npc.scale, effects, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public Vector2 MoveVector2;

		public Vector2 vector;

		public bool finalPhase;

		public int aniType;

		public int floatTimer;

		public int frameCounters;

		public int swingFrame;

		public int burstFrame;

		private bool repeat;

		public List<int> AttackList = new List<int>
		{
			3,
			4,
			5,
			6,
			7
		};

		public List<int> AttackList2 = new List<int>
		{
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35
		};

		public List<int> CopyList;

		public List<int> CopyList2;

		public float auraPercent;

		public bool auraDirection = true;
	}
}
