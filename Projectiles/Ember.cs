using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class Ember : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ember");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.magic = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 50;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 20, 1f, 0f);
				base.projectile.localAI[0] = 1f;
			}
			int num = 8;
			int num2 = Dust.NewDust(new Vector2(base.projectile.position.X + (float)num + 6f, base.projectile.position.Y + (float)num), base.projectile.width - num * 2, base.projectile.height - num * 2, 66, 0f, 0f, 0, new Color(255, 116, 0), 1.5f);
			Main.dust[num2].velocity *= 0.5f;
			Main.dust[num2].velocity += base.projectile.velocity * 0.5f;
			Main.dust[num2].noGravity = true;
			Main.dust[num2].noLight = false;
			Main.dust[num2].scale = 2f;
		}
	}
}
