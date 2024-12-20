using System;

namespace Redemption.Projectiles
{
	public class DNAPro2 : DNAPro1
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("DNA");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			this.DNAproType = 1;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
			this.offsetLeft = false;
		}
	}
}
