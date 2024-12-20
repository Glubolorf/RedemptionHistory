using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class PeaceKeeperShard : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Shard");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(94);
			this.aiType = 94;
			base.projectile.ranged = true;
			base.projectile.magic = false;
		}
	}
}
