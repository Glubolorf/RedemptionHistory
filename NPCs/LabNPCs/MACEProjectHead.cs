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
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 1.9f;
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
					Vector2 newPos = new Vector2(250f, -100f);
					base.npc.Center = base.npc.position + newPos;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[0] <= 2f && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectJaw")))
			{
				int minion = NPC.NewNPC((int)base.npc.position.X + 60, (int)base.npc.position.Y + 170, base.mod.NPCType("MACEProjectJaw"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion].netUpdate = true;
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
					float Speed = 14f;
					Vector2 vector8 = new Vector2(base.npc.position.X + 39f, base.npc.position.Y + 91f);
					Vector2 vector9 = new Vector2(base.npc.position.X + 81f, base.npc.position.Y + 91f);
					int damage = 40;
					int type = base.mod.ProjectileType("MACELaser1");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
					Main.projectile[num55].netUpdate = true;
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
						float Speed2 = 14f;
						Vector2 vector10 = new Vector2(base.npc.position.X + 39f, base.npc.position.Y + 91f);
						Vector2 vector11 = new Vector2(base.npc.position.X + 81f, base.npc.position.Y + 91f);
						int damage2 = 30;
						int type2 = base.mod.ProjectileType("MACELaser1");
						float rotation2 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
						int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
						int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
						Main.projectile[num56].netUpdate = true;
						Main.projectile[num57].netUpdate = true;
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
							float Speed3 = 14f;
							Vector2 vector12 = new Vector2(base.npc.position.X + 39f, base.npc.position.Y + 91f);
							Vector2 vector13 = new Vector2(base.npc.position.X + 81f, base.npc.position.Y + 91f);
							int damage3 = 30;
							int type3 = base.mod.ProjectileType("MACELaser1");
							float rotation3 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
							int num58 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
							int num59 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
							Main.projectile[num58].netUpdate = true;
							Main.projectile[num59].netUpdate = true;
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
						float Speed4 = 10f;
						Vector2 vector14 = new Vector2(base.npc.position.X + 50f, base.npc.position.Y + 54f);
						int damage4 = 30;
						int type4 = base.mod.ProjectileType("XenoShard3");
						float rotation4 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
						int p = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
						Main.projectile[p].netUpdate = true;
					}
					if (base.npc.ai[2] == 140f)
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
					if (base.npc.ai[2] == 170f)
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
					if (base.npc.ai[2] == 200f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut3 = 8;
						for (int k = 0; k < pieCut3; k++)
						{
							int projID3 = Projectile.NewProjectile(base.npc.position.X + 39f, base.npc.position.Y + 91f, 0f, 0f, base.mod.ProjectileType("MACELaser1"), 30, 3f, 255, 0f, 0f);
							Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(7f, 0f), (float)k / (float)pieCut3 * 6.28f);
							Main.projectile[projID3].netUpdate = true;
						}
					}
					if (base.npc.ai[2] >= 320f && base.npc.ai[2] <= 360f)
					{
						base.npc.ai[3] += 1f;
						if (base.npc.ai[3] == 4f)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed5 = 14f;
							Vector2 vector15 = new Vector2(base.npc.position.X + 39f, base.npc.position.Y + 91f);
							int damage5 = 30;
							int type5 = base.mod.ProjectileType("MACELaser1");
							float rotation5 = (float)Math.Atan2((double)(vector15.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector15.X - (player.position.X + (float)player.width * 0.5f)));
							int num60 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
							Main.projectile[num60].netUpdate = true;
						}
						if (base.npc.ai[3] == 8f)
						{
							Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed6 = 14f;
							int damage6 = 30;
							Vector2 vector16 = new Vector2(base.npc.position.X + 81f, base.npc.position.Y + 91f);
							int type6 = base.mod.ProjectileType("MACELaser1");
							float rotation6 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
							int num61 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
							Main.projectile[num61].netUpdate = true;
						}
						if (base.npc.ai[3] >= 12f)
						{
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 320f)
					{
						int p2 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(3f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int p3 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-3f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
						Main.projectile[p3].netUpdate = true;
					}
					if (base.npc.ai[2] == 330f)
					{
						int p4 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(6f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int p5 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-6f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[p4].netUpdate = true;
						Main.projectile[p5].netUpdate = true;
					}
					if (base.npc.ai[2] == 340f)
					{
						int p6 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(9f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int p7 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-9f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[p6].netUpdate = true;
						Main.projectile[p7].netUpdate = true;
					}
					if (base.npc.ai[2] == 350f)
					{
						int p8 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(12f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int p9 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-12f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[p8].netUpdate = true;
						Main.projectile[p9].netUpdate = true;
					}
					if (base.npc.ai[2] == 360f)
					{
						int p10 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(15f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						int p11 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y - 28f), new Vector2(-15f, 0f), base.mod.ProjectileType("MACEOrb1"), 0, 0f, 255, 0f, 0f);
						Main.projectile[p10].netUpdate = true;
						Main.projectile[p11].netUpdate = true;
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
						int minion2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist1"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[minion2].netUpdate = true;
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
						int minion3 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist3"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[minion3].netUpdate = true;
						int minion4 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("MACEProjectFist4"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[minion4].netUpdate = true;
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
