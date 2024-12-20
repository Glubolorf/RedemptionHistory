using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XeniumDischarge2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Discharge");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 400;
			base.projectile.height = 400;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 3;
		}
	}
}
