using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class Plant30 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Plant");
			Main.projFrames[base.projectile.type] = 6;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetSafeDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 22;
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
					if (num >= 6)
					{
						base.projectile.frame = 5;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 5)
					{
						base.projectile.frame = 4;
					}
				}
			}
			Point point = Utils.ToTileCoordinates(base.projectile.Bottom);
			if (!Main.dayTime && Main.tile[point.X, point.Y + 1].type == 2)
			{
				this.IsOnNativeTerrain = true;
			}
			else
			{
				this.IsOnNativeTerrain = false;
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.frame >= 4)
			{
				if (Main.rand.Next(30) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 10f, (float)(-4 + Main.rand.Next(0, 3)), (float)(1 + Main.rand.Next(0, 3)), ModContent.ProjectileType<NightshadeMiniSkull>(), base.projectile.damage, 0f, base.projectile.owner, (float)(this.IsOnNativeTerrain ? 1 : 0), 0f);
				}
				if (Main.rand.Next(30) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 20f, base.projectile.position.Y + 10f, (float)(1 + Main.rand.Next(0, 3)), (float)(-5 + Main.rand.Next(0, 3)), ModContent.ProjectileType<NightshadeMiniSkull>(), base.projectile.damage, 0f, base.projectile.owner, (float)(this.IsOnNativeTerrain ? 1 : 0), 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.8f;
			}
		}
	}
}
