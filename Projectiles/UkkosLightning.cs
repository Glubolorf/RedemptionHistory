﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class UkkosLightning : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ukko's Lightning");
			Main.projFrames[base.projectile.type] = 24;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 63;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 24)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation = 1.5708f;
			if (base.projectile.localAI[0] > 63f)
			{
				base.projectile.Kill();
			}
			if (base.projectile.localAI[0] == 1f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 20f, base.projectile.position.Y - 256f), base.projectile.velocity, base.mod.ProjectileType("UkkosLightningZap"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 36f)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap1").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 12f, base.projectile.position.Y + 12f), base.projectile.velocity, base.mod.ProjectileType("UkkosLightningZoop"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
		}
	}
}
