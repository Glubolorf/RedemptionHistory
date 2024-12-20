using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class StarVortexTele : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Star Vortex");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 60;
			base.projectile.height = 60;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 5f)
			{
				for (int i = 0; i < 4; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 58, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (float)i / 4f * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 15f)
			{
				for (int j = 0; j < 4; j++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 58, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(18f, 0f), (float)j / 4f * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
			}
			if (base.projectile.localAI[0] == 30f)
			{
				Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, ModContent.ProjectileType<StarVortex>(), base.projectile.damage, base.projectile.knockBack, 255, 0f, 0f);
			}
		}
	}
}
