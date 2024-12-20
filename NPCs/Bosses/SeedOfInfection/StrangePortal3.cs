using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.SeedOfInfection
{
	public class StrangePortal3 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/SeedOfInfection/StrangePortal";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Strange Portal");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 42;
			base.projectile.height = 42;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 80;
			base.projectile.alpha = 150;
		}

		public override void AI()
		{
			base.projectile.scale += 1f;
			base.projectile.alpha += 2;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 15)
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
			if (base.projectile.ai[0] == 0f)
			{
				base.projectile.rotation += 0.09f;
			}
			else
			{
				base.projectile.rotation -= 0.09f;
			}
			base.projectile.velocity.X = 0f;
			base.projectile.velocity.Y = 0f;
		}
	}
}
