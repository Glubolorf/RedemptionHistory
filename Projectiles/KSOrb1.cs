using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class KSOrb1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Electric Orb");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 100;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.2f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.localAI[0] += 1f;
			if (base.projectile.alpha >= 120 && base.projectile.localAI[0] < 120f)
			{
				base.projectile.alpha -= 5;
			}
			if (base.projectile.localAI[0] == 60f)
			{
				NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, base.mod.NPCType("SpaceKeeper"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (base.projectile.localAI[0] >= 120f)
			{
				base.projectile.alpha += 5;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
		}
	}
}
