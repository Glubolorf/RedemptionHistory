using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	[AutoloadBossHead]
	public class MACEProjectHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("MACE Project");
		}

		public override void SetDefaults()
		{
			base.npc.width = 120;
			base.npc.height = 140;
			base.npc.friendly = false;
			base.npc.damage = 100;
			base.npc.defense = 50;
			base.npc.lifeMax = 215000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 25, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/LabBossMusic");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 50; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[num].velocity *= 1.9f;
				}
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
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TheMace"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FloppyDisk6"), 1, false, 0, false, false);
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (NPC.AnyNPCs(base.mod.NPCType("MACEProjectJaw")))
			{
				base.npc.dontTakeDamage = true;
			}
			else
			{
				base.npc.dontTakeDamage = false;
				this.trueFightBegin = false;
			}
			this.MACETimer++;
			if (this.MACETimer == 1)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				if (Main.netMode != 1)
				{
					Vector2 vector;
					vector..ctor(250f, -100f);
					base.npc.Center = base.npc.position + vector;
					base.npc.netUpdate = true;
				}
			}
			if (this.MACETimer <= 2 && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectJaw")))
			{
				NPC.NewNPC((int)base.npc.position.X + 60, (int)base.npc.position.Y + 170, base.mod.NPCType("MACEProjectJaw"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (this.MACETimer <= 120)
			{
				base.npc.alpha -= 4;
			}
			if (this.MACETimer > 120)
			{
				this.beginFight = true;
			}
			if (this.beginFight)
			{
				this.fightTimer++;
				if (this.fightTimer == 90)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num = 14f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + 39f, base.npc.position.Y + 91f);
					int num2 = 40;
					int num3 = base.mod.ProjectileType("MACELaser1");
					float num4 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + 81f, base.npc.position.Y + 91f);
					Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer >= 95)
				{
					this.fightTimer = 0;
				}
			}
			if (this.MACETimer > 120 && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectJaw")))
			{
				this.trueFightBegin = true;
			}
			if (this.trueFightBegin)
			{
				this.beginFight = false;
				this.fightTimer = 0;
				this.fightTimer2++;
				if (NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1")) || NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist3")) || NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist4")))
				{
					if (this.fightTimer2 == 60)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num5 = 14f;
						Vector2 vector4;
						vector4..ctor(base.npc.position.X + 39f, base.npc.position.Y + 91f);
						int num6 = 30;
						int num7 = base.mod.ProjectileType("MACELaser1");
						float num8 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
						Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num8) * (double)num5 * -1.0), (float)(Math.Sin((double)num8) * (double)num5 * -1.0), num7, num6, 0f, 0, 0f, 0f);
						Vector2 vector5;
						vector5..ctor(base.npc.position.X + 81f, base.npc.position.Y + 91f);
						Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num8) * (double)num5 * -1.0), (float)(Math.Sin((double)num8) * (double)num5 * -1.0), num7, num6, 0f, 0, 0f, 0f);
					}
					if (this.fightTimer2 >= 60)
					{
						this.fightTimer2 = 0;
					}
				}
				else
				{
					if (this.fightTimer2 >= 60 && this.fightTimer2 < 120)
					{
						this.spamTimer++;
						if (this.spamTimer == 5)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							float num9 = 14f;
							Vector2 vector6;
							vector6..ctor(base.npc.position.X + 39f, base.npc.position.Y + 91f);
							int num10 = 30;
							int num11 = base.mod.ProjectileType("MACELaser1");
							float num12 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
							Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0), (float)(Math.Sin((double)num12) * (double)num9 * -1.0), num11, num10, 0f, 0, 0f, 0f);
							Vector2 vector7;
							vector7..ctor(base.npc.position.X + 81f, base.npc.position.Y + 91f);
							Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0), (float)(Math.Sin((double)num12) * (double)num9 * -1.0), num11, num10, 0f, 0, 0f, 0f);
						}
						if (this.spamTimer >= 10)
						{
							this.spamTimer = 0;
						}
					}
					if (this.fightTimer2 == 90)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num13 = 10f;
						Vector2 vector8;
						vector8..ctor(base.npc.position.X + 50f, base.npc.position.Y + 54f);
						int num14 = 30;
						int num15 = base.mod.ProjectileType("XenoShard3");
						float num16 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num16) * (double)num13 * -1.0), (float)(Math.Sin((double)num16) * (double)num13 * -1.0), num15, num14, 0f, 0, 0f, 0f);
					}
					if (this.fightTimer2 == 140)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num17 = 8;
						for (int i = 0; i < num17; i++)
						{
							int num18 = Projectile.NewProjectile(base.npc.position.X + 39f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[num18].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)i / (float)num17 * 6.28f);
						}
					}
					if (this.fightTimer2 == 170)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num19 = 8;
						for (int j = 0; j < num19; j++)
						{
							int num20 = Projectile.NewProjectile(base.npc.position.X + 81f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[num20].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)j / (float)num19 * 6.28f);
						}
					}
					if (this.fightTimer2 == 200)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num21 = 8;
						for (int k = 0; k < num21; k++)
						{
							int num22 = Projectile.NewProjectile(base.npc.position.X + 39f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[num22].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)k / (float)num21 * 6.28f);
						}
					}
					if (this.fightTimer2 >= 320 && this.fightTimer2 <= 360)
					{
						this.spamTimer2++;
						if (this.spamTimer2 == 4)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							float num23 = 14f;
							Vector2 vector9;
							vector9..ctor(base.npc.position.X + 39f, base.npc.position.Y + 91f);
							int num24 = 30;
							int num25 = base.mod.ProjectileType("MACELaser1");
							float num26 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
							Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num26) * (double)num23 * -1.0), (float)(Math.Sin((double)num26) * (double)num23 * -1.0), num25, num24, 0f, 0, 0f, 0f);
						}
						if (this.spamTimer2 == 8)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							float num27 = 14f;
							int num28 = 30;
							Vector2 vector10;
							vector10..ctor(base.npc.position.X + 81f, base.npc.position.Y + 91f);
							int num29 = base.mod.ProjectileType("MACELaser1");
							float num30 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
							Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num30) * (double)num27 * -1.0), (float)(Math.Sin((double)num30) * (double)num27 * -1.0), num29, num28, 0f, 0, 0f, 0f);
						}
						if (this.spamTimer2 >= 12)
						{
							this.spamTimer2 = 0;
						}
					}
					if (this.fightTimer2 == 320)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(3f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-3f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
					}
					if (this.fightTimer2 == 330)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(6f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-6f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
					}
					if (this.fightTimer2 == 340)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(9f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-9f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
					}
					if (this.fightTimer2 == 350)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(12f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-12f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
					}
					if (this.fightTimer2 == 360)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(15f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-15f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
					}
					if (this.fightTimer2 >= 400)
					{
						this.fightTimer2 = 0;
					}
				}
			}
			if (!NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1")))
			{
				if (base.npc.life <= 215000 && !this.fistSummon1)
				{
					this.fistTimer1++;
					if (this.fistTimer1 == 20)
					{
						NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist1"), 0, 0f, 0f, 0f, 0f, 255);
						this.fistSummon1 = true;
						this.fistTimer1 = 0;
					}
				}
				if (base.npc.life <= 170000 && !this.fistSummon2)
				{
					this.fistTimer2++;
					if (this.fistTimer2 == 20)
					{
						NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist1"), 0, 0f, 0f, 0f, 0f, 255);
						this.fistSummon2 = true;
						this.fistTimer2 = 0;
					}
				}
				if (base.npc.life <= 100000 && !this.fistSummon3)
				{
					this.fistTimer3++;
					if (this.fistTimer3 == 20)
					{
						NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist3"), 0, 0f, 0f, 0f, 0f, 255);
						NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist4"), 0, 0f, 0f, 0f, 0f, 255);
						this.fistSummon3 = true;
						this.fistTimer3 = 0;
					}
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

		private bool trueFightBegin;

		private int fightTimer2;

		private bool fistSummon1;

		private int fistTimer1;

		private bool fistSummon2;

		private int fistTimer2;

		private bool fistSummon3;

		private int fistTimer3;

		private int spamTimer2;
	}
}
