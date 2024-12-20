using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class SeedNadePro : ModProjectile
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
			base.projectile.thrown = false;
			base.projectile.ranged = false;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int j = 0; j < 10; j++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex2].noGravity = true;
				Main.dust[dustIndex2].velocity *= 5f;
				dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex2].velocity *= 3f;
			}
			if (Main.myPlayer == base.projectile.owner)
			{
				int choice = Main.rand.Next(10);
				if (choice == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed1>(), 24, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 1)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed3>(), 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 2)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed4>(), 31, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 3)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed5>(), 34, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 4)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed6>(), 31, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 5)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed8>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 6)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed9>(), 24, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 7)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed10>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 8)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed13>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 9)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed14>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 10)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed29>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				int num = Main.rand.Next(8);
				if (num == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed1>(), 24, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 1)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed3>(), 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 2)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed4>(), 31, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 3)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed5>(), 34, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 4)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed6>(), 31, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 5)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed8>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num == 6)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed9>(), 24, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 7)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed10>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 8)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed13>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 9)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed14>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 10)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed29>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				int num2 = Main.rand.Next(10);
				if (num2 == 0)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed1>(), 24, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num2 == 1)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed3>(), 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num2 == 2)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed4>(), 31, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num2 == 3)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed5>(), 34, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num2 == 4)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed6>(), 31, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num2 == 5)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed8>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (num2 == 6)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed9>(), 24, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 7)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed10>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 8)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed13>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 9)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed14>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (choice == 10)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed29>(), 26, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().moreSeeds)
				{
					int num3 = Main.rand.Next(10);
					if (num3 == 0)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed1>(), 24, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num3 == 1)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed3>(), 0, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num3 == 2)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed4>(), 31, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num3 == 3)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed5>(), 34, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num3 == 4)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed6>(), 31, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num3 == 5)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed8>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num3 == 6)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed9>(), 24, 1f, Main.myPlayer, 0f, 0f);
					}
					if (choice == 7)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed10>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					if (choice == 8)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed13>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					if (choice == 9)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed14>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					if (choice == 10)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed29>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					int num4 = Main.rand.Next(10);
					if (num4 == 0)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed1>(), 24, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num4 == 1)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed3>(), 0, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num4 == 2)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed4>(), 31, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num4 == 3)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed5>(), 34, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num4 == 4)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed6>(), 31, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num4 == 5)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed8>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					if (num4 == 6)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed9>(), 24, 1f, Main.myPlayer, 0f, 0f);
					}
					if (choice == 7)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed10>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					if (choice == 8)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed13>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					if (choice == 9)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed14>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
					if (choice == 10)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<Seed29>(), 26, 1f, Main.myPlayer, 0f, 0f);
					}
				}
			}
		}
	}
}
