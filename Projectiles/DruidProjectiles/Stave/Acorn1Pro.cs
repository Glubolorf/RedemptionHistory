using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Stave
{
	public class Acorn1Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Acorn");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(24);
			this.aiType = 24;
			base.projectile.melee = false;
			base.projectile.ranged = false;
			base.projectile.thrown = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Point point = Utils.ToTileCoordinates(base.projectile.Bottom);
			if (Main.tile[point.X, point.Y + 1].type == 2 || Main.tile[point.X, point.Y].type == 2)
			{
				Projectile projectile = Main.projectile[Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.position.Y - 12f), new Vector2(0f, 0f), base.mod.ProjectileType("SaplingPro"), base.projectile.damage, 4f, base.projectile.owner, 0f, 0f)];
			}
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 78, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.6f;
			}
		}
	}
}
