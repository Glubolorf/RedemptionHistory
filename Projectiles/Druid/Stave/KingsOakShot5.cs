using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class KingsOakShot5 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spore");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(567);
			this.aiType = 567;
			base.projectile.magic = false;
			base.projectile.melee = false;
			base.projectile.ranged = false;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}
	}
}
