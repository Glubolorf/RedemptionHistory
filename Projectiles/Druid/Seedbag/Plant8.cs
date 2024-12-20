using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class Plant8 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shiverthorn");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetSafeDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 42;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 150;
		}

		protected override void PlantAI()
		{
			Player player = Main.player[base.projectile.owner];
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
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 800f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[0] % 30f == 0f && base.projectile.frame >= 4)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 8f), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<ShiverthornIcicle>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
			}
			if (base.projectile.localAI[0] % 40f == 0f && base.projectile.frame >= 4 && this.IsOnNativeTerrain)
			{
				for (int i = 0; i < 8; i++)
				{
					Projectile.NewProjectile(base.projectile.position.X + 16f, base.projectile.position.Y + 8f, (float)(-8 + Main.rand.Next(0, 16)), (float)(-10 + Main.rand.Next(0, 6)), ModContent.ProjectileType<ShiverthornIcicle2>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 80, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		private NPC target;
	}
}
