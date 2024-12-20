using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class WhiteNeedlePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/Melee/" + base.GetType().Name);
				WhiteNeedlePro.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("White Needle");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.melee = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.extraUpdates = 1;
			base.projectile.timeLeft = 80;
			base.projectile.alpha = 255;
			base.projectile.usesLocalNPCImmunity = true;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
			base.projectile.glowMask = WhiteNeedlePro.customGlowMask;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.alpha > 0)
			{
				base.projectile.alpha -= 10;
			}
			base.projectile.velocity *= 1.07f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			base.projectile.localNPCImmunity[target.whoAmI] = 10;
			target.immune[base.projectile.owner] = 0;
			Main.PlaySound(SoundID.Item17, base.projectile.position);
			target.AddBuff(70, 600, false);
			base.projectile.friendly = false;
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

		public static short customGlowMask;
	}
}
