using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class RSlimeSpikePro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Spike");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(605);
			this.aiType = 605;
			base.projectile.timeLeft = 200;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 273, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num].velocity *= 1.6f;
			}
			Projectile.NewProjectile(base.projectile.Top, base.projectile.velocity, base.mod.ProjectileType("RSlimeSpikePro2"), base.projectile.damage, 2f, base.projectile.owner, 0f, 1f);
		}
	}
}
