using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class DreamsongLightPro2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Usable/Potions/DreamsongLightPro";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Celestine Light");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 600;
			base.projectile.height = 600;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			Projectile host = Main.projectile[(int)base.projectile.ai[0]];
			if (!host.active)
			{
				base.projectile.Kill();
			}
			base.projectile.Center = host.Center;
			base.projectile.timeLeft = 10;
			base.projectile.velocity *= 0f;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] < 60f)
			{
				if (base.projectile.localAI[0] < 30f)
				{
					base.projectile.alpha -= 5;
				}
				else
				{
					base.projectile.alpha += 5;
				}
				base.projectile.scale += 0.003f;
				return;
			}
			base.projectile.alpha = 255;
			base.projectile.scale = 1f;
			base.projectile.localAI[0] = 0f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), base.projectile.GetAlpha(Color.White) * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
