using System;
using Redemption.Buffs.Wasteland;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class GreenGasPro3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Gas");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 40;
			base.projectile.height = 40;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.04f;
			if (base.projectile.localAI[0] >= 120f)
			{
				base.projectile.Kill();
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(ModContent.BuffType<BioweaponDebuff>(), 600, false);
		}
	}
}
