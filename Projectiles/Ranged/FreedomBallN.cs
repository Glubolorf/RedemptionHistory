using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class FreedomBallN : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Freedom Plasma Ball");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 100;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 130;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha += 10;
			base.projectile.scale += 0.3f;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}
	}
}
