using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid
{
	public class BionadePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bionade");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(24);
			this.aiType = 24;
			base.projectile.width = 14;
			base.projectile.height = 34;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 60)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 3)
				{
					base.projectile.frame = 2;
				}
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y + 0.4f;
			if (base.projectile.localAI[0] >= 60f && Main.myPlayer == base.projectile.owner && Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-4 + Main.rand.Next(0, 4)), ModContent.ProjectileType<GreenGasPro3>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, base.projectile.position);
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int j = 0; j < 15; j++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dustIndex2].velocity *= 1.4f;
			}
			Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-4 + Main.rand.Next(0, 4)), ModContent.ProjectileType<GreenGasPro3>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), ModContent.ProjectileType<GreenGasPro3>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), ModContent.ProjectileType<GreenGasPro3>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), ModContent.ProjectileType<GreenGasPro3>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
		}
	}
}
