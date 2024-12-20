using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class ShadeVortex : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Vortex");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 192;
			base.projectile.height = 192;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 180;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.2f;
			if (base.projectile.localAI[0] < 60f)
			{
				this.daggerRot = MathHelper.ToRadians(-45f);
				if (base.projectile.alpha > 0)
				{
					base.projectile.alpha -= 5;
				}
				base.projectile.velocity.Y = -1f;
			}
			else
			{
				base.projectile.velocity *= 0.98f;
			}
			if (base.projectile.timeLeft < 60)
			{
				base.projectile.alpha += 10;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D dagger = base.mod.GetTexture("Items/Weapons/PostML/Melee/DaggerOfOathkeeper");
			int num215 = texture.Height / 5;
			int y7 = num215 * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y7, texture.Width, num215);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)num215 / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), -base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Vector2 position2 = base.projectile.Center - Main.screenPosition;
			Rectangle rect2 = new Rectangle(0, 0, dagger.Width, dagger.Height);
			Vector2 origin2 = new Vector2((float)(dagger.Width / 2), (float)(dagger.Height / 2));
			spriteBatch.Draw(dagger, position2, new Rectangle?(rect2), drawColor * ((float)(255 - base.projectile.alpha) / 255f), this.daggerRot, origin2, base.projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public float daggerRot;
	}
}
