using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Hostile
{
	public class MACEOrbLaser1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Zap");
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 500;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.hide = true;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 60;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.alpha -= 20;
			if (base.projectile.alpha <= 0)
			{
				base.projectile.Kill();
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Rectangle rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Main.spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, new Rectangle?(rectangle), base.projectile.GetAlpha(lightColor) * 0.9f, base.projectile.rotation, Utils.Size(rectangle) / 2f, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Main.spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, new Rectangle?(rectangle), base.projectile.GetAlpha(lightColor), base.projectile.rotation, Utils.Size(rectangle) / 2f, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.White * ((float)base.projectile.alpha / 255f));
		}
	}
}
