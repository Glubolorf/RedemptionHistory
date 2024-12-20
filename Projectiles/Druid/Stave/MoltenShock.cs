using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class MoltenShock : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Molten Eruption");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 102;
			base.projectile.penetrate = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 600;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 6)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 6)
				{
					base.projectile.Kill();
				}
			}
			if (base.projectile.frame >= 1 && base.projectile.frame <= 4)
			{
				base.projectile.friendly = true;
			}
			Projectile projectile3 = base.projectile;
			projectile3.velocity.X = projectile3.velocity.X * 0f;
			Projectile projectile4 = base.projectile;
			projectile4.velocity.Y = projectile4.velocity.Y * 0f;
		}
	}
}
