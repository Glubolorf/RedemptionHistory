using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KSExitPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III");
			Main.projFrames[base.projectile.type] = 14;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 42;
			base.projectile.height = 106;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
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
				if (num >= 14)
				{
					base.projectile.frame = 12;
				}
			}
			if (base.projectile.frame >= 10)
			{
				Projectile projectile3 = base.projectile;
				projectile3.velocity.Y = projectile3.velocity.Y - 1f;
			}
		}
	}
}
