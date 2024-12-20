using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class Plant29 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Golden Orange Tree");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 70;
			base.projectile.height = 80;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + 1f;
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 22f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 46f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 44f, base.projectile.position.Y + 40f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 56f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(100) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 60f, base.projectile.position.Y + 48f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] > 240f)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 10, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 10, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 10, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 10, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 10, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 10, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				base.projectile.Kill();
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0f)
			{
				base.projectile.velocity.X = oldVelocity.X * --0f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * --0f;
			}
			return false;
		}
	}
}
