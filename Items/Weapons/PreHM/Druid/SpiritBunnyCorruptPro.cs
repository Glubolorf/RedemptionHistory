﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid
{
	public class SpiritBunnyCorruptPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Spirit Bunny");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 32;
			base.projectile.penetrate = 3;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.alpha = 60;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			base.projectile.usesLocalNPCImmunity = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			base.projectile.localNPCImmunity[target.whoAmI] = 10;
			target.immune[base.projectile.owner] = 0;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritPierce)
			{
				base.projectile.penetrate = 6;
			}
			if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
			{
				base.projectile.spriteDirection = -base.projectile.direction;
			}
			int DustID2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
			Main.dust[DustID2].noGravity = true;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 4;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.localAI[0] > 40f)
			{
				base.projectile.Kill();
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritHoming)
			{
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
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 6f)
			{
				vector *= 11f / magnitude;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(39, 180, false);
		}
	}
}