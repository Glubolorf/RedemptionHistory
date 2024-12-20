using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class GreenGasPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Gas");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 40;
			base.projectile.height = 40;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 200;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.04f;
			if (base.projectile.localAI[0] >= 120f)
			{
				base.projectile.Kill();
			}
		}
	}
}
