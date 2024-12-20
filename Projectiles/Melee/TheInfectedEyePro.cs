using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class TheInfectedEyePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Infected Eye");
		}

		public override void SetDefaults()
		{
			base.projectile.extraUpdates = 0;
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.aiStyle = 99;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			ProjectileID.Sets.YoyosLifeTimeMultiplier[base.projectile.type] = 14f;
			ProjectileID.Sets.YoyosMaximumRange[base.projectile.type] = 300f;
			ProjectileID.Sets.YoyosTopSpeed[base.projectile.type] = 17f;
		}

		public override void AI()
		{
			if (Main.rand.Next(7) == 0)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 273, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}
	}
}
