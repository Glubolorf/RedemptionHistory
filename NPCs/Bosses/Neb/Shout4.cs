using System;

namespace Redemption.NPCs.Bosses.Neb
{
	public class Shout4 : Shout1
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/Shout4";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shout!");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.projectile.width = 285;
			base.projectile.height = 46;
		}
	}
}
