using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XeniumStavePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			int num = 74;
			int num2 = 20;
			int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
			Main.dust[num3].velocity *= 0f;
			Main.dust[num3].noLight = false;
			Main.dust[num3].noGravity = true;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 15f)
			{
				for (int i = 0; i < num2; i++)
				{
					int num4 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num4].noLight = false;
					Main.dust[num4].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 30f)
			{
				for (int j = 0; j < num2; j++)
				{
					int num5 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)num2 * 6.28f);
					Main.dust[num5].noLight = false;
					Main.dust[num5].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 45f)
			{
				for (int k = 0; k < num2; k++)
				{
					int num6 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)num2 * 6.28f);
					Main.dust[num6].noLight = false;
					Main.dust[num6].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 60f)
			{
				for (int l = 0; l < num2; l++)
				{
					int num7 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)num2 * 6.28f);
					Main.dust[num7].noLight = false;
					Main.dust[num7].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 75f)
			{
				for (int m = 0; m < num2; m++)
				{
					int num8 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num8].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m / (float)num2 * 6.28f);
					Main.dust[num8].noLight = false;
					Main.dust[num8].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 90f)
			{
				for (int n = 0; n < num2; n++)
				{
					int num9 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)n / (float)num2 * 6.28f);
					Main.dust[num9].noLight = false;
					Main.dust[num9].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 105f)
			{
				for (int num10 = 0; num10 < num2; num10++)
				{
					int num11 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num11].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)num10 / (float)num2 * 6.28f);
					Main.dust[num11].noLight = false;
					Main.dust[num11].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 120f)
			{
				for (int num12 = 0; num12 < num2; num12++)
				{
					int num13 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num13].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)num12 / (float)num2 * 6.28f);
					Main.dust[num13].noLight = false;
					Main.dust[num13].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 vector = Vector2.Zero;
			float num14 = 400f;
			bool flag = false;
			for (int num15 = 0; num15 < 200; num15++)
			{
				if (Main.npc[num15].active && !Main.npc[num15].dontTakeDamage && !Main.npc[num15].friendly && Main.npc[num15].lifeMax > 5)
				{
					Vector2 vector2 = Main.npc[num15].Center - base.projectile.Center;
					float num16 = (float)Math.Sqrt((double)(vector2.X * vector2.X + vector2.Y * vector2.Y));
					if (num16 < num14)
					{
						vector = vector2;
						num14 = num16;
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.AdjustMagnitude(ref vector);
				base.projectile.velocity = (10f * base.projectile.velocity + vector) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float num = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (num > 17f)
			{
				vector *= 16f / num;
			}
		}

		public override void Kill(int timeLeft)
		{
			int num = 74;
			int num2 = 20;
			for (int i = 0; i < num2; i++)
			{
				int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)num2 * 6.28f);
				Main.dust[num3].noLight = false;
				Main.dust[num3].noGravity = true;
			}
			Main.PlaySound(SoundID.Item62, (int)base.projectile.position.X, (int)base.projectile.position.Y);
		}
	}
}
