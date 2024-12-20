using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SpellboundLancePro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Reginleif, Spellbound Lance");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 64;
			base.projectile.height = 64;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.melee = true;
		}

		public override void AI()
		{
			base.projectile.soundDelay--;
			if (base.projectile.soundDelay <= 0)
			{
				Main.PlaySound(2, (int)base.projectile.Center.X, (int)base.projectile.Center.Y, 71, 1f, 0f);
				base.projectile.soundDelay = 45;
			}
			Player player = Main.player[base.projectile.owner];
			if (Main.myPlayer == base.projectile.owner && (!player.channel || player.noItems || player.CCed))
			{
				base.projectile.Kill();
			}
			Lighting.AddLight(base.projectile.Center, 0.6f, 0.6f, 1f);
			base.projectile.Center = player.MountedCenter;
			Projectile projectile = base.projectile;
			projectile.position.X = projectile.position.X + (float)(player.width / 2 * player.direction);
			base.projectile.spriteDirection = player.direction;
			base.projectile.rotation += 0.5f * (float)player.direction;
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
			int num = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 206, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num].velocity /= 1f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Main.projectileTexture[base.projectile.type];
			spriteBatch.Draw(texture2D, base.projectile.Center - Main.screenPosition, null, Color.White, base.projectile.rotation, new Vector2((float)(texture2D.Width / 2), (float)(texture2D.Height / 2)), 1f, (base.projectile.spriteDirection == 1) ? 0 : 1, 0f);
			return false;
		}
	}
}
