using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MartianForcefieldPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Martian Forcefield");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 280;
			base.projectile.height = 280;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 100;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.rotation += 0.04f;
			if (base.projectile.localAI[0] >= 600f)
			{
				base.projectile.Kill();
			}
			IEnumerable<Projectile> enumerable = Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox));
			foreach (Projectile projectile in enumerable)
			{
				if (base.projectile != projectile && !projectile.friendly && !projectile.minion && projectile.velocity.X != 0f && projectile.velocity.Y != 0f)
				{
					projectile.velocity.X = -projectile.velocity.X;
					projectile.velocity.Y = -projectile.velocity.Y;
				}
			}
			if (Main.myPlayer == base.projectile.owner)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active && !player.dead && base.projectile.getRect().Intersects(player.getRect()))
					{
						player.AddBuff(base.mod.BuffType("MartianShieldBuff"), 30, false);
						return;
					}
				}
			}
		}
	}
}
