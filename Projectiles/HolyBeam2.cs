using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class HolyBeam2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pure Beam");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(259);
			this.aiType = 259;
		}
	}
}
