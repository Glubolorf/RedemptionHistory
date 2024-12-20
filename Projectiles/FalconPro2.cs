using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class FalconPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Earthquake BOOM!");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 92;
			base.projectile.height = 12;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
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
				for (int i = 0; i < 50; i++)
				{
					int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 0, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
		}
	}
}
