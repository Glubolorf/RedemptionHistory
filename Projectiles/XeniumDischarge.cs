using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XeniumDischarge : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Discharge");
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
			base.projectile.timeLeft = 60;
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
			Player player = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha += 10;
			base.projectile.scale += 0.3f;
			if (base.projectile.localAI[0] == 1f)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap1").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 51f, base.projectile.position.Y + 51f), base.projectile.velocity, ModContent.ProjectileType<XeniumDischarge2>(), player.statDefense * 3, 0f, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}
	}
}
