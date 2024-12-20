using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
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
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = false;
		}

		public override void AI()
		{
			int dustType = 74;
			int pieCut = 20;
			int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
			Main.dust[dustID].velocity *= 0f;
			Main.dust[dustID].noLight = false;
			Main.dust[dustID].noGravity = true;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 15f)
			{
				for (int i = 0; i < pieCut; i++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 30f)
			{
				for (int j = 0; j < pieCut; j++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)pieCut * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 45f)
			{
				for (int k = 0; k < pieCut; k++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)pieCut * 6.28f);
					Main.dust[dustID4].noLight = false;
					Main.dust[dustID4].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 60f)
			{
				for (int l = 0; l < pieCut; l++)
				{
					int dustID5 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)pieCut * 6.28f);
					Main.dust[dustID5].noLight = false;
					Main.dust[dustID5].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 75f)
			{
				for (int m = 0; m < pieCut; m++)
				{
					int dustID6 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m / (float)pieCut * 6.28f);
					Main.dust[dustID6].noLight = false;
					Main.dust[dustID6].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 90f)
			{
				for (int n = 0; n < pieCut; n++)
				{
					int dustID7 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)n / (float)pieCut * 6.28f);
					Main.dust[dustID7].noLight = false;
					Main.dust[dustID7].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 105f)
			{
				for (int m2 = 0; m2 < pieCut; m2++)
				{
					int dustID8 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID8].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m2 / (float)pieCut * 6.28f);
					Main.dust[dustID8].noLight = false;
					Main.dust[dustID8].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 120f)
			{
				for (int m3 = 0; m3 < pieCut; m3++)
				{
					int dustID9 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m3 / (float)pieCut * 6.28f);
					Main.dust[dustID9].noLight = false;
					Main.dust[dustID9].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 200f;
			bool target = false;
			for (int k2 = 0; k2 < 200; k2++)
			{
				if (Main.npc[k2].active && !Main.npc[k2].dontTakeDamage && !Main.npc[k2].friendly && Main.npc[k2].lifeMax > 5 && !Main.npc[k2].immortal)
				{
					Vector2 newMove = Main.npc[k2].Center - base.projectile.Center;
					float distanceTo = (float)Math.Sqrt((double)(newMove.X * newMove.X + newMove.Y * newMove.Y));
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				this.AdjustMagnitude(ref move);
				base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 17f)
			{
				vector *= 16f / magnitude;
			}
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 74;
			int pieCut = 20;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			Main.PlaySound(SoundID.Item62, (int)base.projectile.position.X, (int)base.projectile.position.Y);
		}
	}
}
