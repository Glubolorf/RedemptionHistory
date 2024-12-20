using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class CandleLightPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Candle Light");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 180;
			this.drawOriginOffsetY = -8;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.7f / 255f, (float)(255 - base.projectile.alpha) * 0.7f / 255f, (float)(255 - base.projectile.alpha) * 0.8f / 255f);
			base.projectile.velocity *= 0.96f;
			if (Main.rand.Next(5) == 0)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 1f);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.Y = dust.velocity.Y * -2f;
				Main.dust[dustIndex].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 120, false);
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 261;
			int pieCut = 20;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 6f, base.projectile.Center.Y - 6f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(2f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}
	}
}
