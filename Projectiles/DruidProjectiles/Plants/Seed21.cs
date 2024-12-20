using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Seed21 : DruidSeed
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Seed");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 32;
			base.projectile.height = 32;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				this.plantID = ModContent.ProjectileType<Plant21>();
			}
			if (num == 1)
			{
				this.plantID = ModContent.ProjectileType<Plant22>();
			}
			if (num == 2)
			{
				this.plantID = ModContent.ProjectileType<Plant23>();
			}
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			if (this.plantID != -1)
			{
				Projectile projectile = Main.projectile[Projectile.NewProjectile(base.projectile.Top, base.projectile.velocity, this.plantID, base.projectile.damage, 0f, base.projectile.owner, 0f, 1f)];
				projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = base.projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs;
				projectile.timeLeft = (int)((float)projectile.timeLeft * base.projectile.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier);
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 262, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.8f;
			}
		}
	}
}
