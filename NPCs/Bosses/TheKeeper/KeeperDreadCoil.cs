using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	public class KeeperDreadCoil : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dread Coil");
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 34;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 4)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
			{
				base.projectile.spriteDirection = -base.projectile.direction;
			}
			if (base.projectile.localAI[0] == 0f)
			{
				for (int i = 0; i < 8; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 173, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / 8f * 6.28f);
					Main.dust[dustID].noGravity = true;
				}
				KeeperDreadCoil.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			if (Utils.NextBool(Main.rand, 6))
			{
				int dust = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 173, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + 1.57f;
			float[] localAI = base.projectile.localAI;
			int num2 = 1;
			float num3 = localAI[num2] + 1f;
			localAI[num2] = num3;
			if (num3 < 30f)
			{
				Vector2 move = Vector2.Zero;
				float distance = 400f;
				bool target = false;
				for (int j = 0; j < 200; j++)
				{
					if (Main.player[j].active && !Main.player[j].dead)
					{
						Vector2 newMove = Main.player[j].Center - base.projectile.Center;
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
					KeeperDreadCoil.AdjustMagnitude(ref move);
					base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
					KeeperDreadCoil.AdjustMagnitude(ref base.projectile.velocity);
				}
			}
		}

		private static void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 4f)
			{
				vector *= 8f / magnitude;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.White);
		}
	}
}
