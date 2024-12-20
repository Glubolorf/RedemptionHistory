using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.Druid.Stave
{
	public class SaplingPro : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sapling");
			Main.projFrames[base.projectile.type] = 6;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 34;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
			base.projectile.hide = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = false;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
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
			if (num >= 10)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 6)
				{
					base.projectile.frame = 5;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 78, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].velocity *= 1.6f;
			}
		}
	}
}
