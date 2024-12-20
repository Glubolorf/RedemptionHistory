using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class DamagePro5 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("a mighty bash");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 60;
			base.projectile.height = 54;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 15;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			IEnumerable<Projectile> enumerable = Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox));
			foreach (Projectile projectile in enumerable)
			{
				if (base.projectile != projectile && !projectile.friendly && !projectile.minion && projectile.velocity.X != 0f && projectile.velocity.Y != 0f)
				{
					if (projectile.penetrate == 1)
					{
						projectile.penetrate = 2;
					}
					projectile.friendly = true;
					projectile.hostile = false;
					projectile.velocity.X = -projectile.velocity.X * 1.5f;
					projectile.velocity.Y = -projectile.velocity.Y * 1.5f;
				}
			}
		}
	}
}
