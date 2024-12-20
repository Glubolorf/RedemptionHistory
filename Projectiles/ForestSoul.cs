using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class ForestSoul : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.friendly = true;
			base.projectile.penetrate = 5;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 1000;
			base.projectile.light = 0f;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 9;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Soul");
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num].velocity *= 1.4f;
			}
		}

		public override bool PreAI()
		{
			int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, 163, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			Main.dust[num].noGravity = true;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 5f)
			{
				for (int i = 0; i < 25; i++)
				{
					int num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num2].velocity *= 1.4f;
				}
			}
			int num3 = 1;
			if (base.projectile.position.X + (float)(base.projectile.width / 2) < Main.player[base.projectile.owner].position.X + (float)Main.player[base.projectile.owner].width)
			{
				num3 = -1;
			}
			Vector2 vector;
			vector..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num4 = Main.player[base.projectile.owner].position.X + (float)(Main.player[base.projectile.owner].width / 2) + (float)(num3 * 180) - vector.X;
			float num5 = Main.player[base.projectile.owner].position.Y + (float)(Main.player[base.projectile.owner].height / 2) - vector.Y;
			Math.Sqrt((double)(num4 * num4 + num5 * num5));
			float num6 = base.projectile.position.X + (float)(base.projectile.width / 2) - Main.player[base.projectile.owner].position.X - (float)(Main.player[base.projectile.owner].width / 2);
			float num7 = base.projectile.position.Y + (float)base.projectile.height - 59f - Main.player[base.projectile.owner].position.Y - (float)(Main.player[base.projectile.owner].height / 2);
			float num8 = (float)Math.Atan2((double)num7, (double)num6) + 1.57f;
			if (num8 < 0f)
			{
				num8 += 6.283f;
			}
			else if ((double)num8 > 6.283)
			{
				num8 -= 6.283f;
			}
			float num9 = 0.15f;
			if (base.projectile.rotation < num8)
			{
				if ((double)(num8 - base.projectile.rotation) > 3.1415)
				{
					base.projectile.rotation -= num9;
				}
				else
				{
					base.projectile.rotation += num9;
				}
			}
			else if (base.projectile.rotation > num8)
			{
				if ((double)(base.projectile.rotation - num8) > 3.1415)
				{
					base.projectile.rotation += num9;
				}
				else
				{
					base.projectile.rotation -= num9;
				}
			}
			if (base.projectile.rotation > num8 - num9 && base.projectile.rotation < num8 + num9)
			{
				base.projectile.rotation = num8;
			}
			if (base.projectile.rotation < 0f)
			{
				base.projectile.rotation += 6.283f;
			}
			else if ((double)base.projectile.rotation > 6.283)
			{
				base.projectile.rotation -= 6.283f;
			}
			if (base.projectile.rotation > num8 - num9 && base.projectile.rotation < num8 + num9)
			{
				base.projectile.rotation = num8;
			}
			base.projectile.frameCounter++;
			if (base.projectile.frameCounter >= 10)
			{
				base.projectile.frame++;
				base.projectile.frameCounter = 0;
				if (base.projectile.frame >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			Vector2 vector2 = Main.player[base.projectile.owner].Center - base.projectile.Center;
			vector2.Normalize();
			vector2 *= 9f;
			Player player = Main.player[base.projectile.owner];
			double num10 = (double)base.projectile.ai[1] / 2.0;
			double num11 = num10 * 0.017453292519943295;
			double num12 = 100.0;
			base.projectile.position.X = player.Center.X - (float)((int)(Math.Cos(num11) * num12)) - (float)(base.projectile.width / 2);
			base.projectile.position.Y = player.Center.Y - (float)((int)(Math.Sin(num11) * num12)) - (float)(base.projectile.height / 2);
			base.projectile.ai[1] += 2f;
			return false;
		}
	}
}
