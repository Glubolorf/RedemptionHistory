using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles
{
	public class DarkBoltPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 12;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			base.projectile.rotation += 0.09f;
			int DustID2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 27, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[DustID2].noGravity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.myPlayer == base.projectile.owner)
			{
				Projectile.NewProjectile(base.projectile.Top, base.projectile.velocity, ModContent.ProjectileType<DarkSeed>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
		}
	}
}
