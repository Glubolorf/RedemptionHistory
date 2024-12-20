using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class TheKeeperPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadow Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.magic = true;
			base.projectile.penetrate = 2;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
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
			int num666 = 8;
			int num667 = Dust.NewDust(new Vector2(base.projectile.position.X + (float)num666 + 6f, base.projectile.position.Y + (float)num666), base.projectile.width - num666 * 2, base.projectile.height - num666 * 2, 66, 0f, 0f, 0, new Color(131, 0, 0), 1.5f);
			Main.dust[num667].velocity *= 0.5f;
			Main.dust[num667].velocity += base.projectile.velocity * 0.5f;
			Main.dust[num667].noGravity = true;
			Main.dust[num667].noLight = false;
			Main.dust[num667].scale = 2f;
		}
	}
}
