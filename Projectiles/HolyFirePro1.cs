using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class HolyFirePro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Incandesent Flames");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.melee = true;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 30;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<HolyFireDebuff>(), 400, false);
		}

		public override void AI()
		{
			int dust = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 64, 0f, 0f, 100, default(Color), 3f);
			Main.dust[dust].noGravity = true;
		}
	}
}
