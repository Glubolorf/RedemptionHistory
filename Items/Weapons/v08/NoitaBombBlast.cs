using System;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class NoitaBombBlast : ModProjectile
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
			base.DisplayName.SetDefault("Explosion");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 120;
			base.projectile.height = 120;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 3;
		}
	}
}
