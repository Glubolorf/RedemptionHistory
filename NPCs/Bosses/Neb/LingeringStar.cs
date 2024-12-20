using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class LingeringStar : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/CurvingStar";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shooting Star");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 300;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			if (base.projectile.alpha > 0)
			{
				base.projectile.alpha -= 2;
			}
			if (base.projectile.alpha < 100)
			{
				base.projectile.hostile = true;
			}
			base.projectile.rotation += 0.3f;
		}

		public override void Kill(int timeLeft)
		{
			int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 58, 0f, 0f, 100, Color.White, 4f);
			Main.dust[dustID].velocity *= 0f;
			Main.dust[dustID].noGravity = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), RedeColor.NebColour * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
