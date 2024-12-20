using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class WhisperwindPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blight Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 26;
			base.projectile.penetrate = 1;
			base.projectile.ranged = true;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 2;
			if (base.projectile.localAI[0] > 130f)
			{
				base.projectile.Kill();
			}
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

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0.9f)
			{
				base.projectile.velocity.X = oldVelocity.X * -0.9f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0.9f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * -0.9f;
			}
			return false;
		}
	}
}
