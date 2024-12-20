using System;

namespace Redemption.Projectiles
{
	public class XeniumStaffPro2 : XeniumStaffPro1
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/XeniumStaffPro1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Laser");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			this.proType = 1;
			this.offsetLeft = false;
		}
	}
}
