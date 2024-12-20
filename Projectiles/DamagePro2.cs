using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DamagePro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("a swift slash");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 36;
			base.projectile.height = 34;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 6;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
		}
	}
}
