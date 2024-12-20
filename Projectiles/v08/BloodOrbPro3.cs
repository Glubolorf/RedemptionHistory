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
			Projectile projectile = base.projectile;
			int num489 = projectile.frameCounter + 1;
			projectile.frameCounter = num489;
			if (num489 >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num489 = projectile2.frame + 1;
				projectile2.frame = num489;
				if (num489 >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.localAI[0] += 1f;
			if (base.projectile.velocity.X < 0f)
			{
				Projectile projectile3 = base.projectile;
				projectile3.velocity.X = projectile3.velocity.X + 0.1f;
			}
			if (base.projectile.velocity.X > 0f)
			{
				Projectile projectile4 = base.projectile;
				projectile4.velocity.X = projectile4.velocity.X - 0.1f;
			}
			if (base.projectile.velocity.Y < 0f)
			{
				Projectile projectile5 = base.projectile;
				projectile5.velocity.Y = projectile5.velocity.Y + 0.1f;
			}
			if (base.projectile.velocity.Y > 0f)
			{
				Projectile projectile6 = base.projectile;
				projectile6.velocity.Y = projectile6.velocity.Y - 0.1f;
			}
			int num487 = (int)base.projectile.ai[0];
			Vector2 vector36 = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num490 = Main.player[num487].Center.X - vector36.X;
			float num488 = Main.player[num487].Center.Y - vector36.Y;
			if ((float)Math.Sqrt((double)(num490 * num490 + num488 * num488)) < 22f && base.projectile.position.X < Main.player[num487].position.X + (float)Main.player[num487].width && base.projectile.position.X + (float)base.projectile.width > Main.player[num487].position.X && base.projectile.position.Y < Main.player[num487].position.Y + (float)Main.player[num487].height && base.projectile.position.Y + (float)base.projectile.height > Main.player[num487].position.Y)
			{
				if (base.projectile.owner == Main.myPlayer)
				{
					Main.player[num487].statLife++;
					Main.player[num487].HealEffect(1, true);
				}
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 235;
			int pieCut = 20;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}
	}
}
