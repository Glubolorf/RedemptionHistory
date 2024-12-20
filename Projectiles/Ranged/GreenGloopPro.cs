using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Ranged
{
	public class GreenGloopPro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/GreenGloopPro";
			}
		}

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
			base.projectile.ignoreWater = false;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 3)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y + 0.6f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath1, base.projectile.position);
			for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			if (Main.myPlayer == base.projectile.owner)
			{
				for (int j = 0; j < Main.rand.Next(2, 5); j++)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-2 + Main.rand.Next(0, 4)), (float)(-4 + Main.rand.Next(0, 4)), ModContent.ProjectileType<GreenGasPro>(), base.projectile.damage, 0f, base.projectile.owner, 0f, 0f);
				}
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
