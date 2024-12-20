using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Stave
{
	public class MoonlordStaveBoom : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Explosion");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 200;
			base.projectile.height = 200;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = true;
			base.projectile.timeLeft = 3;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			for (int i = 0; i < 8; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 229, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 0f;
				Main.dust[dustIndex].noGravity = true;
			}
		}
	}
}
