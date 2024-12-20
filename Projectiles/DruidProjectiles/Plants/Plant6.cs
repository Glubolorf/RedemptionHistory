using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant6 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crimthorn Bush");
			Main.projFrames[base.projectile.type] = 2;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 58;
			base.projectile.height = 64;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.hide = true;
			base.projectile.timeLeft = 600;
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
			if (this.IsOnNativeTerrain ? (base.projectile.localAI[0] >= 20f) : (base.projectile.localAI[0] >= 60f))
			{
				base.projectile.frameCounter++;
			}
			if (base.projectile.frame > 0)
			{
				base.projectile.friendly = true;
				if (!this.dustSpawn)
				{
					for (int i = 0; i < 10; i++)
					{
						Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 125, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
					}
					this.dustSpawn = true;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 125, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		public bool dustSpawn;
	}
}
