using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class DancingLights : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/EaglecrestGolem/UkkoDancingLights";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dancing Light");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 20;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 254;
			base.projectile.timeLeft = 240;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X + Utils.NextFloat(Main.rand, -0.1f, 0.1f);
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + Utils.NextFloat(Main.rand, -0.1f, 0.1f);
			if (base.projectile.localAI[0] == 0f)
			{
				base.projectile.alpha -= 8;
			}
			else
			{
				base.projectile.localAI[0] += 1f;
			}
			if (base.projectile.timeLeft < 80)
			{
				base.projectile.alpha += 6;
			}
			if (base.projectile.alpha < 100)
			{
				base.projectile.localAI[0] = 1f;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.5f / 255f, (float)(255 - base.projectile.alpha) * 0.2f / 255f);
			if (base.projectile.alpha >= 255 && base.projectile.localAI[0] != 0f)
			{
				base.projectile.Kill();
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D glow = base.mod.GetTexture("ExtraTextures/HolyGlow");
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / 2));
			Rectangle rect2 = new Rectangle(0, 0, glow.Width, glow.Height);
			Vector2 origin2 = new Vector2((float)(glow.Width / 2), (float)(glow.Height / 2));
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(glow, position, new Rectangle?(rect2), drawColor * ((float)(255 - base.projectile.alpha) / 255f) * 0.7f, base.projectile.rotation, origin2, base.projectile.scale * 0.5f, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
