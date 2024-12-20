using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class IceShield2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ice Shield");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 50;
			base.projectile.height = 50;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 140;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (!player.HasBuff(base.mod.BuffType("NatureGuardian26Buff")) || !modPlayer.iceShield)
			{
				this.deadTimer++;
				if (this.deadTimer <= 20)
				{
					base.projectile.alpha -= 5;
				}
				if (this.deadTimer > 20)
				{
					base.projectile.alpha += 5;
				}
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.2f / 255f);
			base.projectile.position.X = player.Center.X - 16f;
			base.projectile.position.Y = player.Center.Y - 21f;
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
			if (player.dead)
			{
				modPlayer.natureGuardian26 = false;
			}
			if (modPlayer.natureGuardian26 || modPlayer.iceShield)
			{
				base.projectile.timeLeft = 60;
			}
		}

		private int deadTimer;
	}
}
