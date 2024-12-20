using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class ExBarrelPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Explosive Barrel");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
			base.projectile.thrown = true;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			base.projectile.Kill();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 20; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 78, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			if (Main.myPlayer == base.projectile.owner)
			{
				for (int j = 0; j < 3; j++)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(4 + Main.rand.Next(0, 8)), 400, 26, 0f, base.projectile.owner, 0f, 0f);
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}
	}
}
