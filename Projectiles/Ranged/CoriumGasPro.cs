using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class CoriumGasPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corium Gas");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 40;
			base.projectile.height = 40;
			base.projectile.penetrate = 5;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 200;
		}

		public override void AI()
		{
			base.projectile.alpha++;
			base.projectile.rotation += 0.04f;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}
	}
}
