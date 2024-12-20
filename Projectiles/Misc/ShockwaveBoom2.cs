using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class ShockwaveBoom2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shockwave Boom");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.friendly = true;
			base.projectile.alpha = 0;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 200;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			if (!Main.dedServ)
			{
				float progress = (180f - (float)base.projectile.timeLeft) / 60f;
				float pulseCount = 1f;
				float rippleSize = 5f;
				float speed = 15f;
				Filters.Scene["MoR:Shockwave"].GetShader().UseProgress(progress).UseOpacity(100f * (1f - progress / 3f));
				base.projectile.localAI[1] += 1f;
				if (base.projectile.localAI[1] >= 0f && base.projectile.localAI[1] <= 60f && !Filters.Scene["MoR:Shockwave"].IsActive())
				{
					Filters.Scene.Activate("MoR:Shockwave", base.projectile.Center, new object[0]).GetShader().UseColor(pulseCount, rippleSize, speed).UseTargetPosition(base.projectile.Center);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			if (!Main.dedServ)
			{
				Filters.Scene["MoR:Shockwave"].Deactivate(new object[0]);
			}
		}
	}
}
