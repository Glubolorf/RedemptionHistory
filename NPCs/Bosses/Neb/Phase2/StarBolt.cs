using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb.Phase2
{
	public class StarBolt : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Star Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 12;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 300;
			base.projectile.extraUpdates = 1;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override bool? CanCutTiles()
		{
			return new bool?(false);
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.alpha -= 10;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			base.projectile.velocity *= 1.01f;
			if (base.projectile.localAI[0] == 0f && Main.myPlayer == base.projectile.owner)
			{
				if (Main.myPlayer == base.projectile.owner)
				{
					Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, ModContent.ProjectileType<NebTeleLine2>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
				}
				base.projectile.localAI[0] = 1f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / 2));
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(Main.DiscoColor) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor, base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D flare = base.mod.GetTexture("ExtraTextures/Star");
			Rectangle rect2 = new Rectangle(0, 0, flare.Width, flare.Height);
			Vector2 origin2 = new Vector2((float)(flare.Width / 2), (float)(flare.Height / 2));
			Vector2 position2 = base.projectile.Center - Main.screenPosition;
			spriteBatch.Draw(flare, position2, new Rectangle?(rect2), Main.DiscoColor, base.projectile.rotation, origin2, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(flare, position2, new Rectangle?(rect2), Main.DiscoColor * 0.4f, base.projectile.rotation + 0.7853982f, origin2, 1f, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
