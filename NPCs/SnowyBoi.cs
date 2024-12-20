using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class SnowyBoi : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chillblood Sorcerer");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.width = 34;
			base.npc.height = 42;
			base.npc.friendly = false;
			base.npc.damage = 55;
			base.npc.defense = 20;
			base.npc.lifeMax = 370;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = 60f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.behindTiles = true;
			this.animationType = 32;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SnowyBoiGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SnowyBoiGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SnowyBoiGore3"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 80, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 80, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 80, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 80, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 80, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.rand.Next(4) == 0)
			{
				base.npc.ai[0] = 650f;
				base.npc.TargetClosest(true);
			}
		}

		public override void AI()
		{
			this.timer++;
			base.npc.TargetClosest(true);
			base.npc.spriteDirection = base.npc.direction;
			Player player = Main.player[base.npc.target];
			if (this.timer == 120)
			{
				float num = 8f;
				Vector2 vector;
				vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num2 = 35;
				int num3 = base.mod.ProjectileType("SnowyBall2");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
				int num5 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				Main.projectile[num5].netUpdate = true;
			}
			if (this.timer == 130)
			{
				float num6 = 10f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num7 = 35;
				int num8 = base.mod.ProjectileType("SnowyBall2");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num9 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
				int num10 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num9) * (double)num6 * -1.0), (float)(Math.Sin((double)num9) * (double)num6 * -1.0), num8, num7, 0f, 0, 0f, 0f);
				Main.projectile[num10].netUpdate = true;
			}
			if (this.timer == 140)
			{
				float num11 = 12f;
				Vector2 vector3;
				vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num12 = 35;
				int num13 = base.mod.ProjectileType("SnowyBall2");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num14 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
				int num15 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num14) * (double)num11 * -1.0), (float)(Math.Sin((double)num14) * (double)num11 * -1.0), num13, num12, 0f, 0, 0f, 0f);
				Main.projectile[num15].netUpdate = true;
			}
			if (this.timer == 150)
			{
				float num16 = 14f;
				Vector2 vector4;
				vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num17 = 35;
				int num18 = base.mod.ProjectileType("SnowyBall2");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num19 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
				int num20 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num19) * (double)num16 * -1.0), (float)(Math.Sin((double)num19) * (double)num16 * -1.0), num18, num17, 0f, 0, 0f, 0f);
				Main.projectile[num20].netUpdate = true;
			}
			if (this.timer >= 160)
			{
				base.npc.ai[0] = 650f;
				base.npc.TargetClosest(true);
				this.timer = 0;
			}
			if (base.npc.ai[2] != 0f && base.npc.ai[3] != 0f)
			{
				Main.PlaySound(SoundID.Item8, base.npc.position);
				for (int i = 0; i < 25; i++)
				{
					int num21 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 113, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num21].velocity *= 3f;
					Main.dust[num21].noGravity = true;
				}
				base.npc.position.X = base.npc.ai[2] * 16f - (float)(base.npc.width / 2) + 8f;
				base.npc.position.Y = base.npc.ai[3] * 16f - (float)base.npc.height;
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.ai[2] = 0f;
				base.npc.ai[3] = 0f;
				Main.PlaySound(SoundID.Item8, base.npc.position);
				for (int j = 0; j < 25; j++)
				{
					int num22 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 113, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num22].velocity *= 3f;
					Main.dust[num22].noGravity = true;
				}
			}
			if (Math.Abs(base.npc.position.X - Main.player[base.npc.target].position.X) + Math.Abs(base.npc.position.Y - Main.player[base.npc.target].position.Y) > 2000f)
			{
				base.npc.ai[0] = 650f;
			}
			if (base.npc.ai[0] >= 650f && Main.netMode != 1)
			{
				base.npc.ai[0] = 1f;
				int num23 = (int)Main.player[base.npc.target].position.X / 16;
				int num24 = (int)Main.player[base.npc.target].position.Y / 16;
				int num25 = (int)base.npc.position.X / 16;
				int num26 = (int)base.npc.position.Y / 16;
				int num27 = 40;
				int num28 = 0;
				for (int k = 0; k < 100; k++)
				{
					num28++;
					int num29 = Main.rand.Next(num23 - num27, num23 + num27);
					int num30 = Main.rand.Next(num24 - num27, num24 + num27);
					for (int l = num30; l < num24 + num27; l++)
					{
						if ((num29 < num23 - 12 || num29 > num23 + 12) && (l < num26 - 1 || l > num26 + 1 || num29 < num25 - 1 || num29 > num25 + 1) && Main.tile[num29, l].nactive())
						{
							bool flag = true;
							if (Main.tile[num29, l - 1].lava())
							{
								flag = false;
							}
							if (flag && Main.tileSolid[(int)Main.tile[num29, l].type] && !Collision.SolidTiles(num29 - 1, num29 + 1, l - 4, l - 1))
							{
								base.npc.ai[1] = 20f;
								base.npc.ai[2] = (float)num29;
								base.npc.ai[3] = (float)l - 1f;
								break;
							}
						}
					}
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] > 0f)
			{
				base.npc.ai[1] -= 1f;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 161 && Main.hardMode && NPC.downedPlantBoss) ? 0.06f : 0f);
		}

		private int timer;

		private bool castingFrames;
	}
}
