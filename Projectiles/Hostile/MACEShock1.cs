﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Hostile
{
	public class MACEShock1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flaming Wave");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 60;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			int dustType = 6;
			int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2.2f);
			Main.dust[dustID].velocity *= 0f;
			Main.dust[dustID].noLight = false;
			Main.dust[dustID].noGravity = true;
			if (this.proType != 0)
			{
				dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f) - base.projectile.velocity, 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
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
					if (this.vectorOffset <= -1.5f)
					{
						this.vectorOffset = -1.5f;
						this.offsetLeft = false;
					}
				}
				else
				{
					this.vectorOffset += 0.5f;
					if (this.vectorOffset >= 1.5f)
					{
						this.vectorOffset = 1.5f;
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
			int dustType = 6;
			for (int i = 0; i < 4; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, dustType, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}

		public int proType;

		public float vectorOffset;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
