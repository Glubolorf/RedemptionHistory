using System;

namespace Redemption.NPCs.Bosses.Neb
{
	public class Shout8 : Shout1
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/Shout8";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shout!");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.projectile.width = 347;
			base.projectile.height = 61;
		}
	}
}
