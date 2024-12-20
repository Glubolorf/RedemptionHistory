using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant1 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plant");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 56;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
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
					if (num >= 8)
					{
						base.projectile.frame = 7;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 7)
					{
						base.projectile.frame = 6;
					}
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] % 30f == 0f && base.projectile.frame >= 6)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 10f), base.projectile.velocity, base.mod.ProjectileType("PollenCloud1"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			}
			if (base.projectile.localAI[0] % 40f == 0f && base.projectile.frame >= 6 && this.IsOnNativeTerrain)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X + 8f, base.projectile.Center.Y - 16f), base.projectile.velocity, base.mod.ProjectileType("DayPulse"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}
	}
}
