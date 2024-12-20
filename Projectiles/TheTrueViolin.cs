using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class TheTrueViolin : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Violin's Song");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(78);
			this.aiType = 78;
			base.projectile.penetrate = 4;
			base.projectile.melee = true;
		}
	}
}
