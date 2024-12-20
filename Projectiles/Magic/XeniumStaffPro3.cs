using System;

namespace Redemption.Projectiles.Magic
{
	public class XeniumStaffPro3 : XeniumStaffPro1
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/Magic/XeniumStaffPro1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Laser");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			this.proType = 2;
			this.offsetLeft = true;
		}
	}
}
