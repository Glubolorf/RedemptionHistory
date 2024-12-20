using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class Transition : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Transition");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 1000;
			base.projectile.height = 1000;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 500;
			base.projectile.scale = 1f;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.scale += 0.1f;
			if (base.projectile.localAI[0] == 1f)
			{
				base.projectile.alpha += 20;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
					return;
				}
			}
			else
			{
				base.projectile.alpha -= 2;
				if (base.projectile.alpha <= 0)
				{
					base.projectile.localAI[0] = 1f;
				}
			}
		}
	}
}
