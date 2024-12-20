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
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[num].velocity *= 1.9f;
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
						int num = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num].velocity *= 2.9f;
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 20f && !this.firstRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					this.firstRoar = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 35f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int j = 0; j < 20; j++)
					{
						int num2 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num2].velocity *= 2.9f;
					}
					int num3 = 5;
					for (int k = 0; k < num3; k++)
					{
						int num4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)k / (float)num3 * 6.28f);
						Main.projectile[num4].netUpdate = true;
					}
				}
				if (base.npc.ai[1] == 40f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int l = 0; l < 20; l++)
					{
						int num5 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num5].velocity *= 2.9f;
					}
					int num6 = 10;
					for (int m = 0; m < num6; m++)
					{
						int num7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)m / (float)num6 * 6.28f);
						Main.projectile[num7].netUpdate = true;
					}
				}
				if (base.npc.ai[1] == 45f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int n = 0; n < 20; n++)
					{
						int num8 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num8].velocity *= 2.9f;
					}
					int num9 = 15;
					for (int num10 = 0; num10 < num9; num10++)
					{
						int num11 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num11].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)num10 / (float)num9 * 6.28f);
						Main.projectile[num11].netUpdate = true;
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
					int num12 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num12].netUpdate = true;
				}
				if (base.npc.ai[1] == 270f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num13 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num13].netUpdate = true;
				}
				if (base.npc.ai[1] == 290f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num14 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num14].netUpdate = true;
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
					for (int num15 = 0; num15 < 20; num15++)
					{
						int num16 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num16].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num17 = 8f;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num18 = 40;
					int num19 = base.mod.ProjectileType("MACEMiniblast");
					float num20 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num21 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num20) * (double)num17 * -1.0), (float)(Math.Sin((double)num20) * (double)num17 * -1.0), num19, num18, 0f, 0, 0f, 0f);
					int num22 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num20) * (double)num17 * -1.0) + -1f, (float)(Math.Sin((double)num20) * (double)num17 * -1.0) + -1f, num19, num18, 0f, 0, 0f, 0f);
					int num23 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num20) * (double)num17 * -1.0) + 1f, (float)(Math.Sin((double)num20) * (double)num17 * -1.0) + 1f, num19, num18, 0f, 0, 0f, 0f);
					Main.projectile[num21].netUpdate = true;
					Main.projectile[num22].netUpdate = true;
					Main.projectile[num23].netUpdate = true;
				}
				if (base.npc.ai[1] == 560f)
				{
					for (int num24 = 0; num24 < 20; num24++)
					{
						int num25 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num25].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num26 = 10f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num27 = 40;
					int num28 = base.mod.ProjectileType("MACEMiniblast");
					float num29 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num30 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0), (float)(Math.Sin((double)num29) * (double)num26 * -1.0), num28, num27, 0f, 0, 0f, 0f);
					int num31 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0) + -1f, (float)(Math.Sin((double)num29) * (double)num26 * -1.0) + -1f, num28, num27, 0f, 0, 0f, 0f);
					int num32 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0) + 1f, (float)(Math.Sin((double)num29) * (double)num26 * -1.0) + 1f, num28, num27, 0f, 0, 0f, 0f);
					Main.projectile[num30].netUpdate = true;
					Main.projectile[num31].netUpdate = true;
					Main.projectile[num32].netUpdate = true;
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
					for (int num33 = 0; num33 < 2; num33++)
					{
						int num34 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num34].velocity *= 2.9f;
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 20f && !this.secondRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					this.secondRoar = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 35f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int num35 = 0; num35 < 20; num35++)
					{
						int num36 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num36].velocity *= 2.9f;
					}
					int num37 = 5;
					for (int num38 = 0; num38 < num37; num38++)
					{
						int num39 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num39].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num38 / (float)num37 * 6.28f);
						Main.projectile[num39].netUpdate = true;
					}
				}
				if (base.npc.ai[2] == 40f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int num40 = 0; num40 < 20; num40++)
					{
						int num41 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num41].velocity *= 2.9f;
					}
					int num42 = 10;
					for (int num43 = 0; num43 < num42; num43++)
					{
						int num44 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num44].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)num43 / (float)num42 * 6.28f);
						Main.projectile[num44].netUpdate = true;
					}
				}
				if (base.npc.ai[2] == 45f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int num45 = 0; num45 < 20; num45++)
					{
						int num46 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num46].velocity *= 2.9f;
					}
					int num47 = 15;
					for (int num48 = 0; num48 < num47; num48++)
					{
						int num49 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num49].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)num48 / (float)num47 * 6.28f);
						Main.projectile[num49].netUpdate = true;
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
					int num50 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num50].netUpdate = true;
				}
				if (base.npc.ai[2] == 260f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num51 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num51].netUpdate = true;
				}
				if (base.npc.ai[2] == 270f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num52 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num52].netUpdate = true;
				}
				if (base.npc.ai[2] == 280f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num53 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num53].netUpdate = true;
				}
				if (base.npc.ai[2] == 290f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num54 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
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
					for (int num55 = 0; num55 < 20; num55++)
					{
						int num56 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num56].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num57 = 8f;
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num58 = 40;
					int num59 = base.mod.ProjectileType("MACEMiniblast");
					float num60 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					int num61 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num60) * (double)num57 * -1.0), (float)(Math.Sin((double)num60) * (double)num57 * -1.0), num59, num58, 0f, 0, 0f, 0f);
					int num62 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num60) * (double)num57 * -1.0) + -1f, (float)(Math.Sin((double)num60) * (double)num57 * -1.0) + -1f, num59, num58, 0f, 0, 0f, 0f);
					int num63 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num60) * (double)num57 * -1.0) + 1f, (float)(Math.Sin((double)num60) * (double)num57 * -1.0) + 1f, num59, num58, 0f, 0, 0f, 0f);
					Main.projectile[num61].netUpdate = true;
					Main.projectile[num62].netUpdate = true;
					Main.projectile[num63].netUpdate = true;
				}
				if (base.npc.ai[2] == 540f)
				{
					for (int num64 = 0; num64 < 20; num64++)
					{
						int num65 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num65].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num66 = 10f;
					Vector2 vector4;
					vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num67 = 40;
					int num68 = base.mod.ProjectileType("MACEMiniblast");
					float num69 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					int num70 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num69) * (double)num66 * -1.0), (float)(Math.Sin((double)num69) * (double)num66 * -1.0), num68, num67, 0f, 0, 0f, 0f);
					Main.projectile[num70].netUpdate = true;
				}
				if (base.npc.ai[2] == 560f)
				{
					for (int num71 = 0; num71 < 20; num71++)
					{
						int num72 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num72].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num73 = 12f;
					Vector2 vector5;
					vector5..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num74 = 40;
					int num75 = base.mod.ProjectileType("MACEMiniblast");
					float num76 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
					int num77 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num76) * (double)num73 * -1.0), (float)(Math.Sin((double)num76) * (double)num73 * -1.0), num75, num74, 0f, 0, 0f, 0f);
					int num78 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num76) * (double)num73 * -1.0) + -1f, (float)(Math.Sin((double)num76) * (double)num73 * -1.0) + -1f, num75, num74, 0f, 0, 0f, 0f);
					int num79 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num76) * (double)num73 * -1.0) + 1f, (float)(Math.Sin((double)num76) * (double)num73 * -1.0) + 1f, num75, num74, 0f, 0, 0f, 0f);
					Main.projectile[num77].netUpdate = true;
					Main.projectile[num78].netUpdate = true;
					Main.projectile[num79].netUpdate = true;
				}
				if (base.npc.ai[2] == 580f)
				{
					for (int num80 = 0; num80 < 20; num80++)
					{
						int num81 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num81].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num82 = 14f;
					Vector2 vector6;
					vector6..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num83 = 40;
					int num84 = base.mod.ProjectileType("MACEMiniblast");
					float num85 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
					int num86 = Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num85) * (double)num82 * -1.0), (float)(Math.Sin((double)num85) * (double)num82 * -1.0), num84, num83, 0f, 0, 0f, 0f);
					Main.projectile[num86].netUpdate = true;
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
					int num87 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num87].netUpdate = true;
				}
				if (base.npc.ai[2] == 660f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num88 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num88].netUpdate = true;
				}
				if (base.npc.ai[2] == 670f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num89 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num89].netUpdate = true;
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
					for (int num90 = 0; num90 < 2; num90++)
					{
						int num91 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num91].velocity *= 2.9f;
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 20f && !this.thirdRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					this.thirdRoar = true;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 40f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num92 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num92].netUpdate = true;
				}
				if (base.npc.ai[3] == 120f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num93 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num93].netUpdate = true;
				}
				if (base.npc.ai[3] == 200f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num94 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num94].netUpdate = true;
				}
				if (base.npc.ai[3] == 240f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num95 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num95].netUpdate = true;
				}
				if (base.npc.ai[3] == 280f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num96 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num96].netUpdate = true;
				}
				if (base.npc.ai[3] == 300f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num97 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num97].netUpdate = true;
				}
				if (base.npc.ai[3] == 320f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num98 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num98].netUpdate = true;
				}
				if (base.npc.ai[3] == 330f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num99 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num99].netUpdate = true;
				}
				if (base.npc.ai[3] == 340f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num100 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num100].netUpdate = true;
				}
				if (base.npc.ai[3] == 350f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num101 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num101].netUpdate = true;
				}
				if (base.npc.ai[3] == 360f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					int num102 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num102].netUpdate = true;
				}
				if (base.npc.ai[3] == 370f)
				{
					for (int num103 = 0; num103 < 40; num103++)
					{
						int num104 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num104].velocity *= 3.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num105 = 11f;
					Vector2 vector7;
					vector7..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num106 = 65;
					int num107 = base.mod.ProjectileType("MACEFireProj");
					float num108 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
					int num109 = Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num108) * (double)num105 * -1.0), (float)(Math.Sin((double)num108) * (double)num105 * -1.0), num107, num106, 0f, 0, 0f, 0f);
					Main.projectile[num109].netUpdate = true;
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
					int num110 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num110].netUpdate = true;
				}
				if (base.npc.ai[3] == 635f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num111 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num111].netUpdate = true;
				}
				if (base.npc.ai[3] == 640f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num112 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num112].netUpdate = true;
				}
				if (base.npc.ai[3] == 645f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num113 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num113].netUpdate = true;
				}
				if (base.npc.ai[3] == 650f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num114 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num114].netUpdate = true;
				}
				if (base.npc.ai[3] == 655f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num115 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num115].netUpdate = true;
				}
				if (base.npc.ai[3] == 660f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num116 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num116].netUpdate = true;
				}
				if (base.npc.ai[3] == 665f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num117 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num117].netUpdate = true;
				}
				if (base.npc.ai[3] == 670f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num118 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num118].netUpdate = true;
				}
				if (base.npc.ai[3] == 675f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num119 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num119].netUpdate = true;
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
					for (int num120 = 0; num120 < 20; num120++)
					{
						int num121 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num121].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num122 = 6f;
					Vector2 vector8;
					vector8..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num123 = 40;
					int num124 = base.mod.ProjectileType("MACEMiniblast");
					float num125 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num126 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num125) * (double)num122 * -1.0), (float)(Math.Sin((double)num125) * (double)num122 * -1.0), num124, num123, 0f, 0, 0f, 0f);
					int num127 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num125) * (double)num122 * -1.0) + -1f, (float)(Math.Sin((double)num125) * (double)num122 * -1.0) + -1f, num124, num123, 0f, 0, 0f, 0f);
					int num128 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num125) * (double)num122 * -1.0) + 1f, (float)(Math.Sin((double)num125) * (double)num122 * -1.0) + 1f, num124, num123, 0f, 0, 0f, 0f);
					Main.projectile[num126].netUpdate = true;
					Main.projectile[num127].netUpdate = true;
					Main.projectile[num128].netUpdate = true;
				}
				if (base.npc.ai[3] == 1255f)
				{
					for (int num129 = 0; num129 < 20; num129++)
					{
						int num130 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num130].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num131 = 7f;
					Vector2 vector9;
					vector9..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num132 = 40;
					int num133 = base.mod.ProjectileType("MACEMiniblast");
					float num134 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					int num135 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num134) * (double)num131 * -1.0), (float)(Math.Sin((double)num134) * (double)num131 * -1.0), num133, num132, 0f, 0, 0f, 0f);
					Main.projectile[num135].netUpdate = true;
				}
				if (base.npc.ai[3] == 1265f)
				{
					for (int num136 = 0; num136 < 20; num136++)
					{
						int num137 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num137].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num138 = 8f;
					Vector2 vector10;
					vector10..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num139 = 40;
					int num140 = base.mod.ProjectileType("MACEMiniblast");
					float num141 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					int num142 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num141) * (double)num138 * -1.0), (float)(Math.Sin((double)num141) * (double)num138 * -1.0), num140, num139, 0f, 0, 0f, 0f);
					int num143 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num141) * (double)num138 * -1.0) + -1f, (float)(Math.Sin((double)num141) * (double)num138 * -1.0) + -1f, num140, num139, 0f, 0, 0f, 0f);
					int num144 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num141) * (double)num138 * -1.0) + 1f, (float)(Math.Sin((double)num141) * (double)num138 * -1.0) + 1f, num140, num139, 0f, 0, 0f, 0f);
					Main.projectile[num142].netUpdate = true;
					Main.projectile[num143].netUpdate = true;
					Main.projectile[num144].netUpdate = true;
				}
				if (base.npc.ai[3] == 1275f)
				{
					for (int num145 = 0; num145 < 20; num145++)
					{
						int num146 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num146].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num147 = 9f;
					Vector2 vector11;
					vector11..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num148 = 40;
					int num149 = base.mod.ProjectileType("MACEMiniblast");
					float num150 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
					int num151 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num150) * (double)num147 * -1.0), (float)(Math.Sin((double)num150) * (double)num147 * -1.0), num149, num148, 0f, 0, 0f, 0f);
					Main.projectile[num151].netUpdate = true;
				}
				if (base.npc.ai[3] == 1285f)
				{
					for (int num152 = 0; num152 < 20; num152++)
					{
						int num153 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num153].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num154 = 10f;
					Vector2 vector12;
					vector12..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num155 = 40;
					int num156 = base.mod.ProjectileType("MACEMiniblast");
					float num157 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					int num158 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num157) * (double)num154 * -1.0), (float)(Math.Sin((double)num157) * (double)num154 * -1.0), num156, num155, 0f, 0, 0f, 0f);
					int num159 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num157) * (double)num154 * -1.0) + -1f, (float)(Math.Sin((double)num157) * (double)num154 * -1.0) + -1f, num156, num155, 0f, 0, 0f, 0f);
					int num160 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num157) * (double)num154 * -1.0) + 1f, (float)(Math.Sin((double)num157) * (double)num154 * -1.0) + 1f, num156, num155, 0f, 0, 0f, 0f);
					Main.projectile[num158].netUpdate = true;
					Main.projectile[num159].netUpdate = true;
					Main.projectile[num160].netUpdate = true;
				}
				if (base.npc.ai[3] == 1295f)
				{
					for (int num161 = 0; num161 < 20; num161++)
					{
						int num162 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num162].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num163 = 11f;
					Vector2 vector13;
					vector13..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num164 = 40;
					int num165 = base.mod.ProjectileType("MACEMiniblast");
					float num166 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
					int num167 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num166) * (double)num163 * -1.0), (float)(Math.Sin((double)num166) * (double)num163 * -1.0), num165, num164, 0f, 0, 0f, 0f);
					Main.projectile[num167].netUpdate = true;
				}
				if (base.npc.ai[3] == 1305f)
				{
					for (int num168 = 0; num168 < 20; num168++)
					{
						int num169 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num169].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num170 = 12f;
					Vector2 vector14;
					vector14..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num171 = 40;
					int num172 = base.mod.ProjectileType("MACEMiniblast");
					float num173 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					int num174 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num173) * (double)num170 * -1.0), (float)(Math.Sin((double)num173) * (double)num170 * -1.0), num172, num171, 0f, 0, 0f, 0f);
					int num175 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num173) * (double)num170 * -1.0) + -1f, (float)(Math.Sin((double)num173) * (double)num170 * -1.0) + -1f, num172, num171, 0f, 0, 0f, 0f);
					int num176 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num173) * (double)num170 * -1.0) + 1f, (float)(Math.Sin((double)num173) * (double)num170 * -1.0) + 1f, num172, num171, 0f, 0, 0f, 0f);
					Main.projectile[num174].netUpdate = true;
					Main.projectile[num175].netUpdate = true;
					Main.projectile[num176].netUpdate = true;
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
					int num177 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num177].netUpdate = true;
				}
				if (base.npc.ai[3] == 1537f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num178 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num178].netUpdate = true;
				}
				if (base.npc.ai[3] == 1539f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num179 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num179].netUpdate = true;
				}
				if (base.npc.ai[3] == 1541f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num180 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num180].netUpdate = true;
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
