using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Petridish
{
	public class PetridishPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petridish");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.4f;
			if (base.projectile.localAI[0] > 40f)
			{
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(13, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 4, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 4f, (float)(-3 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(0, 7)), base.mod.ProjectileType("Bacteria1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 4f, (float)(-3 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(0, 7)), base.mod.ProjectileType("Bacteria2"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 4f, (float)(-3 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(0, 7)), base.mod.ProjectileType("Bacteria3"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 4f, (float)(-3 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(0, 7)), base.mod.ProjectileType("Bacteria1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 4f, (float)(-4 + Main.rand.Next(0, 9)), (float)(-4 + Main.rand.Next(0, 9)), base.mod.ProjectileType("Bacteria2"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 8f, base.projectile.position.Y + 4f, (float)(-4 + Main.rand.Next(0, 9)), (float)(-4 + Main.rand.Next(0, 9)), base.mod.ProjectileType("Bacteria3"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
		}
	}
}
