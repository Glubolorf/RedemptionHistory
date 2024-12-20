using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class Fog2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fog");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 190;
			base.projectile.height = 70;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 220;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] <= 110f)
			{
				base.projectile.alpha--;
			}
			if (base.projectile.localAI[0] > 110f)
			{
				base.projectile.alpha++;
			}
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && !player.dead && base.projectile.getRect().Intersects(player.getRect()))
				{
					player.AddBuff(80, 60, false);
					return;
				}
			}
		}
	}
}
