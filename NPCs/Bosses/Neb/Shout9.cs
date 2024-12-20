using System;

namespace Redemption.NPCs.Bosses.Neb
{
	public class Shout9 : Shout1
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/Shout9";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shout!");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.projectile.width = 539;
			base.projectile.height = 46;
		}
	}
}
