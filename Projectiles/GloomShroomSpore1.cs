using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class GloomShroomSpore1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloom Shroom Spores");
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 32;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 170;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 2;
			base.projectile.rotation += 0.07f;
			if (base.projectile.localAI[0] >= 60f)
			{
				base.projectile.Kill();
			}
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
				if (Main.player[i].active)
				{
					Vector2 vector2 = Main.player[i].Center - base.projectile.Center;
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
				base.projectile.velocity = (10f * base.projectile.velocity + vector) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float num = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (num > 6f)
			{
				vector *= 6f / num;
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(base.mod.BuffType("GloomShroomDebuff"), Main.rand.Next(200, 400), true);
		}
	}
}
