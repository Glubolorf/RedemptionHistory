﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MidnightNebula : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Midnight Nebula");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 70;
			base.projectile.height = 70;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 150;
		}

		public override void AI()
		{
			int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 70, base.projectile.height + 70, 242, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 2f);
			Main.dust[num].noGravity = true;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = ((int)base.projectile.localAI[0] + 50) * 2;
			base.projectile.rotation += 0.06f;
			if (base.projectile.localAI[0] >= 120f)
			{
				base.projectile.Kill();
			}
		}
	}
}