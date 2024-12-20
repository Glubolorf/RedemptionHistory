using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
			base.npc.defense = 12;
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
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossForest1");
			base.npc.noTileCollide = false;
			this.bossBag = base.mod.ItemType("ThornBag");
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
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedGrassSword"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThornBow"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("RootTendril"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ThornSeedBag"), 1, false, 0, false, false);
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
				CombatText.NewText(this.player.getRect(), Color.Gold, "+1", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						for (int j = 0; j < player.inventory.Length; j++)
						{
							if (player.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Nice work, the forest is safe now.", Color.DarkGoldenrod, false);
							}
						}
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
					if (this.customAI[0] == 60f)
					{
						int num = Projectile.NewProjectile(player.Center.X + 500f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num2 = Projectile.NewProjectile(player.Center.X + -500f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num].netUpdate = true;
						Main.projectile[num2].netUpdate = true;
					}
					if (this.customAI[0] == 80f)
					{
						int num3 = Projectile.NewProjectile(player.Center.X + 400f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num4 = Projectile.NewProjectile(player.Center.X + -400f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num3].netUpdate = true;
						Main.projectile[num4].netUpdate = true;
					}
					if (this.customAI[0] == 100f)
					{
						int num5 = Projectile.NewProjectile(player.Center.X + 300f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num6 = Projectile.NewProjectile(player.Center.X + -300f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num5].netUpdate = true;
						Main.projectile[num6].netUpdate = true;
					}
					if (this.customAI[0] == 120f)
					{
						int num7 = Projectile.NewProjectile(player.Center.X + 200f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num8 = Projectile.NewProjectile(player.Center.X + -200f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num7].netUpdate = true;
						Main.projectile[num8].netUpdate = true;
					}
					if (this.customAI[0] == 140f)
					{
						int num9 = Projectile.NewProjectile(player.Center.X + 100f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num10 = Projectile.NewProjectile(player.Center.X + -100f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num9].netUpdate = true;
						Main.projectile[num10].netUpdate = true;
					}
					if (this.customAI[0] == 160f)
					{
						int num11 = Projectile.NewProjectile(player.Center.X + 0f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num11].netUpdate = true;
					}
					if (this.customAI[0] >= 240f)
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
					if (this.customAI[0] == 60f)
					{
						int num12 = Projectile.NewProjectile(player.Center.X + 500f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num13 = Projectile.NewProjectile(player.Center.X + -500f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num12].netUpdate = true;
						Main.projectile[num13].netUpdate = true;
					}
					if (this.customAI[0] == 90f)
					{
						int num14 = Projectile.NewProjectile(player.Center.X + 400f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num15 = Projectile.NewProjectile(player.Center.X + -400f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num14].netUpdate = true;
						Main.projectile[num15].netUpdate = true;
					}
					if (this.customAI[0] == 120f)
					{
						int num16 = Projectile.NewProjectile(player.Center.X + 300f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num17 = Projectile.NewProjectile(player.Center.X + -300f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num16].netUpdate = true;
						Main.projectile[num17].netUpdate = true;
					}
					if (this.customAI[0] == 150f)
					{
						int num18 = Projectile.NewProjectile(player.Center.X + 200f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num19 = Projectile.NewProjectile(player.Center.X + -200f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num18].netUpdate = true;
						Main.projectile[num19].netUpdate = true;
					}
					if (this.customAI[0] == 180f)
					{
						int num20 = Projectile.NewProjectile(player.Center.X + 100f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						int num21 = Projectile.NewProjectile(player.Center.X + -100f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num20].netUpdate = true;
						Main.projectile[num21].netUpdate = true;
					}
					if (this.customAI[0] == 210f)
					{
						int num22 = Projectile.NewProjectile(player.Center.X + 0f, player.Center.Y - 200f, 0f, 0f, base.mod.ProjectileType("ThornSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
						Main.projectile[num22].netUpdate = true;
					}
					if (this.customAI[0] >= 320f)
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
						float num23 = 19f;
						Vector2 vector;
						vector..ctor(base.npc.Center.X, base.npc.Center.Y);
						int num24 = 12;
						int num25 = base.mod.ProjectileType("CursedThornPro3");
						float num26 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
						int num27 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num26) * (double)num23 * -1.0), (float)(Math.Sin((double)num26) * (double)num23 * -1.0), num25, num24, 0f, 0, 0f, 0f);
						Main.projectile[num27].netUpdate = true;
					}
				}
				else if (this.customAI[0] == 60f || this.customAI[0] == 90f || this.customAI[0] == 120f)
				{
					float num28 = 16f;
					Vector2 vector2;
					vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num29 = 10;
					int num30 = base.mod.ProjectileType("CursedThornPro3");
					float num31 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num32 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num31) * (double)num28 * -1.0), (float)(Math.Sin((double)num31) * (double)num28 * -1.0), num30, num29, 0f, 0, 0f, 0f);
					Main.projectile[num32].netUpdate = true;
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
					if (this.customAI[0] == 60f || this.customAI[0] == 120f)
					{
						int num33 = 4;
						for (int i = 0; i < num33; i++)
						{
							int num34 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
							Main.projectile[num34].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)num33 * 6.28f);
							Main.projectile[num34].netUpdate = true;
						}
					}
					if (this.customAI[0] == 90f || this.customAI[0] == 150f)
					{
						int num35 = 8;
						for (int j = 0; j < num35; j++)
						{
							int num36 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
							Main.projectile[num36].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)num35 * 6.28f);
							Main.projectile[num36].netUpdate = true;
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
					if (this.customAI[0] == 60f)
					{
						int num37 = 4;
						for (int k = 0; k < num37; k++)
						{
							int num38 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
							Main.projectile[num38].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)num37 * 6.28f);
							Main.projectile[num38].netUpdate = true;
						}
					}
					if (this.customAI[0] == 100f)
					{
						int num39 = 8;
						for (int l = 0; l < num39; l++)
						{
							int num40 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 55, 10, 3f, 255, 0f, 0f);
							Main.projectile[num40].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)num39 * 6.28f);
							Main.projectile[num40].netUpdate = true;
						}
					}
					if (this.customAI[0] >= 180f)
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
					if (this.customAI[0] >= 60f && this.customAI[0] <= 140f && Main.rand.Next(2) == 0)
					{
						float num41 = 12f;
						Vector2 vector3;
						vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int num42 = 10;
						int num43 = base.mod.ProjectileType<LifeThornPro>();
						float num44 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
						int num45 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num44) * (double)num41 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num44) * (double)num41 * -1.0) + (float)Main.rand.Next(-1, 1), num43, num42, 0f, 0, (float)base.npc.whoAmI, 0f);
						Main.projectile[num45].netUpdate = true;
					}
					if (this.customAI[0] >= 240f)
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
					if (this.customAI[0] >= 60f && this.customAI[0] <= 90f && Main.rand.Next(2) == 0)
					{
						float num46 = 11f;
						Vector2 vector4;
						vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int num47 = 10;
						int num48 = base.mod.ProjectileType<LifeThornPro>();
						float num49 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
						int num50 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num49) * (double)num46 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num49) * (double)num46 * -1.0) + (float)Main.rand.Next(-1, 1), num48, num47, 0f, 0, (float)base.npc.whoAmI, 0f);
						Main.projectile[num50].netUpdate = true;
					}
					if (this.customAI[0] >= 180f)
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
					if (NPC.AnyNPCs(base.mod.NPCType("ManaBarrierPro")))
					{
						this.customAI[0] = 0f;
						this.customAI[2] = 0f;
						this.teleportTimer = 0;
						this.teleport = true;
						base.npc.netUpdate = true;
					}
					else
					{
						int num51 = 20;
						int num52 = 16;
						for (int m = 0; m < num52; m++)
						{
							int num53 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num51, 0f, 0f, 100, Color.White, 3f);
							Main.dust[num53].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m / (float)num52 * 6.28f);
							Main.dust[num53].noLight = false;
							Main.dust[num53].noGravity = true;
						}
						float num54 = 2f;
						float num55 = 0.4f;
						for (int n = 0; n < 1; n++)
						{
							Vector2 vector5 = base.npc.Center + num54 * Utils.ToRotationVector2((float)n * num55);
							int num56 = NPC.NewNPC((int)vector5.X, (int)vector5.Y, base.mod.NPCType("ManaBarrierPro"), 0, (float)base.npc.whoAmI, 0f, (float)n, 0f, 255);
							Main.npc[num56].netUpdate = true;
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
					if (this.customAI[0] == 60f || this.customAI[0] == 90f || this.customAI[0] == 120f || this.customAI[0] == 150f || this.customAI[0] == 180f)
					{
						int num57 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, base.mod.ProjectileType("SlashFlashPro"), 10, 3f, 255, 0f, 0f);
						Main.projectile[num57].netUpdate = true;
					}
					if (this.customAI[0] >= 230f)
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
					if (this.customAI[0] == 50f || this.customAI[0] == 100f || this.customAI[0] == 150f || this.customAI[0] == 200f)
					{
						int num58 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, base.mod.ProjectileType("SlashFlashPro"), 10, 3f, 255, 0f, 0f);
						Main.projectile[num58].netUpdate = true;
					}
					if (this.customAI[0] >= 250f)
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
					int num59 = (int)Main.player[base.npc.target].position.X / 16;
					int num60 = (int)Main.player[base.npc.target].position.Y / 16;
					int num61 = (int)base.npc.position.X / 16;
					int num62 = (int)base.npc.position.Y / 16;
					int num63 = 30;
					int num64 = 0;
					for (int num65 = 0; num65 < 100; num65++)
					{
						num64++;
						int num66 = Main.rand.Next(num59 - num63, num59 + num63);
						int num67 = Main.rand.Next(num60 - num63, num60 + num63);
						for (int num68 = num67; num68 < num60 + num63; num68++)
						{
							if ((num66 < num59 - 12 || num66 > num59 + 12) && (num68 < num62 - 1 || num68 > num62 + 1 || num66 < num61 - 1 || num66 > num61 + 1) && Main.tile[num66, num68].nactive())
							{
								bool flag = true;
								if (Main.tile[num66, num68 - 1].lava())
								{
									flag = false;
								}
								if (flag && Main.tileSolid[(int)Main.tile[num66, num68].type] && !Collision.SolidTiles(num66 - 1, num66 + 1, num68 - 4, num68 - 1))
								{
									base.npc.ai[1] = 20f;
									base.npc.ai[2] = (float)num66;
									base.npc.ai[3] = (float)num68 - 1f;
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
				damage *= 0.4;
			}
			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/Thorn/ThornAttack1");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/Thorn/ThornAppear");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/Thorn/ThornDisappear");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.attacking && !this.appearing && !this.disappearing)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.attacking && !this.appearing && !this.disappearing)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 4;
				int num2 = num * this.attackFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.appearing && !this.disappearing)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 12;
				int num4 = num3 * this.appearFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (!this.appearing && this.disappearing)
			{
				Vector2 vector3;
				vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num5 = texture3.Height / 11;
				int num6 = num5 * this.disappearFrame;
				Main.spriteBatch.Draw(texture3, vector3 - Main.screenPosition, new Rectangle?(new Rectangle(0, num6, texture3.Width, num5)), drawColor, base.npc.rotation, new Vector2((float)texture3.Width / 2f, (float)num5 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
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
