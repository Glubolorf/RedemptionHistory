using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class AncientBoulder : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Boulder");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(261);
			this.aiType = 261;
			base.projectile.width = 32;
			base.projectile.height = 34;
			base.projectile.magic = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}
	}
}
