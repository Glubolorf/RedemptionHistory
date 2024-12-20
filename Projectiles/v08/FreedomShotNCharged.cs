using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class FreedomShotNCharged : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Freedom Charged Shot");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 36;
			base.projectile.height = 32;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.ignoreWater = true;
			base.projectile.extraUpdates = 2;
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.height, base.projectile.width, 58, base.projectile.velocity.X, base.projectile.velocity.Y, 200, default(Color), 1f);
				dust.velocity += base.projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (this.firstHit && base.projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.projectile.Center, new Vector2(0f, 0f), ModContent.ProjectileType<FreedomBallN>(), base.projectile.damage / 6, 0f, base.projectile.owner, 0f, 0f);
				this.firstHit = false;
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, base.projectile.position);
			int p = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<DummyExplosionFreedom>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
			Main.projectile[p].magic = false;
			Main.projectile[p].ranged = true;
			for (int index = 0; index < 30; index++)
			{
				int index2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 58, 0f, 0f, 100, default(Color), 1f);
				Main.dust[index2].velocity *= 1.1f;
				Main.dust[index2].scale *= 0.99f;
			}
		}

		private bool firstHit = true;
	}
}
