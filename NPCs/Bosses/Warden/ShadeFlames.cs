using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class ShadeFlames : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Flames");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 122;
			base.projectile.height = 66;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = true;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 240;
			base.projectile.hide = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
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
				if (num >= 8)
				{
					base.projectile.frame = 0;
				}
			}
			float obj = base.projectile.ai[1];
			if (!0f.Equals(obj))
			{
				if (1f.Equals(obj))
				{
					base.projectile.ai[0] += 1f;
					if (base.projectile.ai[0] > 30f && base.projectile.ai[0] % 3f == 0f && base.projectile.timeLeft >= 60 && Main.myPlayer == base.projectile.owner)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + (float)Main.rand.Next(0, base.projectile.width), base.projectile.Center.Y + 20f), new Vector2((float)Main.rand.Next(-7, 7), -10f), ModContent.ProjectileType<ShadeburstProj>(), base.projectile.damage, 1f, Main.myPlayer, 3f, 0f);
					}
					if (base.projectile.timeLeft < 60)
					{
						base.projectile.alpha = (int)MathHelper.Lerp(255f, 80f, (float)(base.projectile.timeLeft / 60));
					}
				}
			}
			else
			{
				base.projectile.ai[0] += 1f;
				if (base.projectile.ai[0] > 60f)
				{
					base.projectile.alpha -= 10;
				}
				if (base.projectile.alpha <= 0)
				{
					base.projectile.ai[0] = 0f;
					base.projectile.ai[1] = 1f;
				}
			}
			for (int p = 0; p < 1000; p++)
			{
				this.clearCheck = Main.projectile[p];
				if (this.clearCheck.active && this.clearCheck.whoAmI != base.projectile.whoAmI && this.clearCheck.type == ModContent.ProjectileType<ShadeFlames>() && base.projectile.Hitbox.Intersects(this.clearCheck.Hitbox))
				{
					base.projectile.Center = this.PosPick();
				}
			}
		}

		public Vector2 PosPick()
		{
			Vector2[] pickArray = new Vector2[]
			{
				new Vector2(1064f, 1184f),
				new Vector2(1033f, 1202f),
				new Vector2(1069f, 1215f),
				new Vector2(1166f, 1203f),
				new Vector2(1135f, 1184f),
				new Vector2(1133f, 1217f),
				new Vector2(1131f, 1253f),
				new Vector2(1168f, 1268f),
				new Vector2(1132f, 1290f),
				new Vector2(1037f, 1270f),
				new Vector2(1069f, 1290f),
				new Vector2(1067f, 1252f),
				new Vector2(1099f, 1242f)
			};
			return pickArray[Main.rand.Next(Enumerable.Count<Vector2>(pickArray))] * 16f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.projectile.velocity *= 0f;
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			int num215 = texture.Height / 8;
			int y7 = num215 * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y7, texture.Width, num215);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)num215 / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), base.projectile.GetAlpha(Color.White) * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		private Projectile clearCheck;
	}
}
