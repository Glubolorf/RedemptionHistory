using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class InvPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nothingness");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 1;
			base.projectile.height = 1;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 40;
		}
	}
}
