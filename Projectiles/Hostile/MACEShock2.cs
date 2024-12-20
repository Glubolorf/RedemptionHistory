using System;

namespace Redemption.Projectiles.Hostile
{
	public class MACEShock2 : MACEShock1
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flaming Wave");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			this.proType = 1;
			this.offsetLeft = false;
		}
	}
}
