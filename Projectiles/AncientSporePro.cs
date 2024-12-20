using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class AncientSporePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Spore");
			Main.projFrames[base.projectile.type] = 45;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.alpha = 60;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 45)
				{
					base.projectile.frame = 17;
				}
			}
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + 0f;
			base.projectile.rotation += 0.01f;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] > 255f)
			{
				int num = Main.rand.Next(12);
				if (num == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed1"), 40, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 1)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed3"), 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 2)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed4"), 110, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 3)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed5"), 290, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 4)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed6"), 110, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 5)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed8"), 60, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 6)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed9"), 40, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 7)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed10"), 100, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 8)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed13"), 60, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 9)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed14"), 130, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 10)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed15"), 77, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 11)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("Seed17"), 75, 1f, Main.myPlayer, 0f, 0f);
				}
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 262, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 262, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 262, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
				base.projectile.Kill();
			}
		}
	}
}
