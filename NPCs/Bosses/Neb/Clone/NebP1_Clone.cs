using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PostML.Druid.Seedbags;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Items.Weapons.PostML.Summon;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb.Clone
{
	[AutoloadBossHead]
	public class NebP1_Clone : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/NebP1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus Mirage");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 750000;
			base.npc.defense = 170;
			base.npc.damage = 250;
			base.npc.width = 62;
			base.npc.height = 84;
			base.npc.aiStyle = -1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.netAlways = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
			this.bossBag = ModContent.ItemType<NebBag>();
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.ai[3] == 6f;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ThankYouLetter>(), 1, false, 0, false, false);
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebuleusTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebuleusMask>(), 1, false, 0, false, false);
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebuleusVanity>(), 1, false, 0, false, false);
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebWings>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GildedBonnet>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<FreedomStarN>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebulaStarFlail>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ConstellationsBook>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<StarfruitSeedbag>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CosmosChainWeapon>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<PiercingNebulaWeapon>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<StarSerpentsCollar>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GalaxyHeart>(), 1, false, 0, false, false);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0 && (base.npc.ai[0] == 11f || RedeConfigClient.Instance.NoLoreElements))
			{
				this.RazzleDazzle();
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 58, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			RedeWorld.downedNebuleus = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.75;
			return true;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.repeat);
				writer.Write(this.phase);
				writer.Write(this.ID);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.repeat = reader.ReadInt32();
				this.phase = reader.ReadInt32();
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
			int attempts = 0;
			while (attempts == 0)
			{
				if (this.CopyList == null || this.CopyList.Count == 0)
				{
					this.CopyList = new List<int>(this.AttackList);
				}
				this.ID = this.CopyList[Main.rand.Next(0, this.CopyList.Count)];
				this.CopyList.Remove(this.ID);
				base.npc.netUpdate = true;
				if ((this.ID != 0 || this.phase < 3) && (this.ID != 5 || this.phase > 1) && (this.ID != 6 || this.phase > 1) && (this.ID < 8 || this.ID > 10 || this.phase > 0) && (this.ID != 11 || this.phase >= 3) && (this.ID != 12 || this.phase >= 3))
				{
					attempts++;
				}
			}
		}

		public override void AI()
		{
			Main.time = 16200.0;
			Main.dayTime = false;
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
			if (base.npc.ai[3] != 6f)
			{
				if (this.floatTimer == 0)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y + 0.005f;
					if (base.npc.velocity.Y > 0.3f)
					{
						this.floatTimer = 1;
						base.npc.netUpdate = true;
					}
				}
				else if (this.floatTimer == 1)
				{
					NPC npc2 = base.npc;
					npc2.velocity.Y = npc2.velocity.Y - 0.005f;
					if (base.npc.velocity.Y < -0.3f)
					{
						this.floatTimer = 0;
						base.npc.netUpdate = true;
					}
				}
				if (this.floatTimer2 == 0)
				{
					NPC npc3 = base.npc;
					npc3.velocity.X = npc3.velocity.X + 0.01f;
					if (base.npc.velocity.X > 0.4f)
					{
						this.floatTimer2 = 1;
						base.npc.netUpdate = true;
					}
				}
				else if (this.floatTimer2 == 1)
				{
					NPC npc4 = base.npc;
					npc4.velocity.X = npc4.velocity.X - 0.01f;
					if (base.npc.velocity.X < -0.4f)
					{
						this.floatTimer2 = 0;
						base.npc.netUpdate = true;
					}
				}
			}
			if (base.npc.ai[3] != 6f)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc5 = base.npc;
					npc5.frame.Y = npc5.frame.Y + 98;
					if (base.npc.frame.Y > 294)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
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
											if (8f.Equals(obj))
											{
												this.frameCounters++;
												if (this.frameCounters >= 5)
												{
													this.armFrames[5]++;
													this.frameCounters = 0;
												}
												if (this.armFrames[5] >= 19)
												{
													base.npc.ai[3] = 0f;
													this.armFrames[5] = 0;
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
												base.npc.ai[3] = 0f;
												this.armFrames[4] = 4;
											}
										}
									}
									else
									{
										base.npc.frameCounter = 0.0;
										base.npc.frame.Y = 392;
										base.npc.rotation = Utils.ToRotation(base.npc.velocity) + 1.57f;
										if (base.npc.velocity.X < 0f)
										{
											base.npc.spriteDirection = -1;
										}
										else
										{
											base.npc.spriteDirection = 1;
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
									if (this.armFrames[3] >= 9)
									{
										base.npc.ai[3] = 0f;
										this.armFrames[3] = 0;
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
								if (this.armFrames[2] >= 8)
								{
									base.npc.ai[3] = 0f;
									this.armFrames[2] = 0;
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
							if (this.armFrames[2] >= 4)
							{
								this.armFrames[2] = 2;
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
						if (this.armFrames[1] >= 8)
						{
							base.npc.ai[3] = 0f;
							this.armFrames[1] = 0;
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
					if (this.armFrames[1] >= 6)
					{
						this.armFrames[1] = 5;
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
			if (base.npc.ai[0] > 4f)
			{
				if (base.npc.ai[0] != 7f && base.npc.ai[0] != 8f && base.npc.ai[0] != 10f && base.npc.ai[0] != 11f)
				{
					base.npc.dontTakeDamage = false;
				}
				else
				{
					base.npc.dontTakeDamage = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.75f) && this.phase < 1)
			{
				base.npc.ai[0] = 6f;
				base.npc.netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && this.phase < 2)
			{
				base.npc.ai[0] = 6f;
				base.npc.netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f) && this.phase < 3)
			{
				base.npc.ai[0] = 6f;
				base.npc.netUpdate = true;
			}
			switch ((int)base.npc.ai[0])
			{
			case 0:
				base.npc.LookAtPlayer();
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 2f)
				{
					DustHelper.DrawStar(base.npc.Center, 58, 5f, 4f, 1f, 3f, 2f, 0f, true, 0f, -1f);
					DustHelper.DrawStar(base.npc.Center, 59, 5f, 5f, 1f, 3f, 2f, 0f, true, 0f, -1f);
					DustHelper.DrawStar(base.npc.Center, 60, 5f, 6f, 1f, 3f, 2f, 0f, true, 0f, -1f);
					DustHelper.DrawStar(base.npc.Center, 62, 5f, 7f, 1f, 3f, 2f, 0f, true, 0f, -1f);
					for (int d = 0; d < 16; d++)
					{
						int dustIndex = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, ModContent.DustType<RainbowStarDust>(), 0f, 0f, 0, default(Color), 1f);
						Main.dust[dustIndex].velocity *= 6f;
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 60f)
				{
					base.npc.ai[0] = 1f;
					if (RedeConfigClient.Instance.NoLoreElements && RedeWorld.nebDeath == 0)
					{
						base.npc.ai[2] = 1360f;
					}
					else
					{
						base.npc.ai[2] = 0f;
					}
					base.npc.netUpdate = true;
				}
				break;
			case 1:
				base.npc.LookAtPlayer();
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 30f)
				{
					base.npc.ai[3] = 1f;
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 120f), Vector2.Zero, ModContent.ProjectileType<CosmicPortal>(), 0, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (base.npc.ai[2] >= 150f)
				{
					base.npc.ai[3] = 2f;
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 2f;
					base.npc.netUpdate = true;
				}
				break;
			case 2:
				base.npc.LookAtPlayer();
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 40f)
				{
					if (NPC.AnyNPCs(ModContent.NPCType<StarWyvernHead>()))
					{
						if (base.npc.ai[2] == 50f || base.npc.ai[2] == 250f || base.npc.ai[2] == 450f || base.npc.ai[2] == 490f)
						{
							int pieCut = 4;
							for (int j = 0; j < pieCut; j++)
							{
								if (Main.netMode != 1)
								{
									int projID = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele2>(), 40, 0f, Main.myPlayer, 1.01f, 0f);
									Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)j / (float)pieCut * 6.28f);
									Main.projectile[projID].netUpdate = true;
								}
							}
						}
						if (base.npc.ai[2] == 150f || base.npc.ai[2] == 350f || base.npc.ai[2] == 470f || base.npc.ai[2] == 510f)
						{
							int pieCut2 = 4;
							for (int k = 0; k < pieCut2; k++)
							{
								if (Main.netMode != 1)
								{
									int projID2 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele2>(), 40, 0f, Main.myPlayer, 1.01f, 1f);
									Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)k / (float)pieCut2 * 6.28f);
									Main.projectile[projID2].netUpdate = true;
								}
							}
						}
						if (base.npc.ai[2] >= 600f)
						{
							this.Teleport(false, Vector2.Zero);
							base.npc.ai[2] = 40f;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						base.npc.ai[0] = 3f;
						base.npc.ai[2] = 100f;
						base.npc.netUpdate = true;
					}
				}
				break;
			case 3:
				base.npc.LookAtPlayer();
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] >= 240f)
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 4f;
					base.npc.netUpdate = true;
				}
				break;
			case 4:
				this.repeat = 0;
				base.npc.LookAtPlayer();
				this.Teleport(false, Vector2.Zero);
				this.frameCounters = 0;
				base.npc.rotation = 0f;
				base.npc.velocity = Vector2.Zero;
				base.npc.ai[3] = 0f;
				base.npc.ai[0] = 5f;
				base.npc.ai[2] = 0f;
				this.AttackChoice();
				this.circleTimer = 0;
				this.circleRadius = 800;
				base.npc.netUpdate = true;
				break;
			case 5:
				switch (this.ID)
				{
				case 0:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f && this.phase < 1)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout1>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 1f;
					}
					if ((this.phase <= 0) ? (base.npc.ai[2] == 30f || base.npc.ai[2] == 70f) : (base.npc.ai[2] == 30f || base.npc.ai[2] == 50f))
					{
						int pieCut3 = 8;
						for (int l = 0; l < pieCut3; l++)
						{
							if (Main.netMode != 1)
							{
								int projID3 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele2>(), 40, 0f, Main.myPlayer, 1.01f, 0f);
								Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)l / (float)pieCut3 * 6.28f);
								Main.projectile[projID3].netUpdate = true;
							}
						}
					}
					if ((this.phase <= 0) ? (base.npc.ai[2] == 50f) : (base.npc.ai[2] == 40f))
					{
						int pieCut4 = (this.phase > 1) ? 10 : 8;
						for (int m = 0; m < pieCut4; m++)
						{
							if (Main.netMode != 1)
							{
								int projID4 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele2>(), 40, 0f, Main.myPlayer, (this.phase > 1) ? 1.002f : 1.01f, 1f);
								Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m / (float)pieCut4 * 6.28f);
								Main.projectile[projID4].netUpdate = true;
							}
						}
					}
					if ((this.phase <= 0) ? (base.npc.ai[2] == 120f) : (base.npc.ai[2] == 100f))
					{
						base.npc.ai[3] = 2f;
					}
					if ((this.phase <= 0) ? (base.npc.ai[2] >= 160f) : (base.npc.ai[2] >= 140f))
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 1:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f && this.phase < 1)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout2>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 3f;
					}
					if (base.npc.ai[2] >= 30f && base.npc.ai[2] <= 70f && Main.rand.Next(2) == 0)
					{
						int A;
						if (base.npc.spriteDirection != 1)
						{
							A = Main.rand.Next(600, 650);
						}
						else
						{
							A = Main.rand.Next(-650, -600);
						}
						int B = Main.rand.Next(-200, 200) - 700;
						base.npc.Shoot(new Vector2(player.Center.X + (float)A, player.Center.Y + (float)B), ModContent.ProjectileType<StarfallTele>(), 120, new Vector2((base.npc.spriteDirection != 1) ? -12f : 12f, 14f), false, SoundID.Item9.WithVolume(0.5f), "", 0f, 0f);
					}
					if (base.npc.ai[2] == 40f)
					{
						base.npc.ai[3] = 4f;
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 2:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f && this.phase < 1)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout3>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (this.phase < 1)
					{
						if (base.npc.ai[2] == 20f || base.npc.ai[2] == 50f)
						{
							base.npc.ai[3] = 5f;
							this.armFrames[3] = 0;
						}
						if (base.npc.ai[2] == 30f || base.npc.ai[2] == 60f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
					}
					else if (this.phase == 1)
					{
						if (base.npc.ai[2] == 20f || base.npc.ai[2] == 40f || base.npc.ai[2] == 60f)
						{
							base.npc.ai[3] = 5f;
							this.armFrames[3] = 0;
						}
						if (base.npc.ai[2] == 30f || base.npc.ai[2] == 50f || base.npc.ai[2] == 70f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
					}
					else if (this.phase == 2)
					{
						if (base.npc.ai[2] == 20f || base.npc.ai[2] == 50f)
						{
							base.npc.ai[3] = 5f;
							this.armFrames[3] = 0;
						}
						if (base.npc.ai[2] == 30f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) + 0.5f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) - 0.5f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] == 60f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) + 0.78f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) - 0.78f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
					}
					else
					{
						if (base.npc.ai[2] == 20f || base.npc.ai[2] == 40f || base.npc.ai[2] == 60f)
						{
							base.npc.ai[3] = 5f;
							this.armFrames[3] = 0;
						}
						if (base.npc.ai[2] == 30f || base.npc.ai[2] == 70f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) + 0.78f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) - 0.78f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] == 50f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) + 1.2f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) - 1.2f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) + 0.6f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) - 0.6f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 3:
					if (base.npc.ai[3] != 6f)
					{
						base.npc.LookAtPlayer();
						base.npc.netUpdate = true;
					}
					base.npc.ai[2] += 1f;
					if ((this.phase > 1) ? (base.npc.ai[2] == 25f) : (base.npc.ai[2] == 5f))
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<NebTeleLine1>(), 0, base.npc.DirectionTo(player.Center + player.velocity * 20f), false, SoundID.Item1.WithVolume(0f), "", (float)((this.phase > 1) ? 115 : 190), (float)base.npc.whoAmI);
					}
					if (base.npc.ai[2] < 55f)
					{
						this.vector = player.Center + player.velocity * 20f;
					}
					if (base.npc.ai[2] == 65f)
					{
						base.npc.ai[3] = 6f;
						this.Dash((int)base.npc.Distance(player.Center) / 16, true, this.vector);
					}
					else if (base.npc.ai[2] == 86f)
					{
						base.npc.rotation = 0f;
						base.npc.velocity = Vector2.Zero;
						base.npc.netUpdate = true;
						if (this.repeat < 3)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<GiantStarPro>(), base.npc.damage, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
					}
					if (base.npc.ai[2] >= 90f)
					{
						if (this.repeat <= 2)
						{
							this.repeat++;
							if (this.phase > 1)
							{
								base.npc.ai[2] = 20f;
							}
							else
							{
								base.npc.ai[2] = 0f;
							}
							base.npc.netUpdate = true;
						}
						else
						{
							this.repeat = 0;
							base.npc.velocity = Vector2.Zero;
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 4f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
					}
					if (base.npc.velocity.Length() < 10f && base.npc.ai[3] == 6f)
					{
						base.npc.ai[3] = 0f;
					}
					break;
				case 4:
					if (base.npc.ai[3] != 6f)
					{
						base.npc.LookAtPlayer();
						base.npc.netUpdate = true;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 15f)
					{
						NPC npc6 = base.npc;
						npc6.velocity.Y = npc6.velocity.Y - 1f;
					}
					if (base.npc.ai[2] == 15f)
					{
						base.npc.ai[3] = 6f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] == 5f || base.npc.ai[2] == 15f)
					{
						base.npc.Shoot(new Vector2(player.Center.X, player.Center.Y + 350f), ModContent.ProjectileType<DashT>(), 0, new Vector2(0f, -6f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
						for (int n = 0; n < 4; n++)
						{
							int dustID = Dust.NewDust(new Vector2(player.Center.X - 1f, player.Center.Y - 1f + 350f), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
							Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)n / 8f * 6.28f);
							Main.dust[dustID].noLight = false;
							Main.dust[dustID].noGravity = true;
						}
					}
					if (base.npc.ai[2] == 10f || ((this.phase >= 1) ? (base.npc.ai[2] == 20f) : (base.npc.ai[2] == -1f)))
					{
						base.npc.Shoot(new Vector2(player.Center.X, player.Center.Y - 350f), ModContent.ProjectileType<DashT>(), 0, new Vector2(0f, 6f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
						for (int m2 = 0; m2 < 4; m2++)
						{
							int dustID2 = Dust.NewDust(new Vector2(player.Center.X - 1f, player.Center.Y - 1f - 350f), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
							Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m2 / 8f * 6.28f);
							Main.dust[dustID2].noLight = false;
							Main.dust[dustID2].noGravity = true;
						}
					}
					if (base.npc.ai[2] == 50f)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = (float)((this.phase < 1) ? -30 : -35);
						this.Teleport(true, new Vector2(0f, 350f));
					}
					if ((this.phase < 1) ? (base.npc.ai[2] == 70f) : (base.npc.ai[2] == 65f))
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = (float)((this.phase < 1) ? 30 : 35);
						this.Teleport(true, new Vector2(0f, -350f));
					}
					if ((this.phase < 1) ? (base.npc.ai[2] == 90f) : (base.npc.ai[2] == 80f))
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = (float)((this.phase < 1) ? -30 : -35);
						this.Teleport(true, new Vector2(0f, 350f));
					}
					if (this.phase >= 1 && base.npc.ai[2] == 95f)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = (float)((this.phase < 1) ? 30 : 35);
						this.Teleport(true, new Vector2(0f, -350f));
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.velocity = Vector2.Zero;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 5:
				{
					base.npc.LookAtPlayer();
					if (!ScreenPlayer.NebCutscene)
					{
						if (this.circleRadius > 700)
						{
							this.circleRadius -= 2;
						}
						for (int k2 = 0; k2 < 6; k2++)
						{
							double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
							this.vector.X = (float)(Math.Sin(angle) * (double)this.circleRadius);
							this.vector.Y = (float)(Math.Cos(angle) * (double)this.circleRadius);
							Dust dust2 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 58, 0f, 0f, 100, default(Color), 2f)];
							dust2.noGravity = true;
							dust2.velocity = -base.npc.DirectionTo(dust2.position) * 2f;
						}
						if (base.npc.Distance(player.Center) > (float)this.circleRadius)
						{
							Vector2 movement = base.npc.Center - player.Center;
							float difference = movement.Length() - (float)this.circleRadius;
							movement.Normalize();
							movement *= ((difference < 17f) ? difference : 17f);
							player.position += movement;
						}
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f && this.phase < 3)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout9>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 40f)
					{
						base.npc.ai[3] = 7f;
					}
					if (base.npc.ai[2] == 50f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					int sizeOfChains = 32;
					float speed = 1f;
					base.npc.TargetClosest(true);
					if (base.npc.ai[2] == 1f)
					{
						int randFactor = 80;
						for (int i2 = 0; i2 < this.ChainPos.Length; i2++)
						{
							this.ChainPos[i2] = base.npc.Center;
						}
						this.temp[0] = player.Center + new Vector2((float)Main.rand.Next(-randFactor, randFactor), (float)Main.rand.Next(-randFactor, randFactor));
						this.temp[1] = player.Center + new Vector2((base.npc.Center.X - player.Center.X) * 2f, 0f) + new Vector2((float)Main.rand.Next(-randFactor, randFactor), (float)Main.rand.Next(-randFactor, randFactor));
						this.temp[2] = player.Center + new Vector2((base.npc.Center.X - player.Center.X) * 2f, (base.npc.Center.Y - player.Center.Y) * 2f) + new Vector2((float)Main.rand.Next(-randFactor, randFactor), (float)Main.rand.Next(-randFactor, randFactor));
						this.temp[3] = player.Center + new Vector2(0f, (base.npc.Center.Y - player.Center.Y) * 2f) + new Vector2((float)Main.rand.Next(-randFactor, randFactor), (float)Main.rand.Next(-randFactor, randFactor));
						for (int i3 = 0; i3 < this.ChainPos.Length; i3++)
						{
							this.temp[i3] += this.temp[i3] - this.ChainPos[i3];
						}
					}
					for (int i4 = 0; i4 < this.ChainPos.Length; i4++)
					{
						this.getGrad[i4] = (this.temp[i4] - this.ChainPos[i4]) / 32f;
						if (!this.ChainHitBoxArea[i4].Intersects(this.PlayerSafeHitBox) && base.npc.ai[2] < 800f && base.npc.ai[2] > 50f)
						{
							this.ChainPos[i4] += this.getGrad[i4] * speed;
						}
						this.ChainHitBoxArea[i4] = new Rectangle((int)this.ChainPos[i4].X - sizeOfChains / 2, (int)this.ChainPos[i4].Y - sizeOfChains / 2, sizeOfChains, sizeOfChains);
					}
					this.PlayerSafeHitBox = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
					for (int i5 = 0; i5 < this.ChainPos.Length; i5++)
					{
						if (this.ChainHitBoxArea[i5].Intersects(this.PlayerSafeHitBox))
						{
							if (!ScreenPlayer.NebCutscene && base.npc.ai[2] < 300f)
							{
								base.npc.ai[2] = 180f;
								for (int m3 = 0; m3 < 8; m3++)
								{
									int dustID3 = Dust.NewDust(new Vector2(player.Center.X - 1f, player.Center.Y - 1f), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
									Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m3 / 8f * 6.28f);
									Main.dust[dustID3].noLight = false;
									Main.dust[dustID3].noGravity = true;
								}
								ScreenPlayer.NebCutscene = true;
							}
							if (base.npc.ai[2] < 300f)
							{
								Vector2[] chainPos = this.ChainPos;
								int num = i5;
								chainPos[num].Y = chainPos[num].Y + (base.npc.ai[2] - 180f) / 30f;
								player.Center = this.ChainPos[i5];
							}
							else
							{
								if (base.npc.ai[2] == 300f)
								{
									for (int m4 = 0; m4 < 8; m4++)
									{
										int dustID4 = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y - 1000f), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
										Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m4 / 8f * 6.28f);
										Main.dust[dustID4].noLight = false;
										Main.dust[dustID4].noGravity = true;
									}
									this.temp[i5] = new Vector2(player.Center.X, player.Center.Y - 1000f);
									base.npc.Shoot(new Vector2(player.Center.X, player.Center.Y - 1000f), ModContent.ProjectileType<StationaryStar>(), 200, Vector2.Zero, false, SoundID.Item117, "", 0f, 0f);
								}
								else if (this.temp[i5].Y > player.Center.Y && ScreenPlayer.NebCutscene)
								{
									base.npc.ai[2] = 800f;
									ScreenPlayer.NebCutscene = false;
								}
								if (base.npc.ai[2] < 800f)
								{
									Vector2[] chainPos2 = this.ChainPos;
									int num2 = i5;
									chainPos2[num2].Y = chainPos2[num2].Y - (base.npc.ai[2] - 180f) / 4f;
									player.Center = this.ChainPos[i5];
								}
							}
						}
					}
					if (!this.ChainHitBoxArea[0].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[1].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[2].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[3].Intersects(this.PlayerSafeHitBox))
					{
						ScreenPlayer.NebCutscene = false;
					}
					for (int i6 = 0; i6 < this.ChainPos.Length; i6++)
					{
						if (base.npc.ai[2] > 800f)
						{
							this.ChainPos[i6] += (base.npc.Center - this.ChainPos[i6]) / 10f;
						}
					}
					if (base.npc.ai[2] == 850f)
					{
						for (int i7 = 0; i7 < this.ChainPos.Length; i7++)
						{
							this.ChainPos[i7] = base.npc.Center;
						}
						base.npc.velocity = Vector2.Zero;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] >= 240f && !this.ChainHitBoxArea[0].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[1].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[2].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[3].Intersects(this.PlayerSafeHitBox) && base.npc.ai[2] < 800f)
					{
						ScreenPlayer.NebCutsceneflag = false;
						ScreenPlayer.NebCutscene = false;
						base.npc.velocity = Vector2.Zero;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				}
				case 6:
					base.npc.LookAtPlayer();
					if (this.circleRadius > 600)
					{
						this.circleRadius--;
					}
					for (int k3 = 0; k3 < 6; k3++)
					{
						double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
						this.vector.X = (float)(Math.Sin(angle2) * (double)this.circleRadius);
						this.vector.Y = (float)(Math.Cos(angle2) * (double)this.circleRadius);
						Dust dust3 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 58, 0f, 0f, 100, default(Color), 2f)];
						dust3.noGravity = true;
						dust3.velocity = -base.npc.DirectionTo(dust3.position) * 2f;
					}
					if (base.npc.Distance(player.Center) > (float)this.circleRadius)
					{
						Vector2 movement2 = base.npc.Center - player.Center;
						float difference2 = movement2.Length() - (float)this.circleRadius;
						movement2.Normalize();
						movement2 *= ((difference2 < 17f) ? difference2 : 17f);
						player.position += movement2;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f)
					{
						if (this.phase < 3)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout10>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<NebRing>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 20f)
					{
						base.npc.ai[3] = 8f;
					}
					if (base.npc.ai[2] == 30f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y + 132f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X - 67f, base.npc.Center.Y + 115f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X + 67f, base.npc.Center.Y + 115f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 60f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X - 115f, base.npc.Center.Y + 67f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X + 115f, base.npc.Center.Y + 67f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X - 132f, base.npc.Center.Y), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X + 132f, base.npc.Center.Y), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X - 115f, base.npc.Center.Y - 67f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X + 115f, base.npc.Center.Y - 67f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 90f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 132f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X - 67f, base.npc.Center.Y - 115f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X + 67f, base.npc.Center.Y - 115f), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
					}
					if (this.phase >= 3)
					{
						if (base.npc.ai[2] == 95f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout1>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
						if (base.npc.ai[2] == 100f)
						{
							base.npc.ai[3] = 1f;
						}
						if (base.npc.ai[2] == 130f || base.npc.ai[2] == 170f)
						{
							int pieCut5 = 8;
							for (int m5 = 0; m5 < pieCut5; m5++)
							{
								if (Main.netMode != 1)
								{
									int projID5 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.01f, 0f);
									Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m5 / (float)pieCut5 * 6.28f);
									Main.projectile[projID5].netUpdate = true;
								}
							}
						}
						if (base.npc.ai[2] == 150f)
						{
							int pieCut6 = 8;
							for (int m6 = 0; m6 < pieCut6; m6++)
							{
								if (Main.netMode != 1)
								{
									int projID6 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.002f, 1f);
									Main.projectile[projID6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m6 / (float)pieCut6 * 6.28f);
									Main.projectile[projID6].netUpdate = true;
								}
							}
						}
						if (base.npc.ai[2] == 220f)
						{
							base.npc.ai[3] = 2f;
						}
					}
					if (base.npc.ai[2] >= 250f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 8:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f && this.phase < 2)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout8>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 3f;
					}
					if (base.npc.ai[2] >= 40f && base.npc.ai[2] < 120f && Main.rand.Next(4) == 0)
					{
						int A2 = Main.rand.Next(-200, 200) * 6;
						int B2 = Main.rand.Next(-200, 200) - 1000;
						base.npc.Shoot(new Vector2(player.Center.X + (float)A2, player.Center.Y + (float)B2), ModContent.ProjectileType<StarfallTele>(), 120, new Vector2((base.npc.spriteDirection != 1) ? -2f : 2f, 6f), false, SoundID.Item9.WithVolume(0.5f), "", 0f, 0f);
					}
					if (base.npc.ai[2] == 50f)
					{
						base.npc.ai[3] = 4f;
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 9:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f && this.phase < 2)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout5>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 1f;
					}
					if (((this.phase < 2) ? (base.npc.ai[2] % 5f == 0f) : (base.npc.ai[2] % 3f == 0f)) && base.npc.ai[2] >= 30f && base.npc.ai[2] <= 60f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<CurvingStarTele2>(), 120, new Vector2((float)Main.rand.Next(-7, 7), (float)Main.rand.Next(-7, 7)), false, SoundID.Item9.WithVolume(0f), "", 1.01f, 0f);
					}
					if (base.npc.ai[2] == 60f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 100f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 10:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f && this.phase < 2)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout6>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 20f)
					{
						base.npc.ai[3] = 5f;
						this.armFrames[3] = 0;
					}
					if (base.npc.ai[2] == 30f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 60f && this.repeat < 3)
					{
						this.Teleport(false, Vector2.Zero);
						base.npc.ai[2] = 20f;
						base.npc.ai[3] = 5f;
						this.armFrames[3] = 0;
						this.repeat++;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] >= 90f)
					{
						this.repeat = 0;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 11:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f && this.phase < 4)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y - 100f), ModContent.ProjectileType<Shout11>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 3f;
					}
					if (base.npc.ai[2] >= 40f && base.npc.ai[2] < 120f && base.npc.ai[2] % 15f == 0f)
					{
						int A3 = Main.rand.Next(-200, 200) * 6;
						int B3 = Main.rand.Next(-200, 200) - 1000;
						base.npc.Shoot(new Vector2(player.Center.X + (float)A3, player.Center.Y + (float)B3), ModContent.ProjectileType<CrystalStarTele>(), 120, new Vector2((base.npc.spriteDirection != 1) ? -2f : 2f, 6f), false, SoundID.Item9.WithVolume(0.5f), "", 0f, 0f);
					}
					if (base.npc.ai[2] == 50f)
					{
						base.npc.ai[3] = 4f;
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 12:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 1f;
					}
					if (base.npc.ai[2] == 30f || base.npc.ai[2] == 50f)
					{
						int pieCut7 = 8;
						for (int m7 = 0; m7 < pieCut7; m7++)
						{
							if (Main.netMode != 1)
							{
								int projID7 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.01f, 0f);
								Main.projectile[projID7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m7 / (float)pieCut7 * 6.28f);
								Main.projectile[projID7].netUpdate = true;
							}
						}
					}
					if (base.npc.ai[2] == 40f)
					{
						int pieCut8 = 16;
						for (int m8 = 0; m8 < pieCut8; m8++)
						{
							if (Main.netMode != 1)
							{
								int projID8 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.002f, 1f);
								Main.projectile[projID8].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m8 / (float)pieCut8 * 6.28f);
								Main.projectile[projID8].netUpdate = true;
							}
						}
					}
					if (base.npc.ai[2] == 100f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 140f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				}
				break;
			case 6:
				base.npc.LookAtPlayer();
				ScreenPlayer.NebCutsceneflag = false;
				ScreenPlayer.NebCutscene = false;
				for (int i8 = 0; i8 < this.ChainPos.Length; i8++)
				{
					this.ChainPos[i8] = base.npc.Center;
				}
				this.frameCounters = 0;
				base.npc.rotation = 0f;
				base.npc.velocity = Vector2.Zero;
				base.npc.ai[3] = 0f;
				base.npc.ai[1] = 0f;
				if (RedeWorld.nebDeath < 5 || base.npc.life >= (int)((float)base.npc.lifeMax * 0.01f))
				{
					base.npc.ai[2] = 0f;
				}
				else
				{
					base.npc.ai[2] = 2500f;
				}
				base.npc.ai[0] = 7f;
				if (this.phase < 1 && base.npc.life < (int)((float)base.npc.lifeMax * 0.75f))
				{
					this.phase = 1;
					base.npc.netUpdate = true;
				}
				if (this.phase < 2 && base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
				{
					this.phase = 2;
					base.npc.netUpdate = true;
				}
				if (this.phase < 3 && base.npc.life < (int)((float)base.npc.lifeMax * 0.25f))
				{
					this.phase = 3;
					base.npc.netUpdate = true;
				}
				base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<ShockwaveBoom>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				base.npc.netUpdate = true;
				break;
			case 7:
				base.npc.LookAtPlayer();
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 30f)
				{
					Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
					this.RazzleDazzle();
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 40f)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] > 250f)
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 4f;
					base.npc.netUpdate = true;
				}
				break;
			}
			if (Vector2.Distance(base.npc.Center, player.Center) >= 950f && base.npc.ai[0] > 0f && base.npc.ai[1] != 4f && base.npc.ai[1] != 5f && base.npc.ai[1] != 6f && base.npc.ai[1] != 3f)
			{
				this.Teleport(false, Vector2.Zero);
				base.npc.netUpdate = true;
			}
		}

		public override bool CheckDead()
		{
			return true;
		}

		public void Dash(int speed, bool directional, Vector2 target)
		{
			Player player = Main.player[base.npc.target];
			this.RazzleDazzle();
			Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
			if (target == Vector2.Zero)
			{
				target = player.Center;
			}
			if (directional)
			{
				base.npc.velocity = base.npc.DirectionTo(target) * (float)speed;
				return;
			}
			base.npc.velocity.X = (float)((target.X > base.npc.Center.X) ? speed : (-(float)speed));
		}

		public void Teleport(bool specialPos, Vector2 teleportPos)
		{
			Player player = Main.player[base.npc.target];
			this.RazzleDazzle();
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
			if (!Main.dedServ)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Teleport1").WithVolume(0.5f).WithPitchVariance(0.1f), (int)base.npc.position.X, (int)base.npc.position.Y);
			}
			this.RazzleDazzle();
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

		public void RazzleDazzle()
		{
			if (Main.netMode != 1)
			{
				int dustType = 58;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustType2 = 59;
				int pieCut2 = 10;
				for (int j = 0; j < pieCut2; j++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)j / (float)pieCut2 * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				int dustType3 = 60;
				int pieCut3 = 12;
				for (int k = 0; k < pieCut3; k++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType3, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)pieCut3 * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
				int dustType4 = 62;
				int pieCut4 = 14;
				for (int l = 0; l < pieCut4; l++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType4, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)pieCut4 * 6.28f);
					Main.dust[dustID4].noLight = false;
					Main.dust[dustID4].noGravity = true;
				}
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D chain = base.mod.GetTexture("NPCs/Bosses/Neb/CosmosChain1");
			Texture2D wings = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Wings");
			Texture2D armsAni = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_Idle");
			Texture2D armsPrayAni = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_Pray");
			Texture2D armsPrayGlow = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_Pray_Glow");
			Texture2D armsStarfallAni = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_Starfall");
			Texture2D armsStarfallGlow = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_Starfall_Glow");
			Texture2D armsPiercingAni = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_PiercingNebula");
			Texture2D armsPiercingGlow = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_PiercingNebula_Glow");
			Texture2D armsChainAni = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_CosmicChain");
			Texture2D armsChainGlow = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_CosmicChain_Glow");
			Texture2D armsEyesAni = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_LongCharge");
			Texture2D armsEyesGlow = base.mod.GetTexture("NPCs/Bosses/Neb/NebP1_Arms_LongCharge_Glow");
			int spriteDirection = base.npc.spriteDirection;
			int shader = GameShaders.Armor.GetShaderIdFromItemId(2870);
			Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			for (int i = this.oldPos.Length - 1; i >= 0; i--)
			{
				float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
				spriteBatch.Draw(texture, this.oldPos[i] - Main.screenPosition, new Rectangle?(base.npc.frame), Main.DiscoColor * (0.5f * alpha), this.oldrot[i], Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.ai[1] == 5f && base.npc.ai[2] > 50f && base.npc.ai[0] < 6f)
			{
				for (int j = 0; j < this.ChainPos.Length; j++)
				{
					RedeHelper.DrawBezier(spriteBatch, chain, "", Main.DiscoColor, base.npc.Center, this.ChainPos[j], (base.npc.Center + this.ChainPos[j]) / 2f + new Vector2(0f, (float)(130 + (int)(Math.Sin((double)(base.npc.ai[2] / 12f)) * (double)(150f - base.npc.ai[2] / 3f)))), (base.npc.Center + this.ChainPos[j]) / 2f + new Vector2(0f, (float)(130 + (int)(Math.Sin((double)(base.npc.ai[2] / 12f)) * (double)(150f - base.npc.ai[2] / 3f)))), 0.04f, 0f);
				}
			}
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), RedeColor.NebColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			BaseDrawing.DrawTexture(spriteBatch, wings, shader, base.npc, new Color?(base.npc.GetAlpha(Color.White)), true, Utils.Size(base.npc.frame) / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			if (base.npc.ai[3] != 6f)
			{
				if (base.npc.ai[3] == 0f)
				{
					int num214 = armsAni.Height / 4;
					int y6 = num214 * this.armFrames[0];
					Main.spriteBatch.Draw(armsAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, armsAni.Width, num214)), RedeColor.NebColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 1f || base.npc.ai[3] == 2f)
				{
					int num215 = armsPrayAni.Height / 8;
					int y7 = num215 * this.armFrames[1];
					Main.spriteBatch.Draw(armsPrayAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, armsPrayAni.Width, num215)), RedeColor.NebColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsPrayAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					Main.spriteBatch.Draw(armsPrayGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, armsPrayAni.Width, num215)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)armsPrayAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 3f || base.npc.ai[3] == 4f)
				{
					int num216 = armsStarfallAni.Height / 8;
					int y8 = num216 * this.armFrames[2];
					Main.spriteBatch.Draw(armsStarfallAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, armsStarfallAni.Width, num216)), RedeColor.NebColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsStarfallAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					Main.spriteBatch.Draw(armsStarfallGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, armsStarfallAni.Width, num216)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)armsStarfallAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 5f)
				{
					int num217 = armsPiercingAni.Height / 9;
					int y9 = num217 * this.armFrames[3];
					Main.spriteBatch.Draw(armsPiercingAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, armsPiercingAni.Width, num217)), RedeColor.NebColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsPiercingAni.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					Main.spriteBatch.Draw(armsPiercingGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, armsPiercingAni.Width, num217)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)armsPiercingAni.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 7f)
				{
					int num218 = armsChainAni.Height / 7;
					int y10 = num218 * this.armFrames[4];
					Main.spriteBatch.Draw(armsChainAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y10, armsChainAni.Width, num218)), RedeColor.NebColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsChainAni.Width / 2f, (float)num218 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					Main.spriteBatch.Draw(armsChainGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y10, armsChainAni.Width, num218)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)armsChainAni.Width / 2f, (float)num218 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 8f)
				{
					int num219 = armsEyesAni.Height / 19;
					int y11 = num219 * this.armFrames[5];
					Main.spriteBatch.Draw(armsEyesAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y11, armsEyesAni.Width, num219)), RedeColor.NebColour * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsEyesAni.Width / 2f, (float)num219 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					Main.spriteBatch.Draw(armsEyesGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y11, armsEyesAni.Width, num219)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)armsEyesAni.Width / 2f, (float)num219 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public Vector2[] oldPos = new Vector2[5];

		public float[] oldrot = new float[5];

		public Vector2 MoveVector2;

		private Vector2 vector;

		public int frameCounters;

		public int repeat;

		public int floatTimer;

		public int floatTimer2;

		public int[] armFrames = new int[6];

		private Vector2[] ChainPos = new Vector2[4];

		private Vector2[] getGrad = new Vector2[4];

		public Vector2[] temp = new Vector2[4];

		private readonly Rectangle[] ChainHitBoxArea = new Rectangle[4];

		private Rectangle PlayerSafeHitBox;

		public int phase;

		public int circleTimer;

		public int circleRadius;

		public List<int> AttackList = new List<int>
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			8,
			9,
			10,
			11,
			12
		};

		public List<int> CopyList;
	}
}
