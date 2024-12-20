using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class Plant10 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Deathweed");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetSafeDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 160;
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
					if (num >= 6)
					{
						base.projectile.frame = 5;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 5)
					{
						base.projectile.frame = 4;
					}
				}
			}
			Player player = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 800f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[0] % 15f == 0f && base.projectile.frame >= 4)
			{
				Projectile.NewProjectile(this.switchPos ? new Vector2(base.projectile.position.X + 4f, base.projectile.position.Y + 12f) : new Vector2(base.projectile.position.X + 24f, base.projectile.position.Y + 14f), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<DeathweedSpike>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
				if (this.switchPos)
				{
					this.switchPos = false;
				}
				else
				{
					this.switchPos = true;
				}
			}
			if (base.projectile.localAI[0] % 35f == 0f && base.projectile.frame >= 4 && this.IsOnNativeTerrain)
			{
				for (int i = 0; i < 8; i++)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-8 + Main.rand.Next(0, 16)), (float)(-10 + Main.rand.Next(0, 6)), ModContent.ProjectileType<DeathweedSpike2>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 240, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		private NPC target;

		public bool switchPos;
	}
}
