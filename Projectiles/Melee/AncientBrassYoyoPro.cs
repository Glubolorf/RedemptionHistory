using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class AncientBrassYoyoPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Yoyo");
		}

		public override void SetDefaults()
		{
			base.projectile.extraUpdates = 0;
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.aiStyle = 99;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			ProjectileID.Sets.YoyosLifeTimeMultiplier[base.projectile.type] = 7f;
			ProjectileID.Sets.YoyosMaximumRange[base.projectile.type] = 170f;
			ProjectileID.Sets.YoyosTopSpeed[base.projectile.type] = 11f;
		}
	}
}
