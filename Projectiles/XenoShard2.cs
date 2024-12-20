using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XenoShard2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/DruidProjectiles/Plants/XenoShard";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xeno Piece");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 14;
			base.projectile.penetrate = 1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 35;
			base.projectile.timeLeft = 60;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 74, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}
	}
}
