using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class UkkoStrike : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ukko's Lightning");
			Main.projFrames[base.projectile.type] = 24;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 42;
			base.projectile.height = 540;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.scale *= 2f;
		}

		public override void AI()
		{
			this.frameCounters++;
			if (this.frameCounters >= 3)
			{
				this.warningFrames++;
				this.frameCounters = 0;
			}
			if (this.warningFrames >= 2)
			{
				this.warningFrames = 0;
			}
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 21)
				{
					base.projectile.Kill();
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			base.projectile.localAI[0] += 1f;
			if (base.projectile.frame >= 12 && base.projectile.frame < 15)
			{
				base.projectile.hostile = true;
			}
			else
			{
				base.projectile.hostile = false;
			}
			if (base.projectile.localAI[0] == 1f)
			{
				Projectile projectile3 = base.projectile;
				projectile3.position.Y = projectile3.position.Y - 540f;
			}
			if (base.projectile.localAI[0] == 36f)
			{
				Main.player[base.projectile.owner].GetModPlayer<ScreenPlayer>().Rumble(10, 10);
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap1").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Bottom.Y), base.projectile.velocity, ModContent.ProjectileType<UkkoStrikeZap>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D warning = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/LightningWarning");
			base.mod.GetTexture("ExtraTextures/WhiteGlow");
			int height = texture.Height / 24;
			int y = height * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y, texture.Width, height);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)height / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), base.projectile.GetAlpha(Color.White) * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation + 1.5707964f, origin, base.projectile.scale, SpriteEffects.None, 0f);
			int height2 = warning.Height / 2;
			int y2 = height2 * this.warningFrames;
			Rectangle rect2 = new Rectangle(0, y2, warning.Width, height2);
			Vector2 origin2 = new Vector2((float)warning.Width / 2f, (float)height2 / 2f);
			if (base.projectile.frame < 12)
			{
				spriteBatch.Draw(warning, position, new Rectangle?(rect2), base.projectile.GetAlpha(Color.White) * 0.8f * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation + 1.5707964f, origin2, base.projectile.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(144, target.HasBuff(103) ? 320 : 160, true);
			}
		}

		public int warningFrames;

		public int frameCounters;
	}
}
