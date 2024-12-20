using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class SporeFlowerAura : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spore Flower Aura");
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
			base.projectile.alpha = 100;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 3;
			base.projectile.rotation += 0.04f;
			if (base.projectile.localAI[0] >= 60f)
			{
				base.projectile.Kill();
			}
		}
	}
}
