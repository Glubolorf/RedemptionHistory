using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class Plant26 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Creation Rose");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 80;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 20)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.velocity.X = 0f;
			base.projectile.velocity.Y = 0f;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 1f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 8f), new Vector2(0f, -8f), base.mod.ProjectileType("CreationRosePetal"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 8f), new Vector2(0f, 8f), base.mod.ProjectileType("CreationRosePetal"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 8f), new Vector2(-8f, 0f), base.mod.ProjectileType("CreationRosePetal"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 8f), new Vector2(8f, 0f), base.mod.ProjectileType("CreationRosePetal"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 16f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 8f), new Vector2(6f, 6f), base.mod.ProjectileType("CreationRosePetal"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 8f), new Vector2(6f, -6f), base.mod.ProjectileType("CreationRosePetal"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 8f), new Vector2(-6f, 6f), base.mod.ProjectileType("CreationRosePetal"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 8f), new Vector2(-6f, -6f), base.mod.ProjectileType("CreationRosePetal"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (base.projectile.localAI[0] == 30f)
			{
				base.projectile.localAI[0] = 0f;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 35; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num].velocity *= 1.4f;
			}
		}
	}
}
