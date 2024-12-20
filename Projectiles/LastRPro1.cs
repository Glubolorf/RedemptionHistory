using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class LastRPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Redemptive Circle");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 84;
			base.projectile.height = 84;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 140;
		}

		public override void AI()
		{
			if (Main.rand.Next(4) == 0)
			{
				Main.PlaySound(SoundID.Item125, base.projectile.position);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 21, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Projectile.NewProjectile(base.projectile.position.X + 42f, base.projectile.position.Y + 42f, (float)(-12 + Main.rand.Next(0, 24)), (float)(-12 + Main.rand.Next(0, 24)), base.mod.ProjectileType("LastRPro5"), 400, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 2;
			base.projectile.rotation += 1.03f;
			if (base.projectile.localAI[0] >= 120f)
			{
				base.projectile.Kill();
			}
		}
	}
}
