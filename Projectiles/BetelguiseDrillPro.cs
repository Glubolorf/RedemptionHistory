using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class BetelguiseDrillPro : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.aiStyle = 20;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.hide = true;
			base.projectile.ownerHitCheck = true;
			base.projectile.melee = true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Betelguise");
		}
	}
}
