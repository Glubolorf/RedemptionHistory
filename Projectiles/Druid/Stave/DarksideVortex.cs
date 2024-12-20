using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class DarksideVortex : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Darkside Vortex");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 192;
			base.projectile.height = 192;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num8 = projectile.frameCounter + 1;
			projectile.frameCounter = num8;
			if (num8 >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num8 = projectile2.frame + 1;
				projectile2.frame = num8;
				if (num8 >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.rotation += 0.3f;
			base.projectile.velocity *= 0.94f;
			if (base.projectile.timeLeft == 180)
			{
				Main.PlaySound(SoundID.Item74, base.projectile.position);
			}
			if (base.projectile.timeLeft <= 180)
			{
				for (int i = 0; i < 30; i++)
				{
					Vector2 dustRotation = Utils.RotatedBy(new Vector2((float)(Main.rand.Next(25) * 4), 0f), (double)MathHelper.ToRadians((float)(i * 12)), default(Vector2));
					Vector2 dustPosition = base.projectile.Center + dustRotation;
					Vector2 nextDustPosition = base.projectile.Center + Utils.RotatedBy(dustRotation, (double)MathHelper.ToRadians(-4f), default(Vector2));
					Vector2 dustVelocity = dustPosition - nextDustPosition + base.projectile.velocity;
					if (Utils.NextBool(Main.rand, 2))
					{
						Dust dust = Dust.NewDustPerfect(dustPosition, 62, new Vector2?(dustVelocity), 0, default(Color), 1.5f);
						dust.noGravity = true;
						dust.alpha += 10;
						dust.rotation = Utils.ToRotation(dustRotation);
					}
				}
				base.projectile.friendly = true;
			}
			if (base.projectile.timeLeft < 60)
			{
				base.projectile.alpha += 10;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			for (int u = 0; u < 200; u++)
			{
				NPC target = Main.npc[u];
				if (target.active && Vector2.Distance(base.projectile.Center, target.Center) < 300f && !target.boss && target.knockBackResist != 0f && !target.friendly)
				{
					float num9 = 10f;
					Vector2 vector = new Vector2(target.position.X + (float)(target.width / 2), target.position.Y + (float)(target.height / 2));
					float num4 = base.projectile.Center.X - vector.X;
					float num5 = base.projectile.Center.Y - vector.Y;
					float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
					num6 = num9 / num6;
					num4 *= num6;
					num5 *= num6;
					int num7;
					if (Vector2.Distance(base.projectile.Center, target.Center) < 50f)
					{
						num7 = 15;
					}
					else if (Vector2.Distance(base.projectile.Center, target.Center) < 100f)
					{
						num7 = 18;
					}
					else if (Vector2.Distance(base.projectile.Center, target.Center) < 200f)
					{
						num7 = 20;
					}
					else if (Vector2.Distance(base.projectile.Center, target.Center) < 250f)
					{
						num7 = 30;
					}
					else
					{
						num7 = 40;
					}
					target.velocity.X = (target.velocity.X * (float)(num7 - 1) + num4) / (float)num7;
					target.velocity.Y = (target.velocity.Y * (float)(num7 - 1) + num5) / (float)num7;
				}
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.Purple);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			int num215 = texture.Height / 5;
			int y7 = num215 * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y7, texture.Width, num215);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)num215 / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
