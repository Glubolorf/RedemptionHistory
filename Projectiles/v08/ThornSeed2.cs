using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class ThornSeed2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Thorn Seed");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 6;
			base.projectile.height = 6;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 120;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 1f;
			base.projectile.velocity.X = 0f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			int proj = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 36f, 0f, 0f, base.mod.ProjectileType("BigThorns"), base.projectile.damage, 1f, base.projectile.owner, 1f, 0f);
			Main.projectile[proj].hostile = false;
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].melee = true;
			return true;
		}
	}
}
