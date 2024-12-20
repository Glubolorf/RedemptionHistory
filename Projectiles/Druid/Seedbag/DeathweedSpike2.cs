using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class DeathweedSpike2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/Druid/Seedbag/DeathweedSpike";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Deathweed Spike");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = true;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.6f;
		}
	}
}
