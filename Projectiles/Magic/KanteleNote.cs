using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class KanteleNote : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Music");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.magic = true;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.timeLeft = 200;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			if (this.originalVelocity == Vector2.Zero)
			{
				this.originalVelocity = base.projectile.velocity;
			}
			if (this.offsetLeft)
			{
				this.vectorOffset -= 0.1f;
				if (this.vectorOffset <= -0.4f)
				{
					this.vectorOffset = -0.4f;
					this.offsetLeft = false;
				}
			}
			else
			{
				this.vectorOffset += 0.1f;
				if (this.vectorOffset >= 0.4f)
				{
					this.vectorOffset = 0.4f;
					this.offsetLeft = true;
				}
			}
			float velRot = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
			base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(base.projectile.velocity.Length(), 0f), velRot + this.vectorOffset * 0.5f);
			base.projectile.spriteDirection = 1;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(lightColor) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 59, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 0.4f;
			}
		}

		public float vectorOffset;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
