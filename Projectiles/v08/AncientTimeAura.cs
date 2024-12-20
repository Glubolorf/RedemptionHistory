using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class AncientTimeAura : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Time Aura");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 126;
			base.projectile.height = 126;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 254;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.06f;
			if (base.projectile.localAI[0] <= 60f)
			{
				base.projectile.alpha -= 4;
			}
			if (base.projectile.localAI[0] >= 180f)
			{
				base.projectile.alpha += 4;
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			if (RedeHelper.ClosestNPC(ref this.target, 10000f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] > 60f && base.projectile.localAI[0] < 180f && base.projectile.localAI[0] % 10f == 0f)
			{
				Projectile.NewProjectile(base.projectile.Center, RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), base.mod.ProjectileType("SunshardSpark"), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
			}
		}

		private NPC target;
	}
}
