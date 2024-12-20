using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class BarrelPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Barrel");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.friendly = false;
			base.projectile.penetrate = 1;
			base.projectile.hostile = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
			if (base.projectile.localAI[0] > 130f)
			{
				base.projectile.Kill();
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			base.projectile.Kill();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 20; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 78, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num].velocity *= 1.4f;
			}
			Projectile.NewProjectile(base.projectile.position.X + 12f, base.projectile.position.Y + 12f, (float)(-4 + Main.rand.Next(0, 8)), (float)(4 + Main.rand.Next(0, 8)), 686, 30, 4f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 12f, base.projectile.position.Y + 12f, (float)(-4 + Main.rand.Next(0, 8)), (float)(4 + Main.rand.Next(0, 8)), 686, 30, 4f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 12f, base.projectile.position.Y + 12f, (float)(-4 + Main.rand.Next(0, 8)), (float)(4 + Main.rand.Next(0, 8)), 686, 30, 4f, base.projectile.owner, 0f, 1f);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}
	}
}
