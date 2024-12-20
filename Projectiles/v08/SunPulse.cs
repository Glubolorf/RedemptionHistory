using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class SunPulse : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sun Pulse");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 54;
			base.projectile.height = 54;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 60;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha += 15;
			base.projectile.scale += 0.5f;
			if (base.projectile.localAI[0] == 1f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 27f, base.projectile.position.Y + 27f), base.projectile.velocity, base.mod.ProjectileType("SunPulse2"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}
	}
}
