using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class EggBolt : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionShot[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 10;
			base.projectile.penetrate = 1;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				Main.PlaySound(SoundID.Item20, base.projectile.position);
				base.projectile.localAI[0] = 1f;
			}
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
