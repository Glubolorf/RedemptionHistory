using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Hostile
{
	public class BBBone1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone");
		}

		public override void SetDefaults()
		{
			base.projectile.hostile = true;
			base.projectile.width = 20;
			base.projectile.height = 20;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 120;
		}

		public override bool PreAI()
		{
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.7f;
			return true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}
	}
}
