using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class XenoSynthPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Synth");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 180;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = true;
		}

		public override void AI()
		{
			if (this.switchSize)
			{
				base.projectile.localAI[0] -= 1f;
			}
			else
			{
				base.projectile.localAI[0] += 1f;
			}
			int dustType = 74;
			int pieCut = 16;
			if (base.projectile.localAI[0] <= 0f)
			{
				this.switchSize = false;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 5f)
			{
				for (int j = 0; j < pieCut; j++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)j / (float)pieCut * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 10f)
			{
				for (int k = 0; k < pieCut; k++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)k / (float)pieCut * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 15f)
			{
				for (int l = 0; l < pieCut; l++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(3f, 0f), (float)l / (float)pieCut * 6.28f);
					Main.dust[dustID4].noLight = false;
					Main.dust[dustID4].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] >= 20f)
			{
				for (int m = 0; m < pieCut; m++)
				{
					int dustID5 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(2f, 0f), (float)m / (float)pieCut * 6.28f);
					Main.dust[dustID5].noLight = false;
					Main.dust[dustID5].noGravity = true;
				}
				this.switchSize = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X)
			{
				base.projectile.velocity.X = -oldVelocity.X;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y)
			{
				base.projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 7;
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 74;
			int pieCut = 16;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}

		public bool switchSize;
	}
}
