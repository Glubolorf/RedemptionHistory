using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	internal class MadnessSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[base.projectile.type] = 28;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(595);
			this.aiType = 595;
			base.projectile.width = 68;
			base.projectile.height = 64;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 60;
			base.projectile.penetrate = -1;
		}

		public override void AI()
		{
			int num = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, base.mod.DustType("VlitchFlame"), base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(200, 0, 0, 0) * (1f - (float)base.projectile.alpha / 255f));
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
