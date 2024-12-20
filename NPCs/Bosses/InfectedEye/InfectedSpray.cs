using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.InfectedEye
{
	internal class InfectedSpray : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sludge Spray");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = 1;
			base.projectile.alpha = 60;
			base.projectile.timeLeft = 100;
		}

		public override void AI()
		{
			if (base.projectile.timeLeft > 60)
			{
				base.projectile.timeLeft = 60;
			}
			if (base.projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				if (base.projectile.ai[0] == 8f)
				{
					num296 = 0.25f;
				}
				else if (base.projectile.ai[0] == 9f)
				{
					num296 = 0.5f;
				}
				else if (base.projectile.ai[0] == 10f)
				{
					num296 = 0.75f;
				}
				base.projectile.ai[0] += 1f;
				int num297 = 74;
				if (Main.rand.Next(2) == 0)
				{
					for (int num298 = 0; num298 < 4; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, num297, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if (Main.rand.Next(3) == 0)
						{
							Main.dust[num299].noGravity = true;
							Dust expr_DD5D_cp_0 = Main.dust[num299];
							Main.dust[num299].scale *= 2f;
							expr_DD5D_cp_0.velocity.X = expr_DD5D_cp_0.velocity.X * 2f;
							Dust expr_DD7D_cp_0 = Main.dust[num299];
							expr_DD7D_cp_0.velocity.Y = expr_DD7D_cp_0.velocity.Y * 2f;
						}
						Main.dust[num299].scale *= 1f;
						Dust expr_DDE2_cp_0 = Main.dust[num299];
						expr_DDE2_cp_0.velocity.X = expr_DDE2_cp_0.velocity.X * 1.2f;
						Dust expr_DE02_cp_0 = Main.dust[num299];
						expr_DE02_cp_0.velocity.Y = expr_DE02_cp_0.velocity.Y * 1.2f;
						Main.dust[num299].scale *= num296;
						Main.dust[num299].velocity += base.projectile.velocity;
						if (!Main.dust[num299].noGravity)
						{
							Main.dust[num299].velocity *= 0.5f;
						}
					}
				}
			}
			else
			{
				base.projectile.ai[0] += 1f;
			}
			base.projectile.rotation += 0.3f * (float)base.projectile.direction;
		}
	}
}
