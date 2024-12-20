using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class RainbowCatPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Legendary Rainbow Cat");
			Main.projFrames[base.projectile.type] = 17;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 42;
			base.projectile.height = 46;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.alpha = 20;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 17)
				{
					base.projectile.frame = 7;
				}
			}
			if (base.projectile.localAI[0] > 21f)
			{
				if (Main.rand.Next(3) == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-16 + Main.rand.Next(0, 33)), (float)(-16 + Main.rand.Next(0, 33)), 502, base.projectile.damage, 3f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-16 + Main.rand.Next(0, 33)), (float)(-16 + Main.rand.Next(0, 33)), 502, base.projectile.damage, 3f, Main.myPlayer, 0f, 0f);
				}
				if (Main.rand.Next(50) == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-16 + Main.rand.Next(0, 33)), (float)(-16 + Main.rand.Next(0, 33)), 79, base.projectile.damage, 3f, Main.myPlayer, 0f, 0f);
				}
			}
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + 0f;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] > 300f)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 58, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 59, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 60, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 61, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 62, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 64, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 65, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 58, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 59, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 60, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 61, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 62, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 64, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 65, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
				base.projectile.Kill();
			}
		}
	}
}
