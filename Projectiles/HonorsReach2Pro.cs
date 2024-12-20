using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class HonorsReach2Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Honor's Reach");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(116);
			this.aiType = 116;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 200;
		}
	}
}
