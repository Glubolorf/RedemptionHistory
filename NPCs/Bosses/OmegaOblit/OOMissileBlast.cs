using System;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	public class OOMissileBlast : ModProjectile
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
			base.projectile.width = 60;
			base.projectile.height = 60;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 6;
		}
	}
}
