using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class UkkoLightning : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Terraria/Projectile_466";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lightning Arc");
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 20;
			base.projectile.scale = 0.5f;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.alpha = 100;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = true;
			base.projectile.extraUpdates = 3;
			base.projectile.timeLeft = 120 * (base.projectile.extraUpdates + 1);
			base.projectile.penetrate = -1;
			base.projectile.usesIDStaticNPCImmunity = true;
			base.projectile.idStaticNPCHitCooldown = 10;
		}

		public override void AI()
		{
			base.projectile.frameCounter = base.projectile.frameCounter + 1;
			Lighting.AddLight(base.projectile.Center, 0.3f, 0.45f, 0.5f);
			this.colorlerp += 0.05f;
			if (!this.playedsound)
			{
				Main.PlaySound(2, (int)base.projectile.Center.X, (int)base.projectile.Center.Y, 122, 0.5f, -0.5f);
				this.playedsound = true;
			}
			if (base.projectile.velocity == Vector2.Zero)
			{
				if (base.projectile.frameCounter >= base.projectile.extraUpdates * 2)
				{
					base.projectile.frameCounter = 0;
					bool flag = true;
					for (int index = 1; index < base.projectile.oldPos.Length; index++)
					{
						if (base.projectile.oldPos[index] != base.projectile.oldPos[0])
						{
							flag = false;
						}
					}
					if (flag)
					{
						base.projectile.Kill();
						return;
					}
				}
				if (Main.rand.Next(base.projectile.extraUpdates) != 0)
				{
					return;
				}
				for (int index2 = 0; index2 < 2; index2++)
				{
					float num = base.projectile.rotation + (float)(((Main.rand.Next(2) == 1) ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float)(Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2 = new Vector2((float)Math.Cos((double)num) * num2, (float)Math.Sin((double)num) * num2);
					int index3 = Dust.NewDust(base.projectile.Center, 0, 0, 226, vector2.X, vector2.Y, 0, default(Color), 1f);
					Main.dust[index3].noGravity = true;
					Main.dust[index3].scale = 1.2f;
				}
				if (Main.rand.Next(5) != 0)
				{
					return;
				}
				int index4 = Dust.NewDust(base.projectile.Center + Utils.RotatedBy(base.projectile.velocity, 1.57079637050629, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.projectile.width - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[index4].velocity *= 0.5f;
				Main.dust[index4].velocity.Y = -Math.Abs(Main.dust[index4].velocity.Y);
				return;
			}
			else
			{
				if (base.projectile.frameCounter < base.projectile.extraUpdates * 2)
				{
					return;
				}
				base.projectile.frameCounter = 0;
				float num3 = base.projectile.velocity.Length();
				UnifiedRandom unifiedRandom = new UnifiedRandom((int)base.projectile.ai[1]);
				int num4 = 0;
				Vector2 spinningpoint = -Vector2.UnitY;
				Vector2 rotationVector2;
				int num6;
				do
				{
					int num5 = unifiedRandom.Next();
					base.projectile.ai[1] = (float)num5;
					rotationVector2 = Utils.ToRotationVector2((float)((double)(num5 % 100) / 100.0 * 6.28318548202515));
					if ((double)rotationVector2.Y > 0.0)
					{
						rotationVector2.Y -= 1f;
					}
					bool flag2 = false;
					if ((double)rotationVector2.Y > -0.0199999995529652)
					{
						flag2 = true;
					}
					if ((double)rotationVector2.X * (double)(base.projectile.extraUpdates + 1) * 2.0 * (double)num3 + (double)base.projectile.localAI[0] > 40.0)
					{
						flag2 = true;
					}
					if ((double)rotationVector2.X * (double)(base.projectile.extraUpdates + 1) * 2.0 * (double)num3 + (double)base.projectile.localAI[0] < -40.0)
					{
						flag2 = true;
					}
					if (!flag2)
					{
						goto IL_496;
					}
					num6 = num4;
					num4 = num6 + 1;
				}
				while (num6 < 100);
				base.projectile.velocity = Vector2.Zero;
				base.projectile.localAI[1] = 1f;
				goto IL_49A;
				IL_496:
				spinningpoint = rotationVector2;
				IL_49A:
				if (base.projectile.velocity == Vector2.Zero || base.projectile.velocity.Length() < 4f)
				{
					base.projectile.velocity = Utils.RotatedByRandom(Utils.RotatedBy(Vector2.UnitX, (double)base.projectile.ai[0], default(Vector2)), 0.7853981633974483) * 7f;
					base.projectile.ai[1] = (float)Main.rand.Next(100);
					return;
				}
				base.projectile.localAI[0] += (float)((double)spinningpoint.X * (double)(base.projectile.extraUpdates + 1) * 2.0) * num3;
				base.projectile.velocity = Utils.RotatedBy(spinningpoint, (double)base.projectile.ai[0] + 1.57079637050629, default(Vector2)) * num3;
				base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + 1.570796f;
				return;
			}
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			int index = 0;
			while (index < base.projectile.oldPos.Length && ((double)base.projectile.oldPos[index].X != 0.0 || (double)base.projectile.oldPos[index].Y != 0.0))
			{
				Rectangle myRect = projHitbox;
				myRect.X = (int)base.projectile.oldPos[index].X;
				myRect.Y = (int)base.projectile.oldPos[index].Y;
				if (myRect.Intersects(targetHitbox))
				{
					return new bool?(true);
				}
				index++;
			}
			return new bool?(false);
		}

		public override void Kill(int timeLeft)
		{
			float num2 = (float)((double)(base.projectile.rotation + 1.5707964f) + ((Main.rand.Next(2) == 1) ? -1.0 : 1.0) * 1.5707963705062866);
			float num3 = (float)(Main.rand.NextDouble() * 2.0 + 2.0);
			Vector2 vector2 = new Vector2((float)Math.Cos((double)num2) * num3, (float)Math.Sin((double)num2) * num3);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				int index = Dust.NewDust(base.projectile.oldPos[i], 0, 0, 229, vector2.X, vector2.Y, 0, default(Color), 1f);
				Main.dust[index].noGravity = true;
				Main.dust[index].scale = 1.7f;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(144, target.HasBuff(103) ? 320 : 160, true);
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.Lerp(Color.LightYellow, Color.White, 0.5f + (float)Math.Sin((double)this.colorlerp) / 2f) * 0.5f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D13 = Main.projectileTexture[base.projectile.type];
			Rectangle rectangle = texture2D13.Bounds;
			Vector2 origin2 = Utils.Size(rectangle) / 2f;
			Color color27 = base.projectile.GetAlpha(lightColor);
			for (int i = 1; i < ProjectileID.Sets.TrailCacheLength[base.projectile.type]; i++)
			{
				if (!(base.projectile.oldPos[i] == Vector2.Zero) && !(base.projectile.oldPos[i - 1] == base.projectile.oldPos[i]))
				{
					Vector2 offset = base.projectile.oldPos[i - 1] - base.projectile.oldPos[i];
					int length = (int)offset.Length();
					float scale = base.projectile.scale * (float)Math.Sin((double)((float)i / 3.1415927f));
					offset.Normalize();
					for (int j = 0; j < length; j += 3)
					{
						Vector2 value5 = base.projectile.oldPos[i] + offset * (float)j;
						Main.spriteBatch.Draw(texture2D13, value5 + base.projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.projectile.gfxOffY), new Rectangle?(rectangle), color27, base.projectile.rotation, origin2, scale, SpriteEffects.FlipHorizontally, 0f);
					}
				}
			}
			return false;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.projectile.velocity = Vector2.Zero;
			return false;
		}

		private float colorlerp;

		private bool playedsound;
	}
}
