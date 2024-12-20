using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class GlowingMushrooms1 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Glowing Mushrooms");
			Main.projFrames[base.projectile.type] = 7;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
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
					if (num >= 7)
					{
						base.projectile.frame = 6;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 6)
					{
						base.projectile.frame = 5;
					}
				}
			}
			base.projectile.localAI[0] += 1f;
			if (!this.IsOnNativeTerrain)
			{
				if (base.projectile.localAI[0] % 60f == 0f && base.projectile.frame >= 5)
				{
					Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, ModContent.ProjectileType<GlowingMushroomSpores1>(), 0, 0f, base.projectile.owner, 0f, 0f);
					return;
				}
			}
			else if (base.projectile.localAI[0] % 40f == 0f && base.projectile.frame >= 5)
			{
				for (int i = -2; i <= 2; i++)
				{
					Projectile.NewProjectile(base.projectile.Center, 2f * Utils.RotatedBy(Vector2.UnitX, 1.5707963267948966 * (double)i, default(Vector2)), ModContent.ProjectileType<GlowingMushroomSpores1>(), 0, 0f, base.projectile.owner, 0f, 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 240, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}
	}
}
