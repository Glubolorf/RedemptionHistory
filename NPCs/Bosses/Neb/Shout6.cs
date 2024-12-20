using System;

namespace Redemption.NPCs.Bosses.Neb
{
	public class Shout6 : Shout1
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/Shout6";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shout!");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.projectile.width = 556;
			base.projectile.height = 64;
		}
	}
}
