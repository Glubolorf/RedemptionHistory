using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class ShadesoulPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadesoul");
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 20;
			base.projectile.alpha = 255;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.ranged = true;
			base.projectile.timeLeft = 240;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 600f;
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
			int dust = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 0, default(Color), 1f);
			Main.dust[dust].velocity /= 2f;
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 12f)
			{
				vector *= 12f / magnitude;
			}
		}
	}
}
