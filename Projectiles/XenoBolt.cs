using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XenoBolt : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.magic = true;
			base.projectile.penetrate = 3;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 20, 1f, 0f);
				base.projectile.localAI[0] = 1f;
			}
			int num = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 74, 0f, 0f, 100, Color.White, 1.6f);
			Main.dust[num].velocity /= 2f;
		}
	}
}
