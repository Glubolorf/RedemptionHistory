using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MACEAlarm : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Alarm");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 400;
			base.projectile.height = 400;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 200;
			base.projectile.timeLeft = 60;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha += 10;
			base.projectile.scale -= 0.1f;
			if (base.projectile.scale < 0f)
			{
				base.projectile.scale = 0f;
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}
	}
}
