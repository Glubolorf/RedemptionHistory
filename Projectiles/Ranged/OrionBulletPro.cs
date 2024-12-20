using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class OrionBulletPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Orion's Bullet");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 6;
			base.projectile.height = 6;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			int Dust = Dust.NewDust(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), 1, 1, 27, 0f, 0f, 100, default(Color), 2f);
			Main.dust[Dust].noGravity = true;
			Dust dust = Main.dust[Dust];
			dust.velocity.X = 0f;
			dust.velocity.Y = 0f;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5 && !Main.npc[i].immortal)
				{
					Vector2 newMove = Main.npc[i].Center - base.projectile.Center;
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
			if (magnitude > 16f)
			{
				vector *= 16f / magnitude;
			}
		}
	}
}
