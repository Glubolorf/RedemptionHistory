using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DayPulse : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Day Pulse");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 114;
			base.projectile.height = 114;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 150;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 10)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha += 2;
			base.projectile.velocity *= 0.01f;
			base.projectile.rotation += 0.03f;
			if (base.projectile.localAI[0] >= 50f)
			{
				base.projectile.Kill();
			}
			int num = (int)base.projectile.ai[0];
			Vector2 vector;
			vector..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num2 = Main.player[num].Center.X - vector.X;
			float num3 = Main.player[num].Center.Y - vector.Y;
			float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
			if (num4 < 114f && base.projectile.position.X < Main.player[num].position.X + (float)Main.player[num].width && base.projectile.position.X + (float)base.projectile.width > Main.player[num].position.X && base.projectile.position.Y < Main.player[num].position.Y + (float)Main.player[num].height && base.projectile.position.Y + (float)base.projectile.height > Main.player[num].position.Y && base.projectile.owner == Main.myPlayer)
			{
				Main.player[num].AddBuff(146, 600, false);
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).frostburnSeedbag)
			{
				target.AddBuff(44, 160, false);
			}
		}
	}
}
