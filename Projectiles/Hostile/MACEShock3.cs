using System;

namespace Redemption.Projectiles.Hostile
{
	public class MACEShock3 : MACEShock1
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flaming Wave");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			this.proType = 2;
			this.offsetLeft = true;
		}
	}
}
