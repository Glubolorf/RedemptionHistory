using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MACEOrbLaser2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Warning");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 385;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 150;
			base.projectile.timeLeft = 2;
		}
	}
}
