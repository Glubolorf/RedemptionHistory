using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DamagePro1 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/DamagePro";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("a swift stab");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 12;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 6;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.velocity.Y = 0f;
		}
	}
}
