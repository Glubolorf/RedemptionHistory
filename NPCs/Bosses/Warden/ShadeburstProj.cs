using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class ShadeburstProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadeburst");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 200;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			Player target = Main.player[base.projectile.GetNearestAlivePlayer()];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 8)
				{
					base.projectile.frame = 0;
				}
			}
			float obj = base.projectile.ai[0];
			if (!0f.Equals(obj))
			{
				if (1f.Equals(obj))
				{
					base.projectile.tileCollide = true;
					if (this.speed < 15f)
					{
						this.speed *= 1.025f;
					}
					this.turnResist *= 1.015f;
					base.projectile.Move(target.Center, this.speed, this.turnResist, false);
					return;
				}
				if (!2f.Equals(obj))
				{
					if (!3f.Equals(obj))
					{
						if (4f.Equals(obj))
						{
							if (this.originalVelocity == Vector2.Zero)
							{
								this.originalVelocity = base.projectile.velocity;
							}
							if (base.projectile.localAI[0] == 0f)
							{
								this.vectorOffset -= 0.2f;
								if (this.vectorOffset <= -0.8f)
								{
									this.vectorOffset = -0.8f;
									base.projectile.localAI[0] = 1f;
								}
							}
							else
							{
								this.vectorOffset += 0.2f;
								if (this.vectorOffset >= 0.8f)
								{
									this.vectorOffset = 0.8f;
									base.projectile.localAI[0] = 0f;
								}
							}
							base.projectile.hostile = false;
							if (base.projectile.timeLeft < 80)
							{
								base.projectile.tileCollide = true;
							}
							float velRot = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
							base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(base.projectile.velocity.Length(), 0f), velRot + this.vectorOffset * 0.5f);
							return;
						}
						if (!5f.Equals(obj))
						{
							return;
						}
						Projectile projectile3 = base.projectile;
						projectile3.velocity.X = projectile3.velocity.X * 0.98f;
					}
					else
					{
						Projectile projectile4 = base.projectile;
						projectile4.velocity.X = projectile4.velocity.X * 0.96f;
						if (base.projectile.timeLeft < 198)
						{
							base.projectile.tileCollide = true;
							return;
						}
					}
				}
				else
				{
					Projectile projectile5 = base.projectile;
					projectile5.velocity.Y = projectile5.velocity.Y * 0.98f;
					if (base.projectile.velocity.X > -15f)
					{
						Projectile projectile6 = base.projectile;
						projectile6.velocity.X = projectile6.velocity.X - 1f;
						return;
					}
				}
			}
			else
			{
				Projectile projectile7 = base.projectile;
				projectile7.velocity.Y = projectile7.velocity.Y * 0.98f;
				if (base.projectile.velocity.X < 15f)
				{
					Projectile projectile8 = base.projectile;
					projectile8.velocity.X = projectile8.velocity.X + 1f;
					return;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCHit54.WithVolume(0.2f), base.projectile.position);
			for (int i = 0; i < 6; i++)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 30.0);
				this.vector.Y = (float)(Math.Cos(angle) * 30.0);
				Dust dust2 = Main.dust[Dust.NewDust(base.projectile.Center + this.vector, 2, 2, 261, 0f, 0f, 100, default(Color), 1f)];
				dust2.noGravity = true;
				dust2.velocity = base.projectile.DirectionTo(dust2.position) * 4f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D glow = ModContent.GetTexture("Redemption/NPCs/Bosses/Warden/ShadeburstProj_Glow");
			int num215 = texture.Height / 8;
			int y7 = num215 * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y7, texture.Width, num215);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)num215 / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i];
				Color color = base.projectile.GetAlpha(Color.White) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(glow, drawPos + base.projectile.Size / 2f - Main.screenPosition, new Rectangle?(rect), color, base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		private Vector2 vector;

		public float speed = 1f;

		public float turnResist = 20f;

		public float vectorOffset;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
