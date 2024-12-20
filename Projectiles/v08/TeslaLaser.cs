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
			Vector2? vector71 = null;
			if (Utils.HasNaNs(base.projectile.velocity) || base.projectile.velocity == Vector2.Zero)
			{
				base.projectile.velocity = -Vector2.UnitY;
			}
			if (base.projectile.type != base.mod.ProjectileType("TeslaLaser") || !Main.projectile[(int)base.projectile.ai[1]].active || Main.projectile[(int)base.projectile.ai[1]].type != base.mod.ProjectileType("TeslaCoilStaffPro"))
			{
				base.projectile.Kill();
				return;
			}
			float num810 = (float)((int)base.projectile.ai[0]) - 2.5f;
			Vector2 value36 = Vector2.Normalize(Main.projectile[(int)base.projectile.ai[1]].velocity);
			Projectile projectile2 = Main.projectile[(int)base.projectile.ai[1]];
			float num811 = num810 * 0.5235988f;
			Vector2 value37 = Vector2.Zero;
			float num812;
			float y;
			float num813;
			float scaleFactor6;
			if (projectile2.ai[0] < 180f)
			{
				num812 = 1f - projectile2.ai[0] / 180f;
				y = 20f - projectile2.ai[0] / 180f * 14f;
				if (projectile2.ai[0] < 120f)
				{
					num813 = 20f - 4f * (projectile2.ai[0] / 120f);
					base.projectile.Opacity = projectile2.ai[0] / 120f * 0.4f;
				}
				else
				{
					num813 = 16f - 10f * ((projectile2.ai[0] - 120f) / 60f);
					base.projectile.Opacity = 0.4f + (projectile2.ai[0] - 120f) / 60f * 0.6f;
				}
				scaleFactor6 = -22f + projectile2.ai[0] / 180f * 20f;
			}
			else
			{
				num812 = 0f;
				num813 = 1.75f;
				y = 6f;
				base.projectile.Opacity = 1f;
				scaleFactor6 = -2f;
			}
			float num814 = (projectile2.ai[0] + num810 * num813) / (num813 * 6f) * 6.2831855f;
			num811 = Utils.RotatedBy(Vector2.UnitY, (double)num814, default(Vector2)).Y * 0.5235988f * num812;
			value37 = Utils.RotatedBy(Utils.RotatedBy(Vector2.UnitY, (double)num814, default(Vector2)) * new Vector2(4f, y), (double)Utils.ToRotation(projectile2.velocity), default(Vector2));
			base.projectile.position = projectile2.Center + value36 * 16f - base.projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)base.projectile.ai[1]].gfxOffY);
			base.projectile.position += Utils.ToRotationVector2(Utils.ToRotation(projectile2.velocity)) * scaleFactor6;
			base.projectile.position += value37;
			base.projectile.velocity = Utils.RotatedBy(Vector2.Normalize(projectile2.velocity), (double)num811, default(Vector2));
			base.projectile.scale = 1.8f * (1f - num812);
			base.projectile.damage = projectile2.damage;
			if (projectile2.ai[0] >= 180f)
			{
				base.projectile.damage *= 3;
				vector71 = new Vector2?(projectile2.Center);
			}
			if (!Collision.CanHitLine(Main.player[base.projectile.owner].Center, 0, 0, projectile2.Center, 0, 0))
			{
				vector71 = new Vector2?(Main.player[base.projectile.owner].Center);
			}
			base.projectile.friendly = (projectile2.ai[0] > 30f);
			if (Utils.HasNaNs(base.projectile.velocity) || base.projectile.velocity == Vector2.Zero)
			{
				base.projectile.velocity = -Vector2.UnitY;
			}
			float num815 = Utils.ToRotation(base.projectile.velocity);
			base.projectile.rotation = num815 - 1.5707964f;
			base.projectile.velocity = Utils.ToRotationVector2(num815);
			float num816 = 2f;
			float num817 = 0f;
			Vector2 samplingPoint = base.projectile.Center;
			if (vector71 != null)
			{
				samplingPoint = vector71.Value;
			}
			float[] array3 = new float[(int)num816];
			Collision.LaserScan(samplingPoint, base.projectile.velocity, num817 * base.projectile.scale, 2400f, array3);
			float num818 = 0f;
			for (int num819 = 0; num819 < array3.Length; num819++)
			{
				num818 += array3[num819];
			}
			num818 /= num816;
			float amount = 0.75f;
			base.projectile.localAI[1] = MathHelper.Lerp(base.projectile.localAI[1], num818, amount);
			if (Math.Abs(base.projectile.localAI[1] - num818) < 100f && base.projectile.scale > 0.15f)
			{
				Color color = Main.hslToRgb(0.54f, 1f, 0.902f);
				color.A = 0;
				Vector2 vector72 = base.projectile.Center + base.projectile.velocity * (base.projectile.localAI[1] - 14.5f * base.projectile.scale);
				float x = Main.rgbToHsl(new Color(255, 250, 205)).X;
				for (int num820 = 0; num820 < 2; num820++)
				{
					float num821 = Utils.ToRotation(base.projectile.velocity) + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.5707964f;
					float num822 = (float)Main.rand.NextDouble() * 0.8f + 1f;
					Vector2 vector73 = new Vector2((float)Math.Cos((double)num821) * num822, (float)Math.Sin((double)num821) * num822);
					int num823 = Dust.NewDust(vector72, 0, 0, 261, vector73.X, vector73.Y, 0, new Color(255, 250, 205), 1f);
					Main.dust[num823].color = color;
					Main.dust[num823].scale = 1.1f;
					if (base.projectile.scale > 1f)
					{
						Main.dust[num823].velocity *= base.projectile.scale;
						Main.dust[num823].scale *= base.projectile.scale;
					}
					Main.dust[num823].noGravity = true;
					if (base.projectile.scale != 1.4f)
					{
						Dust dust = Dust.CloneDust(num823);
						dust.color = Color.Orange;
						dust.scale /= 2f;
					}
					float num826 = (x + Utils.NextFloat(Main.rand) * 0.4f) % 1f;
					Main.dust[num823].color = Color.Lerp(color, Main.hslToRgb(0.54f, 1f, 0.902f), base.projectile.scale / 1.4f);
				}
				if (Main.rand.Next(5) == 0)
				{
					Vector2 value38 = Utils.RotatedBy(base.projectile.velocity, 1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.projectile.width;
					int num824 = Dust.NewDust(vector72 + value38 - Vector2.One * 4f, 8, 8, 261, 0f, 0f, 100, new Color(255, 250, 205), 1f);
					Main.dust[num824].velocity *= 0.5f;
					Main.dust[num824].velocity.Y = -Math.Abs(Main.dust[num824].velocity.Y);
				}
				DelegateMethods.v3_1 = color.ToVector3() * 0.3f;
				float value39 = 0.1f * (float)Math.Sin((double)(Main.GlobalTime * 20f));
				Vector2 size = new Vector2(base.projectile.velocity.Length() * base.projectile.localAI[1], (float)base.projectile.width * base.projectile.scale);
				float num825 = Utils.ToRotation(base.projectile.velocity);
				if (Main.netMode != 2)
				{
					((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(base.projectile.position + Utils.RotatedBy(new Vector2(size.X * 0.5f, 0f), (double)num825, default(Vector2)), new Color(0.5f, 0.1f * (float)Math.Sign(value39) + 0.5f, 0f, 1f) * Math.Abs(value39), size, 1, num825);
				}
				Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + base.projectile.velocity * base.projectile.localAI[1], (float)base.projectile.width * base.projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
				return;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (base.projectile.velocity == Vector2.Zero)
			{
				return false;
			}
			Texture2D tex = Main.projectileTexture[base.projectile.type];
			float num228 = base.projectile.localAI[1];
			Color value25 = Main.hslToRgb(0.54f, 1f, 0.902f);
			value25.A = 0;
			Vector2 value26 = Utils.Floor(base.projectile.Center);
			value26 += base.projectile.velocity * base.projectile.scale * 10.5f;
			num228 -= base.projectile.scale * 14.5f * base.projectile.scale;
			Vector2 vector29 = new Vector2(base.projectile.scale);
			DelegateMethods.f_1 = 1f;
			DelegateMethods.c_1 = value25 * 0.75f * base.projectile.Opacity;
			Vector2[] oldPos = base.projectile.oldPos;
			new Vector2((float)base.projectile.width, (float)base.projectile.height) / 2f + Vector2.UnitY * base.projectile.gfxOffY - Main.screenPosition;
			Utils.DrawLaser(Main.spriteBatch, tex, value26 - Main.screenPosition, value26 + base.projectile.velocity * num228 - Main.screenPosition, vector29, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
			DelegateMethods.c_1 = new Color(255, 250, 205, 127) * 0.75f * base.projectile.Opacity;
			Utils.DrawLaser(Main.spriteBatch, tex, value26 - Main.screenPosition, value26 + base.projectile.velocity * num228 - Main.screenPosition, vector29 / 2f, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = 2;
			Vector2 unit = base.projectile.velocity;
			Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + unit * base.projectile.localAI[1], (float)base.projectile.width * base.projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (projHitbox.Intersects(targetHitbox))
			{
				return new bool?(true);
			}
			float num6 = 0f;
			if (Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), base.projectile.Center, base.projectile.Center + base.projectile.velocity * base.projectile.localAI[1], 22f * base.projectile.scale, ref num6))
			{
				return new bool?(true);
			}
			return new bool?(false);
		}
	}
}
