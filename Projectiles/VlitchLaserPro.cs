using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class VlitchLaserPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Laser");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(100);
			this.aiType = 100;
		}
	}
}
