using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MACELaser1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Laser");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 6;
			base.projectile.height = 6;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 100;
			base.projectile.alpha = 0;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 2)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 8)
				{
					base.projectile.frame = 7;
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.1f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 74, 0f, 0f, 100, default(Color), 1.3f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}
	}
}
