using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class DamagePro6 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("BOOM!");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 400;
			base.projectile.height = 400;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
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
