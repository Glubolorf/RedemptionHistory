using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class CurvingStarTele : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/CurvingStarTele";
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
			base.projectile.timeLeft = 160;
			base.projectile.alpha = 255;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				base.projectile.alpha -= 2;
				if (base.projectile.alpha <= 170)
				{
					base.projectile.localAI[0] = 1f;
				}
			}
			else
			{
				base.projectile.alpha++;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.ai[1] == 0f)
			{
				base.projectile.velocity = Utils.RotatedBy(base.projectile.velocity, 0.017453292519943295, default(Vector2)) * base.projectile.ai[0];
				return;
			}
			base.projectile.velocity = Utils.RotatedBy(base.projectile.velocity, -0.017453292519943295, default(Vector2)) * base.projectile.ai[0];
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
	}
}
