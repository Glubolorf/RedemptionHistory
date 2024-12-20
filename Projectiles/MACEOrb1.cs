using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MACEOrb1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Orb");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
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
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 30f)
			{
				base.projectile.velocity.X = 0f;
			}
			if (base.projectile.localAI[0] == 60f)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm2").WithVolume(0.5f), base.projectile.position);
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y + 92f, 0f, 0f, ModContent.ProjectileType<MACEOrbLaser2>(), 40, 3f, 255, 0f, 0f);
			}
			if (base.projectile.localAI[0] >= 64f && base.projectile.localAI[0] % 4f == 0f && base.projectile.localAI[0] <= 80f)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y + 92f, 0f, 0f, ModContent.ProjectileType<MACEOrbLaser2>(), 40, 3f, 255, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap1").WithVolume(0.5f).WithPitchVariance(0.1f), base.projectile.position);
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y + 92f, 0f, 0f, ModContent.ProjectileType<MACEOrbLaser1>(), 34, 3f, 255, 0f, 0f);
		}
	}
}
