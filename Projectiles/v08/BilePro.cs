using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class BilePro : ModProjectile
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
			base.DisplayName.SetDefault("Bile");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = 6;
			base.projectile.extraUpdates = 2;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			base.projectile.scale -= 0.002f;
			if (base.projectile.scale <= 0f)
			{
				base.projectile.Kill();
			}
			if (base.projectile.ai[0] <= 3f)
			{
				base.projectile.ai[0] += 1f;
				return;
			}
			base.projectile.velocity.Y = base.projectile.velocity.Y + 0.075f;
			for (int num151 = 0; num151 < 3; num151++)
			{
				float num152 = base.projectile.velocity.X / 3f * (float)num151;
				float num153 = base.projectile.velocity.Y / 3f * (float)num151;
				int num154 = 14;
				int num155 = Dust.NewDust(new Vector2(base.projectile.position.X + (float)num154, base.projectile.position.Y + (float)num154), base.projectile.width - num154 * 2, base.projectile.height - num154 * 2, 74, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num155].noGravity = true;
				Main.dust[num155].velocity *= 0.1f;
				Main.dust[num155].velocity += base.projectile.velocity * 0.5f;
				Dust dust = Main.dust[num155];
				dust.position.X = dust.position.X - num152;
				Dust dust2 = Main.dust[num155];
				dust2.position.Y = dust2.position.Y - num153;
			}
			if (Main.rand.Next(8) == 0)
			{
				int num156 = 16;
				int num157 = Dust.NewDust(new Vector2(base.projectile.position.X + (float)num156, base.projectile.position.Y + (float)num156), base.projectile.width - num156 * 2, base.projectile.height - num156 * 2, 74, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[num157].velocity *= 0.25f;
				Main.dust[num157].velocity += base.projectile.velocity * 0.5f;
				return;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 6;
			target.AddBuff(base.mod.BuffType("BileDebuff"), 600, false);
		}
	}
}
