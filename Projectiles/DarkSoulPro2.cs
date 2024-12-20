using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DarkSoulPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Soul");
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 26;
			base.projectile.alpha = 255;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.magic = true;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.alpha > 70)
			{
				base.projectile.alpha -= 15;
				if (base.projectile.alpha < 70)
				{
					base.projectile.alpha = 70;
				}
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
				base.projectile.velocity = (10f * base.projectile.velocity + vector) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
			if (base.projectile.alpha <= 100)
			{
				int num3 = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, base.mod.DustType("VoidFlame"), 0f, 0f, 0, default(Color), 1f);
				Main.dust[num3].velocity /= 2f;
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
	}
}
