﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class CaveAura : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cave Aura");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 594;
			base.projectile.height = 594;
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
			if (base.projectile.localAI[0] >= 900f)
			{
				base.projectile.Kill();
			}
			if (Main.myPlayer == base.projectile.owner)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active && !player.dead && base.projectile.getRect().Intersects(player.getRect()))
					{
						player.AddBuff(base.mod.BuffType("GraniteAuraBuff"), 1800, false);
						return;
					}
				}
			}
		}
	}
}
