using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Friendly
{
	public class HologramShip : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hologram");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] <= 300f)
			{
				Projectile projectile = base.projectile;
				int num = projectile.frameCounter + 1;
				projectile.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 4)
					{
						base.projectile.frame = 2;
					}
				}
			}
			else
			{
				Projectile projectile3 = base.projectile;
				int num = projectile3.frameCounter + 1;
				projectile3.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile4 = base.projectile;
					num = projectile4.frame + 1;
					projectile4.frame = num;
					if (num >= 7)
					{
						base.projectile.Kill();
					}
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0.6f / 255f);
		}
	}
}
