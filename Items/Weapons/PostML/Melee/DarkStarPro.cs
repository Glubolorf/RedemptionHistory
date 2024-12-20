using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class DarkStarPro : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.aiStyle = -1;
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(Color.Indigo) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			BaseAI.AIBoomerang(base.projectile, ref base.projectile.ai, player.position, player.width, player.height, true, 70f, 30, 34f, 0.6f, true);
			if (this.cooldown > 0)
			{
				this.cooldown--;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (this.cooldown <= 0 && Main.myPlayer == base.projectile.owner)
			{
				for (int i = 0; i < 8; i++)
				{
					Vector2 spawnPos = target.Center + Utils.RotatedBy(Vector2.One, (double)MathHelper.ToRadians((float)(45 * i)), default(Vector2)) * (float)(Math.Max(target.width, target.height) + 40);
					Projectile.NewProjectile(spawnPos, RedeHelper.PolarVector(0.1f, Utils.ToRotation(target.Center - spawnPos)), ModContent.ProjectileType<WhiteNeedlePro>(), base.projectile.damage / 3, 0f, base.projectile.owner, 0f, 0f);
				}
				this.cooldown = 30;
			}
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 120, false);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 8;
			height = 8;
			return true;
		}

		public int cooldown;
	}
}
