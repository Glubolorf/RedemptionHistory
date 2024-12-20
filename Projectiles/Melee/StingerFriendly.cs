using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class StingerFriendly : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Stinger");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.friendly = true;
			base.projectile.penetrate = 3;
			base.projectile.hostile = false;
			base.projectile.tileCollide = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
			{
				lightColor,
				Color.ForestGreen,
				lightColor
			}));
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Utils.NextBool(Main.rand, 3))
			{
				target.AddBuff(20, 300, false);
			}
		}

		public override void Kill(int timeLeft)
		{
			int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 2, 0f, 0f, 100, default(Color), 1.2f);
			Main.dust[dustIndex].velocity *= 0.4f;
		}
	}
}
