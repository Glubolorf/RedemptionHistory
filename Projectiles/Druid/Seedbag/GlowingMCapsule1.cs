using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class GlowingMCapsule1 : DruidSeed
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Glowing Mushroom Capsule");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			this.plantID = ModContent.ProjectileType<GlowingMushrooms1>();
		}
	}
}
