using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items;
using Redemption.Items.Armor.PostML;
using Redemption.Items.DruidDamageClass.SeedBags;
using Redemption.Items.Placeable;
using Redemption.Items.Weapons.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	[AutoloadBossHead]
	public class Thorn : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Thorn, Bane of the Forest");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 2100;
			base.npc.defense = 6;
			base.npc.damage = 14;
			base.npc.width = 62;
			base.npc.height = 60;
			base.npc.aiStyle = -1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0f;
			base.npc.alpha = 0;
			base.npc.noGravity = false;
			base.npc.boss = true;
			base.npc.netAlways = true;
			base.npc.noTileCollide = false;
			this.bossBag = ModContent.ItemType<ThornBag>();
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ThornTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ThornMask>(), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CursedGrassSword>(), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CursedThornBow>(), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<RootTendril>(), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ThornSeedBag>(), 1, false, 0, false, false);
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 28;
			if (!RedeWorld.downedThorn)
			{
				RedeWorld.redemptionPoints++;
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
							{
								Main.NewText("<Chalice of Alignment> Nice work, the forest is safe now.", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gold, "+1", true, false);
					}
				}
			}
			RedeWorld.downedThorn = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
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
				writer.Write(this.teleportTimer);
				writer.Write(this.beginFight);
				writer.Write(this.teleport);
				writer.Write(this.attacking);
				writer.Write(this.appearing);
				writer.Write(this.disappearing);
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
				this.teleportTimer = reader.ReadInt32();
				this.beginFight = reader.ReadBool();
				this.teleport = reader.ReadBool();
				this.attacking = reader.ReadBool();
				this.appearing = reader.ReadBool();
				this.disappearing = reader.ReadBool();
			}
		}

		public override void AI()
		{
			if (!this.title)
			{
				Redemption.ShowTitle(base.npc, 5);
				this.title = true;
			}
			this.Target();
			this.DespawnHandler();
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 15.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 68;
				if (base.npc.frame.Y > 68)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (this.attacking)
			{
				this.attackCounter++;
				if (this.attackCounter > 15)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 4)
				{
					this.attackFrame = 2;
				}
			}
			if (this.appearing)
			{
				this.appearCounter++;
				if (this.appearCounter > 5)
				{
					this.appearFrame++;
					this.appearCounter = 0;
				}
				if (this.appearFrame >= 12)
				{
					this.appearFrame = 11;
				}
			}
			if (this.disappearing)
			{
				this.disappearCounter++;
				if (this.disappearCounter > 5)
				{
					this.disappearFrame++;
					this.disappearCounter = 0;
				}
				if (this.disappearFrame >= 11)
				{
					this.disappearFrame = 10;
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
			if (this.customAI[1] == 0f)
			{
				this.teleport = true;
				base.npc.netUpdate = true;
			}
			else if (this.customAI[1] == 1f)
			{
				this.teleport = true;
				base.npc.netUpdate = true;
				this.customAI[3] = 1f;
			}
			else if (this.customAI[1] == 2f)
			{
				this.customAI[1] = 3f;
				this.choice = Main.rand.Next(5);
				base.npc.netUpdate = true;
			}
			else if (this.customAI[1] == 3f)
			{
				base.npc.netUpdate = true;
				switch (this.choice)
				{
				case 0:
					this.attacking = true;
					this.customAI[0] += 1f;
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
					{
						if (this.customAI[0] == 60f)
						{
							int p = Projectile.NewProjectile(player.Center.X + 500f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p2 = Projectile.NewProjectile(player.Center.X + -500f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p].netUpdate = true;
							Main.projectile[p2].netUpdate = true;
						}
						if (this.customAI[0] == 80f)
						{
							int p3 = Projectile.NewProjectile(player.Center.X + 400f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p4 = Projectile.NewProjectile(player.Center.X + -400f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p3].netUpdate = true;
							Main.projectile[p4].netUpdate = true;
						}
						if (this.customAI[0] == 100f)
						{
							int p5 = Projectile.NewProjectile(player.Center.X + 300f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p6 = Projectile.NewProjectile(player.Center.X + -300f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p5].netUpdate = true;
							Main.projectile[p6].netUpdate = true;
						}
						if (this.customAI[0] == 120f)
						{
							int p7 = Projectile.NewProjectile(player.Center.X + 200f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p8 = Projectile.NewProjectile(player.Center.X + -200f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p7].netUpdate = true;
							Main.projectile[p8].netUpdate = true;
						}
						if (this.customAI[0] == 140f)
						{
							int p9 = Projectile.NewProjectile(player.Center.X + 100f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p10 = Projectile.NewProjectile(player.Center.X + -100f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p9].netUpdate = true;
							Main.projectile[p10].netUpdate = true;
						}
						if (this.customAI[0] == 160f)
						{
							int p11 = Projectile.NewProjectile(player.Center.X + 0f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p11].netUpdate = true;
						}
						if (this.customAI[0] >= 240f)
						{
							this.customAI[0] = 0f;
							this.customAI[1] = 4f;
							this.teleportTimer = 0;
							this.attacking = false;
							this.teleport = true;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						if (this.customAI[0] == 60f)
						{
							int p12 = Projectile.NewProjectile(player.Center.X + 500f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p13 = Projectile.NewProjectile(player.Center.X + -500f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p12].netUpdate = true;
							Main.projectile[p13].netUpdate = true;
						}
						if (this.customAI[0] == 90f)
						{
							int p14 = Projectile.NewProjectile(player.Center.X + 400f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p15 = Projectile.NewProjectile(player.Center.X + -400f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p14].netUpdate = true;
							Main.projectile[p15].netUpdate = true;
						}
						if (this.customAI[0] == 120f)
						{
							int p16 = Projectile.NewProjectile(player.Center.X + 300f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p17 = Projectile.NewProjectile(player.Center.X + -300f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p16].netUpdate = true;
							Main.projectile[p17].netUpdate = true;
						}
						if (this.customAI[0] == 150f)
						{
							int p18 = Projectile.NewProjectile(player.Center.X + 200f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p19 = Projectile.NewProjectile(player.Center.X + -200f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p18].netUpdate = true;
							Main.projectile[p19].netUpdate = true;
						}
						if (this.customAI[0] == 180f)
						{
							int p20 = Projectile.NewProjectile(player.Center.X + 100f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							int p21 = Projectile.NewProjectile(player.Center.X + -100f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p20].netUpdate = true;
							Main.projectile[p21].netUpdate = true;
						}
						if (this.customAI[0] == 210f)
						{
							int p22 = Projectile.NewProjectile(player.Center.X + 0f, player.Center.Y - 200f, 0f, 0f, ModContent.ProjectileType<ThornSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p22].netUpdate = true;
						}
						if (this.customAI[0] >= 320f)
						{
							this.customAI[0] = 0f;
							this.customAI[1] = 4f;
							this.teleportTimer = 0;
							this.attacking = false;
							this.teleport = true;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 1:
					if (Vector2.Distance(base.npc.Center, player.Center) < 300f)
					{
						this.customAI[0] += 1f;
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
						{
							if (this.customAI[0] == 60f || this.customAI[0] == 90f || this.customAI[0] == 120f || this.customAI[0] == 150f || this.customAI[0] == 180f)
							{
								float Speed = 19f;
								Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
								int damage = 12;
								int type = ModContent.ProjectileType<CursedThornPro3>();
								float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
								int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
								Main.projectile[num54].netUpdate = true;
							}
						}
						else if (this.customAI[0] == 60f || this.customAI[0] == 90f || this.customAI[0] == 120f)
						{
							float Speed2 = 16f;
							Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage2 = 10;
							int type2 = ModContent.ProjectileType<CursedThornPro3>();
							float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
							int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
							Main.projectile[num55].netUpdate = true;
						}
						if (this.customAI[0] >= 200f)
						{
							this.customAI[0] = 0f;
							this.customAI[1] = 4f;
							this.teleportTimer = 0;
							this.teleport = true;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						base.npc.netUpdate = true;
						this.customAI[0] = 0f;
						this.choice = Main.rand.Next(5);
					}
					break;
				case 2:
					this.customAI[0] += 1f;
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
					{
						if (this.customAI[0] == 10f)
						{
							for (int i = 0; i < 40; i++)
							{
								double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
								this.vector.X = (float)(Math.Sin(angle) * 100.0);
								this.vector.Y = (float)(Math.Cos(angle) * 100.0);
								Dust dust2 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 3, 0f, 0f, 100, default(Color), 3f)];
								dust2.noGravity = true;
								dust2.velocity = -base.npc.DirectionTo(dust2.position) * 10f;
							}
						}
						if (this.customAI[0] == 80f || this.customAI[0] == 140f)
						{
							int pieCut = 4;
							for (int j = 0; j < pieCut; j++)
							{
								int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
								Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)pieCut * 6.28f);
								Main.projectile[projID].netUpdate = true;
								Main.projectile[projID].timeLeft = 180;
							}
						}
						if (this.customAI[0] == 110f)
						{
							int pieCut2 = 8;
							for (int k = 0; k < pieCut2; k++)
							{
								int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
								Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)pieCut2 * 6.28f);
								Main.projectile[projID2].netUpdate = true;
								Main.projectile[projID2].timeLeft = 180;
							}
						}
						if (this.customAI[0] == 170f)
						{
							int pieCut3 = 16;
							for (int l = 0; l < pieCut3; l++)
							{
								int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
								Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)pieCut3 * 6.28f);
								Main.projectile[projID3].netUpdate = true;
								Main.projectile[projID3].timeLeft = 180;
							}
						}
						if (this.customAI[0] >= 200f)
						{
							if (Main.rand.Next(3) == 0)
							{
								base.npc.netUpdate = true;
								this.customAI[0] = 0f;
								this.choice = Main.rand.Next(5);
							}
							else
							{
								this.customAI[0] = 0f;
								this.customAI[1] = 4f;
								this.teleportTimer = 0;
								this.teleport = true;
								base.npc.netUpdate = true;
							}
						}
					}
					else
					{
						if (this.customAI[0] == 10f)
						{
							for (int m = 0; m < 40; m++)
							{
								double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
								this.vector.X = (float)(Math.Sin(angle2) * 100.0);
								this.vector.Y = (float)(Math.Cos(angle2) * 100.0);
								Dust dust3 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 3, 0f, 0f, 100, default(Color), 3f)];
								dust3.noGravity = true;
								dust3.velocity = -base.npc.DirectionTo(dust3.position) * 10f;
							}
						}
						if (this.customAI[0] == 80f)
						{
							int pieCut4 = 4;
							for (int n = 0; n < pieCut4; n++)
							{
								int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
								Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)n / (float)pieCut4 * 6.28f);
								Main.projectile[projID4].netUpdate = true;
								Main.projectile[projID4].timeLeft = 180;
							}
						}
						if (this.customAI[0] == 120f)
						{
							int pieCut5 = 8;
							for (int m2 = 0; m2 < pieCut5; m2++)
							{
								int projID5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
								Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m2 / (float)pieCut5 * 6.28f);
								Main.projectile[projID5].netUpdate = true;
								Main.projectile[projID5].timeLeft = 180;
							}
						}
						if (this.customAI[0] >= 180f)
						{
							if (Main.rand.Next(3) == 0)
							{
								base.npc.netUpdate = true;
								this.customAI[0] = 0f;
								this.choice = Main.rand.Next(5);
							}
							else
							{
								this.customAI[0] = 0f;
								this.customAI[1] = 4f;
								this.teleportTimer = 0;
								this.teleport = true;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				case 3:
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.9f))
					{
						this.customAI[0] += 1f;
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
						{
							if (this.customAI[0] < 60f)
							{
								for (int k2 = 0; k2 < 3; k2++)
								{
									double angle3 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
									this.vector.X = (float)(Math.Sin(angle3) * 100.0);
									this.vector.Y = (float)(Math.Cos(angle3) * 100.0);
									Dust dust4 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 235, 0f, 0f, 100, default(Color), 1f)];
									dust4.noGravity = true;
									dust4.velocity = -base.npc.DirectionTo(dust4.position) * 10f;
								}
							}
							if (this.customAI[0] >= 60f && this.customAI[0] <= 140f && Main.rand.Next(2) == 0)
							{
								float Speed3 = 9f;
								Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
								int damage3 = 10;
								int type3 = ModContent.ProjectileType<LifeThornPro>();
								float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
								int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), type3, damage3, 0f, 0, (float)base.npc.whoAmI, 0f);
								Main.projectile[num56].netUpdate = true;
							}
							if (this.customAI[0] >= 240f)
							{
								if (Main.rand.Next(3) == 0)
								{
									base.npc.netUpdate = true;
									this.customAI[0] = 0f;
									this.choice = Main.rand.Next(5);
								}
								else
								{
									this.customAI[0] = 0f;
									this.customAI[1] = 4f;
									this.teleportTimer = 0;
									this.teleport = true;
									base.npc.netUpdate = true;
								}
							}
						}
						else
						{
							if (this.customAI[0] < 60f)
							{
								for (int k3 = 0; k3 < 3; k3++)
								{
									double angle4 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
									this.vector.X = (float)(Math.Sin(angle4) * 100.0);
									this.vector.Y = (float)(Math.Cos(angle4) * 100.0);
									Dust dust5 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 235, 0f, 0f, 100, default(Color), 1f)];
									dust5.noGravity = true;
									dust5.velocity = -base.npc.DirectionTo(dust5.position) * 10f;
								}
							}
							if (this.customAI[0] >= 60f && this.customAI[0] <= 90f && Main.rand.Next(2) == 0)
							{
								float Speed4 = 11f;
								Vector2 vector11 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
								int damage4 = 10;
								int type4 = ModContent.ProjectileType<LifeThornPro>();
								float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
								int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0) + (float)Main.rand.Next(-1, 1), type4, damage4, 0f, 0, (float)base.npc.whoAmI, 0f);
								Main.projectile[num57].netUpdate = true;
							}
							if (this.customAI[0] >= 180f)
							{
								if (Main.rand.Next(3) == 0)
								{
									base.npc.netUpdate = true;
									this.customAI[0] = 0f;
									this.choice = Main.rand.Next(5);
								}
								else
								{
									this.customAI[0] = 0f;
									this.customAI[1] = 4f;
									this.teleportTimer = 0;
									this.teleport = true;
									base.npc.netUpdate = true;
								}
							}
						}
					}
					else
					{
						base.npc.netUpdate = true;
						this.customAI[0] = 0f;
						this.choice = Main.rand.Next(5);
					}
					break;
				case 4:
					this.customAI[0] += 1f;
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
					{
						if (this.customAI[0] == 60f || this.customAI[0] == 90f || this.customAI[0] == 120f || this.customAI[0] == 150f || this.customAI[0] == 180f)
						{
							for (int index = 0; index < 8; index++)
							{
								Dust dust6 = Dust.NewDustDirect(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 2f);
								dust6.velocity = -player.DirectionTo(dust6.position) * 10f;
								dust6.noGravity = true;
							}
							for (int k4 = 0; k4 < 16; k4++)
							{
								double angle5 = (double)k4 * 0.39269908169872414;
								this.vector.X = (float)(Math.Sin(angle5) * 60.0);
								this.vector.Y = (float)(Math.Cos(angle5) * 60.0);
								Dust dust7 = Main.dust[Dust.NewDust(player.Center + this.vector - new Vector2(4f, 4f), 1, 1, 269, 0f, 0f, 100, default(Color), 2f)];
								dust7.noGravity = true;
								dust7.velocity = -player.DirectionTo(dust7.position) * 10f;
							}
							int ProjID = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<SlashFlashPro>(), 10, 3f, 255, 0f, 0f);
							Main.projectile[ProjID].netUpdate = true;
						}
						if (this.customAI[0] >= 230f)
						{
							if (Main.rand.Next(4) == 0)
							{
								base.npc.netUpdate = true;
								this.customAI[0] = 0f;
								this.choice = Main.rand.Next(5);
							}
							else
							{
								this.customAI[0] = 0f;
								this.customAI[1] = 4f;
								this.teleportTimer = 0;
								this.teleport = true;
								base.npc.netUpdate = true;
							}
						}
					}
					else
					{
						if (this.customAI[0] == 50f || this.customAI[0] == 100f || this.customAI[0] == 150f || this.customAI[0] == 200f)
						{
							for (int index2 = 0; index2 < 8; index2++)
							{
								Dust dust8 = Dust.NewDustDirect(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 2f);
								dust8.velocity = -player.DirectionTo(dust8.position) * 10f;
								dust8.noGravity = true;
							}
							for (int k5 = 0; k5 < 16; k5++)
							{
								double angle6 = (double)k5 * 0.39269908169872414;
								this.vector.X = (float)(Math.Sin(angle6) * 60.0);
								this.vector.Y = (float)(Math.Cos(angle6) * 60.0);
								Dust dust9 = Main.dust[Dust.NewDust(player.Center + this.vector - new Vector2(4f, 4f), 1, 1, 269, 0f, 0f, 100, default(Color), 2f)];
								dust9.noGravity = true;
								dust9.velocity = -player.DirectionTo(dust9.position) * 10f;
							}
							int p23 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<SlashFlashPro>(), 10, 3f, 255, 0f, 0f);
							Main.projectile[p23].netUpdate = true;
						}
						if (this.customAI[0] >= 250f)
						{
							if (Main.rand.Next(4) == 0)
							{
								base.npc.netUpdate = true;
								this.customAI[0] = 0f;
								this.choice = Main.rand.Next(5);
							}
							else
							{
								this.customAI[0] = 0f;
								this.customAI[1] = 4f;
								this.teleportTimer = 0;
								this.teleport = true;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				}
			}
			if ((Main.expertMode ? (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f)) : (base.npc.life < (int)((float)base.npc.lifeMax * 0.35f))) && !NPC.AnyNPCs(ModContent.NPCType<ManaBarrierPro>()))
			{
				int dustType = 20;
				int pieCut6 = 16;
				for (int m3 = 0; m3 < pieCut6; m3++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m3 / (float)pieCut6 * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				float distance = 2f;
				float k6 = 0.4f;
				for (int count = 0; count < 1; count++)
				{
					Vector2 spawn = base.npc.Center + distance * Utils.ToRotationVector2((float)count * k6);
					int Minion = NPC.NewNPC((int)spawn.X, (int)spawn.Y, ModContent.NPCType<ManaBarrierPro>(), 0, (float)base.npc.whoAmI, 0f, (float)count, 0f, 255);
					Main.npc[Minion].netUpdate = true;
				}
			}
			if (this.teleport)
			{
				this.teleportTimer++;
				if (this.teleportTimer >= 2 && this.teleportTimer <= 62)
				{
					this.disappearing = true;
				}
				if (this.teleportTimer == 62)
				{
					if (base.npc.ai[2] != 0f && base.npc.ai[3] != 0f)
					{
						base.npc.position.X = base.npc.ai[2] * 16f - (float)(base.npc.width / 2) + 8f;
						base.npc.position.Y = base.npc.ai[3] * 16f - (float)base.npc.height;
						base.npc.velocity.X = 0f;
						base.npc.velocity.Y = 0f;
						base.npc.ai[2] = 0f;
						base.npc.ai[3] = 0f;
					}
					base.npc.ai[0] = 1f;
					int playerTilePositionX = (int)Main.player[base.npc.target].position.X / 16;
					int playerTilePositionY = (int)Main.player[base.npc.target].position.Y / 16;
					int npcTilePositionX = (int)base.npc.position.X / 16;
					int npcTilePositionY = (int)base.npc.position.Y / 16;
					int playerTargetShift = 16;
					int num58 = 0;
					for (int s = 0; s < 100; s++)
					{
						num58++;
						int nearPlayerX = Main.rand.Next(playerTilePositionX - playerTargetShift, playerTilePositionX + playerTargetShift);
						for (int num59 = Main.rand.Next(playerTilePositionY - playerTargetShift, playerTilePositionY + playerTargetShift); num59 < playerTilePositionY + playerTargetShift; num59++)
						{
							if ((nearPlayerX < playerTilePositionX - 12 || nearPlayerX > playerTilePositionX + 12) && (num59 < npcTilePositionY - 1 || num59 > npcTilePositionY + 1 || nearPlayerX < npcTilePositionX - 1 || nearPlayerX > npcTilePositionX + 1) && Main.tile[nearPlayerX, num59].nactive())
							{
								bool flag5 = true;
								if (Main.tile[nearPlayerX, num59 - 1].lava())
								{
									flag5 = false;
								}
								if (flag5 && Main.tileSolid[(int)Main.tile[nearPlayerX, num59].type] && !Collision.SolidTiles(nearPlayerX - 1, nearPlayerX + 1, num59 - 4, num59 - 1))
								{
									base.npc.ai[1] = 20f;
									base.npc.ai[2] = (float)nearPlayerX;
									base.npc.ai[3] = (float)num59 - 1f;
									break;
								}
							}
						}
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[1] > 0f)
					{
						base.npc.ai[1] -= 1f;
					}
				}
				if (this.teleportTimer >= 64 && this.teleportTimer <= 124)
				{
					this.appearing = true;
					this.disappearing = false;
				}
				if (this.teleportTimer > 124)
				{
					this.appearing = false;
					this.disappearing = false;
					this.attacking = false;
					this.teleport = false;
					this.teleportTimer = 0;
					if (this.customAI[3] != 0f)
					{
						this.customAI[1] = 2f;
					}
					else
					{
						this.customAI[1] = 1f;
					}
					base.npc.netUpdate = true;
					this.attackFrame = 0;
					this.appearFrame = 0;
					this.disappearFrame = 0;
					this.disappearCounter = 0;
					this.appearCounter = 0;
					this.attackCounter = 0;
					base.npc.netUpdate = true;
				}
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (this.appearing || this.disappearing)
			{
				damage *= 0.4;
			}
			return true;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !this.appearing && !this.disappearing;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D attackAni = base.mod.GetTexture("NPCs/Bosses/Thorn/ThornAttack1");
			Texture2D appearAni = base.mod.GetTexture("NPCs/Bosses/Thorn/ThornAppear");
			Texture2D disappearAni = base.mod.GetTexture("NPCs/Bosses/Thorn/ThornDisappear");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.attacking && !this.appearing && !this.disappearing)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.attacking && !this.appearing && !this.disappearing)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = attackAni.Height / 4;
				int y6 = num214 * this.attackFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.appearing && !this.disappearing)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = appearAni.Height / 12;
				int y7 = num215 * this.appearFrame;
				Main.spriteBatch.Draw(appearAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, appearAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)appearAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (!this.appearing && this.disappearing)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num216 = disappearAni.Height / 11;
				int y8 = num216 * this.disappearFrame;
				Main.spriteBatch.Draw(disappearAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, disappearAni.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)disappearAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
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

		public float[] customAI = new float[4];

		public int choice;

		public Vector2 vector;

		private bool title;

		public static Texture2D glowTex;

		private bool beginFight;

		private bool teleport;

		private int teleportTimer;

		private bool attacking;

		private bool appearing;

		private bool disappearing;

		private int attackFrame;

		private int appearFrame;

		private int disappearFrame;

		private int attackCounter;

		private int appearCounter;

		private int disappearCounter;
	}
}
