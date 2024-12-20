using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class Plant24 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Titanium Bloom");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 40;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 20)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + 1f;
			if (Main.rand.Next(15) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 8f, (float)(-12 + Main.rand.Next(0, 24)), (float)(-12 + Main.rand.Next(0, 14)), base.mod.ProjectileType("TitaniumSparkPro"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] > 160f)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 91, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 91, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 91, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 91, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 91, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 91, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				base.projectile.Kill();
			}
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
