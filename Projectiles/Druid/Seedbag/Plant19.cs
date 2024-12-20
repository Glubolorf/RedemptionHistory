using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class Plant19 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eye Stalk Plant");
			Main.projFrames[base.projectile.type] = 11;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetSafeDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 56;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
			base.projectile.hide = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}

		protected override void PlantAI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				if (this.IsOnNativeTerrain)
				{
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 11)
					{
						base.projectile.frame = 10;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 10)
					{
						base.projectile.frame = 9;
					}
				}
			}
			if (Main.bloodMoon)
			{
				this.IsOnNativeTerrain = true;
			}
			if (base.projectile.frame >= 8)
			{
				if (this.IsOnNativeTerrain)
				{
					if (Main.rand.Next(50) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 8f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-8 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(50) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 28f, base.projectile.position.Y + 16f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-8 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(50) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 22f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-8 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(50) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 30f, base.projectile.position.Y + 24f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-8 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(50) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 38f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-8 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(50) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 26f, base.projectile.position.Y + 34f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-8 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
						return;
					}
				}
				else
				{
					if (Main.rand.Next(100) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 8f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(100) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 28f, base.projectile.position.Y + 16f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(100) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 22f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(100) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 30f, base.projectile.position.Y + 24f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(100) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 38f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
					if (Main.rand.Next(100) == 0)
					{
						Projectile.NewProjectile(base.projectile.position.X + 26f, base.projectile.position.Y + 34f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<PoisonTearPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
					}
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 5, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}
	}
}
