using System;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class AkkaIslandWarning : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Warning");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 1696;
			base.projectile.height = 320;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 180;
			base.projectile.timeLeft = 600;
		}
	}
}
