using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class Plant11 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corpse Flower");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetSafeDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 300;
		}

		public override bool CheckNativeTerrain()
		{
			return Main.bloodMoon;
		}

		protected override void PlantAI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				if (this.IsOnNativeTerrain)
				{
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 5)
					{
						base.projectile.frame = 4;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 4)
					{
						base.projectile.frame = 3;
					}
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] % 60f == 0f && base.projectile.frame >= 3)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X + 24f, base.projectile.Center.Y), base.projectile.velocity, ModContent.ProjectileType<PollenCloud4>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			}
			if (base.projectile.frame >= 3 && this.IsOnNativeTerrain && Main.rand.Next(10) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), base.projectile.velocity, ModContent.ProjectileType<FlyPro>(), 4, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 5, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}
	}
}
