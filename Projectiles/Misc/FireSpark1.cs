using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class FireSpark1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spark");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 10;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.height, base.projectile.width, 6, base.projectile.velocity.X, base.projectile.velocity.Y, 200, default(Color), 1.5f);
			dust.velocity += base.projectile.velocity * 0.3f;
			dust.velocity *= 0.2f;
			dust.noGravity = false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(24, 300, false);
		}
	}
}
