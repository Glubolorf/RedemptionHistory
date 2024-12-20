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
			base.projectile.alpha = 60;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 63;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 21)
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
