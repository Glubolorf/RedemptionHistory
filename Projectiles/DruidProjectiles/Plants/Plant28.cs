using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant28 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Common Ground Bush");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 32;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 300;
		}

		protected override void PlantAI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				if (this.IsOnNativeTerrain)
				{
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 7)
					{
						base.projectile.frame = 6;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 6)
					{
						base.projectile.frame = 5;
					}
				}
			}
			Player player = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 800f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 50f == 0f && base.projectile.frame >= 5)
			{
				Projectile.NewProjectile(this.switchPos ? new Vector2(base.projectile.position.X + 6f, base.projectile.position.Y + 14f) : new Vector2(base.projectile.position.X + 26f, base.projectile.position.Y + 12f), RedeHelper.PolarVector(15f, Utils.ToRotation(this.target.Center - base.projectile.Center)), base.mod.ProjectileType("DeathweedSpike"), base.projectile.damage / 2, base.projectile.knockBack, player.whoAmI, 0f, 0f);
				if (this.switchPos)
				{
					this.switchPos = false;
				}
				else
				{
					this.switchPos = true;
				}
			}
			if (base.projectile.localAI[0] % 100f == 0f && base.projectile.frame >= 5 && this.IsOnNativeTerrain)
			{
				for (int i = 0; i < 12; i++)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 16)), (float)(-10 + Main.rand.Next(0, 6)), base.mod.ProjectileType("DeathweedSpike2"), base.projectile.damage / 2, base.projectile.knockBack, player.whoAmI, 0f, 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 27, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		private NPC target;

		public bool switchPos;
	}
}
