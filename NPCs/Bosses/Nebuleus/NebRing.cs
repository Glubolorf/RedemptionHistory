using System;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class NebRing : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cosmic Ring");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 408;
			base.projectile.height = 408;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 254;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.06f;
			if (base.projectile.localAI[0] <= 120f)
			{
				base.projectile.alpha -= 2;
			}
			if (base.projectile.localAI[0] >= 240f)
			{
				base.projectile.alpha += 2;
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}
	}
}
