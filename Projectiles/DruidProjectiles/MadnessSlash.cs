using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles
{
	internal class MadnessSlash : ModProjectile
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
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 60;
			base.projectile.penetrate = -1;
			base.projectile.melee = false;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			int DustID2 = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, ModContent.DustType<VlitchFlame>(), base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1.5f);
			Main.dust[DustID2].noGravity = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(200, 0, 0, 0) * (1f - (float)base.projectile.alpha / 255f));
		}
	}
}
