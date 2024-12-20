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
				int dustType = 58;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustType2 = 59;
				int pieCut2 = 10;
				for (int j = 0; j < pieCut2; j++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)j / (float)pieCut2 * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				int dustType3 = 60;
				int pieCut3 = 12;
				for (int k = 0; k < pieCut3; k++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType3, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)pieCut3 * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
				int dustType4 = 62;
				int pieCut4 = 14;
				for (int l = 0; l < pieCut4; l++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType4, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)pieCut4 * 6.28f);
					Main.dust[dustID4].noLight = false;
					Main.dust[dustID4].noGravity = true;
				}
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<NebDefeated>(), 0, 3f, 255, 0f, 0f);
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 58, 0f, 0f, 100, default(Color), 4f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}
	}
}
