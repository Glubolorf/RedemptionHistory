using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class StarFruitSeed : DruidSeed
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starfruit Seed");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			this.plantID = ModContent.ProjectileType<StarfruitPlant>();
		}

		public override void AI()
		{
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.7f;
			if (base.projectile.velocity.Y >= 10f)
			{
				base.projectile.velocity.Y = 9f;
			}
			float[] localAI = base.projectile.localAI;
			int num = 0;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 >= 40f)
			{
				if (this.plantID != -1)
				{
					Projectile projectile2 = Main.projectile[Projectile.NewProjectile(base.projectile.Top, base.projectile.velocity, this.plantID, base.projectile.damage, 0f, base.projectile.owner, 1f, 1f)];
					projectile2.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = base.projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs;
					projectile2.timeLeft = (int)((float)projectile2.timeLeft * base.projectile.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier);
				}
				base.projectile.Kill();
			}
		}
	}
}
