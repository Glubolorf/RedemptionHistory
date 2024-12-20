using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class AngelicArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Angelic Arrow");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 10)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 vector = Vector2.Zero;
			float num = 400f;
			bool flag = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5)
				{
					Vector2 vector2 = Main.npc[i].Center - base.projectile.Center;
					float num2 = (float)Math.Sqrt((double)(vector2.X * vector2.X + vector2.Y * vector2.Y));
					if (num2 < num)
					{
						vector = vector2;
						num = num2;
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.AdjustMagnitude(ref vector);
				base.projectile.velocity = (18f * base.projectile.velocity + vector) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0.9f)
			{
				base.projectile.velocity.X = oldVelocity.X * -0.9f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0.9f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * -0.9f;
			}
			return false;
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float num = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (num > 7f)
			{
				vector *= 7f / num;
			}
		}
	}
}
