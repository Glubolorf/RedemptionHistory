using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class GirusLancePro1 : ModProjectile
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/Melee/" + base.GetType().Name + "_Glow");
				GirusLancePro1.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Girus Lance");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 130;
			base.projectile.height = 130;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.melee = true;
			base.projectile.glowMask = GirusLancePro1.customGlowMask;
		}

		public override void AI()
		{
			base.projectile.soundDelay--;
			if (base.projectile.soundDelay <= 0)
			{
				Main.PlaySound(2, (int)base.projectile.Center.X, (int)base.projectile.Center.Y, 15, 1f, 0f);
				base.projectile.soundDelay = 45;
			}
			Player player = Main.player[base.projectile.owner];
			if (Main.myPlayer == base.projectile.owner && (!player.channel || player.noItems || player.CCed))
			{
				base.projectile.Kill();
			}
			Lighting.AddLight(base.projectile.Center, 1f, 0f, 0f);
			base.projectile.Center = player.MountedCenter;
			Projectile projectile = base.projectile;
			projectile.position.X = projectile.position.X + (float)(player.width / 2 * player.direction);
			base.projectile.spriteDirection = player.direction;
			base.projectile.rotation += 0.7f * (float)player.direction;
			if (base.projectile.rotation > 6.2831855f)
			{
				base.projectile.rotation -= 6.2831855f;
			}
			else if (base.projectile.rotation < 0f)
			{
				base.projectile.rotation += 6.2831855f;
			}
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = base.projectile.rotation;
			int dust = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 235, 0f, 0f, 0, default(Color), 1f);
			Main.dust[dust].velocity /= 1f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 4;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, null, Color.White, base.projectile.rotation, new Vector2((float)(texture.Width / 2), (float)(texture.Height / 2)), 1f, (base.projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public static short customGlowMask;
	}
}
