﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class PollenCloud10 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Bell Pollen");
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 170;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 8)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, 262, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			Main.dust[num].noGravity = true;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 3;
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
			float num2 = 400f;
			bool flag = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5)
				{
					Vector2 vector2 = Main.npc[i].Center - base.projectile.Center;
					float num3 = (float)Math.Sqrt((double)(vector2.X * vector2.X + vector2.Y * vector2.Y));
					if (num3 < num2)
					{
						vector = vector2;
						num2 = num3;
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

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).frostburnSeedbag)
			{
				target.AddBuff(44, 160, false);
			}
			target.AddBuff(70, 160, false);
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