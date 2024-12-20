using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class Plant25 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Adamantite Lily");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetSafeDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 68;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
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
					if (num >= 7)
					{
						base.projectile.frame = 6;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 6)
					{
						base.projectile.frame = 5;
					}
				}
			}
			base.projectile.localAI[0] += 1f;
			if (this.IsOnNativeTerrain ? (base.projectile.localAI[0] % 9f == 0f) : (base.projectile.localAI[0] % 15f == 0f && base.projectile.frame >= 4))
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-12 + Main.rand.Next(0, 24)), (float)(-12 + Main.rand.Next(0, 14)), ModContent.ProjectileType<AdamantiteSparkPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}
	}
}
