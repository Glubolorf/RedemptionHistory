using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class SoulSkull : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul Skull");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.aiStyle = 0;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 350;
			base.projectile.penetrate = 2;
			base.projectile.alpha = 60;
			base.projectile.friendly = true;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).spiritPierce)
			{
				base.projectile.penetrate = 6;
			}
			if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
			{
				base.projectile.spriteDirection = -base.projectile.direction;
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Vector2 vector = new Vector2(base.projectile.ai[0], base.projectile.ai[1]) - base.projectile.Center;
			if ((double)base.projectile.timeLeft < 275.0)
			{
				base.projectile.Kill();
			}
			if ((double)vector.Length() < (double)base.projectile.velocity.Length())
			{
				base.projectile.Kill();
			}
			else
			{
				vector.Normalize();
				base.projectile.velocity = Vector2.Lerp(base.projectile.velocity, vector * 11.2f, 0.1f);
			}
			base.projectile.netUpdate = true;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 1f)
			{
				int num = 206;
				int num2 = 8;
				for (int i = 0; i < num2; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num3].noLight = false;
					Main.dust[num3].noGravity = true;
				}
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).spiritHoming)
			{
				if (base.projectile.localAI[0] == 0f)
				{
					this.AdjustMagnitude(ref base.projectile.velocity);
					base.projectile.localAI[0] = 1f;
				}
				Vector2 vector2 = Vector2.Zero;
				float num4 = 400f;
				bool flag = false;
				for (int j = 0; j < 200; j++)
				{
					if (Main.npc[j].active && !Main.npc[j].dontTakeDamage && !Main.npc[j].friendly && Main.npc[j].lifeMax > 5)
					{
						Vector2 vector3 = Main.npc[j].Center - base.projectile.Center;
						float num5 = (float)Math.Sqrt((double)(vector3.X * vector3.X + vector3.Y * vector3.Y));
						if (num5 < num4)
						{
							vector2 = vector3;
							num4 = num5;
							flag = true;
						}
					}
				}
				if (flag)
				{
					this.AdjustMagnitude(ref vector2);
					base.projectile.velocity = (10f * base.projectile.velocity + vector2) / 11f;
					this.AdjustMagnitude(ref base.projectile.velocity);
				}
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float num = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (num > 28f)
			{
				vector *= 27f / num;
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item62, (int)base.projectile.position.X, (int)base.projectile.position.Y);
			int num = 206;
			int num2 = 20;
			for (int i = 0; i < num2; i++)
			{
				int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)num2 * 6.28f);
				Main.dust[num3].noLight = false;
				Main.dust[num3].noGravity = true;
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
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).spiritPierce)
			{
				base.projectile.Kill();
			}
		}
	}
}
