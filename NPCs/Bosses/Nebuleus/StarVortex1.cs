using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class StarVortex1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Star Vortex");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 60;
			base.projectile.height = 60;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 100;
		}

		public override void AI()
		{
			base.projectile.alpha++;
			base.projectile.rotation += 0.09f;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			if (Main.rand.Next(6) == 0)
			{
				Main.PlaySound(SoundID.Item8, base.projectile.position);
				int num = 62;
				int num2 = 8;
				for (int i = 0; i < num2; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num3].noLight = false;
					Main.dust[num3].noGravity = true;
				}
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("NebulaStar"), 50, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
		}
	}
}
