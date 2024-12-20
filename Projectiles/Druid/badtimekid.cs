using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class badtimekid : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sans the ABSOLUTE UNIT");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 300;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			base.projectile.rotation += 0.16f;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 4f)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/sans").WithVolume(0.8f).WithPitchVariance(0.1f), base.projectile.position);
				base.projectile.localAI[0] = 0f;
			}
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 800f;
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
			if (magnitude > 41f)
			{
				vector *= 40f / magnitude;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 1;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(ModContent.BuffType<sansDebuff>(), 60, false);
		}
	}
}
