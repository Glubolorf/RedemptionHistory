using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class BoneChickenPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone Chicken");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.friendly = true;
			base.projectile.timeLeft = 180;
			base.projectile.penetrate = 1;
		}

		public override bool PreAI()
		{
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 6)
				{
					base.projectile.frame = 0;
				}
			}
			if (base.projectile.velocity.X < 0f)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.4f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.X = projectile2.velocity.X * 1.008f;
			base.projectile.velocity.X = MathHelper.Clamp(base.projectile.velocity.X, -10f, 10f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCHit2.WithVolume(0.3f), base.projectile.position);
			for (int i = 0; i < 2; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].velocity *= 1.9f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (oldVelocity.X != base.projectile.velocity.X)
			{
				base.projectile.velocity.X = -oldVelocity.X;
			}
			return false;
		}
	}
}
