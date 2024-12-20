using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	public class MACEProjectJaw : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("MACE Project");
		}

		public override void SetDefaults()
		{
			base.npc.width = 68;
			base.npc.height = 80;
			base.npc.friendly = false;
			base.npc.damage = 100;
			base.npc.defense = 50;
			base.npc.lifeMax = 125000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.noTileCollide = false;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/LabBossMusic");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "MACE Project's Jaw";
			potionType = 58;
			if (!Main.dedServ)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
			}
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.beginFight);
				writer.Write(this.firstRoar);
				writer.Write(this.phase2Done);
				writer.Write(this.phase2);
				writer.Write(this.secondRoar);
				writer.Write(this.phase3Done);
				writer.Write(this.phase3);
				writer.Write(this.thirdRoar);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.beginFight = reader.ReadBool();
				this.firstRoar = reader.ReadBool();
				this.phase2Done = reader.ReadBool();
				this.phase2 = reader.ReadBool();
				this.secondRoar = reader.ReadBool();
				this.phase3Done = reader.ReadBool();
				this.phase3 = reader.ReadBool();
				this.thirdRoar = reader.ReadBool();
			}
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			base.npc.ai[0] += 1f;
			if (base.npc.ai[0] <= 120f)
			{
				base.npc.alpha -= 4;
				base.npc.dontTakeDamage = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] > 120f)
			{
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
				base.npc.netUpdate = true;
			}
			if (this.beginFight)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] >= 15f && base.npc.ai[1] < 20f)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 20f && base.npc.ai[1] < 85f)
				{
					base.npc.velocity.Y = 0f;
					for (int i = 0; i < 2; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex].velocity *= 2.9f;
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 20f && !this.firstRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ShockwaveBoom"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].netUpdate = true;
					this.firstRoar = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 35f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int j = 0; j < 20; j++)
					{
						int dustIndex2 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex2].velocity *= 2.9f;
					}
					int pieCut = 5;
					for (int k = 0; k < pieCut; k++)
					{
						int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)k / (float)pieCut * 6.28f);
						Main.projectile[projID].netUpdate = true;
					}
				}
				if (base.npc.ai[1] == 40f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int l = 0; l < 20; l++)
					{
						int dustIndex3 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex3].velocity *= 2.9f;
					}
					int pieCut2 = 10;
					for (int m = 0; m < pieCut2; m++)
					{
						int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)m / (float)pieCut2 * 6.28f);
						Main.projectile[projID2].netUpdate = true;
					}
				}
				if (base.npc.ai[1] == 45f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int n = 0; n < 20; n++)
					{
						int dustIndex4 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex4].velocity *= 2.9f;
					}
					int pieCut3 = 15;
					for (int m2 = 0; m2 < pieCut3; m2++)
					{
						int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)m2 / (float)pieCut3 * 6.28f);
						Main.projectile[projID3].netUpdate = true;
					}
				}
				if (base.npc.ai[1] >= 85f && base.npc.ai[1] < 90f)
				{
					NPC npc2 = base.npc;
					npc2.velocity.Y = npc2.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 90f && base.npc.ai[1] < 235f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 235f && base.npc.ai[1] < 240f)
				{
					NPC npc3 = base.npc;
					npc3.velocity.Y = npc3.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 240f && base.npc.ai[1] < 300f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 250f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[projID4].netUpdate = true;
				}
				if (base.npc.ai[1] == 270f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p2].netUpdate = true;
				}
				if (base.npc.ai[1] == 290f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p3].netUpdate = true;
				}
				if (base.npc.ai[1] >= 300f && base.npc.ai[1] < 305f)
				{
					NPC npc4 = base.npc;
					npc4.velocity.Y = npc4.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 305f && base.npc.ai[1] < 505f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 505f && base.npc.ai[1] < 510f)
				{
					NPC npc5 = base.npc;
					npc5.velocity.Y = npc5.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 510f && base.npc.ai[1] < 560f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 520f)
				{
					for (int i2 = 0; i2 < 20; i2++)
					{
						int dustIndex5 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex5].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed = 8f;
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage = 40;
					int type = base.mod.ProjectileType("MACEMiniblast");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + -1f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + -1f, type, damage, 0f, 0, 0f, 0f);
					int num56 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + 1f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + 1f, type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
					Main.projectile[num55].netUpdate = true;
					Main.projectile[num56].netUpdate = true;
				}
				if (base.npc.ai[1] == 560f)
				{
					for (int i3 = 0; i3 < 20; i3++)
					{
						int dustIndex6 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex6].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed2 = 10f;
					Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage2 = 40;
					int type2 = base.mod.ProjectileType("MACEMiniblast");
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					int num57 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					int num58 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + -1f, (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + -1f, type2, damage2, 0f, 0, 0f, 0f);
					int num59 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + 1f, (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + 1f, type2, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num57].netUpdate = true;
					Main.projectile[num58].netUpdate = true;
					Main.projectile[num59].netUpdate = true;
				}
				if (base.npc.ai[1] >= 580f && base.npc.ai[1] < 585f)
				{
					NPC npc6 = base.npc;
					npc6.velocity.Y = npc6.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 585f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 760f)
				{
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.65f) && !this.phase2Done && base.npc.ai[1] == 0f)
			{
				this.phase2 = true;
				base.npc.netUpdate = true;
			}
			if (this.phase2)
			{
				base.npc.ai[1] = 0f;
				this.beginFight = false;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] >= 15f && base.npc.ai[2] < 20f)
				{
					NPC npc7 = base.npc;
					npc7.velocity.Y = npc7.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 20f && base.npc.ai[2] < 85f)
				{
					base.npc.velocity.Y = 0f;
					for (int i4 = 0; i4 < 2; i4++)
					{
						int dustIndex7 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex7].velocity *= 2.9f;
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 20f && !this.secondRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					int p4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ShockwaveBoom"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p4].netUpdate = true;
					this.secondRoar = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 35f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int i5 = 0; i5 < 20; i5++)
					{
						int dustIndex8 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex8].velocity *= 2.9f;
					}
					int pieCut4 = 5;
					for (int m3 = 0; m3 < pieCut4; m3++)
					{
						int projID5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)m3 / (float)pieCut4 * 6.28f);
						Main.projectile[projID5].netUpdate = true;
					}
				}
				if (base.npc.ai[2] == 40f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int i6 = 0; i6 < 20; i6++)
					{
						int dustIndex9 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex9].velocity *= 2.9f;
					}
					int pieCut5 = 10;
					for (int m4 = 0; m4 < pieCut5; m4++)
					{
						int projID6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)m4 / (float)pieCut5 * 6.28f);
						Main.projectile[projID6].netUpdate = true;
					}
				}
				if (base.npc.ai[2] == 45f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int i7 = 0; i7 < 20; i7++)
					{
						int dustIndex10 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex10].velocity *= 2.9f;
					}
					int pieCut6 = 15;
					for (int m5 = 0; m5 < pieCut6; m5++)
					{
						int projID7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)m5 / (float)pieCut6 * 6.28f);
						Main.projectile[projID7].netUpdate = true;
					}
				}
				if (base.npc.ai[2] >= 85f && base.npc.ai[2] < 90f)
				{
					NPC npc8 = base.npc;
					npc8.velocity.Y = npc8.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 90f && base.npc.ai[2] < 235f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 235f && base.npc.ai[2] < 240f)
				{
					NPC npc9 = base.npc;
					npc9.velocity.Y = npc9.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 240f && base.npc.ai[2] < 300f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 250f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p5].netUpdate = true;
				}
				if (base.npc.ai[2] == 260f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p6].netUpdate = true;
				}
				if (base.npc.ai[2] == 270f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p7].netUpdate = true;
				}
				if (base.npc.ai[2] == 280f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p8 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p8].netUpdate = true;
				}
				if (base.npc.ai[2] == 290f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p9 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p9].netUpdate = true;
				}
				if (base.npc.ai[2] >= 300f && base.npc.ai[2] < 305f)
				{
					NPC npc10 = base.npc;
					npc10.velocity.Y = npc10.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 305f && base.npc.ai[2] < 485f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 485f && base.npc.ai[2] < 490f)
				{
					NPC npc11 = base.npc;
					npc11.velocity.Y = npc11.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 490f && base.npc.ai[2] < 580f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 520f)
				{
					for (int i8 = 0; i8 < 20; i8++)
					{
						int dustIndex11 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex11].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed3 = 8f;
					Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage3 = 40;
					int type3 = base.mod.ProjectileType("MACEMiniblast");
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					int num60 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
					int num61 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + -1f, (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + -1f, type3, damage3, 0f, 0, 0f, 0f);
					int num62 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + 1f, (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + 1f, type3, damage3, 0f, 0, 0f, 0f);
					Main.projectile[num60].netUpdate = true;
					Main.projectile[num61].netUpdate = true;
					Main.projectile[num62].netUpdate = true;
				}
				if (base.npc.ai[2] == 540f)
				{
					for (int i9 = 0; i9 < 20; i9++)
					{
						int dustIndex12 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex12].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed4 = 10f;
					Vector2 vector11 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage4 = 40;
					int type4 = base.mod.ProjectileType("MACEMiniblast");
					float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
					int num63 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
					Main.projectile[num63].netUpdate = true;
				}
				if (base.npc.ai[2] == 560f)
				{
					for (int i10 = 0; i10 < 20; i10++)
					{
						int dustIndex13 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex13].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed5 = 12f;
					Vector2 vector12 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage5 = 40;
					int type5 = base.mod.ProjectileType("MACEMiniblast");
					float rotation5 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					int num64 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
					int num65 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0) + -1f, (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0) + -1f, type5, damage5, 0f, 0, 0f, 0f);
					int num66 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0) + 1f, (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0) + 1f, type5, damage5, 0f, 0, 0f, 0f);
					Main.projectile[num64].netUpdate = true;
					Main.projectile[num65].netUpdate = true;
					Main.projectile[num66].netUpdate = true;
				}
				if (base.npc.ai[2] == 580f)
				{
					for (int i11 = 0; i11 < 20; i11++)
					{
						int dustIndex14 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex14].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed6 = 14f;
					Vector2 vector13 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage6 = 40;
					int type6 = base.mod.ProjectileType("MACEMiniblast");
					float rotation6 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
					int num67 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
					Main.projectile[num67].netUpdate = true;
				}
				if (base.npc.ai[2] >= 580f && base.npc.ai[2] < 585f)
				{
					NPC npc12 = base.npc;
					npc12.velocity.Y = npc12.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 585f && base.npc.ai[2] < 640f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 640f && base.npc.ai[2] < 645f)
				{
					NPC npc13 = base.npc;
					npc13.velocity.Y = npc13.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 645f && base.npc.ai[2] < 680f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 650f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p10 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p10].netUpdate = true;
				}
				if (base.npc.ai[2] == 660f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p11 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p11].netUpdate = true;
				}
				if (base.npc.ai[2] == 670f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p12 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p12].netUpdate = true;
				}
				if (base.npc.ai[2] >= 680f && base.npc.ai[2] < 685f)
				{
					NPC npc14 = base.npc;
					npc14.velocity.Y = npc14.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 685f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 800f)
				{
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.35f) && base.npc.ai[2] == 0f)
			{
				this.phase3 = true;
				this.phase2Done = true;
				base.npc.netUpdate = true;
			}
			if (this.phase3)
			{
				base.npc.ai[1] = 0f;
				base.npc.ai[2] = 0f;
				this.phase2Done = true;
				this.phase2 = false;
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] >= 15f && base.npc.ai[3] < 20f)
				{
					NPC npc15 = base.npc;
					npc15.velocity.Y = npc15.velocity.Y + 0.9f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 20f && base.npc.ai[3] < 395f)
				{
					base.npc.velocity.Y = 0f;
					for (int i12 = 0; i12 < 2; i12++)
					{
						int dustIndex15 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[dustIndex15].velocity *= 2.9f;
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 20f && !this.thirdRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					int p13 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("ShockwaveBoom"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[p13].netUpdate = true;
					this.thirdRoar = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 40f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p14 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p14].netUpdate = true;
				}
				if (base.npc.ai[3] == 120f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p15 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p15].netUpdate = true;
				}
				if (base.npc.ai[3] == 200f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p16 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p16].netUpdate = true;
				}
				if (base.npc.ai[3] == 240f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p17 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p17].netUpdate = true;
				}
				if (base.npc.ai[3] == 280f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p18 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p18].netUpdate = true;
				}
				if (base.npc.ai[3] == 300f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p19 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p19].netUpdate = true;
				}
				if (base.npc.ai[3] == 320f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p20 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p20].netUpdate = true;
				}
				if (base.npc.ai[3] == 330f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p21 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p21].netUpdate = true;
				}
				if (base.npc.ai[3] == 340f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p22 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p22].netUpdate = true;
				}
				if (base.npc.ai[3] == 350f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p23 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p23].netUpdate = true;
				}
				if (base.npc.ai[3] == 360f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int p24 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p24].netUpdate = true;
				}
				if (base.npc.ai[3] == 370f)
				{
					for (int i13 = 0; i13 < 40; i13++)
					{
						int dustIndex16 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex16].velocity *= 3.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed7 = 11f;
					Vector2 vector14 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage7 = 65;
					int type7 = base.mod.ProjectileType("MACEFireProj");
					float rotation7 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					int num68 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)rotation7) * (double)Speed7 * -1.0), (float)(Math.Sin((double)rotation7) * (double)Speed7 * -1.0), type7, damage7, 0f, 0, 0f, 0f);
					Main.projectile[num68].netUpdate = true;
				}
				if (base.npc.ai[3] >= 395f && base.npc.ai[3] < 400f)
				{
					NPC npc16 = base.npc;
					npc16.velocity.Y = npc16.velocity.Y - 0.9f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 400f && base.npc.ai[3] < 600f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 600f && base.npc.ai[3] < 605f)
				{
					NPC npc17 = base.npc;
					npc17.velocity.Y = npc17.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 605f && base.npc.ai[3] < 695f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 630f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p25 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p25].netUpdate = true;
				}
				if (base.npc.ai[3] == 635f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p26 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p26].netUpdate = true;
				}
				if (base.npc.ai[3] == 640f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p27 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p27].netUpdate = true;
				}
				if (base.npc.ai[3] == 645f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p28 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p28].netUpdate = true;
				}
				if (base.npc.ai[3] == 650f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p29 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p29].netUpdate = true;
				}
				if (base.npc.ai[3] == 655f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p30 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p30].netUpdate = true;
				}
				if (base.npc.ai[3] == 660f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p31 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p31].netUpdate = true;
				}
				if (base.npc.ai[3] == 665f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p32 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p32].netUpdate = true;
				}
				if (base.npc.ai[3] == 670f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p33 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p33].netUpdate = true;
				}
				if (base.npc.ai[3] == 675f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p34 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p34].netUpdate = true;
				}
				if (base.npc.ai[3] >= 695f && base.npc.ai[3] < 700f)
				{
					NPC npc18 = base.npc;
					npc18.velocity.Y = npc18.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 700f && base.npc.ai[3] < 1200f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 1200f && base.npc.ai[3] < 1205f)
				{
					NPC npc19 = base.npc;
					npc19.velocity.Y = npc19.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 1205f && base.npc.ai[3] < 1295f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 1245f)
				{
					for (int i14 = 0; i14 < 20; i14++)
					{
						int dustIndex17 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex17].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed8 = 6f;
					Vector2 vector15 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage8 = 40;
					int type8 = base.mod.ProjectileType("MACEMiniblast");
					float rotation8 = (float)Math.Atan2((double)(vector15.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector15.X - (player.position.X + (float)player.width * 0.5f)));
					int num69 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)rotation8) * (double)Speed8 * -1.0), (float)(Math.Sin((double)rotation8) * (double)Speed8 * -1.0), type8, damage8, 0f, 0, 0f, 0f);
					int num70 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)rotation8) * (double)Speed8 * -1.0) + -1f, (float)(Math.Sin((double)rotation8) * (double)Speed8 * -1.0) + -1f, type8, damage8, 0f, 0, 0f, 0f);
					int num71 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)rotation8) * (double)Speed8 * -1.0) + 1f, (float)(Math.Sin((double)rotation8) * (double)Speed8 * -1.0) + 1f, type8, damage8, 0f, 0, 0f, 0f);
					Main.projectile[num69].netUpdate = true;
					Main.projectile[num70].netUpdate = true;
					Main.projectile[num71].netUpdate = true;
				}
				if (base.npc.ai[3] == 1255f)
				{
					for (int i15 = 0; i15 < 20; i15++)
					{
						int dustIndex18 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex18].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed9 = 7f;
					Vector2 vector16 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage9 = 40;
					int type9 = base.mod.ProjectileType("MACEMiniblast");
					float rotation9 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
					int num72 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)rotation9) * (double)Speed9 * -1.0), (float)(Math.Sin((double)rotation9) * (double)Speed9 * -1.0), type9, damage9, 0f, 0, 0f, 0f);
					Main.projectile[num72].netUpdate = true;
				}
				if (base.npc.ai[3] == 1265f)
				{
					for (int i16 = 0; i16 < 20; i16++)
					{
						int dustIndex19 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex19].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed10 = 8f;
					Vector2 vector17 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage10 = 40;
					int type10 = base.mod.ProjectileType("MACEMiniblast");
					float rotation10 = (float)Math.Atan2((double)(vector17.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector17.X - (player.position.X + (float)player.width * 0.5f)));
					int num73 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)rotation10) * (double)Speed10 * -1.0), (float)(Math.Sin((double)rotation10) * (double)Speed10 * -1.0), type10, damage10, 0f, 0, 0f, 0f);
					int num74 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)rotation10) * (double)Speed10 * -1.0) + -1f, (float)(Math.Sin((double)rotation10) * (double)Speed10 * -1.0) + -1f, type10, damage10, 0f, 0, 0f, 0f);
					int num75 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)rotation10) * (double)Speed10 * -1.0) + 1f, (float)(Math.Sin((double)rotation10) * (double)Speed10 * -1.0) + 1f, type10, damage10, 0f, 0, 0f, 0f);
					Main.projectile[num73].netUpdate = true;
					Main.projectile[num74].netUpdate = true;
					Main.projectile[num75].netUpdate = true;
				}
				if (base.npc.ai[3] == 1275f)
				{
					for (int i17 = 0; i17 < 20; i17++)
					{
						int dustIndex20 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex20].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed11 = 9f;
					Vector2 vector18 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage11 = 40;
					int type11 = base.mod.ProjectileType("MACEMiniblast");
					float rotation11 = (float)Math.Atan2((double)(vector18.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector18.X - (player.position.X + (float)player.width * 0.5f)));
					int num76 = Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)rotation11) * (double)Speed11 * -1.0), (float)(Math.Sin((double)rotation11) * (double)Speed11 * -1.0), type11, damage11, 0f, 0, 0f, 0f);
					Main.projectile[num76].netUpdate = true;
				}
				if (base.npc.ai[3] == 1285f)
				{
					for (int i18 = 0; i18 < 20; i18++)
					{
						int dustIndex21 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex21].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed12 = 10f;
					Vector2 vector19 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage12 = 40;
					int type12 = base.mod.ProjectileType("MACEMiniblast");
					float rotation12 = (float)Math.Atan2((double)(vector19.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector19.X - (player.position.X + (float)player.width * 0.5f)));
					int num77 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)rotation12) * (double)Speed12 * -1.0), (float)(Math.Sin((double)rotation12) * (double)Speed12 * -1.0), type12, damage12, 0f, 0, 0f, 0f);
					int num78 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)rotation12) * (double)Speed12 * -1.0) + -1f, (float)(Math.Sin((double)rotation12) * (double)Speed12 * -1.0) + -1f, type12, damage12, 0f, 0, 0f, 0f);
					int num79 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)rotation12) * (double)Speed12 * -1.0) + 1f, (float)(Math.Sin((double)rotation12) * (double)Speed12 * -1.0) + 1f, type12, damage12, 0f, 0, 0f, 0f);
					Main.projectile[num77].netUpdate = true;
					Main.projectile[num78].netUpdate = true;
					Main.projectile[num79].netUpdate = true;
				}
				if (base.npc.ai[3] == 1295f)
				{
					for (int i19 = 0; i19 < 20; i19++)
					{
						int dustIndex22 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex22].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed13 = 11f;
					Vector2 vector20 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage13 = 40;
					int type13 = base.mod.ProjectileType("MACEMiniblast");
					float rotation13 = (float)Math.Atan2((double)(vector20.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector20.X - (player.position.X + (float)player.width * 0.5f)));
					int num80 = Projectile.NewProjectile(vector20.X, vector20.Y, (float)(Math.Cos((double)rotation13) * (double)Speed13 * -1.0), (float)(Math.Sin((double)rotation13) * (double)Speed13 * -1.0), type13, damage13, 0f, 0, 0f, 0f);
					Main.projectile[num80].netUpdate = true;
				}
				if (base.npc.ai[3] == 1305f)
				{
					for (int i20 = 0; i20 < 20; i20++)
					{
						int dustIndex23 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex23].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed14 = 12f;
					Vector2 vector21 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage14 = 40;
					int type14 = base.mod.ProjectileType("MACEMiniblast");
					float rotation14 = (float)Math.Atan2((double)(vector21.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector21.X - (player.position.X + (float)player.width * 0.5f)));
					int num81 = Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)rotation14) * (double)Speed14 * -1.0), (float)(Math.Sin((double)rotation14) * (double)Speed14 * -1.0), type14, damage14, 0f, 0, 0f, 0f);
					int num82 = Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)rotation14) * (double)Speed14 * -1.0) + -1f, (float)(Math.Sin((double)rotation14) * (double)Speed14 * -1.0) + -1f, type14, damage14, 0f, 0, 0f, 0f);
					int num83 = Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)rotation14) * (double)Speed14 * -1.0) + 1f, (float)(Math.Sin((double)rotation14) * (double)Speed14 * -1.0) + 1f, type14, damage14, 0f, 0, 0f, 0f);
					Main.projectile[num81].netUpdate = true;
					Main.projectile[num82].netUpdate = true;
					Main.projectile[num83].netUpdate = true;
				}
				if (base.npc.ai[3] >= 1395f && base.npc.ai[3] < 1400f)
				{
					NPC npc20 = base.npc;
					npc20.velocity.Y = npc20.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 1400f && base.npc.ai[3] < 1405f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 1505f && base.npc.ai[3] < 1510f)
				{
					NPC npc21 = base.npc;
					npc21.velocity.Y = npc21.velocity.Y + 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 1510f && base.npc.ai[3] < 1600f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 1535f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p35 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p35].netUpdate = true;
				}
				if (base.npc.ai[3] == 1537f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p36 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p36].netUpdate = true;
				}
				if (base.npc.ai[3] == 1539f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p37 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p37].netUpdate = true;
				}
				if (base.npc.ai[3] == 1541f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int p38 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p38].netUpdate = true;
				}
				if (base.npc.ai[3] >= 1600f && base.npc.ai[3] < 1605f)
				{
					NPC npc22 = base.npc;
					npc22.velocity.Y = npc22.velocity.Y - 0.7f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 1605f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 1800f)
				{
					base.npc.ai[3] = 0f;
					base.npc.netUpdate = true;
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.beginFight;
		}

		private bool beginFight;

		private bool firstRoar;

		private bool phase2Done;

		private bool phase2;

		private bool secondRoar;

		private bool phase3Done;

		private bool phase3;

		private bool thirdRoar;
	}
}
