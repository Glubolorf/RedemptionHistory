using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class XenomiteEyeballLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Eye-Beam");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 6;
			base.projectile.height = 6;
			base.projectile.aiStyle = 1;
			base.projectile.friendly = true;
			base.projectile.magic = true;
			base.projectile.penetrate = 3;
			base.projectile.timeLeft = 600;
			base.projectile.alpha = 0;
			base.projectile.light = 0.5f;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			int DustID2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, ModContent.DustType<XenoDust>(), base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 2.9f);
			Main.dust[DustID2].noGravity = true;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 2;
			if (base.projectile.localAI[0] > 130f)
			{
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}
	}
}
