using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SpiritFlame : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Flame");
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.penetrate = 3;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 50;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).spiritPierce)
			{
				base.projectile.penetrate = 6;
			}
			int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, 68, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			Main.dust[num].noGravity = true;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			base.projectile.localAI[0] += 1f;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).spiritHoming)
			{
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
					base.projectile.velocity = (14f * base.projectile.velocity + vector) / 11f;
					this.AdjustMagnitude(ref base.projectile.velocity);
				}
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float num = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (num > 8f)
			{
				vector *= 9f / num;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int crit2 = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref crit2);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref crit2);
			if (crit2 >= 100 || Main.rand.Next(1, 101) <= crit2)
			{
				crit = true;
			}
		}
	}
}
