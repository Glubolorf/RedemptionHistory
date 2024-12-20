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
	public class BigNebuleusClone : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus Mirage");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 450000;
			base.npc.defense = 170;
			base.npc.damage = 200;
			base.npc.width = 82;
			base.npc.height = 116;
			base.npc.aiStyle = -1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath59;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.netAlways = true;
			base.npc.noTileCollide = false;
			this.bossBag = base.mod.ItemType("NebBag");
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebuleusTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.55f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				int dustType = 58;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 5f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustType2 = 59;
				int pieCut2 = 10;
				for (int j = 0; j < pieCut2; j++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 5f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)j / (float)pieCut2 * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				int dustType3 = 60;
				int pieCut3 = 12;
				for (int k = 0; k < pieCut3; k++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType3, 0f, 0f, 100, Color.White, 5f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(18f, 0f), (float)k / (float)pieCut3 * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
				int dustType4 = 62;
				int pieCut4 = 14;
				for (int l = 0; l < pieCut4; l++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType4, 0f, 0f, 100, Color.White, 5f);
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
			damage *= 0.7;
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
				npc3.frame.Y = npc3.frame.Y + 186;
				if (base.npc.frame.Y > 558)
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
				if (this.customAI[0] == 300f)
				{
					this.beginFight = true;
					base.npc.netUpdate = true;
				}
			}
			if (this.beginFight)
			{
				if (this.customAI[0] >= 3180f && this.customAI[0] <= 6390f && Main.rand.Next(60) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int A = Main.rand.Next(-200, 200) * 6;
					int B = Main.rand.Next(-200, 200) - 1000;
					int p = Projectile.NewProjectile(player.Center.X + (float)A, player.Center.Y + (float)B, 2f, 6f, base.mod.ProjectileType("StarFallPro2"), 45, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].netUpdate = true;
				}
				if (this.customAI[0] >= 6390f && Main.rand.Next(50) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int A2 = Main.rand.Next(-200, 200) - 1000;
					int B2 = Main.rand.Next(-200, 200) * 4;
					int p2 = Projectile.NewProjectile(player.Center.X + (float)A2, player.Center.Y + (float)B2, 6f, 0f, base.mod.ProjectileType("StarFallPro2"), 45, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p2].netUpdate = true;
				}
				base.npc.dontTakeDamage = false;
				if (this.customAI[0] == 350f)
				{
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 1f)
				{
					switch (Main.rand.Next(9))
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
					}
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[1] == 4f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout5"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p3].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] < 120f)
				{
					if (Main.rand.Next(10) == 0)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("CurvingStar2"), 50, 1f, 255, 0f, 0f);
						Main.projectile[p4].netUpdate = true;
					}
					if (Main.rand.Next(10) == 0)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("CurvingStar2O"), 50, 1f, 255, 0f, 0f);
						Main.projectile[p5].netUpdate = true;
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
			else if (base.npc.ai[1] == 5f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout6"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p6].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 90f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut = 8;
					for (int i = 0; i < pieCut; i++)
					{
						int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula1"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)i / (float)pieCut * 6.28f);
						int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula2"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)i / (float)pieCut * 6.28f);
						int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula3"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)i / (float)pieCut * 6.28f);
						Main.projectile[projID].netUpdate = true;
						Main.projectile[projID2].netUpdate = true;
						Main.projectile[projID3].netUpdate = true;
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
					int p7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout7"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p7].netUpdate = true;
				}
				if (base.npc.ai[3] == 20f)
				{
					int p8 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					int p9 = Projectile.NewProjectile(base.npc.Center.X - 100f, base.npc.Center.Y + 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					int p10 = Projectile.NewProjectile(base.npc.Center.X + 100f, base.npc.Center.Y + 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p8].netUpdate = true;
					Main.projectile[p9].netUpdate = true;
					Main.projectile[p10].netUpdate = true;
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
			else if (base.npc.ai[1] == 7f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p11 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout9"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p11].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 70f || base.npc.ai[3] == 80f || base.npc.ai[3] == 90f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed = 26f;
					Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage = 10;
					int type = base.mod.ProjectileType("CosmosChain1");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
				}
				if (base.npc.ai[3] >= 95f)
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
					int p12 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout10"), 0, 1f, Main.myPlayer, 0f, 0f);
					int p13 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("NebRing"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p12].netUpdate = true;
					Main.projectile[p13].netUpdate = true;
				}
				if (base.npc.ai[3] == 50f)
				{
					int Minion = NPC.NewNPC((int)base.npc.Center.X - 100, (int)base.npc.Center.Y + 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion].netUpdate = true;
				}
				if (base.npc.ai[3] == 53f)
				{
					int Minion2 = NPC.NewNPC((int)base.npc.Center.X - 100, (int)base.npc.Center.Y - 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion2].netUpdate = true;
				}
				if (base.npc.ai[3] == 56f)
				{
					int Minion3 = NPC.NewNPC((int)base.npc.Center.X + 100, (int)base.npc.Center.Y - 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion3].netUpdate = true;
				}
				if (base.npc.ai[3] == 59f)
				{
					int Minion4 = NPC.NewNPC((int)base.npc.Center.X + 100, (int)base.npc.Center.Y + 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion4].netUpdate = true;
				}
				if (base.npc.ai[3] == 61f)
				{
					int Minion5 = NPC.NewNPC((int)base.npc.Center.X + 200, (int)base.npc.Center.Y, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion5].netUpdate = true;
				}
				if (base.npc.ai[3] == 64f)
				{
					int Minion6 = NPC.NewNPC((int)base.npc.Center.X - 200, (int)base.npc.Center.Y, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion6].netUpdate = true;
				}
				if (base.npc.ai[3] == 67f)
				{
					int Minion7 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 200, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion7].netUpdate = true;
				}
				if (base.npc.ai[3] == 70f)
				{
					int Minion8 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y - 200, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion8].netUpdate = true;
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
			else if (base.npc.ai[1] == 1f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int p14 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout1"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p14].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 80f || base.npc.ai[3] == 100f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut2 = 8;
					for (int j = 0; j < pieCut2; j++)
					{
						int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar2"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)j / (float)pieCut2 * 6.28f);
						Main.projectile[projID4].netUpdate = true;
					}
				}
				if (base.npc.ai[3] == 70f || base.npc.ai[3] == 90f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut3 = 8;
					for (int k = 0; k < pieCut3; k++)
					{
						int projID5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar2O"), 50, 3f, 255, 0f, 0f);
						Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)k / (float)pieCut3 * 6.28f);
						Main.projectile[projID5].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 120f)
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
					int p15 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout3"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p15].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 70f || base.npc.ai[3] == 80f || base.npc.ai[3] == 90f || base.npc.ai[3] == 100f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed2 = 15f;
					Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage2 = 50;
					int type2 = base.mod.ProjectileType("PNebula1");
					int type3 = base.mod.ProjectileType("PNebula2");
					int type4 = base.mod.ProjectileType("PNebula3");
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					int num56 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type3, damage2, 0f, 0, 0f, 0f);
					int num57 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type4, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
					Main.projectile[num56].netUpdate = true;
					Main.projectile[num57].netUpdate = true;
				}
				if (base.npc.ai[3] >= 110f)
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
					int p16 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout4"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p16].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f)
				{
					Main.PlaySound(SoundID.Item121, (int)base.npc.position.X, (int)base.npc.position.Y);
					if (Main.rand.Next(2) == 0)
					{
						int num58 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 7f, 7f, base.mod.ProjectileType("StarVortex1"), 40, 0f, 255, 0f, 0f);
						int num59 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 7f, 7f, base.mod.ProjectileType("StarVortex2"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num58].netUpdate = true;
						Main.projectile[num59].netUpdate = true;
						int num60 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, -7f, -7f, base.mod.ProjectileType("StarVortex1"), 40, 0f, 255, 0f, 0f);
						int num61 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, -7f, -7f, base.mod.ProjectileType("StarVortex2"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num60].netUpdate = true;
						Main.projectile[num61].netUpdate = true;
					}
					else
					{
						int num62 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, -7f, 7f, base.mod.ProjectileType("StarVortex1"), 40, 0f, 255, 0f, 0f);
						int num63 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, -7f, 7f, base.mod.ProjectileType("StarVortex2"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num62].netUpdate = true;
						Main.projectile[num63].netUpdate = true;
						int num64 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 7f, -7f, base.mod.ProjectileType("StarVortex1"), 40, 0f, 255, 0f, 0f);
						int num65 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 7f, -7f, base.mod.ProjectileType("StarVortex2"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num64].netUpdate = true;
						Main.projectile[num65].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 120f)
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
					int p17 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout11"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p17].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] <= 90f && Main.rand.Next(8) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int A3 = Main.rand.Next(-200, 200) * 6;
					int B3 = Main.rand.Next(-200, 200) - 1000;
					int p18 = Projectile.NewProjectile(player.Center.X + (float)A3, player.Center.Y + (float)B3, 2f, 6f, base.mod.ProjectileType("CrystalStarPro1"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p18].netUpdate = true;
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
					int num66 = Main.rand.Next(2);
					if (num66 == 0)
					{
						Vector2 newPos = new Vector2((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + newPos;
						base.npc.netUpdate = true;
					}
					if (num66 == 1)
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
				int pieCut4 = 8;
				for (int l = 0; l < pieCut4; l++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)pieCut4 * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustType2 = 59;
				int pieCut5 = 10;
				for (int m = 0; m < pieCut5; m++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m / (float)pieCut5 * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				int dustType3 = 60;
				int pieCut6 = 12;
				for (int n = 0; n < pieCut6; n++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType3, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)n / (float)pieCut6 * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
				int dustType4 = 62;
				int pieCut7 = 14;
				for (int m2 = 0; m2 < pieCut7; m2++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType4, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(16f, 0f), (float)m2 / (float)pieCut7 * 6.28f);
					Main.dust[dustID4].noLight = false;
					Main.dust[dustID4].noGravity = true;
				}
				this.razzleDazzle = false;
				base.npc.netUpdate = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Vector2 Drawpos = base.npc.Center - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY);
			int shader = GameShaders.Armor.GetShaderIdFromItemId(2870);
			Texture2D myGlowTex = base.mod.GetTexture("NPCs/Bosses/Nebuleus/BigNebuleus_Wings");
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

		private bool beginFight;

		private bool teleport;

		private int teleportTimer;

		private bool razzleDazzle;

		public float[] customAI = new float[1];
	}
}
