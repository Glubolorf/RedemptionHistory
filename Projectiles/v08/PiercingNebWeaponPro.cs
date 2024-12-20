using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class PiercingNebWeaponPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Piercing Nebula");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 180;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			int dustType = 58;
			if (this.proType != 0)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f) - base.projectile.velocity, 2, 2, dustType, 0f, 0f, 100, Color.White, 1.5f);
				Main.dust[dustID].velocity *= 0f;
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			if (this.originalVelocity == Vector2.Zero)
			{
				this.originalVelocity = base.projectile.velocity;
			}
			if (this.proType != 0)
			{
				if (this.offsetLeft)
				{
					this.vectorOffset -= 0.5f;
					if (this.vectorOffset <= -1.3f)
					{
						this.vectorOffset = -1.3f;
						this.offsetLeft = false;
					}
				}
				else
				{
					this.vectorOffset += 0.5f;
					if (this.vectorOffset >= 1.3f)
					{
						this.vectorOffset = 1.3f;
						this.offsetLeft = true;
					}
				}
				float velRot = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
				base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(base.projectile.velocity.Length(), 0f), velRot + this.vectorOffset * 0.5f);
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			base.projectile.spriteDirection = 1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<NebulaSparklePro2>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
		}

		public int proType;

		public float vectorOffset;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
