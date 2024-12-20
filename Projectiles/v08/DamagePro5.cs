using System;
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
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && !proj.friendly && !proj.minion && proj.velocity.X != 0f && proj.velocity.Y != 0f)
				{
					if (proj.penetrate == 1)
					{
						proj.penetrate = 2;
					}
					proj.friendly = true;
					proj.hostile = false;
					proj.velocity.X = -proj.velocity.X * 1.5f;
					proj.velocity.Y = -proj.velocity.Y * 1.5f;
				}
			}
		}
	}
}
