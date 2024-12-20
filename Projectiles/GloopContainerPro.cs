using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class GloopContainerPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloop Container");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.7f;
			if (base.projectile.velocity.Y >= 10f)
			{
				base.projectile.velocity.Y = 9f;
			}
			if (base.projectile.localAI[0] > 35f)
			{
				for (int i = 0; i < 25; i++)
				{
					int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, base.mod.DustType("GloopDust"), 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num].velocity *= 1.4f;
				}
				Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				}
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
				}
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (int i = 0; i < 25; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, base.mod.DustType("GloopDust"), 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num].velocity *= 1.4f;
			}
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			return true;
		}
	}
}
