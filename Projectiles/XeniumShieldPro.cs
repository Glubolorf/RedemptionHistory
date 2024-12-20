using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XeniumShieldPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Barrier");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 100;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 220;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).xeniumBarrier)
			{
				base.projectile.Kill();
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.rotation += 0.04f;
			Player player = Main.player[base.projectile.owner];
			base.projectile.position.X = player.Center.X - 50f;
			base.projectile.position.Y = player.Center.Y - 50f;
			if (base.projectile.localAI[0] == 1f)
			{
				int num = 74;
				int num2 = 20;
				for (int i = 0; i < num2; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num3].noLight = false;
					Main.dust[num3].noGravity = true;
				}
				for (int j = 0; j < num2; j++)
				{
					int num4 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 2f);
					Main.dust[num4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (float)j / (float)num2 * 6.28f);
					Main.dust[num4].noLight = false;
					Main.dust[num4].noGravity = true;
				}
				for (int k = 0; k < num2; k++)
				{
					int num5 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 2f);
					Main.dust[num5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)k / (float)num2 * 6.28f);
					Main.dust[num5].noLight = false;
					Main.dust[num5].noGravity = true;
				}
				Main.PlaySound(SoundID.Item62, (int)base.projectile.position.X, (int)base.projectile.position.Y);
			}
			if (player.dead)
			{
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			int num = 74;
			int num2 = 20;
			for (int i = 0; i < num2; i++)
			{
				int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)num2 * 6.28f);
				Main.dust[num3].noLight = false;
				Main.dust[num3].noGravity = true;
			}
			for (int j = 0; j < num2; j++)
			{
				int num4 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 2f);
				Main.dust[num4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (float)j / (float)num2 * 6.28f);
				Main.dust[num4].noLight = false;
				Main.dust[num4].noGravity = true;
			}
			for (int k = 0; k < num2; k++)
			{
				int num5 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 2f);
				Main.dust[num5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)k / (float)num2 * 6.28f);
				Main.dust[num5].noLight = false;
				Main.dust[num5].noGravity = true;
			}
			Main.PlaySound(SoundID.Item62, (int)base.projectile.position.X, (int)base.projectile.position.Y);
		}
	}
}
