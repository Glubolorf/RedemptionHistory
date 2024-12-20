using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
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
				int dustType = 206;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
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
				for (int j = 0; j < 200; j++)
				{
					if (Main.npc[j].active && !Main.npc[j].dontTakeDamage && !Main.npc[j].friendly && Main.npc[j].lifeMax > 5)
					{
						Vector2 newMove = Main.npc[j].Center - base.projectile.Center;
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
			if (magnitude > 28f)
			{
				vector *= 27f / magnitude;
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item62, (int)base.projectile.position.X, (int)base.projectile.position.Y);
			int dustType = 206;
			int pieCut = 20;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			if (critChance >= 100 || Main.rand.Next(1, 101) <= critChance)
			{
				crit = true;
			}
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritPierce)
			{
				base.projectile.Kill();
			}
		}
	}
}
