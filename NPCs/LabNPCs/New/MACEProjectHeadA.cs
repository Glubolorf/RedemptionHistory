using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	[AutoloadBossHead]
	public class MACEProjectHeadA : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("MACE Project");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 120;
			base.npc.height = 140;
			base.npc.friendly = false;
			base.npc.damage = 100;
			base.npc.defense = 70;
			base.npc.lifeMax = 125000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 25, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.noTileCollide = false;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/LabBossMusic");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreHead"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreForeheadGem"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreMetalPart"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreMetalPart"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreMetalScrap"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreMetalScrap"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreMetalScrap"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGorePaintScrap"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGorePaintScrap"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGorePaintScrap"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreWinglet"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/MACEGoreWinglet"), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			RedeWorld.downedMACE = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (!RedeWorld.labAccess6)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel6A"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TheMace"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FloppyDisk6"), 1, false, 0, false, false);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
				writer.Write(this.customAI[1]);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
				this.customAI[1] = reader.ReadFloat();
			}
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 20.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 142;
				if (base.npc.frame.Y > 142)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			base.npc.rotation = base.npc.velocity.X * 0.05f;
			if (Main.netMode != 1)
			{
				RedeWorld.maceUS = false;
			}
			if (!NPC.AnyNPCs(base.mod.NPCType("CraneTrolley")))
			{
				int minion = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 157, base.mod.NPCType("CraneTrolley"), 0, (float)base.npc.whoAmI, 0f, 0f, 0f, 255);
				Main.npc[minion].netUpdate = true;
			}
			if (NPC.AnyNPCs(base.mod.NPCType("MACEProjectJawA")))
			{
				base.npc.immortal = true;
			}
			else
			{
				base.npc.immortal = false;
			}
			Vector2 PosNormal = new Vector2(this.Origin.X + 1200f, this.Origin.Y + 2624f);
			if (base.npc.ai[1] == 0f)
			{
				base.npc.ai[0] += 1f;
				if (base.npc.ai[0] == 1f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				if (base.npc.ai[0] > 120f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[0] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[1] == 1f)
			{
				if (!NPC.AnyNPCs(base.mod.NPCType("MACEProjectJawA")) && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1A")) && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1B")))
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[1] += 1f;
					base.npc.noTileCollide = false;
					base.npc.netUpdate = true;
				}
				else if (NPC.AnyNPCs(base.mod.NPCType("MACEProjectJawA")))
				{
					float[] ai = base.npc.ai;
					int num62 = 2;
					float num63 = ai[num62] + 1f;
					ai[num62] = num63;
					if (num63 >= 180f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 10f;
						Vector2 vector8 = new Vector2(base.npc.position.X + 39f, base.npc.position.Y + 91f);
						Vector2 vector9 = new Vector2(base.npc.position.X + 81f, base.npc.position.Y + 91f);
						int damage = 30;
						int type = base.mod.ProjectileType("MACELaser1");
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
						int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
						Main.projectile[num55].netUpdate = true;
						base.npc.ai[2] = 0f;
					}
				}
				else if (Vector2.Distance(base.npc.Center, PosNormal) < 10f)
				{
					base.npc.velocity *= 0f;
					base.npc.noTileCollide = false;
					base.npc.netUpdate = true;
					float[] ai2 = base.npc.ai;
					int num64 = 2;
					float num63 = ai2[num64] + 1f;
					ai2[num64] = num63;
					if (num63 >= 180f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed2 = 10f;
						Vector2 vector10 = new Vector2(base.npc.position.X + 39f, base.npc.position.Y + 91f);
						Vector2 vector11 = new Vector2(base.npc.position.X + 81f, base.npc.position.Y + 91f);
						int damage2 = 30;
						int type2 = base.mod.ProjectileType("MACELaser1");
						float rotation2 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
						int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
						int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
						Main.projectile[num56].netUpdate = true;
						Main.projectile[num57].netUpdate = true;
						base.npc.ai[2] = 0f;
					}
				}
				else
				{
					this.MoveToVector2(PosNormal);
					base.npc.noTileCollide = true;
				}
			}
			if (base.npc.ai[1] == 2f)
			{
				switch ((int)base.npc.ai[3])
				{
				case 0:
					break;
				case 1:
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 0f || base.npc.ai[2] == 80f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut = 8;
						for (int i = 0; i < pieCut; i++)
						{
							int projID = Projectile.NewProjectile(base.npc.position.X + 39f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)i / (float)pieCut * 6.28f);
							Main.projectile[projID].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 40f || base.npc.ai[2] == 120f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut2 = 8;
						for (int j = 0; j < pieCut2; j++)
						{
							int projID2 = Projectile.NewProjectile(base.npc.position.X + 81f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)j / (float)pieCut2 * 6.28f);
							Main.projectile[projID2].netUpdate = true;
						}
					}
					if (base.npc.ai[2] > 200f)
					{
						this.landed = false;
						base.npc.ai[3] += 1f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						goto IL_1EB1;
					}
					goto IL_1EB1;
				case 2:
					if (base.npc.ai[2] <= 120f)
					{
						base.npc.ai[2] += 1f;
						if (player.Center.X > base.npc.Center.X)
						{
							NPC npc2 = base.npc;
							npc2.velocity.X = npc2.velocity.X + 0.2f;
						}
						else
						{
							NPC npc3 = base.npc;
							npc3.velocity.X = npc3.velocity.X - 0.2f;
						}
					}
					else if (base.npc.ai[2] > 120f && base.npc.ai[2] < 300f)
					{
						base.npc.velocity.X = 0f;
						base.npc.ai[2] = 300f;
					}
					if (base.npc.ai[2] == 300f && !this.landed)
					{
						NPC npc4 = base.npc;
						npc4.velocity.Y = npc4.velocity.Y + 0.2f;
					}
					if (this.landed && base.npc.ai[2] == 300f)
					{
						int pieCut3 = 8;
						for (int k = 0; k < pieCut3; k++)
						{
							int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ElectricZapPro1"), 34, 3f, 255, 0f, 0f);
							Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)k / (float)pieCut3 * 6.28f);
							Main.projectile[projID3].netUpdate = true;
							Main.projectile[projID3].timeLeft = 200;
							Main.projectile[projID3].tileCollide = false;
						}
						int pieCut4 = 16;
						for (int l = 0; l < pieCut4; l++)
						{
							int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ElectricZapPro1"), 34, 3f, 255, 0f, 0f);
							Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)l / (float)pieCut4 * 6.28f);
							Main.projectile[projID4].netUpdate = true;
							Main.projectile[projID4].timeLeft = 200;
							Main.projectile[projID4].tileCollide = false;
						}
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/EarthBoom").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						int p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 70f), new Vector2(10f, 0f), base.mod.ProjectileType("MACEShock1"), 36, 3f, 255, 0f, 0f);
						p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 70f), new Vector2(10f, 0f), base.mod.ProjectileType("MACEShock2"), 36, 3f, 255, 0f, 0f);
						p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 70f), new Vector2(10f, 0f), base.mod.ProjectileType("MACEShock3"), 36, 3f, 255, 0f, 0f);
						p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 70f), new Vector2(-10f, 0f), base.mod.ProjectileType("MACEShock1"), 36, 3f, 255, 0f, 0f);
						p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 70f), new Vector2(-10f, 0f), base.mod.ProjectileType("MACEShock2"), 36, 3f, 255, 0f, 0f);
						p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 70f), new Vector2(-10f, 0f), base.mod.ProjectileType("MACEShock3"), 36, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
						this.landed = false;
						base.npc.ai[2] = 400f;
					}
					if (base.npc.ai[2] < 400f)
					{
						goto IL_1EB1;
					}
					if (Vector2.Distance(base.npc.Center, PosNormal) < 10f)
					{
						base.npc.velocity *= 0f;
						this.crashed = false;
						this.landed = false;
						base.npc.noTileCollide = false;
						base.npc.netUpdate = true;
						base.npc.ai[3] += 1f;
						base.npc.ai[2] = 0f;
						goto IL_1EB1;
					}
					this.MoveToVector2(PosNormal);
					base.npc.noTileCollide = true;
					goto IL_1EB1;
				case 3:
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 20f == 0f && base.npc.ai[2] <= 80f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed3 = 9f;
						Vector2 vector12 = new Vector2(base.npc.position.X + 39f, base.npc.position.Y + 91f);
						int damage3 = 30;
						int type3 = base.mod.ProjectileType("MACELaser1");
						float rotation3 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
						int num58 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
						Main.projectile[num58].netUpdate = true;
					}
					if (base.npc.ai[2] == 10f || base.npc.ai[2] == 30f || base.npc.ai[2] == 50f || base.npc.ai[2] == 70f || base.npc.ai[2] == 90f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed4 = 9f;
						int damage4 = 30;
						Vector2 vector13 = new Vector2(base.npc.position.X + 81f, base.npc.position.Y + 91f);
						int type4 = base.mod.ProjectileType("MACELaser1");
						float rotation4 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
						int num59 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
						Main.projectile[num59].netUpdate = true;
					}
					if (base.npc.ai[2] == 50f)
					{
						for (int m = 0; m < 2; m++)
						{
							int p2 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), (m == 0) ? new Vector2(3f, 0f) : new Vector2(-3f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p2].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 60f)
					{
						for (int n = 0; n < 2; n++)
						{
							int p3 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), (n == 0) ? new Vector2(6f, 0f) : new Vector2(-6f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p3].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 70f)
					{
						for (int i2 = 0; i2 < 2; i2++)
						{
							int p4 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), (i2 == 0) ? new Vector2(9f, 0f) : new Vector2(-9f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p4].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 80f)
					{
						for (int i3 = 0; i3 < 2; i3++)
						{
							int p5 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), (i3 == 0) ? new Vector2(12f, 0f) : new Vector2(-12f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p5].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 90f)
					{
						for (int i4 = 0; i4 < 2; i4++)
						{
							int p6 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), (i4 == 0) ? new Vector2(15f, 0f) : new Vector2(-15f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p6].netUpdate = true;
						}
					}
					if (base.npc.ai[2] > 200f)
					{
						base.npc.ai[3] += 1f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						this.crashed = false;
						goto IL_1EB1;
					}
					goto IL_1EB1;
				case 4:
					if (base.npc.ai[2] == 0f)
					{
						NPC npc5 = base.npc;
						npc5.velocity.X = npc5.velocity.X - 0.1f;
					}
					if (this.crashed && base.npc.ai[2] == 0f)
					{
						Vector2 vel = Vector2.Normalize(base.npc.velocity);
						for (int i5 = 0; i5 < 14; i5++)
						{
							vel = Utils.RotatedBy(vel, 0.4487989505128276, default(Vector2));
							int p7 = Projectile.NewProjectile(base.npc.Left, vel * 5f, base.mod.ProjectileType("ElectricZapPro1"), 34, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p7].timeLeft = 200;
							Main.projectile[p7].netUpdate = true;
							Main.projectile[p7].tileCollide = false;
						}
						for (int i6 = 0; i6 < 20; i6++)
						{
							vel = Utils.RotatedBy(vel, 0.3141592653589793, default(Vector2));
							int p8 = Projectile.NewProjectile(base.npc.Left, vel * 4f, base.mod.ProjectileType("ElectricZapPro1"), 34, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p8].timeLeft = 200;
							Main.projectile[p8].netUpdate = true;
							Main.projectile[p8].tileCollide = false;
						}
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/EarthBoom").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						base.npc.ai[2] = 1f;
					}
					if (base.npc.ai[2] < 1f)
					{
						goto IL_1EB1;
					}
					if (Vector2.Distance(base.npc.Center, PosNormal) < 10f)
					{
						base.npc.velocity *= 0f;
						this.crashed = false;
						base.npc.noTileCollide = false;
						base.npc.netUpdate = true;
						base.npc.ai[3] += 1f;
						base.npc.ai[2] = 0f;
						goto IL_1EB1;
					}
					this.MoveToVector2(PosNormal);
					base.npc.noTileCollide = true;
					goto IL_1EB1;
				case 5:
					base.npc.ai[3] = 0f;
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
					goto IL_1EB1;
				default:
					base.npc.ai[2] = 0f;
					break;
				}
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] % 30f == 0f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed5 = 10f;
					Vector2 vector14 = new Vector2(base.npc.position.X + 39f, base.npc.position.Y + 91f);
					Vector2 vector15 = new Vector2(base.npc.position.X + 81f, base.npc.position.Y + 91f);
					int damage5 = 30;
					int type5 = base.mod.ProjectileType("MACELaser1");
					float rotation5 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					int num60 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
					int num61 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
					Main.projectile[num60].netUpdate = true;
					Main.projectile[num61].netUpdate = true;
				}
				if (base.npc.ai[2] == 30f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed6 = 9f;
					Vector2 vector16 = new Vector2(base.npc.position.X + 50f, base.npc.position.Y + 54f);
					int damage6 = 30;
					int type6 = base.mod.ProjectileType("XenoShard3");
					float rotation6 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
					int p9 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
					Main.projectile[p9].netUpdate = true;
				}
				if (base.npc.ai[2] >= 120f)
				{
					base.npc.ai[3] += 1f;
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			IL_1EB1:
			if (base.npc.collideY && base.npc.velocity.Y > 0f)
			{
				if (!this.landed)
				{
					for (int i7 = 0; i7 < 20; i7++)
					{
						Dust.NewDust(base.npc.BottomLeft, Main.rand.Next(base.npc.width), 1, 31, 0f, 0f, 0, default(Color), 1f);
					}
					this.landed = true;
					base.npc.netUpdate = true;
				}
				base.npc.velocity.X = 0f;
			}
			if (base.npc.collideX && base.npc.velocity.X < 0f)
			{
				if (!this.crashed)
				{
					for (int i8 = 0; i8 < 20; i8++)
					{
						Dust.NewDust(base.npc.TopLeft, 1, Main.rand.Next(base.npc.height), 31, 0f, 0f, 0, default(Color), 1f);
					}
					this.crashed = true;
					base.npc.netUpdate = true;
				}
				base.npc.velocity.Y = 0f;
			}
			if (base.npc.collideX && base.npc.velocity.X > 0f)
			{
				if (!this.crashed)
				{
					for (int i9 = 0; i9 < 20; i9++)
					{
						Dust.NewDust(base.npc.TopRight, 1, Main.rand.Next(base.npc.height), 31, 0f, 0f, 0, default(Color), 1f);
					}
					this.crashed = true;
					base.npc.netUpdate = true;
				}
				base.npc.velocity.Y = 0f;
			}
			if (!NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1A")) && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1B")) && base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && this.customAI[0] == 0f)
			{
				this.customAI[1] += 1f;
				if (this.customAI[1] >= 20f)
				{
					int minion2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist1A"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion2].netUpdate = true;
					int minion3 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist1B"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion3].netUpdate = true;
					base.npc.ai[1] = 1f;
					this.customAI[0] = 1f;
					this.customAI[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = 6f;
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1A")))
			{
				damage *= 0.01;
			}
			else
			{
				damage *= 0.7;
			}
			return true;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			int spriteDirection = base.npc.spriteDirection;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.ai[1] >= 1f;
		}

		public Vector2 Origin = new Vector2((float)((int)((float)Main.maxTilesX * 0.55f)), (float)((int)((float)Main.maxTilesY * 0.65f))) * 16f;

		public float[] customAI = new float[2];

		private bool crashed;

		private bool landed;

		private bool crashed2;
	}
}
