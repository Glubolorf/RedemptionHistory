using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class DamagePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("a swift stab");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 12;
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
		}
	}
}
