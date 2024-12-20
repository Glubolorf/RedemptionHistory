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
	public class PZ2Fight : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/LabNPCs/New/PZ2Eyelid";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Patient Zero");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 98;
			base.npc.height = 80;
			base.npc.friendly = false;
			base.npc.damage = 110;
			base.npc.defense = 80;
			base.npc.lifeMax = 340000;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			if (RedeWorld.downedPatientZero)
			{
				base.npc.alpha = 255;
			}
			else
			{
				base.npc.alpha = 0;
			}
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.noTileCollide = false;
			base.npc.netAlways = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.ai[0] != 0f && !NPC.AnyNPCs(base.mod.NPCType("PZ2BodyCover")))
			{
				for (int i = 0; i < 80; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 1f);
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
			}
		}

		public override void AI()
		{
			this.customAI[0] += 1f;
			if (this.customAI[0] > 10f)
			{
				this.bodyFrame++;
				this.customAI[0] = 0f;
			}
			if (this.bodyFrame >= 8)
			{
				this.bodyFrame = 0;
			}
			this.coverCounter++;
			if (this.coverCounter > 20)
			{
				this.coverFrame++;
				this.coverCounter = 0;
			}
			if (this.coverFrame >= 4)
			{
				this.coverFrame = 0;
			}
			if (base.npc.ai[0] >= 1f && Main.netMode == 0)
			{
				if (this.customAI[1] == 0f)
				{
					base.npc.scale += 0.04f;
					if (base.npc.scale > 1.04f)
					{
						this.customAI[1] = 1f;
						base.npc.netUpdate = true;
					}
				}
				else if (this.customAI[1] == 1f)
				{
					base.npc.scale -= 0.04f;
					if (base.npc.scale <= 1f)
					{
						this.customAI[1] = 2f;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					this.customAI[1] += 1f;
					if (this.customAI[1] >= 32f)
					{
						this.customAI[1] = 0f;
					}
				}
			}
			if (this.eyeOpen)
			{
				if (base.npc.frame.Y != 0)
				{
					base.npc.frameCounter += 1.0;
					if (base.npc.frameCounter >= 20.0)
					{
						base.npc.frameCounter = 0.0;
						NPC npc = base.npc;
						npc.frame.Y = npc.frame.Y + 82;
						if (base.npc.frame.Y > 246)
						{
							base.npc.frameCounter = 0.0;
							base.npc.frame.Y = 0;
						}
					}
				}
			}
			else if (base.npc.frame.Y != 164)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 20.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc2 = base.npc;
					npc2.frame.Y = npc2.frame.Y + 82;
					if (base.npc.frame.Y > 246)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			Player player = Main.player[base.npc.target];
			RedeWorld.pzUS = false;
			if (base.npc.ai[0] == 0f && !NPC.AnyNPCs(base.mod.NPCType("PZ2BodyCover")))
			{
				int minion = NPC.NewNPC((int)base.npc.Center.X + 3, (int)base.npc.Center.Y + 149, base.mod.NPCType("PZ2BodyCover"), 0, (float)base.npc.whoAmI, 0f, 0f, 0f, 255);
				Main.npc[minion].netUpdate = true;
			}
			if (base.npc.ai[0] != 0f && !NPC.AnyNPCs(base.mod.NPCType("PZ2BodyCover")))
			{
				base.npc.active = false;
			}
			if (NPC.AnyNPCs(base.mod.NPCType("HiveGrowth")) || base.npc.ai[0] < 2f || base.npc.ai[3] != 0f)
			{
				base.npc.dontTakeDamage = true;
			}
			else
			{
				base.npc.dontTakeDamage = false;
			}
			if (base.npc.ai[3] == 0f && base.npc.ai[0] == 2f && NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int minion2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion2].netUpdate = true;
			}
			if (base.npc.ai[0] == 0f)
			{
				base.npc.frame.Y = 164;
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 1f && Main.netMode != 0 && Main.netMode != 1)
				{
					Vector2 newPos = new Vector2(32f, -254f);
					base.npc.Center = base.npc.position + newPos;
					base.npc.netUpdate = true;
				}
				if (RedeWorld.downedPatientZero)
				{
					base.npc.alpha -= 4;
					if (base.npc.ai[1] == 1f)
					{
						for (int i = 0; i < 80; i++)
						{
							int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 3f);
							Main.dust[dustIndex].velocity *= 5.9f;
						}
					}
				}
				if (base.npc.ai[1] >= 550f)
				{
					base.npc.ai[0] += 1f;
					base.npc.ai[1] = 0f;
					this.eyeOpen = true;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[0] == 1f)
			{
				base.npc.ai[1] += 1f;
				if ((base.npc.life > (int)((float)base.npc.lifeMax * 0.35f)) ? (base.npc.ai[1] % 50f == 0f) : (base.npc.ai[1] % 40f == 0f))
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion3 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion3].netUpdate = true;
				}
				if (base.npc.ai[1] >= 180f)
				{
					base.npc.ai[0] += 1f;
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.75f) && this.customAI[2] == 0f)
			{
				base.npc.ai[0] = 1f;
				base.npc.ai[2] = 0f;
				base.npc.ai[1] = 0f;
				this.customAI[2] += 1f;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.35f) && this.customAI[2] == 1f)
			{
				base.npc.ai[0] = 1f;
				base.npc.ai[2] = 0f;
				base.npc.ai[1] = 0f;
				this.customAI[2] += 1f;
			}
			if (base.npc.ai[3] != 0f)
			{
				this.eyeOpen = false;
			}
			if (base.npc.ai[0] == 2f && base.npc.ai[3] == 0f)
			{
				this.eyeOpen = true;
				if (base.npc.life > (int)((float)base.npc.lifeMax * 0.75f))
				{
					switch ((int)base.npc.ai[2])
					{
					case 0:
						break;
					case 1:
						this.LASERTIMEEVERYBODY = true;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] >= 0f && base.npc.ai[1] < 83f)
						{
							for (int j = 0; j < 2; j++)
							{
								int p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (j == 0) ? new Vector2(10f, 0f) : new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p].netUpdate = true;
							}
							for (int k = 0; k < 2; k++)
							{
								int p2 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (k == 0) ? new Vector2(0f, 10f) : new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p2].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 2f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 87f)
						{
							for (int l = 0; l < 2; l++)
							{
								int p3 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (l == 0) ? new Vector2(10f, 0f) : new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p3].netUpdate = true;
							}
							for (int m = 0; m < 2; m++)
							{
								int p4 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (m == 0) ? new Vector2(0f, 10f) : new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p4].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 260f)
						{
							this.LASERTIMEEVERYBODY = false;
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							goto IL_1588;
						}
						goto IL_1588;
					case 2:
						this.LASERTIMEEVERYBODY = false;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] % 60f == 0f)
						{
							Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed = 8f;
							Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage = 50;
							int type = base.mod.ProjectileType("PatientBlast");
							float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
							int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
							Main.projectile[num54].netUpdate = true;
						}
						if (base.npc.ai[1] > 280f)
						{
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							goto IL_1588;
						}
						goto IL_1588;
					case 3:
						this.LASERTIMEEVERYBODY = true;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] >= 0f && base.npc.ai[1] < 83f)
						{
							for (int n = 0; n < 2; n++)
							{
								int p5 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (n == 0) ? new Vector2(10f, -10f) : new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p5].netUpdate = true;
							}
							for (int i2 = 0; i2 < 2; i2++)
							{
								int p6 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i2 == 0) ? new Vector2(10f, 10f) : new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p6].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 2f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 87f)
						{
							for (int i3 = 0; i3 < 2; i3++)
							{
								int p7 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i3 == 0) ? new Vector2(10f, -10f) : new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p7].netUpdate = true;
							}
							for (int i4 = 0; i4 < 2; i4++)
							{
								int p8 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i4 == 0) ? new Vector2(10f, 10f) : new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p8].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 180f)
						{
							this.LASERTIMEEVERYBODY = false;
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							goto IL_1588;
						}
						goto IL_1588;
					case 4:
						this.LASERTIMEEVERYBODY = false;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] % 80f == 0f)
						{
							Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed2 = 8f;
							Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage2 = 50;
							int type2 = base.mod.ProjectileType("PatientBlast");
							float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
							int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
							Main.projectile[num55].netUpdate = true;
						}
						if (base.npc.ai[1] > 280f)
						{
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							goto IL_1588;
						}
						goto IL_1588;
					case 5:
						base.npc.ai[2] = 1f;
						base.npc.ai[1] = 0f;
						goto IL_1588;
					default:
						base.npc.ai[1] = 0f;
						break;
					}
					this.LASERTIMEEVERYBODY = false;
					float[] ai = base.npc.ai;
					int num64 = 1;
					float num65 = ai[num64] + 1f;
					ai[num64] = num65;
					if (num65 >= 400f)
					{
						base.npc.ai[2] += 1f;
						base.npc.ai[1] = 0f;
						base.npc.netUpdate = true;
					}
				}
				IL_1588:
				if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.75f) && base.npc.life > (int)((float)base.npc.lifeMax * 0.35f))
				{
					switch ((int)base.npc.ai[2])
					{
					case 0:
						break;
					case 1:
						this.LASERTIMEEVERYBODY = true;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] >= 0f && base.npc.ai[1] < 83f)
						{
							int p9 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p9].netUpdate = true;
						}
						if (base.npc.ai[1] == 2f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 87f)
						{
							int p10 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
							Main.projectile[p10].netUpdate = true;
						}
						if (base.npc.ai[1] >= 30f && base.npc.ai[1] < 113f)
						{
							int p11 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, -5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p11].netUpdate = true;
						}
						if (base.npc.ai[1] == 32f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 137f)
						{
							int p12 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, -5f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
							Main.projectile[p12].netUpdate = true;
						}
						if (base.npc.ai[1] >= 90f && base.npc.ai[1] < 173f)
						{
							int p13 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p13].netUpdate = true;
						}
						if (base.npc.ai[1] == 92f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 177f)
						{
							int p14 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 0f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
							Main.projectile[p14].netUpdate = true;
						}
						if (base.npc.ai[1] >= 150f && base.npc.ai[1] < 233f)
						{
							int p15 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, 5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p15].netUpdate = true;
						}
						if (base.npc.ai[1] == 152f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 237f)
						{
							int p16 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, 5f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
							Main.projectile[p16].netUpdate = true;
						}
						if (base.npc.ai[1] >= 210f && base.npc.ai[1] < 293f)
						{
							int p17 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p17].netUpdate = true;
						}
						if (base.npc.ai[1] == 212f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 297f)
						{
							int p18 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
							Main.projectile[p18].netUpdate = true;
						}
						if (base.npc.ai[1] >= 270f && base.npc.ai[1] < 353f)
						{
							int p19 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, 5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p19].netUpdate = true;
						}
						if (base.npc.ai[1] == 272f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 357f)
						{
							int p20 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, 5f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
							Main.projectile[p20].netUpdate = true;
						}
						if (base.npc.ai[1] >= 330f && base.npc.ai[1] < 413f)
						{
							int p21 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p21].netUpdate = true;
						}
						if (base.npc.ai[1] == 332f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 417f)
						{
							int p22 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
							Main.projectile[p22].netUpdate = true;
						}
						if (base.npc.ai[1] >= 390f && base.npc.ai[1] < 473f)
						{
							int p23 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, -5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
							Main.projectile[p23].netUpdate = true;
						}
						if (base.npc.ai[1] == 392f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 477f)
						{
							int p24 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, -5f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
							Main.projectile[p24].netUpdate = true;
						}
						if (base.npc.ai[1] >= 600f)
						{
							this.LASERTIMEEVERYBODY = false;
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							goto IL_2F1B;
						}
						goto IL_2F1B;
					case 2:
						this.LASERTIMEEVERYBODY = false;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] % 100f == 0f)
						{
							Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed3 = 8f;
							Vector2 vector10 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage3 = 50;
							int type3 = base.mod.ProjectileType("PatientBlast");
							float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
							int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
							int num57 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + -1f, (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + -1f, type3, damage3, 0f, 0, 0f, 0f);
							int num58 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + 1f, (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + 1f, type3, damage3, 0f, 0, 0f, 0f);
							Main.projectile[num56].netUpdate = true;
							Main.projectile[num57].netUpdate = true;
							Main.projectile[num58].netUpdate = true;
						}
						if (base.npc.ai[1] > 380f)
						{
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							goto IL_2F1B;
						}
						goto IL_2F1B;
					case 3:
						this.LASERTIMEEVERYBODY = true;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] >= 0f && base.npc.ai[1] < 83f)
						{
							for (int i5 = 0; i5 < 2; i5++)
							{
								int p25 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i5 == 0) ? new Vector2(10f, -10f) : new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p25].netUpdate = true;
							}
							for (int i6 = 0; i6 < 2; i6++)
							{
								int p26 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i6 == 0) ? new Vector2(10f, 10f) : new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p26].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 2f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 87f)
						{
							for (int i7 = 0; i7 < 2; i7++)
							{
								int p27 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i7 == 0) ? new Vector2(10f, -10f) : new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p27].netUpdate = true;
							}
							for (int i8 = 0; i8 < 2; i8++)
							{
								int p28 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i8 == 0) ? new Vector2(10f, 10f) : new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p28].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 50f && base.npc.ai[1] < 133f)
						{
							for (int i9 = 0; i9 < 2; i9++)
							{
								int p29 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i9 == 0) ? new Vector2(0f, -10f) : new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p29].netUpdate = true;
							}
							for (int i10 = 0; i10 < 2; i10++)
							{
								int p30 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i10 == 0) ? new Vector2(10f, 0f) : new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p30].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 52f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 137f)
						{
							for (int i11 = 0; i11 < 2; i11++)
							{
								int p31 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i11 == 0) ? new Vector2(0f, -10f) : new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p31].netUpdate = true;
							}
							for (int i12 = 0; i12 < 2; i12++)
							{
								int p32 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i12 == 0) ? new Vector2(10f, 0f) : new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p32].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 230f)
						{
							this.LASERTIMEEVERYBODY = false;
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							goto IL_2F1B;
						}
						goto IL_2F1B;
					case 4:
						this.LASERTIMEEVERYBODY = false;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] % 60f == 0f)
						{
							Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed4 = 8f;
							Vector2 vector11 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage4 = 50;
							int type4 = base.mod.ProjectileType("PatientBlast");
							float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
							int num59 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
							int num60 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0) + -1f, (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0) + -1f, type4, damage4, 0f, 0, 0f, 0f);
							int num61 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0) + 1f, (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0) + 1f, type4, damage4, 0f, 0, 0f, 0f);
							Main.projectile[num59].netUpdate = true;
							Main.projectile[num60].netUpdate = true;
							Main.projectile[num61].netUpdate = true;
						}
						if (base.npc.ai[1] > 220f)
						{
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							goto IL_2F1B;
						}
						goto IL_2F1B;
					case 5:
						base.npc.ai[2] = 1f;
						base.npc.ai[1] = 0f;
						goto IL_2F1B;
					default:
						base.npc.ai[1] = 0f;
						break;
					}
					this.LASERTIMEEVERYBODY = false;
					float[] ai2 = base.npc.ai;
					int num66 = 1;
					float num65 = ai2[num66] + 1f;
					ai2[num66] = num65;
					if (num65 >= 40f)
					{
						base.npc.ai[2] += 1f;
						base.npc.ai[1] = 0f;
						base.npc.netUpdate = true;
					}
				}
				IL_2F1B:
				if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.35f))
				{
					switch ((int)base.npc.ai[2])
					{
					case 0:
						break;
					case 1:
						this.LASERTIMEEVERYBODY = true;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] >= 0f && base.npc.ai[1] < 83f)
						{
							for (int i13 = 0; i13 < 2; i13++)
							{
								int p33 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i13 == 0) ? new Vector2(0f, -10f) : new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p33].netUpdate = true;
							}
							for (int i14 = 0; i14 < 2; i14++)
							{
								int p34 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i14 == 0) ? new Vector2(10f, 0f) : new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p34].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 2f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 87f)
						{
							for (int i15 = 0; i15 < 2; i15++)
							{
								int p35 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i15 == 0) ? new Vector2(0f, -10f) : new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p35].netUpdate = true;
							}
							for (int i16 = 0; i16 < 2; i16++)
							{
								int p36 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i16 == 0) ? new Vector2(10f, 0f) : new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p36].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 100f && base.npc.ai[1] < 163f)
						{
							int pieCut = 6;
							for (int m2 = 0; m2 < pieCut; m2++)
							{
								int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m2 / (float)pieCut * 6.28f);
								Main.projectile[projID].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 102f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 167f)
						{
							int pieCut2 = 6;
							for (int m3 = 0; m3 < pieCut2; m3++)
							{
								int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser4"), 50, 0f, 255, 0f, 0f);
								Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m3 / (float)pieCut2 * 6.28f);
								Main.projectile[projID2].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 200f && base.npc.ai[1] < 243f)
						{
							int pieCut3 = 8;
							for (int m4 = 0; m4 < pieCut3; m4++)
							{
								int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m4 / (float)pieCut3 * 6.28f);
								Main.projectile[projID3].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 202f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 247f)
						{
							int pieCut4 = 8;
							for (int m5 = 0; m5 < pieCut4; m5++)
							{
								int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser4"), 50, 0f, 255, 0f, 0f);
								Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m5 / (float)pieCut4 * 6.28f);
								Main.projectile[projID4].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 400f)
						{
							this.LASERTIMEEVERYBODY = false;
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						return;
					case 2:
						this.LASERTIMEEVERYBODY = false;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] % 40f == 0f)
						{
							Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed5 = 8f;
							Vector2 vector12 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage5 = 50;
							int type5 = base.mod.ProjectileType("PatientBlast");
							float rotation5 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
							int num62 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
							Main.projectile[num62].netUpdate = true;
						}
						if (base.npc.ai[1] > 230f)
						{
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						return;
					case 3:
						this.LASERTIMEEVERYBODY = true;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] >= 0f && base.npc.ai[1] < 83f)
						{
							for (int i17 = 0; i17 < 2; i17++)
							{
								int p37 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i17 == 0) ? new Vector2(10f, -10f) : new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p37].netUpdate = true;
							}
							for (int i18 = 0; i18 < 2; i18++)
							{
								int p38 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i18 == 0) ? new Vector2(10f, 10f) : new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p38].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 2f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 87f)
						{
							for (int i19 = 0; i19 < 2; i19++)
							{
								int p39 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i19 == 0) ? new Vector2(10f, -10f) : new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p39].netUpdate = true;
							}
							for (int i20 = 0; i20 < 2; i20++)
							{
								int p40 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i20 == 0) ? new Vector2(10f, 10f) : new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p40].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 60f && base.npc.ai[1] < 143f)
						{
							for (int i21 = 0; i21 < 2; i21++)
							{
								int p41 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i21 == 0) ? new Vector2(0f, -10f) : new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p41].netUpdate = true;
							}
							for (int i22 = 0; i22 < 2; i22++)
							{
								int p42 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i22 == 0) ? new Vector2(10f, 0f) : new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p42].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 62f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 147f)
						{
							for (int i23 = 0; i23 < 2; i23++)
							{
								int p43 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i23 == 0) ? new Vector2(0f, -10f) : new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p43].netUpdate = true;
							}
							for (int i24 = 0; i24 < 2; i24++)
							{
								int p44 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i24 == 0) ? new Vector2(10f, 0f) : new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p44].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 120f && base.npc.ai[1] < 203f)
						{
							for (int i25 = 0; i25 < 2; i25++)
							{
								int p45 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i25 == 0) ? new Vector2(10f, -10f) : new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p45].netUpdate = true;
							}
							for (int i26 = 0; i26 < 2; i26++)
							{
								int p46 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i26 == 0) ? new Vector2(10f, 10f) : new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
								Main.projectile[p46].netUpdate = true;
							}
						}
						if (base.npc.ai[1] == 122f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (base.npc.ai[1] == 207f)
						{
							for (int i27 = 0; i27 < 2; i27++)
							{
								int p47 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i27 == 0) ? new Vector2(10f, -10f) : new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p47].netUpdate = true;
							}
							for (int i28 = 0; i28 < 2; i28++)
							{
								int p48 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), (i28 == 0) ? new Vector2(10f, 10f) : new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser3"), 50, 0f, 255, 0f, 0f);
								Main.projectile[p48].netUpdate = true;
							}
						}
						if (base.npc.ai[1] >= 330f)
						{
							this.LASERTIMEEVERYBODY = false;
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						return;
					case 4:
						this.LASERTIMEEVERYBODY = false;
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] % 100f == 0f)
						{
							Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed6 = 8f;
							Vector2 vector13 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage6 = 50;
							int type6 = base.mod.ProjectileType("PatientBlast");
							float rotation6 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
							int num63 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
							Main.projectile[num63].netUpdate = true;
						}
						if (base.npc.ai[1] > 330f)
						{
							base.npc.ai[2] += 1f;
							base.npc.ai[1] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						return;
					case 5:
						base.npc.ai[2] = 1f;
						base.npc.ai[1] = 0f;
						return;
					default:
						base.npc.ai[1] = 0f;
						break;
					}
					this.LASERTIMEEVERYBODY = false;
					float[] ai3 = base.npc.ai;
					int num67 = 1;
					float num65 = ai3[num67] + 1f;
					ai3[num67] = num65;
					if (num65 >= 40f)
					{
						base.npc.ai[2] += 1f;
						base.npc.ai[1] = 0f;
						base.npc.netUpdate = true;
						return;
					}
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool CheckDead()
		{
			base.npc.life = 1;
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.ai[0] >= 1f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D eyeAni = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2Pupil");
			Texture2D eyeGlow = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2Pupil_Glow");
			Texture2D bodyAni = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2");
			Texture2D bodyGlow = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2_Glow");
			Texture2D coverAni = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2BodyCover2");
			Texture2D sludgeAni = base.mod.GetTexture("NPCs/LabNPCs/New/SlimeThings");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenterC = new Vector2(base.npc.Center.X + 15f, base.npc.Center.Y + 7f);
			int num214C = sludgeAni.Height / 1;
			int y6C = 0;
			Main.spriteBatch.Draw(sludgeAni, drawCenterC - Main.screenPosition, new Rectangle?(new Rectangle(0, y6C, sludgeAni.Width, num214C)), drawColor, base.npc.rotation, new Vector2((float)sludgeAni.Width / 2f, (float)num214C / 2f), 1f, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Vector2 drawCenterB = new Vector2(base.npc.Center.X - 2f, base.npc.Center.Y + 14f);
			int num214B = bodyAni.Height / 8;
			int y6B = num214B * this.bodyFrame;
			Main.spriteBatch.Draw(bodyAni, drawCenterB - Main.screenPosition, new Rectangle?(new Rectangle(0, y6B, bodyAni.Width, num214B)), drawColor, base.npc.rotation, new Vector2((float)bodyAni.Width / 2f, (float)num214B / 2f), base.npc.scale * 2f, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Main.spriteBatch.Draw(bodyGlow, drawCenterB - Main.screenPosition, new Rectangle?(new Rectangle(0, y6B, bodyAni.Width, num214B)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)bodyAni.Width / 2f, (float)num214B / 2f), base.npc.scale * 2f, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			if (base.npc.ai[3] == 0f)
			{
				Vector2 drawCenterD = new Vector2(base.npc.Center.X - 2f, base.npc.Center.Y + 18f);
				int num214D = coverAni.Height / 4;
				int y6D = num214D * this.coverFrame;
				Main.spriteBatch.Draw(coverAni, drawCenterD - Main.screenPosition, new Rectangle?(new Rectangle(0, y6D, coverAni.Width, num214D)), drawColor, base.npc.rotation, new Vector2((float)coverAni.Width / 2f, (float)num214D / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (!this.LASERTIMEEVERYBODY)
			{
				Vector2 drawCenterA = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214A = eyeAni.Height / 1;
				int y6A = num214A * this.eyeFrame;
				Main.spriteBatch.Draw(eyeAni, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, y6A, eyeAni.Width, num214A)), drawColor, Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), new Vector2((float)eyeAni.Width / 2f, (float)num214A / 2f), base.npc.scale, SpriteEffects.None, 0f);
				Main.spriteBatch.Draw(eyeGlow, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, y6A, eyeAni.Width, num214A)), base.npc.GetAlpha(Color.White), Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), new Vector2((float)eyeAni.Width / 2f, (float)num214A / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public bool LASERTIMEEVERYBODY;

		public float[] customAI = new float[5];

		private int bodyFrame;

		private bool eyeOpen;

		private int eyeFrame;

		private int coverFrame;

		private int coverCounter;
	}
}
