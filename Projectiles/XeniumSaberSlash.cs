using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	internal class XeniumSaberSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[base.projectile.type] = 28;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(595);
			this.aiType = 595;
			base.projectile.width = 68;
			base.projectile.height = 64;
			base.projectile.melee = true;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 80;
			base.projectile.penetrate = -1;
		}

		public override void AI()
		{
			int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 74, 0f, 0f, 100, default(Color), 1.2f);
			Main.dust[dustIndex].velocity *= 1.4f;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(0, 200, 0, 0) * (1f - (float)base.projectile.alpha / 255f));
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 3;
		}
	}
}
