using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class TeslaLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tesla Beam");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.friendly = true;
			base.projectile.magic = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = false;
			base.projectile.usesLocalNPCImmunity = true;
			base.projectile.localNPCHitCooldown = 0;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 5;
		}

		public override void AI()
		{
			Vector2? vector = null;
			if (Utils.HasNaNs(base.projectile.velocity) || base.projectile.velocity == Vector2.Zero)
			{
				base.projectile.velocity = -Vector2.UnitY;
			}
			if (base.projectile.type != base.mod.ProjectileType("TeslaLaser") || !Main.projectile[(int)base.projectile.ai[1]].active || Main.projectile[(int)base.projectile.ai[1]].type != base.mod.ProjectileType("TeslaCoilStaffPro"))
			{
				base.projectile.Kill();
				return;
			}
			float num = (float)((int)base.projectile.ai[0]) - 2.5f;
			Vector2 vector2 = Vector2.Normalize(Main.projectile[(int)base.projectile.ai[1]].velocity);
			Projectile projectile = Main.projectile[(int)base.projectile.ai[1]];
			float num2 = num * 0.5235988f;
			Vector2 vector3 = Vector2.Zero;
			float num3;
			float num4;
			float num5;
			float num6;
			if (projectile.ai[0] < 180f)
			{
				num3 = 1f - projectile.ai[0] / 180f;
				num4 = 20f - projectile.ai[0] / 180f * 14f;
				if (projectile.ai[0] < 120f)
				{
					num5 = 20f - 4f * (projectile.ai[0] / 120f);
					base.projectile.Opacity = projectile.ai[0] / 120f * 0.4f;
				}
				else
				{
					num5 = 16f - 10f * ((projectile.ai[0] - 120f) / 60f);
					base.projectile.Opacity = 0.4f + (projectile.ai[0] - 120f) / 60f * 0.6f;
				}
				num6 = -22f + projectile.ai[0] / 180f * 20f;
			}
			else
			{
				num3 = 0f;
				num5 = 1.75f;
				num4 = 6f;
				base.projectile.Opacity = 1f;
				num6 = -2f;
			}
			float num7 = (projectile.ai[0] + num * num5) / (num5 * 6f) * 6.2831855f;
			num2 = Utils.RotatedBy(Vector2.UnitY, (double)num7, default(Vector2)).Y * 0.5235988f * num3;
			vector3 = Utils.RotatedBy(Utils.RotatedBy(Vector2.UnitY, (double)num7, default(Vector2)) * new Vector2(4f, num4), (double)Utils.ToRotation(projectile.velocity), default(Vector2));
			base.projectile.position = projectile.Center + vector2 * 16f - base.projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)base.projectile.ai[1]].gfxOffY);
			base.projectile.position += Utils.ToRotationVector2(Utils.ToRotation(projectile.velocity)) * num6;
			base.projectile.position += vector3;
			base.projectile.velocity = Utils.RotatedBy(Vector2.Normalize(projectile.velocity), (double)num2, default(Vector2));
			base.projectile.scale = 1.8f * (1f - num3);
			base.projectile.damage = projectile.damage;
			if (projectile.ai[0] >= 180f)
			{
				base.projectile.damage *= 3;
				vector = new Vector2?(projectile.Center);
			}
			if (!Collision.CanHitLine(Main.player[base.projectile.owner].Center, 0, 0, projectile.Center, 0, 0))
			{
				vector = new Vector2?(Main.player[base.projectile.owner].Center);
			}
			base.projectile.friendly = (projectile.ai[0] > 30f);
			if (Utils.HasNaNs(base.projectile.velocity) || base.projectile.velocity == Vector2.Zero)
			{
				base.projectile.velocity = -Vector2.UnitY;
			}
			float num8 = Utils.ToRotation(base.projectile.velocity);
			base.projectile.rotation = num8 - 1.5707964f;
			base.projectile.velocity = Utils.ToRotationVector2(num8);
			float num9 = 2f;
			float num10 = 0f;
			Vector2 vector4 = base.projectile.Center;
			if (vector != null)
			{
				vector4 = vector.Value;
			}
			float[] array = new float[(int)num9];
			Collision.LaserScan(vector4, base.projectile.velocity, num10 * base.projectile.scale, 2400f, array);
			float num11 = 0f;
			for (int i = 0; i < array.Length; i++)
			{
				num11 += array[i];
			}
			num11 /= num9;
			float num12 = 0.75f;
			base.projectile.localAI[1] = MathHelper.Lerp(base.projectile.localAI[1], num11, num12);
			if (Math.Abs(base.projectile.localAI[1] - num11) < 100f && base.projectile.scale > 0.15f)
			{
				Color color = Main.hslToRgb(0.54f, 1f, 0.902f);
				color.A = 0;
				Vector2 vector5 = base.projectile.Center + base.projectile.velocity * (base.projectile.localAI[1] - 14.5f * base.projectile.scale);
				float x = Main.rgbToHsl(new Color(255, 250, 205)).X;
				for (int j = 0; j < 2; j++)
				{
					float num13 = Utils.ToRotation(base.projectile.velocity) + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.5707964f;
					float num14 = (float)Main.rand.NextDouble() * 0.8f + 1f;
					Vector2 vector6;
					vector6..ctor((float)Math.Cos((double)num13) * num14, (float)Math.Sin((double)num13) * num14);
					int num15 = Dust.NewDust(vector5, 0, 0, 261, vector6.X, vector6.Y, 0, new Color(255, 250, 205), 1f);
					Main.dust[num15].color = color;
					Main.dust[num15].scale = 1.1f;
					if (base.projectile.scale > 1f)
					{
						Main.dust[num15].velocity *= base.projectile.scale;
						Main.dust[num15].scale *= base.projectile.scale;
					}
					Main.dust[num15].noGravity = true;
					if (base.projectile.scale != 1.4f)
					{
						Dust dust = Dust.CloneDust(num15);
						dust.color = Color.Orange;
						dust.scale /= 2f;
					}
					float num16 = (x + Utils.NextFloat(Main.rand) * 0.4f) % 1f;
					Main.dust[num15].color = Color.Lerp(color, Main.hslToRgb(0.54f, 1f, 0.902f), base.projectile.scale / 1.4f);
				}
				if (Main.rand.Next(5) == 0)
				{
					Vector2 vector7 = Utils.RotatedBy(base.projectile.velocity, 1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.projectile.width;
					int num17 = Dust.NewDust(vector5 + vector7 - Vector2.One * 4f, 8, 8, 261, 0f, 0f, 100, new Color(255, 250, 205), 1f);
					Main.dust[num17].velocity *= 0.5f;
					Main.dust[num17].velocity.Y = -Math.Abs(Main.dust[num17].velocity.Y);
				}
				DelegateMethods.v3_1 = color.ToVector3() * 0.3f;
				float value = 0.1f * (float)Math.Sin((double)(Main.GlobalTime * 20f));
				Vector2 vector8;
				vector8..ctor(base.projectile.velocity.Length() * base.projectile.localAI[1], (float)base.projectile.width * base.projectile.scale);
				float num18 = Utils.ToRotation(base.projectile.velocity);
				if (Main.netMode != 2)
				{
					((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(base.projectile.position + Utils.RotatedBy(new Vector2(vector8.X * 0.5f, 0f), (double)num18, default(Vector2)), new Color(0.5f, 0.1f * (float)Math.Sign(value) + 0.5f, 0f, 1f) * Math.Abs(value), vector8, 1, num18);
				}
				Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + base.projectile.velocity * base.projectile.localAI[1], (float)base.projectile.width * base.projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (base.projectile.velocity == Vector2.Zero)
			{
				return false;
			}
			Texture2D texture2D = Main.projectileTexture[base.projectile.type];
			float num = base.projectile.localAI[1];
			Color color = Main.hslToRgb(0.54f, 1f, 0.902f);
			color.A = 0;
			Vector2 vector = Utils.Floor(base.projectile.Center);
			vector += base.projectile.velocity * base.projectile.scale * 10.5f;
			num -= base.projectile.scale * 14.5f * base.projectile.scale;
			Vector2 vector2;
			vector2..ctor(base.projectile.scale);
			DelegateMethods.f_1 = 1f;
			DelegateMethods.c_1 = color * 0.75f * base.projectile.Opacity;
			Vector2 vector3 = base.projectile.oldPos[0];
			new Vector2((float)base.projectile.width, (float)base.projectile.height) / 2f + Vector2.UnitY * base.projectile.gfxOffY - Main.screenPosition;
			Utils.DrawLaser(Main.spriteBatch, texture2D, vector - Main.screenPosition, vector + base.projectile.velocity * num - Main.screenPosition, vector2, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
			DelegateMethods.c_1 = new Color(255, 250, 205, 127) * 0.75f * base.projectile.Opacity;
			Utils.DrawLaser(Main.spriteBatch, texture2D, vector - Main.screenPosition, vector + base.projectile.velocity * num - Main.screenPosition, vector2 / 2f, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = 2;
			Vector2 velocity = base.projectile.velocity;
			Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + velocity * base.projectile.localAI[1], (float)base.projectile.width * base.projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (projHitbox.Intersects(targetHitbox))
			{
				return new bool?(true);
			}
			float num = 0f;
			if (Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), base.projectile.Center, base.projectile.Center + base.projectile.velocity * base.projectile.localAI[1], 22f * base.projectile.scale, ref num))
			{
				return new bool?(true);
			}
			return new bool?(false);
		}
	}
}
