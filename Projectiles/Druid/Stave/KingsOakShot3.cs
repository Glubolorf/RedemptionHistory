using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class KingsOakShot3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pine Needle");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(336);
			this.aiType = 336;
			base.projectile.magic = false;
			base.projectile.melee = false;
			base.projectile.ranged = false;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}
	}
}
