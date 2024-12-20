using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class GreenGloopPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Green Gloop");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.ranged = true;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 3)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.6f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath1, base.projectile.position);
			for (int i = 0; i < 30; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num].velocity *= 1.4f;
			}
			Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-4 + Main.rand.Next(0, 4)), base.mod.ProjectileType("GreenGasPro"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-4 + Main.rand.Next(0, 4)), base.mod.ProjectileType("GreenGasPro"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("GreenGasPro"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("GreenGasPro"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-2 + Main.rand.Next(0, 4)), (float)(-2 + Main.rand.Next(0, 4)), base.mod.ProjectileType("GreenGasPro"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}
	}
}
