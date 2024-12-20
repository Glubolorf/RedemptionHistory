using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class UkkoLightning : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lightning");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.alpha = 255;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 4;
			base.projectile.timeLeft = 120 * (base.projectile.extraUpdates + 1);
			base.projectile.usesLocalNPCImmunity = true;
			base.projectile.localNPCHitCooldown = 0;
			base.projectile.penetrate = -1;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				projHitbox.X = (int)base.projectile.oldPos[i].X;
				projHitbox.Y = (int)base.projectile.oldPos[i].Y;
				if (projHitbox.Intersects(targetHitbox))
				{
					return new bool?(true);
				}
			}
			return base.Colliding(projHitbox, targetHitbox);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.localAI[1] < 1f)
			{
				base.projectile.localAI[1] += 2f;
				base.projectile.position += base.projectile.velocity;
				base.projectile.velocity = Vector2.Zero;
			}
			return false;
		}

		public override void AI()
		{
			base.projectile.numUpdates = base.projectile.extraUpdates;
			while (base.projectile.numUpdates >= 0)
			{
				base.projectile.numUpdates--;
				if (base.projectile.frameCounter == 0 || base.projectile.oldPos[0] == Vector2.Zero)
				{
					for (int num31 = base.projectile.oldPos.Length - 1; num31 > 0; num31--)
					{
						base.projectile.oldPos[num31] = base.projectile.oldPos[num31 - 1];
					}
					base.projectile.oldPos[0] = base.projectile.position;
					float num32 = base.projectile.rotation + 1.5707964f + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.5707964f;
					float num33 = (float)Main.rand.NextDouble() * 2f + 2f;
					Vector2 vector2 = new Vector2((float)Math.Cos((double)num32) * num33, (float)Math.Sin((double)num32) * num33);
					int num34 = Dust.NewDust(base.projectile.oldPos[base.projectile.oldPos.Length - 1], 0, 0, 226, vector2.X, vector2.Y, 0, default(Color), 1f);
					Main.dust[num34].noGravity = true;
					Main.dust[num34].scale = 1.7f;
				}
			}
			base.projectile.frameCounter++;
			Lighting.AddLight(base.projectile.Center, (float)(Color.LightYellow.R / byte.MaxValue), (float)(Color.LightYellow.G / byte.MaxValue), (float)(Color.LightYellow.B / byte.MaxValue));
			if (base.projectile.velocity == Vector2.Zero)
			{
				if (base.projectile.frameCounter >= base.projectile.extraUpdates * 2)
				{
					base.projectile.frameCounter = 0;
					bool flag35 = true;
					for (int num35 = 1; num35 < base.projectile.oldPos.Length; num35++)
					{
						if (base.projectile.oldPos[num35] != base.projectile.oldPos[0])
						{
							flag35 = false;
						}
					}
					if (flag35)
					{
						base.projectile.Kill();
						return;
					}
				}
				if (Main.rand.Next(base.projectile.extraUpdates) == 0)
				{
					for (int num36 = 0; num36 < 2; num36++)
					{
						float num37 = base.projectile.rotation + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.5707964f;
						float num38 = (float)Main.rand.NextDouble() * 0.8f + 1f;
						Vector2 vector3 = new Vector2((float)Math.Cos((double)num37) * num38, (float)Math.Sin((double)num37) * num38);
						int num39 = Dust.NewDust(base.projectile.Center, 0, 0, 226, vector3.X, vector3.Y, 0, default(Color), 1f);
						Main.dust[num39].noGravity = true;
						Main.dust[num39].scale = 1.2f;
					}
					if (Main.rand.Next(5) == 0)
					{
						Vector2 value49 = Utils.RotatedBy(base.projectile.velocity, 1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.projectile.width;
						int num40 = Dust.NewDust(base.projectile.Center + value49 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[num40].velocity *= 0.5f;
						Main.dust[num40].velocity.Y = -Math.Abs(Main.dust[num40].velocity.Y);
						return;
					}
				}
			}
			else if (base.projectile.frameCounter >= base.projectile.extraUpdates * 2)
			{
				base.projectile.frameCounter = 0;
				float num41 = base.projectile.velocity.Length();
				UnifiedRandom unifiedRandom = new UnifiedRandom((int)base.projectile.ai[1]);
				int num42 = 0;
				Vector2 spinningpoint2 = -Vector2.UnitY;
				Vector2 vector4;
				do
				{
					int num43 = unifiedRandom.Next();
					base.projectile.ai[1] = (float)num43;
					num43 %= 100;
					vector4 = Utils.ToRotationVector2((float)num43 / 100f * 6.2831855f);
					if (vector4.Y > 0f)
					{
						vector4.Y *= -1f;
					}
					bool flag36 = false;
					if (vector4.Y > -0.02f)
					{
						flag36 = true;
					}
					if (vector4.X * (float)(base.projectile.extraUpdates + 1) * 2f * num41 + base.projectile.localAI[0] > 40f)
					{
						flag36 = true;
					}
					if (vector4.X * (float)(base.projectile.extraUpdates + 1) * 2f * num41 + base.projectile.localAI[0] < -40f)
					{
						flag36 = true;
					}
					if (!flag36)
					{
						goto IL_5DE;
					}
				}
				while (num42++ < 100);
				base.projectile.velocity = Vector2.Zero;
				base.projectile.localAI[1] = 1f;
				goto IL_5E2;
				IL_5DE:
				spinningpoint2 = vector4;
				IL_5E2:
				if (base.projectile.velocity != Vector2.Zero)
				{
					base.projectile.localAI[0] += spinningpoint2.X * (float)(base.projectile.extraUpdates + 1) * 2f * num41;
					base.projectile.velocity = Utils.RotatedBy(spinningpoint2, (double)(base.projectile.ai[0] + 1.5707964f), default(Vector2)) * num41;
					base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + 1.5707964f;
					return;
				}
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(144, target.HasBuff(103) ? 320 : 160, true);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Color color25 = Lighting.GetColor((int)((double)base.projectile.position.X + (double)base.projectile.width * 0.5) / 16, (int)(((double)base.projectile.position.Y + (double)base.projectile.height * 0.5) / 16.0));
			Vector2 end = base.projectile.position + new Vector2((float)base.projectile.width, (float)base.projectile.height) / 2f + Vector2.UnitY * base.projectile.gfxOffY - Main.screenPosition;
			Texture2D tex3 = Main.extraTexture[33];
			base.projectile.GetAlpha(color25);
			Vector2 scale16 = new Vector2(base.projectile.scale) / 2f;
			for (int num291 = 0; num291 < 3; num291++)
			{
				if (num291 == 0)
				{
					scale16 = new Vector2(base.projectile.scale) * 0.6f;
					DelegateMethods.c_1 = new Color(255, 255, 255) * 0.5f;
				}
				else if (num291 == 1)
				{
					scale16 = new Vector2(base.projectile.scale) * 0.4f;
					DelegateMethods.c_1 = Color.LightYellow * 0.5f;
				}
				else
				{
					scale16 = new Vector2(base.projectile.scale) * 0.2f;
					DelegateMethods.c_1 = Color.LightYellow * 0.5f;
				}
				DelegateMethods.f_1 = 1f;
				for (int num292 = base.projectile.oldPos.Length - 1; num292 > 0; num292--)
				{
					if (!(base.projectile.oldPos[num292] == Vector2.Zero))
					{
						Vector2 start = base.projectile.oldPos[num292] + new Vector2((float)base.projectile.width, (float)base.projectile.height) / 2f + Vector2.UnitY * base.projectile.gfxOffY - Main.screenPosition;
						Vector2 end2 = base.projectile.oldPos[num292 - 1] + new Vector2((float)base.projectile.width, (float)base.projectile.height) / 2f + Vector2.UnitY * base.projectile.gfxOffY - Main.screenPosition;
						Utils.DrawLaser(Main.spriteBatch, tex3, start, end2, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
					}
				}
				if (base.projectile.oldPos[0] != Vector2.Zero)
				{
					DelegateMethods.f_1 = 1f;
					Vector2 start2 = base.projectile.oldPos[0] + new Vector2((float)base.projectile.width, (float)base.projectile.height) / 2f + Vector2.UnitY * base.projectile.gfxOffY - Main.screenPosition;
					Utils.DrawLaser(Main.spriteBatch, tex3, start2, end, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
				}
			}
			return false;
		}
	}
}
