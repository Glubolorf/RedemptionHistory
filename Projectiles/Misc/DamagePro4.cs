using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class DamagePro4 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("a mighty blow");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 14;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 10;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
		}
	}
}
