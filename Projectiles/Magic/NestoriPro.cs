using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class NestoriPro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Music");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 34;
			base.projectile.magic = true;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			int dustType = 173;
			int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
			Main.dust[dustID].velocity *= 0f;
			Main.dust[dustID].noLight = false;
			Main.dust[dustID].noGravity = true;
			if (this.originalVelocity == Vector2.Zero)
			{
				this.originalVelocity = base.projectile.velocity;
			}
			if (this.offsetLeft)
			{
				this.vectorOffset -= 0.2f;
				if (this.vectorOffset <= -1f)
				{
					this.vectorOffset = -1f;
					this.offsetLeft = false;
				}
			}
			else
			{
				this.vectorOffset += 0.2f;
				if (this.vectorOffset >= 1f)
				{
					this.vectorOffset = 1f;
					this.offsetLeft = true;
				}
			}
			float velRot = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
			base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(base.projectile.velocity.Length(), 0f), velRot + this.vectorOffset * 0.5f);
			base.projectile.rotation = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + base.projectile.velocity) + 1.57f - 0.7853982f;
			base.projectile.spriteDirection = 1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 4.4f;
			}
		}

		public float vectorOffset;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
