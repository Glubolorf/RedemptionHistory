﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MudAura : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ring of Thorns");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 204;
			base.projectile.height = 204;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 220;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (!player.HasBuff(base.mod.BuffType("NatureGuardian11Buff")))
			{
				base.projectile.Kill();
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.rotation += 0.06f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.2f / 255f);
			base.projectile.position.X = player.Center.X - 102f;
			base.projectile.position.Y = player.Center.Y - 102f;
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.dead)
			{
				modPlayer.natureGuardian11 = false;
			}
			if (modPlayer.natureGuardian11)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}