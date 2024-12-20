using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class BileContainerPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bile Container");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.4f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item27, base.projectile.position);
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 74, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int j = 0; j < 6; j++)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-4 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(-6, 0)), ModContent.ProjectileType<BilePro>(), base.projectile.damage / 2, 4f, Main.myPlayer, 0f, 0f);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			return true;
		}
	}
}
