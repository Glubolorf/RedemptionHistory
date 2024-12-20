using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant29 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Golden Orange Tree");
			Main.projFrames[base.projectile.type] = 2;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 70;
			base.projectile.height = 78;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 240;
			base.projectile.hide = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}

		protected override void PlantAI()
		{
			if (base.projectile.frameCounter >= 20)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile = base.projectile;
				int num = projectile.frame + 1;
				projectile.frame = num;
				if (num >= 2)
				{
					base.projectile.frame = 1;
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 40f)
			{
				base.projectile.frameCounter++;
			}
			if (base.projectile.frame > 0)
			{
				if (!this.leafSpawn)
				{
					for (int i = 0; i < 10; i++)
					{
						Gore.NewGore(new Vector2(base.projectile.position.X + (float)Main.rand.Next(0, 70), base.projectile.position.Y + (float)Main.rand.Next(0, 48)), base.projectile.velocity, base.mod.GetGoreSlot("Gores/v08/OrangeTreeFX"), 1f);
					}
					this.leafSpawn = true;
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 22f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 46f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 44f, base.projectile.position.Y + 40f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 56f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 60f, base.projectile.position.Y + 48f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-8 + Main.rand.Next(0, 16)), base.mod.ProjectileType("GoldenOrange"), base.projectile.damage, 7f, base.projectile.owner, 0f, 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 4; i++)
			{
				Gore.NewGore(new Vector2(base.projectile.position.X + (float)Main.rand.Next(0, 70), base.projectile.position.Y + (float)Main.rand.Next(0, 48)), base.projectile.velocity, base.mod.GetGoreSlot("Gores/v08/OrangeTreeFX"), 1f);
			}
			for (int j = 0; j < 6; j++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 10, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		public bool leafSpawn;
	}
}
