using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class NebulaSparklePro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebula Blast");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 64;
			base.projectile.height = 80;
			base.projectile.penetrate = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.melee = true;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 100;
			base.projectile.timeLeft = 30;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 3;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 5)
				{
					base.projectile.Kill();
				}
			}
			Projectile projectile3 = base.projectile;
			projectile3.velocity.X = projectile3.velocity.X * 0f;
			Projectile projectile4 = base.projectile;
			projectile4.velocity.Y = projectile4.velocity.Y * 0f;
		}
	}
}
