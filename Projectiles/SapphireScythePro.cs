using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SapphireScythePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sapphire Scythe");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(45);
			this.aiType = 45;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 120;
			base.projectile.knockBack = 0f;
			base.projectile.penetrate = 2;
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 41, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			}
		}
	}
}
