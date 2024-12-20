using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions.HoloMinions
{
	public class HoloProjector : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hologram Projector");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 8;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + 0f;
			if (base.projectile.localAI[0] == 30f)
			{
				int num = Main.rand.Next(7);
				if (num == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 40f, 0f, 0f, base.mod.ProjectileType("HoloMinion1"), base.projectile.damage, 5f, Main.myPlayer, 0f, 0f);
				}
				if (num == 1 || num == 4)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 40f, 0f, 0f, base.mod.ProjectileType("HoloMinion2"), base.projectile.damage, 5f, Main.myPlayer, 0f, 0f);
				}
				if (num == 2 || num == 5)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 40f, 0f, 0f, base.mod.ProjectileType("HoloMinion3"), base.projectile.damage, 5f, Main.myPlayer, 0f, 0f);
				}
				if (num == 3 || num == 6)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 40f, 0f, 0f, base.mod.ProjectileType("HoloMinion4"), base.projectile.damage, 5f, Main.myPlayer, 0f, 0f);
				}
			}
			if (base.projectile.localAI[0] == 60f)
			{
				base.projectile.Kill();
			}
		}
	}
}
