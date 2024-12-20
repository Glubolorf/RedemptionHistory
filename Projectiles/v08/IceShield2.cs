using System;
using System.Linq;
using Redemption.Buffs;
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
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (!player.HasBuff(ModContent.BuffType<NatureGuardian26Buff>()))
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
			if (player.dead)
			{
				modPlayer.natureGuardian26 = false;
			}
			if (modPlayer.natureGuardian26)
			{
				base.projectile.timeLeft = 60;
			}
		}

		private int deadTimer;
	}
}
