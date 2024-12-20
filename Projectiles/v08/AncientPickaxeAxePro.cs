using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class AncientPickaxeAxePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Pickaxe Axe");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(272);
			this.aiType = 272;
			base.projectile.width = 36;
			base.projectile.height = 36;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}
	}
}
