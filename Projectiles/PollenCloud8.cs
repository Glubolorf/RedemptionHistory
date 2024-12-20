using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class PollenCloud8 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pollen Cloud");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 136;
			base.projectile.height = 132;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 170;
			base.projectile.timeLeft = 60;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 3;
			base.projectile.velocity *= 0.01f;
			base.projectile.rotation += 0.03f;
		}
	}
}
