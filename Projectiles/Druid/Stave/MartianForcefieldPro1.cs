using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
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
			base.projectile.alpha = 150;
			base.projectile.timeLeft = 600;
		}

		public override void AI()
		{
			Projectile worldTree = Main.projectile[(int)base.projectile.ai[0]];
			base.projectile.Center = worldTree.Center;
			base.projectile.velocity = Vector2.Zero;
			base.projectile.rotation += 0.04f;
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && !proj.friendly && !proj.minion && proj.velocity.X != 0f && proj.velocity.Y != 0f)
				{
					proj.velocity.X = -proj.velocity.X;
					proj.velocity.Y = -proj.velocity.Y;
				}
			}
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(ModContent.BuffType<MartianShieldBuff>(), 30, false);
				}
			}
		}

		private Player clearCheck;
	}
}
