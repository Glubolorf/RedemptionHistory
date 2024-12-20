using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class CrystalScythe : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Sickle");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 52;
			base.projectile.height = 52;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 0;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha += 5;
			if (base.projectile.localAI[0] == 1f)
			{
				int num = 254;
				int num2 = 16;
				for (int i = 0; i < num2; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num3].noLight = false;
					Main.dust[num3].noGravity = true;
				}
				int num4 = 255;
				int num5 = 8;
				for (int j = 0; j < num5; j++)
				{
					int num6 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num4, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)j / (float)num5 * 6.28f);
					Main.dust[num6].noLight = false;
					Main.dust[num6].noGravity = true;
				}
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 vector;
			vector..ctor((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 vector2 = base.projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(lightColor) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], vector2, null, color, base.projectile.rotation, vector, base.projectile.scale, 0, 0f);
			}
			return true;
		}
	}
}
