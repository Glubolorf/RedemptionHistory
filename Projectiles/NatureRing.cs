using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class NatureRing : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nature Ring");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 422;
			base.projectile.height = 422;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 220;
		}

		public override void AI()
		{
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).creationBonus)
			{
				base.projectile.Kill();
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.rotation += 0.04f;
			Player player = Main.player[base.projectile.owner];
			base.projectile.position.X = player.Center.X - 211f;
			base.projectile.position.Y = player.Center.Y - 211f;
			int num = (int)base.projectile.ai[0];
			Vector2 vector;
			vector..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num2 = Main.player[num].Center.X - vector.X;
			float num3 = Main.player[num].Center.Y - vector.Y;
			Math.Sqrt((double)(num2 * num2 + num3 * num3));
			if (base.projectile.position.X < Main.player[num].position.X + (float)Main.player[num].width && base.projectile.position.X + (float)base.projectile.width > Main.player[num].position.X && base.projectile.position.Y < Main.player[num].position.Y + (float)Main.player[num].height && base.projectile.position.Y + (float)base.projectile.height > Main.player[num].position.Y && base.projectile.owner == Main.myPlayer)
			{
				Main.player[num].AddBuff(base.mod.BuffType("DruidsBlessing"), 2, false);
			}
			if (player.dead)
			{
				base.projectile.Kill();
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
			target.AddBuff(base.mod.BuffType("DruidsBane"), 2, false);
		}
	}
}
