using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class JanitorBucketPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bucket");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(24);
			this.aiType = 24;
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.penetrate = 1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			if (Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-6 + Main.rand.Next(0, 6)), (float)(-8 + Main.rand.Next(0, 8)), ModContent.ProjectileType<WaterSplash>(), base.projectile.damage / 2, 0f, base.projectile.owner, 0f, 1f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 8; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 8, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}
	}
}
