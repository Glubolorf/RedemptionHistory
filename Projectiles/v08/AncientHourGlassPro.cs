using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class AncientHourGlassPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Hourglass");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 38;
			base.projectile.height = 38;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 300;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override bool PreAI()
		{
			base.projectile.rotation += 0.05f;
			return true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			base.projectile.Kill();
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(ModContent.BuffType<TimestopDebuff>(), Main.rand.Next(40, 80), true);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item27, base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 269, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
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
