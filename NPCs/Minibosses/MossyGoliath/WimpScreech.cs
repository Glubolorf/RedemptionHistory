using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	public class WimpScreech : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Roar");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 80;
			base.projectile.height = 80;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.ranged = true;
			base.projectile.penetrate = 4;
			base.projectile.timeLeft = 120;
			base.projectile.alpha = 50;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (this.originalVelocity == Vector2.Zero)
			{
				this.originalVelocity = base.projectile.velocity;
			}
			base.projectile.alpha += 4;
			base.projectile.scale += 0.04f;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 5;
			if (Main.rand.Next(5) == 0)
			{
				target.AddBuff(31, 200, false);
			}
		}

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
