﻿using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	[AutoloadBossHead]
	public class ThornPZ : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Thorn, Bane of the Forest");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 210000;
			base.npc.defense = 38;
			base.npc.damage = 140;
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
			if (RedeConfigClient.Instance.AntiAntti)
			{
				this.music = 5;
			}
			else
			{
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossForest1");
			}
			base.npc.noTileCollide = false;
			this.bossBag = base.mod.ItemType("ThornPZBag");
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ThornTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ThornMask"), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThornBow2"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThornFlail"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThorns"), Main.rand.Next(9, 18), false, 0, false, false);
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
				for (int i = 0; i < 40; i++)
				{
					Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			RedeWorld.downedThornPZ = true;
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
				if (this.appearCounter > 3)
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
				if (this.disappearCounter > 3)
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
			if (this.customAI[1] != 1f)
			{
				this.customAI[3] += 1f;
			}
			if (this.customAI[3] == 1f)
			{
				this.teleport = true;
				base.npc.netUpdate = true;
			}
			if (this.customAI[3] == 2f)
			{
				base.npc.alpha = 0;
				base.npc.netUpdate = true;
			}
			if (this.customAI[1] == 1f)
			{
				switch (Main.rand.Next(6))
				{
				case 0:
					this.customAI[2] = 1f;
					break;
				case 1:
					this.customAI[2] = 2f;
					break;
				case 2:
					this.customAI[2] = 3f;
					break;
				case 3:
					this.customAI[2] = 4f;
					break;
				case 4:
					this.customAI[2] = 5f;
					break;
				case 5:
					this.customAI[2] = 6f;
					break;
				}
				this.customAI[1] = 0f;
				this.customAI[0] = 0f;
				base.npc.netUpdate = true;
			}
			if (this.customAI[2] == 1f)
			{
				this.attacking = true;
				this.customAI[0] += 1f;
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
				{
					if (this.customAI[0] == 30f)
					{
						int p = Projectile.NewProjectile(player.Center.X + 500f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p2 = Projectile.NewProjectile(player.Center.X + -500f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p].netUpdate = true;
						Main.projectile[p2].netUpdate = true;
					}
					if (this.customAI[0] == 40f)
					{
						int p3 = Projectile.NewProjectile(player.Center.X + 400f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p4 = Projectile.NewProjectile(player.Center.X + -400f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p3].netUpdate = true;
						Main.projectile[p4].netUpdate = true;
					}
					if (this.customAI[0] == 50f)
					{
						int p5 = Projectile.NewProjectile(player.Center.X + 300f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p6 = Projectile.NewProjectile(player.Center.X + -300f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p5].netUpdate = true;
						Main.projectile[p6].netUpdate = true;
					}
					if (this.customAI[0] == 60f)
					{
						int p7 = Projectile.NewProjectile(player.Center.X + 200f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p8 = Projectile.NewProjectile(player.Center.X + -200f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p7].netUpdate = true;
						Main.projectile[p8].netUpdate = true;
					}
					if (this.customAI[0] == 70f)
					{
						int p9 = Projectile.NewProjectile(player.Center.X + 100f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p10 = Projectile.NewProjectile(player.Center.X + -100f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p9].netUpdate = true;
						Main.projectile[p10].netUpdate = true;
					}
					if (this.customAI[0] == 80f)
					{
						int p11 = Projectile.NewProjectile(player.Center.X + 0f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p11].netUpdate = true;
					}
					if (this.customAI[0] >= 160f)
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.attacking = false;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (this.customAI[0] == 30f)
					{
						int p12 = Projectile.NewProjectile(player.Center.X + 500f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p13 = Projectile.NewProjectile(player.Center.X + -500f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p12].netUpdate = true;
						Main.projectile[p13].netUpdate = true;
					}
					if (this.customAI[0] == 50f)
					{
						int p14 = Projectile.NewProjectile(player.Center.X + 400f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p15 = Projectile.NewProjectile(player.Center.X + -400f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p14].netUpdate = true;
						Main.projectile[p15].netUpdate = true;
					}
					if (this.customAI[0] == 70f)
					{
						int p16 = Projectile.NewProjectile(player.Center.X + 300f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p17 = Projectile.NewProjectile(player.Center.X + -300f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p16].netUpdate = true;
						Main.projectile[p17].netUpdate = true;
					}
					if (this.customAI[0] == 90f)
					{
						int p18 = Projectile.NewProjectile(player.Center.X + 200f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p19 = Projectile.NewProjectile(player.Center.X + -200f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p18].netUpdate = true;
						Main.projectile[p19].netUpdate = true;
					}
					if (this.customAI[0] == 110f)
					{
						int p20 = Projectile.NewProjectile(player.Center.X + 100f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						int p21 = Projectile.NewProjectile(player.Center.X + -100f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p20].netUpdate = true;
						Main.projectile[p21].netUpdate = true;
					}
					if (this.customAI[0] == 130f)
					{
						int p22 = Projectile.NewProjectile(player.Center.X + 0f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 45, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[p22].netUpdate = true;
					}
					if (this.customAI[0] >= 200f)
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.attacking = false;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
			}
			else if (this.customAI[2] == 2f)
			{
				this.customAI[0] += 1f;
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
				{
					if (this.customAI[0] == 60f || this.customAI[0] == 90f || this.customAI[0] == 120f || this.customAI[0] == 150f || this.customAI[0] == 180f)
					{
						float Speed = 24f;
						Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage = 46;
						int type = base.mod.ProjectileType("CursedThornPro6");
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
					}
				}
				else if (this.customAI[0] == 40f || this.customAI[0] == 60f || this.customAI[0] == 80f || this.customAI[0] == 100f)
				{
					float Speed2 = 16f;
					Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage2 = 46;
					int type2 = base.mod.ProjectileType("CursedThornPro6");
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
				}
				if (this.customAI[0] >= 200f)
				{
					this.customAI[0] = 0f;
					this.customAI[2] = 0f;
					this.teleportTimer = 0;
					this.teleport = true;
					base.npc.netUpdate = true;
				}
			}
			else if (this.customAI[2] == 3f)
			{
				this.customAI[0] += 1f;
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
				{
					if (this.customAI[0] == 60f || this.customAI[0] == 100f)
					{
						int pieCut = 8;
						for (int i = 0; i < pieCut; i++)
						{
							int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 45, 3f, 255, 0f, 0f);
							Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
							Main.projectile[projID].netUpdate = true;
							Main.projectile[projID].timeLeft = 180;
						}
					}
					if (this.customAI[0] == 80f || this.customAI[0] == 120f)
					{
						int pieCut2 = 16;
						for (int j = 0; j < pieCut2; j++)
						{
							int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 45, 3f, 255, 0f, 0f);
							Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)pieCut2 * 6.28f);
							Main.projectile[projID2].netUpdate = true;
							Main.projectile[projID2].timeLeft = 180;
						}
					}
					if (this.customAI[0] >= 200f)
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (this.customAI[0] == 40f || this.customAI[0] == 120f)
					{
						int pieCut3 = 8;
						for (int k = 0; k < pieCut3; k++)
						{
							int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 45, 3f, 255, 0f, 0f);
							Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)pieCut3 * 6.28f);
							Main.projectile[projID3].netUpdate = true;
							Main.projectile[projID3].timeLeft = 180;
						}
					}
					if (this.customAI[0] == 80f)
					{
						int pieCut4 = 16;
						for (int l = 0; l < pieCut4; l++)
						{
							int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 45, 3f, 255, 0f, 0f);
							Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)pieCut4 * 6.28f);
							Main.projectile[projID4].netUpdate = true;
							Main.projectile[projID4].timeLeft = 180;
						}
					}
					if (this.customAI[0] >= 220f)
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
			}
			else if (this.customAI[2] == 4f)
			{
				this.customAI[0] += 1f;
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
				{
					if (this.customAI[0] >= 50f && this.customAI[0] <= 160f && Main.rand.Next(2) == 0)
					{
						float Speed3 = 17f;
						Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage3 = 45;
						int type3 = ModContent.ProjectileType<LifeThornPro2>();
						float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
						int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), type3, damage3, 0f, 0, (float)base.npc.whoAmI, 0f);
						Main.projectile[num56].netUpdate = true;
					}
					if (this.customAI[0] >= 260f)
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (this.customAI[0] >= 50f && this.customAI[0] <= 120f && Main.rand.Next(2) == 0)
					{
						float Speed4 = 17f;
						Vector2 vector11 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage4 = 45;
						int type4 = ModContent.ProjectileType<LifeThornPro2>();
						float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
						int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0) + (float)Main.rand.Next(-1, 1), type4, damage4, 0f, 0, (float)base.npc.whoAmI, 0f);
						Main.projectile[num57].netUpdate = true;
					}
					if (this.customAI[0] >= 200f)
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
			}
			else if (this.customAI[2] == 5f)
			{
				this.customAI[0] += 1f;
				if (this.customAI[0] == 60f)
				{
					if (NPC.AnyNPCs(base.mod.NPCType("ManaBarrierPro2")))
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
					else
					{
						int dustType = 20;
						int pieCut5 = 16;
						for (int m = 0; m < pieCut5; m++)
						{
							int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 3f);
							Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m / (float)pieCut5 * 6.28f);
							Main.dust[dustID].noLight = false;
							Main.dust[dustID].noGravity = true;
						}
						float distance = 2f;
						float n = 0.4f;
						for (int count = 0; count < 1; count++)
						{
							Vector2 spawn = base.npc.Center + distance * Utils.ToRotationVector2((float)count * n);
							int Minion = NPC.NewNPC((int)spawn.X, (int)spawn.Y, base.mod.NPCType("ManaBarrierPro2"), 0, (float)base.npc.whoAmI, 0f, (float)count, 0f, 255);
							Main.npc[Minion].netUpdate = true;
						}
					}
				}
				if (this.customAI[0] >= 120f)
				{
					this.customAI[0] = 0f;
					this.customAI[2] = 0f;
					this.teleportTimer = 0;
					this.teleport = true;
					base.npc.netUpdate = true;
				}
			}
			else if (this.customAI[2] == 6f)
			{
				this.customAI[0] += 1f;
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
				{
					if (this.customAI[0] == 50f || this.customAI[0] == 55f || this.customAI[0] == 60f || this.customAI[0] == 65f || this.customAI[0] == 70f || this.customAI[0] == 75f || this.customAI[0] == 80f || this.customAI[0] == 85f || this.customAI[0] == 90f || this.customAI[0] == 95f)
					{
						int p23 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, base.mod.ProjectileType("SlashFlashPro"), 45, 3f, 255, 0f, 0f);
						Main.projectile[p23].netUpdate = true;
					}
					if (this.customAI[0] >= 160f)
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (this.customAI[0] == 50f || this.customAI[0] == 70f || this.customAI[0] == 90f || this.customAI[0] == 120f || this.customAI[0] == 160f || this.customAI[0] == 220f)
					{
						int p24 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, base.mod.ProjectileType("SlashFlashPro"), 45, 3f, 255, 0f, 0f);
						Main.projectile[p24].netUpdate = true;
					}
					if (this.customAI[0] >= 270f)
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
				}
			}
			if (this.teleport)
			{
				this.teleportTimer++;
				if (this.teleportTimer >= 2 && this.teleportTimer <= 33)
				{
					this.disappearing = true;
				}
				if (this.teleportTimer == 33)
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
					int playerTargetShift = 30;
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
				if (this.teleportTimer >= 35 && this.teleportTimer <= 71)
				{
					this.appearing = true;
					this.disappearing = false;
				}
				if (this.teleportTimer > 71)
				{
					this.appearing = false;
					this.disappearing = false;
					this.attacking = false;
					this.teleport = false;
					this.teleportTimer = 0;
					this.customAI[1] = 1f;
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
				damage *= 0.1;
			}
			return true;
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
