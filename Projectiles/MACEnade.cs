using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MACEnade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Big Electonade");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(30);
			this.aiType = 30;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.timeLeft = 60;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 15)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 3)
				{
					base.projectile.frame = 2;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item93, base.projectile.position);
			for (int i = 0; i < 15; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].velocity *= 1.4f;
			}
			for (int j = 0; j < 10; j++)
			{
				int num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 5f;
				num2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num2].velocity *= 3f;
			}
			Projectile.NewProjectile(base.projectile.position.X, base.projectile.position.Y, 0f, 0f, base.mod.ProjectileType("ElectronadeTeslaFieldH"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
		}
	}
}
