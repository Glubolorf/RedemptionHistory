using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class StarVortex : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Star Vortex");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 60;
			base.projectile.height = 60;
			base.projectile.penetrate = -1;
			base.projectile.aiStyle = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.scale = 0.05f;
			base.projectile.timeLeft = 320;
		}

		public override void AI()
		{
			base.projectile.rotation += 0.03f;
			base.projectile.velocity *= 0f;
			if (Main.netMode != 1)
			{
				base.projectile.ai[1] += 1f;
			}
			if (base.projectile.ai[0] == 0f)
			{
				if (base.projectile.scale < 2f)
				{
					base.projectile.scale += 0.05f;
				}
				if (base.projectile.alpha > 0)
				{
					base.projectile.alpha -= 5;
				}
				if (base.projectile.ai[1] > 240f)
				{
					base.projectile.ai[0] = 1f;
					base.projectile.ai[1] = 0f;
					base.projectile.netUpdate = true;
				}
			}
			else
			{
				if (base.projectile.scale > 0f)
				{
					base.projectile.scale -= 0.05f;
				}
				if (base.projectile.alpha < 255)
				{
					base.projectile.alpha += 5;
				}
				if (base.projectile.ai[1] > 30f)
				{
					base.projectile.active = false;
					base.projectile.netUpdate = true;
				}
			}
			for (int u = 0; u < 255; u++)
			{
				Player target = Main.player[u];
				if (target.active && Vector2.Distance(base.projectile.Center, target.Center) < 450f)
				{
					float num8 = 10f;
					Vector2 vector = new Vector2(target.position.X + (float)(target.width / 2), target.position.Y + (float)(target.height / 2));
					float num4 = base.projectile.Center.X - vector.X;
					float num5 = base.projectile.Center.Y - vector.Y;
					float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
					num6 = num8 / num6;
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
					else if (Vector2.Distance(base.projectile.Center, target.Center) < 300f)
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
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 3000f;
			bool targetted = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.player[i].active)
				{
					Vector2 newMove = Main.player[i].Center - base.projectile.Center;
					float distanceTo = (float)Math.Sqrt((double)(newMove.X * newMove.X + newMove.Y * newMove.Y));
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						targetted = true;
					}
				}
			}
			if (targetted)
			{
				this.AdjustMagnitude(ref move);
				base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 2f)
			{
				vector *= 2f / magnitude;
			}
		}

		public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
		{
			Texture2D Tex = Main.projectileTexture[base.projectile.type];
			Texture2D Vortex = base.mod.GetTexture("NPCs/Bosses/Neb/StarVortex2");
			Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
			BaseDrawing.DrawTexture(spritebatch, Vortex, 0, base.projectile.position, base.projectile.width, base.projectile.height, base.projectile.scale, base.projectile.rotation, 0, 1, frame, new Color?(base.projectile.GetAlpha(RedeColor.COLOR_GLOWPULSE)), true, default(Vector2));
			BaseDrawing.DrawTexture(spritebatch, Tex, 0, base.projectile.position, base.projectile.width, base.projectile.height, base.projectile.scale, -base.projectile.rotation, 0, 1, frame, new Color?(base.projectile.GetAlpha(RedeColor.COLOR_GLOWPULSE)), true, default(Vector2));
			return false;
		}
	}
}
