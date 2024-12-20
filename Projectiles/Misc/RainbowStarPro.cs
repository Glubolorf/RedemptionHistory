using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class RainbowStarPro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/ExtraTextures/WhiteFlare";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rainbow Shimmer");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 114;
			base.projectile.height = 114;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 300;
			base.projectile.scale = 1f;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			base.projectile.velocity *= 0.96f;
			float obj = base.projectile.ai[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (!2f.Equals(obj))
					{
						return;
					}
					base.projectile.alpha += this.alphaIncrease + (int)base.projectile.ai[1];
					base.projectile.scale -= this.scaleIncrease;
					if (base.projectile.alpha >= 255 || (double)base.projectile.scale < 0.01)
					{
						base.projectile.Kill();
					}
				}
				else
				{
					base.projectile.alpha -= this.alphaIncrease + (int)base.projectile.ai[1];
					base.projectile.scale += this.scaleIncrease;
					if (base.projectile.alpha <= 0)
					{
						this.scaleIncrease = Utils.NextFloat(Main.rand, 0.02f, 0.1f);
						this.alphaIncrease = Main.rand.Next(3, 10);
						base.projectile.ai[0] = 2f;
						return;
					}
				}
				return;
			}
			base.projectile.scale = 0.01f;
			this.scaleIncrease = Utils.NextFloat(Main.rand, 0.01f, 0.05f);
			this.alphaIncrease = Main.rand.Next(5, 10);
			base.projectile.ai[0] = 1f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Rectangle rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(lightColor) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			Main.spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, new Rectangle?(rectangle), base.projectile.GetAlpha(lightColor) * 1.5f * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, Utils.Size(rectangle) / 2f, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Main.DiscoColor);
		}

		public float scaleIncrease;

		public int alphaIncrease;
	}
}
