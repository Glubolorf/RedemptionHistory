using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SeedNade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Seed Grenade");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(30);
			this.aiType = 30;
			base.projectile.timeLeft = 200;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			for (int i = 0; i < 15; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].velocity *= 1.4f;
			}
			for (int j = 0; j < 10; j++)
			{
				int num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 5f;
				num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num2].velocity *= 3f;
			}
			int num3 = Main.rand.Next(10);
			if (num3 == 0)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed1"), 4, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num3 == 1)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed3"), 0, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num3 == 2)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed4"), 11, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num3 == 3)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed5"), 14, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num3 == 4)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed6"), 11, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num3 == 5)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed8"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num3 == 6)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed9"), 4, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num3 == 7)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed10"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			int num4 = Main.rand.Next(8);
			if (num4 == 0)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed1"), 4, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 1)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed3"), 0, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 2)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed4"), 11, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 3)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed5"), 14, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 4)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed6"), 11, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 5)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed8"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 6)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed9"), 4, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 7)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed10"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 8)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed13"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num4 == 9)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed14"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			int num5 = Main.rand.Next(10);
			if (num5 == 0)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed1"), 4, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 1)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed3"), 0, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 2)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed4"), 11, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 3)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed5"), 14, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 4)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed6"), 11, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 5)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed8"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 6)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed9"), 4, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 7)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed10"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 8)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed13"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			if (num5 == 9)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed14"), 6, 1f, Main.myPlayer, 0f, 0f);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).moreSeeds)
			{
				int num6 = Main.rand.Next(10);
				if (num6 == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed1"), 4, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 1)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed3"), 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 2)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed4"), 11, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 3)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed5"), 14, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 4)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed6"), 11, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 5)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed8"), 6, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 6)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed9"), 4, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 7)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed10"), 6, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 8)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed13"), 6, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num6 == 9)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed14"), 6, 1f, Main.myPlayer, 0f, 0f);
				}
				int num7 = Main.rand.Next(10);
				if (num7 == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed1"), 4, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 1)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed3"), 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 2)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed4"), 11, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 3)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed5"), 14, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 4)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed6"), 11, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 5)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed8"), 6, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 6)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed9"), 4, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 7)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed10"), 6, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 8)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed13"), 6, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num7 == 9)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("Seed14"), 6, 1f, Main.myPlayer, 0f, 0f);
				}
			}
		}
	}
}
