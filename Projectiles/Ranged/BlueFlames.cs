using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class BlueFlames : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ultra-Heated Flames");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 12;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.ranged = true;
			base.projectile.penetrate = -1;
			base.projectile.extraUpdates = 3;
			base.projectile.timeLeft = 90;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.01f / 255f, (float)(255 - base.projectile.alpha) * 0.01f / 255f, (float)(255 - base.projectile.alpha) * 0.5f / 255f);
			if (base.projectile.timeLeft > 90)
			{
				base.projectile.timeLeft = 90;
			}
			if (base.projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				if (base.projectile.ai[0] == 8f)
				{
					num296 = 0.25f;
				}
				else if (base.projectile.ai[0] == 9f)
				{
					num296 = 0.5f;
				}
				else if (base.projectile.ai[0] == 10f)
				{
					num296 = 0.75f;
				}
				base.projectile.ai[0] += 1f;
				int num297 = 92;
				if (Main.rand.Next(2) == 0)
				{
					for (int num298 = 0; num298 < 1; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, num297, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if (num297 == 92 && Main.rand.Next(3) == 0)
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 0.5f;
							Dust dust = Main.dust[num299];
							dust.velocity.X = dust.velocity.X * 2f;
							Dust dust2 = Main.dust[num299];
							dust2.velocity.Y = dust2.velocity.Y * 2f;
						}
						if (num297 == 92 && Main.rand.Next(6) == 0)
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 1f;
							Dust dust3 = Main.dust[num299];
							dust3.velocity.X = dust3.velocity.X * 2f;
							Dust dust4 = Main.dust[num299];
							dust4.velocity.Y = dust4.velocity.Y * 2f;
						}
						else
						{
							Main.dust[num299].scale *= 1.5f;
						}
						Dust dust5 = Main.dust[num299];
						dust5.velocity.X = dust5.velocity.X * 1.2f;
						Dust dust6 = Main.dust[num299];
						dust6.velocity.Y = dust6.velocity.Y * 1.2f;
						Main.dust[num299].scale *= num296;
						if (num297 == 56)
						{
							Main.dust[num299].velocity += base.projectile.velocity;
							if (!Main.dust[num299].noGravity)
							{
								Main.dust[num299].velocity *= 0.5f;
							}
						}
					}
				}
			}
			else
			{
				base.projectile.ai[0] += 1f;
			}
			base.projectile.rotation += 0.3f * (float)base.projectile.direction;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 3;
			target.AddBuff(ModContent.BuffType<UltraFlameDebuff>(), 400, false);
		}
	}
}
