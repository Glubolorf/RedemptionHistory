using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class CorruptSoul2 : ModProjectile
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
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corruption Soul");
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 41, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override bool PreAI()
		{
			int DustID2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, 41, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			Main.dust[DustID2].noGravity = true;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 5f)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 41, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
			}
			int Num3 = 1;
			if (base.projectile.position.X + (float)(base.projectile.width / 2) < Main.player[base.projectile.owner].position.X + (float)Main.player[base.projectile.owner].width)
			{
				Num3 = -1;
			}
			Vector2 Vector = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num = Main.player[base.projectile.owner].position.X + (float)(Main.player[base.projectile.owner].width / 2) + (float)(Num3 * 180) - Vector.X;
			float Num4 = Main.player[base.projectile.owner].position.Y + (float)(Main.player[base.projectile.owner].height / 2) - Vector.Y;
			Math.Sqrt((double)(num * num + Num4 * Num4));
			float Num5 = base.projectile.position.X + (float)(base.projectile.width / 2) - Main.player[base.projectile.owner].position.X - (float)(Main.player[base.projectile.owner].width / 2);
			float Num6 = (float)Math.Atan2((double)(base.projectile.position.Y + (float)base.projectile.height - 59f - Main.player[base.projectile.owner].position.Y - (float)(Main.player[base.projectile.owner].height / 2)), (double)Num5) + 1.57f;
			if (Num6 < 0f)
			{
				Num6 += 6.283f;
			}
			else if ((double)Num6 > 6.283)
			{
				Num6 -= 6.283f;
			}
			float Num7 = 0.15f;
			if (base.projectile.rotation < Num6)
			{
				if ((double)(Num6 - base.projectile.rotation) > 3.1415)
				{
					base.projectile.rotation -= Num7;
				}
				else
				{
					base.projectile.rotation += Num7;
				}
			}
			else if (base.projectile.rotation > Num6)
			{
				if ((double)(base.projectile.rotation - Num6) > 3.1415)
				{
					base.projectile.rotation += Num7;
				}
				else
				{
					base.projectile.rotation -= Num7;
				}
			}
			if (base.projectile.rotation > Num6 - Num7 && base.projectile.rotation < Num6 + Num7)
			{
				base.projectile.rotation = Num6;
			}
			if (base.projectile.rotation < 0f)
			{
				base.projectile.rotation += 6.283f;
			}
			else if ((double)base.projectile.rotation > 6.283)
			{
				base.projectile.rotation -= 6.283f;
			}
			if (base.projectile.rotation > Num6 - Num7 && base.projectile.rotation < Num6 + Num7)
			{
				base.projectile.rotation = Num6;
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
			Vector2 direction = Main.player[base.projectile.owner].Center - base.projectile.Center;
			direction.Normalize();
			direction *= 9f;
			Player player = Main.player[base.projectile.owner];
			double rad = (double)base.projectile.ai[1] / 2.0 * 0.017453292519943295;
			double dist = 100.0;
			base.projectile.position.X = player.Center.X - (float)((int)(Math.Cos(rad) * dist)) - (float)(base.projectile.width / 2);
			base.projectile.position.Y = player.Center.Y - (float)((int)(Math.Sin(rad) * dist)) - (float)(base.projectile.height / 2);
			base.projectile.ai[1] += 2f;
			return false;
		}
	}
}
