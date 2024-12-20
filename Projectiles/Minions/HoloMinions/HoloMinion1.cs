using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions.HoloMinions
{
	public class HoloMinion1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holo Chicken");
			Main.projFrames[base.projectile.type] = 2;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.width = 28;
			base.projectile.height = 20;
			base.projectile.friendly = true;
			Main.projPet[base.projectile.type] = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 1f;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 100;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			this.aiType = 317;
		}

		public override void AI()
		{
			bool flag = base.projectile.type == base.mod.ProjectileType("HoloMinion1");
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (flag)
			{
				if (player.dead)
				{
					modPlayer.holoMinion = false;
				}
				if (modPlayer.holoMinion)
				{
					base.projectile.timeLeft = 2;
				}
			}
			for (int i = 0; i < 1000; i++)
			{
				if (i != base.projectile.whoAmI && Main.projectile[i].active && Main.projectile[i].owner == base.projectile.owner && Main.projectile[i].type == base.projectile.type && Math.Abs(base.projectile.position.X - Main.projectile[i].position.X) + Math.Abs(base.projectile.position.Y - Main.projectile[i].position.Y) < (float)base.projectile.width)
				{
					if (base.projectile.position.X < Main.projectile[i].position.X)
					{
						base.projectile.velocity.X = base.projectile.velocity.X - 0.05f;
					}
					else
					{
						base.projectile.velocity.X = base.projectile.velocity.X + 0.05f;
					}
					if (base.projectile.position.Y < Main.projectile[i].position.Y)
					{
						base.projectile.velocity.Y = base.projectile.velocity.Y - 0.05f;
					}
					else
					{
						base.projectile.velocity.Y = base.projectile.velocity.Y + 0.05f;
					}
				}
			}
			float num = base.projectile.position.X;
			float num2 = base.projectile.position.Y;
			float num3 = 900f;
			bool flag2 = false;
			if (base.projectile.ai[1] == 0f)
			{
				bool friendly = base.projectile.friendly;
			}
			if (base.projectile.ai[0] == 0f)
			{
				for (int j = 0; j < 200; j++)
				{
					if (Main.npc[j].CanBeChasedBy(base.projectile, false))
					{
						float num4 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
						float num5 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
						float num6 = Math.Abs(base.projectile.position.X + (float)(base.projectile.width / 2) - num4) + Math.Abs(base.projectile.position.Y + (float)(base.projectile.height / 2) - num5);
						if (num6 < num3 && Collision.CanHit(base.projectile.position, base.projectile.width, base.projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
						{
							num3 = num6;
							num = num4;
							num2 = num5;
							flag2 = true;
						}
					}
				}
			}
			else
			{
				base.projectile.tileCollide = false;
			}
			if (!flag2)
			{
				base.projectile.friendly = true;
				float num7 = 8f;
				if (base.projectile.ai[0] == 1f)
				{
					num7 = 12f;
				}
				Vector2 vector;
				vector..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
				float num8 = Main.player[base.projectile.owner].Center.X - vector.X;
				float num9 = Main.player[base.projectile.owner].Center.Y - vector.Y - 60f;
				float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
				if (num10 < 100f && base.projectile.ai[0] == 1f && !Collision.SolidCollision(base.projectile.position, base.projectile.width, base.projectile.height))
				{
					base.projectile.ai[0] = 0f;
				}
				if (num10 > 2000f)
				{
					base.projectile.position.X = Main.player[base.projectile.owner].Center.X - (float)(base.projectile.width / 2);
					base.projectile.position.Y = Main.player[base.projectile.owner].Center.Y - (float)(base.projectile.width / 2);
				}
				if (num10 > 70f)
				{
					num10 = num7 / num10;
					num8 *= num10;
					num9 *= num10;
					base.projectile.velocity.X = (base.projectile.velocity.X * 20f + num8) / 21f;
					base.projectile.velocity.Y = (base.projectile.velocity.Y * 20f + num9) / 21f;
				}
				else
				{
					if (base.projectile.velocity.X == 0f && base.projectile.velocity.Y == 0f)
					{
						base.projectile.velocity.X = -0.15f;
						base.projectile.velocity.Y = -0.05f;
					}
					base.projectile.velocity *= 1.01f;
				}
				base.projectile.friendly = false;
				base.projectile.rotation = base.projectile.velocity.X * 0.05f;
				base.projectile.frameCounter++;
				if (base.projectile.frameCounter >= 5)
				{
					base.projectile.frameCounter = 0;
					base.projectile.frame++;
				}
				if (base.projectile.frame >= 2)
				{
					base.projectile.frame = 0;
				}
				if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
				{
					base.projectile.spriteDirection = -base.projectile.direction;
					return;
				}
			}
			else
			{
				if (base.projectile.ai[1] == -1f)
				{
					base.projectile.ai[1] = 17f;
				}
				if (base.projectile.ai[1] > 0f)
				{
					base.projectile.ai[1] -= 1f;
				}
				if (base.projectile.ai[1] == 0f)
				{
					base.projectile.friendly = true;
					float num11 = 8f;
					Vector2 vector2;
					vector2..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
					float num12 = num - vector2.X;
					float num13 = num2 - vector2.Y;
					float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
					if (num14 < 100f)
					{
						num11 = 10f;
					}
					num14 = num11 / num14;
					num12 *= num14;
					num13 *= num14;
					base.projectile.velocity.X = (base.projectile.velocity.X * 14f + num12) / 15f;
					base.projectile.velocity.Y = (base.projectile.velocity.Y * 14f + num13) / 15f;
				}
				else
				{
					base.projectile.friendly = false;
					if (Math.Abs(base.projectile.velocity.X) + Math.Abs(base.projectile.velocity.Y) < 10f)
					{
						base.projectile.velocity *= 1.05f;
					}
				}
				base.projectile.rotation = base.projectile.velocity.X * 0.05f;
				if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
				{
					base.projectile.spriteDirection = -base.projectile.direction;
				}
			}
		}

		public override bool MinionContactDamage()
		{
			return true;
		}
	}
}
