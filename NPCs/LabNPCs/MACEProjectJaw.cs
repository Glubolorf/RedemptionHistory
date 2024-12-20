using System;
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
			base.npc.lifeMax = 185000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
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

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			this.MACETimer++;
			if (this.MACETimer <= 120)
			{
				base.npc.alpha -= 4;
				base.npc.dontTakeDamage = true;
			}
			if (this.MACETimer > 120)
			{
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
			}
			if (this.beginFight)
			{
				this.fightTimer++;
				if (this.fightTimer >= 15 && this.fightTimer < 20)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y + 0.7f;
				}
				if (this.fightTimer >= 20 && this.fightTimer < 85)
				{
					base.npc.velocity.Y = 0f;
					for (int i = 0; i < 2; i++)
					{
						int num = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num].velocity *= 2.9f;
					}
				}
				if (this.fightTimer == 20 && !this.firstRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					this.firstRoar = true;
				}
				if (this.fightTimer == 35)
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
					}
				}
				if (this.fightTimer == 40)
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
					}
				}
				if (this.fightTimer == 45)
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
					}
				}
				if (this.fightTimer >= 85 && this.fightTimer < 90)
				{
					NPC npc2 = base.npc;
					npc2.velocity.Y = npc2.velocity.Y - 0.7f;
				}
				if (this.fightTimer >= 90 && this.fightTimer < 235)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer >= 235 && this.fightTimer < 240)
				{
					NPC npc3 = base.npc;
					npc3.velocity.Y = npc3.velocity.Y + 0.7f;
				}
				if (this.fightTimer >= 240 && this.fightTimer < 300)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer == 250)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 270)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 290)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer >= 300 && this.fightTimer < 305)
				{
					NPC npc4 = base.npc;
					npc4.velocity.Y = npc4.velocity.Y - 0.7f;
				}
				if (this.fightTimer >= 305 && this.fightTimer < 505)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer >= 505 && this.fightTimer < 510)
				{
					NPC npc5 = base.npc;
					npc5.velocity.Y = npc5.velocity.Y + 0.7f;
				}
				if (this.fightTimer >= 510 && this.fightTimer < 560)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer == 520)
				{
					for (int num12 = 0; num12 < 20; num12++)
					{
						int num13 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num13].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num14 = 8f;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num15 = 40;
					int num16 = base.mod.ProjectileType("MACEMiniblast");
					float num17 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num17) * (double)num14 * -1.0), (float)(Math.Sin((double)num17) * (double)num14 * -1.0), num16, num15, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num17) * (double)num14 * -1.0) + -1f, (float)(Math.Sin((double)num17) * (double)num14 * -1.0) + -1f, num16, num15, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num17) * (double)num14 * -1.0) + 1f, (float)(Math.Sin((double)num17) * (double)num14 * -1.0) + 1f, num16, num15, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 560)
				{
					for (int num18 = 0; num18 < 20; num18++)
					{
						int num19 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num19].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num20 = 10f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num21 = 40;
					int num22 = base.mod.ProjectileType("MACEMiniblast");
					float num23 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num23) * (double)num20 * -1.0), (float)(Math.Sin((double)num23) * (double)num20 * -1.0), num22, num21, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num23) * (double)num20 * -1.0) + -1f, (float)(Math.Sin((double)num23) * (double)num20 * -1.0) + -1f, num22, num21, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num23) * (double)num20 * -1.0) + 1f, (float)(Math.Sin((double)num23) * (double)num20 * -1.0) + 1f, num22, num21, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer >= 580 && this.fightTimer < 585)
				{
					NPC npc6 = base.npc;
					npc6.velocity.Y = npc6.velocity.Y - 0.7f;
				}
				if (this.fightTimer >= 585)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer >= 760)
				{
					this.fightTimer = 0;
				}
			}
			if (base.npc.life <= 155000 && !this.phase2Done && this.fightTimer == 0)
			{
				this.phase2 = true;
			}
			if (this.phase2)
			{
				this.fightTimer = 0;
				this.beginFight = false;
				this.fightTimer2++;
				if (this.fightTimer2 >= 15 && this.fightTimer2 < 20)
				{
					NPC npc7 = base.npc;
					npc7.velocity.Y = npc7.velocity.Y + 0.7f;
				}
				if (this.fightTimer2 >= 20 && this.fightTimer2 < 85)
				{
					base.npc.velocity.Y = 0f;
					for (int num24 = 0; num24 < 2; num24++)
					{
						int num25 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num25].velocity *= 2.9f;
					}
				}
				if (this.fightTimer2 == 20 && !this.secondRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					this.secondRoar = true;
				}
				if (this.fightTimer2 == 35)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int num26 = 0; num26 < 20; num26++)
					{
						int num27 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num27].velocity *= 2.9f;
					}
					int num28 = 5;
					for (int num29 = 0; num29 < num28; num29++)
					{
						int num30 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num30].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num29 / (float)num28 * 6.28f);
					}
				}
				if (this.fightTimer2 == 40)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int num31 = 0; num31 < 20; num31++)
					{
						int num32 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num32].velocity *= 2.9f;
					}
					int num33 = 10;
					for (int num34 = 0; num34 < num33; num34++)
					{
						int num35 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num35].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)num34 / (float)num33 * 6.28f);
					}
				}
				if (this.fightTimer2 == 45)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int num36 = 0; num36 < 20; num36++)
					{
						int num37 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num37].velocity *= 2.9f;
					}
					int num38 = 15;
					for (int num39 = 0; num39 < num38; num39++)
					{
						int num40 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEMiniblast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num40].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)num39 / (float)num38 * 6.28f);
					}
				}
				if (this.fightTimer2 >= 85 && this.fightTimer2 < 90)
				{
					NPC npc8 = base.npc;
					npc8.velocity.Y = npc8.velocity.Y - 0.7f;
				}
				if (this.fightTimer2 >= 90 && this.fightTimer2 < 235)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer2 >= 235 && this.fightTimer2 < 240)
				{
					NPC npc9 = base.npc;
					npc9.velocity.Y = npc9.velocity.Y + 0.7f;
				}
				if (this.fightTimer2 >= 240 && this.fightTimer2 < 300)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer2 == 250)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 260)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 270)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 280)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 290)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 >= 300 && this.fightTimer2 < 305)
				{
					NPC npc10 = base.npc;
					npc10.velocity.Y = npc10.velocity.Y - 0.7f;
				}
				if (this.fightTimer2 >= 305 && this.fightTimer2 < 485)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer2 >= 485 && this.fightTimer2 < 490)
				{
					NPC npc11 = base.npc;
					npc11.velocity.Y = npc11.velocity.Y + 0.7f;
				}
				if (this.fightTimer2 >= 490 && this.fightTimer2 < 580)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer2 == 520)
				{
					for (int num41 = 0; num41 < 20; num41++)
					{
						int num42 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num42].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num43 = 8f;
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num44 = 40;
					int num45 = base.mod.ProjectileType("MACEMiniblast");
					float num46 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num46) * (double)num43 * -1.0), (float)(Math.Sin((double)num46) * (double)num43 * -1.0), num45, num44, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num46) * (double)num43 * -1.0) + -1f, (float)(Math.Sin((double)num46) * (double)num43 * -1.0) + -1f, num45, num44, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num46) * (double)num43 * -1.0) + 1f, (float)(Math.Sin((double)num46) * (double)num43 * -1.0) + 1f, num45, num44, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 540)
				{
					for (int num47 = 0; num47 < 20; num47++)
					{
						int num48 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num48].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num49 = 10f;
					Vector2 vector4;
					vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num50 = 40;
					int num51 = base.mod.ProjectileType("MACEMiniblast");
					float num52 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num52) * (double)num49 * -1.0), (float)(Math.Sin((double)num52) * (double)num49 * -1.0), num51, num50, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 560)
				{
					for (int num53 = 0; num53 < 20; num53++)
					{
						int num54 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num54].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num55 = 12f;
					Vector2 vector5;
					vector5..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num56 = 40;
					int num57 = base.mod.ProjectileType("MACEMiniblast");
					float num58 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num58) * (double)num55 * -1.0), (float)(Math.Sin((double)num58) * (double)num55 * -1.0), num57, num56, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num58) * (double)num55 * -1.0) + -1f, (float)(Math.Sin((double)num58) * (double)num55 * -1.0) + -1f, num57, num56, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num58) * (double)num55 * -1.0) + 1f, (float)(Math.Sin((double)num58) * (double)num55 * -1.0) + 1f, num57, num56, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 580)
				{
					for (int num59 = 0; num59 < 20; num59++)
					{
						int num60 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num60].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num61 = 14f;
					Vector2 vector6;
					vector6..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num62 = 40;
					int num63 = base.mod.ProjectileType("MACEMiniblast");
					float num64 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num64) * (double)num61 * -1.0), (float)(Math.Sin((double)num64) * (double)num61 * -1.0), num63, num62, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 >= 580 && this.fightTimer2 < 585)
				{
					NPC npc12 = base.npc;
					npc12.velocity.Y = npc12.velocity.Y - 0.7f;
				}
				if (this.fightTimer2 >= 585 && this.fightTimer2 < 640)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer2 >= 640 && this.fightTimer2 < 645)
				{
					NPC npc13 = base.npc;
					npc13.velocity.Y = npc13.velocity.Y + 0.7f;
				}
				if (this.fightTimer2 >= 645 && this.fightTimer2 < 680)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer2 == 650)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 660)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 670)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 >= 680 && this.fightTimer2 < 685)
				{
					NPC npc14 = base.npc;
					npc14.velocity.Y = npc14.velocity.Y - 0.7f;
				}
				if (this.fightTimer2 >= 685)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer2 >= 800)
				{
					this.fightTimer2 = 0;
				}
			}
			if (base.npc.life <= 100000 && this.fightTimer2 == 0)
			{
				this.phase3 = true;
				this.phase2Done = true;
			}
			if (this.phase3)
			{
				this.fightTimer = 0;
				this.fightTimer2 = 0;
				this.phase2Done = true;
				this.phase2 = false;
				this.fightTimer3++;
				if (this.fightTimer3 >= 15 && this.fightTimer3 < 20)
				{
					NPC npc15 = base.npc;
					npc15.velocity.Y = npc15.velocity.Y + 0.9f;
				}
				if (this.fightTimer3 >= 20 && this.fightTimer3 < 395)
				{
					base.npc.velocity.Y = 0f;
					for (int num65 = 0; num65 < 2; num65++)
					{
						int num66 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num66].velocity *= 2.9f;
					}
				}
				if (this.fightTimer3 == 20 && !this.thirdRoar)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
					}
					this.thirdRoar = true;
				}
				if (this.fightTimer3 == 40)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 120)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 200)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 240)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 280)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 300)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 320)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 330)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 340)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 350)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 360)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm1").WithVolume(0.6f).WithPitchVariance(0f), -1, -1);
					}
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("MACEAlarm"), 0, 0f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 370)
				{
					for (int num67 = 0; num67 < 40; num67++)
					{
						int num68 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num68].velocity *= 3.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num69 = 11f;
					Vector2 vector7;
					vector7..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num70 = 65;
					int num71 = base.mod.ProjectileType("MACEFireProj");
					float num72 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num72) * (double)num69 * -1.0), (float)(Math.Sin((double)num72) * (double)num69 * -1.0), num71, num70, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 >= 395 && this.fightTimer3 < 400)
				{
					NPC npc16 = base.npc;
					npc16.velocity.Y = npc16.velocity.Y - 0.9f;
				}
				if (this.fightTimer3 >= 400 && this.fightTimer3 < 600)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer3 >= 600 && this.fightTimer3 < 605)
				{
					NPC npc17 = base.npc;
					npc17.velocity.Y = npc17.velocity.Y + 0.7f;
				}
				if (this.fightTimer3 >= 605 && this.fightTimer3 < 695)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer3 == 630)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 635)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 640)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 645)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 650)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 655)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 660)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 665)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 670)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 675)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 >= 695 && this.fightTimer3 < 700)
				{
					NPC npc18 = base.npc;
					npc18.velocity.Y = npc18.velocity.Y - 0.7f;
				}
				if (this.fightTimer3 >= 700 && this.fightTimer3 < 1200)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer3 >= 1200 && this.fightTimer3 < 1205)
				{
					NPC npc19 = base.npc;
					npc19.velocity.Y = npc19.velocity.Y + 0.7f;
				}
				if (this.fightTimer3 >= 1205 && this.fightTimer3 < 1295)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer3 == 1245)
				{
					for (int num73 = 0; num73 < 20; num73++)
					{
						int num74 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num74].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num75 = 6f;
					Vector2 vector8;
					vector8..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num76 = 40;
					int num77 = base.mod.ProjectileType("MACEMiniblast");
					float num78 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num78) * (double)num75 * -1.0), (float)(Math.Sin((double)num78) * (double)num75 * -1.0), num77, num76, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num78) * (double)num75 * -1.0) + -1f, (float)(Math.Sin((double)num78) * (double)num75 * -1.0) + -1f, num77, num76, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num78) * (double)num75 * -1.0) + 1f, (float)(Math.Sin((double)num78) * (double)num75 * -1.0) + 1f, num77, num76, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1255)
				{
					for (int num79 = 0; num79 < 20; num79++)
					{
						int num80 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num80].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num81 = 7f;
					Vector2 vector9;
					vector9..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num82 = 40;
					int num83 = base.mod.ProjectileType("MACEMiniblast");
					float num84 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num84) * (double)num81 * -1.0), (float)(Math.Sin((double)num84) * (double)num81 * -1.0), num83, num82, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1265)
				{
					for (int num85 = 0; num85 < 20; num85++)
					{
						int num86 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num86].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num87 = 8f;
					Vector2 vector10;
					vector10..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num88 = 40;
					int num89 = base.mod.ProjectileType("MACEMiniblast");
					float num90 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num90) * (double)num87 * -1.0), (float)(Math.Sin((double)num90) * (double)num87 * -1.0), num89, num88, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num90) * (double)num87 * -1.0) + -1f, (float)(Math.Sin((double)num90) * (double)num87 * -1.0) + -1f, num89, num88, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num90) * (double)num87 * -1.0) + 1f, (float)(Math.Sin((double)num90) * (double)num87 * -1.0) + 1f, num89, num88, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1275)
				{
					for (int num91 = 0; num91 < 20; num91++)
					{
						int num92 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num92].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num93 = 9f;
					Vector2 vector11;
					vector11..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num94 = 40;
					int num95 = base.mod.ProjectileType("MACEMiniblast");
					float num96 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num96) * (double)num93 * -1.0), (float)(Math.Sin((double)num96) * (double)num93 * -1.0), num95, num94, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1285)
				{
					for (int num97 = 0; num97 < 20; num97++)
					{
						int num98 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num98].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num99 = 10f;
					Vector2 vector12;
					vector12..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num100 = 40;
					int num101 = base.mod.ProjectileType("MACEMiniblast");
					float num102 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num102) * (double)num99 * -1.0), (float)(Math.Sin((double)num102) * (double)num99 * -1.0), num101, num100, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num102) * (double)num99 * -1.0) + -1f, (float)(Math.Sin((double)num102) * (double)num99 * -1.0) + -1f, num101, num100, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num102) * (double)num99 * -1.0) + 1f, (float)(Math.Sin((double)num102) * (double)num99 * -1.0) + 1f, num101, num100, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1295)
				{
					for (int num103 = 0; num103 < 20; num103++)
					{
						int num104 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num104].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num105 = 11f;
					Vector2 vector13;
					vector13..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num106 = 40;
					int num107 = base.mod.ProjectileType("MACEMiniblast");
					float num108 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num108) * (double)num105 * -1.0), (float)(Math.Sin((double)num108) * (double)num105 * -1.0), num107, num106, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1305)
				{
					for (int num109 = 0; num109 < 20; num109++)
					{
						int num110 = Dust.NewDust(new Vector2(base.npc.Center.X, base.npc.Center.Y), 6, 6, 158, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num110].velocity *= 2.9f;
					}
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num111 = 12f;
					Vector2 vector14;
					vector14..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num112 = 40;
					int num113 = base.mod.ProjectileType("MACEMiniblast");
					float num114 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num114) * (double)num111 * -1.0), (float)(Math.Sin((double)num114) * (double)num111 * -1.0), num113, num112, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num114) * (double)num111 * -1.0) + -1f, (float)(Math.Sin((double)num114) * (double)num111 * -1.0) + -1f, num113, num112, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num114) * (double)num111 * -1.0) + 1f, (float)(Math.Sin((double)num114) * (double)num111 * -1.0) + 1f, num113, num112, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 >= 1395 && this.fightTimer3 < 1400)
				{
					NPC npc20 = base.npc;
					npc20.velocity.Y = npc20.velocity.Y - 0.7f;
				}
				if (this.fightTimer3 >= 1400 && this.fightTimer3 < 1405)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer3 >= 1505 && this.fightTimer3 < 1510)
				{
					NPC npc21 = base.npc;
					npc21.velocity.Y = npc21.velocity.Y + 0.7f;
				}
				if (this.fightTimer3 >= 1510 && this.fightTimer3 < 1600)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer3 == 1535)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 1537)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 1539)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 1541)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("MACEnade"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 >= 1600 && this.fightTimer3 < 1605)
				{
					NPC npc22 = base.npc;
					npc22.velocity.Y = npc22.velocity.Y - 0.7f;
				}
				if (this.fightTimer3 >= 1605)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.fightTimer3 >= 1800)
				{
					this.fightTimer3 = 0;
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

		private int MACETimer;

		private bool beginFight;

		private int fightTimer;

		private int spamTimer;

		private bool firstRoar;

		private bool phase2Done;

		private bool phase2;

		private int fightTimer2;

		private bool secondRoar;

		private bool phase3Done;

		private bool phase3;

		private int fightTimer3;

		private bool thirdRoar;
	}
}
