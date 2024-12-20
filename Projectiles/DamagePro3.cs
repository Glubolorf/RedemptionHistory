using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DamagePro3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("a swift thrust");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 46;
			base.projectile.height = 20;
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
