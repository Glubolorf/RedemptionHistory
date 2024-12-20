using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class ShadowMaw : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadow Maw");
			Main.projFrames[base.projectile.type] = 18;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 62;
			base.projectile.height = 64;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 800;
			base.projectile.scale = 2f;
		}

		public override void AI()
		{
			Player player = Main.player[(int)base.projectile.ai[0]];
			if ((player.dead || !player.active) && base.projectile.timeLeft > 60)
			{
				base.projectile.timeLeft = 60;
			}
			if (base.projectile.Center.X > player.Center.X)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<PlayerMarionette>()))
			{
				base.projectile.Kill();
			}
			float obj = base.projectile.ai[1];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					return;
				}
				Projectile projectile = base.projectile;
				int num = projectile.frameCounter + 1;
				projectile.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 18)
					{
						if (base.projectile.timeLeft > 60)
						{
							base.projectile.timeLeft = 60;
						}
						base.projectile.frame = 0;
					}
				}
				base.projectile.localAI[0] += 1f;
				if (base.projectile.frame >= 15)
				{
					base.projectile.hostile = true;
				}
				else
				{
					base.projectile.hostile = false;
				}
				if (base.projectile.timeLeft < 60)
				{
					base.projectile.alpha = (int)MathHelper.Lerp(0f, 255f, (float)(base.projectile.timeLeft / 60));
				}
			}
			else
			{
				Projectile projectile3 = base.projectile;
				int num = projectile3.frameCounter + 1;
				projectile3.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile4 = base.projectile;
					num = projectile4.frame + 1;
					projectile4.frame = num;
					if (num >= 11)
					{
						base.projectile.frame = 5;
					}
				}
				if (base.projectile.Distance(player.Center) < 40f)
				{
					base.projectile.frame = 11;
					base.projectile.ai[1] = 1f;
				}
				if (base.projectile.timeLeft < 60)
				{
					base.projectile.alpha = (int)MathHelper.Lerp(0f, 255f, (float)(base.projectile.timeLeft / 60));
					return;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			int num215 = texture.Height / 18;
			int y7 = num215 * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y7, texture.Width, num215);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)num215 / 2f);
			SpriteEffects spriteEffects = (base.projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), base.projectile.GetAlpha(Color.White) * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, spriteEffects, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
