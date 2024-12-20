using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class GreenGasPro2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/GreenGasPro";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Gas");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 40;
			base.projectile.height = 40;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 200;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			base.projectile.rotation += 0.04f;
		}
	}
}
