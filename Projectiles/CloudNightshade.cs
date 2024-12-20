using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class CloudNightshade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Poisonous Vapor Cloud");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 28;
			base.projectile.penetrate = -1;
			base.projectile.magic = true;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.07f;
			if (Main.myPlayer == base.projectile.owner)
			{
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 10f, base.projectile.position.Y + 18f), new Vector2(0f, 0f), ModContent.ProjectileType<CloudNightshadeRain>(), 11, 3f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 14f), new Vector2(0f, 0f), ModContent.ProjectileType<CloudNightshadeRain>(), 11, 3f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 20f, base.projectile.position.Y + 20f), new Vector2(0f, 0f), ModContent.ProjectileType<CloudNightshadeRain>(), 11, 3f, base.projectile.owner, 0f, 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 40; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}
	}
}
