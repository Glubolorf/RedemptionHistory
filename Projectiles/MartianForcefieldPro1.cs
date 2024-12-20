using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MartianForcefieldPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Martian Forcefield");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 280;
			base.projectile.height = 280;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 100;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.rotation += 0.04f;
			if (base.projectile.localAI[0] >= 600f)
			{
				base.projectile.Kill();
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && !proj.friendly && !proj.minion && proj.velocity.X != 0f && proj.velocity.Y != 0f)
				{
					proj.velocity.X = -proj.velocity.X;
					proj.velocity.Y = -proj.velocity.Y;
				}
			}
			int num487 = (int)base.projectile.ai[0];
			Vector2 vector36 = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num489 = Main.player[num487].Center.X - vector36.X;
			float num488 = Main.player[num487].Center.Y - vector36.Y;
			Math.Sqrt((double)(num489 * num489 + num488 * num488));
			if (base.projectile.position.X < Main.player[num487].position.X + (float)Main.player[num487].width && base.projectile.position.X + (float)base.projectile.width > Main.player[num487].position.X && base.projectile.position.Y < Main.player[num487].position.Y + (float)Main.player[num487].height && base.projectile.position.Y + (float)base.projectile.height > Main.player[num487].position.Y && base.projectile.owner == Main.myPlayer)
			{
				Main.player[num487].AddBuff(base.mod.BuffType("MartianShieldBuff"), 30, false);
			}
		}
	}
}
