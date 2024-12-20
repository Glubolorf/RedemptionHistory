using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class NightshadeMiniSkull : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Skull");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 41;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 27, 0f, 0f, 100, Color.White, 1f);
			Main.dust[dustID].velocity *= 0f;
			Main.dust[dustID].noLight = false;
			Main.dust[dustID].noGravity = true;
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y - 0.5f;
			if (base.projectile.localAI[0] >= 40f)
			{
				if (base.projectile.ai[0] == 1f)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<CloudNightshade3>(), 11, 3f, base.projectile.owner, 0f, 0f);
				}
				else
				{
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<CloudNightshade2>(), 11, 3f, base.projectile.owner, 0f, 0f);
				}
				for (int i = 0; i < 6; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 4; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}
	}
}
