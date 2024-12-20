﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
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
				float num = 1f;
				if (base.projectile.ai[0] == 8f)
				{
					num = 0.25f;
				}
				else if (base.projectile.ai[0] == 9f)
				{
					num = 0.5f;
				}
				else if (base.projectile.ai[0] == 10f)
				{
					num = 0.75f;
				}
				base.projectile.ai[0] += 1f;
				int num2 = 92;
				if (Main.rand.Next(2) == 0)
				{
					for (int i = 0; i < 1; i++)
					{
						int num3 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, num2, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if (num2 == 92 && Main.rand.Next(3) == 0)
						{
							Main.dust[num3].noGravity = true;
							Main.dust[num3].scale *= 0.5f;
							Dust dust = Main.dust[num3];
							dust.velocity.X = dust.velocity.X * 2f;
							Dust dust2 = Main.dust[num3];
							dust2.velocity.Y = dust2.velocity.Y * 2f;
						}
						if (num2 == 92 && Main.rand.Next(6) == 0)
						{
							Main.dust[num3].noGravity = true;
							Main.dust[num3].scale *= 1f;
							Dust dust3 = Main.dust[num3];
							dust3.velocity.X = dust3.velocity.X * 2f;
							Dust dust4 = Main.dust[num3];
							dust4.velocity.Y = dust4.velocity.Y * 2f;
						}
						else
						{
							Main.dust[num3].scale *= 1.5f;
						}
						Dust dust5 = Main.dust[num3];
						dust5.velocity.X = dust5.velocity.X * 1.2f;
						Dust dust6 = Main.dust[num3];
						dust6.velocity.Y = dust6.velocity.Y * 1.2f;
						Main.dust[num3].scale *= num;
						if (num2 == 56)
						{
							Main.dust[num3].velocity += base.projectile.velocity;
							if (!Main.dust[num3].noGravity)
							{
								Main.dust[num3].velocity *= 0.5f;
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
			target.AddBuff(base.mod.BuffType("UltraFlameDebuff"), 400, false);
		}
	}
}
