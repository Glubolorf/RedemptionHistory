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
				for (int i = 0; i < 35; i++)
				{
					int num = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num].velocity *= 3f;
					Main.dust[num].noGravity = true;
				}
			}
		}

		public override bool CheckDead()
		{
			base.npc.SetDefaults(base.mod.NPCType("XenomiteCrystalPhase2"), -1f);
			Main.NewText("The crystal has shattered...", Color.ForestGreen.R, Color.ForestGreen.G, Color.ForestGreen.B, false);
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
			Player player = Main.player[base.npc.target];
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
				for (int i = 0; i < 25; i++)
				{
					int num = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num].velocity *= 3f;
					Main.dust[num].noGravity = true;
				}
			}
			if (this.beginFight)
			{
				base.npc.ai[1] += 1f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[1] == 280f)
				{
					float num2 = 8f;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num3 = 18;
					int num4 = base.mod.ProjectileType("ShardShot1");
					float num5 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num6 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
					int num7 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0) + -1f, (float)(Math.Sin((double)num5) * (double)num2 * -1.0) + -1f, num4, num3, 0f, 0, 0f, 0f);
					int num8 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0) + 1f, (float)(Math.Sin((double)num5) * (double)num2 * -1.0) + 1f, num4, num3, 0f, 0, 0f, 0f);
					Main.projectile[num6].netUpdate = true;
					Main.projectile[num7].netUpdate = true;
					Main.projectile[num8].netUpdate = true;
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
					{
						int num9 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0) + -2f, (float)(Math.Sin((double)num5) * (double)num2 * -1.0) + -1f, num4, num3, 0f, 0, 0f, 0f);
						int num10 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0) + 2f, (float)(Math.Sin((double)num5) * (double)num2 * -1.0) + 1f, num4, num3, 0f, 0, 0f, 0f);
						Main.projectile[num9].netUpdate = true;
						Main.projectile[num10].netUpdate = true;
					}
				}
				if (Main.expertMode)
				{
					if (base.npc.ai[1] >= 390f)
					{
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
						{
							for (int j = 0; j < 12; j++)
							{
								int num11 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("ShardShot2"), 40, 3f, 255, 0f, 0f);
								Main.projectile[num11].netUpdate = true;
							}
						}
						else
						{
							for (int k = 0; k < 8; k++)
							{
								int num12 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("ShardShot2"), 40, 3f, 255, 0f, 0f);
								Main.projectile[num12].netUpdate = true;
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
					float num13 = 12f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num14 = 17;
					int num15 = base.mod.ProjectileType("XenomiteFragmentPro");
					float num16 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num17 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num16) * (double)num13 * -1.0), (float)(Math.Sin((double)num16) * (double)num13 * -1.0), num15, num14, 0f, 0, 0f, 0f);
					Main.projectile[num17].netUpdate = true;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.life < 4000 && base.npc.life >= 2000)
				{
					if (Main.rand.Next(150) == 0)
					{
						int num18 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num18].netUpdate = true;
					}
				}
				else if (base.npc.life < 2000 && Main.rand.Next(100) == 0)
				{
					int num19 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num19].netUpdate = true;
				}
				if (base.npc.life < 4000)
				{
					this.customAI[0] += 1f;
					if (this.customAI[0] == 600f)
					{
						int num20 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(0f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
						int num21 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(0f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
						int num22 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
						int num23 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
						Main.projectile[num20].netUpdate = true;
						Main.projectile[num21].netUpdate = true;
						Main.projectile[num22].netUpdate = true;
						Main.projectile[num23].netUpdate = true;
						this.customAI[0] = 0f;
					}
				}
				if (base.npc.life < 3000 && this.customAI[0] == 300f)
				{
					int num24 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
					int num25 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
					int num26 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
					int num27 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 17, 3f, 255, 0f, 0f);
					Main.projectile[num24].netUpdate = true;
					Main.projectile[num25].netUpdate = true;
					Main.projectile[num26].netUpdate = true;
					Main.projectile[num27].netUpdate = true;
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
