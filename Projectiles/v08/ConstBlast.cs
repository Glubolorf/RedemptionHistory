using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class ConstBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Constellations");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 32;
			base.projectile.height = 32;
			base.projectile.timeLeft = 360;
			base.projectile.penetrate = -1;
			base.projectile.extraUpdates = 3;
			base.projectile.friendly = true;
			base.projectile.magic = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			if (base.projectile.ai[0] < 200f)
			{
				this.AICharge();
			}
			else
			{
				this.AILaunch();
			}
			base.projectile.ai[0] += 1f;
		}

		private void AICharge()
		{
			base.projectile.position -= base.projectile.velocity;
			if (base.projectile.ai[0] == 0f)
			{
				base.projectile.position += base.projectile.velocity * 1f;
				base.projectile.ai[1] = 0.1f;
				if (base.projectile.velocity.X > 0f)
				{
					base.projectile.spriteDirection = 1;
				}
				else
				{
					base.projectile.spriteDirection = -1;
				}
				base.projectile.scale = 1.5f;
				this.staPos = base.projectile.position + base.projectile.velocity * 1f;
				this.endPos = base.projectile.position;
				for (int i = 0; i < base.projectile.timeLeft; i++)
				{
					Vector2 vector = base.projectile.velocity * 0.5f;
					Vector2 vector2 = Collision.TileCollision(new Vector2(this.endPos.X - 2f + (float)(base.projectile.width / 2), this.endPos.Y - 2f + (float)(base.projectile.height / 2)), vector, 4, 4, true, true, 1);
					if (vector != vector2)
					{
						this.endPos += vector2;
						break;
					}
					vector2 = Collision.TileCollision(new Vector2(this.endPos.X - 2f + (float)(base.projectile.width / 2), this.endPos.Y - 2f + (float)(base.projectile.height / 2)) + vector, vector, 4, 4, true, true, 1);
					if (vector != vector2)
					{
						this.endPos += vector + vector2;
						break;
					}
					this.endPos += base.projectile.velocity;
				}
				for (int j = 0; j < 5; j++)
				{
					int num = Dust.NewDust(base.projectile.position + new Vector2((float)(base.projectile.width / 2), (float)(base.projectile.height / 2)) + base.projectile.velocity * 1f, 1, 1, 63, 0f, 0f, 100, Color.White, 1.5f);
					Main.dust[num].noGravity = true;
					Main.dust[num].velocity *= 0.4f;
					num = Dust.NewDust(this.endPos + new Vector2((float)(base.projectile.width / 2), (float)(base.projectile.height / 2)), 0, 0, 63, 0f, 0f, 100, Color.White, 1f);
					Main.dust[num].noGravity = true;
					Main.dust[num].velocity *= 0.4f;
				}
			}
			base.projectile.scale -= 0.007f;
			base.projectile.ai[1] *= 1.004f;
			base.projectile.rotation += 0.05f * (float)base.projectile.spriteDirection * (base.projectile.ai[0] * 0.2f);
		}

		private void AILaunch()
		{
			if (base.projectile.ai[0] == 200f)
			{
				base.projectile.position -= base.projectile.velocity * 1f;
				base.projectile.ai[1] = 1.7f;
				base.projectile.scale = 1.2f;
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 33, 0.4f, 0.2f);
			}
			if (base.projectile.damage > 1 && base.projectile.timeLeft % 2 == 0)
			{
				base.projectile.damage--;
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X);
			base.projectile.scale *= 0.992f;
			if (base.projectile.ai[1] > 0.12f)
			{
				base.projectile.ai[1] *= 0.98f;
			}
			int num = Dust.NewDust(base.projectile.position + new Vector2((float)base.projectile.width / 2f, (float)base.projectile.height / 2f), 1, 1, 63, 0f, 0f, 100, Color.White, 1f);
			Main.dust[num].noGravity = true;
			Main.dust[num].velocity *= 0.5f;
			Vector2 vector = base.projectile.velocity * 0.5f;
			Vector2 vector2 = Collision.TileCollision(new Vector2(base.projectile.position.X - 2f + (float)(base.projectile.width / 2), base.projectile.position.Y - 2f + (float)(base.projectile.height / 2)), vector, 4, 4, true, true, 1);
			if (vector != vector2)
			{
				base.projectile.Kill();
			}
			vector2 = Collision.TileCollision(new Vector2(base.projectile.position.X - 2f + (float)(base.projectile.width / 2), base.projectile.position.Y - 2f + (float)(base.projectile.height / 2)) + vector, vector, 4, 4, true, true, 1);
			if (vector != vector2)
			{
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 40; i++)
			{
				int num = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 63, (float)i / 90f * -base.projectile.velocity.X, (float)i / 90f * -base.projectile.velocity.Y, 100, Color.White, 1.2f);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 0.6f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Main.projectileTexture[base.projectile.type];
			Vector2 vector;
			vector..ctor((float)texture2D.Width / 2f, (float)texture2D.Height / 2f);
			this.drawLaser(spriteBatch, vector);
			spriteBatch.Draw(texture2D, base.projectile.position - Main.screenPosition + vector, new Rectangle?(new Rectangle(0, 0, texture2D.Width, texture2D.Height)), Color.White, base.projectile.rotation, vector, base.projectile.scale, 0, 0f);
			return false;
		}

		private void drawLaser(SpriteBatch spritebatch, Vector2 centre)
		{
			Vector2 vector = base.projectile.position + new Vector2((float)(base.projectile.width / 2), (float)(base.projectile.height / 2));
			if (base.projectile.ai[0] > 0f)
			{
				Vector2 vector2;
				Vector2 vector3;
				if (base.projectile.ai[0] < 200f)
				{
					vector2 = vector;
					vector3 = this.endPos + centre;
				}
				else
				{
					vector2 = this.staPos + centre;
					vector3 = vector;
				}
				Utils.DrawLaser(spritebatch, base.mod.GetTexture("Projectiles/v08/ConstBlast_Beam"), vector2 - Main.screenPosition, vector3 - Main.screenPosition, new Vector2(base.projectile.ai[1]), new Utils.LaserLineFraming(this.ConstLaser));
			}
		}

		private void ConstLaser(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
		{
			color = Color.White;
			if (stage == 0)
			{
				distCovered = 33f;
				frame = new Rectangle(0, 0, 16, 16);
				origin = Utils.Size(frame) / 2f;
				return;
			}
			if (stage == 1)
			{
				frame = new Rectangle(0, 16, 16, 16);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			if (stage == 2)
			{
				distCovered = 22f;
				frame = new Rectangle(0, 24, 16, 16);
				origin = new Vector2((float)(frame.Width / 2), 1f);
				return;
			}
			distCovered = base.projectile.velocity.Length() * 90f;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
		}

		private const int chargeTime = 200;

		private const float muzzleDist = 1f;

		private const int hitboxSize = 4;

		private const int hitboxHalfSize = 2;

		private Vector2 staPos;

		private Vector2 endPos;
	}
}
