using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class PlutoniumBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plutonium Beam");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.aiStyle = 0;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.ranged = true;
			base.projectile.extraUpdates = 100;
			base.projectile.timeLeft = 800;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] > 9f)
			{
				for (int num447 = 0; num447 < 4; num447++)
				{
					Vector2 vector33 = base.projectile.position;
					vector33 -= base.projectile.velocity * ((float)num447 * 0.25f);
					base.projectile.alpha = 255;
					int num448 = Dust.NewDust(vector33, base.projectile.width, base.projectile.height, 226, 0f, 0f, 200, default(Color), 1f);
					Main.dust[num448].position = vector33;
					Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Main.dust[num448].velocity *= 0.2f;
					Main.dust[num448].noGravity = true;
				}
			}
		}
	}
}
