using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class Plant7 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icar's Flower");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 80;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 10f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 20f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 30f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 50f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 60f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 70f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 90f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 100f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 110f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 130f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 36f, base.projectile.position.Y + 68f), base.projectile.velocity, base.mod.ProjectileType("IcarsFlowerPetal"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] > 170f)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				base.projectile.Kill();
			}
		}
	}
}
