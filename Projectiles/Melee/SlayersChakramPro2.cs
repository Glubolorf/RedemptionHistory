using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class SlayersChakramPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cyber Chakram");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.friendly = true;
			base.projectile.melee = true;
			base.projectile.magic = false;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = 5;
			base.projectile.timeLeft = 180;
			base.projectile.light = 0.7f;
		}

		public override void AI()
		{
			base.projectile.velocity.X = 0f;
			base.projectile.velocity.Y = 0f;
			base.projectile.alpha += 3;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			if (base.projectile.localAI[0] == 0f)
			{
				base.projectile.rotation = base.projectile.ai[0];
				base.projectile.localAI[0] = 1f;
			}
		}
	}
}
