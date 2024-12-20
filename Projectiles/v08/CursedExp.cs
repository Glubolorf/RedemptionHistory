using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class CursedExp : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Explosion");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 98;
			base.projectile.height = 98;
			base.projectile.penetrate = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 600;
			base.projectile.usesLocalNPCImmunity = true;
			base.projectile.localNPCHitCooldown = 5;
			base.projectile.melee = true;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 4)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 6)
				{
					base.projectile.Kill();
				}
			}
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y * 0f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(39, 600, false);
		}
	}
}
