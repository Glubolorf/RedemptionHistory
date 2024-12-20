using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	[AutoloadBossHead]
	public class NebuleusClone : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus Mirage");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 650000;
			base.npc.defense = 170;
			base.npc.damage = 200;
			base.npc.width = 50;
			base.npc.height = 76;
			base.npc.aiStyle = -1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath59;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.netAlways = true;
			base.npc.noTileCollide = false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.55f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void NPCLoot()
		{
			if (!Main.expertMode)
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebuleusTrophy"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebuleusMask"), 1, false, 0, false, false);
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebuleusVanity"), 1, false, 0, false, false);
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebWings"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GildedBonnet"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FreedomStarN"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebulaStarFlail"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ConstellationsBook"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("StarfruitSeedbag"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CosmosChainWeapon"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PiercingNebulaWeapon"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("StarSerpentsCollar"), 1, false, 0, false, false);
				}
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GalaxyHeart"), 1, false, 0, false, false);
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				int dustType = 58;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustType2 = 59;
				int pieCut2 = 10;
				for (int j = 0; j < pieCut2; j++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)j / (float)pieCut2 * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				int dustType3 = 60;
				int pieCut3 = 12;
				for (int k = 0; k < pieCut3; k++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType3, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(18f, 0f), (float)k / (float)pieCut3 * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
				int dustType4 = 62;
				int pieCut4 = 14;
				for (int l = 0; l < pieCut4; l++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType4, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(20f, 0f), (float)l / (float)pieCut4 * 6.28f);
					Main.dust[dustID4].noLight = false;
					Main.dust[dustID4].noGravity = true;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 58, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.75;
			if (Main.expertMode && base.npc.life <= (int)((float)base.npc.lifeMax * 0.07f))
			{
				damage = 1.0;
			}
			return true;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
				writer.Write(this.teleportTimer);
				writer.Write(this.beginFight);
				writer.Write(this.teleport);
				writer.Write(this.razzleDazzle);
				writer.Write(this.phase2);
				writer.Write(this.phase3);
				writer.Write(this.phase4);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
				this.teleportTimer = reader.ReadInt32();
				this.beginFight = reader.ReadBool();
				this.teleport = reader.ReadBool();
				this.razzleDazzle = reader.ReadBool();
				this.phase2 = reader.ReadBool();
				this.phase3 = reader.ReadBool();
				this.phase4 = reader.ReadBool();
			}
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (base.npc.ai[0] == 0f)
			{
				NPC npc = base.npc;
				npc.velocity.Y = npc.velocity.Y + 0.005f;
				if (base.npc.velocity.Y > 0.3f)
				{
					base.npc.ai[0] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[0] == 1f)
			{
				NPC npc2 = base.npc;
				npc2.velocity.Y = npc2.velocity.Y - 0.005f;
				if (base.npc.velocity.Y < -0.3f)
				{
					base.npc.ai[0] = 0f;
					base.npc.netUpdate = true;
				}
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc3 = base.npc;
				npc3.frame.Y = npc3.frame.Y + 82;
				if (base.npc.frame.Y > 328)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (base.npc.ai[2] != 1f)
			{
				this.customAI[0] += 1f;
			}
			if (this.customAI[0] == 1f)
			{
				this.razzleDazzle = true;
			}
			if (!this.beginFight)
			{
				base.npc.dontTakeDamage = true;
				if (this.customAI[0] == 120f)
				{
					this.beginFight = true;
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernHead"), 0, 0f, 0f, 0f, 0f, 255);
					base.npc.netUpdate = true;
				}
			}
			if (this.beginFight)
			{
				if (NPC.AnyNPCs(base.mod.NPCType("StarWyvernHead")))
				{
					base.npc.dontTakeDamage = true;
					if (this.customAI[0] == 1500f || this.customAI[0] == 1700f || this.customAI[0] == 1900f || this.customAI[0] == 1940f)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut = 8;
						for (int i = 0; i < pieCut; i++)
						{
							int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar"), 50, 3f, 255, 0f, 0f);
							Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)i / (float)pieCut * 6.28f);
							Main.projectile[projID].netUpdate = true;
						}
					}
					if (this.customAI[0] == 1600f || this.customAI[0] == 1800f || this.customAI[0] == 1920f || this.customAI[0] == 1960f)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut2 = 8;
						for (int j = 0; j < pieCut2; j++)
						{
							int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStarO"), 50, 3f, 255, 0f, 0f);
							Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)j / (float)pieCut2 * 6.28f);
							Main.projectile[projID2].netUpdate = true;
						}
					}
					if (this.customAI[0] >= 2000f)
					{
						this.customAI[0] = 1400f;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (base.npc.life > (int)((float)base.npc.lifeMax * 0.07f))
					{
						base.npc.dontTakeDamage = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[0] < 3000f)
					{
						this.customAI[0] = 3000f;
					}
					if (this.customAI[0] == 3300f)
					{
						base.npc.ai[2] = 1f;
						base.npc.netUpdate = true;
					}
				}
				if (base.npc.ai[2] == 1f)
				{
					if (base.npc.life > (int)((float)base.npc.lifeMax * 0.75f))
					{
						switch (Main.rand.Next(4))
						{
						case 0:
							base.npc.ai[1] = 1f;
							break;
						case 1:
							base.npc.ai[1] = 2f;
							break;
						case 2:
							base.npc.ai[1] = 3f;
							break;
						case 3:
							base.npc.ai[1] = 4f;
							break;
						}
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					else if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.75f) && base.npc.life > (int)((float)base.npc.lifeMax * 0.5f))
					{
						if (!this.phase2)
						{
							base.npc.ai[3] += 1f;
							if (base.npc.ai[3] == 1f)
							{
								int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ShockwaveBoom"), 0, 1f, Main.myPlayer, 0f, 0f);
								Main.projectile[p].netUpdate = true;
							}
							if (base.npc.ai[3] == 31f)
							{
								Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
							}
							if (base.npc.ai[3] == 40f)
							{
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							}
							if (base.npc.ai[3] > 90f)
							{
								this.phase2 = true;
								base.npc.ai[3] = 0f;
								base.npc.netUpdate = true;
							}
						}
						if (this.phase2)
						{
							switch (Main.rand.Next(8))
							{
							case 0:
								base.npc.ai[1] = 1f;
								break;
							case 1:
								base.npc.ai[1] = 2f;
								break;
							case 2:
								base.npc.ai[1] = 3f;
								break;
							case 3:
								base.npc.ai[1] = 4f;
								break;
							case 4:
								base.npc.ai[1] = 5f;
								break;
							case 5:
								base.npc.ai[1] = 6f;
								break;
							case 6:
								base.npc.ai[1] = 7f;
								break;
							case 7:
								base.npc.ai[1] = 8f;
								break;
							}
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
					}
					else if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.5f) && base.npc.life > (int)((float)base.npc.lifeMax * 0.25f))
					{
						if (!this.phase3)
						{
							base.npc.ai[3] += 1f;
							if (base.npc.ai[3] == 1f)
							{
								int p2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ShockwaveBoom"), 0, 1f, Main.myPlayer, 0f, 0f);
								Main.projectile[p2].netUpdate = true;
							}
							if (base.npc.ai[3] == 31f)
							{
								Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
							}
							if (base.npc.ai[3] == 40f)
							{
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							}
							if (base.npc.ai[3] > 90f)
							{
								this.phase3 = true;
								base.npc.ai[3] = 0f;
								base.npc.netUpdate = true;
							}
						}
						if (this.phase3)
						{
							switch (Main.rand.Next(10))
							{
							case 0:
								base.npc.ai[1] = 1f;
								break;
							case 1:
								base.npc.ai[1] = 2f;
								break;
							case 2:
								base.npc.ai[1] = 3f;
								break;
							case 3:
								base.npc.ai[1] = 4f;
								break;
							case 4:
								base.npc.ai[1] = 5f;
								break;
							case 5:
								base.npc.ai[1] = 6f;
								break;
							case 6:
								base.npc.ai[1] = 7f;
								break;
							case 7:
								base.npc.ai[1] = 8f;
								break;
							case 8:
								base.npc.ai[1] = 9f;
								break;
							case 9:
								base.npc.ai[1] = 10f;
								break;
							}
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
					}
					else if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.25f))
					{
						if (Main.expertMode)
						{
							if (base.npc.life > (int)((float)base.npc.lifeMax * 0.07f))
							{
								if (!this.phase4)
								{
									base.npc.ai[3] += 1f;
									if (base.npc.ai[3] == 1f)
									{
										int p3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ShockwaveBoom"), 0, 1f, Main.myPlayer, 0f, 0f);
										Main.projectile[p3].netUpdate = true;
									}
									if (base.npc.ai[3] == 31f)
									{
										Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
									}
									if (base.npc.ai[3] == 40f)
									{
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
									}
									if (base.npc.ai[3] > 90f)
									{
										this.phase4 = true;
										base.npc.ai[3] = 0f;
										base.npc.netUpdate = true;
									}
								}
								if (this.phase4)
								{
									switch (Main.rand.Next(11))
									{
									case 0:
										base.npc.ai[1] = 11f;
										break;
									case 1:
										base.npc.ai[1] = 2f;
										break;
									case 2:
										base.npc.ai[1] = 12f;
										break;
									case 3:
										base.npc.ai[1] = 13f;
										break;
									case 4:
										base.npc.ai[1] = 5f;
										break;
									case 5:
										base.npc.ai[1] = 6f;
										break;
									case 6:
										base.npc.ai[1] = 7f;
										break;
									case 7:
										base.npc.ai[1] = 8f;
										break;
									case 8:
										base.npc.ai[1] = 9f;
										break;
									case 9:
										base.npc.ai[1] = 10f;
										break;
									case 10:
										base.npc.ai[1] = 14f;
										break;
									}
									base.npc.ai[2] = 0f;
									base.npc.netUpdate = true;
								}
							}
						}
						else
						{
							if (!this.phase4)
							{
								base.npc.ai[3] += 1f;
								if (base.npc.ai[3] == 1f)
								{
									int p4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ShockwaveBoom"), 0, 1f, Main.myPlayer, 0f, 0f);
									Main.projectile[p4].netUpdate = true;
								}
								if (base.npc.ai[3] == 31f)
								{
									Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
								}
								if (base.npc.ai[3] == 30f)
								{
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								}
								if (base.npc.ai[3] > 90f)
								{
									this.phase4 = true;
									base.npc.ai[3] = 0f;
									base.npc.netUpdate = true;
								}
							}
							if (this.phase4)
							{
								switch (Main.rand.Next(11))
								{
								case 0:
									base.npc.ai[1] = 11f;
									break;
								case 1:
									base.npc.ai[1] = 2f;
									break;
								case 2:
									base.npc.ai[1] = 12f;
									break;
								case 3:
									base.npc.ai[1] = 13f;
									break;
								case 4:
									base.npc.ai[1] = 5f;
									break;
								case 5:
									base.npc.ai[1] = 6f;
									break;
								case 6:
									base.npc.ai[1] = 7f;
									break;
								case 7:
									base.npc.ai[1] = 8f;
									break;
								case 8:
									base.npc.ai[1] = 9f;
									break;
								case 9:
									base.npc.ai[1] = 10f;
									break;
								case 10:
									base.npc.ai[1] = 14f;
									break;
								}
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
							}
						}
					}
					if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.07f) && Main.expertMode)
					{
						base.npc.dontTakeDamage = true;
						base.npc.ai[3] += 1f;
						if (base.npc.ai[3] == 1f)
						{
							Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[3] == 10f)
						{
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3453, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3454, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3455, 1, false, 0, false, false);
						}
						if (base.npc.ai[3] >= 0f)
						{
							this.musicChange1 = false;
							base.npc.netUpdate = true;
						}
						else
						{
							this.musicChange1 = true;
						}
						if (base.npc.ai[3] == 563f)
						{
							int p5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("Transition"), 0, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p5].netUpdate = true;
						}
						if (base.npc.ai[3] == 660f)
						{
							int p6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ShockwaveBoom2"), 0, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p6].netUpdate = true;
						}
						if (base.npc.ai[3] > 690f)
						{
							base.npc.SetDefaults(base.mod.NPCType("BigNebuleusClone"), -1f);
							base.npc.netUpdate = true;
						}
					}
				}
			}
			if (base.npc.ai[1] == 1f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout1"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[projID3].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 100f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut3 = 8;
					for (int k = 0; k < pieCut3; k++)
					{
						int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)k / (float)pieCut3 * 6.28f);
						Main.projectile[projID4].netUpdate = true;
					}
				}
				if (base.npc.ai[3] == 80f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut4 = 8;
					for (int l = 0; l < pieCut4; l++)
					{
						int projID5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStarO"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)l / (float)pieCut4 * 6.28f);
						Main.projectile[projID5].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 160f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 2f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout2"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p7].netUpdate = true;
				}
				if (base.npc.ai[3] >= 50f && base.npc.ai[3] <= 90f && Main.rand.Next(2) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int A = Main.rand.Next(-50, 50);
					int B = Main.rand.Next(-200, 200) - 1000;
					int p8 = Projectile.NewProjectile(player.Center.X + (float)A, player.Center.Y + (float)B, 0f, 13f, base.mod.ProjectileType("StarFallPro"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p8].netUpdate = true;
				}
				if (base.npc.ai[3] >= 100f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 3f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int projID6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout3"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[projID6].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 90f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed = 14f;
					Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage = 50;
					int type = base.mod.ProjectileType("PNebula1");
					int type2 = base.mod.ProjectileType("PNebula2");
					int type3 = base.mod.ProjectileType("PNebula3");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int p9 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					int p10 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type2, damage, 0f, 0, 0f, 0f);
					int p11 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type3, damage, 0f, 0, 0f, 0f);
					Main.projectile[p9].netUpdate = true;
					Main.projectile[p10].netUpdate = true;
					Main.projectile[p11].netUpdate = true;
				}
				if (base.npc.ai[3] >= 100f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 4f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p12 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout4"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p12].netUpdate = true;
				}
				if (base.npc.ai[3] == 50f)
				{
					Main.PlaySound(SoundID.Item121, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed2 = 7f;
					Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage2 = 40;
					int type4 = base.mod.ProjectileType("StarVortex1");
					int type5 = base.mod.ProjectileType("StarVortex2");
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type4, damage2, 0f, 0, 0f, 0f);
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type5, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
					Main.projectile[num55].netUpdate = true;
				}
				if (base.npc.ai[3] >= 70f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 5f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p13 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout5"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p13].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] < 90f)
				{
					if (Main.rand.Next(15) == 0)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p14 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("CurvingStar"), 50, 1f, 255, 0f, 0f);
						Main.projectile[p14].netUpdate = true;
					}
					if (Main.rand.Next(15) == 0)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p15 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("CurvingStarO"), 50, 1f, 255, 0f, 0f);
						Main.projectile[p15].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 100f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 6f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p16 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout6"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p16].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut5 = 8;
					for (int m = 0; m < pieCut5; m++)
					{
						int projID7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula1"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)m / (float)pieCut5 * 6.28f);
						int projID8 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula2"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID8].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)m / (float)pieCut5 * 6.28f);
						int projID9 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula3"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)m / (float)pieCut5 * 6.28f);
						Main.projectile[projID7].netUpdate = true;
						Main.projectile[projID8].netUpdate = true;
						Main.projectile[projID9].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 80f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 7f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p17 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout7"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p17].netUpdate = true;
				}
				if (base.npc.ai[3] == 20f)
				{
					int p18 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					int p19 = Projectile.NewProjectile(base.npc.Center.X - 100f, base.npc.Center.Y + 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					int p20 = Projectile.NewProjectile(base.npc.Center.X + 100f, base.npc.Center.Y + 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p18].netUpdate = true;
					Main.projectile[p19].netUpdate = true;
					Main.projectile[p20].netUpdate = true;
				}
				if (base.npc.ai[3] >= 100f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 8f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p21 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout8"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p21].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] <= 160f && Main.rand.Next(4) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int A2 = Main.rand.Next(-200, 200) * 6;
					int B2 = Main.rand.Next(-200, 200) - 1000;
					int p22 = Projectile.NewProjectile(player.Center.X + (float)A2, player.Center.Y + (float)B2, 2f, 6f, base.mod.ProjectileType("StarFallPro"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p22].netUpdate = true;
				}
				if (base.npc.ai[3] >= 160f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 9f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p23 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout9"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p23].netUpdate = true;
				}
				if (base.npc.ai[3] == 50f || base.npc.ai[3] == 70f || base.npc.ai[3] == 90f || base.npc.ai[3] == 110f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed3 = 26f;
					Vector2 vector10 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage3 = 10;
					int type6 = base.mod.ProjectileType("CosmosChain1");
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type6, damage3, 0f, 0, 0f, 0f);
					Main.projectile[num56].netUpdate = true;
				}
				if (base.npc.ai[3] >= 115f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 10f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p24 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout10"), 0, 1f, Main.myPlayer, 0f, 0f);
					int p25 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("NebRing"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p24].netUpdate = true;
					Main.projectile[p25].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f)
				{
					int Minion = NPC.NewNPC((int)base.npc.Center.X - 100, (int)base.npc.Center.Y + 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion].netUpdate = true;
				}
				if (base.npc.ai[3] == 70f)
				{
					int Minion2 = NPC.NewNPC((int)base.npc.Center.X - 100, (int)base.npc.Center.Y - 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion2].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f)
				{
					int Minion3 = NPC.NewNPC((int)base.npc.Center.X + 100, (int)base.npc.Center.Y - 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion3].netUpdate = true;
				}
				if (base.npc.ai[3] == 90f)
				{
					int Minion4 = NPC.NewNPC((int)base.npc.Center.X + 100, (int)base.npc.Center.Y + 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion4].netUpdate = true;
				}
				if (base.npc.ai[3] == 100f)
				{
					int Minion5 = NPC.NewNPC((int)base.npc.Center.X + 200, (int)base.npc.Center.Y, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion5].netUpdate = true;
				}
				if (base.npc.ai[3] == 110f)
				{
					int Minion6 = NPC.NewNPC((int)base.npc.Center.X - 200, (int)base.npc.Center.Y, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion6].netUpdate = true;
				}
				if (base.npc.ai[3] == 120f)
				{
					int Minion7 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 200, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion7].netUpdate = true;
				}
				if (base.npc.ai[3] == 130f)
				{
					int Minion8 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y - 200, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion8].netUpdate = true;
				}
				if (base.npc.ai[3] >= 160f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 11f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p26 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout1"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p26].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 80f || base.npc.ai[3] == 140f || base.npc.ai[3] == 160f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut6 = 8;
					for (int n = 0; n < pieCut6; n++)
					{
						int projID10 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar2"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID10].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)n / (float)pieCut6 * 6.28f);
						Main.projectile[projID10].netUpdate = true;
					}
				}
				if (base.npc.ai[3] == 70f || base.npc.ai[3] == 150f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut7 = 8;
					for (int m2 = 0; m2 < pieCut7; m2++)
					{
						int projID11 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar2O"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID11].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m2 / (float)pieCut7 * 6.28f);
						Main.projectile[projID11].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 180f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 12f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p27 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout3"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p27].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 70f || base.npc.ai[3] == 80f || base.npc.ai[3] == 90f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed4 = 15f;
					Vector2 vector11 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage4 = 50;
					int type7 = base.mod.ProjectileType("PNebula1");
					int type8 = base.mod.ProjectileType("PNebula2");
					int type9 = base.mod.ProjectileType("PNebula3");
					float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
					int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type7, damage4, 0f, 0, 0f, 0f);
					int num58 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type8, damage4, 0f, 0, 0f, 0f);
					int num59 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type9, damage4, 0f, 0, 0f, 0f);
					Main.projectile[num57].netUpdate = true;
					Main.projectile[num58].netUpdate = true;
					Main.projectile[num59].netUpdate = true;
				}
				if (base.npc.ai[3] >= 100f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 13f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p28 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout4"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p28].netUpdate = true;
				}
				if (base.npc.ai[3] == 50f || base.npc.ai[3] == 80f)
				{
					Main.PlaySound(SoundID.Item121, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed5 = 7f;
					Vector2 vector12 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage5 = 40;
					int type10 = base.mod.ProjectileType("StarVortex1");
					int type11 = base.mod.ProjectileType("StarVortex2");
					float rotation5 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					int num60 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type10, damage5, 0f, 0, 0f, 0f);
					int num61 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type11, damage5, 0f, 0, 0f, 0f);
					Main.projectile[num60].netUpdate = true;
					Main.projectile[num61].netUpdate = true;
				}
				if (base.npc.ai[3] >= 90f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 14f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int projID12 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout11"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[projID12].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] <= 90f && Main.rand.Next(10) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int A3 = Main.rand.Next(-200, 200) * 6;
					int B3 = Main.rand.Next(-200, 200) - 1000;
					int p29 = Projectile.NewProjectile(player.Center.X + (float)A3, player.Center.Y + (float)B3, 2f, 6f, base.mod.ProjectileType("CrystalStarPro1"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p29].netUpdate = true;
				}
				if (base.npc.ai[3] >= 100f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.Distance(Main.player[base.npc.target].Center) >= 950f && !this.teleport)
			{
				this.teleport = true;
				base.npc.netUpdate = true;
			}
			if (this.teleport)
			{
				base.npc.netUpdate = true;
				this.teleportTimer++;
				if (this.teleportTimer == 2)
				{
					this.razzleDazzle = true;
				}
				if (this.teleportTimer == 4 && Main.netMode != 1)
				{
					int num62 = Main.rand.Next(2);
					if (num62 == 0)
					{
						Vector2 newPos = new Vector2((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + newPos;
						base.npc.netUpdate = true;
					}
					if (num62 == 1)
					{
						Vector2 newPos2 = new Vector2((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + newPos2;
						base.npc.netUpdate = true;
					}
				}
				if (this.teleportTimer >= 6)
				{
					this.razzleDazzle = true;
					this.teleport = false;
					this.teleportTimer = 0;
					base.npc.netUpdate = true;
				}
			}
			if (this.razzleDazzle)
			{
				int dustType = 58;
				int pieCut8 = 8;
				for (int m3 = 0; m3 < pieCut8; m3++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m3 / (float)pieCut8 * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustType2 = 59;
				int pieCut9 = 10;
				for (int m4 = 0; m4 < pieCut9; m4++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)m4 / (float)pieCut9 * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				int dustType3 = 60;
				int pieCut10 = 12;
				for (int m5 = 0; m5 < pieCut10; m5++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType3, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m5 / (float)pieCut10 * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
				int dustType4 = 62;
				int pieCut11 = 14;
				for (int m6 = 0; m6 < pieCut11; m6++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType4, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m6 / (float)pieCut11 * 6.28f);
					Main.dust[dustID4].noLight = false;
					Main.dust[dustID4].noGravity = true;
				}
				base.npc.netUpdate = true;
				this.razzleDazzle = false;
			}
			if (this.musicChange1)
			{
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
			}
			if (Main.expertMode && base.npc.life <= (int)((float)base.npc.lifeMax * 0.07f))
			{
				base.npc.immortal = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			base.mod.GetTexture("NPCs/Bosses/Nebuleus/Nebuleus_Wings");
			Vector2 Drawpos = base.npc.Center - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY);
			int shader = GameShaders.Armor.GetShaderIdFromItemId(2870);
			Texture2D myGlowTex = base.mod.GetTexture("NPCs/Bosses/Nebuleus/Nebuleus_Wings");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, Drawpos, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White) * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			BaseDrawing.DrawTexture(spriteBatch, myGlowTex, shader, base.npc, new Color?(base.npc.GetAlpha(Color.White)), true, Utils.Size(base.npc.frame) / 2f);
			return false;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
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

		private Player player;

		public float[] customAI = new float[1];

		private int teleportTimer;

		private bool beginFight;

		private bool teleport;

		private bool razzleDazzle;

		private bool musicChange1;

		private bool phase2;

		private bool phase3;

		private bool phase4;
	}
}
