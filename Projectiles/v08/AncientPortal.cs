using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class AncientPortal : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Portal");
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 70;
			base.projectile.height = 72;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = Projectile.SentryLifeTime;
			base.projectile.sentry = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 1400f, base.projectile.Center, true, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 8f == 0f)
			{
				for (int i = 0; i < 2; i++)
				{
					int num = Main.rand.Next(3);
					if (num == 0)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-35, 35), base.projectile.Center.Y + (float)Main.rand.Next(-35, 35)), RedeHelper.PolarVector(20f, Utils.ToRotation(this.target.Center - base.projectile.Center)), base.mod.ProjectileType("AncientArrowHeadPro"), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
					}
					if (num == 1)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-35, 35), base.projectile.Center.Y + (float)Main.rand.Next(-35, 35)), RedeHelper.PolarVector(20f, Utils.ToRotation(this.target.Center - base.projectile.Center)), base.mod.ProjectileType("AncientDaggerPro"), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
					}
					if (num == 2)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-35, 35), base.projectile.Center.Y + (float)Main.rand.Next(-35, 35)), RedeHelper.PolarVector(20f, Utils.ToRotation(this.target.Center - base.projectile.Center)), base.mod.ProjectileType("AncientStalacmitePro"), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
					}
				}
			}
			for (int j = 0; j < 3; j++)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 34.0);
				this.vector.Y = (float)(Math.Cos(angle) * 35.0);
				Main.dust[Dust.NewDust(base.projectile.Center + this.vector, 1, 1, 269, 0f, 0f, 100, default(Color), 3f)].noGravity = true;
			}
		}

		private NPC target;

		private Vector2 vector;
	}
}
