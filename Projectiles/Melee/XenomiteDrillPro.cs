using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class XenomiteDrillPro : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.aiStyle = 20;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.hide = true;
			base.projectile.ownerHitCheck = true;
			base.projectile.melee = true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Drill");
		}

		public override void AI()
		{
			int DustID2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, ModContent.DustType<XenoDust>(), base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 2.9f);
			Main.dust[DustID2].noGravity = true;
		}
	}
}
