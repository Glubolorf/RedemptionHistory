using System;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class SharpRockPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sharp Rock");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 50;
			base.projectile.height = 50;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 260;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}
	}
}
