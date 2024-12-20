using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class HolyGroundPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Earthquake BOOM!");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 276;
			base.projectile.height = 36;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.melee = true;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 2;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			if (base.projectile.localAI[0] == 1f)
			{
				Main.PlaySound(SoundID.Item89, base.projectile.position);
				for (int i = 0; i < 90; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 0, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 2.9f;
				}
			}
		}
	}
}
