using System;

namespace Redemption.Projectiles.Melee
{
	public class PNebulaWep3 : PiercingNebWeaponPro
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Piercing Nebula");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			this.proType = 2;
			this.offsetLeft = true;
		}
	}
}
