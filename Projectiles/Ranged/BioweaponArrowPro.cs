using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Wasteland;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class BioweaponArrowPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bio-Weapon Arrow");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.aiStyle = 1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.ranged = true;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 600;
			base.projectile.alpha = 0;
			base.projectile.light = 0.5f;
			base.projectile.ignoreWater = false;
			base.projectile.tileCollide = true;
			base.projectile.extraUpdates = 1;
			this.aiType = 103;
			base.projectile.arrow = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.LightGreen);
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

		public override void Kill(int timeleft)
		{
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			for (int num468 = 0; num468 < 4; num468++)
			{
				num468 = Dust.NewDust(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), base.projectile.width, base.projectile.height, 74, -base.projectile.velocity.X * 0.2f, -base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<BioweaponDebuff>(), 90, false);
		}
	}
}
