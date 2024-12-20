using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	public class KeeperSoulCharge : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul Charge");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 34;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
			base.projectile.magic = true;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 4)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			Lighting.AddLight(base.projectile.Center, base.projectile.Opacity, base.projectile.Opacity, base.projectile.Opacity);
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + 3.1415927f;
			base.projectile.velocity = Utils.RotatedBy(base.projectile.velocity, (double)Utils.NextFloat(Main.rand, -0.1f, 0.1f), default(Vector2));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 180, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 4.4f;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(255, 255, 255, 0));
		}
	}
}
