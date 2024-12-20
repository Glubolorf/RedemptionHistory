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
				Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
			}
			if (this.timer == 130)
			{
				float num5 = 10f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num6 = 35;
				int num7 = base.mod.ProjectileType("SnowyBall2");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num8 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num8) * (double)num5 * -1.0), (float)(Math.Sin((double)num8) * (double)num5 * -1.0), num7, num6, 0f, 0, 0f, 0f);
			}
			if (this.timer == 140)
			{
				float num9 = 12f;
				Vector2 vector3;
				vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num10 = 35;
				int num11 = base.mod.ProjectileType("SnowyBall2");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num12 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0), (float)(Math.Sin((double)num12) * (double)num9 * -1.0), num11, num10, 0f, 0, 0f, 0f);
			}
			if (this.timer == 150)
			{
				float num13 = 14f;
				Vector2 vector4;
				vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num14 = 35;
				int num15 = base.mod.ProjectileType("SnowyBall2");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
				float num16 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num16) * (double)num13 * -1.0), (float)(Math.Sin((double)num16) * (double)num13 * -1.0), num15, num14, 0f, 0, 0f, 0f);
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
					int num17 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 113, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num17].velocity *= 3f;
					Main.dust[num17].noGravity = true;
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
					int num18 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 113, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num18].velocity *= 3f;
					Main.dust[num18].noGravity = true;
				}
			}
			if (Math.Abs(base.npc.position.X - Main.player[base.npc.target].position.X) + Math.Abs(base.npc.position.Y - Main.player[base.npc.target].position.Y) > 2000f)
			{
				base.npc.ai[0] = 650f;
			}
			if (base.npc.ai[0] >= 650f && Main.netMode != 1)
			{
				base.npc.ai[0] = 1f;
				int num19 = (int)Main.player[base.npc.target].position.X / 16;
				int num20 = (int)Main.player[base.npc.target].position.Y / 16;
				int num21 = (int)base.npc.position.X / 16;
				int num22 = (int)base.npc.position.Y / 16;
				int num23 = 40;
				int num24 = 0;
				for (int k = 0; k < 100; k++)
				{
					num24++;
					int num25 = Main.rand.Next(num19 - num23, num19 + num23);
					int num26 = Main.rand.Next(num20 - num23, num20 + num23);
					for (int l = num26; l < num20 + num23; l++)
					{
						if ((num25 < num19 - 12 || num25 > num19 + 12) && (l < num22 - 1 || l > num22 + 1 || num25 < num21 - 1 || num25 > num21 + 1) && Main.tile[num25, l].nactive())
						{
							bool flag = true;
							if (Main.tile[num25, l - 1].lava())
							{
								flag = false;
							}
							if (flag && Main.tileSolid[(int)Main.tile[num25, l].type] && !Collision.SolidTiles(num25 - 1, num25 + 1, l - 4, l - 1))
							{
								base.npc.ai[1] = 20f;
								base.npc.ai[2] = (float)num25;
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
