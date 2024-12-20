using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class BloodOrbPro3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blood Orb");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 300;
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
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.localAI[0] += 1f;
			if (base.projectile.velocity.X < 0f)
			{
				Projectile projectile = base.projectile;
				projectile.velocity.X = projectile.velocity.X + 0.1f;
			}
			if (base.projectile.velocity.X > 0f)
			{
				Projectile projectile2 = base.projectile;
				projectile2.velocity.X = projectile2.velocity.X - 0.1f;
			}
			if (base.projectile.velocity.Y < 0f)
			{
				Projectile projectile3 = base.projectile;
				projectile3.velocity.Y = projectile3.velocity.Y + 0.1f;
			}
			if (base.projectile.velocity.Y > 0f)
			{
				Projectile projectile4 = base.projectile;
				projectile4.velocity.Y = projectile4.velocity.Y - 0.1f;
			}
			int num = (int)base.projectile.ai[0];
			Vector2 vector;
			vector..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num2 = Main.player[num].Center.X - vector.X;
			float num3 = Main.player[num].Center.Y - vector.Y;
			float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
			if (num4 < 22f && base.projectile.position.X < Main.player[num].position.X + (float)Main.player[num].width && base.projectile.position.X + (float)base.projectile.width > Main.player[num].position.X && base.projectile.position.Y < Main.player[num].position.Y + (float)Main.player[num].height && base.projectile.position.Y + (float)base.projectile.height > Main.player[num].position.Y)
			{
				if (base.projectile.owner == Main.myPlayer)
				{
					Main.player[num].statLife++;
					Main.player[num].HealEffect(1, true);
				}
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			int num = 235;
			int num2 = 20;
			for (int i = 0; i < num2; i++)
			{
				int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)num2 * 6.28f);
				Main.dust[num3].noLight = false;
				Main.dust[num3].noGravity = true;
			}
		}
	}
}
