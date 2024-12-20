using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class BlueOrb2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/BlueOrb1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blue Orb");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.alpha = 60;
			base.projectile.magic = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			int DustID2 = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 15, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1.5f);
			Main.dust[DustID2].noGravity = true;
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.localAI[0] < 10f)
			{
				Projectile projectile = base.projectile;
				projectile.velocity.Y = projectile.velocity.Y + -0.6f;
			}
			if (base.projectile.localAI[0] >= 10f)
			{
				Projectile projectile2 = base.projectile;
				projectile2.velocity.Y = projectile2.velocity.Y + 0.6f;
			}
			if (base.projectile.localAI[0] >= 19f)
			{
				base.projectile.localAI[0] = 0f;
			}
		}
	}
}
