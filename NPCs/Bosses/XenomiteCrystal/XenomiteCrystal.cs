using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.XenomiteCrystal
{
	[AutoloadBossHead]
	public class XenomiteCrystal : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Crystal");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 164;
			base.npc.height = 82;
			base.npc.friendly = false;
			base.npc.damage = 34;
			base.npc.defense = 10;
			base.npc.lifeMax = 4500;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 10;
			base.npc.boss = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossXeno1");
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			this.aiType = 34;
			this.animationType = 34;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				for (int num67 = 0; num67 < 35; num67++)
				{
					int num68 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num68].velocity *= 3f;
					Main.dust[num68].noGravity = true;
				}
			}
		}

		public override bool CheckDead()
		{
			base.npc.SetDefaults(base.mod.NPCType("XenomiteCrystalPhase2"), -1f);
			if (!RedeConfigClient.Instance.NoBossText)
			{
				Main.NewText("The crystal has shattered...", Color.ForestGreen.R, Color.ForestGreen.G, Color.ForestGreen.B, false);
			}
			return false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
			}
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			base.npc.ai[0] += 1f;
			Player P = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.ai[3] += 1f;
			if (base.npc.ai[3] <= 120f)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.alpha -= 2;
				base.npc.dontTakeDamage = true;
			}
			if (base.npc.ai[3] > 120f)
			{
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
			}
			if (base.npc.ai[3] == 120f)
			{
				for (int num67 = 0; num67 < 25; num67++)
				{
					int num68 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num68].velocity *= 3f;
					Main.dust[num68].noGravity = true;
				}
			}
			if (this.beginFight)
			{
				base.npc.ai[1] += 1f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[1] == 280f)
				{
					float Speed = 8f;
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage = 18;
					int type = base.mod.ProjectileType("ShardShot1");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector8.X - (P.position.X + (float)P.width * 0.5f)));
					int num69 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					int num70 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + -1f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + -1f, type, damage, 0f, 0, 0f, 0f);
					int num71 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + 1f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + 1f, type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num69].netUpdate = true;
					Main.projectile[num70].netUpdate = true;
					Main.projectile[num71].netUpdate = true;
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
					{
						int num72 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + -2f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + -1f, type, damage, 0f, 0, 0f, 0f);
						int num73 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + 2f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + 1f, type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num72].netUpdate = true;
						Main.projectile[num73].netUpdate = true;
					}
				}
				if (Main.expertMode)
				{
					if (base.npc.ai[1] >= 390f)
					{
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
						{
							for (int i = 0; i < 12; i++)
							{
								int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("ShardShot2"), 17, 3f, 255, 0f, 0f);
								Main.projectile[p].netUpdate = true;
							}
						}
						else
						{
							for (int j = 0; j < 8; j++)
							{
								int p2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("ShardShot2"), 17, 3f, 255, 0f, 0f);
								Main.projectile[p2].netUpdate = true;
							}
						}
						base.npc.ai[1] = 0f;
					}
				}
				else if (base.npc.ai[1] >= 290f)
				{
					base.npc.ai[1] = 0f;
				}
				if (base.npc.ai[2] >= 300f)
				{
					float Speed2 = 12f;
					Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage2 = 17;
					int type2 = base.mod.ProjectileType("XenomiteFragmentPro");
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector9.X - (P.position.X + (float)P.width * 0.5f)));
					int num74 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num74].netUpdate = true;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.life < 4000 && base.npc.life >= 2000)
				{
					if (Main.rand.Next(150) == 0)
					{
						int Minion = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion].netUpdate = true;
					}
				}
				else if (base.npc.life < 2000 && Main.rand.Next(100) == 0)
				{
					int Minion2 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion2].netUpdate = true;
				}
				if (base.npc.life < 4000)
				{
					this.customAI[0] += 1f;
					if (this.customAI[0] == 600f)
					{
						int p3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(0f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
						int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(0f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
						int p5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
						int p6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
						Main.projectile[p3].netUpdate = true;
						Main.projectile[p4].netUpdate = true;
						Main.projectile[p5].netUpdate = true;
						Main.projectile[p6].netUpdate = true;
						this.customAI[0] = 0f;
					}
				}
				if (base.npc.life < 3000 && this.customAI[0] == 300f)
				{
					int p7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
					int p8 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
					int p9 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
					int p10 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
					Main.projectile[p7].netUpdate = true;
					Main.projectile[p8].netUpdate = true;
					Main.projectile[p9].netUpdate = true;
					Main.projectile[p10].netUpdate = true;
				}
			}
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("PuriumFlame"), 0f, 0f, 0, default(Color), 1f);
			}
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

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.beginFight;
		}

		private Player player;

		public float[] customAI = new float[1];

		private bool beginFight;
	}
}
