using System;

namespace Redemption.NPCs.Bosses.Neb
{
	public class PNebula2 : PNebula1
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

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
