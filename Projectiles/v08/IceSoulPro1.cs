using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class IceSoulPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chilling Soul");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(359);
			this.aiType = 359;
			base.projectile.width = 16;
			base.projectile.height = 16;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
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
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float num = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (num > 9f)
			{
				vector *= 10f / num;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(1) == 0))
			{
				target.AddBuff(44, 80, false);
			}
		}
	}
}
