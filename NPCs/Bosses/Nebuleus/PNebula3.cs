using System;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class PNebula3 : PNebula1
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
