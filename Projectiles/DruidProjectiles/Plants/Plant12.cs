using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant12 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Shard");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.alpha = 35;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 170;
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
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 7)
				{
					base.projectile.frame = 2;
				}
			}
			Projectile projectile3 = base.projectile;
			projectile3.velocity.X = projectile3.velocity.X * 0f;
			Projectile projectile4 = base.projectile;
			projectile4.velocity.Y = projectile4.velocity.Y * 0f;
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 800f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 15f == 0f && base.projectile.frame >= 2)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(15f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<XenoShard>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, ModContent.DustType<XenoDust>(), base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		private NPC target;
	}
}
