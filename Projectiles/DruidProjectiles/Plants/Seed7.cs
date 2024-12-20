using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Seed7 : DruidSeed
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icar's Flower Seed");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 24;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			this.plantID = ModContent.ProjectileType<Plant7>();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.8f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			if (this.plantID != -1)
			{
				Projectile projectile = Main.projectile[Projectile.NewProjectile(base.projectile.Bottom, base.projectile.velocity, this.plantID, base.projectile.damage, 0f, base.projectile.owner, 0f, 1f)];
				projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = base.projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs;
				projectile.timeLeft = (int)((float)projectile.timeLeft * base.projectile.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier);
			}
			return true;
		}

		public override void AI()
		{
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y - 0.7f;
			if (base.projectile.velocity.Y >= -10f)
			{
				base.projectile.velocity.Y = -9f;
			}
			float[] localAI = base.projectile.localAI;
			int num = 0;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 >= 40f)
			{
				if (this.plantID != -1)
				{
					Projectile projectile2 = Main.projectile[Projectile.NewProjectile(base.projectile.Bottom, base.projectile.velocity, this.plantID, base.projectile.damage, 0f, base.projectile.owner, 1f, 1f)];
					projectile2.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = base.projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs;
					projectile2.timeLeft = (int)((float)projectile2.timeLeft * base.projectile.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier);
				}
				base.projectile.Kill();
			}
		}
	}
}
