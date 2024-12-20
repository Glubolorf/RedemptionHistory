using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class HolyGroundPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Earthquake Starter");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 10;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 1.2f;
			base.projectile.velocity.X = 0f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, ModContent.ProjectileType<HolyGroundPro2>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			return true;
		}
	}
}
