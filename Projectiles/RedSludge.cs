using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class RedSludge : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Red Sludge");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 30;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.75f;
			if (base.projectile.localAI[0] > 30f)
			{
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, ModContent.ProjectileType<HardenedBlackGloop>(), 10, 0f, base.projectile.owner, 0f, 1f);
			return true;
		}
	}
}
