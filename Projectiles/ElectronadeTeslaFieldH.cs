using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class ElectronadeTeslaFieldH : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/ElectronadeTeslaField";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tesla Field");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 100;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 100f)
			{
				base.projectile.scale -= 0.02f;
			}
		}
	}
}
