﻿using System;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class HealShroomCapsule1 : DruidSeed
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Heal Shroom Capsule");
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
			this.plantID = base.mod.ProjectileType("HealShroom1");
		}
	}
}
