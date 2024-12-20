using System;
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
			if (Main.myPlayer == base.projectile.owner)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active && !player2.dead && base.projectile.getRect().Intersects(player2.getRect()))
					{
						player2.AddBuff(base.mod.BuffType("DruidsBlessing"), 2, false);
						return;
					}
				}
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(base.mod.BuffType("DruidsBane"), 2, false);
		}
	}
}
