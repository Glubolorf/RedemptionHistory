using System;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	public class OONormalBeamR2 : OONormalBeamR
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Deathray");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			this.offsetLeft = true;
		}
	}
}
