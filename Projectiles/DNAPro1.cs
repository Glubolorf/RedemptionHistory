using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DNAPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("DNA");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.melee = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 200;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = true;
			base.projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			int num = (this.DNAproType == 0) ? 0 : ((this.DNAproType == 1) ? 235 : 172);
			if (this.DNAproType != 0)
			{
				int num2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f) - base.projectile.velocity, 2, 2, num, 0f, 0f, 100, Color.White, 1.2f);
				Main.dust[num2].velocity *= 0f;
				Main.dust[num2].noLight = false;
				Main.dust[num2].noGravity = true;
			}
			if (this.originalVelocity == Vector2.Zero)
			{
				this.originalVelocity = base.projectile.velocity;
			}
			if (this.DNAproType != 0)
			{
				if (this.offsetLeft)
				{
					this.vectorOffset -= 0.08f;
					if (this.vectorOffset <= -0.5f)
					{
						this.vectorOffset = -0.5f;
						this.offsetLeft = false;
					}
				}
				else
				{
					this.vectorOffset += 0.08f;
					if (this.vectorOffset >= 0.5f)
					{
						this.vectorOffset = 0.5f;
						this.offsetLeft = true;
					}
				}
				float num3 = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
				base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(base.projectile.velocity.Length(), 0f), num3 + this.vectorOffset * 0.5f);
			}
			base.projectile.rotation = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + base.projectile.velocity) + 1.57f - 0.7853982f;
			base.projectile.spriteDirection = 1;
		}

		public override void Kill(int timeLeft)
		{
			int num = (this.DNAproType == 0) ? 0 : ((this.DNAproType == 1) ? 235 : 172);
			if (this.DNAproType != 0)
			{
				for (int i = 0; i < 4; i++)
				{
					int num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, num, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num2].velocity *= 1.9f;
				}
			}
		}

		public int DNAproType;

		public float vectorOffset;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
