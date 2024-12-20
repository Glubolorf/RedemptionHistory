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
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			if (Main.rand.Next(4) == 0)
			{
				Main.PlaySound(SoundID.Item125, base.projectile.position);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 21, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				if (Main.myPlayer == base.projectile.owner)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-12 + Main.rand.Next(0, 24)), (float)(-12 + Main.rand.Next(0, 24)), ModContent.ProjectileType<LastRPro5>(), 400, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			base.projectile.alpha += 2;
			base.projectile.rotation += 1.03f;
		}
	}
}
