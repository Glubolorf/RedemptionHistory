using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class XeniumStaffPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Laser");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.magic = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 300;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			int dustType = 74;
			int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
			Main.dust[dustID].velocity *= 0f;
			Main.dust[dustID].noLight = false;
			Main.dust[dustID].noGravity = true;
			if (this.proType != 0)
			{
				dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f) - base.projectile.velocity, 2, 2, dustType, 0f, 0f, 100, Color.White, 1.2f);
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
					this.vectorOffset -= 0.14f;
					if (this.vectorOffset <= -1f)
					{
						this.vectorOffset = -1f;
						this.offsetLeft = false;
					}
				}
				else
				{
					this.vectorOffset += 0.14f;
					if (this.vectorOffset >= 1f)
					{
						this.vectorOffset = 1f;
						this.offsetLeft = true;
					}
				}
				float velRot = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
				base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(base.projectile.velocity.Length(), 0f), velRot + this.vectorOffset * 0.5f);
			}
			base.projectile.rotation = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + base.projectile.velocity) + 1.57f - 0.7853982f;
			base.projectile.spriteDirection = 1;
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 74;
			int pieCut = 20;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for (int j = 0; j < pieCut; j++)
			{
				int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (float)j / (float)pieCut * 6.28f);
				Main.dust[dustID2].noLight = false;
				Main.dust[dustID2].noGravity = true;
			}
			Main.PlaySound(SoundID.Item62, (int)base.projectile.position.X, (int)base.projectile.position.Y);
		}

		public int proType;

		public float vectorOffset;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
