using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant21 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Bell Flower");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 40;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 160;
		}

		protected override void PlantAI()
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
				if (num >= 8)
				{
					base.projectile.frame = 7;
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] % 30f == 0f && base.projectile.frame >= 3)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), base.projectile.velocity, ModContent.ProjectileType<PollenCloud10>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 262, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}
	}
}
