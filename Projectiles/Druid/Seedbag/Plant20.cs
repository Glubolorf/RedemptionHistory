using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class Plant20 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Martian Tree");
			Main.projFrames[base.projectile.type] = 3;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetSafeDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 72;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 220;
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
				if (num >= 3)
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
						Gore.NewGore(new Vector2(base.projectile.position.X + (float)Main.rand.Next(0, 70), base.projectile.position.Y + (float)Main.rand.Next(0, 60)), base.projectile.velocity, base.mod.GetGoreSlot("Gores/v08/MartianTreeFX"), 1f);
					}
					this.leafSpawn = true;
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 40f, base.projectile.position.Y + 14f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<ThornFluff1>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 20f, base.projectile.position.Y + 30f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<ThornFluff1>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 48f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<ThornFluff1>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 62f, base.projectile.position.Y + 30f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<ThornFluff1>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 54f, base.projectile.position.Y + 52f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<ThornFluff1>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(100) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 30f, base.projectile.position.Y + 54f, (float)(-6 + Main.rand.Next(0, 12)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<ThornFluff1>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 4; i++)
			{
				Gore.NewGore(new Vector2(base.projectile.position.X + (float)Main.rand.Next(0, 70), base.projectile.position.Y + (float)Main.rand.Next(0, 60)), base.projectile.velocity, base.mod.GetGoreSlot("Gores/v08/MartianTreeFX"), 1f);
			}
		}

		public bool leafSpawn;
	}
}
