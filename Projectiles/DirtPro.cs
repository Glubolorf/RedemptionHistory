using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DirtPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dirt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.melee = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}
	}
}
