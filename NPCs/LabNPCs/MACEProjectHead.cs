using System;
using System.IO;
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
			base.npc.defense = 70;
			base.npc.lifeMax = 125000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 25, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
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
			if (!RedeWorld.labAccess6)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel6"), 1, false, 0, false, false);
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
				writer.Write(this.beginFight);
				writer.Write(this.trueFightBegin);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
				this.customAI[1] = reader.ReadFloat();
				this.beginFight = reader.ReadBool();
				this.trueFightBegin = reader.ReadBool();
			}
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
			base.npc.ai[0] += 1f;
			if (base.npc.ai[0] == 1f)
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
			if (base.npc.ai[0] <= 2f && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectJaw")))
			{
				int num = NPC.NewNPC((int)base.npc.position.X + 60, (int)base.npc.position.Y + 170, base.mod.NPCType("MACEProjectJaw"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num].netUpdate = true;
			}
			if (base.npc.ai[0] <= 120f)
			{
				base.npc.alpha -= 4;
			}
			if (base.npc.ai[0] > 120f)
			{
				this.beginFight = true;
				base.npc.netUpdate = true;
			}
			if (this.beginFight)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 90f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num2 = 14f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + 39f, base.npc.position.Y + 91f);
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + 81f, base.npc.position.Y + 91f);
					int num3 = 40;
					int num4 = base.mod.ProjectileType("MACELaser1");
					float num5 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num6 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
					int num7 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
					Main.projectile[num6].netUpdate = true;
					Main.projectile[num7].netUpdate = true;
				}
				if (base.npc.ai[1] >= 95f)
				{
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[0] > 120f && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectJaw")))
			{
				this.trueFightBegin = true;
				base.npc.netUpdate = true;
			}
			if (this.trueFightBegin)
			{
				this.beginFight = false;
				base.npc.ai[1] = 0f;
				base.npc.ai[2] += 1f;
				if (NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1")) || NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist3")) || NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist4")))
				{
					if (base.npc.ai[2] == 60f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num8 = 14f;
						Vector2 vector4;
						vector4..ctor(base.npc.position.X + 39f, base.npc.position.Y + 91f);
						Vector2 vector5;
						vector5..ctor(base.npc.position.X + 81f, base.npc.position.Y + 91f);
						int num9 = 30;
						int num10 = base.mod.ProjectileType("MACELaser1");
						float num11 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
						int num12 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num11) * (double)num8 * -1.0), (float)(Math.Sin((double)num11) * (double)num8 * -1.0), num10, num9, 0f, 0, 0f, 0f);
						int num13 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num11) * (double)num8 * -1.0), (float)(Math.Sin((double)num11) * (double)num8 * -1.0), num10, num9, 0f, 0, 0f, 0f);
						Main.projectile[num12].netUpdate = true;
						Main.projectile[num13].netUpdate = true;
					}
					if (base.npc.ai[2] >= 60f)
					{
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (base.npc.ai[2] >= 60f && base.npc.ai[2] < 120f)
					{
						base.npc.ai[3] += 1f;
						if (base.npc.ai[3] == 5f)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							float num14 = 14f;
							Vector2 vector6;
							vector6..ctor(base.npc.position.X + 39f, base.npc.position.Y + 91f);
							Vector2 vector7;
							vector7..ctor(base.npc.position.X + 81f, base.npc.position.Y + 91f);
							int num15 = 30;
							int num16 = base.mod.ProjectileType("MACELaser1");
							float num17 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
							int num18 = Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num17) * (double)num14 * -1.0), (float)(Math.Sin((double)num17) * (double)num14 * -1.0), num16, num15, 0f, 0, 0f, 0f);
							int num19 = Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num17) * (double)num14 * -1.0), (float)(Math.Sin((double)num17) * (double)num14 * -1.0), num16, num15, 0f, 0, 0f, 0f);
							Main.projectile[num18].netUpdate = true;
							Main.projectile[num19].netUpdate = true;
						}
						if (base.npc.ai[3] >= 10f)
						{
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 90f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num20 = 10f;
						Vector2 vector8;
						vector8..ctor(base.npc.position.X + 50f, base.npc.position.Y + 54f);
						int num21 = 30;
						int num22 = base.mod.ProjectileType("XenoShard3");
						float num23 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num24 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num23) * (double)num20 * -1.0), (float)(Math.Sin((double)num23) * (double)num20 * -1.0), num22, num21, 0f, 0, 0f, 0f);
						Main.projectile[num24].netUpdate = true;
					}
					if (base.npc.ai[2] == 140f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num25 = 8;
						for (int i = 0; i < num25; i++)
						{
							int num26 = Projectile.NewProjectile(base.npc.position.X + 39f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[num26].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)i / (float)num25 * 6.28f);
							Main.projectile[num26].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 170f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num27 = 8;
						for (int j = 0; j < num27; j++)
						{
							int num28 = Projectile.NewProjectile(base.npc.position.X + 81f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[num28].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)j / (float)num27 * 6.28f);
							Main.projectile[num28].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 200f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num29 = 8;
						for (int k = 0; k < num29; k++)
						{
							int num30 = Projectile.NewProjectile(base.npc.position.X + 39f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[num30].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)k / (float)num29 * 6.28f);
							Main.projectile[num30].netUpdate = true;
						}
					}
					if (base.npc.ai[2] >= 320f && base.npc.ai[2] <= 360f)
					{
						base.npc.ai[3] += 1f;
						if (base.npc.ai[3] == 4f)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							float num31 = 14f;
							Vector2 vector9;
							vector9..ctor(base.npc.position.X + 39f, base.npc.position.Y + 91f);
							int num32 = 30;
							int num33 = base.mod.ProjectileType("MACELaser1");
							float num34 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
							int num35 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num34) * (double)num31 * -1.0), (float)(Math.Sin((double)num34) * (double)num31 * -1.0), num33, num32, 0f, 0, 0f, 0f);
							Main.projectile[num35].netUpdate = true;
						}
						if (base.npc.ai[3] == 8f)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							float num36 = 14f;
							int num37 = 30;
							Vector2 vector10;
							vector10..ctor(base.npc.position.X + 81f, base.npc.position.Y + 91f);
							int num38 = base.mod.ProjectileType("MACELaser1");
							float num39 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
							int num40 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num39) * (double)num36 * -1.0), (float)(Math.Sin((double)num39) * (double)num36 * -1.0), num38, num37, 0f, 0, 0f, 0f);
							Main.projectile[num40].netUpdate = true;
						}
						if (base.npc.ai[3] >= 12f)
						{
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 320f)
					{
						int num41 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(3f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int num42 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-3f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[num41].netUpdate = true;
						Main.projectile[num42].netUpdate = true;
					}
					if (base.npc.ai[2] == 330f)
					{
						int num43 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(6f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int num44 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-6f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[num43].netUpdate = true;
						Main.projectile[num44].netUpdate = true;
					}
					if (base.npc.ai[2] == 340f)
					{
						int num45 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(9f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int num46 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-9f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[num45].netUpdate = true;
						Main.projectile[num46].netUpdate = true;
					}
					if (base.npc.ai[2] == 350f)
					{
						int num47 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(12f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int num48 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-12f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[num47].netUpdate = true;
						Main.projectile[num48].netUpdate = true;
					}
					if (base.npc.ai[2] == 360f)
					{
						int num49 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(15f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int num50 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-15f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[num49].netUpdate = true;
						Main.projectile[num50].netUpdate = true;
					}
					if (base.npc.ai[2] >= 460f)
					{
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
				}
			}
			if (!NPC.AnyNPCs(base.mod.NPCType("MACEProjectFist1")))
			{
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.75f) && this.customAI[0] == 0f)
				{
					this.customAI[1] += 1f;
					if (this.customAI[1] >= 20f)
					{
						int num51 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist1"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num51].netUpdate = true;
						this.customAI[0] = 1f;
						this.customAI[1] = 0f;
						base.npc.netUpdate = true;
					}
				}
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.4f) && this.customAI[0] == 1f)
				{
					this.customAI[1] += 1f;
					if (this.customAI[1] >= 20f)
					{
						int num52 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist3"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num52].netUpdate = true;
						int num53 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num53].netUpdate = true;
						this.customAI[0] = 2f;
						this.customAI[1] = 0f;
						base.npc.netUpdate = true;
					}
				}
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.7;
			return true;
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

		private bool trueFightBegin;

		public float[] customAI = new float[2];
	}
}
