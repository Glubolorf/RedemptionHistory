using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
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

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				int dustType = 78;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				base.projectile.localAI[0] = 1f;
			}
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
				Projectile projectile = Main.projectile[Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.position.Y - 12f), new Vector2(0f, 0f), ModContent.ProjectileType<SaplingPro>(), base.projectile.damage, 4f, base.projectile.owner, 0f, 0f)];
			}
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 78, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.6f;
			}
		}
	}
}
