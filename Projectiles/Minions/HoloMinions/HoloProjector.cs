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
			Projectile projectile3 = base.projectile;
			projectile3.velocity.X = projectile3.velocity.X * 0f;
			Projectile projectile4 = base.projectile;
			projectile4.velocity.Y = projectile4.velocity.Y + 0f;
			if (base.projectile.localAI[0] == 30f)
			{
				int minionChoice = Main.rand.Next(7);
				if (minionChoice == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 40f, 0f, 0f, ModContent.ProjectileType<HoloMinion1>(), base.projectile.damage, 5f, Main.myPlayer, 0f, 0f);
				}
				if (minionChoice == 1 || minionChoice == 4)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 40f, 0f, 0f, ModContent.ProjectileType<HoloMinion2>(), base.projectile.damage, 5f, Main.myPlayer, 0f, 0f);
				}
				if (minionChoice == 2 || minionChoice == 5)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 40f, 0f, 0f, ModContent.ProjectileType<HoloMinion3>(), base.projectile.damage, 5f, Main.myPlayer, 0f, 0f);
				}
				if (minionChoice == 3 || minionChoice == 6)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y - 40f, 0f, 0f, ModContent.ProjectileType<HoloMinion4>(), base.projectile.damage, 5f, Main.myPlayer, 0f, 0f);
				}
			}
			if (base.projectile.localAI[0] == 60f)
			{
				base.projectile.Kill();
			}
		}
	}
}
