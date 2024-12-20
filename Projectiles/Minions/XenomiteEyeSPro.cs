using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class XenomiteEyeSPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionShot[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.penetrate = -1;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 20, 1f, 0f);
				base.projectile.localAI[0] = 1f;
			}
			int num666 = 8;
			int num667 = Dust.NewDust(new Vector2(base.projectile.position.X + (float)num666 + 6f, base.projectile.position.Y + (float)num666), base.projectile.width - num666 * 2, base.projectile.height - num666 * 2, 66, 0f, 0f, 0, new Color(88, 255, 0), 1.5f);
			Main.dust[num667].velocity *= 0.5f;
			Main.dust[num667].velocity += base.projectile.velocity * 0.5f;
			Main.dust[num667].noGravity = true;
			Main.dust[num667].noLight = false;
			Main.dust[num667].scale = 1f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.projectile.penetrate = -1;
			base.projectile.maxPenetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.position += base.projectile.velocity;
			base.projectile.velocity = Vector2.Zero;
			base.projectile.timeLeft = 180;
			return false;
		}
	}
}
