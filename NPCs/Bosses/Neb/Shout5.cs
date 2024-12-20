using System;

namespace Redemption.NPCs.Bosses.Neb
{
	public class Shout5 : Shout1
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/Shout5";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shout!");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.projectile.width = 447;
			base.projectile.height = 46;
		}
	}
}
