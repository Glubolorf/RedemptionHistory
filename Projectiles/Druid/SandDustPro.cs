using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class SandDustPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dust Cloud");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 50;
			base.projectile.height = 50;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 180;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 32, 0f, 0f, 100, default(Color), 3f);
			Main.dust[dustIndex].velocity *= 1.1f;
			Main.dust[dustIndex].noGravity = true;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 30f)
			{
				base.projectile.velocity *= 0.8f;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 4;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(ModContent.BuffType<SandDustDebuff>(), 60, false);
		}
	}
}
