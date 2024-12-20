using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class VlitchLaserPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Laser");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(100);
			this.aiType = 100;
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			base.projectile.friendly = true;
			base.projectile.hostile = false;
		}
	}
}
