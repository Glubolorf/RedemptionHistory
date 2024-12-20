using System;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class PNebula2 : PNebula1
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Piercing Nebula");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			this.proType = 1;
			this.offsetLeft = false;
		}
	}
}
