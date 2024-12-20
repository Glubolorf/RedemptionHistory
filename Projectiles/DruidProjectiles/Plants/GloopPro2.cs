using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class GloopPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Black Gloop");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			base.projectile.velocity.X = 0f;
			base.projectile.velocity.Y = 0f;
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 10)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 8)
				{
					base.projectile.frame = 7;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 20; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<GloopDust>(), 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 6;
		}
	}
}
