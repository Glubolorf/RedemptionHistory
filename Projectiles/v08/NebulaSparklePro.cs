using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class NebulaSparklePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebula Sparkle");
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
			base.projectile.timeLeft = 120;
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
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f);
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
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<NebulaSparklePro2>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			int dustType = 58;
			int pieCut = 4;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 3.14f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 58;
			int pieCut = 4;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}
	}
}
