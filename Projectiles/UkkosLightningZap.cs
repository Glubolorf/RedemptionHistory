using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class UkkosLightningZap : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ukko's Lightning");
			Main.projFrames[base.projectile.type] = 24;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 540;
			base.projectile.height = 42;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 63;
			base.projectile.scale *= 2f;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 21)
				{
					base.projectile.frame = 0;
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation = 1.5708f;
			if (base.projectile.localAI[0] > 63f)
			{
				base.projectile.Kill();
			}
		}
	}
}
