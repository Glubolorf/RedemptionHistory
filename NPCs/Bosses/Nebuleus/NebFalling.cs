using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class NebFalling : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus, Angel of the Cosmos");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 76;
			base.projectile.height = 82;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = true;
		}

		public override void AI()
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 58, 0f, 0f, 100, default(Color), 3f);
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			base.projectile.rotation -= 0.01f;
			if (base.projectile.localAI[0] == 1f)
			{
				int num = 58;
				int num2 = 8;
				for (int i = 0; i < num2; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num3].noLight = false;
					Main.dust[num3].noGravity = true;
				}
				int num4 = 59;
				int num5 = 10;
				for (int j = 0; j < num5; j++)
				{
					int num6 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num4, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)j / (float)num5 * 6.28f);
					Main.dust[num6].noLight = false;
					Main.dust[num6].noGravity = true;
				}
				int num7 = 60;
				int num8 = 12;
				for (int k = 0; k < num8; k++)
				{
					int num9 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num7, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)num8 * 6.28f);
					Main.dust[num9].noLight = false;
					Main.dust[num9].noGravity = true;
				}
				int num10 = 62;
				int num11 = 14;
				for (int l = 0; l < num11; l++)
				{
					int num12 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num10, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num12].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)num11 * 6.28f);
					Main.dust[num12].noLight = false;
					Main.dust[num12].noGravity = true;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("NebDefeated"), 0, 3f, 255, 0f, 0f);
			for (int i = 0; i < 25; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 58, 0f, 0f, 100, default(Color), 4f);
				Main.dust[num].velocity *= 1.9f;
			}
		}
	}
}
